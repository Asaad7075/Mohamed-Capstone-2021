/*
Author: Jakub Kawski
Created: 2021/03/22

RideTicket Table Stored Procedures, Fake Data
*/

print''print'***using backontrack_db ***'
GO
USE backontrack_db
GO
/*
Author : Jakub Kawski
Created : 2021/03/22
*/
print '' print '**** Creating RideTicket Tables ****'
GO
CREATE TABLE [dbo].[RideTicket](
	[TicketID]			[int]	NOT NULL,
	[ClientID]			[int]	NOT NULL,
	[FetchGeoID]		[int]	NOT NULL,
	[DestinationGeoID]	[int]	NOT NULL,
	[RequiresHandicapAccessibleVehicle] [bit] NOT NULL DEFAULT 0,
	[TimeRangeStart]	[time]	NOT NULL,
	[TimeRangeEnd]		[time]	NOT NULL,
	[DateOfRide]		[date]	NOT NULL,
	CONSTRAINT [fk_rideticket_ticketID] FOREIGN KEY([TicketID]) REFERENCES [dbo].[Ticket]([TicketID]),
	/*CONSTRAINT [fk_rideticket_donationID] FOREIGN KEY([ClientID]) REFERENCES [dbo].[Client]([ClientID]),*/
	CONSTRAINT [fk_rideticket_fetchgeoID] FOREIGN KEY([FetchGeoID]) REFERENCES [dbo].[GeoLocation]([GeoID]),
	CONSTRAINT [fk_rideticket_destinationgeoID] FOREIGN KEY([DestinationGeoID]) REFERENCES [dbo].[GeoLocation]([GeoID])
)
GO
/*
Author : Jakub Kawski
Created : 2021/03/12
*/
print '' print '**** Creating sp_Insert_Ride_Ticket ****'
GO
CREATE PROCEDURE [dbo].[sp_Insert_Ride_Ticket]
(
	@ClientID			[int],
	@FetchGeoID			[int],
	@DestinationGeoID	[int],
	@TimeRangeStart		[time],
	@TimeRangeEnd		[time],
	@DateOfRide			[date],
	@RequiresHandicapAccessibleVehicle [bit]
)
AS
	BEGIN
		DECLARE @TicketID [int]
		EXEC @TicketID = sp_insert_new_ticket @TicketTypeID = 3;
		INSERT INTO RideTicket
			([TicketID], [ClientID], [FetchGeoID], [DestinationGeoID], [TimeRangeStart], [TimeRangeEnd], [DateOfRide], [RequiresHandicapAccessibleVehicle])
		VALUES
			(@TicketID, @ClientID, @FetchGeoID, @DestinationGeoID, @TimeRangeStart, @TimeRangeEnd, @DateOfRide, @RequiresHandicapAccessibleVehicle)
	END
GO

/*
Author : Jakub Kawski
Created : 2021/03/22
*/
print''print'**** Creating sp_Select_All_ride_Tickets ****'
GO
CREATE PROCEDURE [dbo].[sp_Select_All_Ride_Tickets]
AS
	BEGIN
		SELECT 
			RideTicket.TicketID,
			RideTicket.ClientID,
			RideTicket.FetchGeoID,
			RideTicket.DestinationGeoID,
			RideTicket.TimeRangeStart,	
			RideTicket.TimeRangeEnd,		
			RideTicket.DateOfRide,	
			RideTicket.RequiresHandicapAccessibleVehicle,
			Ticket.RouteID,
			Ticket.StopNumber,
			Ticket.CreatedAt,
			Ticket.StatusID,
			TicketStatus.StatusDescription,
			Ticket.Notes,
			Ticket.EstimatedArrival,			
			fetchGeoLocation.ZipCode,
			fetchGeoLocation.StreetAddressLineOne,
			fetchGeoLocation.StreetAddressLineTwo,
			fetchZipCode.City,
			fetchZipCode.State,
			destinationGeoLocation.ZipCode,
			destinationGeoLocation.StreetAddressLineOne,
			destinationGeoLocation.StreetAddressLineTwo,
			destinationZipCode.City,
			destinationZipCode.State,
			Client.FirstName,
			Client.LastName
			/*GeoLocation.Coordinate,*/
		FROM RideTicket
		JOIN Ticket
			ON RideTicket.TicketID = Ticket.TicketID
		JOIN GeoLocation AS fetchGeoLocation
			ON RideTicket.FetchGeoID = fetchGeoLocation.GeoID
		JOIN GeoLocation AS destinationGeoLocation
			ON RideTicket.DestinationGeoID = destinationGeoLocation.GeoID
		JOIN TicketStatus
			ON Ticket.StatusID = TicketStatus.StatusID
		JOIN ZipCode AS fetchZipCode
			ON fetchGeoLocation.ZipCode = fetchZipCode.ZipCode
		JOIN ZipCode AS destinationZipCode
			ON destinationGeoLocation.ZipCode = destinationZipCode.ZipCode
		JOIN Client 
			ON RideTicket.ClientID = Client.ClientID
	END
