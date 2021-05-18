using DataAccessInterfaces;
using DataAccessLayer;
using DomainModels;
using LogicInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    /// <summary>
    /// Zach Stultz
    /// Created: 2021/02/19
    ///
    /// Vehicle Maintenance Report Logic Layer class for Back-On-track.
    /// <summary>
    public class VehicleMaintenanceReportManager : IVehicleMaintenanceReportManager
    {
        private IVehicleMaintenanceReportAccessor _vehicleMaintenanceReportAccessor;

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/02/19
        ///
        /// Default Constructor
        /// </summary>
        public VehicleMaintenanceReportManager()
        {
            _vehicleMaintenanceReportAccessor = new VehicleMaintenanceReportAccessor();
        }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/02/19
        ///
        /// Overloaded Constructor
        public VehicleMaintenanceReportManager(IVehicleMaintenanceReportAccessor vehicleMaintenanceReportAccessor)
        {
            _vehicleMaintenanceReportAccessor = vehicleMaintenanceReportAccessor;
        }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/02/12
        ///
        /// Inserts the vehicle maintenance report.
        /// </summary>
        /// <param name="vehicleMaintenanceReport">The vehicle maintenance report.</param>
        /// <returns>A bool.</returns>
        public bool AddVehicleMaintenanceReport(VehicleMaintenanceReportVM vehicleMaintenanceReport)
        {
            bool result = false;
            bool duplicate = false;
            List<VehicleMaintenanceReportVM> databaseClone = _vehicleMaintenanceReportAccessor.SelectAllVehicleMaintenanceReports();

            foreach (VehicleMaintenanceReportVM item in databaseClone)
            {
                if (vehicleMaintenanceReport.VinNumber == item.VinNumber &&
                    vehicleMaintenanceReport.VehicleMaintenanceTypeName == item.VehicleMaintenanceTypeName &&
                    vehicleMaintenanceReport.VehicleMaintenanceServiceDate == item.VehicleMaintenanceServiceDate &&
                    vehicleMaintenanceReport.MaintenanceFinished == item.MaintenanceFinished &&
                    vehicleMaintenanceReport.VehicleMaintenanceNotes == item.VehicleMaintenanceNotes)
                {
                    duplicate = true;
                }
            }
            if (!duplicate)
            {
                try
                {
                    result = _vehicleMaintenanceReportAccessor.InsertVehicleMaintenanceReport(vehicleMaintenanceReport);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            } else
            {
                result = false;
                throw new Exception("Vehicle Maintenance Report already exists in the database.");
            }
            return result;
        }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/18
        ///
        /// Edits the vehicle maintenance report.
        /// </summary>
        /// <param name="vehicleMaintenanceReport">The vehicle maintenance report.</param>
        /// <returns>An int.</returns>
        public bool EditVehicleMaintenanceReport(VehicleMaintenanceReportVM oldVehicleMaintenanceReport, VehicleMaintenanceReportVM newVehicleMaintenanceReport)
        {
            bool result = false;

            try
            {
                result = (1 == _vehicleMaintenanceReportAccessor.UpdateVehicleMaintenanceReport(oldVehicleMaintenanceReport, newVehicleMaintenanceReport));

                if (!result)
                {
                    throw new ApplicationException("Data not updated.");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Update Failed.", ex);
            }
            return result;
        }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/16
        ///
        /// Retrieves the vehicle maintenance report by Vin.
        /// </summary>
        /// <param name="VinNumber">The Vin number.</param>
        /// <returns>A VehicleMaintenanceReport.</returns>
        public List<VehicleMaintenanceReportVM> RetrieveVehicleMaintenanceReportByVin(string VinNumber)
        {
            List<VehicleMaintenanceReportVM> result = new List<VehicleMaintenanceReportVM>();
            try
            {
                result = _vehicleMaintenanceReportAccessor.SelectVehicleMaintenanceReportByVin(VinNumber);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data could not be pulled at the moment\n\n"
                    + ex.Message + "\n\n" + ex.InnerException);
            }
            return result;
        }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/18
        ///
        /// Retrieves the all active maintenance reports.
        /// </summary>
        /// <returns>A list of VehicleMaintenanceReports.</returns>
        public List<VehicleMaintenanceReportVM> RetrieveAllActiveMaintenanceReports()
        {
            List<VehicleMaintenanceReportVM> result = new List<VehicleMaintenanceReportVM>();
            try
            {
                result = _vehicleMaintenanceReportAccessor.SelectAllVehicleMaintenanceReports();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data could not be pulled at the moment.\n\n"
                    + ex.Message + "\n\n" + ex.InnerException);
            }

            return result;
        }
    }
}
