CREATE DATABASE ComputerPartsShop; 
GO 

USE ComputerPartsShop;
GO

CREATE TABLE Category (
    CategoryID INT PRIMARY KEY IDENTITY,
    CategoryName NVARCHAR(100)
);

-- �������� ������� Manufacturer (�������������)
CREATE TABLE Manufacturer (
    ManufacturerID INT PRIMARY KEY IDENTITY,
    ManufacturerName NVARCHAR(100)
);

-- �������� ������� Product (������)
CREATE TABLE Product (
    ProductID INT PRIMARY KEY IDENTITY,
    ProductName NVARCHAR(100),
    Price DECIMAL(18, 2),
    CategoryID INT,
    ManufacturerID INT,
    FOREIGN KEY (CategoryID) REFERENCES Category(CategoryID),
    FOREIGN KEY (ManufacturerID) REFERENCES Manufacturer(ManufacturerID)
);

-- �������� ������� Customer (����������)
CREATE TABLE Customer (
    CustomerID INT PRIMARY KEY IDENTITY,
    CustomerName NVARCHAR(100),
    Email NVARCHAR(100)
);

-- �������� ������� Order (������)
CREATE TABLE [Order] (
    OrderID INT PRIMARY KEY IDENTITY,
    CustomerID INT,
    OrderDate DATETIME,
    FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID)
);

-- �������� ������� OrderDetails (������ ������)
CREATE TABLE OrderDetails (
    OrderDetailID INT PRIMARY KEY IDENTITY,
    OrderID INT,
    ProductID INT,
    Quantity INT,
    FOREIGN KEY (OrderID) REFERENCES [Order](OrderID),
    FOREIGN KEY (ProductID) REFERENCES Product(ProductID)
);

-- ������� ������ � ������� Category
INSERT INTO Category (CategoryName) 
VALUES ('Graphics Cards'), ('Processors'), ('Motherboards');

-- ������� ������ � ������� Manufacturer
INSERT INTO Manufacturer (ManufacturerName) 
VALUES ('Intel'), ('AMD'), ('NVIDIA');

-- ������� ������ � ������� Product
INSERT INTO Product (ProductName, Price, CategoryID, ManufacturerID) 
VALUES 
('GeForce RTX 3080', 700, 1, 3),
('Ryzen 9 5900X', 450, 2, 2),
('Core i9-10900K', 500, 2, 1);

-- ������� ������ � ������� Customer
INSERT INTO Customer (CustomerName, Email) 
VALUES ('John Doe', 'john@example.com');

-- ������� ������ � ������� Order
INSERT INTO [Order] (CustomerID, OrderDate) 
VALUES (1, GETDATE());

-- ������� ������ � ������� OrderDetails
INSERT INTO OrderDetails (OrderID, ProductID, Quantity) 
VALUES (1, 1, 1);
