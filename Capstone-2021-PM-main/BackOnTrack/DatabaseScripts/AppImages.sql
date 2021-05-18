print '' print '*** using backontrack_db ***'
GO
USE backontrack_db
GO

/**************************************
Chantal Shirley
Created: 2021/03/24
Description: General Application 
Image Table Creation
***************************************/

-- App Image Table
print '' print '*** creating vehicle image table ***'
GO
CREATE TABLE dbo.AppImages
(
	PhotoName			nvarchar(200),
	FilePhoto			varbinary(max) NOT NULL,
	PhotoDescription	nvarchar(150),
	CONSTRAINT pk_photoName PRIMARY KEY(PhotoName)
)
GO

/**************************************
Chantal Shirley
Created: 2021/03/24
Description: Application Image Stored
Procedure Creation (sp_get_image_by_name)
***************************************/
print '' print '*** sp_get_image_by_name ***'
GO
CREATE PROCEDURE dbo.sp_get_image_by_name 
(
	@PhotoName	nvarchar(200)
)
AS
	BEGIN
		SELECT
			FilePhoto
		FROM
			AppImages
		WHERE
			PhotoName = @PhotoName
	END
GO

print '' print '*** sp_insert_image_by_name ***'
GO
CREATE PROCEDURE sp_insert_image_by_name
(
	@PhotoName	nvarchar(200),
	@FilePhoto	varbinary(max),
	@PhotoDescription nvarchar(150)
)
AS
	BEGIN
		INSERT INTO AppImages
		(
			PhotoName,
			FilePhoto,
			PhotoDescription
		)
		VALUES
		(
			@PhotoName,
			@FilePhoto,
			@PhotoDescription
		)
		SELECT SCOPE_IDENTITY()
	END
GO

-- Stored Procedure Test Records
--/**************************************
-- Chantal Shirley
-- Comment the line above and below, 
-- if you would like to test app 
-- image data 

print '' print '*** Stored Procedure App Image Tests ***'
GO
DECLARE @PhotoName nvarchar(200)='DefaultVehicleImg.png'
DECLARE @PhotoDescription nvarchar(150)='Default image for vehicles that do not have images'
DECLARE @FilePhoto varbinary(max)
Set @FilePhoto = (SELECT * FROM OPENROWSET(BULK '$(FullScriptDir)\Resources\DefaultVehicleImg.png', SINGLE_BLOB) as img)
EXEC sp_insert_image_by_name 
	@PhotoName,
	@FilePhoto,
	@PhotoDescription
GO

--select * from appimages go
--***************************************/