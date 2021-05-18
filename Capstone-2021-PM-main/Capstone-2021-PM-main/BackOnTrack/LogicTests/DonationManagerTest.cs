using DataAccessFakes;
using DataAccessInterfaces;
using DomainModels;
using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicTests
{
    /// <summary>
    /// Asaad Mohamed
    /// Created: 2021/02/22
    /// 
    /// This donation  approve  test class for Retrieve All donation  List method and 
    /// update donation method
    /// 
    /// </summary>
    [TestClass]
    public class DonationManagerTest
    {
        private IDonationAccessor _fakeDonationAccessor;
        private DonationManager _donationManager;
        /// <summary>
        /// Asaad Mohamed 
        /// Created: 2021/02/22
        /// 
        /// Setup accessor for data access dependency.
        /// </summary>

        [TestInitialize]
        public void TestSetup()
        {
            _fakeDonationAccessor = new DonationFake();

            _donationManager = new DonationManager(_fakeDonationAccessor);
        }

        /// <summary>
        /// Asaad Mohamed 
        /// Created: 2021/02/22
        /// test to Retrieve All donation List method
        /// </summary>

        [TestMethod]
        public void TestRetrieveAllDonationList()
        {
            // Arrange 
            List<Donation> RetrieveAllDonationList;

            //Act

            RetrieveAllDonationList = _donationManager.RetrieveAllDonationList();

            // Assert
            Assert.AreEqual(2, RetrieveAllDonationList.Count);
        }
        /// <summary>
        /// Asaad Mohamed 
        /// Created: 2021/02/04
        /// test to update donation  List method
        /// </summary>

        [TestMethod]
        public void TestEditDonationItem()
        {
            // arrange
            Donation oldDonationItem = new Donation()
            {
                DonationID = 1,
                DonorID = 2,
                NameOfItem = "Shoe",
                Description = "Boot with size 9 USA",
                EstValue = 200.0M,
                AgeofItem = 2,
                DropOff = true,
                PickUp = true,
                PickUpDateTime = DateTime.Parse("2021-04-02"),
                MailReceipt = true,
                EmailReceipt = true,
                DonationStatus = "approve"


            };
            Donation newDonationItem = new Donation()
            {
                DonationID = 4,
                DonorID = 4,
                NameOfItem = "car",
                Description = "Toyota camry 2000 ",
                EstValue = 1000.0M,
                AgeofItem = 21,
                DropOff = true,
                PickUp = true,
                PickUpDateTime = DateTime.Now,
                MailReceipt = true,
                EmailReceipt = true,
                DonationStatus = "approve"

            };
            bool result = false;

            // act
            result = _donationManager.EditDonation(oldDonationItem, newDonationItem);

            // assert
            Assert.IsTrue(result);

        }

        /// <summary>
        /// Asaad Mohamed 
        /// Created: 2021/03/15
        /// test to delete donation 
        /// </summary>

        [TestMethod]
        public void TestDeleteDonation()
        {
            Donation donation = new Donation();
            // arrange
            const int expectedCount = 1;
            int actualCount;

            // act
            actualCount = _fakeDonationAccessor.DeleteDonation(donation.DonationID);

            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Asaad Mohamed 
        /// Created: 2021/03/24
        /// test to insert new Donation to the List 
        /// </summary>

        [TestMethod]
        public void TestInsertDonationBoolGood()
        {
            // Arrange
            DonationManager donationManager = new DonationManager(_fakeDonationAccessor);
            //const int expectedResult = 0;
            //int actualResult;
            Donation donation = (new Donation()
            {
                DonationID = 1,
                DonorID = 1,
                NameOfItem = "Table",
                Description = "Office table",
                EstValue = 200,
                AgeofItem = 2,
                DropOff = true,
                PickUp = false,
                PickUpDateTime = new DateTime(2020, 05, 14),
                MailReceipt = false,
                EmailReceipt = true,
                OrderQty = 3,
                DonationStatus = "Submited",
            });
            bool expectedResult = true;
            bool actualResult;
            // Act
             actualResult = donationManager.InsertDonation(donation);

            // Assert
             Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void SelectDonationByDonationIdGood()
        {
            // Arrange
            int expectedResult = 2;
            int actualResult;

            // Act
            Donation donation = _donationManager.RetrieveDonationByDonationID(2);
            actualResult = donation.DonationID;

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void SelectImageByDonationIdGood()
        {
            // Arrange
            int expectedResult = 50;
            int actualResult;
            int donationId = 2;

            // Act
            byte[] rawData = _donationManager.RetrieveDonationImageByDonationID(donationId);
            actualResult = rawData.Length;

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
