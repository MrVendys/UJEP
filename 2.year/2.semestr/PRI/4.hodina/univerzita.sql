-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Počítač: db
-- Vytvořeno: Úte 26. bře 2024, 13:27
-- Verze serveru: 8.3.0
-- Verze PHP: 8.2.8

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Databáze: `univerzita`
--

-- --------------------------------------------------------

--
-- Struktura tabulky `Departments`
--

CREATE TABLE `Departments` (
  `DepartmentID` int NOT NULL,
  `FacultyID` int DEFAULT NULL,
  `Name` varchar(100) DEFAULT NULL,
  `Head` varchar(100) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Vypisuji data pro tabulku `Departments`
--

INSERT INTO `Departments` (`DepartmentID`, `FacultyID`, `Name`, `Head`) VALUES
(1, 1, 'Department of Mechanical Engineering', 'Prof. Ing. Petr Kovář, DrSc.'),
(2, 1, 'Department of Electrical Engineering', 'Prof. Ing. Kateřina Svobodová, CSc.'),
(3, 2, 'Department of Finance', 'Prof. Mgr. Kateřina Nováková, Ph.D.'),
(4, 2, 'Department of Marketing', 'Prof. Mgr. Eva Svobodová, Ph.D.'),
(5, 3, 'Department of Literature', 'Prof. PhDr. Jan Kovář, DrSc.'),
(6, 3, 'Department of Fine Arts', 'Prof. Mgr. Kateřina Kovářová, DrSc.'),
(7, 4, 'Department of Biology', 'Prof. RNDr. Petr Novák, DrSc.'),
(8, 4, 'Department of Physics', 'Prof. RNDr. Jan Kovář, DrSc.');

-- --------------------------------------------------------

--
-- Struktura tabulky `Faculties`
--

CREATE TABLE `Faculties` (
  `FacultyID` int NOT NULL,
  `Name` varchar(100) DEFAULT NULL,
  `Dean` varchar(100) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Vypisuji data pro tabulku `Faculties`
--

INSERT INTO `Faculties` (`FacultyID`, `Name`, `Dean`) VALUES
(1, 'Faculty of Engineering', 'Prof. Ing. Martin Novák, CSc.'),
(2, 'Faculty of Business Administration', 'Prof. Mgr. Jan Nový, Ph.D.'),
(3, 'Faculty of Arts', 'Prof. PhDr. Marie Nováková, DrSc.'),
(4, 'Faculty of Science', 'Prof. RNDr. Jiří Nový, DrSc.');

-- --------------------------------------------------------

--
-- Struktura tabulky `Students`
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
-- Vypisuji data pro tabulku `Students`
--

INSERT INTO `Students` (`StudentID`, `Name`, `Surname`, `Faculty`, `Email`, `AcademicYear`, `Street`, `Town`) VALUES
(123456, 'Jan', 'Novak', 'Faculty of Informatics', 'jan.novak@example.com', 3, 'Vysoká 10', 'Praha'),
(345678, 'Tomáš', 'Novotný', 'Faculty of Law', 'tomas.novotny@example.com', 4, 'Panská 15', 'Olomouc'),
(456789, 'Eva', 'Nováková', 'Faculty of Economics', 'eva.novakova@example.com', 3, 'Havlíčkova 5', 'Brno'),
(789012, 'Anna', 'Kovářová', 'Faculty of Medicine', 'anna.kovarova@example.com', 2, 'Úvoz 22', 'Brno'),
(987654, 'Petr', 'Svoboda', 'Faculty of Arts', 'petr.svoboda@example.com', 2, 'Vodičkova 18', 'Prague');

--
-- Indexy pro exportované tabulky
--

--
-- Indexy pro tabulku `Faculties`
--
ALTER TABLE `Faculties`
  ADD PRIMARY KEY (`FacultyID`);

--
-- Indexy pro tabulku `Students`
--
ALTER TABLE `Students`
  ADD PRIMARY KEY (`StudentID`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
