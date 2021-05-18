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
    /// Created: 2021/02/04
    /// 
    /// This donor form approve  test class for Retrieve All Donation Form List method and 
    /// update donor form method
    /// 
    /// </summary>
    
    [TestClass]
    public class DonationFormManagerTest
    {
        private IDonationFormAccessor _fakeDonorFormAccessor;
        private DonationFormManager _donorFormManager;

        /// <summary>
        /// Asaad Mohamed 
        /// Created: 2021/02/04
        /// 
        /// Setup accessor for data access dependency.
        /// </summary>

        [TestInitialize]
        public void TestSetup()
        {
            _fakeDonorFormAccessor = new DonationFormFake();

            _donorFormManager = new DonationFormManager(_fakeDonorFormAccessor);
        }
        /// <summary>
        /// Asaad Mohamed 
        /// Created: 2021/02/04
        /// test to Retrieve All Donation Form List method
        /// </summary>
        [TestMethod]
        public void TestRetrieveAllDonorFormList()
        {
            // Arrange 
            List<DonationForm> RetrieveAllDonorFormList;


            //Act

            RetrieveAllDonorFormList = _donorFormManager.RetrieveAllDonorFormList();



            // Assert
            Assert.AreEqual(2, RetrieveAllDonorFormList.Count);
        }

        /// <summary>
        /// Asaad Mohamed 
        /// Created: 2021/02/04
        /// test to update Donation Form List method
        /// </summary>
        [TestMethod]
        public void TestEditDonorForm()
        {
            // arrange
            DonationForm oldDonorForm = new DonationForm()
            {
                DonorFormID = 1,
                DonorID = 2,
                DateCreated = DateTime.Now,
                Status = "pending"

            };
            DonationForm newDonorForm = new DonationForm()
            {
                DonorFormID = 4,
                DonorID = 4,
                DateCreated = DateTime.Now,
                Status = "approve"

            };
            bool result = false;

            // act
            result = _donorFormManager.EditDonorForm(oldDonorForm, newDonorForm);

            // assert
            Assert.IsTrue(result);

        }

        [TestMethod]
        public void TestSelectDonationFormByID()
        {
            // Arrange
            DonationForm donationForm;
            int expected = 1;
            int id = 1;

            // Act
            donationForm = _fakeDonorFormAccessor.SelectDonationFormById(id);

            //Assert
            Assert.AreEqual(expected, donationForm.DonorFormID);
        }


        /// <summary>
        /// Asaad Mohamed 
        /// Created: 2021/02/04
        /// test to insert new Donation Form List method
        /// </summary>

        [TestMethod]
        public void TestInsertCreateDonationFormBoolGood()
        {
            // Arrange
            const int expectedResult = 0;
            int actualResult;
            DonationForm donationForm = new DonationForm
            {
                DonorFormID = 1,
                DonorID = 2,
                DateCreated = new DateTime(2020, 05, 14),
                Status = "approve",

            };

            // Act
            actualResult = _fakeDonorFormAccessor.InsertDonationForm(donationForm);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
