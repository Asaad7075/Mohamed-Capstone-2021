print '' print '*** using backontrack_db ***'
GO
USE backontrack_db
GO
/**************************************
Chantal Shirley
Created: 2021/03/10
Description: Drivers License Class Table Creation
***************************************/

-- Drivers License Table

print '' print '*** creating drivers license table ***'
GO
CREATE TABLE dbo.DriversLicenseClass
(
	LicenseClass nvarchar(50) NOT NULL,
	Description nvarchar (500) NOT NULL
	CONSTRAINT pk_licClass PRIMARY KEY(LicenseClass) 
)
GO

/**************************************
Chantal Shirley
Created: 2021/03/10
Description: Drivers License Class Stored
Procedure Creation (
sp_select_all_drivers_license_classes)
***************************************/
-- Drivers License Class Stored Procedures

print '' print '*** creating sp_select_all_drivers_license_classes ***'
GO
CREATE PROCEDURE dbo.sp_select_all_drivers_license_classes
AS
	BEGIN
		SELECT 
			LicenseClass
		FROM
			DriversLicenseClass
		ORDER BY
			LicenseClass ASC
	END
GO

print '' print '*** Stored Procedure Tests ***'
GO

INSERT INTO dbo.DriversLicenseClass
(
	LicenseClass,
	Description
)
VALUES
(
	'A',
	'Motor vehicles generally over 26,000 lbs, towing loads greater than 10,000 lbs.'
),
(
	'B',
	'Motor vehicles generally over 26,000 lbs, towing loads less than 10,000 lbs.'
),
(
	'C',
	'Motor vehicles generally lower than 26,000 lbs, towing loads less than 10,000 lbs, having no more than 15 passengers.'
),
(
	'M',
	'Motorcycle license.'
)
GO

--EXEC sp_select_all_drivers_license_classes
--GO