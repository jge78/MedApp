﻿CREATE TABLE [dbo].[Appointments]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	Patient INT NOT NULL,
	Medic INT NOT NULL,
	[Date] DATE NOT NULL,
	[Time] TIME
)
