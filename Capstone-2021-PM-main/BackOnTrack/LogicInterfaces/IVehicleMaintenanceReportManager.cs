using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicInterfaces
{
    /// <summary>
    /// Zach Stultz
    /// Created: 2021/02/12
    ///
    /// Vehicle Maintenance Report Logic Layer Interface..
    /// <summary>
    public interface IVehicleMaintenanceReportManager
    {
        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/12
        ///
        /// Adds the vehicle maintenance report.
        /// </summary>
        /// <param name="vehicleMaintenanceReport">The vehicle maintenance report.</param>
        /// <returns>An int.</returns>
        bool AddVehicleMaintenanceReport(VehicleMaintenanceReportVM vehicleMaintenanceReport);

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/18
        ///
        /// Edits the vehicle maintenance report.
        /// </summary>
        /// <param name="vehicleMaintenanceReport">The vehicle maintenance report.</param>
        /// <returns>An int.</returns>
        bool EditVehicleMaintenanceReport(VehicleMaintenanceReportVM oldVehicleMaintenanceReport, VehicleMaintenanceReportVM newVehicleMaintenanceReport);

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/12
        ///
        /// Retrieves the vehicle maintenance report by Vin.
        /// </summary>
        /// <param name="VinNumber">The Vin number.</param>
        /// <returns>An int.</returns>
        List<VehicleMaintenanceReportVM> RetrieveVehicleMaintenanceReportByVin(string VinNumber);

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/18
        ///
        /// Retrieves the all active maintenance reports.
        /// </summary>
        /// <returns>A list of VehicleMaintenanceReports.</returns>
        List<VehicleMaintenanceReportVM> RetrieveAllActiveMaintenanceReports();
    }
}
