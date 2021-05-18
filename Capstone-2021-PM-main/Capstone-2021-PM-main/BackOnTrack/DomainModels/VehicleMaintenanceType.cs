using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels
{
    /// <summary>
    /// Zach Stultz
    /// Created: 2021/02/12
    ///
    /// Vehicle Maintenance Type Model Class.
    /// <summary>
    public class VehicleMaintenanceType
    {
        /// <summary>
        /// The type of maintenance needed for a vehicle
        /// </summary>
        public string VehicleMaintenanceTypeName { get; set; }

        /// <summary>
        /// A description of the maintenance type needed
        /// </summary>
        public string VehicleMaintenanceTypeDescription { get; set; }

    }
}
