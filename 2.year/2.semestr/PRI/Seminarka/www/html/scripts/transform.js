document.addEventListener('DOMContentLoaded', function() {
    // Load XML file
    var xmlhttp = new XMLHttpRequest();
    xmlhttp.open('GET', '../xml/games.xml', false);
    xmlhttp.send();
    var xmlDoc = xmlhttp.responseXML;

    // Load XSL file
    var xslhttp = new XMLHttpRequest();
    xslhttp.open('GET', '../xml/games.xsl', false);
    xslhttp.send();
    var xslDoc = xslhttp.responseXML;

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
