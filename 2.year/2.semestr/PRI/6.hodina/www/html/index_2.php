<?php
$filenameBase = '../xml/studium_e';

$xmlFile = "../xml/studium.xml";
$xsdFile = "../xml/studium.xsd";
$xslFile = "$filenameBase.xsl";

$xml = new DOMDocument;
$xml->load($xmlFile);
$xml->schemaValidate($xsdFile);

$xsl = new DOMDocument;
$xsl->load($xslFile);

$xslt = new XSLTProcessor();
$xslt->importStylesheet($xsl);
$xml = $xslt->transformToXml($xml);

echo $xml;