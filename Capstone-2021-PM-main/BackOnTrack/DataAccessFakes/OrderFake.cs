using DataAccessInterfaces;
using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessFakes
{
    /// <summary>
    /// Chantal Shirley
    /// Created: 2021/04/21
    /// 
    /// Fake for testing order
    /// accessors and data flow.
    /// </summary>
    public class OrderFake : IOrderAccessor
    {
        private Order _order = null;

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/21
        /// 
        /// Empty constructor that initializes fake
        /// order data.
        /// </summary>
        public OrderFake()
        {
            _order = new Order()
            {
                OrderID = 10004,
                Items = new Dictionary<int, Donation>()
            };

            _order.Items.Add(
                1000003,
                new Donation()
                {
                    DonationID = 1000003,
                    AgeofItem = 0,
                    Description = "King-sized bed with brand new mattress.",
                    DonatedImage = new byte[10],
                    DonationStatus = "Pending",
                    DonorID = 100003,
                    DropOff = true,
                    EmailReceipt = true,
                    EstValue = 1200,
                    MailReceipt = true,
                    NameOfItem = "Bed Set",
                    OrderQty = 2,
                    PickUp = true,
                    PickUpDateTime = new DateTime(2021, 06, 02)
                }
            );
        }

        public int InsertOrder(int clientID, int donationID, string dateRequested)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/25
        /// 
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="clientID"></param>
        /// <returns></returns>
        public bool SelectOrderByClientIDandOrderID(int orderID, int clientID)
        {
            return false; // Will implement
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/21
        /// 
        /// Returns an order based on the
        /// specified order id.
        /// </summary>
        /// <param name="ticketOrderId"></param>
        /// <returns></returns>
        public Order SelectOrderByOrderID(int ticketOrderId)
        {
            if (_order.OrderID == ticketOrderId)
            {
                return _order;
            }
            else
            {
                return null;
            }
        }

        public List<Order> SelectOrdersByClientID(int clientID)
        {
            throw new NotImplementedException();
        }
    }
}
