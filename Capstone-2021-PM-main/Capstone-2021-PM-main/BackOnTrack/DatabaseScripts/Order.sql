/*
Author: Jakub Kawski
Created: 2021/25/02

Dummy order table/data
*/

print''print'***using backontrack_db ***'
GO
USE backontrack_db
GO

/*
Author : Jakub Kawski
Created : 2021/02/19
***********************
Chantal Shirley
Updated: 2021/04/08
Adding relation to the 
donation items table.
Updated: 2021/04/19
Created dbo.OrderDonationLineItems table 
to add structure to delivery
tickets. Deleted donation relation from
order table.
*/
print '' print '**** Creating OrderTables ****'
GO
CREATE TABLE dbo.[Order]
(
	[OrderID]	[int]IDENTITY(10000, 1)	NOT NULL,
	[ClientID]	[int]					NOT NULL,
	[DonationID]	[int]					NULL,
	[DateOrdered]	[NVARCHAR](10)			NULL,
	CONSTRAINT [pk_OrderID] PRIMARY KEY ([OrderID] ASC),
	CONSTRAINT [fk_order_clientID] FOREIGN KEY([ClientID]) REFERENCES [dbo].[Client]([ClientID])
)
GO
/*
Author : Jakub Kawski
Created : 2021/02/19
*/
print'' print'**** adding fake order data'
GO
INSERT INTO dbo.[Order]
	(ClientID, DonationID, DateOrdered)
VALUES
	(10000, 1000002, '5-5-2021'),
	(10001, 1000002, '5-5-2021'),
	(10002, 1000002, '5-5-2021'),
	(10003, 1000002, '5-5-2021'),
	(10004, 1000002, '5-5-2021'),
	(10005, 1000002, '5-5-2021'),
	(10006, 1000002, '5-5-2021'),
	(10007, 1000002, '5-5-2021'),
	(10008, 1000002, '5-5-2021'),
	(10009, 1000002, '5-5-2021'),
	(10010, 1000002, '5-5-2021')
GO

/*
Chantal Shirley
Created: 2021/04/19
*/
CREATE TABLE dbo.OrderDonationLineItems
(
	OrderID							INT,
	OrderLineItemSequence			INT IDENTITY(1, 1) NOT NULL,
	DonationID						INT NOT NULL,
	OrderQty						INT NOT NULL,
	CONSTRAINT [pk_order_donation_line_item_sequence] 
		PRIMARY KEY ([OrderID], [OrderLineItemSequence] ASC),
	CONSTRAINT [fk_order_line_item] FOREIGN KEY (OrderID) 
		REFERENCES [dbo].[Order]([OrderID]), 
	CONSTRAINT [fk_donated_item] FOREIGN KEY([DonationID]) 
		REFERENCES [dbo].[Donation]([DonationID])
)
GO


/**************************************
Chantal Shirley
Created: 2021/04/08
Description: Vehicle Stored
Procedure Creation (sp_update_order_by_order_id)
Updated: 2021/04/19
Saving data to line items table.
**************************************
Chantal Shirley
Updated: 2021/04/11
Procedure Creation (sp_select_order_id_by_clientid)
Updated: 2021/04/19
Saving data to line items table.
Updated: 2021/04/19
Procedure Creation (sp_update_order_line_item_by_order_id)
Updated: 2021/04/20
Procedure Creation (sp_insert_order_line_item)
Updated: 2021/04/25
Description: 
sp_select_order_by_orderid_and_clientid
***************************************/
print '' print '*** creating sp_insert_order_line_item ***'
GO
CREATE PROCEDURE dbo.sp_insert_order_line_item
(
	@OrderID		[INT],
	@DonationID		[INT],
	@OrderQty	[INT]
)
AS
	BEGIN
		INSERT INTO dbo.OrderDonationLineItems
		(
			OrderID,
			DonationID,
			OrderQty
		)
		VALUES
		(
			@OrderID,
			@DonationID,
			@OrderQty
		)
		SELECT SCOPE_IDENTITY()
	END
GO

EXEC sp_insert_order_line_item 10004, 1000000, 3
EXEC sp_insert_order_line_item 10004, 1000001, 1
EXEC sp_insert_order_line_item 10004, 1000002, 5
EXEC sp_insert_order_line_item 10005, 1000000, 1
EXEC sp_insert_order_line_item 10006, 1000002, 1



