print''print'***using backontrack_db ***'
GO
USE backontrack_db
GO

Update Ticket
SET RouteID = 100001,
	StatusID = 2
WHERE TicketID = 10005;

UPDATE Ticket
SET RouteID = 100001,
	StatusID = 2
WHERE TicketID = 10006;

Update Ticket
SET RouteID = 100001,
	StatusID = 2
WHERE TicketID = 10007;

UPDATE Ticket
SET RouteID = 100001,
	StatusID = 2
WHERE TicketID = 10008;

/*Nate Hepker, updates tickets tu use as fake data when running program*//*
Update Ticket
SET RouteID = 100005
WHERE TicketID = 10010;

UPDATE Ticket
SET RouteID = 100006
WHERE TicketID = 10011;

Update Ticket
SET RouteID = 100007
WHERE TicketID = 10012;

UPDATE Ticket
SET RouteID = 100008
WHERE TicketID = 10013;
*/
/*
UPDATE Ticket
SET TicketTypeID = 3
WHERE TicketID = 10010;

UPDATE Ticket
SET TicketTypeID = 3
WHERE TicketID = 10011;

UPDATE Ticket
SET TicketTypeID = 3
WHERE TicketID = 10012

UPDATE Ticket
SET TicketTypeID = 3
WHERE TicketID = 10013;
*/




/**************************************
Nate Hepker
Created: 2021/03/21
Description: Table for RideReviews
***************************************/
print''print'*** Creating the table for Ride Reviews ***'
GO
CREATE TABLE [dbo].[RideReview](
	[RideReviewID]	[int]IDENTITY(100000, 1)NOT NULL,
	[TicketID]		[int]					NOT NULL,
	[ClientID]		[int]					NOT NULL,
	[ClientDriverRating][int]				NULL,
	[ClientComment]	[nvarchar](500)			NULL,
	[DriverID]		[int]					NOT NULL,
	[DriverClientRating][int]				NULL,
	[DriverComment]	[nvarchar](500)			NULL,
	CONSTRAINT [pk_ridereview_ridereviewID] PRIMARY KEY([RideReviewID] ASC),
	CONSTRAINT [fk_ridereview_ticketID] FOREIGN KEY([TicketID])
			REFERENCES [dbo].[Ticket]([TicketID]),
	CONSTRAINT [fk_ridereview_clientID] FOREIGN KEY([ClientID])
			REFERENCES [dbo].[Client]([ClientID]),
	CONSTRAINT [fk_ridereview_driverID] FOREIGN KEY([DriverID])
			REFERENCES [dbo].[Employee]([EmployeeID])
	)
GO

/**************************************
Nate Hepker
Created: 2021/03/21
Description: Creats a store procedure for getting all the ride reviews
***************************************/
print''print'*** Creating sp_select_all_ride_reviews ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_ride_reviews]
AS
	BEGIN
		SELECT RideReviewID, RideReview.TicketID, DriverID, Employee.FirstName, Employee.LastName,
			DriverClientRating, DriverComment, Client.ClientID, Client.FirstName, 
			Client.LastName, ClientComment
		FROM RideReview
		JOIN Client
			ON RideReview.ClientID = Client.ClientID
		JOIN Employee
			ON RideReview.DriverID = Employee.EmployeeID
		JOIN Ticket
			ON RideReview.TicketID = Ticket.TicketID
		WHERE Ticket.TicketTypeID = 3
		ORDER BY RideReviewID ASC
	END
GO

/**************************************
Nate Hepker
Created: 2021/03/21
Updated: 2021/04/24
Description: Creats a store procedure for getting all the ride reviews for drivers
***************************************/
print''print'*** Creating sp_select_all_driver_ride_reviews ***'
GO

