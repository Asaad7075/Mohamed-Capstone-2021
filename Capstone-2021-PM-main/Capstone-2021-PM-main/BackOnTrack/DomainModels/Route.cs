using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels
{
    /// <summary>
    /// Zach Stultz
    /// Created: 2021/02/24
    ///
    /// The Route Model Class.
    /// <summary>
    public class Route
    {
        public int RouteID { get; set; }
        public DateTime DateOfRoute { get; set; }
        public int? DriverEmployeeID { get; set; }
        public bool Active { get; set; }
        public string LicensePlateNumber { get; set; }
    }
}
