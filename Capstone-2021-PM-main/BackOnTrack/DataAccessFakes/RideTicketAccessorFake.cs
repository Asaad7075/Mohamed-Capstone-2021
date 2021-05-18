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
    /// <summary>
    /// Jakub Kawski
    /// 2021/03/26
    /// 
    /// </summary>
    public class RideTicketAccessorFake : IRideTicketAccessor
    {
        private List<RideTicketVM> _tickets = new List<RideTicketVM>();
        public RideTicketAccessorFake()
        {
            _tickets.Add(new RideTicketVM()
            {
                TicketID = 10000,
                TicketType = 3,
                FetchCity = "Iowa City",
                FetchState = "IA",
                FetchGeoID = 10001,
                FetchStreetAddressLineOne = "101 Main Ave",
                FetchStreetAddressLineTwo = "",
                FetchZipCode = "55522",
                FetchCoordinate = new Coordinate(24.000, 125.0000),
                DestinationCity = "Iowa City",
                DestinationState = "IA",
                DestinationGeoID = 10002,
                DestinationStreetAddressLineOne = "202 Lame Ave",
                DestinationStreetAddressLineTwo = "",
                DestinationZipCode = "55522",
                DestinationCoordinate = new Coordinate(24.000, 125.0000),
                CreatedAt = DateTime.Now,
                EstimatedArrival = DateTime.Now,
                ClientID = 10001,
                Notes = "N/A",
                RouteID = -1,
                StatusDescription = "Waiting for assignment",
                StatusID = 1,
                StopNumber = -1,
                ClientFirstName = "Jimmy",
                ClientLastName = "Pulp",
                RequiresHAV = false,
                DateOfRide = DateTime.Now.AddDays(5),
                TimeRangeStart = new TimeSpan(8, 0, 0),
                TimeRangeEnd = new TimeSpan(12, 30, 0)               
            });
            _tickets.Add(new RideTicketVM()
            {
                TicketID = 10001,
                TicketType = 3,
                FetchCity = "Iowa City",
                FetchState = "IA",
                FetchGeoID = 10001,
                FetchStreetAddressLineOne = "56 Heaven Rd",
                FetchStreetAddressLineTwo = "",
                FetchZipCode = "55522",
                FetchCoordinate = new Coordinate(24.000, 125.0000),
                DestinationCity = "Iowa City",
                DestinationState = "IA",
                DestinationGeoID = 10002,
                DestinationStreetAddressLineOne = "202 Lame Ave",
                DestinationStreetAddressLineTwo = "",
                DestinationZipCode = "55522",
                DestinationCoordinate = new Coordinate(24.000, 125.0000),
                CreatedAt = DateTime.Now,
                EstimatedArrival = DateTime.Now,
                ClientID = 10001,
                Notes = "N/A",
                RouteID = -1,
                StatusDescription = "Waiting for assignment",
                StatusID = 1,
                StopNumber = -1,
                ClientFirstName = "Luke",
                ClientLastName = "Mob",
                RequiresHAV = false,
                DateOfRide = DateTime.Now.AddDays(5),
                TimeRangeStart = new TimeSpan(8, 0, 0),
                TimeRangeEnd = new TimeSpan(12, 30, 0)
            });
            _tickets.Add(new RideTicketVM()
            {
                TicketID = 10002,
                TicketType = 3,
                FetchCity = "Iowa City",
                FetchState = "IA",
                FetchGeoID = 10001,
                FetchStreetAddressLineOne = "101 Main Ave",
                FetchStreetAddressLineTwo = "",
                FetchZipCode = "55522",
                FetchCoordinate = new Coordinate(24.000, 125.0000),
                DestinationCity = "Iowa City",
                DestinationState = "IA",
                DestinationGeoID = 10002,
                DestinationStreetAddressLineOne = "202 Lame Ave",
                DestinationStreetAddressLineTwo = "",
                DestinationZipCode = "55522",
                DestinationCoordinate = new Coordinate(24.000, 125.0000),
                CreatedAt = DateTime.Now,
                EstimatedArrival = DateTime.Now,
                ClientID = 10001,
                Notes = "N/A",
                RouteID = -1,
                StatusDescription = "Waiting for assignment",
                StatusID = 1,
                StopNumber = -1,
                ClientFirstName = "Jimmy",
                ClientLastName = "Pulp",
                RequiresHAV = false,
                DateOfRide = DateTime.Now.AddDays(5),
                TimeRangeStart = new TimeSpan(8, 0, 0),
                TimeRangeEnd = new TimeSpan(12, 30, 0)
            });
            _tickets.Add(new RideTicketVM()
            {
                TicketID = 10003,
                TicketType = 3,
                FetchCity = "Iowa City",
                FetchState = "IA",
                FetchGeoID = 10001,
                FetchStreetAddressLineOne = "Firehouse St",
                FetchStreetAddressLineTwo = "",
                FetchZipCode = "55522",
                FetchCoordinate = new Coordinate(24.000, 125.0000),
                DestinationCity = "Iowa City",
                DestinationState = "IA",
                DestinationGeoID = 10002,
                DestinationStreetAddressLineOne = "88 Birch Rd",
                DestinationStreetAddressLineTwo = "",
                DestinationZipCode = "55522",
                DestinationCoordinate = new Coordinate(24.000, 125.0000),
                CreatedAt = DateTime.Now,
                EstimatedArrival = DateTime.Now,
                ClientID = 10001,
                Notes = "N/A",
                RouteID = -1,
                StatusDescription = "Waiting for assignment",
                StatusID = 1,
                StopNumber = -1,
                ClientFirstName = "Nape",
                ClientLastName = "Tenor",
                RequiresHAV = false,
                DateOfRide = DateTime.Now,
                TimeRangeStart = new TimeSpan(8, 0, 0),
                TimeRangeEnd = new TimeSpan(12, 30, 0)
            });
            _tickets.Add(new RideTicketVM()
            {
                TicketID = 10004,
                TicketType = 3,
                FetchCity = "Iowa City",
                FetchState = "IA",
                FetchGeoID = 10001,
                FetchStreetAddressLineOne = "590 Cook St",
                FetchStreetAddressLineTwo = "",
                FetchZipCode = "55522",
                FetchCoordinate = new Coordinate(24.000, 125.0000),
                DestinationCity = "Iowa City",
                DestinationState = "IA",
                DestinationGeoID = 10002,
                DestinationStreetAddressLineOne = "2nd Ave",
                DestinationStreetAddressLineTwo = "",
                DestinationZipCode = "55522",
                DestinationCoordinate = new Coordinate(24.000, 125.0000),
                CreatedAt = DateTime.Now,
                EstimatedArrival = DateTime.Now,
                ClientID = 10001,
                Notes = "N/A",
                RouteID = -1,
                StatusDescription = "Waiting for assignment",
                StatusID = 1,
                StopNumber = -1,
                ClientFirstName = "Gordan",
                ClientLastName = "Steak",
                RequiresHAV = false,
                DateOfRide = DateTime.Now,
                TimeRangeStart = new TimeSpan(8, 0, 0),
                TimeRangeEnd = new TimeSpan(12, 30, 0)
            });
        }
        /// <summary>
        /// Jakub Kawski
        /// 2021/03/26
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        public int DeleteRideTicket(RideTicketVM ticket)
        {
            int countBeforeRemove = _tickets.Count;
            int ticketIndex = _tickets.FindIndex(t => t.TicketID == ticket.TicketID);
            _tickets.RemoveAt(ticketIndex);
            int countAfterRemove = _tickets.Count;

            return countBeforeRemove - countAfterRemove;
        }
        /// <summary>
        /// Jakub KAwski
        /// 2021/03/26
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        public int InsertRideTicket(RideTicketVM ticket)
        {
            int oldCount = _tickets.Count;
            int newCount;
            _tickets.Add(ticket);
            newCount = _tickets.Count;
            return newCount - oldCount;
        }
        /// <summary>
        /// Jakub Kawski
        /// 2021/03/26
        /// </summary>
        /// <returns></returns>
        public List<RideTicketVM> SelectAllRideTickets()
        {
            return _tickets;
        }

        public List<RideTicketVM> SelectRideTicketsByDate(DateTime date)
        {
            var tickets = _tickets.FindAll(t => t.DateOfRide.Date == date.Date);
            return tickets;
        }

        public List<RideTicketVM> SelectRideTicketsByRouteID(int routeID)
        {
            var tickets = _tickets.FindAll(t => t.RouteID == routeID);
            return tickets;
        }

        /// <summary>
        /// Jakub Kawski
        /// 2021/03/26
        /// </summary>
        /// <param name="newTicket"></param>
        /// <param name="oldTicket"></param>
        /// <returns></returns>
        public int UpdateRideTicket(RideTicketVM newTicket, RideTicketVM oldTicket)
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
