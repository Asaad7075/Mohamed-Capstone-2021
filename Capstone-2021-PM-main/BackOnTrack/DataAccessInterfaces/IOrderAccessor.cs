using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    /// <summary>
    /// Chantal Shirley
    /// Created: 2021/04/11
    /// 
    /// Accessor fo orders in the order table.
    /// </summary>
    public interface IOrderAccessor
    {
        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/11
        /// 
        /// Retrieves donation 
        /// information by order ids.
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        /// <remarks>
        /// Chantal Shirley
        /// Updated: 2021/04/20
        /// 
        /// Updated to reflect order data
        /// more accurately.
        /// </remarks>
        Order SelectOrderByOrderID(int ticketOrderId);
        bool SelectOrderByClientIDandOrderID(int orderID, int clientID);

        /// <summary>
        /// Richard Schroeder
        /// Created: 2021/04/11
        /// 
        /// Inserts order into order table
        /// </summary>
        /// <param name="clientID"></param>
        /// <param name="donationID"></param>
        /// <returns></returns>
        int InsertOrder(int clientID, int donationID, string dateRequested);

        /// <summary>
        /// Richard Schroeder
        /// Created: 2021/04/11
        /// 
        /// Selects orders by clientID
        /// </summary>
        /// <param name="clientID"></param>
        /// <returns></returns>
        List<Order> SelectOrdersByClientID(int clientID);
    }
}
