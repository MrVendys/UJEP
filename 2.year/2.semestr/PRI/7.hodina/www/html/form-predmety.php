<!DOCTYPE html>
<html lang="cs">

<? require __DIR__ . '/predmety.php' ?>

<head>
    <title>Home</title>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css">
</head>

<body>
<?php
    $predmety = tabulkaPredmetu("");
    ?>
    <div class="container">
        <div class="text-center">
            <h1>Vyberte předmět</h1>
        </div>
        <div class="row justify-content-center">
            <div class="col-md-6">
                <form method="GET">
                    <div class="form-group">
                        <label class="text-gray-700 text-sm font-bold mr-2">Předmět: </label>
                        <?= $predmety ?>
                    </div>
                    <button type="submit" class="btn btn-primary">Odeslat</button>
                </form>
            </div>
        </div>
        <div class="row justify-content-center">
            <div class="col-md-6">
                <?php
                if (isset($_GET["kod_predmetu"])) {
                    $kod_predmetu = $_GET["kod_predmetu"];
                    echo vratPredmet($kod_predmetu);
                }
                ?>
            </div>
        </div>
    </div>

    <script src="https://code.jquery.com/jquery-3.5.1.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
</body>

</html>