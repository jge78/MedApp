USE MedAppUserDB

SELECT * FROM Users 
--TRUNCATE TABLE Users 
/*
1	Medic
2	User
3	Patient
*/

SELECT * FROM Users WHERE UserType=1	--Medics
SELECT * FROM Users WHERE UserType=2	--Users
SELECT * FROM Users WHERE UserType=3	--Patients


--Add(User user)
INSERT INTO dbo.Users (UserType,FirstName,LastName,DateOfBirth,PhoneNumber,Email)
VALUES(2,'Test 1','Test1 LastName','19900115','55-6666558855','test@test.com')

INSERT INTO dbo.Users (UserType,FirstName,LastName,DateOfBirth,PhoneNumber,Email) 
VALUES(2,'Test 1','Test1 LastName','19900115','55-6666558855','test@test.com')

--Patients
INSERT INTO dbo.Users (UserType,FirstName,LastName,DateOfBirth,PhoneNumber,Email)
VALUES(3,'Jorge','Gonzalez','19780518','55-6666558855','jge77777@test.com')

INSERT INTO dbo.Users (UserType,FirstName,LastName,DateOfBirth,PhoneNumber,Email)
VALUES(3,'Jorge','Espinosa','19780518','55-6666558855','jge8888@test.com')

INSERT INTO dbo.Users (UserType,FirstName,LastName,DateOfBirth,PhoneNumber,Email)
VALUES(3,'Juan','Perez','19820518','55-6995558855','juanp@test.com')

INSERT INTO dbo.Users (UserType,FirstName,LastName,DateOfBirth,PhoneNumber,Email)
VALUES(3,'Maria','Lopez','19900622','55-688855855','mari_lopez@test.com')






USE MedAppAppointmentsDB

--Shifts
SET DATEFIRST 1	--MONDAY => 1
SELECT DATEPART(DW,GETDATE())

DECLARE 
	@StartTime TIME

SET @StartTime = GETDATE()

SET @StartTime='09:00'

SELECT @StartTime

SELECT * FROM Appointments
--Patient,Medic,Date,Time

SELECT * FROM MedAppUserDB.dbo.Users WHERE UserType=1	--Medics
SELECT * FROM MedAppUserDB.dbo.Users WHERE UserType=3	--Patients
SELECT * FROM MedAppAppointmentsDB.dbo.Shifts
SELECT * FROM MedAppAppointmentsDB.dbo.Appointments


SELECT * FROM Shifts

--House
INSERT INTO Shifts (Medic,DayOfWeek,StartTime,EndTime) VALUES (2,1,'09:00','14:00')
INSERT INTO Shifts (Medic,DayOfWeek,StartTime,EndTime) VALUES (2,2,'09:00','14:00')
INSERT INTO Shifts (Medic,DayOfWeek,StartTime,EndTime) VALUES (2,3,'09:00','14:00')
INSERT INTO Shifts (Medic,DayOfWeek,StartTime,EndTime) VALUES (2,4,'09:00','14:00')
INSERT INTO Shifts (Medic,DayOfWeek,StartTime,EndTime) VALUES (2,5,'09:00','14:00')

SELECT * FROM Appointments

SELECT Id,Patient,Medic,[Date],CONVERT(SMALLDATETIME,[Time]) FROM Appointments