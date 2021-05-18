using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using LogicLayer;
using DomainModels;
using DataAccessInterfaces;
using DataAccessFakes;
using LogicInterfaces;
using DomainModels.Services;

/// <summary>
/// Summary description for ServiceReviewTests
/// </summary>
namespace LogicTests
{
    [TestClass]
    public class ServiceReviewManagerTests
    {
        private IServiceReviewAccessor _serviceReviewAccessor;

        /// <summary>
        /// Chase Martin
        /// Created: 2021/04/09
        /// 
        /// Set up the accessor for the database dependency
        /// </summary>
        [TestInitialize]
        public void TestSetup()
        {
            _serviceReviewAccessor = new ServiceReviewFake();
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/04/09
        /// 
        /// This test will test InsertServiceReviewReturnsRowsAffected to ensure it
        /// returns a correct amount of rows
        /// in the database.
        /// </summary>
        [TestMethod]
        public void TestInsertServiceReviewReturnsRowsAffected()
        {
            ServiceReview serviceReview = new ServiceReview();
            // arrange
            const int expectedCount = 1;
            int actualCount;

            // act
            actualCount = _serviceReviewAccessor.InsertServiceReview(serviceReview);


            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/04/09
        /// This test will test creating and adding the logic of adding a service review.
        /// </summary>
        [TestMethod]
        public void TestAddServiceReviewFromUserGood()
        {
            //arrange
            ServiceReviewManager serviceReviewManager = new ServiceReviewManager(_serviceReviewAccessor);
            ServiceReview serviceReview = (new ServiceReview()
            {
                ServiceName = "Haircut",
                ProviderFirstName = "Payton",
                ProviderLastName = "Rud",
                Rating = "5",
                ClientComment = "Really Good",
                ServiceReviewID = 1004,
            });
            bool expectedResult = true;
            bool actualResult;

            //act
            actualResult = serviceReviewManager.AddServiceReviewFromUser(serviceReview);
            //assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/04/20
        /// 
        /// Tests that an Service Review
        /// is deleted and the count is
        /// decreased.
        /// </summary>
        [TestMethod]
        public void TestDeleteServiceRemovesServiceReview()
        {
            ServiceReview serviceReview = new ServiceReview();
            // arrange
            const int expectedCount = 0;
            int actualCount;

            // act
            actualCount = _serviceReviewAccessor.DeleteServiceReview(serviceReview);

            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }

    }
}


