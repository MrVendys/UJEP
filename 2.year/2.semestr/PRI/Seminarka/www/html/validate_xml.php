<?php
// validate_xml.php

function validateXML($xmlFile, $xsdFile) {
    $xml = new DOMDocument();
    $xml->load($xmlFile);

    $xsd = new DOMDocument();
    $xsd->load($xsdFile);

    if ($xml->schemaValidate($xsdFile)) {
        echo "XML is valid according to the schema.";
    } else {
        echo "XML is not valid according to the schema.";
        libxml_use_internal_errors(true);
        $errors = libxml_get_errors();
        foreach ($errors as $error) {
            echo "<br>", $error->message;
        }
        libxml_clear_errors();
    }
}

?>
