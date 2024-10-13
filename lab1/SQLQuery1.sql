CREATE DATABASE PostServiceDB;
GO

USE PostServiceDB;
GO

-- Таблиця Клієнтів
CREATE TABLE Customers (
    CustomerID INT PRIMARY KEY IDENTITY,
    FirstName NVARCHAR(50),
    LastName NVARCHAR(50),
    Address NVARCHAR(100),
    Phone NVARCHAR(15)
);

-- Таблиця Посилок
CREATE TABLE Packages (
    PackageID INT PRIMARY KEY IDENTITY,
    Description NVARCHAR(255),
    Weight DECIMAL(5, 2),
    ShippingCost DECIMAL(10, 2),
    DeliveryStatus NVARCHAR(20),
    SenderID INT FOREIGN KEY REFERENCES Customers(CustomerID),
    ReceiverID INT FOREIGN KEY REFERENCES Customers(CustomerID)
);

-- Таблиця Маршрутів
CREATE TABLE DeliveryRoutes (
    RouteID INT PRIMARY KEY IDENTITY,
    PackageID INT FOREIGN KEY REFERENCES Packages(PackageID),
    StartLocation NVARCHAR(100),
    Destination NVARCHAR(100),
    ShippingDate DATE,
    ExpectedDeliveryDate DATE
);
-- Вставка клієнтів
INSERT INTO Customers (FirstName, LastName, Address, Phone)
VALUES ('Ivan', 'Ivanov', 'Kyiv, Ukraine', '380501234567'),
       ('Petro', 'Petrenko', 'Lviv, Ukraine', '380502345678');

-- Вставка посилок
INSERT INTO Packages (Description, Weight, ShippingCost, DeliveryStatus, SenderID, ReceiverID)
VALUES ('Books', 2.5, 50.00, 'In transit', 1, 2);

-- Вставка маршрутів
INSERT INTO DeliveryRoutes (PackageID, StartLocation, Destination, ShippingDate, ExpectedDeliveryDate)
VALUES (1, 'Kyiv', 'Lviv', '2024-09-25', '2024-09-28');
