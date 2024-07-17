document.addEventListener('DOMContentLoaded', function() {
    var searchForm = document.getElementById('searchForm');
    searchForm.addEventListener('submit', function(event) {
        event.preventDefault();
        var searchTitle = document.getElementById('searchTitle').value.trim();
        if (searchTitle) {
            searchGame(searchTitle);
        } else {
            alert('Please enter a game title to search.');
        }
    });
});

function searchGame(title) {
    loadXMLDoc('../xml/games.xml', function(xmlDoc) {
        var games = xmlDoc.getElementsByTagName('game');
        var results = [];

        for (var i = 0; i < games.length; i++) {
            var gameTitle = games[i].getElementsByTagName('title')[0].textContent;
            if (gameTitle.toLowerCase().includes(title.toLowerCase())) {
                results.push({
                    title: gameTitle,
                    developer: games[i].getElementsByTagName('developer')[0].textContent,
                    publisher: games[i].getElementsByTagName('publisher')[0].textContent,
                    platform: games[i].getElementsByTagName('platform')[0].textContent,
                    release_date: games[i].getElementsByTagName('release_date')[0].textContent,
                    genre: games[i].getElementsByTagName('genre')[0].textContent
                });
            }
        }

        displaySearchResults(results);
    });
}

function displaySearchResults(results) {
    var searchResultsDiv = document.getElementById('searchResults');
    if (results.length > 0) {
        var html = '<h2>Search Results</h2><ul class="list-group">';
        results.forEach(function(game) {
            html += `
                <li class="list-group-item">
                    <h4>${game.title}</h4>
                    <p><strong>Developer:</strong> ${game.developer}</p>
                    <p><strong>Publisher:</strong> ${game.publisher}</p>
                    <p><strong>Platform:</strong> ${game.platform}</p>
                    <p><strong>Release Date:</strong> ${game.release_date}</p>
                    <p><strong>Genre:</strong> ${game.genre}</p>
                    <button class="btn btn-success" onclick="editGame('${game.title}')">Edit</button>
                    <button class="btn btn-danger ml-2" onclick="deleteGame('${game.title}')">Delete</button>
                </li>
            `;
        });
        html += '</ul>';
        searchResultsDiv.innerHTML = html;
    } else {
        searchResultsDiv.innerHTML = '<p>No results found.</p>';
    }
}

function editGame(title) {
    // Redirect to edit_game_details.html with game title as a query parameter
    window.location.href = `../edit_game_details.html?game=${encodeURIComponent(title)}`;
}

function deleteGame(title) {
    if (confirm(`Are you sure you want to delete the game "${title}"?`)) {
        var xhr = new XMLHttpRequest();
        xhr.onreadystatechange = function() {
            if (this.readyState == 4) {
                if (this.status == 200) {
                    alert('Game deleted successfully.');
                    removeGameFromUI(title); // Update UI
                } else {
                    alert('Error deleting game.');
                }
            }
        };
        xhr.open('POST', '../php/delete_game.php', true);
        xhr.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
        xhr.send('title=' + encodeURIComponent(title));
    }
}

function removeGameFromUI(title) {
    var gameElements = document.querySelectorAll('.list-group-item');
    gameElements.forEach(function(gameElement) {
        if (gameElement.querySelector('h4').textContent === title) {
            gameElement.parentNode.removeChild(gameElement);
        }
    });
}


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
