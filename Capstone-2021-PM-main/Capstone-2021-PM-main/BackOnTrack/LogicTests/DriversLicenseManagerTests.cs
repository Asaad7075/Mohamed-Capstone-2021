using DataAccessFakes;
using DataAccessInterfaces;
using DomainModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace LogicTests
{
    /// <summary>
    /// Chantal Shirley
    /// Created: 2021/02/02
    /// 
    /// Drivers license test class for all public methods associated with manipulating
    /// drivers license data.
    /// </summary>
    [TestClass]
    public class DriversLicenseManagerTests
    {
        private IDriversLicenseAccessor _driversLicenseAccessor;

        /// <summary>
        /// Chantal Shirley 
        /// Created: 2021/02/02
        /// 
        /// Setup accessor for data access dependency.
        /// </summary>
        [TestInitialize]
        public void TestSetup()
        {
            _driversLicenseAccessor = new DriversLicenseFake();
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/02/02
        /// 
        /// Test for adding a drivers license, when one does not exist.
        /// </summary>
        [TestMethod]
        public void TestInsertDriversLicenseReturnsBoolGood()
        {
            // Arrange
            const bool expectedResult = true;
            bool actualResult;
            DriversLicense driversLicense = new DriversLicense
            {
                LicenseNumber = "IA3209432734",
                EmployeeID = 34892347,
                LicenseType = "C",
                LicenseIssuedDate = new DateTime(2020, 05, 14),
                LicenseExpiryDate = new DateTime(2032, 11, 06),
                IsActive = true
            };

            // Act
            actualResult = _driversLicenseAccessor.InsertDriversLicense(driversLicense);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }


        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/02/09
        /// 
        /// Tests for error throw when user attempts to add 
        /// an already existing drivers license.
        /// </summary>
        [TestMethod]
        public void TestInsertDriversLicenseReturnsBoolBad()
        {
            // Arrange
            const bool expectedResult = true;
            bool actualResult;
            DriversLicense driversLicense = new DriversLicense
            {
                LicenseNumber = "IA3209432734",
                EmployeeID = 34892347,
                LicenseType = "C",
                LicenseIssuedDate = new DateTime(2020, 05, 14),
                LicenseExpiryDate = new DateTime(2032, 11, 06),
                IsActive = true
            };

            // Act
            actualResult = _driversLicenseAccessor.InsertDriversLicense(driversLicense);


        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/02
        /// 
        /// Tests for selecting drivers license by license number.
        /// </summary>
        [TestMethod]
        public void TestSelectDriversLicenseByLicenseNumberGood()
        {
            // Arrange
            const bool expectedResult = true;
            bool actualResult;

            // Act
            actualResult = _driversLicenseAccessor.SelectDriversLicenseByLicenseNumber("IA834343424");

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/02
        /// 
        /// Tests for selecting drivers license by license number.
        /// </summary>
        [TestMethod]
        public void TestSelectDriversLicenseByLicenseNumberBad()
        {
            // Arrange
            const bool expectedResult = false;
            bool actualResult;

            // Act
            actualResult = _driversLicenseAccessor.SelectDriversLicenseByLicenseNumber("gfdngj");

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/02
        /// 
        /// Tests for selecting drivers license types.
        /// </summary>
        [TestMethod]
        public void TestSelectDriversLicenseTypesGood()
        {
            // Arrange
            const int expectedResult = 4;
            int actualResult;

            // Act
            List<string> rawResult = _driversLicenseAccessor.SelectDriversLicenseTypes();
            actualResult = rawResult.Count;
            
            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

    }
}
