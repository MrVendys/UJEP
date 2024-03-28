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
header('Content-Type: application/xml');
echo $xml->asXML();