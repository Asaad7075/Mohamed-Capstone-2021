using BingMapsRESTToolkit;
using DataAccessFakes;
using DataAccessInterfaces;
using DomainModels.Tickets;
using LogicInterfaces;
using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Text;

namespace LogicTests
{
    /// <summary>
    /// Jakub Kawski
    /// 2021/03/26
    /// Summary description for RideTicketManagerUnitTests
    /// 
    /// </summary>
    [TestClass]
    public class RideTicketManagerUnitTests
    {
        private List<RideTicketVM> _tickets;
        private IRideTicketAccessor _rideTicketAccessor;
        private IGeoLocationAccessor _geoLocationAccessor;
        [TestInitialize]
        public void TestSetup()
        {
            _rideTicketAccessor = new RideTicketAccessorFake();
            _geoLocationAccessor = new GeoLocationAccessorFakes();
        }
        /// <summary>
        /// Jakub Kawski
        /// 2021/03/26
        /// 
        /// Test the logic layer manager returns a list.
        /// </summary>
        [TestMethod]
        public void TestRideTicketManagerRetrieveAllTicketsReturnsList()
        {
            //arrange
            IRideTicketManager ticketManager = new RideTicketManager(_rideTicketAccessor, _geoLocationAccessor);
            int expectedListCount = 5;
            int actualListCount;

            //act
            _tickets = ticketManager.RetrieveAllTickets();
            actualListCount = _tickets.Count;

            //assert
            Assert.AreEqual(expectedListCount, actualListCount);
        }
        /// <summary>
        /// Jakub Kawski
        /// 2021/03/15
        /// 
        /// Test the manager returns true when adding a ticket.
        /// </summary>
        [TestMethod]
        public void TestRideTicketManagerAddTicketReturnsTrue()
        {
            //arrange
            IRideTicketManager ticketManager = new RideTicketManager(_rideTicketAccessor, _geoLocationAccessor);
            bool expectedResult = true;
            bool actualResult = false;
            RideTicketVM ticket = new RideTicketVM()
            {
                TicketID = 10010,
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
            };

            //act
            actualResult = ticketManager.AddTicket(ticket);

            //assert
            Assert.AreEqual(expectedResult, actualResult);
        }
        /// <summary>
        /// Jakub Kawski
        /// 2021/03/26
        /// </summary>
        [TestMethod]
        public void TestRideTicketManagerUpdateReturnsTrue()
        {
            //arrange
            IRideTicketManager ticketManager = new RideTicketManager(_rideTicketAccessor, _geoLocationAccessor);
            RideTicketVM newTicket = new RideTicketVM()
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
            };
            RideTicketVM oldTicket = new RideTicketVM()
            {
                TicketID = 10001,
                TicketType = 3,
                FetchCity = "NoWhere",
                FetchState = "IA",
                FetchGeoID = 10001,
                FetchStreetAddressLineOne = "65 Orange Rd",
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
                ClientLastName = "Duke",
                RequiresHAV = false,
                DateOfRide = DateTime.Now.AddDays(5),
                TimeRangeStart = new TimeSpan(8, 0, 0),
                TimeRangeEnd = new TimeSpan(12, 30, 0)
            };
            bool expected = true;
            bool actual = false;
            //act
            actual = ticketManager.UpdateTicket(newTicket, oldTicket);
            //assert
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        /// Jakub Kawski
        /// 2021/03/26
        /// </summary>
        [TestMethod]
        public void TestRideTicketManagerDeleteReturnsTrue()
        {
            //arrange
            IRideTicketManager ticketManager = new RideTicketManager(_rideTicketAccessor, _geoLocationAccessor);
            RideTicketVM ticket = new RideTicketVM()
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
            };
            bool expected = true;
            bool actual = false;
            //act
            actual = ticketManager.DeleteTicket(ticket);
            //assert
            Assert.AreEqual(expected, actual);
        }
    }
}