print '' print '*** creating sp_update_order_qty_by_order_id_and_donation_id ***'
GO
CREATE PROCEDURE dbo.sp_update_order_qty_by_order_id_and_donation_id
(
	@OrderID INT,
	@DonationID	INT,
	@OrderQty INT
)
AS
	BEGIN TRANSACTION;
	BEGIN TRY
		UPDATE
			[OrderDonationLineItems]
		SET
			OrderQty = @OrderQty

		WHERE
			OrderID = @OrderID AND
			DonationID = @DonationID
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
	END CATCH

	IF @@TRANCOUNT > 0  
	COMMIT TRANSACTION;  
	RETURN @@ROWCOUNT
GO

EXEC sp_update_order_qty_by_order_id_and_donation_id 10004, 1000000,2
GO

print '' print '*** creating sp_select_orders_by_order_id ***'
GO
CREATE PROCEDURE dbo.sp_select_order_by_order_id
(
	@OrderID	[INT]
)
AS
	BEGIN
		SELECT
			*
		FROM
			OrderDonationLineItems
		WHERE
			OrderID = @OrderID
		ORDER BY
			OrderID DESC
	END
GO

print '' print '*** creating sp_select_order_by_orderid_and_clientid ***'
GO
CREATE PROCEDURE dbo.sp_select_order_by_orderid_and_clientid
(
	@OrderID	int,
	@ClientID	int
)
AS
	BEGIN
		SELECT Count(*)
		FROM
			[Order]
		WHERE
			OrderID = @OrderID AND
			ClientID = @ClientID
		RETURN
			@@ROWCOUNT
	END
GO

EXEC sp_select_order_by_orderid_and_clientid 10004, 10004
GO

--select *
--from orderdonationlineitems
--go

--EXEC sp_select_order_by_order_id 10004
--GO

--print '' print '*** creating sp_update_order_donor_id_by_order_id ***'
--GO
--CREATE PROCEDURE dbo.sp_update_order_donation_id_by_order_id
--(
--	@OrderID int,
--	@DonationID int
--)
--AS
--	BEGIN TRANSACTION;
--	BEGIN TRY
--		UPDATE
--			[Order]
--		SET
--			DonationID = @DonationID
--		WHERE
--			OrderID = @OrderID
--	END TRY
--	BEGIN CATCH
--		IF @@TRANCOUNT > 0
--			ROLLBACK TRANSACTION
--	END CATCH

--	IF @@TRANCOUNT > 0  
--	COMMIT TRANSACTION;  
--	RETURN @@ROWCOUNT
--GO

--EXEC sp_update_order_donation_id_by_order_id 10004, 1000000
--GO

--EXEC sp_update_order_donation_id_by_order_id 10005, 1000001
--GO

--EXEC sp_update_order_donation_id_by_order_id 10006, 1000002
--GO

--print '' print '*** creating sp_select_donation_id_by_order_id ***'
--GO
--CREATE PROCEDURE dbo.sp_select_donation_id_by_order_id
--(
--	@OrderID	int
--)
--AS
--	BEGIN
--		SELECT
--			DonationID
--		FROM
--			[Order]
--		WHERE
--			OrderID = @OrderID
--	END
--GO

--EXEC sp_select_donation_id_by_order_id 10004
--GO
--EXEC sp_select_donation_id_by_order_id 10005
--GO
--EXEC sp_select_donation_id_by_order_id 10006
--GO

/*************************************
Richard Schroeder
Created: 2021/04/16
Description: Store new order
Procedure Creation (sp_insert_order)
**************************************
Richard Schroeder
Updated: 2021/04/27
Description: Added DateOrdered parameter 
			 to pull like orders by date
**************************************/
print '' print '*** creating sp_insert_order ***'
GO
CREATE PROCEDURE dbo.sp_insert_order
(
	@ClientID      	[INT],
	@DonationID    	[INT],
	@DateOrdered	[NVARCHAR](10)
)
AS
	BEGIN
		INSERT INTO [dbo].[Order]
			([ClientID], [DonationID], [DateOrdered])
		VALUES
			(@ClientID, @DonationID, @DateOrdered) 
	    SELECT SCOPE_IDENTITY()
	END
GO

/**************************************
Richard Schroeder
Created: 2021/04/16
Description: Select all orders by client id
Procedure Creation (sp_select_orders_by_client_id)
**************************************
Richard Schroeder
Updated: 2021/04/27
Description: Added DateOrdered parameter 
			 to pull like orders by date
**************************************/
print '' print '*** creating sp_select_orders_by_client_id ***'
GO
CREATE PROCEDURE dbo.sp_select_orders_by_client_id
(
	@ClientID       [INT]
)
AS
	BEGIN
		SELECT OrderID, DonationID, ClientID, DateOrdered
        FROM [dbo].[Order]
        WHERE ClientID = @ClientID
        RETURN @@ROWCOUNT
	END
GO