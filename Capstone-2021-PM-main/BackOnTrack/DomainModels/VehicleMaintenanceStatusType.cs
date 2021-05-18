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
    /// The maintenance status type.
    /// </summary>
    public class VehicleMaintenanceStatusType
    {
        /// <summary>
        /// The severity of the maintenance status.
        /// </summary>
        public string MaintenanceStatusType { get; set; }
        /// <summary>
        /// A description of the severity.
        /// </summary>
        public string MaintenanceStatusDescription { get; set; }
    }
}
