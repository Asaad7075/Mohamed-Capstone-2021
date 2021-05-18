using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using DataAccessFakes;
using DataAccessInterfaces;
using DomainModels;
using LogicLayer;

namespace LogicTests
{
    /// <summary>
    /// Zach Stultz
    /// Created: 2021/02/12
    ///
    /// Unit Tests for VehicleMaintenanceReportManagerTests
    /// <summary>
    [TestClass]
    public class VehicleMaintenanceReportManagerTests
    {
        private IVehicleMaintenanceReportAccessor _vehicleMaintenanceReportAccessor;
        private VehicleMaintenanceReportManager _vehicleMaintenanceReportManager;

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/19
        ///
        /// Constructor sets up our accessors for data access dependency.
        /// <summary>
        [TestInitialize]
        public void TestSetup()
        {
            _vehicleMaintenanceReportAccessor = new VehicleMaintenanceReportFake();
            _vehicleMaintenanceReportManager = new VehicleMaintenanceReportManager(_vehicleMaintenanceReportAccessor);
        }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/19
        ///
        /// Helper method that helps avoid repeating logic and save space.
        /// </summary>
        /// <returns>A list of VehicleMaintenanceReportVMS.</returns>
        public VehicleMaintenanceReportVM ManufactureVehicleMaintenanceReport(int vehicleMaintenanceReportID, string vinNumber, string vehicleMake,
                string vehicleModel, string licensePlate, string maintenanceType, DateTime maintenanceServiceDate, bool maintenanceFinished,
                string maintenanceNotes, bool isQueued)
        {
            VehicleMaintenanceReportVM result = new VehicleMaintenanceReportVM();
            result.VehicleMaintenanceReportID = vehicleMaintenanceReportID;
            result.VinNumber = vinNumber;
            result.VehicleMake = vehicleMake;
            result.VehicleModel = vehicleModel;
            result.LicensePlate = licensePlate;
            result.MaintenanceType = maintenanceType;
            result.MaintenanceServiceDate = maintenanceServiceDate;
            result.MaintenanceFinished = maintenanceFinished;
            result.MaintenanceNotes = maintenanceNotes;
            result.IsQueued = isQueued;
            return result;
        }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/19
        ///
        /// Test for adding a vehicle maintenance report, when one does not exist.
        /// </summary>
        [TestMethod]
        public void TestAddVehicleMaintenanceReport_AddedVehicleMaintenanceReport_ReturnsBoolGood()
        {
            // Arrange
            bool expectedResult = true;
            bool actualResult = false;
            VehicleMaintenanceReportVM vehicleMaintenanceReportVMGood = new VehicleMaintenanceReportVM
            {
                VehicleMaintenanceReportID = 100000,
                VinNumber = "JNKCV51F14M797775",
                VehicleMake = "Honda",
                VehicleModel = "Cruz",
                LicensePlate = "2CTTN1",
                MaintenanceType = "AIR FILTER",
                MaintenanceServiceDate = new DateTime(2020, 02, 05),
                MaintenanceFinished = true,
                MaintenanceNotes = "Changed and clean.",
                IsQueued = false,
            };
            // Act
            actualResult = _vehicleMaintenanceReportAccessor.InsertVehicleMaintenanceReport(vehicleMaintenanceReportVMGood);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/19
        ///
        /// Test for a duplicate entry when user tries to add an existing item.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestAddVehicleMaintenanceReport_AddedDriversLicense_ReturnsBoolBad()
        {

            // Arrange
            bool actualResult;
            VehicleMaintenanceReportVM vehicleMaintenanceReportVMBad = new VehicleMaintenanceReportVM
            {
                VehicleMaintenanceReportID = 100001,
                VinNumber = "JNKCV51F14M797774",
                VehicleMake = "Honda",
                VehicleModel = "Cruz",
                LicensePlate = "2CTTN6",
                MaintenanceType = "AIR FILTER",
                MaintenanceServiceDate = new DateTime(2020, 02, 05),
                MaintenanceFinished = true,
                MaintenanceNotes = "Changed and clean.",
                IsQueued = false,
            };

            // Act & Assert
            for (int i = 0; i < 2; i++)
            {
                actualResult = _vehicleMaintenanceReportAccessor.InsertVehicleMaintenanceReport(vehicleMaintenanceReportVMBad);
            }
        }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/19
        ///
        /// Test the select all active maintenance reports for bool good result.
        /// </summary>
        [TestMethod]
        public void TestRetrieveAllActiveMaintenanceReports_RetrievedAllMaintenanceReportByActive_ReturnsBoolGood()
        {
            int expectedResult = 2;
            int actualResult = 0;
            List<VehicleMaintenanceReportVM> vehicleMaintenanceReports = _vehicleMaintenanceReportAccessor.SelectAllActiveMaintenanceReports();
            int total = vehicleMaintenanceReports.Count;

            foreach (VehicleMaintenanceReport item in vehicleMaintenanceReports)
            {
                if (item.IsQueued.Equals(true))
                {
                    actualResult++;
                }
            }
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/19
        ///
        /// Tests the select all active maintenance reports for bool bad result.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestRetrieveAllActiveMaintenanceReports_RetrievedAllMaintenanceReportByActive_ReturnsBoolBad()
        {
            int expectedResult = 4;
            int actualResult = 0;
            List<VehicleMaintenanceReportVM> vehicleMaintenanceReports = _vehicleMaintenanceReportAccessor.SelectAllActiveMaintenanceReports();
            int total = vehicleMaintenanceReports.Count;

            foreach (VehicleMaintenanceReport item in vehicleMaintenanceReports)
            {
                if (item.IsQueued.Equals(true))
                {
                    actualResult++;
                }
            }
            if(expectedResult != actualResult)
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/19
        ///
        /// Tests the select all vehicle maintenance report by Vin for returns bool good.
        /// </summary>
        [TestMethod]
        public void TestRetrieveAllVehicleMaintenanceReportByVin_RetrievedAllMaintenanceReportByVin_ReturnsBoolGood()
        {
            VehicleMaintenanceReportVM vehicleMaintenanceReportVMGood = new VehicleMaintenanceReportVM
            {
                VehicleMaintenanceReportID = 100000,
                VinNumber = "JNDF51F33M7500LC",
                VehicleMake = "Honda",
                VehicleModel = "Cruz",
                LicensePlate = "2CTT0D",
                MaintenanceType = "AIR FILTER",
                MaintenanceServiceDate = new DateTime(2020, 02, 05),
                MaintenanceFinished = true,
                MaintenanceNotes = "Changed and clean.",
                IsQueued = false,
            };
            _vehicleMaintenanceReportAccessor.InsertVehicleMaintenanceReport(vehicleMaintenanceReportVMGood);

            bool expectedResult = false;
            List<VehicleMaintenanceReportVM> vehicleMaintenanceReports = _vehicleMaintenanceReportAccessor.SelectVehicleMaintenanceReportByVin("JNKCV51F14M790000");
            bool actualResult = true;
            if (vehicleMaintenanceReports.Count == 0)
            {
                actualResult = false;
            }
            else
            {
                for (int i = 0; i < vehicleMaintenanceReports.Count; i++)
                {
                    if (vehicleMaintenanceReports[i].VinNumber.Equals(expectedResult))
                    {
                        actualResult = true;
                    }
                }
            }
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/19
        ///
        /// Tests the select all vehicle maintenance report by Vin for returns bool bad.
        /// </summary>
        [TestMethod]
        public void TestRetrieveAllVehicleMaintenanceReportByVin_RetrievedAllMaintenanceReportByVin_ReturnsBoolBad()
        {
            VehicleMaintenanceReportVM vehicleMaintenanceReportVMGood = new VehicleMaintenanceReportVM
            {
                VehicleMaintenanceReportID = 100000,
                VinNumber = "JNDF51F33M750007",
                VehicleMake = "Honda",
                VehicleModel = "Cruz",
                LicensePlate = "2CTT99",
                MaintenanceType = "AIR FILTER",
                MaintenanceServiceDate = new DateTime(2020, 02, 05),
                MaintenanceFinished = true,
                MaintenanceNotes = "Changed and clean.",
                IsQueued = false,
            };
            _vehicleMaintenanceReportAccessor.InsertVehicleMaintenanceReport(vehicleMaintenanceReportVMGood);

            bool expectedResult = false;
            List<VehicleMaintenanceReportVM> vehicleMaintenanceReports = _vehicleMaintenanceReportAccessor.SelectVehicleMaintenanceReportByVin("JNKCV51F14M790000");
            bool actualResult = true;
            if(vehicleMaintenanceReports.Count == 0)
            {
                actualResult = false;
            } else
            {
                for (int i = 0; i < vehicleMaintenanceReports.Count; i++)
                {
                    if (vehicleMaintenanceReports[i].VinNumber.Equals(expectedResult))
                    {
                        actualResult = true;
                    }
                }
            }
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/19
        ///
        /// Tests the edit vehicle maintenance report and returns bool good.
        /// </summary>
        [TestMethod]
        public void TestEditVehicleMaintenanceReport_EditMaintenanceReport_ReturnsBoolGood()
        {
            VehicleMaintenanceReportVM oldVehicleMaintenanceReportVM = new VehicleMaintenanceReportVM()
            {
                VehicleMaintenanceReportID = 100000,
                VinNumber = "JNDF51F33M750007",
                VehicleMake = "Honda",
                VehicleModel = "Cruz",
                LicensePlate = "2CTT99",
                MaintenanceType = "AIR FILTER",
                MaintenanceServiceDate = new DateTime(2020, 02, 05),
                MaintenanceFinished = true,
                MaintenanceNotes = "Changed and clean.",
                IsQueued = false,
            };

            VehicleMaintenanceReportVM newVehicleMaintenanceReportVM = new VehicleMaintenanceReportVM()
            {
                VehicleMaintenanceReportID = 100000,
                VinNumber = "3GNDA23P47S645148",
                VehicleMake = "Chevy",
                VehicleModel = "Cruz",
                LicensePlate = "2CTT99",
                MaintenanceType = "Tires",
                MaintenanceServiceDate = new DateTime(2020, 02, 06),
                MaintenanceFinished = true,
                MaintenanceNotes = "Rotated..",
                IsQueued = false,
            };

            bool result = false;

            // act
            result = _vehicleMaintenanceReportManager.EditVehicleMaintenanceReport(oldVehicleMaintenanceReportVM, newVehicleMaintenanceReportVM);

            // assert
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/19
        ///
        /// Tests the edit vehicle maintenance report and returns bool bad.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestEditVehicleMaintenanceReport_EditedMaintenanceReport_ReturnsBoolBad()
        {
            VehicleMaintenanceReportVM oldVehicleMaintenanceReportVM = new VehicleMaintenanceReportVM()
            {
                VehicleMaintenanceReportID = 100004,
                VinNumber = "JNDF51F33M750007",
                VehicleMake = "Honda",
                VehicleModel = "Cruz",
                LicensePlate = "2CTT99",
                MaintenanceType = "AIR FILTER",
                MaintenanceServiceDate = new DateTime(2020, 02, 05),
                MaintenanceFinished = true,
                MaintenanceNotes = "Changed and clean.",
                IsQueued = false,
            };

            VehicleMaintenanceReportVM newVehicleMaintenanceReportVM = new VehicleMaintenanceReportVM()
            {
                VehicleMaintenanceReportID = 100000,
                VinNumber = "3GNDA23P47S645555",
                VehicleMake = "Chevssy",
                VehicleModel = "Cruzss",
                LicensePlate = "2CTT88",
                MaintenanceType = "Tires",
                MaintenanceServiceDate = new DateTime(2020, 02, 06),
                MaintenanceFinished = true,
                MaintenanceNotes = "Rotated..",
                IsQueued = false,
            };

            bool result = false;

            // act
            result = _vehicleMaintenanceReportManager.EditVehicleMaintenanceReport(oldVehicleMaintenanceReportVM, newVehicleMaintenanceReportVM);

            // assert
            Assert.IsTrue(result);
        }
    }
}
