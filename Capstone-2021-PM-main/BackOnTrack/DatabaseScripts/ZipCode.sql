print''print'***using backontrack_db ***'
GO
USE backontrack_db
GO

/**************************************
Chase Martin
Created: 2021/02/18 
Description: Creating ZipCode table to carry out CRUD functions
for Logistics when signed in as Admin.
**************************************/
print '' print '*** creating zip code table ***'
GO
CREATE TABLE [dbo].[ZipCode](
	[ZipCode]		[nvarchar](25)		NOT NULL,
	[City]			[nvarchar](100)		NOT NULL DEFAULT 2,
	[State]			[nvarchar](2)		NOT NULL,
	[isServicable]	[bit]				NOT NULL DEFAUlT 0,
	[Active]		[bit]				NOT NULL DEFAULT 1
	CONSTRAINT [pk_ZipCode] PRIMARY KEY ([ZipCode] ASC)
)
GO

/*
print '' print '*** creating zip code test records ***'
GO
INSERT INTO [dbo].[ZipCode]
		([ZipCode], [City], [State], [isServicable]) 
	VALUES
		('52314', 'Mount Vernon', 'IA', '1'),
		('55522', 'Solon', 'IA', '1'),
		('55521', 'North Liberty', 'IA', '1'),
		('52315', 'Marion', 'IA', '1')
		
GO*/

/**************************************
Chase Martin
Created: 2021/02/18 
Description: Stored procedure. Selects
all zip codes.
**************************************/
print '' print '*** creating sp_select_all_zipcodes ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_zipcodes]
AS
	BEGIN
		SELECT ZipCode, City, State, isServicable
		FROM ZipCode
	END
GO

/**************************************
Chase Martin
Created: 2021/02/18 
Description: Stored procedure. Inserts
an zip code into ZipCode
**************************************/
print'' print'*** creating sp_add_zipcode ***'
GO
CREATE PROCEDURE [dbo].[sp_add_zipcode]
(
	@ZipCode		[nvarchar](25),
	@City			[nvarchar](100),
	@State			[nvarchar](2),
	@isServicable	[bit]
)
AS
	BEGIN
		INSERT INTO ZipCode
			(ZipCode, City, State, isServicable)
		VALUES
			(@ZipCode, @City, @State, @isServicable)
		RETURN @@ROWCOUNT
	END
GO


/**************************************
Chase Martin
Created: 2021/02/19 
Description: Created sp_update_zipcode.
**************************************/
print '' print '*** creating sp_update_zipcode ***'
GO
CREATE PROCEDURE [dbo].[sp_update_zipcode]
	(
	@NewZipCode				[nvarchar](25),
	@NewCity				[nvarchar](100),
	@NewState			[nvarchar](2),
	@NewIsServicable			bit,
	@OldZipCode			[nvarchar](25),
	@OldCity				[nvarchar](100),
	@OldState			[nvarchar](2),
	@OldIsServicable			bit
	)
AS
	BEGIN
		UPDATE ZipCode
			SET ZipCode = @NewZipCode,
				City = @NewCity,
				State = @NewState,
				isServicable = @NewIsServicable
			WHERE ZipCode = @OldZipCode
			AND City = @OldCity
			AND State = @OldState
			AND isServicable = @OldIsServicable
		RETURN @@ROWCOUNT
	END
GO

/**************************************
Chase Martin
Created: 2021/02/18 
Description: Stored procedure. Selects providers in zip code selected
**************************************/
print '' print '*** creating sp_select_providers_by_zipcode ***'
GO
CREATE PROCEDURE [dbo].[sp_select_providers_by_zipcode]
(
	@ZipCode [nvarchar](25)
)
AS
	BEGIN
		SELECT ZipCode, City, State, isServicable 
		FROM ZipCode
		WHERE ZipCode= @ZipCode
		ORDER BY City ASC
	END
GO

