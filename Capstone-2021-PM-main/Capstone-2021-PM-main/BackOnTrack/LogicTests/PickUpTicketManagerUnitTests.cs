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

namespace LogicTests
{
    [TestClass]
    public class PickUpTicketManagerUnitTests
    {
        private List<PickUpTicketVM> _tickets;
        private IPickUpTicketAccessor _pickupTicketAccessor;
        private IGeoLocationAccessor _geoLocationAccessor;
        [TestInitialize]
        public void TestSetup()
        {
            _pickupTicketAccessor = new PickUpTicketAccessorFake();
            _geoLocationAccessor = new GeoLocationAccessorFakes();
        }
        /// <summary>
        /// Jakub Kawski
        /// 2021/03/15
        /// 
        /// Test the logic layer manager returns a list.
        /// </summary>
        [TestMethod]
        public void TestPickUpTicketManagerRetrieveAllTicketsReturnsList()
        {
            //arrange
            IPickUpTicketManager ticketManager = new PickUpTicketManager(_pickupTicketAccessor, _geoLocationAccessor);
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
        public void TestPickUpTicketManagerAddTicketReturnsTrue()
        {
            //arrange
            IPickUpTicketManager ticketManager = new PickUpTicketManager(_pickupTicketAccessor, _geoLocationAccessor);
            bool expectedResult = true;
            bool actualResult = false;
            PickUpTicketVM ticket = new PickUpTicketVM() 
            {
                TicketID = 10009,
                TicketType = 2,
                DonationID = 1,
                City = "Des Moines",
                State = "IA",
                GeoID = 10000,
                StreetAddressLineOne = "42 Tester Way",
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
            };

            //act
            actualResult = ticketManager.AddTicket(ticket);

            //assert
            Assert.AreEqual(expectedResult, actualResult);
        }
        [TestMethod]
        public void TestPickUpTicketManagerUpdateReturnsTrue()
        {
            //arrange
            IPickUpTicketManager ticketManager = new PickUpTicketManager(_pickupTicketAccessor, _geoLocationAccessor);
            PickUpTicketVM newTicket = new PickUpTicketVM()
            {
                TicketID = 10005,
                TicketType = (int)TicketType.PickUp
            };
            PickUpTicketVM oldTicket = new PickUpTicketVM()
            {
                TicketID = 10005,
                TicketType = (int)TicketType.PickUp
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
        /// 2021/03/19
        /// </summary>
        [TestMethod]
        public void TestPickUpTicketManagerDeleteReturnsTrue()
        {
            //arrange
            IPickUpTicketManager ticketManager = new PickUpTicketManager(_pickupTicketAccessor, _geoLocationAccessor);
            PickUpTicketVM ticket = new PickUpTicketVM()
            {
                TicketID = 10005,
                TicketType = (int)TicketType.PickUp,
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
