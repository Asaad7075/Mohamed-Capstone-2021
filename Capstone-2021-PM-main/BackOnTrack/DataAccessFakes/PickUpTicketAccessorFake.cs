using BingMapsRESTToolkit;
using DataAccessInterfaces;
using DomainModels.Tickets;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessFakes
{
    public class PickUpTicketAccessorFake : IPickUpTicketAccessor
    {
        private List<PickUpTicketVM> _tickets;
        public PickUpTicketAccessorFake()
        {
            _tickets = new List<PickUpTicketVM>()
            {
                new PickUpTicketVM()
                {
                    TicketID = 10005,
                    TicketType = 2,
                    DonationID = 1,
                    City = "Iowa City",
                    State = "IA",
                    GeoID = 10000,
                    StreetAddressLineOne = "123 Lucas St",
                    StreetAddressLineTwo = "",
                    ZipCode = "55522",
                    Coordinate = new Coordinate(50.000, 45.0000),
                    RequestDateStart = new DateTime(),
                    RequestDateEnd = new DateTime(),
                    TimeRangeStart = new TimeSpan(),
                    TimeRangeEnd = new TimeSpan(),
                    CreatedAt = DateTime.Now,
                    EstimatedArrival = DateTime.Now,
                    Notes = "N/A",
                    RouteID = -1,
                    StatusDescription = "Waiting for assignment",
                    StatusID = 1,
                    StopNumber = -1,
                    Items = new Dictionary<string, int>()                
                },
                new PickUpTicketVM()
                {
                    TicketID = 10006,
                    TicketType = 2,
                    DonationID = 1,
                    City = "Iowa City",
                    State = "IA",
                    GeoID = 10000,
                    StreetAddressLineOne = "123 Rain Dr",
                    StreetAddressLineTwo = "",
                    ZipCode = "55522",
                    Coordinate = new Coordinate(50.000, 45.0000),
                    RequestDateStart = new DateTime(),
                    RequestDateEnd = new DateTime(),
                    TimeRangeStart = new TimeSpan(),
                    TimeRangeEnd = new TimeSpan(),
                    CreatedAt = DateTime.Now,
                    EstimatedArrival = DateTime.Now,
                    Notes = "N/A",
                    RouteID = -1,
                    StatusDescription = "Waiting for assignment",
                    StatusID = 1,
                    StopNumber = -1,
                    Items = new Dictionary<string, int>(),
                },
                new PickUpTicketVM()
                {
                    TicketID = 10007,
                    TicketType = 2,
                    DonationID = 1,
                    City = "Cedar Rapids",
                    State = "IA",
                    GeoID = 10000,
                    StreetAddressLineOne = "13 Tree Ln",
                    StreetAddressLineTwo = "",
                    ZipCode = "55022",
                    Coordinate = new Coordinate(50.000, 45.0000),
                    RequestDateStart = new DateTime(),
                    RequestDateEnd = new DateTime(),
                    TimeRangeStart = new TimeSpan(),
                    TimeRangeEnd = new TimeSpan(),
                    CreatedAt = DateTime.Now,
                    EstimatedArrival = DateTime.Now,
                    Notes = "N/A",
                    RouteID = -1,
                    StatusDescription = "Waiting for assignment",
                    StatusID = 1,
                    StopNumber = -1,
                    Items = new Dictionary<string, int>(),
                },
                new PickUpTicketVM()
                {
                    TicketID = 10008,
                    TicketType = 2,
                    DonationID = 1,
                    City = "Solon",
                    State = "IA",
                    GeoID = 10000,
                    StreetAddressLineOne = "1 Nowhere Dr",
                    StreetAddressLineTwo = "",
                    ZipCode = "55522",
                    Coordinate = new Coordinate(50.000, 45.0000),
                    RequestDateStart = new DateTime(),
                    RequestDateEnd = new DateTime(),
                    TimeRangeStart = new TimeSpan(),
                    TimeRangeEnd = new TimeSpan(),
                    CreatedAt = DateTime.Now,
                    EstimatedArrival = DateTime.Now,
                    Notes = "N/A",
                    RouteID = -1,
                    StatusDescription = "Waiting for assignment",
                    StatusID = 1,
                    StopNumber = -1,
                    Items = new Dictionary<string, int>(),
                },
                new PickUpTicketVM()
                {
                    TicketID = 10009,
                    TicketType = 2,
                    DonationID = 1,
                    City = "Waterdeep",
                    State = "IA",
                    GeoID = 10000,
                    StreetAddressLineOne = "87 Sasson Way",
                    StreetAddressLineTwo = "",
                    ZipCode = "55523",
                    Coordinate = new Coordinate(50.000, 45.0000),
                    RequestDateStart = new DateTime(),
                    RequestDateEnd = new DateTime(),
                    TimeRangeStart = new TimeSpan(),
                    TimeRangeEnd = new TimeSpan(),
                    CreatedAt = DateTime.Now,
                    EstimatedArrival = DateTime.Now,
                    Notes = "N/A",
                    RouteID = -1,
                    StatusDescription = "Waiting for assignment",
                    StatusID = 1,
                    StopNumber = -1,
                    Items = new Dictionary<string, int>(),
                }
            };
        }
        /// <summary>
        /// Jakub Kawski
        /// 2021/03/19
        /// 
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        public int DeletePickUpTicket(PickUpTicketVM ticket)
        {
            int countBeforeRemove = _tickets.Count;
            int ticketIndex = _tickets.FindIndex(t => t.TicketID == ticket.TicketID);
            _tickets.RemoveAt(ticketIndex);
            int countAfterRemove = _tickets.Count;

            return countBeforeRemove - countAfterRemove;
        }

        /// <summary>
        /// Jakub Kawski
        /// 2021/03/15
        /// 
        /// Fake method, returns 1 if add was successful.
        /// Used for unit testing.
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        public int InsertPickUpTicket(PickUpTicketVM ticket)
        {
            int oldCount = _tickets.Count;
            int newCount;
            _tickets.Add(ticket);
            newCount = _tickets.Count;
            return newCount - oldCount;
        }
        /// <summary>
        /// Jakub Kawski
        /// 2021//03/15
        /// 
        /// Returns list of Pick Up Tickets for unit tsting
        /// </summary>
        /// <returns></returns>
        public List<PickUpTicketVM> SelectAllPickUpTickets()
        {
            return _tickets;
        }

        public List<PickUpTicketVM> SelectPickUpTicketsByDate(DateTime date)
        {
            var tickets = _tickets.FindAll(t => t.RequestDateEnd > date && t.RequestDateStart < date);
            return tickets;
        }

        public List<PickUpTicketVM> SelectPickUpTicketsByRouteID(int routeID)
        {
            var tickets = _tickets.FindAll(t => t.RouteID == routeID);
            return tickets;
        }

        public int UpdatePickUpTicket(PickUpTicketVM newTicket, PickUpTicketVM oldTicket)
        {
            int result = 0;
            if (newTicket.TicketID == oldTicket.TicketID)
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
