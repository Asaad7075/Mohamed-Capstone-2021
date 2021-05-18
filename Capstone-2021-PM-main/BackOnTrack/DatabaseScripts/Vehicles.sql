print '' print '*** using backontrack_db ***'
GO
USE backontrack_db
GO

/**************************************
Chantal Shirley
Created: 2021/02/13
Description: Vehicle Table Creation
***************************************/

-- Vehicle Table

print '' print '*** creating vehicle table ***'
GO
CREATE TABLE dbo.Vehicles
(
	VinNumber			nvarchar(17) NOT NULL,
	LicensePlateNumber	nvarchar(10) NOT NULL,
	VehicleMake			nvarchar(50) NOT NULL,
	VehicleModel		nvarchar(50) NOT NULL,
	VehicleYear			smallint NOT NULL,
	LicenseClass		nvarchar(50) NOT NULL,
	Mileage				int NOT NULL,
	ADACompliant		bit NOT NULL DEFAULT 0,
	IsActive			bit NOT NULL DEFAULT 1,
	GeoID	 			int NOT NULL DEFAULT 0,
	CONSTRAINT pk_vinNo PRIMARY KEY(VinNumber),
	-- CONSTRAINT fk_geoIDVehicles FOREIGN KEY(GeoID)
		-- REFERENCES dbo.GeoLocation(GeoID),
	CONSTRAINT ak_licNo UNIQUE(LicensePlateNumber)
)
GO
/**************************************
Chantal Shirley
Created: 2021/02/13
Description: Vehicle Stored
Procedure Creation (sp_select_all_vehicles,
sp_archive_vehicle_by_vin_number)
***************************************
Chantal Shirley
Updated: 2021/04/07
Updates made around transactions with 
suggestions from Microsoft API:
https://docs.microsoft.com/en-us/sql/t-sql/language-elements/try-catch-transact-sql?view=sql-server-ver15
***************************************/

-- Vehicle Stored Procedures

print '' print '*** creating sp_select_all_active_vehicles ***'
GO
CREATE PROCEDURE dbo.sp_select_all_active_vehicles
AS
	BEGIN
		SELECT
			*
		FROM
			Vehicles
		WHERE
			IsActive != 0
		ORDER BY
			VehicleYear DESC
	END
GO

print '' print '*** creating sp_archive_vehicle_by_vin_number ***'
GO
CREATE PROCEDURE dbo.sp_archive_vehicle_by_vin_number
(
	@VinNumber nvarchar(17)
)
AS
	BEGIN TRANSACTION;
	BEGIN TRY
		UPDATE
			Vehicles
		SET
			IsActive = 0
		WHERE
			VinNumber = @VinNumber
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
	END CATCH

	IF @@TRANCOUNT > 0  
	COMMIT TRANSACTION;  
	RETURN @@ROWCOUNT
GO

print '' print '*** creating sp_update_vehicle_by_vin_number ***'
GO
CREATE PROCEDURE dbo.sp_update_vehicle_by_vin_number
(
	@VinNumber nvarchar(17),
	@Mileage int,
	@LicensePlateNumber nvarchar(10)
)
AS
	BEGIN TRANSACTION;
	BEGIN TRY
		UPDATE
			Vehicles
		SET
			Mileage = @Mileage,
			LicensePlateNumber = @LicensePlateNumber
		WHERE
			VinNumber = @VinNumber
		END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
	END CATCH

	IF @@TRANCOUNT > 0  
	COMMIT TRANSACTION;
	RETURN @@ROWCOUNT
GO

/**************************************
Chantal Shirley
Created: 2021/02/13
Description: Vehicles Stored
Procedure Tests
***************************************/
-- Stored Procedure Test Records
--/**************************************
-- Comment the line above and below, 
-- if you would like to test drivers 
-- license data 

print '' print '*** Stored Procedure Tests for Vehicle data ***'
GO
INSERT INTO dbo.Vehicles
(
	VinNumber,
	LicensePlateNumber,
	VehicleMake,
	VehicleModel,
	VehicleYear,
	LicenseClass,
	Mileage,
	ADACompliant,
	isActive,
	GeoID
)
VALUES
(
	'FJUE7YGQFKPFM4DWE',
	'IFM321',
	'Kia',
	'Sedona',
	2011,
	'C',
	57825,
	1,
	1,
	0
),
(
	'0D537YGQFKPFM4JOO',
	'DI3942',
	'Peterbilt',
	'Model 579',
	2010,
	'A',
	300,
	0,
	1,
	0
),
(
	'5SX43TNWCO0Q4Y2N6',
	'3841JD',
	'Mercedes-Benz',
	'Sprinter',
	2016,
	'C',
	3452,
	1,
	1,
	0
),
(
	'PDIE7YGQFKPFM4JOO',
	'CI3495',
	'Peterbilt',
	'Model 579',
	2010,
	'C',
	300,
	0,
	1,
	0
),
(
	'PDIE7YGQFKPFM4JID',
	'DOE320',
	'Mercedes-Benz',
	'Sprinter',
	2005,
	'C',
	8526,
	1,
	1,
	0
),
(
	'PDIE7YGQFKPFM4IDS',
	'DOE321',
	'Honda',
	'Civic',
	2012,
	'C',
	9854,
	0,
	1,
	0
),
(
	'PDIE7YGQFKPFM4DYS',
	'PEI321',
	'Honda',
	'Accord',
	2012,
	'C',
	105852,
	0,
	1,
	0
),
(
	'PDIE7YGQFKPFM4DWE',
	'PEI3DS',
	'Toyota',
	'Sienna',
	2011,
	'C',
	56816,
	1,
	1,
	0
)
GO

-- EXEC sp_select_all_active_vehicles
-- GO

--DECLARE @VinNumber nvarchar(17)='0D537YGQFKPFM4JOO'
--EXEC sp_archive_vehicle_by_vin_number @VinNumber
--GO

-- EXEC sp_select_all_active_vehicles
-- GO	


--***************************************/

/**************************************
Nate Hepker
Created: 2021/02/14 
Description: Creates stored procedure sp_insert_vehicle.
**************************************
UpdateFirstName UpdaterLastName
Updated: YYYY/MM/DD
***************************************/
print '' print '*** creating stored procedure sp_insert_vehicle ***'
GO
CREATE PROCEDURE[dbo].[sp_insert_vehicle] 
	(
		@VinNumber				[nvarchar](17),
		@LicensePlateNumber		[nvarchar](10),
		@VehicleMake			[nvarchar](50),
		@VehicleModel			[nvarchar](50),
		@VehicleYear			[smallint],
		@LicenseClass			[nvarchar](50),
		@Mileage				[int],
		@ADACompliant			[bit]
	)
AS
	BEGIN
		INSERT INTO	Vehicles
			(VinNumber, LicensePlateNumber, VehicleMake, VehicleModel, VehicleYear,
			 LicenseClass, Mileage, ADACompliant)
		VALUES
			(@VinNumber, @LicensePlateNumber, @VehicleMake, @VehicleModel, @VehicleYear,
			 @LicenseClass, @Mileage, @ADACompliant)
		SELECT SCOPE_IDENTITY()
	END
GO