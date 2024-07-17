<?php
// Function to validate XML against XSD
function validateXML($xmlFile, $xsdFile) {
    $xml = new DOMDocument();
    $xml->load($xmlFile);

    $xsd = new DOMDocument();
    $xsd->load($xsdFile);

    if ($xml->schemaValidate($xsdFile)) {
        return true; // XML is valid according to the schema
    } else {
        return false; // XML is not valid according to the schema
    }
}

// Check if form is submitted
if ($_SERVER["REQUEST_METHOD"] == "POST") {
    
    // Initialize notification variable
    $notification = "";
    
    // Retrieve form data
    $title = htmlspecialchars($_POST['title']);
    $developer = htmlspecialchars($_POST['developer']);
    $publisher = htmlspecialchars($_POST['publisher']);
    $platforms = htmlspecialchars($_POST['platform']);
    $release_date = htmlspecialchars($_POST['release_date']);
    $genre = htmlspecialchars($_POST['genre']);
    
    // Validate and sanitize data
    if (empty($title) || empty($developer) || empty($publisher) || empty($platforms) || empty($release_date) || empty($genre)) {
        $notification = "All fields are required.";
    } else {
        // Validate XML against XSD
        $xmlFile = '../xml/games.xml';
        $xsdFile = '../xml/games.xsd';
        if (!validateXML($xmlFile, $xsdFile)) {
            $notification = "Failed to validate XML against XSD.";
        } else {
            // Load current games.xml
            $xml = new DOMDocument();
            $xml->load($xmlFile);
            
            $root = $xml->documentElement;
            $maxId = 0;

            // Find the highest id in the existing game elements
            $gameElements = $root->getElementsByTagName('game');
            foreach ($gameElements as $game) {
                $id = $game->getAttribute('id');
                if ($id !== null && $id !== '') {
                    $maxId = max($maxId, intval($id));
                }
            }

            // Calculate the new ID
            $newId = $maxId + 1;

            // Create new <game> element
            $newGame = $xml->createElement('game');
            $newGame->setAttribute('id', $newId);

            // Create child elements and append to <game> element
            $newGame->appendChild($xml->createElement('title', $title));
            $newGame->appendChild($xml->createElement('developer', $developer));
            $newGame->appendChild($xml->createElement('publisher', $publisher));

            // Create <platforms> element to hold multiple platform entries
            $platformsElement = $xml->createElement('platforms');

            // Split the platforms string into an array
            $platformList = explode(',', $platforms);

            // Trim whitespace and create individual <platform> elements
            foreach ($platformList as $platform) {
                $platform = trim(ucfirst(strtolower($platform)));
                $platformElement = $xml->createElement('platform', $platform);
                $platformsElement->appendChild($platformElement);
            }

            // Append the <platforms> element to the new <game> element
            $newGame->appendChild($platformsElement);

            $newGame->appendChild($xml->createElement('release_date', $release_date));
            $newGame->appendChild($xml->createElement('genre', $genre));
            
            // Append <game> to <games> element
            $xml->documentElement->appendChild($newGame);
            
            // Save updated XML back to file
            if ($xml->save('../xml/games.xml')) {
                // Redirect back to game list page on success
                header('Location: ../games.html');
                exit;
            } else {
                $notification = "Failed to save game data.";
            }
        }
    }
    
    // Display notification if there was an error
    if (!empty($notification)) {
        echo '<script>alert("' . $notification . '"); window.history.back();</script>';
        exit;
    }
    
} else {
    // Handle if form is not submitted
    echo "Form not submitted.";
}
?>
