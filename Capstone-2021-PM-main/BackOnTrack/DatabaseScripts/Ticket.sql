print''print'***using backontrack_db ***'
GO
USE backontrack_db
GO
/*
Author : Jakub Kawski
Created : 2021/02/19

The routepoint table 
*/
print''print'***create ticket table***'
GO
CREATE TABLE [dbo].[Ticket](
	[TicketID]		[int]	IDENTITY(10000, 1)	NOT NULL,
	[TicketTypeID]	[int]						NOT NULL,
	[RouteID]		[int]						NULL,
	[StopNumber]	[int]						NULL DEFAULT -1,
	[CreatedAt]		[datetime]					NOT NULL	DEFAULT GETDATE(),
	[EstimatedArrival] [datetime]				NULL DEFAULT GETDATE(),
	[StatusID]		[int]						NOT NULL	DEFAULT 1,
	[Notes]			[nvarchar](500)				NULL DEFAULT 'N/A',
	CONSTRAINT [pk_TicketID] PRIMARY KEY([TicketID] ASC),
	CONSTRAINT [fk_ticket_tickettypeID] FOREIGN KEY([TicketTypeID]) REFERENCES [dbo].[TicketType]([TicketTypeID]),
	CONSTRAINT [fk_routepoint_routeID] FOREIGN KEY([RouteID]) REFERENCES [dbo].[Route]([RouteID])
)
GO
/*
Author : Jakub Kawski
Created : 2021/02/19

This sp is only called inside the dp when creating tickets
*/
print''print'****creating sp_insert_new_ticket'
GO
CREATE PROCEDURE [dbo].[sp_insert_new_ticket]
(
	@TicketTypeID [int]
)
AS
	BEGIN
		INSERT INTO Ticket
			(TicketTypeID)
		VALUES
			(@TicketTypeID)
		RETURN SCOPE_IDENTITY()
	END
GO
/*
Author : Jakub Kawski
Created : 2021/02/19

This sp is only called inside the dp when creating tickets
*/
print''print'****creating sp_update_ticket'
GO
CREATE PROCEDURE [dbo].[sp_update_ticket]
(
	@TicketID [int],
	@OldRouteID [int],
	@OldStopNumber [int],
	@OldEstimatedArrival [datetime],
	@OldStatusID [int],
	@OldNotes [nvarchar](500),
	@NewRouteID [int],
	@NewStopNumber [int],
	@NewEstimatedArrival [datetime],
	@NewStatusID [int],
	@NewNotes [nvarchar](500)
)
AS
	BEGIN
		UPDATE Ticket
		SET RouteID = @NewRouteID,
			StopNumber = @NewStopNumber,
			EstimatedArrival = @NewEstimatedArrival,
			StatusID = @NewStatusID,
			Notes = @NewNotes
		WHERE TicketID = @TicketID
			AND RouteID = @OldRouteID
			AND StopNumber = @OldStopNumber
			AND StatusID = @OldStatusID
			AND Notes = @OldNotes
	END
GO
