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
    /// 2021/02/19
    /// 
    /// Interface for Delivery Ticket Accessor
    /// </summary>
    /// <remarks>
    /// Chantal Shirley
    /// Updated: 2021/04/09
    /// SelectDeliveryTicketsByClientId
    /// Updated: 2021/04/12
    /// SelectDeliveryTicketByOrderID
    /// </remarks>
    public interface IDeliveryTicketAccessor
    {
        int InsertDeliveryTicket(DeliveryTicketVM ticket);
        int UpdateDeliveryTicket(DeliveryTicketVM newTicket, DeliveryTicketVM oldTicket);
        List<DeliveryTicketVM> SelectAllDeliveryTickets();
        int DeleteDeliveryTicket(DeliveryTicketVM ticket);
        List<DeliveryTicketVM> SelectDeliveryTicketsByClientId(int clientId);
        DeliveryTicketVM SelectDeliveryTicketByOrderID(int orderID);
        List<DeliveryTicketVM> SelectDeliveryTicketsByRouteID(int routeID);
    }
}
