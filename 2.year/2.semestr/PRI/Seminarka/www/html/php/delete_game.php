<?php
if ($_SERVER["REQUEST_METHOD"] == "POST") {
    // Získání názvu hry k odstranění
    $title = htmlspecialchars($_POST['title']);
    
    if (empty($title)) {
        die("Název hry je povinný.");
    }
    
    // Načtení souboru XML
    $xml = new DOMDocument();
    $xml->load('../xml/games.xml');
    
    // Nalezení uzlu hry k odstranění
    $xpath = new DOMXPath($xml);
    $query = "//game[title='" . $title . "']";
    $gameNode = $xpath->query($query)->item(0);
    
    if (!$gameNode) {
        die("Hra nebyla nalezena.");
    }
    
    // Odstranění uzlu hry
    $gameNode->parentNode->removeChild($gameNode);
    
    // Uložení aktualizovaného XML zpět do souboru
    $xml->save('../xml/games.xml');
    
    // Odpověď s úspěchem
    header('Location: ../games.html');
    echo "Hra byla úspěšně odstraněna.";
} else {
    // Ošetření, pokud metoda požadavku není POST
    header('Location: ../games.html');
    echo "Neplatná metoda požadavku.";
}
?>
