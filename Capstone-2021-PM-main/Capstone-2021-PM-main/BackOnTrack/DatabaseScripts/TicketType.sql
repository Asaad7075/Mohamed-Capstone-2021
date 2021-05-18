print''print'***using backontrack_db ***'
GO
USE backontrack_db
GO
/*
Author : Jakub Kawski
Created : 2021/02/19
*/
print''print'***create tickettype table***'
GO
CREATE TABLE [dbo].[TicketType](
	[TicketTypeID]		[int]	NOT NULL,
	[TicketTypeName]	[nvarchar](50) NOT NULL,
	CONSTRAINT [pk_TicketTypeID] PRIMARY KEY ([TicketTypeID] ASC)
)
GO
/*
Author : Jakub Kawski
Created : 2021/02/19
*/
print''print'*** inserting ticket types***'
GO
INSERT INTO TicketType
	(TicketTypeID, TicketTypeName)
VALUES
	(1, "Delivery Ticket"),
	(2, "PickUp Ticket"),
	(3, "Ride Ticket")
GO