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
    /// Created: 2021/03/19
    ///
    /// The vehicle maintenance status logic interface.
    /// </summary>
    public interface IVehicleMaintenanceStatusManager
    {
        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/19
        ///
        /// Adds the vehicle maintenance status type.
        /// </summary>
        /// <param name="vehicleMaintenanceStatusType">The vehicle maintenance status type.</param>
        /// <returns>An int.</returns>
        bool AddVehicleMaintenanceStatusType(VehicleMaintenanceStatusType vehicleMaintenanceStatusType);

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/19
        ///
        /// Adds the vehicle maintenance status.
        /// </summary>
        /// <param name="vehicleMaintenanceStatus">The vehicle maintenance status.</param>
        /// <returns>An int.</returns>
        bool AddVehicleMaintenanceStatus(VehicleMaintenanceStatus vehicleMaintenanceStatus);

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/25
        ///
        /// Retrieves the all vehicle maintenance statuses.
        /// </summary>
        /// <returns>A list of VehicleMaintenanceStatuses.</returns>
        List<VehicleMaintenanceStatus> RetrieveAllVehicleMaintenanceStatuses();

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/25
        ///
        /// Retrieves the all vehicle maintenance status types.
        /// </summary>
        /// <returns>A list of VehicleMaintenanceStatusTypes.</returns>
        List<VehicleMaintenanceStatusType> RetrieveAllVehicleMaintenanceStatusTypes();

    }
}
