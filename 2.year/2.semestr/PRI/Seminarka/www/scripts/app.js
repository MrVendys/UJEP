document.addEventListener("DOMContentLoaded", function() {
    // Funkce pro načtení knih
    function loadBooks() {
        fetch('php/getBooks.php')
            .then(response => response.text())
            .then(data => {
                let parser = new DOMParser();
                let xmlDoc = parser.parseFromString(data, "text/xml");
                displayBooks(xmlDoc);
            });
    }

    function displayBooks(xmlDoc) {
        let books = xmlDoc.getElementsByTagName("book");
        let bookList = document.getElementById("bookList");
        bookList.innerHTML = "<tr><th>Titulek</th><th>Autor</th><th>Rok</th><th>ISBN</th><th>Akce</th></tr>";

        for (let i = 0; i < books.length; i++) {
            let book = books[i];
            let title = book.getElementsByTagName("title")[0].childNodes[0].nodeValue;
            let author = book.getElementsByTagName("author")[0].childNodes[0].nodeValue;
            let year = book.getElementsByTagName("year")[0].childNodes[0].nodeValue;
            let isbn = book.getElementsByTagName("isbn")[0].childNodes[0].nodeValue;

            bookList.innerHTML += `
                <tr>
                    <td>${title}</td>
                    <td>${author}</td>
                    <td>${year}</td>
                    <td>${isbn}</td>
                    <td><a href="editBook.html?index=${i}">Editovat</a></td>
                </tr>`;
        }
    }

    loadBooks();
});
