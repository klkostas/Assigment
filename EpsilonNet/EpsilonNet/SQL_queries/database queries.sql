CREATE TABLE Customers
(
	CustomerId int  IDENTITY(1,1)  primary key,
	Name varchar(250),
	Surname varchar(250),
	Tin varchar(25),
	Address varchar(255),
	Phone varchar(128),
	Fax varchar(128)
);

insert into Customers (Name,Surname,Tin,Address,Phone,Fax)
values
('Kostas','Lalaounis','21451541512','Ag.Konstantinou','6983775317','2104598633')

CREATE TABLE Suppliers
(
	SupplierId int  IDENTITY(1,1)  primary key,
	Name varchar(250),
	Surname varchar(250),
	Tin varchar(25),
	Address varchar(255),
	Phone varchar(128),
	Fax varchar(128)
);

INSERT INTO Suppliers(Name,Surname,Tin,Address,Phone,Fax)
VALUES
('Dimitris','Giannou','0000000000','Ag.Konstantinou','6983775317','2104598633');

CREATE TABLE Items
(
	ItemId int  IDENTITY(1,1)  primary key,
	Description varchar(250),
	SupplierID int ,
	DefaultPrice decimal(19,2),
	AlarmPrice decimal(19,2),
	Constraint FK_Items_Suppliers FOREIGN KEY(SupplierID) REFERENCES Suppliers(SupplierId)
	ON DELETE CASCADE
	ON UPDATE CASCADE
);

ALTER TABLE Items
ADD CONSTRAINT UQ_Items_ForeignKeyColumn UNIQUE (SupplierID);


CREATE TABLE Sales
(
	SaleId int  IDENTITY(1,1)  primary key,
	DateSale Datetime,
	CustomerID Int,
	Justification varchar(255)
	Constraint FK_Sales_Customer FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerId)
	ON DELETE CASCADE
	ON UPDATE CASCADE
);
ALTER TABLE Sales
ADD CONSTRAINT Sales_ForeignKeyColumn UNIQUE (CustomerID);

CREATE TABLE SalesDetail
(
	SaleDetailId int  IDENTITY(1,1)  primary key,
	SaleID int,
	ItemID int,
	Price decimal(19,2),
	Quantity Int
	Constraint FK_SalesDetail_Sales FOREIGN KEY (SaleID) REFERENCES Sales(SaleId),
	Constraint FK_SalesDetail_Items FOREIGN KEY (ItemID) REFERENCES Items(ItemId)
        ON DELETE CASCADE
	ON UPDATE CASCADE
);
ALTER TABLE SalesDetail
ADD CONSTRAINT SalesDetail_ForeignKeySaleId UNIQUE (SaleID);

ALTER TABLE SalesDetail
ADD CONSTRAINT Sales_ForeignKeyItemID UNIQUE (ItemID);

CREATE TABLE Purchases
(
	PurchaseId int  IDENTITY(1,1)  primary key,
	DatePurchase Datetime,
	SupplierID int,
	Justification varchar(255)
	Constraint FK_Purchases_Suppliers FOREIGN KEY (SupplierID) REFERENCES Suppliers(SupplierId)
 	ON DELETE CASCADE
	ON UPDATE CASCADE
);

ALTER TABLE Purchases
ADD CONSTRAINT Purchases_ForeignKeySupplierID UNIQUE (SupplierID);

CREATE TABLE PurchasesDetail
(
	PurchaseDetailId int  IDENTITY(1,1)  primary key,
	PurchaseID int,
	ItemID int,
	Price decimal(19,2),
	Quantity Int
	Constraint FK_PurchasesDetail_Purchases FOREIGN KEY (PurchaseID) REFERENCES Purchases(PurchaseId),
	Constraint FK_PurchasesDetail_Items FOREIGN KEY (ItemID) REFERENCES Items(ItemId)
	ON DELETE CASCADE
	ON UPDATE CASCADE
);
ALTER TABLE PurchasesDetail
ADD CONSTRAINT PurchasesDetail_ForeignKeyPurchaseID UNIQUE (PurchaseID);

ALTER TABLE PurchasesDetail
ADD CONSTRAINT PurchasesDetail_ForeignKeyItemID UNIQUE (ItemID);
