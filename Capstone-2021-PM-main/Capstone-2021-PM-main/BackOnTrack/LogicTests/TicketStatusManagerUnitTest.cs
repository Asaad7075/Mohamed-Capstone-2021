using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DomainModels.Tickets;
using LogicInterfaces;
using LogicLayer;
using DataAccessInterfaces;
using DataAccessFakes;

namespace LogicTests
{
    /// <summary>
    /// Summary description for TicketStatusManagerUnitTest
    /// </summary>
    [TestClass]
    public class TicketStatusManagerUnitTest
    {
        private List<TicketStatus> _statuses;
        private ITicketStatusAccessor _ticketStatusAccessor;

        [TestInitialize]
        public void TestSetup()
        {
            _ticketStatusAccessor = new TicketStatusAccessorFake();
        }
        [TestMethod]
        public void TestTicketStatusManagerReturnsList()
        {
            //arrange
            ITicketStatusManager ticketStatusManager = new TicketStatusManager(_ticketStatusAccessor);
            int expectedCount = 4;
            int actualCount;
            //act
            _statuses = ticketStatusManager.RetrieveAllTicketStatuses();
            actualCount = _statuses.Count;
            //assert
            Assert.AreEqual(expectedCount, actualCount);
        }
    }
}
