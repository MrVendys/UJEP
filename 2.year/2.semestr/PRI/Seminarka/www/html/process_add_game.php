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
    $platform = htmlspecialchars($_POST['platform']);
    $release_date = htmlspecialchars($_POST['release_date']);
    $genre = htmlspecialchars($_POST['genre']);
    
    // Validate and sanitize data (add more validation as needed)
    // Example: Check if required fields are not empty
    if (empty($title) || empty($developer) || empty($publisher) || empty($platform) || empty($release_date) || empty($genre)) {
        $notification = "All fields are required.";
    } else {
        // Validate XML against XSD
        $xmlFile = 'games.xml';
        $xsdFile = 'games.xsd';
        if (!validateXML($xmlFile, $xsdFile)) {
            $notification = "Failed to validate XML against XSD.";
        } else {
            // Load current games.xml
            $xml = new DOMDocument();
            $xml->load($xmlFile);
            
            // Create new <game> element
            $newGame = $xml->createElement('game');
            
            // Create child elements and append to <game> element
            $newGame->appendChild($xml->createElement('title', $title));
            $newGame->appendChild($xml->createElement('developer', $developer));
            $newGame->appendChild($xml->createElement('publisher', $publisher));
            $newGame->appendChild($xml->createElement('platform', $platform));
            $newGame->appendChild($xml->createElement('release_date', $release_date));
            $newGame->appendChild($xml->createElement('genre', $genre));
            
            // Append <game> to <games> element
            $xml->documentElement->appendChild($newGame);
            
            // Save updated XML back to file
            if ($xml->save('games.xml')) {
                // Redirect back to game list page on success
                header('Location: games.html');
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
