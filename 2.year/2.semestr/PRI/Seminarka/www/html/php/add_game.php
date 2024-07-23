<?php
// Validace XML dokumentu proti XSL
function validateXML($xmlFile, $xsdFile) {
    $xml = new DOMDocument();
    $xml->load($xmlFile);

    $xsd = new DOMDocument();
    $xsd->load($xsdFile);

    if ($xml->schemaValidate($xsdFile)) {
        return true; // XML je v pořádku podle schématu XSL
    } else {
        return false; // XML není v pořádku podle schématu XSL
    }
}

// Kontrola, jestli byl form odeslán
if ($_SERVER["REQUEST_METHOD"] == "POST") {
    
    // Inicializace proměnné pro notifikaci
    $notification = "";
    
    // Získání dat z formuláře
    $title = htmlspecialchars($_POST['title']);
    $developer = htmlspecialchars($_POST['developer']);
    $publisher = htmlspecialchars($_POST['publisher']);
    $platforms = htmlspecialchars($_POST['platform']);
    $release_date = htmlspecialchars($_POST['release_date']);
    $genre = htmlspecialchars($_POST['genre']);
    
    // Validování dat
    if (empty($title) || empty($developer) || empty($publisher) || empty($platforms) || empty($release_date) || empty($genre)) {
        $notification = "All fields are required.";
    } else {
        // Validace XML proti XSD
        $xmlFile = '../xml/games.xml';
        $xsdFile = '../xml/games.xsd';
        if (!validateXML($xmlFile, $xsdFile)) {
            $notification = "Failed to validate XML against XSD.";
        } else {
            // Načtení XML games.xml
            $xml = new DOMDocument();
            $xml->load($xmlFile);
            
            $root = $xml->documentElement;
            $maxId = 0;

            // Naleznutí elementu s největším id (tudíž poslední element)
            $gameElements = $root->getElementsByTagName('game');
            foreach ($gameElements as $game) {
                $id = $game->getAttribute('id');
                if ($id !== null && $id !== '') {
                    $maxId = max($maxId, intval($id));
                }
            }

            // Počítání nového ID
            $newId = $maxId + 1;

            // Vytvoření nového <game> elementu
            $newGame = $xml->createElement('game');
            $newGame->setAttribute('id', $newId);

            // Vytvoření dětského elementu a přidání do <game> elementu
            $newGame->appendChild($xml->createElement('title', $title));
            $newGame->appendChild($xml->createElement('developer', $developer));
            $newGame->appendChild($xml->createElement('publisher', $publisher));

            // Vytvoření elementu <plarforms> na uložení více elementu <platform> (Pc, Xbox..)
            $platformsElement = $xml->createElement('platforms');

            // Rozdělení textu hracích platforem podle ","
            $platformList = explode(',', $platforms);
            
            // Oddělení prázdných znaků
            foreach ($platformList as $platform) {
                $platform = trim(ucfirst(strtolower($platform)));
                $platformElement = $xml->createElement('platform', $platform);
                $platformsElement->appendChild($platformElement);
            }

            // Přidání elementu <platforms> do nového elementu <game>
            $newGame->appendChild($platformsElement);

            $newGame->appendChild($xml->createElement('release_date', $release_date));
            $newGame->appendChild($xml->createElement('genre', $genre));
            
            // Přidání <game> do hlavního (kořenového) elementu <games>
            $xml->documentElement->appendChild($newGame);
            
            // Uložení aktualizovaného xml dokumentu
            if ($xml->save('../xml/games.xml')) {
                // Přesměrování zpátky na stránku games.html
                header('Location: ../games.html');
                exit;
            } else {
                $notification = "Failed to save game data.";
            }
        }
    }
    
    // Zobrazení notifikace při nějkém problému
    if (!empty($notification)) {
        echo '<script>alert("' . $notification . '"); window.history.back();</script>';
        exit;
    }
    
} else {
    echo "Form not submitted.";
}
?>
