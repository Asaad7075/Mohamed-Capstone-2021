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
    /// Vehicle Maintenance Report view model class for Back-On-track.
    /// <summary>
    public class VehicleMaintenanceReportVM : VehicleMaintenanceReport
    {
        public string VinNumber { get; set; }
        public string VehicleMake { get; set; }
        public string VehicleModel { get; set; }
        public string LicensePlate { get; set; }
        public string MaintenanceType { get; set; }
        public DateTime? MaintenanceServiceDate { get; set; }
        public bool MaintenanceFinished { get; set; }
        public string MaintenanceNotes { get; set; }
    }
}
