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
    /// Created: 2021/03/01
    /// 
    /// This donor  test class for Retrieve All donor List method 
    /// 
    /// </summary>
    [TestClass]
    public class DonorManagerTestcs
    {
        private IDonorAccessor _fakeDonorAccessor;
        private DonorManager _donorManager;

        /// <summary>
        /// Asaad Mohamed 
        /// Created: 2021/03/01
        /// 
        /// Setup accessor for data access dependency.
        /// </summary>

        [TestInitialize]
        public void TestSetup()
        {
            _fakeDonorAccessor = new DonorsFake();

            _donorManager = new DonorManager(_fakeDonorAccessor);
        }
        /// <summary>
        /// Asaad Mohamed 
        /// Created: 2021/03/01
        /// Test to Retrieve All donor List method
        /// </summary>

        [TestMethod]
        public void TestRetrieveAllDonorList()
        {
            // Arrange 
            List<Donor> RetrieveAllDonorList;


            //Act

            RetrieveAllDonorList = _donorManager.RetrieveAllDonorListByActive();



            // Assert
            Assert.AreEqual(2, RetrieveAllDonorList.Count);
        }

        [TestMethod]
        public void TestInsertDonorGood()
        {
            // Arrange
            DonorManager donorManager = new DonorManager(_fakeDonorAccessor);
            
            Donor donor = (new Donor()
            {
                DonorID = 1,
                Business = true,
                Individual = false,
                BusinessName = "WellGreen",
                FirstName = "Harrbi",
                LastName = "Noah",
                MiddleInitial = "A",
                Address = "733 kirkwood",
                ZipCode = "52404",
                PhoneNumber ="5274513243",
                Email = "Harrbi@gmail.com",
                SS = "10836474747",
                EIN = "63536474747",
                Active = true,
            });
            bool expectedResult = true;
            bool actualResult;
            // Act
            actualResult = donorManager.InsertDonor(donor);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }


        /// <summary>
        /// Asaad Mohamed 
        /// Created: 2021/04/01
        /// test to update donor  List method
        /// </summary>

        [TestMethod]
        public void TestEditDonor()
        {
            // arrange
            Donor oldDonor = new Donor()
            {

                DonorID = 2,
                Business = true,
                Individual = false,
                BusinessName = "Wellgreen",
                FirstName = "Alli",
                LastName = "Musab",
                MiddleInitial = "A",
                Address = "77 Th ave ",
                ZipCode = "657374",
                PhoneNumber = "7463834453",
                Email = "ali@gmail.com",
                SS = "54664566465",
                EIN = "6746466747"


            };
            Donor newDonor = new Donor()
            {

                DonorID = 4,
                Business = false,
                Individual = true,
                BusinessName = "Wallmart",
                FirstName = "Mark",
                LastName = "Sam",
                MiddleInitial = "A",
                Address = "32 Blv SW ",
                ZipCode = "36472",
                PhoneNumber = "2357462643",
                Email = "mark@gmail.com",
                SS = "54657563774",
                EIN = "2123233443"

            };
            bool result = false;

            // act
            result = _donorManager.EditDonor(oldDonor, newDonor);

            // assert
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Asaad Mohamed
        /// created: 2021/04/09
        /// </summary>
        [TestMethod]
        public void TestSelectDonorByID()
        {
            // Arrange
            Donor donor;
            string expected = "Emma";
            int id = 1;

            // Act
            donor = _fakeDonorAccessor.SelectDonorById(id);

            //Assert
            Assert.AreEqual(expected, donor.FirstName);
        }

        /// <summary>
        /// Asaad Mohamed 
        /// Created: 2021/03/15
        /// test to delete donor 
        /// </summary>

        [TestMethod]
        public void TestDeleteDonor()
        {
            Donor donor = new Donor();
            // arrange
            const int expectedCount = 1;
            int actualCount;

            // act
            actualCount = _fakeDonorAccessor.DeleteDonor(donor.DonorID);

            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod]
        public void SelectDonorByDonorEmailGood()
        {
            // Arrange
            bool expectedResult = true;
            bool actualResult;
            string email = "emma@gmail.com";

            // Act
            actualResult = _fakeDonorAccessor.SelectDonorByEmail(email);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/30
        /// 
        /// Reused donor data from Asaad's previous
        /// method.
        /// </summary>
        [TestMethod]
        public void InsertDonorFromWebGood()
        {
            // Arrange
            bool expectedResult = true;
            bool actualResult;
            Donor donor= new Donor
            {
                DonorID = 4,
                Business = false,
                Individual = true,
                BusinessName = "Wal-Mart",
                FirstName = "Mark",
                LastName = "Sam",
                MiddleInitial = "A",
                Address = "32 Blv SW ",
                ZipCode = "36472",
                PhoneNumber = "2357462643",
                Email = "mark@gmail.com",
                SS = "54657563774",
                EIN = "2123233443"

            };
            string passwordHash = "helloworld".hashSHA256().ToUpper();

            // Act
            actualResult = _fakeDonorAccessor.InsertDonorFromWeb(donor, passwordHash);

            // Assert
            Assert.AreEqual(actualResult, expectedResult);

        }

        [TestMethod]
        public void SelectDonorByEmailAndPasswordGood()
        {
            // Arrange
            bool expectedResult = true;
            bool actualResult;
            Donor donor = new Donor
            {
                DonorID = 4,
                Business = false,
                Individual = true,
                BusinessName = "Wal-Mart",
                FirstName = "Mark",
                LastName = "Sam",
                MiddleInitial = "A",
                Address = "32 Blv SW ",
                ZipCode = "36472",
                PhoneNumber = "2357462643",
                Email = "mark@gmail.com",
                SS = "54657563774",
                EIN = "2123233443"
            };
            string passwordHash = "helloworld".hashSHA256().ToUpper();
            bool testResult = _fakeDonorAccessor.InsertDonorFromWeb(donor, passwordHash);

            // Act
            actualResult = _fakeDonorAccessor.SelectDonorByEmailAndPassword(donor.Email, passwordHash);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
