
<!DOCTYPE html>
<html lang="cs">

<head>
    <title>Simple XML</title>
    <style>
        header {
            padding: 10px 0;
        }

        .navbar {
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin: 0 auto;
            padding: 0 20px;
        }

        .navbar-title {
            font-size: 2rem;
            text-decoration: none;
        }

        nav ul {
            list-style-type: none;
            margin: 0;
            padding: 0;
            display: flex;
        }

        nav ul li {
            margin-left: 20px;
        }

        nav ul li a {
            text-decoration: none;
            color: #fff;
            padding: 10px 20px;
            background-color: #4CAF50;
            border-radius: 4px;
            transition: background-color 0.3s ease;
        }

        nav ul li a:hover {
            background-color: #45a049;
        }
    </style>

</head>

<body>
    <header>
        <div class="navbar">
            <div class="navbar-title">Simple XML</div>
            <nav>
                <ul>
                    <li><a href="index.php">Validator</a></li>
                    <li><a href="htmlValidate.php">XML Viewer</a></li>
                    <li><a href="simpleXml.php">Simple XML</a></li>
                </ul>
            </nav>
        </div>
    </header>
    <hr>

    <pre>
    <?php
    $xml = simplexml_load_file('xml/faculties.xml');

    function traverseSimpleXML($xml, $level = 0)
    {
        $space = fn($level) => str_repeat(' ', $level * 2);
        $attributes = $xml->attributes();

        foreach ($attributes as $name => $value) {
            echo $space($level) . "$name - " . (string) $value . PHP_EOL;
        }

        $children = $xml->children();
        foreach ($children as $name => $value) {
            if ($value->count() > 0) {
                echo $space($level) . "$name:" . PHP_EOL;
                traverseSimpleXML($value, $level + 1);
            } else {
                echo $space($level) . "$name = " . (string) $value . PHP_EOL;
            }
        }
    }


    traverseSimpleXML($xml);
    ?>
    </pre>
    <?php

    $xml = new SimpleXMLElement('<fakulta/>'); // kořen
    
    $fakulta = $xml->addChild('fakulta');
    $fakulta->addAttribute('id', 'Pri');

    $dekan = $fakulta->addChild('dekan');
    $dekan->addChild('jmeno', 'Michal');


    echo $xml->asXML();

    ?>

    </br>
    </br>
    </br>
    </br>
    <?php
    $xml = new SimpleXMLElement('<?xml-stylesheet type="text/xsl" href="xml/faculties.xsl"?><faculties/>');
    $db = mysqli_connect("db", "admin", "heslo", "university");

    $faculties = $db->query(
        "SELECT FacultyID, Name, DeanID 
        FROM Faculties"
    )->fetch_all(MYSQLI_ASSOC);

    foreach ($faculties as $faculty) {
        $facultyElement = $xml->addChild('faculty');
        $facultyElement->addChild('name', $faculty['Name']);

        $dean = $db->query(
            "SELECT Name, Surname, Position, Contact 
            FROM Employees 
            WHERE EmployeeID={$faculty['DeanID']}"
        )->fetch_assoc();

        $deanElement = $facultyElement->addChild('dean');
        $deanElement->addChild('name', $dean['Name'] . " " . $dean['Surname'] . " ");


        $departments = $db->query(
            "SELECT DepartmentID, Name, HeadID 
            FROM Departments 
            WHERE FacultyID={$faculty['FacultyID']}"
        )->fetch_all(MYSQLI_ASSOC);

        $departmentsElement = $facultyElement->addChild('departments');
        foreach ($departments as $department) {
            $departmentElement = $departmentsElement->addChild('department');
            $departmentElement->addChild('name', $department['Name']);

            $headResult = $db->query(
                "SELECT Name, Surname, Position, Contact 
                FROM Employees 
                WHERE EmployeeID={$department['HeadID']}"
            );

            $head = $headResult->fetch_assoc();
            $headElement = $departmentElement->addChild('head');
            $headElement->addChild('name', $head['Name'] . " " . "$head[Surname]" . " ");

            $employees = $db->query(
                "SELECT e.Name, e.Surname, e.Position, e.Contact, GROUP_CONCAT(t.Title) AS Titles
                FROM Employees e
                LEFT JOIN EmployeeTitles t ON e.EmployeeID = t.EmployeeID 
                WHERE e.DepartmentID={$department['DepartmentID']}
                GROUP BY e.EmployeeID"
            )->fetch_all(MYSQLI_ASSOC);

            $employeesElement = $departmentElement->addChild('employees');
            foreach ($employees as $employee) {
                $employeeElement = $employeesElement->addChild('employee');
                $employeeElement->addChild('name', $employee['Name']);
                $employeeElement->addChild('surname', $employee['Surname']);
                $employeeElement->addChild('position', $employee['Position']);
                $employeeElement->addChild('contact', $employee['Contact']);
                $employeeTitles = $employeeElement->addChild('titles');

                // Split the concatenated string of titles into an array
                $titles = explode(',', $employee['Titles']);
                foreach ($titles as $title) {
                    $employeeTitles->addChild('title', trim($title));
                }
            }

        }
    }
    $xsl = new DOMDocument;
    $xsl->load('xml/faculties.xsl');
    $proc = new XSLTProcessor;
    $proc->importStyleSheet($xsl);
    echo $proc->transformToXML($xml);

    ?>
</body>

</html>
