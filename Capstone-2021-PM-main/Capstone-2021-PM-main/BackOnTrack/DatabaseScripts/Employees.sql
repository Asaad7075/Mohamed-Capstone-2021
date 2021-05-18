print '' print '*** using backontrack_db ***'
GO
USE backontrack_db
GO

/**************************************
Chantal Shirley
Created: 2021/02/24
Description: Employee Table Creation
***************************************/

-- Employee Table

print '' print '*** creating employee table ***'
GO
CREATE TABLE dbo.Employee
(
	EmployeeID		int				IDENTITY(100000, 1)	NOT NULL,
	FirstName		nvarchar(50)	NOT NULL,
	LastName		nvarchar(50)	NOT NULL,
	PhoneNumber		nvarchar(11)	NOT NULL,
	Email			nvarchar(100)	NOT NULL,
	PasswordHash		nvarchar(100)	NOT NULL DEFAULT
	'9C9064C59F1FFA2E174EE754D2979BE80DD30DB552EC03E7E327E9B1A4BD594E',
	Active			bit				NOT NULL DEFAULT 1,
	Address			nvarchar(100)	NOT NULL,
	Gender			nvarchar(50)	NOT NULL,
	CONSTRAINT pk_employeeID PRIMARY KEY(EmployeeID ASC),
	CONSTRAINT [ak_email] UNIQUE(Email ASC)
)
GO
/*
Jakub Kawski
2021/03/08

Changed the brad record password to 'password' for
easier testing/debugging after a fresh DB creation
*/
/***************************************
Updater: Zach Stultz
Updated: 04-30-21
Remarks: Added additional employees.
***************************************/
print '' print '*** creating brad test record ***'
GO
INSERT INTO dbo.Employee
		(Email, FirstName, LastName, PhoneNumber, Address, Gender, PasswordHash)
VALUES
		('brad@backontrack.com', 'Brad', 'Majors', '3195551111', '1234 Main St.', 'Male',
		'5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8')
GO
print '' print '*** creating employee test records ***'
GO
INSERT INTO dbo.Employee
		(Email, FirstName, LastName, PhoneNumber, Address, Gender)
VALUES
		('janet@backontrack.com', 'Janet', 'Weiss', '3195552222', '1234 Main St.', 'Female'),
		('everett@backontrack.com', 'Everett', 'Scott', '3195553333', '1234 Main St.', 'Male'),
		('magenta@backontrack.com', 'Magenta', 'Orrore', '3195554444', '1234 Main St.', 'Female'),
		('rocky@company.com', 'Rocky', 'Orrore', '3195555555', '1234 Main St.', 'Male'),
		('debera@backontrack.com', 'Debera', 'Jensen', '6024235803', '1234 Main St.', 'Female'),
		('michael@backontrack.com', 'Michael', 'Bryant', '8474894015', '1234 Main St.', 'Male'),
		('jacob@backontrack.com', 'Jacob', 'London', '8173274924', '1234 Main St.', 'Male'),
		('susan@backontrack.com', 'Susan', 'Cheek', '5076762141', '1234 Main St.', 'Female'),
		('anthony@backontrack.com', 'Anthony', 'Kennedy', '7017297065', '1234 Main St.', 'Male'),
		('lawrence@backontrack.com', 'Lawrence', 'Taylor', '4404797947', '1234 Main St.', 'Male'),
		('ramona@backontrack.com', 'Ramona', 'Casillas', '6317415195', '1234 Main St.', 'Female'),
		('nora@backontrack.com', 'Nora', 'Holzman', '2068196333', '1234 Main St.', 'Female'),
		('kristin@backontrack.com', 'Kristin', 'Trujillo', '5627865514', '1234 Main St.', 'Female'),
		('jeffrey@backontrack.com', 'Jeffrey', 'Carter', '4237918235', '1234 Main St.', 'Male'),
		('trista@backontrack.com', 'Trista', 'Farmer', '3044392942', '1234 Main St.', 'Female'),
		('joseph@backontrack.com', 'Joseph', 'Ellis', '9518237158', '1234 Main St.', 'Male'),
		('rupert@backontrack.com', 'Rupert', 'Lingle', '5153604555', '1234 Main St.', 'Male'),
		('barbara@backontrack.com', 'Barbara', 'Rodriguez', '7276574777', '1234 Main St.', 'Female')
GO

print '' print '*** creating sp_select_employee_by_id ***'
GO
CREATE PROCEDURE dbo.sp_select_employee_by_id
	(
		@EmployeeID		int
	)
AS
	BEGIN
		SELECT		EmployeeID, FirstName, LastName, PhoneNumber, Email, Active, Address, Gender
		FROM		Employee
		WHERE		EmployeeID = @EmployeeID
	END
GO
/*
Select *
From Employee
*/

