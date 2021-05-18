using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModels;

namespace LogicInterfaces
{
    /// <summary>
    /// Chantal Shirley
    /// Created: 2021/04/11
    /// 
    /// Manages access to order data.
    /// </summary>
    public interface IOrderManager
    {
        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/11
        /// 
        /// Manages access to retrieving 
        /// donation data by order id.
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        Order RetrieveOrderByOrderID(int ticketOrderId);
        bool AuthorizeTicketRetrievalByClientID(int orderID, int clientId);

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/11
        /// 
        /// Manages access to retrieving 
        /// donation data by order id.
        /// </summary>
        /// <param name="clientID"></param>
        /// <param name="donationID"></param>
        /// <returns></returns>
        bool AddOrder(int clientID, int donationID, string dateRequested);

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/11
        /// 
        /// Manages access to retrieving 
        /// donation data by order id.
        /// </summary>
        /// <param name="clientID"></param>
        /// <returns></returns>
        List<Order> GetOrdersByClientID(int clientID);
    }
}
