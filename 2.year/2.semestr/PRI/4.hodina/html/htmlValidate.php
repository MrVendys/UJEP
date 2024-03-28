<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>HTML Validation</title>
    <style>
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

        .button {
            text-decoration: none;
            color: #fff;
            padding: 10px 20px;
            background-color: #4CAF50;
            border-radius: 4px;
            margin: 10px 10px;
            transition: background-color 0.3s ease;
        }

        .button:hover {
            background-color: #45a049;
        }

        .button-row {
            display: flex;
            justify-content: flex-start;
            align-items: center;
            flex-wrap: wrap;
        }
    </style>
</head>

<body>
    <header>
        <div class="navbar">
            <div class="navbar-title">
                XML Viewer
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

    <div class="button-row">
        <?php $files = glob('xml/*.xml');
        foreach ($files as $file) {
            $filename = pathinfo($file, PATHINFO_FILENAME);
            echo "<a href='?file=$filename' class='button'>$filename   </a>";
        } ?>
    </div>
    <?php
    if (isset($_GET["file"])) {
        $file = "xml/" . $_GET["file"] . ".xml";
        $file_xsl = "xml/" . $_GET["file"] . ".xsl";

        $xml = new DOMDocument;
        $xml->load($file);
        $xsl = new DOMDocument;
        $xsl->load($file_xsl);
        $xslt = new XSLTProcessor();
        $xslt->importStylesheet($xsl);
        $transformovany_xml = $xslt->transformToXml($xml);
        echo $transformovany_xml;
    }

    ?>

</body>

</html>