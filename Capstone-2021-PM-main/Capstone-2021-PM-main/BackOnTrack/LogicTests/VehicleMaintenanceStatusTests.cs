using DataAccessFakes;
using DataAccessInterfaces;
using DomainModels;
using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicTests
{
    /// <summary>
    /// Zach Stultz
    /// Created: 2021/03/19
    ///
    /// The Vehicle Maintenance Status Tests.
    /// </summary>
    [TestClass]
    public class VehicleMaintenanceStatusTests
    {
        private IVehicleMaintenanceStatusAccessor _vehicleMaintenanceStatusAccessor;
        private VehicleMaintenanceStatusManager _vehicleMaintenanceStatusManager;

        private List<VehicleMaintenanceStatus> _vehicleMaintenanceStatuses;
        private List<VehicleMaintenanceStatusType> _vehicleMaintenanceStatusTypes;

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/19
        ///
        /// Constructor sets up our accessors for data access dependency.
        /// <summary>
        [TestInitialize]
        public void TestSetup()
        {
            _vehicleMaintenanceStatusAccessor = new VehicleMaintenanceStatusFake();
            _vehicleMaintenanceStatusManager = new VehicleMaintenanceStatusManager(_vehicleMaintenanceStatusAccessor);
        }


        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/19
        ///
        /// Tests the insert vehicle maintenance status for returns bool good.
        /// </summary>
        [TestMethod]
        public void TestAddVehicleMaintenanceStatus_AddedMaintenanceStatus_ReturnsBoolGood()
        {
            // Arrange
            bool expectedResult = true;
            bool actualResult = false;

            VehicleMaintenanceStatus vehicleMaintenanceStatus = new VehicleMaintenanceStatus
            {
                StatusID = 100004,
                VinNumber = "WBA3C1C5XEK193803",
                MaintenanceStatusType = "Active",
                HasMaintenanceReport = true,
                IdentifiedMaintenance = "Tire Rotation"
            };
            // Act
            actualResult = _vehicleMaintenanceStatusAccessor.InsertVehicleMaintenanceStatus(vehicleMaintenanceStatus);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/19
        ///
        /// Tests the insert vehicle maintenance status for returns bool bad.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestAddVehicleMaintenanceStatus_AddedMaintenanceStatus_ReturnsBoolBad()
        {
            // Arrange
            bool expectedResult = true;
            bool actualResult = false;

            VehicleMaintenanceStatus vehicleMaintenanceStatus = new VehicleMaintenanceStatus
            {
                StatusID = 100000,
                VinNumber = "WBA3C1C5XEK193803",
                MaintenanceStatusType = "Active",
                HasMaintenanceReport = true,
                IdentifiedMaintenance = "Tire Rotation"
            };
            // Act
            actualResult = _vehicleMaintenanceStatusAccessor.InsertVehicleMaintenanceStatus(vehicleMaintenanceStatus);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/19
        ///
        /// Tests the insert vehicle maintenance status type for returns bool good.
        /// </summary>
        [TestMethod]
        public void TestAddVehicleMaintenanceStatusType_AddedMaintenanceStatus_ReturnsBoolGood()
        {
            // Arrange
            bool expectedResult = true;
            bool actualResult = false;

            VehicleMaintenanceStatusType vehicleMaintenanceStatusType = new VehicleMaintenanceStatusType
            {
                MaintenanceStatusType = "Test",
                MaintenanceStatusDescription = "test"
            };
            // Act
            actualResult = _vehicleMaintenanceStatusAccessor.InsertVehicleMaintenanceStatusType(vehicleMaintenanceStatusType);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/19
        ///
        /// Tests the insert vehicle maintenance status type for returns bool bad.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestAddVehicleMaintenanceStatusType_AddedMaintenanceStatus_ReturnsBoolBad()
        {
            // Arrange
            bool expectedResult = true;
            bool actualResult = false;

            VehicleMaintenanceStatusType vehicleMaintenanceStatusType = new VehicleMaintenanceStatusType
            {
                MaintenanceStatusType = "Active", // a duplicate that already exists in the DB.
                MaintenanceStatusDescription = "An active maintenance."
            };
            // Act
            actualResult = _vehicleMaintenanceStatusAccessor.InsertVehicleMaintenanceStatusType(vehicleMaintenanceStatusType);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }


        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/25
        ///
        /// Tests the vehicle maintenance status manager returns maintenance status list.
        /// </summary>
        [TestMethod]
        public void TestVehicleMaintenanceStatusManager_ReturnsMaintenanceStatusList()
        {
            // arrange
            VehicleMaintenanceStatusManager vehicleMaintenanceStatusTypeManager =
                new VehicleMaintenanceStatusManager(_vehicleMaintenanceStatusAccessor);
            const int expectedDataObjectCount = 4;
            int actualDataObjectCount;

            // act
            _vehicleMaintenanceStatuses = _vehicleMaintenanceStatusAccessor.SelectAllVehicleMaintenanceStatuses();
            actualDataObjectCount = _vehicleMaintenanceStatuses.Count;

            // assert
            Assert.AreEqual(expectedDataObjectCount, actualDataObjectCount);
        }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/25
        ///
        /// Tests the vehicle maintenance status manager returns vehicle maintenance status type list.
        /// </summary>
        [TestMethod]
        public void TestVehicleMaintenanceStatusManager_ReturnsVehicleMaintenanceStatusTypeList()
        {
            // arrange
            VehicleMaintenanceStatusManager vehicleMaintenanceStatusTypeManager =
                new VehicleMaintenanceStatusManager(_vehicleMaintenanceStatusAccessor);
            const int expectedDataObjectCount = 9;
            int actualDataObjectCount;

            // act
            _vehicleMaintenanceStatusTypes = _vehicleMaintenanceStatusAccessor.SelectAllVehicleMaintenanceStatusTypes();
            actualDataObjectCount = _vehicleMaintenanceStatusTypes.Count;

            // assert
            Assert.AreEqual(expectedDataObjectCount, actualDataObjectCount);
        }
    }
}
