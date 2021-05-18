/*
Author: Jakub Kawski
Created: 2021/03/12

PickUpTicket Table Stored Procedures, Fake Data
*/

print''print'***using backontrack_db ***'
GO
USE backontrack_db
GO
/*
Author : Jakub Kawski
Created : 2021/03/12
*/
print '' print '**** Creating PickUpTicket Tables ****'
GO
CREATE TABLE [dbo].[PickUpTicket](
	[TicketID]			[int]	NOT NULL,
	[DonationID]		[int]	NOT NULL,
	[GeoID]				[int]	NOT NULL,
	[TimeRangeStart]	[time]	NOT NULL,
	[TimeRangeEnd]		[time]	NOT NULL,
	[RequestDateStart]	[date]	NOT NULL,
	[RequestDateEnd]	[date]	NOT NULL,
	CONSTRAINT [fk_pickupticket_ticketID] FOREIGN KEY([TicketID]) REFERENCES [dbo].[Ticket]([TicketID]),
	/*CONSTRAINT [fk_pickupticket_donationID] FOREIGN KEY([DonationID]) REFERENCES [dbo].[Donation]([DonationID]),*/
	CONSTRAINT [fk_pickupticket_geoID] FOREIGN KEY([GeoID]) REFERENCES [dbo].[GeoLocation]([GeoID])
)
GO
/*
Author : Jakub Kawski
Created : 2021/03/12
*/
print '' print '**** Creating sp_Insert_PickUp_Ticket ****'
GO
CREATE PROCEDURE [dbo].[sp_Insert_PickUp_Ticket]
(
	@DonationID			[int],
	@GeoID				[int],
	@TimeRangeStart		[time],
	@TimeRangeEnd		[time],
	@RequestDateStart	[date],
	@RequestDateEnd		[date]
)
AS
	BEGIN
		DECLARE @TicketID [int]
		EXEC @TicketID = sp_insert_new_ticket @TicketTypeID = 2;
		INSERT INTO PickUpTicket
			([TicketID], [DonationID], [GeoID], [TimeRangeStart], [TimeRangeEnd], [RequestDateStart], [RequestDateEnd])
		VALUES
			(@TicketID, @DonationID, @GeoID, @TimeRangeStart, @TimeRangeEnd, @RequestDateStart, @RequestDateEnd)
	END
GO

/*
Author : Jakub Kawski
Created : 2021/03/12
*/
print''print'**** Creating sp_Select_All_Delivery_Tickets ****'
GO
CREATE PROCEDURE [dbo].[sp_Select_All_PickUp_Tickets]
AS
	BEGIN
		SELECT 
			PickUpTicket.TicketID,
			PickUpTicket.DonationID,
			PickUpTicket.GeoID,
			PickUpTicket.TimeRangeStart,	
			PickUpTicket.TimeRangeEnd,		
			PickUpTicket.RequestDateStart,	
			PickUpTicket.RequestDateEnd,		
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
			ZipCode.State
			/*GeoLocation.Coordinate,*/
		FROM PickUpTicket
		JOIN Ticket
			ON PickUpTicket.TicketID = Ticket.TicketID
		JOIN GeoLocation
			ON PickUpTicket.GeoID = GeoLocation.GeoID
		JOIN TicketStatus
			ON Ticket.StatusID = TicketStatus.StatusID
		JOIN ZipCode
			ON GeoLocation.ZipCode = ZipCode.ZipCode
	END
GO


