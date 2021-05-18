/*
Author: Jakub Kawski
Created: 2021/25/02

DeliveryTicket Table Stored Procedures, Fake Data
*/

print''print'***using backontrack_db ***'
GO
USE backontrack_db
GO
/*
Author : Jakub Kawski
Created : 2021/02/19
*/
print '' print '**** Creating DeliveryTickets Tables ****'
GO
CREATE TABLE [dbo].[DeliveryTicket](
	[TicketID]	[int]	NOT NULL,
	[OrderID]	[int]	NOT NULL,
	[GeoID]		[int]	NOT NULL,
	CONSTRAINT [fk_deliveryticket_ticketID] FOREIGN KEY([TicketID]) REFERENCES [dbo].[Ticket]([TicketID]),
	CONSTRAINT [fk_deliveryticket_orderID] FOREIGN KEY([OrderID]) REFERENCES [dbo].[Order]([OrderID]),
	CONSTRAINT [fk_deliveryticket_geoID] FOREIGN KEY([GeoID]) REFERENCES [dbo].[GeoLocation]([GeoID])
)
GO
/*
Author : Jakub Kawski
Created : 2021/02/19
*/
print '' print '**** Creating sp_Insert_Delivery_Ticket ****'
GO
CREATE PROCEDURE [dbo].[sp_Insert_Delivery_Ticket]
(
	@OrderID	[int],
	@GeoID		[int]	
)
AS
	BEGIN
		DECLARE @TicketID [int]
		EXEC @TicketID = sp_insert_new_ticket @TicketTypeID = 1;
		INSERT INTO DeliveryTicket
			([TicketID], [OrderID], [GeoID])
		VALUES
			(@TicketID, @OrderID, @GeoID)
	END
GO
/*
Author : Jakub Kawski
Created : 2021/02/19
*/
print''print'**** Creating sp_Select_All_Delivery_Tickets ****'
GO
CREATE PROCEDURE [dbo].[sp_Select_All_Delivery_Tickets]
AS
	BEGIN
		SELECT 
			DeliveryTicket.TicketID,
			DeliveryTicket.OrderID,
			DeliveryTicket.GeoID,
			Ticket.RouteID,
			Ticket.StopNumber,
			Ticket.CreatedAt,
			Ticket.StatusID,
			TicketStatus.StatusDescription,
			Ticket.Notes,
			Ticket.EstimatedArrival,			
			GeoLocation.ZipCode,
			GeoLocation.StreetAddressLineOne,
			GeoLocation.StreetAddressLineTwo,
			ZipCode.City,
			ZipCode.State,
			Client.FirstName,
			Client.LastName
			/*GeoLocation.Coordinate,*/
		FROM DeliveryTicket
		JOIN Ticket
			ON DeliveryTicket.TicketID = Ticket.TicketID
		JOIN GeoLocation
			ON DeliveryTicket.GeoID = GeoLocation.GeoID
		JOIN TicketStatus
			ON Ticket.StatusID = TicketStatus.StatusID
		JOIN ZipCode
			ON GeoLocation.ZipCode = ZipCode.ZipCode
		JOIN [Order]
			ON DeliveryTicket.OrderID = [Order].OrderID
		JOIN Client
			ON [Order].ClientID = Client.ClientID
	END
GO
/*
Author : Jakub Kawski
Created : 2021/03/04
*/
print''print'****creating sp_update_delivery_ticket****'
GO
CREATE PROCEDURE [dbo].[sp_update_delivery_ticket]
(
	@TicketID [int],
	@OldOrderID [int],
	@OldGeoID [int],
	@OldRouteID [int]=null,
	@OldStopNumber [int],
	@OldEstimatedArrival [datetime],
	@OldStatusID [int],
	@OldNotes [nvarchar](500),
	@NewOrderID [int],
	@NewGeoID [int],
	@NewRouteID [int]=null,
	@NewStopNumber [int],
	@NewEstimatedArrival [datetime],
	@NewStatusID [int],
	@NewNotes [nvarchar](500)
)
AS
	BEGIN TRY
		BEGIN TRANSACTION
		UPDATE Ticket
		SET RouteID = @NewRouteID,
			StopNumber = @NewStopNumber,
			EstimatedArrival = @NewEstimatedArrival,
			StatusID = @NewStatusID,
			Notes = @NewNotes
		WHERE TicketID = @TicketID
			AND (@OldRouteID IS NULL OR RouteID = @OldRouteID)
			AND StopNumber = @OldStopNumber
			AND StatusID = @OldStatusID
			AND Notes = @OldNotes
		;

		UPDATE dbo.DeliveryTicket
		SET OrderID = @NewOrderID,
			GeoID = @NewGeoID
		WHERE TicketID = @TicketID		
			AND OrderID = @OldOrderID
			AND GeoID = @OldGeoID
		
		COMMIT
		RETURN @@ROWCOUNT
	END TRY
	BEGIN CATCH
		
		ROLLBACK
		RETURN -10

	END CATCH
