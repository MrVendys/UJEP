document.addEventListener('DOMContentLoaded', function() {
    // Načtení souboru XML
    var xmlhttp = new XMLHttpRequest();
    xmlhttp.open('GET', '../xml/games.xml', false);
    xmlhttp.send();
    var xmlDoc = xmlhttp.responseXML;

    // Načtení souboru XSL
    var xslhttp = new XMLHttpRequest();
    xslhttp.open('GET', '../xml/games.xsl', false);
    xslhttp.send();
    var xslDoc = xslhttp.responseXML;

    // Transformace XML pomocí XSLT
    var processor = new XSLTProcessor();
    processor.importStylesheet(xslDoc);
    var resultDoc = processor.transformToDocument(xmlDoc);

    // Extrakce transformovaného HTML obsahu
    var htmlContent = new XMLSerializer().serializeToString(resultDoc);

    // Zobrazení transformovaného obsahu v těle tabulky
    var gameList = document.getElementById('gameList');
    gameList.innerHTML = htmlContent;
});
