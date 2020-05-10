CREATE DATABASE [Shop]
GO

USE [Shop];

CREATE TABLE [dbo].[Category]
(
	[Id] INT IDENTITY(1, 1) PRIMARY KEY NOT NULL,
	[Name] NVARCHAR(50) NOT NULL
);

INSERT INTO [dbo].[Category]
           ([Name])
     VALUES (N'Fruits'),
			(N'Vegetables'),
			(N'Dairy products'),
			(N'Drinks'),
			(N'Grocery');	 
	 
	 
CREATE TABLE [dbo].[Product]
(
	[Id] INT IDENTITY(1, 1) PRIMARY KEY NOT NULL,
	[Name] NVARCHAR(50) NOT NULL,
	[Price] INT NOT NULL,
	[CategoryId] INT NOT NULL,
	FOREIGN KEY([CategoryId]) REFERENCES [dbo].[Category] ([Id])
);

INSERT INTO [dbo].[Product]
           ([Name]
           ,[Price]
           ,[CategoryId])
     VALUES (N'Milk',	78,	3),
			(N'Apples', 64,	1),
			(N'Bananas',	80,	1),
			(N'Pasta', 59,	5),
			(N'Cream', 101, 3),
			(N'Tomatoes', 89, 2),
			(N'Juice', 72, 4),
			(N'Water', 23, 4),
			(N'Rice', 74, 5),
			(N'Pears', 89, 1);
