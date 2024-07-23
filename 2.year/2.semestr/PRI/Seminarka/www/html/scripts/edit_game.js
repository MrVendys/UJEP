document.addEventListener('DOMContentLoaded', function() {
    var searchForm = document.getElementById('searchForm');
    searchForm.addEventListener('submit', function(event) {
        event.preventDefault();
        var searchTitle = document.getElementById('searchTitle').value.trim();
        if (searchTitle) {
            searchGame(searchTitle);
        } else {
            alert('Prosím zadejte název hry k vyhledání.');
        }
    });
});

// Funkce pro vyhledání hry podle názvu
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

// Funkce pro zobrazení výsledků vyhledávání
function displaySearchResults(results) {
    var searchResultsDiv = document.getElementById('searchResults');
    if (results.length > 0) {
        var html = '<h2>Výsledky vyhledávání</h2><ul class="list-group">';
        results.forEach(function(game) {
            html += `
                <li class="list-group-item">
                    <h4>${game.title}</h4>
                    <p><strong>Vývojář:</strong> ${game.developer}</p>
                    <p><strong>Vydavatel:</strong> ${game.publisher}</p>
                    <p><strong>Platforma:</strong> ${game.platform}</p>
                    <p><strong>Datum vydání:</strong> ${game.release_date}</p>
                    <p><strong>Žánr:</strong> ${game.genre}</p>
                    <button class="btn btn-success" onclick="editGame('${game.title}')">Editovat</button>
                    <button class="btn btn-danger ml-2" onclick="deleteGame('${game.title}')">Smazat</button>
                </li>
            `;
        });
        html += '</ul>';
        searchResultsDiv.innerHTML = html;
    } else {
        searchResultsDiv.innerHTML = '<p>Žádné výsledky nebyly nalezeny.</p>';
    }
}

// Funkce pro úpravu hry
function editGame(title) {
    // Přesměrování na edit_game_details.html s názvem hry jako parametrem dotazu
    window.location.href = `../edit_game_details.html?game=${encodeURIComponent(title)}`;
}

// Funkce pro smazání hry
function deleteGame(title) {
    if (confirm(`Opravdu chcete smazat hru "${title}"?`)) {
        var xhr = new XMLHttpRequest();
        xhr.onreadystatechange = function() {
            if (this.readyState == 4) {
                if (this.status == 200) {
                    alert('Hra byla úspěšně smazána.');
                    removeGameFromUI(title); // Aktualizace UI
                } else {
                    alert('Chyba při mazání hry.');
                }
            }
        };
        xhr.open('POST', '../php/delete_game.php', true);
        xhr.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
        xhr.send('title=' + encodeURIComponent(title));
    }
}

// Funkce pro odstranění hry z UI
function removeGameFromUI(title) {
    var gameElements = document.querySelectorAll('.list-group-item');
    gameElements.forEach(function(gameElement) {
        if (gameElement.querySelector('h4').textContent === title) {
            gameElement.parentNode.removeChild(gameElement);
        }
    });
}

// Funkce pro načtení XML dokumentu
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
