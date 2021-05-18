using DataAccessFakes;
using DataAccessInterfaces;
using DomainModels.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicTests
{
    /// <summary>
    /// Thomas Stout
    /// Created: 2021/03/19
    /// 
    /// Class for testing ServiceManager
    /// logic methods.
    /// </summary>
    [TestClass]
    public class ServiceManagerTests
    {
        //Instantiates IInventoryAccessor
        IServiceAccessor _serviceAccessor;

        //Sets _inventoryAccessor to InventoryFake().
        [TestInitialize]
        public void TestSetup()
        {
            _serviceAccessor = new ServiceFake();
        }

        /// <summary>
        /// Thomas Stout
        /// Created: 2021/03/19
        /// 
        /// Tests that a service is inserted and the count
        /// is increased.
        /// </summary>
        [TestMethod]
        public void TestInsertServiceReturnsRowsAffected()
        {
            Service service = new Service();
            // arrange
            const int expectedCount = 6;
            int actualCount;

            // act
            actualCount = _serviceAccessor.InsertService(service);


            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Thomas Stout
        /// Created: 2021/03/25
        /// 
        /// Tests that all Services
        /// are retrieved.
        /// </summary>
        [TestMethod]
        public void TestRetrieveAllServicesReturnsItems()
        {
            // arrange
            const int expectedCount = 5;
            int actualCount;

            //act
            actualCount = _serviceAccessor.SelectAllServices().Count;

            //assert
            Assert.AreEqual(expectedCount, actualCount);
        }
        /// <summary>
        /// Thomas Stout
        /// Created: 2021/03/25
        /// 
        /// Tests that an Service
        /// is deleted and the count is
        /// decreased.
        /// </summary>
        [TestMethod]
        public void TestDeleteServiceRemovesService()
        {
            Service service = new Service();
            // arrange
            const int expectedCount = 4;
            int actualCount;

            // act
            actualCount = _serviceAccessor.DeleteService(service.ServiceID);

            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }
        /// <summary>
        /// Thomas Stout
        /// Created: 2021/02/25
        /// 
        /// Tests that an inventory item
        /// is edited and the count remains
        /// the same.
        /// </summary>
        [TestMethod]
        public void TestEditServiceReturnsUpdatedService()
        {
            Service service = new Service();
            // arrange
            const int expectedCount = 5;
            int actualCount;


            // act
            actualCount = _serviceAccessor.UpdateService(service);

            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }
    }
}
