using DomainModels.Tickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    /// <summary>
    /// Jakub Kawski
    /// 2021/03/26
    /// 
    /// </summary>
    public interface IRideTicketAccessor
    { 
        int InsertRideTicket(RideTicketVM ticket);
        int UpdateRideTicket(RideTicketVM newTicket, RideTicketVM oldTicket);
        List<RideTicketVM> SelectAllRideTickets();
        int DeleteRideTicket(RideTicketVM ticket);

        List<RideTicketVM> SelectRideTicketsByRouteID(int routeID);
        List<RideTicketVM> SelectRideTicketsByDate(DateTime date);
    }
}
