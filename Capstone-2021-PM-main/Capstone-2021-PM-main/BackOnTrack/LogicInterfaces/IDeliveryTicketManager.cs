using DomainModels.Tickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicInterfaces
{
    public interface IDeliveryTicketManager
    {
        bool AddTicket(DeliveryTicketVM ticket);
        List<DeliveryTicketVM> RetrieveAllTickets();
        bool UpdateTicket(DeliveryTicketVM newTicket, DeliveryTicketVM oldTicket);
        bool DeleteTicket(DeliveryTicketVM ticket);
        List<DeliveryTicketVM> RetrieveTicketsByClientId(int clientId);
        DeliveryTicketVM RetrieveTicketByOrderID(int orderID);
    }
}