GO

print'' print'**** creating sp_delete_delivery_ticket*****'
GO
CREATE PROCEDURE [dbo].[sp_delete_delivery_ticket]
(
	@TicketID[int]
)
AS
	BEGIN TRY
	BEGIN TRANSACTION
		DELETE FROM DeliveryTicket
		WHERE TicketID = @TicketID
		;
		DELETE FROM Ticket
		WHERE TicketID = @TicketID
		COMMIT
	END TRY
	BEGIN CATCH
		
		ROLLBACK
		RETURN -10

	END CATCH
GO


print''print'**** creating sp_select_delivery_ticket_by_routeID ****'
GO
CREATE PROCEDURE [dbo].[sp_select_delivery_ticket_by_routeID]
(
	@RouteID [int]
)
AS
		SELECT 
			DeliveryTicket.TicketID,
			DeliveryTicket.OrderID,
			DeliveryTicket.GeoID,
			Ticket.RouteID,
			Ticket.StopNumber,
			Ticket.CreatedAt,
			Ticket.StatusID,
			TicketStatus.StatusDescription,
			Ticket.Notes,
			Ticket.EstimatedArrival,			
			GeoLocation.ZipCode,
			GeoLocation.StreetAddressLineOne,
			GeoLocation.StreetAddressLineTwo,
			ZipCode.City,
			ZipCode.State,
			Client.FirstName,
			Client.LastName
			/*GeoLocation.Coordinate,*/
		FROM DeliveryTicket
		JOIN Ticket
			ON DeliveryTicket.TicketID = Ticket.TicketID
		JOIN GeoLocation
			ON DeliveryTicket.GeoID = GeoLocation.GeoID
		JOIN TicketStatus
			ON Ticket.StatusID = TicketStatus.StatusID
		JOIN ZipCode
			ON GeoLocation.ZipCode = ZipCode.ZipCode
		JOIN [Order]
			ON DeliveryTicket.OrderID = [Order].OrderID
		JOIN Client
			ON [Order].ClientID = Client.ClientID
		WHERE Ticket.RouteID = @RouteID
GO

/*
Author : Jakub Kawski
Created : 2021/02/19
*/
/*
print''print'**** creating dumby delivery ticket data ****'
GO
print'' print'**** step one ****'
GO
DECLARE @delivery_geoid_one [int]
EXEC @delivery_geoid_one = sp_Insert_GeoLocation @ZipCode = '55522', 
@StreetAddressLineOne = '123 Rainbow Ave', 
@StreetAddressLineTwo = '',
@Coordinate = 'POINT(-122.35900 47.65129)';

DECLARE @delivery_geoid_two [int]
EXEC @delivery_geoid_two = sp_Insert_GeoLocation @ZipCode = '55522', 
@StreetAddressLineOne = '10 Main Ave', 
@StreetAddressLineTwo = '',
@Coordinate = 'POINT(-122.35900 47.65129)';

DECLARE @delivery_geoid_three [int]
EXEC @delivery_geoid_three = sp_Insert_GeoLocation @ZipCode = '55521', 
@StreetAddressLineOne = '23 Dog St', 
@StreetAddressLineTwo = '',
@Coordinate = 'POINT(-122.35900 47.65129)';

DECLARE @delivery_geoid_four [int]
EXEC @delivery_geoid_four = sp_Insert_GeoLocation @ZipCode = '55521', 
@StreetAddressLineOne = '45 Cat Rde',
@StreetAddressLineTwo = '',
@Coordinate = 'POINT(-122.35900 47.65129)';

DECLARE @delivery_geoid_five [int]
EXEC @delivery_geoid_five = sp_Insert_GeoLocation @ZipCode = '55522', 
@StreetAddressLineOne = '20 Backwater Rd', 
@StreetAddressLineTwo = '',
@Coordinate = 'POINT(-122.35900 47.65129)';
*/
print''print'**** creating fake delivery ticket data ****'

EXEC sp_Insert_Delivery_Ticket @OrderID = 10000, @GeoID = 10035;
EXEC sp_Insert_Delivery_Ticket @OrderID = 10001, @GeoID = 10036;
EXEC sp_Insert_Delivery_Ticket @OrderID = 10002, @GeoID = 10037;
EXEC sp_Insert_Delivery_Ticket @OrderID = 10003, @GeoID = 10038;
EXEC sp_Insert_Delivery_Ticket @OrderID = 10004, @GeoID = 10039;
EXEC sp_Insert_Delivery_Ticket @OrderID = 10005, @GeoID = 10040;
EXEC sp_Insert_Delivery_Ticket @OrderID = 10006, @GeoID = 10041;
EXEC sp_Insert_Delivery_Ticket @OrderID = 10007, @GeoID = 10042;
EXEC sp_Insert_Delivery_Ticket @OrderID = 10008, @GeoID = 10043;
EXEC sp_Insert_Delivery_Ticket @OrderID = 10009, @GeoID = 10044;
EXEC sp_Insert_Delivery_Ticket @OrderID = 10010, @GeoID = 10045;

