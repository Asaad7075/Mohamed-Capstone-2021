print '' print '*** using backontrack_db ***'
GO
USE backontrack_db
GO

/**************************************
Chantal Shirley
Created: 2021/03/12
Description: Vehicle Image Table Creation
***************************************/

-- Vehicle Image Table
print '' print '*** creating vehicle image table ***'
GO
CREATE TABLE dbo.VehicleImages
(
	VehicleId			int IDENTITY(100000,1),
	VehicleVinNumber	nvarchar(17),
	PhotoName			nvarchar(50),
	PhotoDescription	nvarchar(150),
	FilePhoto			varbinary(max) NOT NULL,
	CONSTRAINT pk_vehicleId PRIMARY KEY(VehicleId),
	CONSTRAINT fk_vehicleImageVinNumber FOREIGN KEY (VehicleVinNumber)
		REFERENCES dbo.Vehicles(VinNumber)
)
GO

/**************************************
Chantal Shirley
Created: 2021/03/12
Description: Vehicle Stored
Procedure Creation (sp_get_image_by_vin )
**************************************
Chantal Shirley
Updated: 2021/03/18
Description: Images will be pulled from
view in Vehicle View file 
established by join table.
***************************************/

print '' print '*** sp_insert_image_by_vin ***'
GO
CREATE PROCEDURE sp_insert_image_by_vin
(
	@VehicleVinNumber	nvarchar(17),
	@FilePhoto			varbinary(max)
)
AS
	BEGIN
		INSERT INTO dbo.VehicleImages
		(
			VehicleVinNumber,
			FilePhoto
		)
		VALUES
		(
			@VehicleVinNumber,
			@FilePhoto
		)
		SELECT SCOPE_IDENTITY()
	END
GO

print '' print '*** sp_update_image_by_vin ***'
GO
CREATE PROCEDURE sp_update_image_by_vin
(
	@VehicleVinNumber	nvarchar(17),
	@FilePhoto			varbinary(max)
)
AS
	BEGIN
		UPDATE
			VehicleImages
		SET
			FilePhoto = @FilePhoto
		WHERE
			VehicleVinNumber = @VehicleVinNumber
		RETURN @@ROWCOUNT
	END
GO

/**************************************
Chantal Shirley
Created: 2021/03/12
Description: Vehicles Images Stored
Procedure Tests
***************************************/
-- Stored Procedure Test Records
--/**************************************
-- Comment the line above and below, 
-- if you would like to test drivers 
-- license data 
print '' print '*** Stored Procedure Vehicle Image Tests ***'
GO
DECLARE @VinNumber nvarchar(17)='0D537YGQFKPFM4JOO'
DECLARE @FilePhoto varbinary(max)
Set @FilePhoto = (SELECT * FROM OPENROWSET(BULK '$(FullScriptDir)\Resources\Peterbilt579.jpg', SINGLE_BLOB) as img)
EXEC sp_insert_image_by_vin 
	@VinNumber,
	@FilePhoto
GO

-- Vehicle courtesy of limited creative commons https://commons.wikimedia.org/wiki/File:2018_Kia_Sedona_LX_3.3L_V6_front_5.23.18.jpg#/media/File:2018_Kia_Sedona_LX_3.3L_V6_front_5.23.18.jpg
INSERT INTO dbo.VehicleImages 
SELECT
	"FJUE7YGQFKPFM4DWE",
	"Kia Sedona", 
	"Multi-passenger mini-van",
	*
FROM
OPENROWSET(
	BULK '$(FullScriptDir)\Resources\KiaSedona.jpg'
	, SINGLE_BLOB
) as image
GO
-- Vehicle courtesy of limited creative commons https://commons.wikimedia.org/wiki/File:2019_Mercedes-Benz_Sprinter_314_CDi_2.1.jpg#/media/File:2019_Mercedes-Benz_Sprinter_314_CDi_2.1.jpg
INSERT INTO dbo.VehicleImages 
SELECT
	"5SX43TNWCO0Q4Y2N6",
	"Mercedes-Benz Sprinter", 
	"Multi-passenger commercial van",
	*
FROM
OPENROWSET(
	BULK '$(FullScriptDir)\Resources\Mercedes-BenzSprinter.jpg'
	, SINGLE_BLOB
) as image
GO
-- Vehicle courtesy of limited creative commons https://flic.kr/p/2j2Gks1
INSERT INTO dbo.VehicleImages 
SELECT
	"PDIE7YGQFKPFM4JOO",
	"Peterbilt Model 579", 
	"Heavy duty truck with 485 Horsepower.",
	*
FROM
OPENROWSET(
	BULK '$(FullScriptDir)\Resources\Peterbilt579.jpg'
	, SINGLE_BLOB
) as image
GO
-- Vehicle courtesy of limited creative commons https://commons.wikimedia.org/wiki/File:2019_Mercedes-Benz_Sprinter_314_CDi_2.1.jpg#/media/File:2019_Mercedes-Benz_Sprinter_314_CDi_2.1.jpg
INSERT INTO dbo.VehicleImages 
SELECT
	"PDIE7YGQFKPFM4JID",
	"Mercedes-Benz Sprinter", 
	"Multi-passenger commercial van",
	*
FROM
OPENROWSET(
	BULK '$(FullScriptDir)\Resources\Mercedes-BenzSprinter.jpg'
	, SINGLE_BLOB
) as image
GO
-- Vehicle courtesy of limited creative commons https://commons.wikimedia.org/wiki/File:2016_Honda_Civic_LX.jpeg#/media/File:2016_Honda_Civic_LX.jpeg
INSERT INTO dbo.VehicleImages 
SELECT
	"PDIE7YGQFKPFM4IDS",
	"Honda Civic", 
	"4-Door Sedan-Sized Vehicle",
	*
FROM
OPENROWSET(
	BULK '$(FullScriptDir)\Resources\HondaCivic.jpeg'
	, SINGLE_BLOB
) as image
GO
-- Vehicle courtesy of limited creative commons https://commons.wikimedia.org/wiki/File:2018_Honda_Accord_12.17.17.jpg#/media/File:2018_Honda_Accord_12.17.17.jpg
INSERT INTO dbo.VehicleImages 
SELECT
	"PDIE7YGQFKPFM4DYS",
	"Honda Accord", 
	"4-Door Sedan-Sized Vehicle",
	*
FROM
OPENROWSET(
	BULK '$(FullScriptDir)\Resources\HondaAccord.jpg'
	, SINGLE_BLOB
) as image
GO
-- Vehicle courtesy of limited creative commons https://commons.wikimedia.org/wiki/File:2021_Toyota_Sienna_Hybrid_Limited,_front_1.14.21.jpg#/media/File:2021_Toyota_Sienna_Hybrid_Limited,_front_1.14.21.jpg
INSERT INTO dbo.VehicleImages 
SELECT
	"PDIE7YGQFKPFM4DWE",
	"Toyota Sienna", 
	"Multi-passenger mini-van",
	*
FROM
OPENROWSET(
	BULK '$(FullScriptDir)\Resources\ToyotaSienna.jpg'
	, SINGLE_BLOB
) as image
GO

--EXEC sp_get_image_by_vin 
--	@VehicleVinNumber = "0D537YGQFKPFM4JOO"
--GO
--***************************************/