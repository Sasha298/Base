CREATE DATABASE ComputerPartsShop; 
GO 

USE ComputerPartsShop;
GO

CREATE TABLE Category (
    CategoryID INT PRIMARY KEY IDENTITY,
    CategoryName NVARCHAR(100)
);

-- Создание таблицы Manufacturer (Производители)
CREATE TABLE Manufacturer (
    ManufacturerID INT PRIMARY KEY IDENTITY,
    ManufacturerName NVARCHAR(100)
);

-- Создание таблицы Product (Товары)
CREATE TABLE Product (
    ProductID INT PRIMARY KEY IDENTITY,
    ProductName NVARCHAR(100),
    Price DECIMAL(18, 2),
    CategoryID INT,
    ManufacturerID INT,
    FOREIGN KEY (CategoryID) REFERENCES Category(CategoryID),
    FOREIGN KEY (ManufacturerID) REFERENCES Manufacturer(ManufacturerID)
);

-- Создание таблицы Customer (Покупатели)
CREATE TABLE Customer (
    CustomerID INT PRIMARY KEY IDENTITY,
    CustomerName NVARCHAR(100),
    Email NVARCHAR(100)
);

-- Создание таблицы Order (Заказы)
CREATE TABLE [Order] (
    OrderID INT PRIMARY KEY IDENTITY,
    CustomerID INT,
    OrderDate DATETIME,
    FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID)
);

-- Создание таблицы OrderDetails (Детали заказа)
CREATE TABLE OrderDetails (
    OrderDetailID INT PRIMARY KEY IDENTITY,
    OrderID INT,
    ProductID INT,
    Quantity INT,
    FOREIGN KEY (OrderID) REFERENCES [Order](OrderID),
    FOREIGN KEY (ProductID) REFERENCES Product(ProductID)
);

-- Вставка данных в таблицу Category
INSERT INTO Category (CategoryName) 
VALUES ('Graphics Cards'), ('Processors'), ('Motherboards');

-- Вставка данных в таблицу Manufacturer
INSERT INTO Manufacturer (ManufacturerName) 
VALUES ('Intel'), ('AMD'), ('NVIDIA');

-- Вставка данных в таблицу Product
INSERT INTO Product (ProductName, Price, CategoryID, ManufacturerID) 
VALUES 
('GeForce RTX 3080', 700, 1, 3),
('Ryzen 9 5900X', 450, 2, 2),
('Core i9-10900K', 500, 2, 1);

-- Вставка данных в таблицу Customer
INSERT INTO Customer (CustomerName, Email) 
VALUES ('John Doe', 'john@example.com');

-- Вставка данных в таблицу Order
INSERT INTO [Order] (CustomerID, OrderDate) 
VALUES (1, GETDATE());

-- Вставка данных в таблицу OrderDetails
INSERT INTO OrderDetails (OrderID, ProductID, Quantity) 
VALUES (1, 1, 1);
