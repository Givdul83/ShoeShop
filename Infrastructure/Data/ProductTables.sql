DROP TABLE ProductImages
DROP TABLE Products
DROP TABLE Prices
DROP TABLE Manufacturers
DROP TABLE Images


CREATE TABLE Images(

Id int Identity Primary Key,
ImageURL nvarchar(450) unique

)

CREATE TABLE Manufacturers
(
Id int Identity Primary Key,
Manufacturer nvarchar(50) unique 
)

CREATE TABLE Prices
(
Id int Identity Primary Key,
Price money unique
)

CREATE TABLE Products
(
Id int Identity Primary Key,
Title nvarchar(100) NOT NULL,
ManufacturerId int NOT NULL,
PriceId int NOT NULL,

FOREIGN KEY (ManufacturerID) REFERENCES Manufacturers(Id),
FOREIGN KEY (PriceId) REFERENCES Prices(Id)
 
)

CREATE TABLE ProductImages
(
ProductId int,
ImageId int,
Primary key (ProductId, ImageId),
FOREIGN KEY (ProductId) REFERENCES Products(Id),
FOREIGN KEY (ImageId) REFERENCES Images(Id)

)