CREATE PROCEDURE [dbo].[sp_select_all_driver_ride_reviews]
AS
	BEGIN
		SELECT RideReviewID, RideReview.TicketID, DriverID, Employee.FirstName, Employee.LastName,
			DriverClientRating, DriverComment, Client.ClientID, Client.FirstName, 
			Client.LastName--, ClientComment
		FROM RideReview
		JOIN Client
			ON RideReview.ClientID = Client.ClientID
		JOIN Employee
			ON RideReview.DriverID = Employee.EmployeeID
		JOIN Ticket
			ON RideReview.TicketID = Ticket.TicketID
		WHERE Ticket.TicketTypeID = 3
		AND DriverComment IS NOT NULL
		ORDER BY RideReviewID ASC
	END
GO

/**************************************
Nate Hepker
Created: 2021/03/21
Description: Creats a store procedure for inserting a new RideReview
into the database
a ride to review.
***************************************/
print '' print '**** Creating sp_insert_ride_review ****'
GO
CREATE PROCEDURE [dbo].[sp_insert_ride_review]
(
	@TicketID			[int],
	@ClientID			[int],
	@DriverID	        [int],
	@DriverClientRating  [int],
	@DriverComment	    [nvarchar](500)
)
AS
	BEGIN
		INSERT INTO [dbo].[RideReview]
			([TicketID], [ClientID], [DriverID], [DriverClientRating], [DriverComment])    
		VALUES
			(@TicketID, @ClientID, @DriverID, @DriverClientRating, @DriverComment)    
	END
GO

/**************************************
Nate Hepker
Created: 2021/04/16
Description: Creats a store procedure for inserting a new RideReview
into the database
a ride to review.
***************************************/
print '' print '**** Creating sp_insert_driver_ride_review ****'
GO
CREATE PROCEDURE [dbo].[sp_insert_driver_ride_review]
(
	@TicketID			[int],
	@ClientID			[int],
	@DriverID	        [int],
	@DriverClientRating  [int],
	@DriverComment	    [nvarchar](500)
)
AS
	BEGIN
		INSERT INTO [dbo].[RideReview]
			([TicketID], [ClientID], [DriverID], [DriverClientRating], [DriverComment])    
		VALUES
			(@TicketID, @ClientID, @DriverID, @DriverClientRating, @DriverComment)    
	END
GO

/**************************************
Nate Hepker
Created: 2021/04/10
Description: Creats a store procedure for inserting a new RideReview
from client into the database
a ride to review.
***************************************/
print '' print '**** Creating sp_insert_client_ride_review ****'
GO
CREATE PROCEDURE [dbo].[sp_insert_client_ride_review]
(
	@TicketID			[int],
	@ClientID			[int],
	@DriverID	        [int],
	@ClientDriverRating [int],
	@ClientComment	    [nvarchar](500)
)
AS
	BEGIN
		INSERT INTO [dbo].[RideReview]
			([TicketID], [ClientID], [DriverID], [ClientDriverRating], [ClientComment])    
		VALUES
			(@TicketID, @ClientID, @DriverID, @ClientDriverRating, @ClientComment)    
	END
GO

/**************************************
Nate Hepker
Created: 2021/03/21
Description: Creates a store procedure for getting 
the ride tickets by the employeeID to use for selecting
a ride to review.
***************************************/
print''print'*** Creating sp_select_tickets_by_employee_id ***'
GO
--drop procedure sp_select_tickets_by_employee_id;
CREATE PROCEDURE [dbo].[sp_select_tickets_by_employee_id]
	(
		@EmployeeID		[int]
	)
AS
	BEGIN
		SELECT Ticket.TicketID, Ticket.TicketTypeID, Route.DriverEmployeeID, Employee.FirstName,
			Employee.LastName, RideTicket.ClientID, Client.FirstName, Client.LastName
		FROM Ticket
		JOIN RideTicket
			ON Ticket.TicketID = RideTicket.TicketID
		JOIN Route
			ON Ticket.RouteID = Route.RouteID
		JOIN Client
			ON RideTicket.ClientID = Client.ClientID
		JOIN Employee
			ON Route.DriverEmployeeID = Employee.EmployeeID
		FULL OUTER JOIN RideReview
			ON RideTicket.TicketID = RideReview.TicketID
		WHERE Route.DriverEmployeeID = @EmployeeID
		ORDER BY Ticket.TicketID ASC
	END
