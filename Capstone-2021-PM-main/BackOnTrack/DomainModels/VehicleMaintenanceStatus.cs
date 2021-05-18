using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels
{
    /// <summary>
    /// Zach Stultz
    /// Created: 2021/03/19
    ///
    /// The Vehicle Maintenance Status Domain Class.
    /// </summary>
    public class VehicleMaintenanceStatus
    {

        /// <summary>
        /// The Status ID, Primary Key
        /// </summary>
        public int StatusID { get; set; }

        /// <summary>
        /// The Vin Number, Foreign Key
        /// </summary>
        public string VinNumber { get; set; }

        /// <summary>
        /// Maintenance Status Type, Foreign Key
        /// </summary>
        public string MaintenanceStatusType { get; set; }

        /// <summary>
        /// Whether or not it has a maintenance report.
        /// </summary>
        public bool HasMaintenanceReport { get; set; }

        /// <summary>
        /// The Identified Maintenance.
        /// </summary>
        public string IdentifiedMaintenance { get; set; }
    }
}
