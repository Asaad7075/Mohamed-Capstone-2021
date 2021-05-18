print''print'***using backontrack_db ***'
GO
USE backontrack_db
GO
/*
Author : Jakub Kawski
Created : 2021/02/19
*/
print '' print '**** Creating GeoLocation Table ****'
GO
CREATE TABLE [dbo].[GeoLocation](
	[GeoID]			[int]			IDENTITY(10000, 1)	NOT NULL,
	[Coordinate]	[geography]							NULL, /*Need to be implemented later*/
	[ZipCode]		[nvarchar](25)						NOT NULL,
	[StreetAddressLineOne] [nvarchar](255)				NOT NULL,
	[StreetAddressLineTwo] [nvarchar](255)				NULL,
	CONSTRAINT [pk_GeoID] PRIMARY KEY([GeoID] ASC),
	CONSTRAINT [fk_Geolocation_zipcode] FOREIGN KEY([ZipCode]) REFERENCES [dbo].[ZipCode]([ZipCode])
	/*CONSTRAINT [ak_ZipCode_And_StreetAddress] UNIQUE([ZipCode] ASC, [StreetAddressLineOne])*/
	/*Constraint commented out until we can retrieve cooridnates using api*/
	/*CONSTRAINT [ak_Coordinate] UNIQUE([Coordinate] ASC)*/
)
GO
/*
Author : Jakub Kawski
Created : 2021/02/19
*/
print''print '**** creating sp_Insert_GeoLocation ****'
GO
CREATE PROCEDURE sp_Insert_GeoLocation(
	@ZipCode		[nvarchar](50),
	@StreetAddressLineOne  [nvarchar](255),
	@StreetAddressLineTwo  [nvarchar](255),
	@Coordinate		[nvarchar](255) = NULL
)
AS
	BEGIN
	
		INSERT INTO GeoLocation
			([ZipCode], [StreetAddressLineOne],[StreetAddressLineTwo], [Coordinate])
		VALUES
			(@ZipCode, @StreetAddressLineOne, @StreetAddressLineTwo, geography::STGeomFromText(@Coordinate, 4326))
		SELECT SCOPE_IDENTITY()
		RETURN SCOPE_IDENTITY()
	END
GO
/*
Author : Jakub Kawski
Created : 2021/02/19
*/
print''print '**** creaing sp_select_geolocation_by_address ****'
GO
CREATE PROCEDURE sp_select_geolocation_by_address(
	@StreetAddressLineOne	nvarchar(255),
	@StreetAddressLineTwo	nvarchar(255),
	@Zipcode		nvarchar(25)
)
AS
	BEGIN
		SELECT GeoID,			
			   Coordinate.ToString(),
			   ZipCode,		
			   StreetAddressLineOne,
			   StreetAddressLineTwo
		FROM GeoLocation
		WHERE GeoLocation.StreetAddressLineOne = @StreetAddressLineOne
			AND GeoLocation.StreetAddressLineTwo = @StreetAddressLineTwo
			AND GeoLocation.ZipCode = @ZipCode
	END
GO


