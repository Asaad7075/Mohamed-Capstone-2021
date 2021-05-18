print''print'***using backontrack_db ***'
GO
USE backontrack_db
GO
/*
Author : Jakub Kawski
Created : 2021/02/19

The ticket status table 
*/
print''print'***create ticket status table***'
GO
CREATE TABLE [dbo].[TicketStatus](
	[StatusID]			[int]IDENTITY(0, 1)			NOT NULL,
	[StatusDescription]	[nvarchar](250)				NOT NULL,
	CONSTRAINT [pk_StatusID] PRIMARY KEY([StatusID] ASC)
)
GO
/*
Author : Jakub Kawski
Created : 2021/03/01

*/
print''print'**** create sp_select_all_ticket_statuses ****'
GO
CREATE PROCEDURE [dbo].[sp_select_all_ticket_statuses]
AS
	BEGIN
		SELECT StatusID, StatusDescription
		FROM TicketStatus
	END
GO

/* dumby data */
print''print'****inserting status dumby data***'
GO
INSERT INTO TicketStatus
	(StatusDescription)
VALUES
	("Completed"),
	("Awaiting Assignment"),
	("Assigned"),
	("On-the-way"),
	("In Progress"),
	("Delivered"),
	("Picked Up")
GO