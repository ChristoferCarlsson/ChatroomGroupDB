
---- Create Database
CREATE DATABASE ChatroomDB;

---- Create Customers table
CREATE TABLE Users (
    UserId INT PRIMARY KEY,
    UserName VARCHAR(255) NOT NULL,
	UserPassword VARCHAR(255) NOT NULL,
    Email VARCHAR(255) NOT NULL
);

---- Insert Dummy Data
INSERT INTO Users (UserId, UserName, UserPassword, Email) VALUES
(1, 'John21', 'Password123', 'john.doe@example.com'),
(2, 'Dog_Lover', 'CaninesRulz', 'jane.smith@example.com'),
(3, 'LarsTheMan', '11223344', 'robert.brown@example.com')