/*
Author : Jakub Kawski
Created : 2021/03/19
*/
print''print'****creating sp_update_pickup_ticket****'
GO
CREATE PROCEDURE [dbo].[sp_update_pickup_ticket]
(
	@TicketID [int],
	@OldDonationID [int],
	@OldGeoID [int],
	@OldRouteID [int]=null,
	@OldStopNumber [int],
	@OldEstimatedArrival [datetime],
	@OldStatusID [int],
	@OldNotes [nvarchar](500),
	@OldTimeRangeStart [time],
	@OldTimeRangeEnd [time],
	@OldRequestDateStart [date],
	@OldRequestDateEnd [date],
	@NewDonationID [int],
	@NewGeoID [int],
	@NewRouteID [int]=null,
	@NewStopNumber [int],
	@NewEstimatedArrival [datetime],
	@NewStatusID [int],
	@NewNotes [nvarchar](500),
	@NewTimeRangeStart [time],
	@NewTimeRangeEnd [time],
	@NewRequestDateStart [date],
	@NewRequestDateEnd [date]
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
		UPDATE dbo.PickUpTicket
		SET DonationID = @NewDonationID,
			GeoID = @NewGeoID,
			RequestDateStart = @NewRequestDateStart,
			RequestDateEnd = @NewRequestDateEnd,
			TimeRangeStart = @NewTimeRangeStart,
			TimeRangeEnd = @NewTimeRangeEnd
		WHERE TicketID = @TicketID
			AND DonationID = @OldDonationID
			AND GeoID = @OldGeoID
			AND RequestDateStart = @OldRequestDateStart
			AND RequestDateEnd = @OldRequestDateEnd
			AND TimeRangeStart = @OldTimeRangeStart
			AND TimeRangeEnd = @OldTimeRangeEnd
		
		COMMIT
		RETURN @@ROWCOUNT
	END TRY
	BEGIN CATCH
		
		ROLLBACK
		RETURN -10

	END CATCH
GO

/*
Author : Jakub Kawski
Created : 2021/03/19
*/
print'' print'**** creating sp_delete_delivery_ticket*****'
GO
CREATE PROCEDURE [dbo].[sp_delete_pickup_ticket]
(
	@TicketID[int]
)
AS
	BEGIN TRY
	BEGIN TRANSACTION
		DELETE FROM PickUpTicket
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

print''print'**** creating sp_select_pickup_ticket_by_date ****'
GO
CREATE PROCEDURE [dbo].[sp_select_pickup_ticket_by_date]
(
	@SelectedDate [date]
)
AS
		SELECT 
			PickUpTicket.TicketID,
			PickUpTicket.DonationID,
			PickUpTicket.GeoID,
			PickUpTicket.TimeRangeStart,	
			PickUpTicket.TimeRangeEnd,		
			PickUpTicket.RequestDateStart,	
			PickUpTicket.RequestDateEnd,		
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
			ZipCode.State
			/*GeoLocation.Coordinate,*/
		FROM PickUpTicket
		JOIN Ticket
			ON PickUpTicket.TicketID = Ticket.TicketID
		JOIN GeoLocation
			ON PickUpTicket.GeoID = GeoLocation.GeoID
		JOIN TicketStatus
			ON Ticket.StatusID = TicketStatus.StatusID
		JOIN ZipCode
			ON GeoLocation.ZipCode = ZipCode.ZipCode
		WHERE @SelectedDate BETWEEN PickUpTicket.RequestDateStart AND PickUpTicket.RequestDateEnd
GO

print''print'**** creating sp_select_pickup_ticket_by_routeID ****'
GO
CREATE PROCEDURE [dbo].[sp_select_pickup_ticket_by_routeID]
(
	@RouteID [int]
)
AS
		SELECT 
			PickUpTicket.TicketID,
			PickUpTicket.DonationID,
			PickUpTicket.GeoID,
			PickUpTicket.TimeRangeStart,	
			PickUpTicket.TimeRangeEnd,		
			PickUpTicket.RequestDateStart,	
			PickUpTicket.RequestDateEnd,		
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
			ZipCode.State
			/*GeoLocation.Coordinate,*/
		FROM PickUpTicket
		JOIN Ticket
			ON PickUpTicket.TicketID = Ticket.TicketID
		JOIN GeoLocation
			ON PickUpTicket.GeoID = GeoLocation.GeoID
		JOIN TicketStatus
			ON Ticket.StatusID = TicketStatus.StatusID
		JOIN ZipCode
			ON GeoLocation.ZipCode = ZipCode.ZipCode
		WHERE Ticket.RouteID = @RouteID
GO

/*
Author : Jakub Kawski
Created : 2021/03/12
*//*
print''print'**** creating dumby pickup ticket data ****'
GO
print'' print'**** step one ****'
GO
DECLARE @pickup_geoid_one [int]
EXEC @pickup_geoid_one = sp_Insert_GeoLocation @ZipCode = '52314', 
@StreetAddressLineOne = '123 Lucas St', 
@StreetAddressLineTwo = '',
@Coordinate = 'POINT(50.000 45.000)';

DECLARE @pickup_geoid_two [int]
EXEC @pickup_geoid_two = sp_Insert_GeoLocation @ZipCode = '55522', 
@StreetAddressLineOne = '123 Rain Dr', 
@StreetAddressLineTwo = '',
@Coordinate = 'POINT(50.000 45.000)';

DECLARE @pickup_geoid_three [int]
EXEC @pickup_geoid_three = sp_Insert_GeoLocation @ZipCode = '55521', 
@StreetAddressLineOne = '13 Tree Ln', 
@StreetAddressLineTwo = '',
@Coordinate = 'POINT(50.000 45.0009)';

DECLARE @pickup_geoid_four [int]
EXEC @pickup_geoid_four = sp_Insert_GeoLocation @ZipCode = '55521', 
@StreetAddressLineOne = '45 Cat Rde',
@StreetAddressLineTwo = '',
@Coordinate = 'POINT(50.000 45.000)';

DECLARE @pickup_geoid_five [int]
EXEC @pickup_geoid_five = sp_Insert_GeoLocation @ZipCode = '52314', 
@StreetAddressLineOne = '87 Sasson Way', 
@StreetAddressLineTwo = '',
@Coordinate = 'POINT(50.000 45.000)';
*/
print''print'**** step two ****'


DECLARE @datetimeOrigin [datetime] = GETDATE();
DECLARE @dateStart [date] = CONVERT(date, @datetimeOrigin);
DECLARE @dateEnd [date] = CONVERT(date, DATEADD(day, 10, @datetimeOrigin));
/*DECLARE @timeStart [time] = CONVERT(time, @datetimeOrigin);*/
DECLARE @timeStart [time] = '7:00:00.0';
DECLARE @timeEnd [time] = '17:30:00.0';
/*CONVERT(time, DATEADD(hour, 8, @datetimeOrigin));*/
print''print'**** step 2.5 ****'

EXEC sp_Insert_PickUp_Ticket @DonationID = 10000, @GeoID = 10008, @TimeRangeStart = @timeStart, @TimeRangeEnd = @timeEnd, @RequestDateStart = @dateStart, @RequestDateEnd = @dateEnd;
EXEC sp_Insert_PickUp_Ticket @DonationID = 10001, @GeoID = 10009, @TimeRangeStart = @timeStart, @TimeRangeEnd = @timeEnd, @RequestDateStart = @dateStart, @RequestDateEnd = @dateEnd;
EXEC sp_Insert_PickUp_Ticket @DonationID = 10002, @GeoID = 10010, @TimeRangeStart = @timeStart, @TimeRangeEnd = @timeEnd, @RequestDateStart = @dateStart, @RequestDateEnd = @dateEnd;
EXEC sp_Insert_PickUp_Ticket @DonationID = 10003, @GeoID = 10011, @TimeRangeStart = @timeStart, @TimeRangeEnd = @timeEnd, @RequestDateStart = @dateStart, @RequestDateEnd = @dateEnd;
EXEC sp_Insert_PickUp_Ticket @DonationID = 10004, @GeoID = 10012, @TimeRangeStart = @timeStart, @TimeRangeEnd = @timeEnd, @RequestDateStart = @dateStart, @RequestDateEnd = @dateEnd;
EXEC sp_Insert_PickUp_Ticket @DonationID = 10000, @GeoID = 10013, @TimeRangeStart = @timeStart, @TimeRangeEnd = @timeEnd, @RequestDateStart = @dateStart, @RequestDateEnd = @dateEnd;
EXEC sp_Insert_PickUp_Ticket @DonationID = 10001, @GeoID = 10014, @TimeRangeStart = @timeStart, @TimeRangeEnd = @timeEnd, @RequestDateStart = @dateStart, @RequestDateEnd = @dateEnd;
EXEC sp_Insert_PickUp_Ticket @DonationID = 10002, @GeoID = 10015, @TimeRangeStart = @timeStart, @TimeRangeEnd = @timeEnd, @RequestDateStart = @dateStart, @RequestDateEnd = @dateEnd;
EXEC sp_Insert_PickUp_Ticket @DonationID = 10003, @GeoID = 10016, @TimeRangeStart = @timeStart, @TimeRangeEnd = @timeEnd, @RequestDateStart = @dateStart, @RequestDateEnd = @dateEnd;
EXEC sp_Insert_PickUp_Ticket @DonationID = 10004, @GeoID = 10017, @TimeRangeStart = @timeStart, @TimeRangeEnd = @timeEnd, @RequestDateStart = @dateStart, @RequestDateEnd = @dateEnd;
EXEC sp_Insert_PickUp_Ticket @DonationID = 10000, @GeoID = 10018, @TimeRangeStart = @timeStart, @TimeRangeEnd = @timeEnd, @RequestDateStart = @dateStart, @RequestDateEnd = @dateEnd;
EXEC sp_Insert_PickUp_Ticket @DonationID = 10001, @GeoID = 10019, @TimeRangeStart = @timeStart, @TimeRangeEnd = @timeEnd, @RequestDateStart = @dateStart, @RequestDateEnd = @dateEnd;
EXEC sp_Insert_PickUp_Ticket @DonationID = 10002, @GeoID = 10020, @TimeRangeStart = @timeStart, @TimeRangeEnd = @timeEnd, @RequestDateStart = @dateStart, @RequestDateEnd = @dateEnd;
EXEC sp_Insert_PickUp_Ticket @DonationID = 10003, @GeoID = 10021, @TimeRangeStart = @timeStart, @TimeRangeEnd = @timeEnd, @RequestDateStart = @dateStart, @RequestDateEnd = @dateEnd;
EXEC sp_Insert_PickUp_Ticket @DonationID = 10004, @GeoID = 10022, @TimeRangeStart = @timeStart, @TimeRangeEnd = @timeEnd, @RequestDateStart = @dateStart, @RequestDateEnd = @dateEnd;
EXEC sp_Insert_PickUp_Ticket @DonationID = 10000, @GeoID = 10023, @TimeRangeStart = @timeStart, @TimeRangeEnd = @timeEnd, @RequestDateStart = @dateStart, @RequestDateEnd = @dateEnd;
EXEC sp_Insert_PickUp_Ticket @DonationID = 10001, @GeoID = 10024, @TimeRangeStart = @timeStart, @TimeRangeEnd = @timeEnd, @RequestDateStart = @dateStart, @RequestDateEnd = @dateEnd;
EXEC sp_Insert_PickUp_Ticket @DonationID = 10002, @GeoID = 10025, @TimeRangeStart = @timeStart, @TimeRangeEnd = @timeEnd, @RequestDateStart = @dateStart, @RequestDateEnd = @dateEnd;
EXEC sp_Insert_PickUp_Ticket @DonationID = 10003, @GeoID = 10026, @TimeRangeStart = @timeStart, @TimeRangeEnd = @timeEnd, @RequestDateStart = @dateStart, @RequestDateEnd = @dateEnd;
EXEC sp_Insert_PickUp_Ticket @DonationID = 10004, @GeoID = 10027, @TimeRangeStart = @timeStart, @TimeRangeEnd = @timeEnd, @RequestDateStart = @dateStart, @RequestDateEnd = @dateEnd;
EXEC sp_Insert_PickUp_Ticket @DonationID = 10000, @GeoID = 10028, @TimeRangeStart = @timeStart, @TimeRangeEnd = @timeEnd, @RequestDateStart = @dateStart, @RequestDateEnd = @dateEnd;
EXEC sp_Insert_PickUp_Ticket @DonationID = 10001, @GeoID = 10029, @TimeRangeStart = @timeStart, @TimeRangeEnd = @timeEnd, @RequestDateStart = @dateStart, @RequestDateEnd = @dateEnd;
EXEC sp_Insert_PickUp_Ticket @DonationID = 10002, @GeoID = 10030, @TimeRangeStart = @timeStart, @TimeRangeEnd = @timeEnd, @RequestDateStart = @dateStart, @RequestDateEnd = @dateEnd;
EXEC sp_Insert_PickUp_Ticket @DonationID = 10003, @GeoID = 10031, @TimeRangeStart = @timeStart, @TimeRangeEnd = @timeEnd, @RequestDateStart = @dateStart, @RequestDateEnd = @dateEnd;
EXEC sp_Insert_PickUp_Ticket @DonationID = 10004, @GeoID = 10032, @TimeRangeStart = @timeStart, @TimeRangeEnd = @timeEnd, @RequestDateStart = @dateStart, @RequestDateEnd = @dateEnd;
