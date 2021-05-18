print '' print '*** using backontrack_db ***'
GO
USE backontrack_db
GO

/**************************************
Chantal Shirley
Created: 2021/03/18
Description: Vehicle View Creation
***************************************/

-- Vehicle Views

print '' print '*** creating vehicle view table ***'
GO
CREATE VIEW dbo.VehicleImageView AS
	SELECT 
		Vehicles.VinNumber,
		VehicleImages.FilePhoto,
		Vehicles.LicensePlateNumber,
		Vehicles.VehicleMake,
		Vehicles.VehicleModel,
		Vehicles.VehicleYear,
		Vehicles.Mileage
	FROM
		Vehicles LEFT JOIN
			VehicleImages ON
				Vehicles.VinNumber = VehicleImages.VehicleVinNumber
	WHERE
		Vehicles.IsActive = 1
GO

/**************************************
Chantal Shirley
Created: 2021/03/18
Description: Vehicles Images Stored
Procedure Tests
***************************************/

print '' print '*** sp_get_image_by_vin ***'
GO
CREATE PROCEDURE dbo.sp_get_image_by_vin 
(
	@VehicleVinNumber	nvarchar(17)
)
AS
	BEGIN
		SELECT
			FilePhoto
		FROM
			VehicleImageView
		WHERE
			VinNumber = @VehicleVinNumber
	END
GO

print '' print '*** sp_get_all_vehicles_vms ***'
GO
CREATE PROCEDURE dbo.sp_get_all_vehicles_vms
AS
	BEGIN
		SELECT
			*
		FROM
			VehicleImageView
	END
GO

--EXEC sp_get_image_by_vin 
--	@VehicleVinNumber = "0D537YGQFKPFM4JOO"
--GO

--EXEC sp_get_all_vehicles_vms
--GO