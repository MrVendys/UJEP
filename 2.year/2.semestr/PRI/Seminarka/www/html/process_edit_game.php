<?php
// Check if form is submitted
if ($_SERVER["REQUEST_METHOD"] == "POST") {
    
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
        die("All fields are required.");
    }
    
    // Load current games.xml
    $xml = new DOMDocument();
    $xml->load('games.xml');
    
    // Find and update <game> element
    $games = $xml->getElementsByTagName('game');
    $gameUpdated = false;

    foreach ($games as $game) {
        $gameTitle = $game->getElementsByTagName('title')->item(0)->textContent;
        if ($gameTitle === $title) {
            $game->getElementsByTagName('developer')->item(0)->textContent = $developer;
            $game->getElementsByTagName('publisher')->item(0)->textContent = $publisher;
            $game->getElementsByTagName('platform')->item(0)->textContent = $platform;
            $game->getElementsByTagName('release_date')->item(0)->textContent = $release_date;
            $game->getElementsByTagName('genre')->item(0)->textContent = $genre;
            $gameUpdated = true;
            break;
        }
    }
    
    // Save updated XML back to file if game was updated
    if ($gameUpdated) {
        $xml->save('games.xml');
        header('Location: games.html');
        echo "Game details updated successfully.";
    } else {
        echo "Failed to update game details.";
    }
    
} else {
    // Handle if form is not submitted
    echo "Form not submitted.";
}
header('Location: games.html');
?>
