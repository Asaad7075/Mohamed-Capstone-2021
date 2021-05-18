using DataAccessFakes;
using DataAccessInterfaces;
using DomainModels;
using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace LogicTests
{
    /// <summary>
    /// Summary description for VehicleManagerTests
    /// </summary>
    [TestClass]
    public class VehicleManagerTests
    {
        private IVehicleAccessor _vehicleAccessor;

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/02/10
        /// 
        /// Set up accessor for data access dependency.
        /// </summary>
        [TestInitialize]
        public void TestSetup()
        {
            _vehicleAccessor = new VehicleFake();
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/02/10
        /// 
        /// Tests for deleting an already existing vehicle.
        /// </summary>
        [TestMethod]
        public void TestDeleteVehicleGood()
        {
            // Arrange
            const bool expectedResult = true;
            bool actualResult;
            List<Vehicle> vehicles = _vehicleAccessor.SelectAllVehicles();

            // Act
            actualResult = _vehicleAccessor.DeleteVehicle(vehicles[0]);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);

        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/02/12
        /// 
        /// Tests for when the user attempts to delete a vehicle
        /// that does not exist in the database.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestDeleteVehicleDoesNotExistBad()
        {
            // Arrange
            bool actualResult;
            List<Vehicle> vehicles = _vehicleAccessor.SelectAllVehicles();
            Vehicle vehicle = vehicles[0];
            // Act & Assert
            for (int i = 0; i < 2; i++)
            {
                actualResult = _vehicleAccessor.DeleteVehicle(vehicle);
            }
        }

        /// <summary>
        /// Nate Hepker
        /// Created:2021/02/12
        /// 
        /// Tests for a valid AddVehicle.
        /// </summary>
        [TestMethod]
        public void TestAddVehicleGood()
        {
            // arrange
            VehicleManager vehicleManager = new VehicleManager(_vehicleAccessor);
            Vehicle vehicle = (new Vehicle()
            {
                VinNumber = "ThisIsAFakeVIN000",
                LicensePlateNumber = "FakPlat",
                VehicleMake = "FakeMake",
                VehicleModel = "FakeModel",
                VehicleYear = 1111,
                LicenseClass = "C",
                Mileage = 40000,
                ADACompliant = false,
                GeoID = 10000
            });

            bool expectedResult = true;
            bool actualResult;

            // act
            actualResult = vehicleManager.AddVehicle(vehicle);
            //assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// Nate Hepker
        /// Created:2021/02/14
        /// 
        /// Tests for adding an existing vehicle.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestAddVehicleAlreadyExists()
        {
            // arrange
            VehicleManager vehicleManager = new VehicleManager(_vehicleAccessor);
            Vehicle vehicle = (new Vehicle()
            {
                VinNumber = "ThisIsAFakeVIN000",
                LicensePlateNumber = "FakPlat",
                VehicleMake = "FakeMake",
                VehicleModel = "FakeModel",
                VehicleYear = 1111,
                LicenseClass = "C",
                Mileage = 40000,
                ADACompliant = false,
                GeoID = 10000
            });
            bool actualResult = true;

            // act
            for (int i = 0; i < 2; i++)
            {
                actualResult = vehicleManager.AddVehicle(vehicle);
            }
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/02
        /// 
        /// Tests for updating an already existing vehicle.
        /// </summary>
        [TestMethod]
        public void UpdateVehicleThroughVMByVinGood()
        {
            // Arrange
            const bool expectedResult = true;
            bool actualResult;

            // Act
            actualResult = _vehicleAccessor.UpdateVehicleThroughVMByVin("5SX43TNWCO0Q4Y2N6",
                    "PZALUVR", "2500");

            // Assert
            Assert.AreEqual(expectedResult, actualResult);

        }


        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/02
        /// 
        /// Tests for updating an already existing vehicle.
        /// </summary>
        [TestMethod]
        public void UpdateVehicleThroughVMByVinBad()
        {
            // Arrange
            const bool expectedResult = false;
            bool actualResult;

            // Act
            actualResult = _vehicleAccessor.UpdateVehicleThroughVMByVin("5SX43TNWCO",
                    "PZALUVR", "2500");

            // Assert
            Assert.AreEqual(expectedResult, actualResult);

        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/02
        /// 
        /// Tests for selecting all vehicles vm.
        /// </summary>
        [TestMethod]
        public void SelectAllVehicleVMsGood()
        {
            // Arrange
            const int expectedResult = 2;
            int actualResult;

            // Act
            ObservableCollection<VehicleVM> rawResult = _vehicleAccessor.SelectAllVehiclesVMs();
            actualResult = rawResult.Count;

            // Assert
            Assert.AreEqual(expectedResult, actualResult);

        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/02
        /// 
        /// Tests for deleting a vehicle vm.
        /// </summary>
        [TestMethod]
        public void DeleteVehicleVMGood()
        {
            // Arrange
            const bool expectedResult = true;
            bool actualResult;

            // Act
            ObservableCollection<VehicleVM> rawResult = _vehicleAccessor.SelectAllVehiclesVMs();
            VehicleVM actVehicle = rawResult[0];
            actualResult = _vehicleAccessor.DeleteVehicleThroughVM(actVehicle);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);

        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/02
        /// 
        /// Tests for deleting a vehicle vm.
        /// </summary>
        [TestMethod]
        public void DeleteVehicleVMBad()
        {
            // Arrange
            const bool expectedResult = false;
            bool actualResult;

            // Act
            ObservableCollection<VehicleVM> rawResult = _vehicleAccessor.SelectAllVehiclesVMs();
            actualResult = _vehicleAccessor.DeleteVehicleThroughVM(null);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);

        }

    }
}
