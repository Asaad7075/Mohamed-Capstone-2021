print '' print '*** using backontrack_db ***'
GO
USE backontrack_db
GO

/**************************************
Chantal Shirley
Created: 2021/02/14
Description: Drivers License Table Creation
***************************************/

-- Drivers License Table

print '' print '*** creating drivers license table ***'
GO
CREATE TABLE dbo.DriversLicense
(
	LicenseNumber nvarchar(100) NOT NULL,
	EmployeeID int NOT NULL,
	LicenseType nvarchar(15) NOT NULL,
	LicenseIssuedDate date NOT NULL,
	LicenseExpiryDate date NOT NULL,
	IsActive bit NOT NULL DEFAULT 1
	CONSTRAINT pk_licNo PRIMARY KEY(LicenseNumber) --,
	CONSTRAINT fk_employeeIDDriversLicense FOREIGN KEY(EmployeeID)
		REFERENCES dbo.Employee(EmployeeID)
)
GO
/**************************************
Jakub Kawski
Created: 2021/04/24
Description: Get all the employees that have a drivers license that is active
***************************************/
print '' print '*** creating sp_select_all_drivers ***'
GO
CREATE PROCEDURE dbo.sp_select_all_drivers
AS
	BEGIN
		SELECT		Employee.EmployeeID, Email, FirstName, LastName, PhoneNumber, Active, [Address], Gender
		FROM		Employee 
		JOIN		DriversLicense
			ON		Employee.EmployeeID = DriversLicense.EmployeeID
		WHERE		Employee.Active = 1
			AND		DriversLicense.IsActive = 1
	END
GO



/**************************************
Chantal Shirley
Created: 2021/02/14
Description: Drivers License Stored
Procedure Creation (sp_add_drivers_license,
sp_retrieve_drivers_license_by_license_number)
***************************************/
-- Drivers License Stored Procedures

print '' print '*** creating sp_add_drivers_license ***'
GO
CREATE PROCEDURE dbo.sp_insert_drivers_license
(
	@LicenseNumber			nvarchar(100),
	@EmployeeID				int,
	@LicenseType			nvarchar(15),
	@LicenseIssuedDate		date,
	@LicenseExpiryDate		date,
	@IsActive				bit
)
AS
	BEGIN
		INSERT INTO
			DriversLicense
			(
				LicenseNumber,
				EmployeeID,
				LicenseType,
				LicenseIssuedDate,
				LicenseExpiryDate,
				IsActive
			)
		VALUES
		(
			@LicenseNumber,
			@EmployeeID,
			@LicenseType,
			@LicenseIssuedDate,
			@LicenseExpiryDate,
			@IsActive
		)
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** creating sp_retrieve_drivers_license_by_license_number ***'
GO
CREATE PROCEDURE dbo.sp_retrieve_drivers_license_by_license_number
(
	@LicenseNumber nvarchar(100)
)
AS
	BEGIN
		SELECT
			Count(*)
		FROM
			DriversLicense
		WHERE
			LicenseNumber = @LicenseNumber
	END
GO

/**************************************
Chantal Shirley
Created: 2021/02/14
Description: Drivers License Stored
Procedure Tests
***************************************/
-- Stored Procedure Test Records
/**************************************
-- Comment the line above and below, 
-- if you would like to test drivers 
-- license data 
*/
print '' print '*** Stored Procedure Tests ***'

INSERT INTO dbo.DriversLicense
(
	LicenseNumber,
	EmployeeID,
	LicenseType,
	LicenseIssuedDate,
	LicenseExpiryDate,
	IsActive

)
VALUES
(
	'IA834343424',
	100000,
	'C',
	'2018/01/15',
	'2025/02/13',
	1
),
(
	'IA305894358',
	100001,
	'A',
	'2015/05/13',
	'2022/11/04',
	1
)
GO
/*
DECLARE @LicenseNumber nvarchar(100) = 'IA34234832',
		@EmployeeID int = 342932,
		@LicenseType nvarchar(15) = 'A',
		@LicenseIssuedDate date = '2014/09/15',
		@LicenseExpiryDate date = '2019/05/20',
		@IsActive bit = 1

EXEC sp_add_drivers_license @LicenseNumber,
							@EmployeeID,
							@LicenseType,
							@LicenseIssuedDate,
							@LicenseExpiryDate,
							@IsActive
GO

SELECT *
FROM
	DriversLicense
GO

**************************************/
