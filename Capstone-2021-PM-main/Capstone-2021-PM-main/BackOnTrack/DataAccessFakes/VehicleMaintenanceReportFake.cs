using DataAccessInterfaces;
using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessFakes
{
    /// <summary>
    /// Zach Stultz
    /// Created: 2021/02/12
    ///
    /// Our Fakes Class for testing.
    /// <summary>
    public class VehicleMaintenanceReportFake : IVehicleMaintenanceReportAccessor
    {
        private List<VehicleMaintenanceReportVM> _vehicleMaintenanceReports;


        /// <summary>
        /// Zach Stultz
        /// Created: 2021/02/12
        ///
        /// Initializes a new instance of the <see cref="VehicleMaintenanceReportFake"/> class
        /// and creates some objects and adds them to a list.
        /// </summary>
        public VehicleMaintenanceReportFake()
        {
            _vehicleMaintenanceReports = new List<VehicleMaintenanceReportVM>();

            _vehicleMaintenanceReports.Add(new VehicleMaintenanceReportVM()
            {
                VehicleMaintenanceReportID = 100000,
                VinNumber = "JNKCV51F14M797774",
                VehicleMake = "Honda",
                VehicleModel = "Cruz",
                LicensePlate = "2CTTN6",
                MaintenanceType = "AIR FILTER",
                MaintenanceServiceDate = new DateTime(2020, 02, 05),
                MaintenanceFinished = true,
                MaintenanceNotes = "Changed and clean.",
                IsQueued = false,
            });
            _vehicleMaintenanceReports.Add(new VehicleMaintenanceReportVM()
            {
                VehicleMaintenanceReportID = 100001,
                VinNumber = "1N4AL21E79N422497",
                VehicleMake = "MAZDA",
                VehicleModel = "MX-5 MIATA",
                LicensePlate = "4RRGG3",
                MaintenanceType = "HEADLIGHTS, TURN SIGNALS, BRAKE, AND PARKING LIGHTS",
                MaintenanceServiceDate = new DateTime(2020, 02, 06),
                MaintenanceFinished = false,
                MaintenanceNotes = null,
                IsQueued = true,
            });
            _vehicleMaintenanceReports.Add(new VehicleMaintenanceReportVM()
            {
                VehicleMaintenanceReportID = 100002,
                VinNumber = "1GKDM15Z5LB524504",
                VehicleMake = "BMW",
                VehicleModel = "323I",
                LicensePlate = "4CLVY1",
                MaintenanceType = "OIL AND COOLANT LEVELS",
                MaintenanceServiceDate = new DateTime(2020, 02, 07),
                MaintenanceFinished = true,
                MaintenanceNotes = "Runs smooth.",
                IsQueued = false,
            });
            _vehicleMaintenanceReports.Add(new VehicleMaintenanceReportVM()
            {
                VehicleMaintenanceReportID = 100003,
                VinNumber = "2A4GP44R67R388209",
                VehicleMake = "MITSUBISHI FUSO",
                VehicleModel = "FG140",
                LicensePlate = "1LMUR1",
                MaintenanceType = "ROTATE TIRES",
                MaintenanceServiceDate = new DateTime(2020, 02, 08),
                MaintenanceFinished = false,
                MaintenanceNotes = "Three of the four tires have been rotated, one is in the process of being fully replaced.",
                IsQueued = true,
            });
        }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/02/12
        ///
        /// Inserts the vehicle maintenance report.
        /// </summary>
        /// <param name="vehicleMaintenanceReport">The vehicle maintenance report.</param>
        /// <returns>A bool.</returns>
        public bool InsertVehicleMaintenanceReport(VehicleMaintenanceReportVM vehicleMaintenanceReport)
        {
            bool result = false;
            bool duplicate = false;

            for (int i = 0; i < _vehicleMaintenanceReports.Count; i++)
            {
                if (
                    vehicleMaintenanceReport.VehicleMake == _vehicleMaintenanceReports[i].VehicleMake &&
                    vehicleMaintenanceReport.VehicleModel == _vehicleMaintenanceReports[i].VehicleModel &&
                    vehicleMaintenanceReport.LicensePlate == _vehicleMaintenanceReports[i].LicensePlate &&
                    vehicleMaintenanceReport.VehicleMaintenanceTypeName == _vehicleMaintenanceReports[i].VehicleMaintenanceTypeName &&
                    vehicleMaintenanceReport.VehicleMaintenanceServiceDate == _vehicleMaintenanceReports[i].VehicleMaintenanceServiceDate &&
                    vehicleMaintenanceReport.MaintenanceFinished == _vehicleMaintenanceReports[i].MaintenanceFinished &&
                    vehicleMaintenanceReport.VehicleMaintenanceNotes == _vehicleMaintenanceReports[i].VehicleMaintenanceNotes)
                {
                    duplicate = true;
                }
            }
            if (duplicate.Equals(true))
            {
                throw new Exception("Vehicle Maintenance Report already exists in the database.");
            }
            else
            {
                _vehicleMaintenanceReports.Add(vehicleMaintenanceReport);
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/02/12
        ///
        /// Selects the all active maintenance reports.
        /// </summary>
        /// <returns>A list of VehicleMaintenanceReports.</returns>
        public List<VehicleMaintenanceReportVM> SelectAllActiveMaintenanceReports()
        {
            List<VehicleMaintenanceReportVM> activeVehicleMaintenanceReports = new List<VehicleMaintenanceReportVM>();
            foreach (VehicleMaintenanceReportVM item in _vehicleMaintenanceReports)
            {
                if (item.IsQueued.Equals(true))
                {
                    activeVehicleMaintenanceReports.Add(item);
                }
            }
            return activeVehicleMaintenanceReports;
        }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/19
        ///
        /// Selects all the maintenance reports.
        /// </summary>
        /// <returns>A list of VehicleMaintenanceReports.</returns>
        public List<VehicleMaintenanceReportVM> SelectAllVehicleMaintenanceReports()
        {
            return _vehicleMaintenanceReports;
        }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/02/12
        ///
        /// Selects the vehicle maintenance report by Vin.
        /// </summary>
        /// <param name="VinNumber">The Vin number.</param>
        /// <returns>A VehicleMaintenanceReport.</returns>
        public List<VehicleMaintenanceReportVM> SelectVehicleMaintenanceReportByVin(string VinNumber)
        {
            List<VehicleMaintenanceReportVM> result = new List<VehicleMaintenanceReportVM>();
            foreach (VehicleMaintenanceReportVM item in _vehicleMaintenanceReports)
            {
                if (item.VinNumber.Equals(VinNumber))
                {
                    result.Add(item);
                }
            }
            return result;
        }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/02/12
        ///
        /// Updates the vehicle maintenance report.
        /// </summary>
        /// <param name="vehicleMaintenanceReport">The vehicle maintenance report.</param>
        /// <returns>An int.</returns>
        public int UpdateVehicleMaintenanceReport(VehicleMaintenanceReportVM oldVehicleMaintenanceReport, VehicleMaintenanceReportVM newVehicleMaintenanceReport)
        {
            int result = 0;
            if (oldVehicleMaintenanceReport.VehicleMaintenanceReportID == newVehicleMaintenanceReport.VehicleMaintenanceReportID)
            {
                try
                {
                    foreach (var v in _vehicleMaintenanceReports)
                    {
                        if (oldVehicleMaintenanceReport.VehicleMaintenanceReportID == newVehicleMaintenanceReport.VehicleMaintenanceReportID)
                        {
                            oldVehicleMaintenanceReport.VinNumber = newVehicleMaintenanceReport.VinNumber;
                            oldVehicleMaintenanceReport.VehicleMake = newVehicleMaintenanceReport.VehicleMake;
                            oldVehicleMaintenanceReport.VehicleModel = newVehicleMaintenanceReport.VehicleModel;
                            oldVehicleMaintenanceReport.LicensePlate = newVehicleMaintenanceReport.LicensePlate;
                            oldVehicleMaintenanceReport.MaintenanceType = newVehicleMaintenanceReport.MaintenanceType;
                            oldVehicleMaintenanceReport.MaintenanceServiceDate = newVehicleMaintenanceReport.MaintenanceServiceDate;
                            oldVehicleMaintenanceReport.MaintenanceFinished = newVehicleMaintenanceReport.MaintenanceFinished;
                            oldVehicleMaintenanceReport.MaintenanceNotes = newVehicleMaintenanceReport.MaintenanceNotes;
                            oldVehicleMaintenanceReport.IsQueued = newVehicleMaintenanceReport.IsQueued;
                            result = 1;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return result;
        }
    }
}
