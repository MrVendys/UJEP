<?php
// Kontrola, zda byl formulář odeslán
if ($_SERVER["REQUEST_METHOD"] == "POST") {
    
    // Získání dat z formuláře
    $title = htmlspecialchars($_POST['title']);
    $developer = htmlspecialchars($_POST['developer']);
    $publisher = htmlspecialchars($_POST['publisher']);
    $platforms = htmlspecialchars($_POST['platform']);
    $release_date = htmlspecialchars($_POST['release_date']);
    $genre = htmlspecialchars($_POST['genre']);
    
    // Validace a sanitizace dat
    if (empty($title) || empty($developer) || empty($publisher) || empty($platforms) || empty($release_date) || empty($genre)) {
        die("Všechna pole jsou povinná.");
    }
    
    // Načtení aktuálního games.xml
    $xml = new DOMDocument();
    $xml->load('../xml/games.xml');
    
    // Nalezení a aktualizace elementu <game>
    $games = $xml->getElementsByTagName('game');
    $gameUpdated = false;

    foreach ($games as $game) {
        $gameTitle = $game->getElementsByTagName('title')->item(0)->textContent;
        if ($gameTitle === $title) {
            $game->getElementsByTagName('developer')->item(0)->textContent = $developer;
            $game->getElementsByTagName('publisher')->item(0)->textContent = $publisher;
            $platformsNode = $game->getElementsByTagName('platforms')->item(0);

            // Odstranění existujících platforem
            while ($platformsNode->hasChildNodes()) {
                $platformsNode->removeChild($platformsNode->firstChild);
            }

            // Rozdělení platforem podle "," a vytvoření odpovídajících elementů <platform>
            $platformArray = explode(',', $platforms);
            foreach ($platformArray as $platformItem) {
                $platformItem = trim($platformItem); 
                $newPlatformElement = $xml->createElement('platform', $platformItem);
                $platformsNode->appendChild($newPlatformElement);
            }
            $game->getElementsByTagName('release_date')->item(0)->textContent = $release_date;
            $game->getElementsByTagName('genre')->item(0)->textContent = $genre;
            $gameUpdated = true;
            break;
        }
    }
    
    // Uložení aktualizovaného XML zpět do souboru, pokud byla hra aktualizována
    if ($gameUpdated) {
        $xml->save('../xml/games.xml');
        header('Location: ../games.html');
        echo "Detaily hry byly úspěšně aktualizovány.";
    } else {
        echo "Nepodařilo se aktualizovat detaily hry.";
    }
    
} else {
    // Ošetření, pokud formulář nebyl odeslán
    echo "Formulář nebyl odeslán.";
}
header('Location: ../games.html');
?>
