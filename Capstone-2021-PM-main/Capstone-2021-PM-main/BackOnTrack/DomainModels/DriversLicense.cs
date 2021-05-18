using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels
{
    /// <summary>
    /// Chantal Shirley
    /// Created: 2021/02/02
    /// 
    /// Drivers license data storage model class for Back-On-Track
    /// volunteers. This information validates a volunteers ability
    /// to drive the differently classed vehicles that are associated with 
    /// the vehicle sotrage modal.
    /// </summary>
    public class DriversLicense
    {
        public string LicenseNumber { get; set; } // Primary Key, uniquely identifying drivers licenses from one another
        public int EmployeeID { get; set; } // Foreign Key from Employee table
        public string LicenseType { get; set; } // The class of vehicle a license allows
        public DateTime LicenseIssuedDate { get; set; } // The day the license was issued and became valid
        public DateTime LicenseExpiryDate { get; set; } // The day the license expires and is no longer valid
        public bool IsActive { get; set; } // Whether or not the drivers license is still active in the system and not archived
    }
}
