document.addEventListener("DOMContentLoaded", function() {
    // Funkce pro načtení parametrů z URL
    function getQueryParams() {
        let params = {};
        window.location.search.substring(1).split("&").forEach(function(pair) {
            let [key, value] = pair.split("=");
            params[key] = decodeURIComponent(value);
        });
        return params;
    }

    let params = getQueryParams();
    let bookIndex = params["index"];

    // Načítání knihy na základě indexu
    if (bookIndex !== undefined) {
        fetch(`php/getBook.php?index=${bookIndex}`)
            .then(response => response.json())
            .then(data => {
                document.getElementById("bookIndex").value = bookIndex;
                document.getElementById("title").value = data.title;
                document.getElementById("author").value = data.author;
                document.getElementById("year").value = data.year;
                document.getElementById("isbn").value = data.isbn;
            });
    }

    document.getElementById("editBookForm").addEventListener("submit", function(event) {
        event.preventDefault();

        let formData = new FormData(event.target);
        fetch('php/updateBook.php', {
            method: 'POST',
            body: formData
        }).then(response => response.text())
          .then(result => {
              alert(result);
              window.location.href = 'index.html';
          });
    });
});
