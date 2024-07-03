﻿CREATE TABLE [dbo].[Users]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	UserType INT NOT NULL CONSTRAINT CK_UserType CHECK(UserType IN(1,2,3)),
	FirstName VARCHAR(100) NOT NULL,
	LastName VARCHAR(100) NOT NULL,
	DateOfBirth DATE,
	PhoneNumber VARCHAR(50),
	Email VARCHAR(100)
)