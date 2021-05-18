using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels
{
    /// <summary>
    /// Chantal Shirley
    /// Created: 2021/02/09
    /// 
    /// Storage Model for vehicle objects.
    /// </summary>
    public class Vehicle
    {
        public string VinNumber { get; set; } // PK, Uniquely identifiable number; traditionally they are 17 characters long
        public string LicensePlateNumber { get; set; } // Unique Number identifiable number
        public string VehicleMake { get; set; } // The brand of vehicle
        public string VehicleModel { get; set; } // The version of the vehicle
        public int VehicleYear { get; set; } // The debut year of the vehicle
        public string LicenseClass { get; set; } // The minimum license required to operate the vehicle
        public int Mileage { get; set; } // The current amount of mileage on the vehicle
        public bool ADACompliant { get; set; } // Stands for Americans With Disabilities Act Compliant
        public bool IsActive { get; set; } // Determines whether or not it needs to be archived
        public int? GeoID { get; set; } // The last marked location of the vehicle
    }
}
