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
    /// Created: 2021/03/19
    ///
    /// The vehicle maintenance status accessors interface.
    /// </summary>
    public interface IVehicleMaintenanceStatusAccessor
    {
        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/19
        ///
        /// Inserts the vehicle maintenance status type.
        /// </summary>
        /// <param name="vehicleMaintenanceStatusType">The vehicle maintenance status type.</param>
        /// <returns>An int.</returns>
        bool InsertVehicleMaintenanceStatusType(VehicleMaintenanceStatusType vehicleMaintenanceStatusType);

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/19
        ///
        /// Inserts the vehicle maintenance status.
        /// </summary>
        /// <param name="vehicleMaintenanceStatus">The vehicle maintenance status.</param>
        /// <returns>An int.</returns>
        bool InsertVehicleMaintenanceStatus(VehicleMaintenanceStatus vehicleMaintenanceStatus);


        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/25
        ///
        /// Selects the all vehicle maintenance statuses.
        /// </summary>
        /// <returns>A list of VehicleMaintenanceStatuses.</returns>
        List<VehicleMaintenanceStatus> SelectAllVehicleMaintenanceStatuses();

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/25
        ///
        /// Selects the all vehicle maintenance status types.
        /// </summary>
        /// <returns>A list of VehicleMaintenanceStatusTypes.</returns>
        List<VehicleMaintenanceStatusType> SelectAllVehicleMaintenanceStatusTypes();
    }
}
