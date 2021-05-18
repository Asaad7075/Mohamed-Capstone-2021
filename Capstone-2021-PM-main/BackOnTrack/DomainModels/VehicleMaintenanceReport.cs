using System;
using System.ComponentModel.DataAnnotations;

namespace DomainModels
{
    /// <summary>
    /// Zach Stultz
    /// Created: 2021/02/12
    ///
    /// Vehicle Maintenance Report storage model class for Back-On-track.
    /// <summary>
    public class VehicleMaintenanceReport
    {
        /// <summary>
        /// The unique log number for a vehicle being maintained.
        /// </summary>
        public int VehicleMaintenanceReportID { get; set; }
        /// <summary>
        /// Vehicle Vin Number.
        /// </summary>
        public string VinNumber { get; set; }
        /// <summary>
        /// The Vehicle Maintenance Type.
        /// </summary>
        public string VehicleMaintenanceTypeName;
        /// <summary>
        /// The date in which a vehicle was serviced.
        /// </summary>
        public DateTime? VehicleMaintenanceServiceDate { get; set; }
        /// <summary>
        /// Flag for whether maintenance is finished, with 0 being no and 1 being yes.
        /// </summary>
        public bool MaintenanceFinished { get; set; }
        /// <summary>
        /// Any additional notes that the maintenance type does not cover.
        /// </summary>
        public string VehicleMaintenanceNotes { get; set; }
        /// <summary>
        /// Flag for whether it is an active maintenance job.
        /// </summary>
        public bool IsQueued { get; set; }
    }
}