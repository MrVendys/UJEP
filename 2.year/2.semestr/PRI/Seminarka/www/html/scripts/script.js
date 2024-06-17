document.addEventListener('DOMContentLoaded', function() {
    var gameTitle = getParameterByName('game');
    
    if (gameTitle) {
        loadXMLDoc('xml/games.xml', function(xmlDoc) {
            var game = findGameDetails(xmlDoc, gameTitle);
            if (game) {
                displayGameDetails(game);
            } else {
                displayErrorMessage();
            }
        });
    } else {
        displayNoGameSelectedMessage();
    }
});

function loadXMLDoc(url, callback) {
    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function() {
        if (this.readyState == 4 && this.status == 200) {
            var xmlDoc = this.responseXML;
            callback(xmlDoc);
        }
    };
    xhttp.open('GET', url, true);
    xhttp.send();
}

function findGameDetails(xmlDoc, gameTitle) {
    var games = xmlDoc.getElementsByTagName('game');
    for (var i = 0; i < games.length; i++) {
        var title = games[i].getElementsByTagName('title')[0].textContent;
        if (title === gameTitle) {
            return {
                title: title,
                developer: games[i].getElementsByTagName('developer')[0].textContent,
                publisher: games[i].getElementsByTagName('publisher')[0].textContent,
                platform: games[i].getElementsByTagName('platform')[0].textContent,
                release_date: games[i].getElementsByTagName('release_date')[0].textContent,
                genre: games[i].getElementsByTagName('genre')[0].textContent
            };
        }
    }
    return null;
}

function displayGameDetails(game) {
    var gameDetailsDiv = document.getElementById('gameDetails');
    gameDetailsDiv.innerHTML = `
        <p><strong>Title:</strong> ${game.title}</p>
        <p><strong>Developer:</strong> ${game.developer}</p>
        <p><strong>Publisher:</strong> ${game.publisher}</p>
        <p><strong>Platform:</strong> ${game.platform}</p>
        <p><strong>Release Date:</strong> ${game.release_date}</p>
        <p><strong>Genre:</strong> ${game.genre}</p>
    `;
}

function displayErrorMessage() {
    var gameDetailsDiv = document.getElementById('gameDetails');
    gameDetailsDiv.innerHTML = '<p>Game details not found.</p>';
}

function displayNoGameSelectedMessage() {
    var gameDetailsDiv = document.getElementById('gameDetails');
    gameDetailsDiv.innerHTML = '<p>No game selected.</p>';
}

function getParameterByName(name) {
    var urlParams = new URLSearchParams(window.location.search);
    return urlParams.get(name);
}
