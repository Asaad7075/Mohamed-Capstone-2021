print''print'***using backontrack_db ***'
GO
USE backontrack_db
GO

/**************************************
Thomas Stout
Created: 2021/02/18 
Description: Created Inventory table.
**************************************
UpdateFirstName UpdaterLastName
Updated: YYYY/MM/DD
***************************************/
print''print'***create Inventory table***'
GO
CREATE TABLE [dbo].[Inventory](
	[InventoryID]			[int] IDENTITY(100000, 1)	NOT NULL,
	[InventoryQuantity]		[int]						NOT NULL,
	[ItemName]				[nvarchar](250)				NOT NULL,
	[ItemDescription]		[nvarchar](500)				NOT NULL
	CONSTRAINT [pk_InventoryID] PRIMARY KEY ([InventoryID] ASC)
)


/**************************************
Thomas Stout
Created: 2021/02/18 
Description: Stored procedure. Inserts
an item into inventory
**************************************
UpdateFirstName UpdaterLastName
Updated: YYYY/MM/DD
***************************************/
print'' print'*** creating sp_Insert_Inventory_Item ***'
GO
CREATE PROCEDURE [dbo].[sp_Insert_Inventory_Item]
(
	@InventoryQuantity	[int],
	@ItemName			[nvarchar](250),
	@ItemDescription	[nvarchar](500)
)
AS
	BEGIN
		INSERT INTO Inventory
			(InventoryQuantity, ItemName, ItemDescription)
		VALUES
			(@InventoryQuantity, @ItemName, @ItemDescription)
		RETURN @@ROWCOUNT
	END
GO

/**************************************
Thomas Stout
Created: 2021/02/23
Description: Stored procedure. Deletes
an item from inventory
**************************************
UpdateFirstName UpdaterLastName
Updated: YYYY/MM/DD
***************************************/
print'' print'*** creating sp_Delete_Inventory_Item ***'
GO
CREATE PROCEDURE [dbo].[sp_Delete_Inventory_Item]
(
	@InventoryID		[int]
)
AS
	BEGIN		
		DELETE FROM Inventory
			WHERE InventoryID = @InventoryID
		RETURN @@ROWCOUNT
	END
GO

/**************************************
Thomas Stout
Created: 2021/02/23
Description: Stored procedure. Selects
all items from inventory
**************************************
UpdateFirstName UpdaterLastName
Updated: YYYY/MM/DD
***************************************/
print'' print'*** creating sp_Select_All_Inventory_Items ***'
GO
CREATE PROCEDURE [dbo].[sp_Select_All_Inventory_Items]
AS
	BEGIN
		SELECT 	InventoryID, InventoryQuantity, ItemName, ItemDescription
		FROM 	Inventory
		ORDER BY InventoryID ASC
	END
GO

/**************************************
Thomas Stout
Created: 2021/02/23
Description: Stored procedure. Updates
an item from inventory
**************************************
UpdateFirstName UpdaterLastName
Updated: YYYY/MM/DD
***************************************/
print '' print '*** creating sp_Update_Inventory_Item ***'
GO
CREATE PROCEDURE [dbo].[sp_Update_Inventory_Item]
	(
		@InventoryID				[int],
		@InventoryQuantity			[int],
		@ItemName					[nvarchar](250),
		@ItemDescription			[nvarchar](500)
	)
AS
	BEGIN
		UPDATE Inventory
			SET InventoryQuantity = @InventoryQuantity,
				ItemName = @ItemName,
				ItemDescription = @ItemDescription
			WHERE InventoryId = @InventoryID
		RETURN @@ROWCOUNT
	END
GO



