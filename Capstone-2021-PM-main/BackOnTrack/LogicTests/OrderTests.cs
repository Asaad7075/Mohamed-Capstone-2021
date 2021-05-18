using DataAccessFakes;
using DataAccessInterfaces;
using DomainModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicTests
{
    /// <summary>
    /// Summary description for OrderTests
    /// </summary>
    [TestClass]
    public class OrderTests
    {
        private IOrderAccessor _orderAccessor;

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/21
        /// 
        /// Set up accessor for data access dependency.
        /// </summary>
        [TestInitialize]
        public void TestSetup() 
        {
            _orderAccessor = new OrderFake();
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/21
        /// 
        /// Test for selecting an 
        /// already existing order 
        /// by order id.
        /// </summary>
        [TestMethod]
        public void SelectOrderByOrderIDGood()
        {
            // Arrange
            int expectedResult = 10004;
            int actualResult;
            int testOrderID = 10004;
            // Act
            Order testOrder = _orderAccessor.SelectOrderByOrderID(
                testOrderID);
            actualResult = testOrder.OrderID;

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
