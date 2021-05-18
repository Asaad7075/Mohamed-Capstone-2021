using DataAccessInterfaces;
using DataAccessLayer;
using LogicInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModels;

namespace LogicLayer
{
    /// <summary>
    /// Chantal Shirley
    /// Created: 2021/04/11
    /// 
    /// Manages access to order data.
    /// </summary>
    public class OrderManager : IOrderManager
    {
        private IOrderAccessor _orderAccessor = new OrderAccessor();

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/11
        /// 
        /// Empty constructor for intializing the OrderManager.
        /// </summary>
        public OrderManager()
        {
            _orderAccessor = new OrderAccessor();
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/11
        /// 
        /// Constructor for dependency injection.
        /// </summary>
        /// <param name="orderAccessor"></param>
        public OrderManager(IOrderAccessor orderAccessor)
        {
            _orderAccessor = orderAccessor;
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/11
        /// 
        /// Adds order to Order table
        /// </summary>
        /// <param name="donationID"></param>
        /// <param name="clientID"></param>
        public bool AddOrder(int clientID, int donationID, string dateRequested)
        {
            bool result = false;
            try
            {
                result = (1 == _orderAccessor.InsertOrder(clientID, donationID, dateRequested));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Order insersion failed" + ex.Message);
            }
            return result;
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/25
        /// 
        /// Retrieves information that 
        /// signifies whether a delivery 
        /// ticket belongs to the associated clientId.
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public bool AuthorizeTicketRetrievalByClientID(int orderID, int clientID)
        {
            bool result = false;

            try
            {
                result = _orderAccessor.SelectOrderByClientIDandOrderID(orderID, clientID);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public List<Order> GetOrdersByClientID(int clientID)
        {
            List<Order> clientOrders = new List<Order>();
            try
            {
                clientOrders = _orderAccessor.SelectOrdersByClientID(clientID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Order insertion failed" + ex.Message);
            }
            return clientOrders;
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/20
        /// 
        /// Manages access for retrieving
        /// order data by order id.
        /// </summary>
        /// <param name="ticketOrderId"></param>
        /// <returns></returns>
        public Order RetrieveOrderByOrderID(int ticketOrderId)
        {
            Order order = null;

            try
            {
                order = _orderAccessor.SelectOrderByOrderID(ticketOrderId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Order information could not be retrieved." +
                    "\n\n" + ex.Message);
            }

            return order;
        }
    }
}
