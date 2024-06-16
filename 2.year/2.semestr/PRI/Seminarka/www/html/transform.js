document.addEventListener('DOMContentLoaded', function() {
    // Load XML file
    var xmlhttp = new XMLHttpRequest();
    xmlhttp.open('GET', 'games.xml', false);
    xmlhttp.send();
    var xmlDoc = xmlhttp.responseXML;

    // Load XSL file
    var xslhttp = new XMLHttpRequest();
    xslhttp.open('GET', 'games.xsl', false);
    xslhttp.send();
    var xslDoc = xslhttp.responseXML;

    // Validate XML against XSD (optional)
    // Note: JavaScript does not have built-in XML schema validation, so this typically
    //       happens on the server-side in PHP or other server-side languages.

    // Transform XML using XSLT
    var processor = new XSLTProcessor();
    processor.importStylesheet(xslDoc);
    var resultDoc = processor.transformToDocument(xmlDoc);

    // Extract transformed HTML content
    var htmlContent = new XMLSerializer().serializeToString(resultDoc);

    // Display transformed content in the table body
    var gameList = document.getElementById('gameList');
    gameList.innerHTML = htmlContent;
});
