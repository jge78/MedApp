USE MedAppUserDB

SELECT * FROM Users

--Add(User user)
INSERT INTO dbo.Users (UserType,FirstName,LastName,DateOfBirth,PhoneNumber,Email)
VALUES(2,'Test 1','Test1 LastName','19900115','55-6666558855','test@test.com')

INSERT INTO dbo.Users (UserType,FirstName,LastName,DateOfBirth,PhoneNumber,Email) 
VALUES(2,'Test 1','Test1 LastName','19900115','55-6666558855','test@test.com')

