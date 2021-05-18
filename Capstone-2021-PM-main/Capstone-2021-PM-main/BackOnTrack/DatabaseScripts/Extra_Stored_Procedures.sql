print''print'***using backontrack_db ***'
GO
USE backontrack_db
GO
/*
Jakub Kawski
2021/04/28

Just a space to put stored procedures that dont fit in one table or needs 
a lot of db context to execute
*/

print''print'***creating sp_get_ticket_meta_data ***'
GO
CREATE PROCEDURE [dbo].[sp_get_ticket_meta_data]
AS
	BEGIN
SELECT 
		Count(Ticket.TicketID) as 'Total Unassigned',
		Count(DeliveryTicket.TicketID) as 'Total Deliveries Unassigned',
		Count(PickupTicket.TicketID) as 'Total Pickups Unassigned',
		Count(RideTicket.TicketID) as 'Total Rides Unassigned'
FROM Ticket
FULL JOIN DeliveryTicket 
	ON Ticket.TicketID = DeliveryTicket.TicketID
FULL JOIN PickupTicket
	ON Ticket.TicketID = PickupTicket.TicketID
FULL JOIN RideTicket
	ON Ticket.TicketID = RideTicket.TicketID
WHERE
	Ticket.StatusID = 1

	END
GO