-- Stored procedure for creating a new Employee record
CREATE PROCEDURE [dbo].[sp_Create_Employee]
(
	@FirstName		[nvarchar](50),
	@LastName		[nvarchar](50),
	@PhoneNumber	[nvarchar](11),
	@Email			[nvarchar](100),
	@Address		[nvarchar](100),
	@Gender			[nvarchar](100)
)
AS
	BEGIN
		INSERT INTO [dbo].[Employee]
		([Email], [FirstName], [LastName], [PhoneNumber], [Gender], [Address])
	VALUES
		(@Email, @FirstName, @LastName, @PhoneNumber, @Gender, @Address)
	RETURN @@ROWCOUNT
	END
GO

print '' print '*** creating sp_Update_PasswordHash ***'
GO
CREATE PROCEDURE [dbo].[sp_Update_PasswordHash]
	(
		@Email				[nvarchar](100),
		@OldPasswordHash	[nvarchar](100),
		@NewPasswordHash	[nvarchar](100)
	)
AS
	BEGIN
		UPDATE Employee
			SET PasswordHash = @NewPasswordHash
			WHERE Email = @Email
			  AND PasswordHash = @OldPasswordHash
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** creating sp_Authenticate_User ***'
GO
CREATE PROCEDURE [dbo].[sp_Authenticate_User]
	(
		@Email				[nvarchar](100),
		@PasswordHash		[nvarchar](100)
	)
AS
	BEGIN
		SELECT COUNT(Email)
		FROM Employee
		WHERE Email = @Email
		  AND PasswordHash = @PasswordHash
		  AND Active = 1
	END
GO

print '' print '*** creating sp_select_user_by_email ***'
GO
CREATE PROCEDURE [dbo].[sp_Select_User_By_Email]
	(
		@Email				[nvarchar](100)
	)
AS
	BEGIN
		SELECT EmployeeID, FirstName, LastName, PhoneNumber, Email, Active, Address, Gender
		FROM Employee
		WHERE Email = @Email
	END
GO

/**************************************
Nate Hepker
Created: 2021/03/03
Description: Creates the sp_select_employees_by_active stored procedure.
**************************************
UpdateFirstName UpdaterLastName
Updated: YYYY/MM/DD
***************************************/
print '' print '*** creating sp_select_employees_by_active ***'
GO
CREATE PROCEDURE [dbo].[sp_select_employees_by_active]
	(
		@Active		[bit]
	)
AS
	BEGIN
		SELECT		EmployeeID, Email, FirstName, LastName, PhoneNumber, Active, Address, Gender
		FROM		Employee
		WHERE		Active = @Active
		ORDER BY 	LastName ASC
	END
GO


/**************************************
Richard Schroeder
Created: 2021/03/16
Description: Creates the sp_update_employee_profile_data stored procedure.
**************************************
UpdateFirstName UpdaterLastName
Updated: YYYY/MM/DD
***************************************/

print '' print '*** creating sp_update_employee_profile_data ***'
GO
CREATE PROCEDURE [dbo].[sp_update_employee_profile_data]
	(
		@EmployeeID			[int],

		@NewFirstName		[nvarchar](50),
		@NewLastName		[nvarchar](50),
		@NewPhoneNumber		[nvarchar](11),
		@NewEmail			[nvarchar](100),
		@NewAddress			[nvarchar](100),
		@NewGender			[nvarchar](100),

		@OldFirstName		[nvarchar](50),
		@OldLastName		[nvarchar](50),
		@OldPhoneNumber		[nvarchar](11),
		@OldEmail			[nvarchar](100),
		@OldAddress			[nvarchar](100),
		@OldGender			[nvarchar](100)
	)
AS
	BEGIN
		UPDATE Employee
			SET Email = @NewEmail,
				FirstName = @NewFirstName,
				LastName = @NewLastName,
				PhoneNumber = @NewPhoneNumber,
				Address = @NewAddress,
				Gender = @NewGender
			WHERE EmployeeID = @EmployeeID
			AND Email = @OldEmail
			AND Firstname = @OldFirstName
			AND LastName = @OldLastName
			AND PhoneNumber = @OldPhoneNumber
			AND Address = @OldAddress
			AND	Gender = @OldGender
		RETURN @@ROWCOUNT
	END
GO

/**************************************
Richard Schroeder
Created: 2021/03/16
Description: Creates the sp_deactivate_employee stored procedure.
**************************************
UpdateFirstName UpdaterLastName
Updated: YYYY/MM/DD
***************************************/

print '' print '*** creating sp_deactivate_employee ***'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_employee]
(
	@EmployeeID			[int]
)
AS
	BEGIN
		UPDATE Employee
			SET Active = 0
			WHERE Employee.EmployeeID = @EmployeeID
		RETURN @@ROWCOUNT
	END
GO

/**************************************
Richard Schroeder
Created: 2021/03/16
Description: Creates the sp_deactivate_employee stored procedure.
**************************************
UpdateFirstName UpdaterLastName
Updated: YYYY/MM/DD
***************************************/

print '' print '*** creating sp_reactivate_employee ***'
GO
CREATE PROCEDURE [dbo].sp_reactivate_employee
(
	@EmployeeID			[int]
)
AS
	BEGIN
		UPDATE Employee
			SET Active = 1
			WHERE Employee.EmployeeID = @EmployeeID
		RETURN @@ROWCOUNT
	END
GO