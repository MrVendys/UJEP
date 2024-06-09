<!DOCTYPE html>
<html lang="cs">

<head>
    <meta charset="UTF-8">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css">
</head>

<body>
    <?php
    $myName = $_POST['name'] ?? '';
    $Number = $_POST['number'] ?? '';
    $Email = $_POST['email'] ?? '';
    ?>

    <div class="container">
        <h2>Form Information</h2>
        <table class="table">
            <thead>
                <tr>
                    <th>Name</th>
                    <th>Number</th>
                    <th>Email</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td><?= $myName ?></td>
                    <td><?= $Number ?></td>
                    <td><?= $Email ?></td>
                </tr>
            </tbody>
        </table>
    </div>

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
</body>

</html>