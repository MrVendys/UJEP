<!DOCTYPE html>
<html lang="cs">

<?php $title = 'XML validátor' ?>

<head>
    <title>
        <?= $title ?>
    </title>
    <style>
        .form-container {
            display: flex;
            flex-wrap: wrap;
            justify-content: space-between;
            margin: 2% auto;
            max-width: 90%;
        }

        .form-wrapper {
            width: 100%;
            margin-bottom: 2%;
            box-sizing: border-box;
        }

        .form-wrapper form {
            width: 100%;
            border: 1px solid #ccc;
            padding: 20px;
            background-color: #f9f9f9;
            text-align: center;
        }

        .form-wrapper input[type="file"] {
            width: calc(100% - 22px);
        }

        .form-wrapper input[type="submit"] {
            background-color: #4CAF50;
            color: white;
            padding: 10px 20px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            width: 50%;
            box-sizing: border-box;
            margin: 0 auto;
        }

        .form-wrapper input[type="submit"]:hover {
            background-color: #45a049;
        }

        @media screen and (min-width: 800px) {
            .form-container {
                flex-direction: row;
            }

            .form-wrapper {
                width: 48%;
            }
        }

        td {
            padding-bottom: 10px;
        }

        header {
            padding: 10px 0;
        }

        .navbar {
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin: 0 auto;
            padding: 0 20px;
        }

        .navbar-title {
            font-size: 2rem;
            text-decoration: none;
        }

        nav ul {
            list-style-type: none;
            margin: 0;
            padding: 0;
            display: flex;
        }

        nav ul li {
            margin-left: 20px;
        }

        nav ul li a {
            text-decoration: none;
            color: #fff;
            padding: 10px 20px;
            background-color: #4CAF50;
            border-radius: 4px;
            transition: background-color 0.3s ease;
        }

        nav ul li a:hover {
            background-color: #45a049;
        }
    </style>

</head>

<body>
    <header>
        <div class="navbar">
            <div class="navbar-title">
                <?= $title ?>
            </div>
            <nav>
                <ul>
                    <li><a href="index.php">Validator</a></li>
                    <li><a href="htmlValidate.php">XML Viewer</a></li>
                </ul>
            </nav>
        </div>
    </header>
    <hr>
    <div class="form-container">
        <div class="form-wrapper">
            <h1>Validátor pomocí DTD</h1>
            <p>Nahrajte XML soubor, případně také DTD soubor.</p>
            <form enctype="multipart/form-data" method="POST">
                <table>
                    <tr>
                        <td class="cell">XML soubor:</td>
                        <td><input type="file" name="xml" accept="text/xml" data-max-file-size="2M"></td>
                    </tr>
                    <tr class="cell">
                        <td>DTD soubor:</td>
                        <td><input type="file" name="dtd" data-max-file-size="2M"></td>
                    </tr>
                </table>
                <input type="submit" value="Odeslat"></td>
            </form>
        </div>

        <div class="form-wrapper">
            <h1>Validátor pomocí XSD</h1>
            <p>Nahrajte XML soubor, případně také XSD soubor.</p>
            <form enctype="multipart/form-data" method="POST">
                <table>
                    <tr class="cell">
                        <td>XML soubor:</td>
                        <td><input type="file" name="xml" accept="text/xml" data-max-file-size="2M"></td>
                    </tr>
                    <tr class="cell">
                        <td>XSD soubor:</td>
                        <td><input type="file" name="xsd" data-max-file-size="2M"></td>
                    </tr>
                </table>
                <input type="submit" value="Odeslat"></td>
            </form>
        </div>
    </div>
    <?php
    function printErrors()
    { ?>
        <table>
            <?php foreach (libxml_get_errors() as $error) { ?>
                <tr>
                    <td>
                        <?= $error->line ?>
                    </td>
                    <td>
                        <?= $error->message ?>
                    </td>
                </tr>
            <?php } ?>
        </table>
        <?php
    }

    function validateDTD($xmlPath, $dtdPath = '')
    {
        $doc = new DOMDocument;

        // proběhne kontrola well-formed
        libxml_use_internal_errors(true);
        $doc->loadXML(file_get_contents($xmlPath));
        printErrors();
        libxml_use_internal_errors(false);

        // Máme root a DTD?
        @$root = $doc->firstElementChild->tagName;
        if ($root && $dtdPath) {
            $root = $doc->firstElementChild->tagName;
            $systemId = 'data://text/plain;base64,' . base64_encode(file_get_contents($dtdPath));

            echo "<p>Validuji podle DTD. Kořen: <b>$root</b></p>";

            // inject DTD into XML
            $creator = new DOMImplementation;
            $doctype = $creator->createDocumentType($root, '', $systemId);
            $newDoc = $creator->createDocument(null, '', $doctype);
            $newDoc->encoding = "utf-8";

            $oldRootNode = $doc->getElementsByTagName($root)->item(0);
            $newRootNode = $newDoc->importNode($oldRootNode, true);

            $newDoc->appendChild($newRootNode);
            $doc = $newDoc;
        }

        // validace
        libxml_use_internal_errors(true);
        $isValid = $doc->validate();
        printErrors();
        libxml_use_internal_errors(false);

        return $isValid;
    }

    function validateXsd($xmlPath, $xsdPath = '')
    {
        $doc = new DOMDocument;
        libxml_use_internal_errors(true);
        $doc->loadXML(file_get_contents($xmlPath));
        printErrors();
        libxml_use_internal_errors(false);

        $isValid = false;
        if ($xsdPath) {
            libxml_use_internal_errors(true);
            $isValid = $doc->schemaValidate($xsdPath);
            printErrors();
            libxml_use_internal_errors(false);
        }

        return $isValid;
    }

    $xmlFile = @$_FILES['xml'];
    $dtdFile = @$_FILES['dtd'];
    $xsdFile = @$_FILES['xsd'];
    if (@$xmlTmpName = $xmlFile['tmp_name']) {
        if (@$dtdTmpName = $dtdFile['tmp_name']) {
            $isValid = validateDTD($xmlTmpName, $dtdTmpName);
            if ($isValid)
                echo "Nahraný XML soubor je validní.";
        }

        if (@$xsdTmpName = $xsdFile['tmp_name']) {
            $isValid = validateXsd($xmlTmpName, $xsdTmpName);
            if ($isValid)
                echo "Nahraný XML soubor je validní.";
        }
    }
    ?>
</body>

</html>