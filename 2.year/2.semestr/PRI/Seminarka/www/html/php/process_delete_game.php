<?php
if ($_SERVER["REQUEST_METHOD"] == "POST") {
    // Retrieve title of the game to delete
    $title = htmlspecialchars($_POST['title']);
    
    if (empty($title)) {
        die("Game title is required.");
    }
    
    // Load XML file
    $xml = new DOMDocument();
    $xml->load('../xml/games.xml');
    
    // Find the game node to delete
    $xpath = new DOMXPath($xml);
    $query = "//game[title='" . $title . "']";
    $gameNode = $xpath->query($query)->item(0);
    
    if (!$gameNode) {
        die("Game not found.");
    }
    
    // Remove the game node
    $gameNode->parentNode->removeChild($gameNode);
    
    // Save the updated XML back to file
    $xml->save('../xml/games.xml');
    
    // Respond with success
    header('Location: ../games.html');
    echo "Game deleted successfully.";
} else {
    // Handle if request method is not POST
    header('Location: ../games.html');
    echo "Invalid request method.";
}
?>
