print''print'***using backontrack_db ***'
GO
USE backontrack_db
GO

/**************************************
Richard Schroeder
Created: 2021/03/11
Description: Create Supply Inventory Table
**************************************/
print '' print '*** creating SupplyInventory table ***'
GO
CREATE TABLE [dbo].[SupplyInventory]
(
	[SupplyInventoryID]			[INT] IDENTITY(100000, 1) NOT NULL,
	[SupplySerialNumber]		[INT] NOT NULL UNIQUE, --UNIQUE constraint to not allow duplicate serial numbers
	[MaterialName]				[NVARCHAR](100) NOT NULL,
	[SupplyDescription]			[NVARCHAR](100) NOT NULL,
	[SupplyInventoryQuantity]	[INT] NOT NULL
)
GO

/**************************************
Richard Schroeder
Created: 2021/03/11
Description: Insert test records to the SupplyInventory table
**************************************/
print '' print '*** creating SupplyInventory test records ***'
GO
INSERT INTO [dbo].[SupplyInventory]
		(SupplySerialNumber, MaterialName, SupplyDescription, SupplyInventoryQuantity)
VALUES
		('196735', 'Brown packing tape, 6in wide', 'Brown packing tape to seal boxes', 1000),
		('528149', 'Clear packing tape, 4in wide', 'Clear packing tape', 600),
		('249378', 'Packing Tape Roll', 'Brown packing tape to seal boxes', 200),
		('536417', 'Packing peanuts', '20oz bag of packing peanuts', 850)
GO

/**************************************
Richard Schroeder
Created: 2021/03/12
Description: Create Supply Inventory Table
**************************************/
print'' print'*** creating sp_delete_supply_item ***'
GO
CREATE PROCEDURE [dbo].[sp_delete_supply_item]
(
	@SupplyInventoryID		[int]
)
AS
	BEGIN		
		DELETE FROM SupplyInventory
			WHERE SupplyInventoryID = @SupplyInventoryID
		RETURN @@ROWCOUNT
	END
GO

/**************************************
Richard Schroeder
Created: 2021/03/11
Description: Stored procedure to insert new SupplyInventory records
**************************************/
print '' print '*** creating sp_insert_supply_item ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_supply_item]
	(
		@SupplySerialNumber			[INT],
		@MaterialName				[NVARCHAR](100),
		@SupplyDescription			[NVARCHAR](100),
		@SupplyInventoryQuantity	[INT]
	)
AS
	BEGIN
		INSERT INTO [dbo].[SupplyInventory]
			([SupplySerialNumber], [MaterialName], [SupplyDescription], [SupplyInventoryQuantity])
		VALUES
			(@SupplySerialNumber, @MaterialName, @SupplyDescription, @SupplyInventoryQuantity)
		SELECT SCOPE_IDENTITY()
	END
GO
/**************************************
Richard Schroeder
Created: 2021/03/12
Description: Stored procedure to update existing supply inventory data
**************************************/
print '' print '*** creating sp_update_supply_item ***'
GO
CREATE PROCEDURE [dbo].[sp_update_supply_item]
	(
	    @SupplyInventoryID				[INT],
		@OldSupplySerialNumber			[INT],
		@OldMaterialName				[NVARCHAR](100),
		@OldSupplyDescription			[NVARCHAR](100),
		@OldSupplyInventoryQuantity		[INT],
		@NewSupplySerialNumber			[INT],
		@NewMaterialName				[NVARCHAR](100),
		@NewSupplyDescription			[NVARCHAR](100),
		@NewSupplyInventoryQuantity		[INT]
	)
AS
	BEGIN
		UPDATE [dbo].[SupplyInventory]
			SET SupplySerialNumber = @NewSupplySerialNumber,
				MaterialName = @NewMaterialName,			
				SupplyDescription = @NewSupplyDescription,		
				SupplyInventoryQuantity = @NewSupplyInventoryQuantity
				WHERE SupplyInventoryID = @SupplyInventoryID
				AND SupplySerialNumber = @OldSupplySerialNumber
				AND MaterialName = @OldMaterialName	
				AND SupplyDescription = @OldSupplyDescription
				AND SupplyInventoryQuantity = @OldSupplyInventoryQuantity
		RETURN @@ROWCOUNT
	END
GO
/**************************************
Richard Schroeder
Created: 2021/03/12
Description: Stored procedure to select all supplies in the DB
**************************************/
print '' print '*** creating sp_select_supply_inventory ***'
GO
CREATE PROCEDURE [dbo].[sp_select_supply_inventory]
AS
	BEGIN
		SELECT *
		FROM SupplyInventory
		ORDER BY SupplyInventoryID ASC
	END
GO
