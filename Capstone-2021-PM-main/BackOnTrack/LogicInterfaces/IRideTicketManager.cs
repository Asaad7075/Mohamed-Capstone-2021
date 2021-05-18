using DomainModels.Services;
using DomainModels.Tickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicInterfaces
{
    public interface IRideTicketManager
    {
        bool AddTicket(RideTicketVM ticket);
        List<RideTicketVM> RetrieveAllTickets();
        bool UpdateTicket(RideTicketVM newTicket, RideTicketVM oldTicket);
        bool DeleteTicket(RideTicketVM ticket);
        RideTicketVM DataCollectionForRTVM(ServiceVM service, int clientID, string email);
    }
}