GO
/*
exec [sp_select_tickets_by_employee_id] @EmployeeID = 100000;

select* from Ticket;
select* from RideTicket;
select* from Route;
select* from Client;
select* from Employee;

select* from Ticket;
select* from Ticket;
select* from Ticket;
select* from Ticket;
*/


/**************************************
Nate Hepker
Created: 2021/04/09
Description: Creates a store procedure for getting all
the ride tickets to use for selecting a ride to review.
***************************************/
print''print'*** Creating sp_select_all_ride_tickets_to_review ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_ride_tickets_to_review]
AS
	BEGIN
		SELECT Ticket.TicketID, Ticket.TicketTypeID, Route.DriverEmployeeID, Employee.FirstName,
			Employee.LastName, RideTicket.ClientID, Client.FirstName, Client.LastName
		FROM Ticket
		JOIN RideTicket
			ON Ticket.TicketID = RideTicket.TicketID
		JOIN Route
			ON Ticket.RouteID = Route.RouteID
		JOIN Client
			ON RideTicket.ClientID = Client.ClientID
		JOIN Employee
			ON Route.DriverEmployeeID = Employee.EmployeeID
		FULL OUTER JOIN RideReview
			ON RideTicket.TicketID = RideReview.TicketID
		ORDER BY Ticket.TicketID ASC
	END
GO

/**************************************
Nate Hepker
Created: 2021/04/17
Description: Creates a store procedure for getting 
the ride tickets by the clientID to use for selecting
a ride to review.
***************************************/
print''print'*** Creating sp_select_tickets_by_client_id ***'
GO
CREATE PROCEDURE [dbo].[sp_select_tickets_by_client_id]
	(
		@ClientID		[int]
	)
AS
	BEGIN
		SELECT Ticket.TicketID, Ticket.TicketTypeID, Route.DriverEmployeeID, Employee.FirstName,
			Employee.LastName, RideTicket.ClientID, Client.FirstName, Client.LastName
		FROM Ticket
		JOIN RideTicket
			ON Ticket.TicketID = RideTicket.TicketID
		JOIN Route
			ON Ticket.RouteID = Route.RouteID
		JOIN Client
			ON RideTicket.ClientID = Client.ClientID
		JOIN Employee
			ON Route.DriverEmployeeID = Employee.EmployeeID
		FULL OUTER JOIN RideReview
			ON RideTicket.TicketID = RideReview.TicketID
		WHERE RideTicket.ClientID = @ClientID
		ORDER BY Ticket.TicketID ASC
	END
GO

/**************************************
Nate Hepker
Created: 2021/3/30
Description: Creates a store procedure for updating 
a ride review
***************************************/
print''print'*** Creating sp_update_ride_review_from_driver ***'
GO
CREATE PROCEDURE [dbo].[sp_update_ride_review_from_driver]
	(
		@RideReviewID				[int],
		@NewDriverClientRating		[int],
		@NewDriverComment			[nvarchar](500),
		@OldDriverClientRating		[int],
		@OldDriverComment			[nvarchar](500)
	)
AS
	BEGIN
		UPDATE RideReview
			SET DriverClientRating = @NewDriverClientRating,
				DriverComment = @NewDriverComment
			WHERE RideReviewID = @RideReviewID
			AND DriverClientRating = @OldDriverClientRating
			AND DriverComment = @OldDriverComment
		RETURN @@ROWCOUNT
	END
GO

/**************************************
Nate Hepker
Created: 2021/4/20
Description: Creates a store procedure for updating 
a ride review
***************************************/
print''print'*** Creating sp_update_ride_review_from_client ***'
GO
CREATE PROCEDURE [dbo].[sp_update_ride_review_from_client]
	(
		@RideReviewID				[int],
		@NewClientDriverRating		[int],
		@NewClientComment			[nvarchar](500),
		@OldClientDriverRating		[int],
		@OldClientComment			[nvarchar](500)
	)
