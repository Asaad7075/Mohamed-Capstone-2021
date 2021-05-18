using DomainModels.Tickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicInterfaces
{
    public interface IPickUpTicketManager
    {
        List<PickUpTicketVM> RetrieveAllTickets();
        bool AddTicket(PickUpTicketVM ticket);
        bool UpdateTicket(PickUpTicketVM newTicket, PickUpTicketVM oldTicket);
        bool DeleteTicket(PickUpTicketVM ticket);
        List<PickUpTicketVM> RetrievePickUpTicketsByDate(DateTime dateTime);
    }
}
