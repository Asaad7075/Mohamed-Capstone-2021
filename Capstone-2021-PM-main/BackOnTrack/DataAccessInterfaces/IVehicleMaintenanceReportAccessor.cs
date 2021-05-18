using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    /// <summary>
    /// Zach Stultz
    /// Created: 2021/02/12
    ///
    /// Vehicle Maintenance Report Accessors Class.
    /// <summary>
    public interface IVehicleMaintenanceReportAccessor
    {
        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/12
        ///
        /// Inserts the vehicle maintenance report.
        /// </summary>
        /// <param name="vehicleMaintenanceReport">The vehicle maintenance report.</param>
        /// <returns>An int.</returns>
        bool InsertVehicleMaintenanceReport(VehicleMaintenanceReportVM vehicleMaintenanceReport);

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/18
        ///
        /// Updates the vehicle maintenance report.
        /// </summary>
        /// <param name="vehicleMaintenanceReport">The vehicle maintenance report.</param>
        /// <returns>An int.</returns>
        int UpdateVehicleMaintenanceReport(VehicleMaintenanceReportVM oldVehicleMaintenanceReport, VehicleMaintenanceReportVM newVehicleMaintenanceReport);

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/12
        ///
        /// Selects the vehicle maintenance report by Vin.
        /// </summary>
        /// <param name="VinNumber">The Vin number.</param>
        /// <returns>An int.</returns>
        List<VehicleMaintenanceReportVM> SelectVehicleMaintenanceReportByVin(string VinNumber);

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/18
        ///
        /// Selects all the active maintenance reports.
        /// </summary>
        /// <returns>A list of VehicleMaintenanceReports.</returns>
        List<VehicleMaintenanceReportVM> SelectAllVehicleMaintenanceReports();

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/19
        ///
        /// Selects all the active maintenance reports.
        /// </summary>
        /// <returns>A list of VehicleMaintenanceReports.</returns>
        List<VehicleMaintenanceReportVM> SelectAllActiveMaintenanceReports();
    }
}
