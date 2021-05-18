/// Jakub Kawski
/// Created: 2021/02/25
/// 
/// 
/// </summary>
///
/// <remarks>
/// Updater Name
/// Updated: yyyy/mm/dd
/// </remarks>
using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccessInterfaces;
using DataAccessFakes;
using DomainModels.Tickets;
using LogicLayer;
using LogicInterfaces;
using System.Linq;

namespace LogicTests
{
    /// <summary>
    /// Summary description for DeliveryTicketManagerUnitTests
    /// </summary>
    [TestClass]
    public class DeliveryTicketManagerUnitTests
    {
        private List<DeliveryTicketVM> _tickets;
        private IDeliveryTicketAccessor _deliveryTicketAccessor;
        private IGeoLocationAccessor _geoLocationAccessor;
        /// Jakub Kawski
        /// Created: 2021/02/25
        /// 
        /// 
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// </remarks>
        [TestInitialize]
        public void TestSetup()
        {
            _deliveryTicketAccessor = new DeliveryTicketAccessorFake();
            _geoLocationAccessor = new GeoLocationAccessorFakes();
        }
        /// Jakub Kawski
        /// Created: 2021/02/25
        /// 
        /// 
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// </remarks>
        [TestMethod]
        public void TestDeliveryTicketManagerInsertTicketReturnsTrue()
        {
            //arrange
            IDeliveryTicketManager ticketManager = new DeliveryTicketManager(_deliveryTicketAccessor, _geoLocationAccessor);
            DeliveryTicketVM ticket = new DeliveryTicketVM()
            {
                GeoID = 10000,
                TicketType = (int)TicketType.Delivery
            };
            bool expected = true;
            bool actual = false;
            //act
            actual = ticketManager.AddTicket(ticket);
            //assert
            Assert.AreEqual(expected, actual);
        }
        /// Jakub Kawski
        /// Created: 2021/02/25
        /// 
        /// 
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// </remarks>
        [TestMethod]
        public void TestDeliveryTicketManagerRetrieveAllTicketsReturnsList()
        {
            //arrange
            IDeliveryTicketManager ticketManager = new DeliveryTicketManager(_deliveryTicketAccessor, _geoLocationAccessor);
            int expectedListCount = 5;
            int actualListCount;

            //act
            _tickets = ticketManager.RetrieveAllTickets();
            actualListCount = _tickets.Count;

            //assert
            Assert.AreEqual(expectedListCount, actualListCount);
        }
        /// Jakub Kawski
        /// Created: 2021/02/25
        /// 
        /// 
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// </remarks>
        [TestMethod]
        public void TestDeliveryTicketManagerDeleteTicketReturnsTrue()
        {
            //arrange
            IDeliveryTicketManager ticketManager = new DeliveryTicketManager(_deliveryTicketAccessor, _geoLocationAccessor);
            DeliveryTicketVM ticket = new DeliveryTicketVM()
            {
                TicketID = 10000,
                TicketType = (int)TicketType.Delivery
            };
            bool expected = true;
            bool actual = false;
            //act
            actual = ticketManager.DeleteTicket(ticket);
            //assert
            Assert.AreEqual(expected, actual);
        }
        /// Jakub Kawski
        /// Created: 2021/02/25
        /// 
        /// 
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// </remarks>
        [TestMethod]
        public void TestDeliveryTicketManagerUpdateTicketReturnsTrue()
        {
            //arrange
            IDeliveryTicketManager ticketManager = new DeliveryTicketManager(_deliveryTicketAccessor, _geoLocationAccessor);
            DeliveryTicketVM newTicket = new DeliveryTicketVM()
            {
                TicketID = 10000,
                TicketType = (int)TicketType.Delivery
            };
            DeliveryTicketVM oldTicket = new DeliveryTicketVM()
            {
                TicketID = 10000,
                TicketType = (int)TicketType.Delivery
            };
            bool expected = true;
            bool actual = false;
            //act
            actual = ticketManager.UpdateTicket(newTicket, oldTicket);
            //assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/09
        /// 
        /// Tests retrieval of delivery
        /// tickets by client id.
        /// </summary>
        [TestMethod]
        public void SelectDeliveryTicketsByClientIdGood()
        {
            // arrange
            _deliveryTicketAccessor = new DeliveryTicketAccessorFake();
            int clientId = 10004;
            List<DeliveryTicketVM> result;
            int expected = 1; // count of delivery tickets
            int actual;

            // act
            result = _deliveryTicketAccessor.SelectDeliveryTicketsByClientId(clientId);
            actual = result.Count();

            // assert
            Assert.AreEqual(expected, actual);

        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/16
        /// 
        /// Returns the delivery 
        /// ticket based on the order ID.
        /// </summary>
        [TestMethod]
        public void SelectDeliveryTicketByOrderIDGood()
        {
            // Arrange
            int expectedResult = 10000; // TicketID
            int actualResult;
            int orderID = 10004;

            // Assert
            DeliveryTicketVM testTicket = _deliveryTicketAccessor.SelectDeliveryTicketByOrderID(orderID);
            actualResult = testTicket.TicketID;

            // Act
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
