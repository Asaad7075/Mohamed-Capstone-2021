using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using LogicLayer;
using DomainModels;
using DataAccessInterfaces;
using DataAccessFakes;
using LogicInterfaces;
using DomainModels.Tickets;

namespace LogicTests
{
    /// <summary>
    /// Summary description for RideReviewTests
    /// </summary>
    [TestClass]
    public class RideReviewManagerTests
    {
        private IRideReviewAccessor _rideReviewAccessor;

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/03/07
        /// 
        /// Set up the accessor for the database dependency
        /// </summary>
        [TestInitialize]
        public void TestSetup()
        {
            _rideReviewAccessor = new RideReviewFake();
        }

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/03/07
        /// 
        /// This test will test RetriveAllRideReviews to ensure it returns all the RideReviews
        /// in the database.
        /// </summary>
        [TestMethod]
        public void TestRetriveAllRideReviewsReturnsAllRideReviews()
        {
            TestSetup();
            //arrange
            RideReviewManager rideReviewManager = new RideReviewManager(_rideReviewAccessor);
            int expectedRideReviewCount = 5;
            int actualRideReviewCount;

            //act
            List<RideReviewVM> rideReviews = rideReviewManager.RetrieveAllRideReviews();
            actualRideReviewCount = rideReviews.Count;

            //assert
            Assert.AreEqual(expectedRideReviewCount, actualRideReviewCount);
        }

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/03/07
        /// This test will test creating and adding the logic of adding a ride review.
        /// </summary>
        [TestMethod]
        public void TestAddRideReviewFromDriverGood()
        {
            //arrange
            RideReviewManager rideReviewManager = new RideReviewManager(_rideReviewAccessor);
            RideReviewVM rideReview = (new RideReviewVM()
            {
                RideReviewID = 100004,
                TicketID = 100004,
                ClientID = 100004,
                DriverID = 100004,
                DriverClientRating = 1,
                DriverComment = "Angry fake rider, even gave a fake tip."
            });
            bool expectedResult = true;
            bool actualResult;

            //act
            actualResult = rideReviewManager.AddRideReviewFromDriver(rideReview);
            //assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/03/13
        /// This test will get a ticket by employee id
        /// </summary>
        [TestMethod]
        public void TestRetrieveTicketByEmployeeIDGood()
        {
            //arrange
            RideReviewManager rideReviewManager = new RideReviewManager(_rideReviewAccessor);
            int expectedTicketCount = 2;
            int actualTicketCount;

            //act
            List<RideReviewVM> tickets = rideReviewManager.RetrieveRideTicketsByEmployeeID(100000);
            actualTicketCount = tickets.Count;

            //assert
            Assert.AreEqual(expectedTicketCount, actualTicketCount);
        }

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/03/28
        /// This test will edit a selected ride review.
        /// </summary>
        [TestMethod]
        public void TestEditRideReviewGood()
        {
            //arrange
            RideReviewManager rideReviewManager = new RideReviewManager(_rideReviewAccessor);
            RideReview oldRideReview = (new RideReview()
            {
                RideReviewID = 100004,
                TicketID = 100004,
                ClientID = 100004,
                DriverID = 100004,
                DriverClientRating = 1,
                DriverComment = "Angry fake rider, even gave a fake tip."
            });
            RideReview newRideReview = (new RideReview()
            {
                RideReviewID = 100004,
                TicketID = 100004,
                ClientID = 100004,
                DriverID = 100004,
                DriverClientRating = 4,
                DriverComment = "Wasent so bad, Client did leave a real tip. I just didn't see it."
            });
            bool expectedResult = true;
            bool actualResult;

            //act
            actualResult = rideReviewManager.EditRideReviewFromDriver(oldRideReview, newRideReview);

            //assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/03/28
        /// This test will edit a selected ride review.
        /// </summary>
        [TestMethod]
        public void TestDeleteRideReviewGood()
        {
            //arrange
            RideReviewManager rideReviewManager = new RideReviewManager(_rideReviewAccessor);
            List<RideReviewVM> tickets = _rideReviewAccessor.SelectAllRideReviews();
            bool expectedResult = true;
            bool actualResult;

            //act
            actualResult = rideReviewManager.RemoveRideReviewFromDriver(tickets[0]);

            //assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// Nate Hepker
        /// Created 2021/04/06
        /// Tests the RetriveTicketByClientID is valid and gets only the tickets for the client to review
        /// </summary>
        [TestMethod]
        public void TestRetrieveTicketByClientIDGood()
        {
            //arrange
            RideReviewManager rideReviewManager = new RideReviewManager(_rideReviewAccessor);
            int expectedTicketCount = 3;
            int actualTicketCount;

            //act
            List<RideReviewVM> tickets = rideReviewManager.RetrieveRideTicketsByClientID(100020);
            actualTicketCount = tickets.Count;

            //assert
            Assert.AreEqual(expectedTicketCount, actualTicketCount);
        }

        /// <summary>
        /// Nate Hepker
        /// Created 2021/04/09
        /// Tests the RetriveRideTicketsToReview is valid and gets only the tickets for the client to review
        /// </summary>
        [TestMethod]
        public void TestRetrieveAllRideTicketToReviewGood()
        {
            //arrange
            RideReviewManager rideReviewManager = new RideReviewManager(_rideReviewAccessor);
            int expectedTicketCount = 3;
            int actualTicketCount;

            //act
            List<RideReviewVM> tickets = rideReviewManager.RetrieveAllRideTicketToReview();
            actualTicketCount = tickets.Count;

            //assert
            Assert.AreEqual(expectedTicketCount, actualTicketCount);
        }



        /// <summary>
        /// Nate Hepker
        /// Created 2021/04/17
        /// Tests edit logic for editening the ride review from a client on the web side
        /// </summary>
        [TestMethod]
        public void TestEditClientRideReviewGood()
        {
            //arrange
            RideReviewManager rideReviewManager = new RideReviewManager(_rideReviewAccessor);
            RideReview oldRideReview = (new RideReview()
            {
                RideReviewID = 100004,
                TicketID = 100004,
                ClientID = 100004,
                DriverID = 100004,
                DriverClientRating = 1,
                DriverComment = "Angry fake rider, even gave a fake tip."
            });
            RideReview newRideReview = (new RideReview()
            {
                RideReviewID = 100004,
                TicketID = 100004,
                ClientID = 100004,
                DriverID = 100004,
                DriverClientRating = 4,
                DriverComment = "Wasent so bad, Client did leave a real tip. I just didn't see it."
            });
            bool expectedResult = true;
            bool actualResult;

            //act
            actualResult = rideReviewManager.EditRideReviewFromClient(oldRideReview, newRideReview);

            //assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// Nate Hepker
        /// Created 2021/04/17
        /// test retrieving the ride review by its id
        /// </summary>
        [TestMethod]
        public void TestRetrieveReviewByReviewIDGood()
        {
            //arrange
            RideReviewManager rideReviewManager = new RideReviewManager(_rideReviewAccessor);
            RideReview expectedRideReview = new RideReview()
            {
                RideReviewID = 100001,
                TicketID = 100001,
                ClientID = 100001,
                ClientDriverRating = 5,
                ClientComment = "Fantastic Fake Ride!",
                DriverID = 100001,
                DriverClientRating = 5,
                DriverComment = "Nice fake rider, even gave a fake tip."
            };
            RideReview actualRideReview;
            //act
            actualRideReview = _rideReviewAccessor.SelectRideReviewsByReviewID(100001);
            //assert
            Assert.AreEqual(expectedRideReview.RideReviewID, actualRideReview.RideReviewID);
        }

        /// <summary>
        /// Nate Hepker
        /// Created 2021/04/17
        /// Throws exeption if an invalid review is added
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestAddRideReviewFromDriverBad()
        {
            //arrange
            RideReviewManager rideReviewManager = new RideReviewManager(_rideReviewAccessor);
            RideReviewVM rideReview = (new RideReviewVM()
            {
                RideReviewID = 100004,
                TicketID = 100004,
                ClientID = 100004,
                DriverID = 100004,
                DriverClientRating = 1,
                DriverComment = ""
            });

            //act
            rideReviewManager.AddRideReviewFromDriver(rideReview);
        }

        /// <summary>
        /// Nate Hepker
        /// Created 2021/04/17
        /// Throws exeption if the employee id does not have any rides to review
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestRetrieveTicketByEmployeeIDBad()
        {
            //arrange
            RideReviewManager rideReviewManager = new RideReviewManager(_rideReviewAccessor);

            //act
            List<RideReviewVM> tickets = rideReviewManager.RetrieveRideTicketsByEmployeeID(99999);
        }

        /// <summary>
        /// Nate Hepker
        /// Created 2021/04/17
        /// Throws exeption if a ride review is edited with a bad comment
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestEditRideReviewCommentBad()
        {
            //arrange
            RideReviewManager rideReviewManager = new RideReviewManager(_rideReviewAccessor);
            RideReview oldRideReview = (new RideReview()
            {
                RideReviewID = 100004,
                TicketID = 100004,
                ClientID = 100004,
                DriverID = 100004,
                DriverClientRating = 1,
                DriverComment = "Angry fake rider, even gave a fake tip."
            });
            RideReview newRideReview = (new RideReview()
            {
                RideReviewID = 100004,
                TicketID = 100004,
                ClientID = 100004,
                DriverID = 100004,
                DriverClientRating = 4,
                DriverComment = ""
            });

            //act
            rideReviewManager.EditRideReviewFromDriver(oldRideReview, newRideReview);
        }

        /// <summary>
        /// Nate Hepker
        /// Created 2021/04/17
        /// Throws exeption if a ride review is edited with a bad rating
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestEditRideReviewRatingBad()
        {
            //arrange
            RideReviewManager rideReviewManager = new RideReviewManager(_rideReviewAccessor);
            RideReview oldRideReview = (new RideReview()
            {
                RideReviewID = 100004,
                TicketID = 100004,
                ClientID = 100004,
                DriverID = 100004,
                DriverClientRating = 1,
                DriverComment = "Angry fake rider, even gave a fake tip."
            });
            RideReview newRideReview = (new RideReview()
            {
                RideReviewID = 100004,
                TicketID = 100004,
                ClientID = 100004,
                DriverID = 100004,
                DriverClientRating = 0,
                DriverComment = "New comment"
            });

            //act
            rideReviewManager.EditRideReviewFromDriver(oldRideReview, newRideReview);
        }

    }
}
