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
    /// Chase Martin
    /// Created: 2021/03/26
    /// 
    /// Class for testing ServiceProviderManager
    /// logic methods.
    /// </summary>
    [TestClass]
    public class ServiceProviderManagerTests
    {
        
        IServiceProviderAccessor _serviceProviderAccessor;

       
        [TestInitialize]
        public void TestSetup()
        {
            _serviceProviderAccessor = new ServiceProviderFake();
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/03/26
        /// 
        /// Tests that a service provider is inserted and the count
        /// is increased.
        /// </summary>
        [TestMethod]
        public void TestInsertServiceProviderReturnsRowsAffected()
        {
            ServiceProvider serviceProvider = new ServiceProvider();
            // arrange
            const int expectedCount = 6;
            int actualCount;

            // act
            actualCount = _serviceProviderAccessor.InsertServiceProvider(serviceProvider);


            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/03/26
        /// 
        /// Tests that all Service Providers
        /// are retrieved.
        /// </summary>
        [TestMethod]
        public void TestRetrieveAllServiceProvidersReturnsProviders()
        {
            // arrange
            const int expectedCount = 5;
            int actualCount;

            //act
            actualCount = _serviceProviderAccessor.SelectAllServiceProviders().Count;

            //assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/03/26
        /// 
        /// Tests that an Service Provider
        /// is deleted and the count is
        /// decreased.
        /// </summary>
        [TestMethod]
        public void TestDeleteServiceProviderRemovesProvider()
        {
            ServiceProvider serviceProvider = new ServiceProvider();
            // arrange
            const int expectedCount = 4;
            int actualCount;

            // act
            actualCount = _serviceProviderAccessor.DeleteServiceProvider(serviceProvider.ServiceProviderID);

            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }
        /// <summary>
        /// Chase Martin
        /// Created: 2021/02/26
        /// 
        /// Tests that a Service Provider
        /// is edited and the count remains
        /// the same.
        /// </summary>
        [TestMethod]
        public void TestEditServiceProviderReturnsUpdatedProvider()
        {
            ServiceProvider serviceProvider = new ServiceProvider();
            // arrange
            const int expectedCount = 5;
            int actualCount;


            // act
            actualCount = _serviceProviderAccessor.UpdateServiceProvider(serviceProvider);

            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }

      
    }
}

