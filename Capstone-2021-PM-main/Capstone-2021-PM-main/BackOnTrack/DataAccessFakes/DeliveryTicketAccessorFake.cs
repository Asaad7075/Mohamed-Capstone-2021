/// Jakub Kawski
/// Created: 2021/02/13
/// 
///
/// </summary>
///
/// <remarks>
/// Updater Name
/// Updated: yyyy/mm/dd
/// </remarks>
using BingMapsRESTToolkit;
using DataAccessInterfaces;
using DomainModels;
using DomainModels.Tickets;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessFakes
{
    /// <summary>
    /// Jakub Kawski
    /// 2021/02/19
    /// 
    /// Delivery Ticket Accessor Fake Class, creates fake data to use
    /// during testing of managers that use a deliveryticketaccessor.
    /// </summary>
    /// <remarks>
    /// Chantal Shirley
    /// Updated: 2021/04/09
    /// 
    /// Making changes for web presentation testing.
    /// </remarks>
    public class DeliveryTicketAccessorFake : IDeliveryTicketAccessor
    {
        private Dictionary<int, int> _orderNumAndClientId = new Dictionary<int, int>();
        private List<DeliveryTicketVM> _tickets = new List<DeliveryTicketVM>();
        /// <summary>
        /// Jakub Kawski
        /// 2021/02/19
        /// 
        /// </summary>
        /// <remarks>
        /// Chantal Shirley
        /// Updated: 2021/04/09
        /// 
        /// Made changes to last orderID to
        /// coincide with structure of implemented database.
        /// </remarks>
        public DeliveryTicketAccessorFake()
        {
            _tickets.Add(new DeliveryTicketVM()
            {
                TicketID = 10000,
                TicketType = 1,
                City = "Iowa City",
                State = "IA",
                GeoID = 10000,
                StreetAddressLineOne = "123 Raindbow Ave",
                ZipCode = "55522",
                Coordinate = new Coordinate(24.000, 125.0000),
                CreatedAt = DateTime.Now,
                EstimatedArrival = DateTime.Now,
                OrderID = 10001,
                Notes = "N/A",
                RouteID = -1,
                StatusDescription = "Waiting for assignment",
                StatusID = 1,
                StopNumber = -1,
                Items = new Dictionary<string, int>()
            });
            _tickets.Add(new DeliveryTicketVM()
            {
                TicketID = 10000,
                TicketType = 1,
                City = "Iowa City",
                State = "IA",
                GeoID = 10001,
                StreetAddressLineOne = "10 Main Ave",
                ZipCode = "55522",
                Coordinate = new Coordinate(24.000, 125.0000),
                CreatedAt = DateTime.Now,
                EstimatedArrival = DateTime.Now,
                OrderID = 10001,
                Notes = "N/A",
                RouteID = -1,
                StatusDescription = "Waiting for assignment",
                StatusID = 1,
                StopNumber = -1,
                Items = new Dictionary<string, int>()
            });
            _tickets.Add(new DeliveryTicketVM()
            {
                TicketID = 10000,
                TicketType = 1,
                City = "Iowa City",
                State = "IA",
                GeoID = 10002,
                StreetAddressLineOne = "23 Dog St",
                ZipCode = "55521",
                Coordinate = new Coordinate(24.000, 125.0000),
                CreatedAt = DateTime.Now,
                EstimatedArrival = DateTime.Now,
                OrderID = 10001,
                Notes = "N/A",
                RouteID = -1,
                StatusDescription = "Waiting for assignment",
                StatusID = 1,
                StopNumber = -1,
                Items = new Dictionary<string, int>()
            });
            _tickets.Add(new DeliveryTicketVM()
            {
                TicketID = 10000,
                TicketType = 1,
                City = "Iowa City",
                State = "IA",
                GeoID = 10003,
                StreetAddressLineOne = "45 Cat Rd",
                ZipCode = "55521",
                Coordinate = new Coordinate(24.000, 125.0000),
                CreatedAt = DateTime.Now,
                EstimatedArrival = DateTime.Now,
                OrderID = 10001,
                Notes = "N/A",
                RouteID = -1,
                StatusDescription = "Waiting for assignment",
                StatusID = 1,
                StopNumber = -1,
                Items = new Dictionary<string, int>()
            });
            _tickets.Add(new DeliveryTicketVM()
            {
                TicketID = 10000,
                TicketType = 1,
                City = "Iowa City",
                State = "IA",
                GeoID = 10004,
                StreetAddressLineOne = "20 Backwater Rd",
                ZipCode = "55522",
                Coordinate = new Coordinate(24.000, 125.0000),
                CreatedAt = DateTime.Now,
                EstimatedArrival = DateTime.Now,
                OrderID = 10004,
                Notes = "N/A",
                RouteID = -1,
                StatusDescription = "Waiting for assignment",
                StatusID = 1,
                StopNumber = -1,
                Items = new Dictionary<string, int>()
            });

            // Simulating view table check
            _orderNumAndClientId.Add(10004,10004);

        }
        /// <summary>
        /// Jakub Kawski
        /// 2021/02/19
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        public int DeleteDeliveryTicket(DeliveryTicketVM ticket)
        {
            _tickets.Remove(ticket);
            return _tickets.Count;
        }
        /// <summary>
        /// Jakub Kawski
        /// 2021/02/19
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        public int InsertDeliveryTicket(DeliveryTicketVM ticket)
        {
            int result = 0;
            if (ticket.TicketType == (int)TicketType.Delivery)
            {
                ticket.TicketID = _tickets.Count + 10000;
                _tickets.Add(ticket);
            }
            result = _tickets.Last().TicketID;
            return result;
        }
        /// <summary>
        /// Jakub Kawski 
        /// 2021/02/19
        /// 
        /// </summary>
        /// <returns></returns>
        public List<DeliveryTicketVM> SelectAllDeliveryTickets()
        {
            return _tickets;
        }

        /// <summary>
        /// Chantal Shirley
        /// 2021/04/12
        /// 
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public DeliveryTicketVM SelectDeliveryTicketByOrderID(int orderID)
        {
            foreach (var ticket in _tickets)
            {
                if (ticket.OrderID == orderID)
                {
                    return ticket;
                }
            }

            return null; // Meaning nothing was found
        }

        /// <summary>
        /// Chantal Shirley
        /// 2021/04/08
        /// 
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public List<DeliveryTicketVM> SelectDeliveryTicketsByClientId(int clientId)
        {
            List<int> orderIds = new List<int>();
            List<DeliveryTicketVM> ticketsResult = new List<DeliveryTicketVM>();

            foreach (KeyValuePair<int,int> orderId in _orderNumAndClientId)
            {
                if (orderId.Value == clientId)
                {
                    orderIds.Add(orderId.Key);
                }
            }

            foreach (DeliveryTicketVM ticket in _tickets)
            {
                if (orderIds.Contains(ticket.OrderID))
                {
                    ticketsResult.Add(ticket);
                }
            }

            return ticketsResult;
        }

        public List<DeliveryTicketVM> SelectDeliveryTicketsByRouteID(int routeID)
        {
            var tickets = _tickets.FindAll(t => t.RouteID == routeID);
            return tickets;
        }

        /// <summary>
        /// Jakub Kawski
        /// 2021/02/19
        /// 
        /// </summary>
        /// <param name="newTicket"></param>
        /// <param name="oldTicket"></param>
        /// <returns></returns>
        public int UpdateDeliveryTicket(DeliveryTicketVM newTicket, DeliveryTicketVM oldTicket)
        {
            int result = 0;
            if(newTicket.TicketID == oldTicket.TicketID)
            {
                int ticketIndex = _tickets.FindIndex(t => t.TicketID == oldTicket.TicketID);
                _tickets.RemoveAt(ticketIndex);
                int countAfterRemove = _tickets.Count;
                _tickets.Add(newTicket);
                int countAfterAdd = _tickets.Count;
                result = countAfterAdd - countAfterRemove;
            }
            

            return result;
        }
    }
}
