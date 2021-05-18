print''print'***using backontrack_db ***'
GO
USE backontrack_db;
GO


print''print'***making #zip table ***'
;
CREATE TABLE #ZIP
(
	[Zip]					[nvarchar](25),
	[Type]					[nvarchar](50),
	[Decommissioned]		[nvarchar](100),
	[Primary_City]			[nvarchar](100),
	[State]					[nvarchar](10),
	[County]				[nvarchar](100),
	[Timezone]				[nvarchar](50)
)
;

BULK INSERT #ZIP
FROM '$(FullScriptDir)\CSV_DATA\zip_code_database.csv'
WITH
(
	FIRSTROW = 1,
	DATAFILETYPE = 'widechar',
	FIELDTERMINATOR = ',',
	FIELDQUOTE = '"',
	ROWTERMINATOR = '\n',
	TABLOCK,
	KEEPNULLS -- Treat empty ields as NULLS
)
;


print''print'***inserting zip data ***';

INSERT INTO [dbo].[ZipCode]
		([ZipCode], [City], [State]) 
SELECT [Zip], [Primary_City], [State]
FROM #ZIP
WHERE #ZIP.Decommissioned = '0'
;
DROP TABLE #ZIP
;
GO

/* Need to set the csv to UTF format with powershell shift-click folder */
/* Get-Content .\zip_code_database_raw.csv -Encoding UTF8 | % { $_ -replace "NULL","" } | Out-File ".\zip_code_database.csv" */ 