<?php
$xml = new DOMDocument;
$xml->load('../xml/studium.xml');

$xsl = new DOMDocument;
$xsl->load('../xml/studium.xsl');

$xslt = new XSLTProcessor();
$xslt->importStylesheet($xsl);

header('Content-Type: application/xml');
echo $xslt->transformToXml($xml);