AS
	BEGIN
		UPDATE RideReview
			SET ClientDriverRating = @NewClientDriverRating,
				ClientComment = @NewClientComment
			WHERE RideReviewID = @RideReviewID
			AND ClientDriverRating = @OldClientDriverRating
			AND ClientComment = @OldClientComment
		RETURN @@ROWCOUNT
	END
GO

/**************************************
Nate Hepker
Created: 2021/04/03
Description: Creates a store procedure for deleting 
a ride review safely
***************************************/
print''print'*** Creating sp_delete_ride_review_from_driver ***'
GO
CREATE PROCEDURE [dbo].[sp_delete_ride_review_from_driver]
	(
		@RideReviewID	[int]
	)
AS
	BEGIN TRY
		BEGIN TRANSACTION
		DELETE FROM RideReview
			WHERE RideReviewID = @RideReviewID;
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK WORK;
	END CATCH
GO

/**************************************
Nate Hepker
Created: 2021/04/16
Description: Creates a store procedure for getting all the ride reviews for client
***************************************/
print''print'*** Creating sp_select_all_client_ride_reviews ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_client_ride_reviews]
AS
	BEGIN
		SELECT RideReviewID, RideReview.TicketID, DriverID, Employee.FirstName, Employee.LastName, 
			Client.ClientID, Client.FirstName, Client.LastName, ClientDriverRating, ClientComment,DriverComment
		FROM RideReview
		JOIN Client
			ON RideReview.ClientID = Client.ClientID
		JOIN Employee
			ON RideReview.DriverID = Employee.EmployeeID
		JOIN Ticket
			ON RideReview.TicketID = Ticket.TicketID
		WHERE Ticket.TicketTypeID = 3
			AND ClientDriverRating > 0
		ORDER BY RideReviewID ASC
	END
GO

/**************************************
Nate Hepker
Created: 2021/04/17
Description: Creates a store procedure for getting all the ride reviews
for that client by id
***************************************/
print''print'*** Creating sp_select_all_client_ride_reviews_by_id ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_client_ride_reviews_by_id]
	(
		@ClientID	[int]
	)
AS
	BEGIN
		SELECT RideReviewID, RideReview.TicketID, DriverID, Employee.FirstName, Employee.LastName, 
			Client.ClientID, Client.FirstName, Client.LastName, ClientDriverRating, ClientComment
		FROM RideReview
		JOIN Client
			ON RideReview.ClientID = Client.ClientID
		JOIN Employee
			ON RideReview.DriverID = Employee.EmployeeID
		JOIN Ticket
			ON RideReview.TicketID = Ticket.TicketID
		WHERE Ticket.TicketTypeID = 3
			AND Client.ClientID = @ClientID
			AND ClientDriverRating > 0
		ORDER BY RideReviewID ASC
	END
GO

/**************************************
Nate Hepker
Created: 2021/04/22
Description: Creates a store procedure for getting a ride review
by the rrid
***************************************/
print''print'*** Creating sp_select_ride_reviews_by_review_id ***'
GO
CREATE PROCEDURE [dbo].[sp_select_ride_reviews_by_review_id]
	(
		@RideReviewID	[int]
	)
AS
	BEGIN
		SELECT RideReviewID, RideReview.TicketID, DriverID, Employee.FirstName, Employee.LastName, 
			Client.ClientID, Client.FirstName, Client.LastName, ClientDriverRating, ClientComment
		FROM RideReview
		JOIN Client
			ON RideReview.ClientID = Client.ClientID
		JOIN Employee
			ON RideReview.DriverID = Employee.EmployeeID
		JOIN Ticket
			ON RideReview.TicketID = Ticket.TicketID
		WHERE Ticket.TicketTypeID = 3
			AND RideReview.RideReviewID = @RideReviewID
			AND ClientDriverRating > 0
		ORDER BY RideReviewID ASC
	END
GO