GO


/*
Author : Jakub Kawski
Created : 2021/03/22
*/
print''print'****creating sp_update_ride_ticket****'
GO
CREATE PROCEDURE [dbo].[sp_update_ride_ticket]
(
	@TicketID [int],
	@OldClientID [int],
	@OldFetchGeoID [int],
	@OldDestinationGeoID [int],
	@OldRouteID [int]=null,
	@OldStopNumber [int],
	@OldEstimatedArrival [datetime],
	@OldStatusID [int],
	@OldNotes [nvarchar](500),
	@OldTimeRangeStart [time],
	@OldTimeRangeEnd [time],
	@OldDateOfRide [date],
	@OldRequiresHandicapAccessibleVehicle [bit],
	@NewClientID [int],
	@NewFetchGeoID [int],
	@NewDestinationGeoID [int],
	@NewRouteID [int]=null,
	@NewStopNumber [int],
	@NewEstimatedArrival [datetime],
	@NewStatusID [int],
	@NewNotes [nvarchar](500),
	@NewTimeRangeStart [time],
	@NewTimeRangeEnd [time],
	@NewDateOfRide [date],
	@NewRequiresHandicapAccessibleVehicle [bit]
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
		UPDATE dbo.RideTicket
		SET ClientID = @NewClientID,
			FetchGeoID = @NewFetchGeoID,
			DestinationGeoID = @NewDestinationGeoID,
			DateOfRide = @NewDateOfRide,
			TimeRangeStart = @NewTimeRangeStart,
			TimeRangeEnd = @NewTimeRangeEnd,
			RequiresHandicapAccessibleVehicle = @NewRequiresHandicapAccessibleVehicle
		WHERE TicketID = @TicketID
			AND ClientID = @OldClientID
			AND FetchGeoID = @OldFetchGeoID
			AND DestinationGeoID = @OldDestinationGeoID
			AND DateOfRide = @OldDateOfRide
			AND TimeRangeStart = @OldTimeRangeStart
			AND TimeRangeEnd = @OldTimeRangeEnd
			AND RequiresHandicapAccessibleVehicle = @OldRequiresHandicapAccessibleVehicle 
		
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
Created : 2021/03/22
*/
print'' print'**** creating sp_delete_ride_ticket*****'
GO
CREATE PROCEDURE [dbo].[sp_delete_ride_ticket]
(
	@TicketID[int]
)
AS
	BEGIN TRY
	BEGIN TRANSACTION
		DELETE FROM RideTicket
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


print''print'**** creating sp_select_ride_ticket_by_date ****'
GO
CREATE PROCEDURE [dbo].[sp_select_ride_ticket_by_date]
(
	@SelectedDate [date]
)
AS
		SELECT 
			RideTicket.TicketID,
			RideTicket.ClientID,
			RideTicket.FetchGeoID,
			RideTicket.DestinationGeoID,
			RideTicket.TimeRangeStart,	
			RideTicket.TimeRangeEnd,		
			RideTicket.DateOfRide,	
			RideTicket.RequiresHandicapAccessibleVehicle,
			Ticket.RouteID,
			Ticket.StopNumber,
			Ticket.CreatedAt,
			Ticket.StatusID,
			TicketStatus.StatusDescription,
			Ticket.Notes,
			Ticket.EstimatedArrival,			
			fetchGeoLocation.ZipCode,
			fetchGeoLocation.StreetAddressLineOne,
			fetchGeoLocation.StreetAddressLineTwo,
			fetchZipCode.City,
			fetchZipCode.State,
			destinationGeoLocation.ZipCode,
			destinationGeoLocation.StreetAddressLineOne,
			destinationGeoLocation.StreetAddressLineTwo,
			destinationZipCode.City,
			destinationZipCode.State,
			Client.FirstName,
			Client.LastName
			/*GeoLocation.Coordinate,*/
		FROM RideTicket
		JOIN Ticket
			ON RideTicket.TicketID = Ticket.TicketID
		JOIN GeoLocation AS fetchGeoLocation
			ON RideTicket.FetchGeoID = fetchGeoLocation.GeoID
		JOIN GeoLocation AS destinationGeoLocation
			ON RideTicket.DestinationGeoID = destinationGeoLocation.GeoID
		JOIN TicketStatus
			ON Ticket.StatusID = TicketStatus.StatusID
		JOIN ZipCode AS fetchZipCode
			ON fetchGeoLocation.ZipCode = fetchZipCode.ZipCode
		JOIN ZipCode AS destinationZipCode
			ON destinationGeoLocation.ZipCode = destinationZipCode.ZipCode
		JOIN Client 
			ON RideTicket.ClientID = Client.ClientID
		WHERE RideTicket.DateOfRide = @SelectedDate
GO

print''print'**** creating sp_select_ride_ticket_by_routeID ****'
GO
CREATE PROCEDURE [dbo].[sp_select_ride_ticket_by_routeID]
(
	@RouteID [int]
)
AS
		SELECT 
			RideTicket.TicketID,
			RideTicket.ClientID,
			RideTicket.FetchGeoID,
			RideTicket.DestinationGeoID,
			RideTicket.TimeRangeStart,	
			RideTicket.TimeRangeEnd,		
			RideTicket.DateOfRide,	
			RideTicket.RequiresHandicapAccessibleVehicle,
			Ticket.RouteID,
			Ticket.StopNumber,
			Ticket.CreatedAt,
			Ticket.StatusID,
			TicketStatus.StatusDescription,
			Ticket.Notes,
			Ticket.EstimatedArrival,			
			fetchGeoLocation.ZipCode,
			fetchGeoLocation.StreetAddressLineOne,
			fetchGeoLocation.StreetAddressLineTwo,
			fetchZipCode.City,
			fetchZipCode.State,
			destinationGeoLocation.ZipCode,
			destinationGeoLocation.StreetAddressLineOne,
			destinationGeoLocation.StreetAddressLineTwo,
			destinationZipCode.City,
			destinationZipCode.State,
			Client.FirstName,
			Client.LastName
			/*GeoLocation.Coordinate,*/
		FROM RideTicket
		JOIN Ticket
			ON RideTicket.TicketID = Ticket.TicketID
		JOIN GeoLocation AS fetchGeoLocation
			ON RideTicket.FetchGeoID = fetchGeoLocation.GeoID
		JOIN GeoLocation AS destinationGeoLocation
			ON RideTicket.DestinationGeoID = destinationGeoLocation.GeoID
		JOIN TicketStatus
			ON Ticket.StatusID = TicketStatus.StatusID
		JOIN ZipCode AS fetchZipCode
			ON fetchGeoLocation.ZipCode = fetchZipCode.ZipCode
		JOIN ZipCode AS destinationZipCode
			ON destinationGeoLocation.ZipCode = destinationZipCode.ZipCode
		JOIN Client 
			ON RideTicket.ClientID = Client.ClientID
		WHERE Ticket.RouteID = @RouteID
GO

/*
Author : Jakub Kawski
Created : 2021/03/22
*/
print''print'**** creating dumby pickup ticket data ****'
GO
/*
print'' print'**** step one ****'
GO
DECLARE @ride_geoid_one [int]
EXEC @ride_geoid_one = sp_Insert_GeoLocation @ZipCode = '52314', 
@StreetAddressLineOne = '123 MarySue St', 
@StreetAddressLineTwo = '',
@Coordinate = 'POINT(50.000 45.000)';

DECLARE @ride_geoid_two [int]
EXEC @ride_geoid_two = sp_Insert_GeoLocation @ZipCode = '55522', 
@StreetAddressLineOne = '123 Thunderwave Dr', 
@StreetAddressLineTwo = '',
@Coordinate = 'POINT(50.000 45.000)';

DECLARE @ride_geoid_three [int]
EXEC @ride_geoid_three = sp_Insert_GeoLocation @ZipCode = '55521', 
@StreetAddressLineOne = '13 LeaveValley Ln', 
@StreetAddressLineTwo = '',
@Coordinate = 'POINT(50.000 45.0009)';

DECLARE @ride_geoid_four [int]
EXEC @ride_geoid_four = sp_Insert_GeoLocation @ZipCode = '55521', 
@StreetAddressLineOne = '45 Puma Rd',
@StreetAddressLineTwo = '',
@Coordinate = 'POINT(50.000 45.000)';

DECLARE @ride_geoid_five [int]
EXEC @ride_geoid_five = sp_Insert_GeoLocation @ZipCode = '52314', 
@StreetAddressLineOne = '123 MarySue St', 
@StreetAddressLineTwo = '',
@Coordinate = 'POINT(50.000 45.000)';

DECLARE @ride_geoid_six [int]
EXEC @ride_geoid_six = sp_Insert_GeoLocation @ZipCode = '55522', 
@StreetAddressLineOne = '123 Thunderwave Dr', 
@StreetAddressLineTwo = '',
@Coordinate = 'POINT(50.000 45.000)';

DECLARE @ride_geoid_seven [int]
EXEC @ride_geoid_seven = sp_Insert_GeoLocation @ZipCode = '55521', 
@StreetAddressLineOne = '13 LeaveValley Ln', 
@StreetAddressLineTwo = '',
@Coordinate = 'POINT(50.000 45.0009)';

DECLARE @ride_geoid_eight [int]
EXEC @ride_geoid_eight = sp_Insert_GeoLocation @ZipCode = '55521', 
@StreetAddressLineOne = '45 Puma Rd',
@StreetAddressLineTwo = '',
@Coordinate = 'POINT(50.000 45.000)';
*/
print''print'**** step two ****'


DECLARE @datetimeOrigin [datetime] = GETDATE();
DECLARE @dateOfRide [date] = CONVERT(date, DATEADD(day, 10, @datetimeOrigin));
DECLARE @timeStart [time] = CONVERT(time, @datetimeOrigin);
DECLARE @timeEnd [time] = CONVERT(time, DATEADD(hour, 1, @datetimeOrigin));
print''print'**** step 2.5 ****'

EXEC sp_Insert_Ride_Ticket @ClientID = 10013, @FetchGeoID = 10013, @DestinationGeoID = 10017, @TimeRangeStart = @timeStart, @TimeRangeEnd = @timeEnd, @DateOfRide = @dateOfRide, @RequiresHandicapAccessibleVehicle = 0;
EXEC sp_Insert_Ride_Ticket @ClientID = 10014, @FetchGeoID = 10014, @DestinationGeoID = 10018, @TimeRangeStart = @timeStart, @TimeRangeEnd = @timeEnd, @DateOfRide = @dateOfRide, @RequiresHandicapAccessibleVehicle = 0;
EXEC sp_Insert_Ride_Ticket @ClientID = 10015, @FetchGeoID = 10015, @DestinationGeoID = 10019, @TimeRangeStart = @timeStart, @TimeRangeEnd = @timeEnd, @DateOfRide = @dateOfRide, @RequiresHandicapAccessibleVehicle = 0;
EXEC sp_Insert_Ride_Ticket @ClientID = 10016, @FetchGeoID = 10016, @DestinationGeoID = 10020, @TimeRangeStart = @timeStart, @TimeRangeEnd = @timeEnd, @DateOfRide = @dateOfRide, @RequiresHandicapAccessibleVehicle = 0;

EXEC sp_Insert_Ride_Ticket @ClientID = 10017, @FetchGeoID = 10021, @DestinationGeoID = 10030, @TimeRangeStart = @timeStart, @TimeRangeEnd = @timeEnd, @DateOfRide = @dateOfRide, @RequiresHandicapAccessibleVehicle = 0;
EXEC sp_Insert_Ride_Ticket @ClientID = 10006, @FetchGeoID = 10022, @DestinationGeoID = 10031, @TimeRangeStart = @timeStart, @TimeRangeEnd = @timeEnd, @DateOfRide = @dateOfRide, @RequiresHandicapAccessibleVehicle = 0;
EXEC sp_Insert_Ride_Ticket @ClientID = 10007, @FetchGeoID = 10023, @DestinationGeoID = 10032, @TimeRangeStart = @timeStart, @TimeRangeEnd = @timeEnd, @DateOfRide = @dateOfRide, @RequiresHandicapAccessibleVehicle = 0;
EXEC sp_Insert_Ride_Ticket @ClientID = 10008, @FetchGeoID = 10024, @DestinationGeoID = 10033, @TimeRangeStart = @timeStart, @TimeRangeEnd = @timeEnd, @DateOfRide = @dateOfRide, @RequiresHandicapAccessibleVehicle = 0;
EXEC sp_Insert_Ride_Ticket @ClientID = 10009, @FetchGeoID = 10025, @DestinationGeoID = 10034, @TimeRangeStart = @timeStart, @TimeRangeEnd = @timeEnd, @DateOfRide = @dateOfRide, @RequiresHandicapAccessibleVehicle = 0;
EXEC sp_Insert_Ride_Ticket @ClientID = 10010, @FetchGeoID = 10027, @DestinationGeoID = 10035, @TimeRangeStart = @timeStart, @TimeRangeEnd = @timeEnd, @DateOfRide = @dateOfRide, @RequiresHandicapAccessibleVehicle = 0;
EXEC sp_Insert_Ride_Ticket @ClientID = 10011, @FetchGeoID = 10028, @DestinationGeoID = 10036, @TimeRangeStart = @timeStart, @TimeRangeEnd = @timeEnd, @DateOfRide = @dateOfRide, @RequiresHandicapAccessibleVehicle = 0;
EXEC sp_Insert_Ride_Ticket @ClientID = 10012, @FetchGeoID = 10029, @DestinationGeoID = 10037, @TimeRangeStart = @timeStart, @TimeRangeEnd = @timeEnd, @DateOfRide = @dateOfRide, @RequiresHandicapAccessibleVehicle = 0;

