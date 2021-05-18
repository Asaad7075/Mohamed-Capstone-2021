print''print'***using backontrack_db ***'
GO
USE backontrack_db;
GO


print''print'***making temp location table ***'
;
CREATE TABLE #Location
(
	[Address]				[nvarchar](25),
	[Zip]					[nvarchar](50),
)
;

print''print '***** loading data for temp location table ****'
GO
BULK INSERT #Location
FROM '$(FullScriptDir)\CSV_DATA\address_database.csv'
WITH
(
	DATAFILETYPE = 'widechar',
	FIELDTERMINATOR = ',',
	ROWTERMINATOR = '\n',
	TABLOCK,
	KEEPNULLS -- Treat empty fields as NULLS
)
;
GO


print''print'***inserting  temp location data into geolocation***';

INSERT INTO [dbo].[GeoLocation]
		([StreetAddressLineOne], [StreetAddressLineTwo], [ZipCode]) 
SELECT [Address], '', [Zip]
FROM #Location
;
DROP TABLE #Location
;
GO

/* Need to set the csv to UTF format with powershell shift-click folder */
/* 
Get-Content .\address_database_raw.csv -Encoding UTF8 | % { $_ -replace "NULL","" } | Out-File ".\address_database.csv" 
*/