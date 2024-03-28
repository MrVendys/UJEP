-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: db
-- Generation Time: Mar 19, 2024 at 04:09 PM
-- Server version: 8.3.0
-- PHP Version: 8.2.17

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `university`
--

-- --------------------------------------------------------

--
-- Table structure for table `Courses`
--

CREATE TABLE `Courses` (
  `CourseID` int NOT NULL,
  `CourseName` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `Courses`
--

INSERT INTO `Courses` (`CourseID`, `CourseName`) VALUES
(1, 'Math'),
(2, 'Physics'),
(3, 'Chemistry'),
(4, 'English'),
(5, 'History'),
(6, 'Microeconomics'),
(7, 'Macroeconomics'),
(8, 'Finance'),
(9, 'Marketing'),
(10, 'Accounting'),
(11, 'Literature'),
(12, 'History of Art'),
(13, 'Music'),
(14, 'Philosophy'),
(15, 'Sociology'),
(16, 'Biology'),
(17, 'Anatomy'),
(18, 'Physiology'),
(19, 'Constitutional Law'),
(20, 'Criminal Law'),
(21, 'Civil Law'),
(22, 'International Law'),
(23, 'Legal Writing');

-- --------------------------------------------------------

--
-- Table structure for table `Departments`
--

CREATE TABLE `Departments` (
  `DepartmentID` int NOT NULL,
  `FacultyID` int DEFAULT NULL,
  `Name` varchar(100) DEFAULT NULL,
  `HeadID` int DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `Departments`
--

INSERT INTO `Departments` (`DepartmentID`, `FacultyID`, `Name`, `HeadID`) VALUES
(1, 1, 'Department of Mechanical Engineering', 1),
(2, 1, 'Department of Electrical Engineering', 2),
(3, 2, 'Department of Finance', 3),
(4, 2, 'Department of Marketing', 4),
(5, 3, 'Department of Literature', 5),
(6, 3, 'Department of Fine Arts', 6),
(7, 4, 'Department of Biology', 7),
(8, 4, 'Department of Physics', 8);

-- --------------------------------------------------------

--
-- Table structure for table `Employees`
--

CREATE TABLE `Employees` (
  `EmployeeID` int NOT NULL,
  `DepartmentID` int DEFAULT NULL,
  `Name` varchar(50) DEFAULT NULL,
  `Surname` varchar(50) DEFAULT NULL,
  `Position` varchar(100) DEFAULT NULL,
  `Contact` varchar(100) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `Employees`
--

INSERT INTO `Employees` (`EmployeeID`, `DepartmentID`, `Name`, `Surname`, `Position`, `Contact`) VALUES
(1, 1, 'Anna', 'Novotná', 'Assistant Professor', 'anna.novotna@facultyengineering.cz'),
(2, 1, 'Martin', 'Novák', 'Research Assistant', 'martin.novak@facultyengineering.cz'),
(3, 2, 'Petr', 'Novotný', 'Associate Professor', 'petr.novotny@facultyengineering.cz'),
(4, 2, 'Lucie', 'Kovářová', 'Researcher', 'lucie.kovarova@facultyengineering.cz'),
(5, 3, 'Tomáš', 'Kovář', 'Assistant Professor', 'tomas.kovar@facultybusiness.cz'),
(6, 3, 'Pavel', 'Novák', 'Associate Professor', 'pavel.novak@facultybusiness.cz'),
(7, 4, 'Marie', 'Nováková', 'Research Assistant', 'marie.novakova@facultybusiness.cz'),
(8, 4, 'Petr', 'Novotný', 'Associate Professor', 'petr.novotny@facultybusiness.cz'),
(9, 5, 'Lucie', 'Novotná', 'Associate Professor', 'lucie.novotna@facultyarts.cz'),
(10, 5, 'Pavel', 'Novák', 'Assistant Professor', 'pavel.novak@facultyarts.cz'),
(11, 6, 'Tomáš', 'Kovář', 'Professor', 'tomas.kovar@facultyarts.cz'),
(12, 6, 'Eva', 'Nováková', 'Research Assistant', 'eva.novakova@facultyarts.cz'),
(13, 7, 'Martin', 'Novotný', 'Associate Professor', 'martin.novotny@facultyscience.cz'),
(14, 7, 'Kateřina', 'Nováková', 'Assistant Professor', 'katerina.novakova@facultyscience.cz'),
(15, 8, 'Petr', 'Novák', 'Professor', 'petr.novak@facultyscience.cz'),
(16, 8, 'Lucie', 'Nováková', 'Research Assistant', 'lucie.novakova@facultyscience.cz');

-- --------------------------------------------------------

--
-- Table structure for table `EmployeeTitles`
--

CREATE TABLE `EmployeeTitles` (
  `EmployeeID` int DEFAULT NULL,
  `Title` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `EmployeeTitles`
--

INSERT INTO `EmployeeTitles` (`EmployeeID`, `Title`) VALUES
(1, 'Ing.'),
(1, 'Ph.D.'),
(2, 'Ing.'),
(3, 'Ing.'),
(3, 'Ph.D.'),
(4, 'Ph.D.'),
(5, 'Mgr.'),
(5, 'Ph.D.'),
(6, 'Mgr.'),
(6, 'Ph.D.'),
(7, 'Mgr.'),
(7, 'Ph.D.'),
(8, 'Mgr.'),
(8, 'Ph.D.'),
(9, 'PhDr.'),
(9, 'DrSc.'),
(10, 'PhDr.'),
(11, 'Mgr.'),
(11, 'DrSc.'),
(12, 'Mgr.'),
(13, 'RNDr.'),
(13, 'DrSc.'),
(14, 'RNDr.'),
(15, 'RNDr.'),
(15, 'DrSc.'),
(16, 'RNDr.');

-- --------------------------------------------------------

--
-- Table structure for table `Faculties`
--

CREATE TABLE `Faculties` (
  `FacultyID` int NOT NULL,
  `Name` varchar(100) DEFAULT NULL,
  `DeanID` int DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `Faculties`
--

INSERT INTO `Faculties` (`FacultyID`, `Name`, `DeanID`) VALUES
(1, 'Faculty of Engineering', 1),
(2, 'Faculty of Business Administration', 13),
(3, 'Faculty of Arts', 9),
(4, 'Faculty of Science', 15);

-- --------------------------------------------------------

--
-- Table structure for table `StudentCourses`
--

CREATE TABLE `StudentCourses` (
  `StudentID` int NOT NULL,
  `CourseID` int NOT NULL,
  `Completed` bit(1) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `StudentCourses`
--

INSERT INTO `StudentCourses` (`StudentID`, `CourseID`, `Completed`) VALUES
(123456, 1, b'0'),
(123456, 2, b'1'),
(123456, 3, b'0'),
(123456, 4, b'0'),
(123456, 5, b'1'),
(456789, 6, b'0'),
(456789, 7, b'1'),
(456789, 8, b'0'),
(456789, 9, b'1'),
(456789, 10, b'1'),
(789012, 16, b'1'),
(789012, 17, b'0'),
(789012, 18, b'1'),
(789012, 19, b'1'),
(789012, 20, b'0'),
(789012, 21, b'1'),
(789012, 22, b'1'),
(789012, 23, b'1'),
(987654, 11, b'1'),
(987654, 12, b'0'),
(987654, 13, b'1'),
(987654, 14, b'1'),
(987654, 15, b'0');

-- --------------------------------------------------------

--
-- Table structure for table `Students`
--

CREATE TABLE `Students` (
  `StudentID` int NOT NULL,
  `Name` varchar(50) DEFAULT NULL,
  `Surname` varchar(50) DEFAULT NULL,
  `Faculty` varchar(50) DEFAULT NULL,
  `Email` varchar(100) DEFAULT NULL,
  `AcademicYear` int DEFAULT NULL,
  `Street` varchar(100) DEFAULT NULL,
  `Town` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `Students`
--

INSERT INTO `Students` (`StudentID`, `Name`, `Surname`, `Faculty`, `Email`, `AcademicYear`, `Street`, `Town`) VALUES
(123456, 'Jan', 'Novak', 'Faculty of Informatics', 'jan.novak@example.com', 3, 'Vysoká 10', 'Praha'),
(345678, 'Tomáš', 'Novotný', 'Faculty of Law', 'tomas.novotny@example.com', 4, 'Panská 15', 'Olomouc'),
(456789, 'Eva', 'Nováková', 'Faculty of Economics', 'eva.novakova@example.com', 3, 'Havlíčkova 5', 'Brno'),
(789012, 'Anna', 'Kovářová', 'Faculty of Medicine', 'anna.kovarova@example.com', 2, 'Úvoz 22', 'Brno'),
(987654, 'Petr', 'Svoboda', 'Faculty of Arts', 'petr.svoboda@example.com', 2, 'Vodičkova 18', 'Prague');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `Courses`
--
ALTER TABLE `Courses`
  ADD PRIMARY KEY (`CourseID`);

--
-- Indexes for table `Departments`
--
ALTER TABLE `Departments`
  ADD PRIMARY KEY (`DepartmentID`),
  ADD KEY `fk_Departments_Faculties` (`FacultyID`),
  ADD KEY `fk_Departments_Employees` (`HeadID`);

--
-- Indexes for table `Employees`
--
ALTER TABLE `Employees`
  ADD PRIMARY KEY (`EmployeeID`),
  ADD KEY `DepartmentID` (`DepartmentID`);

--
-- Indexes for table `EmployeeTitles`
--
ALTER TABLE `EmployeeTitles`
  ADD KEY `EmployeeID` (`EmployeeID`);

--
-- Indexes for table `Faculties`
--
ALTER TABLE `Faculties`
  ADD PRIMARY KEY (`FacultyID`),
  ADD KEY `fk_Faculties_Employees` (`DeanID`);

--
-- Indexes for table `StudentCourses`
--
ALTER TABLE `StudentCourses`
  ADD PRIMARY KEY (`StudentID`,`CourseID`),
  ADD KEY `CourseID` (`CourseID`);

--
-- Indexes for table `Students`
--
ALTER TABLE `Students`
  ADD PRIMARY KEY (`StudentID`);

--
-- Constraints for dumped tables
--

--
-- Constraints for table `Departments`
--
ALTER TABLE `Departments`
  ADD CONSTRAINT `fk_Departments_Employees` FOREIGN KEY (`HeadID`) REFERENCES `Employees` (`EmployeeID`),
  ADD CONSTRAINT `fk_Departments_Faculties` FOREIGN KEY (`FacultyID`) REFERENCES `Faculties` (`FacultyID`);

--
-- Constraints for table `Employees`
--
ALTER TABLE `Employees`
  ADD CONSTRAINT `Employees_ibfk_1` FOREIGN KEY (`DepartmentID`) REFERENCES `Departments` (`DepartmentID`);

--
-- Constraints for table `EmployeeTitles`
--
ALTER TABLE `EmployeeTitles`
  ADD CONSTRAINT `EmployeeTitles_ibfk_1` FOREIGN KEY (`EmployeeID`) REFERENCES `Employees` (`EmployeeID`);

--
-- Constraints for table `Faculties`
--
ALTER TABLE `Faculties`
  ADD CONSTRAINT `fk_Faculties_Employees` FOREIGN KEY (`DeanID`) REFERENCES `Employees` (`EmployeeID`);

--
-- Constraints for table `StudentCourses`
--
ALTER TABLE `StudentCourses`
  ADD CONSTRAINT `StudentCourses_ibfk_1` FOREIGN KEY (`StudentID`) REFERENCES `Students` (`StudentID`),
  ADD CONSTRAINT `StudentCourses_ibfk_2` FOREIGN KEY (`CourseID`) REFERENCES `Courses` (`CourseID`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;