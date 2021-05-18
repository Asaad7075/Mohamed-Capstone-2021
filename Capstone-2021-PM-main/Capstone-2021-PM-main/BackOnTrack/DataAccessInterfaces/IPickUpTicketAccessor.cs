using DomainModels.Tickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    public interface IPickUpTicketAccessor
    {
        List<PickUpTicketVM> SelectAllPickUpTickets();
        int InsertPickUpTicket(PickUpTicketVM ticket);
        int UpdatePickUpTicket(PickUpTicketVM newTicket, PickUpTicketVM oldTicket);
        int DeletePickUpTicket(PickUpTicketVM ticket);
        List<PickUpTicketVM> SelectPickUpTicketsByRouteID(int routeID);
        List<PickUpTicketVM> SelectPickUpTicketsByDate(DateTime date);
    }
}
