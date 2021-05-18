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
    /// View model for authorized quick search of drivers license data.
    /// </summary>
    public class DriversLicenseViewModel : DriversLicense
    {
        public string EmployeeFirstName { get; set; } 
        public string EmployeeLastName { get; set; }
    }
}