GO

print''print'****creating fake delivery ticket data 2*****'
UPDATE Ticket
SET RouteID = 100000,
	StatusID = 2
WHERE TicketID = 10041
;
UPDATE Ticket
SET RouteID = 100000,
	StatusID = 2
WHERE TicketID = 10042
;
UPDATE Ticket
SET RouteID = 100000,
	StatusID = 2
WHERE TicketID = 10043
;
UPDATE Ticket
SET RouteID = 100000,
	StatusID = 2
WHERE TicketID = 10044
;
UPDATE Ticket
SET RouteID = 100000,
	StatusID = 2
WHERE TicketID = 10045
;
UPDATE Ticket
SET RouteID = 100000,
	StatusID = 2
WHERE TicketID = 10046
;
UPDATE Ticket
SET RouteID = 100000,
	StatusID = 2
WHERE TicketID = 10047
;



/*
EXEC sp_update_delivery_ticket
	@TicketID = 10000,
	@OldOrderID = 10001,
	@OldGeoID = @delivery_geoid_one,
	@OldRouteID = -1,
	@OldStopNumber = -1,
	@OldEstimatedArrival = NULL,
	@OldStatusID [int],
	@OldNotes [nvarchar](500),
	@NewOrderID [int],
	@NewGeoID [int],
	@NewRouteID [int],
	@NewStopNumber [int],
	@NewEstimatedArrival [datetime],
	@NewStatusID [int],
	@NewNotes [nvarchar](500)*/

/**************************************
Chantal Shirley
Created: 2021/04/08
Description: Vehicle Stored Procedure 
sp_select_delivery_tickets_by_client_id,
Slightly modified Jakub's code to with
where statement for sproc.
***************************************
Chantal Shirley
Updated: 2021/04/12
Description: 
sp_select_delivery_ticket_by_order_id
***************************************/
print '' print '*** sp_select_delivery_tickets_by_client_id ***'
GO
CREATE PROCEDURE dbo.sp_select_delivery_tickets_by_client_id
(
	@ClientId int
)
AS
	BEGIN
		SELECT 
			DeliveryTicket.TicketID,
			DeliveryTicket.OrderID,
			DeliveryTicket.GeoID,
			Ticket.RouteID,
			Ticket.StopNumber,
			Ticket.CreatedAt,
			Ticket.StatusID,
			TicketStatus.StatusDescription,
			Ticket.Notes,
			Ticket.EstimatedArrival,			
			GeoLocation.ZipCode,
			GeoLocation.StreetAddressLineOne,
			GeoLocation.StreetAddressLineTwo,
			ZipCode.City,
			ZipCode.State,
			Client.FirstName,
			Client.LastName
			/*GeoLocation.Coordinate,*/
		FROM DeliveryTicket
		JOIN Ticket
			ON DeliveryTicket.TicketID = Ticket.TicketID
		JOIN GeoLocation
			ON DeliveryTicket.GeoID = GeoLocation.GeoID
		JOIN TicketStatus
			ON Ticket.StatusID = TicketStatus.StatusID
		JOIN ZipCode
			ON GeoLocation.ZipCode = ZipCode.ZipCode
		JOIN [Order]
			ON DeliveryTicket.OrderID = [Order].OrderID
		JOIN Client
			ON [Order].ClientID = Client.ClientID
		WHERE
			[Order].ClientID = @ClientID
	END
GO

print '' print '*** sp_select_delivery_ticket_by_order_id ***'
GO
CREATE PROCEDURE dbo.sp_select_delivery_ticket_by_order_id
(
	@OrderID int
)
AS
	BEGIN
		SELECT 
			DeliveryTicket.TicketID,
			DeliveryTicket.OrderID,
			DeliveryTicket.GeoID,
			Ticket.RouteID,
			Ticket.StopNumber,
			Ticket.CreatedAt,
			Ticket.StatusID,
			TicketStatus.StatusDescription,
			Ticket.Notes,
			Ticket.EstimatedArrival,			
			GeoLocation.ZipCode,
			GeoLocation.StreetAddressLineOne,
			GeoLocation.StreetAddressLineTwo,
			ZipCode.City,
			ZipCode.State,
			Client.FirstName,
			Client.LastName
			/*GeoLocation.Coordinate,*/
		FROM DeliveryTicket
		JOIN Ticket
			ON DeliveryTicket.TicketID = Ticket.TicketID
		JOIN GeoLocation
			ON DeliveryTicket.GeoID = GeoLocation.GeoID
		JOIN TicketStatus
			ON Ticket.StatusID = TicketStatus.StatusID
		JOIN ZipCode
			ON GeoLocation.ZipCode = ZipCode.ZipCode
		JOIN [Order]
			ON DeliveryTicket.OrderID = [Order].OrderID
		JOIN Client
			ON [Order].ClientID = Client.ClientID
		WHERE
			[Order].OrderID = @OrderID
	END
GO

