using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessFakes;
using DataAccessInterfaces;
using DomainModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogicLayer;
using LogicInterfaces;

namespace LogicTests
{
    /// <summary>
    /// Chase Martin
    /// Created: 2021/02/12
    /// 
    /// Zip code test class for all public methods associated with manipulating
    /// zip code data.
    /// </summary>
    [TestClass]
    public class ZipCodeManagerTests
    {
        private IZipCodeAccessor _zipCodeAccessor;

        /// <summary>
        /// Chase Martin
        /// Created: 2021/02/12
        /// 
        /// Setup method for accessor data access dependency.
        /// </summary>
        [TestInitialize]
        public void TestSetup()
        {
            _zipCodeAccessor = new ZipCodeFake();
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/02/12
        /// 
        /// Test for adding a zip code
        /// </summary>
        [TestMethod]
        public void TestInsertZipCodeReturnsGood()
        {
            // Arrange
            const bool expectedResult = true;
            bool actualResult;
            ZipCodeFile zipCode = new ZipCodeFile
            {
                ZipCode = "52314",
                City = "Solon",
                State = "IA",
                isServicable = true
            };

            // Act
            actualResult = _zipCodeAccessor.InsertZipCodeBool(zipCode);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/02/12
        /// 
        ///  Test for retrieving list of zip codes.
        /// </summary>
        [TestMethod]
        public void TestUserManagerRetrieveAllZipCodesReturnsList()
        {
            //arrange
            IZipCodeManager zipCodeManager = new ZipCodeManager(_zipCodeAccessor);
            int expectedValue = 3;

            //act
            int actualValue = zipCodeManager.RetrieveAllZipCodes().Count;

            //assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/02/13
        /// 
        /// Tests for error throw when user attempts to add 
        /// an already existing zip code.
        /// </summary>
        [TestMethod]
        public void TestInsertZipCodeReturnsBad()
        {

        }
    }
}
