using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels.Services
{
    /// <summary>
    /// Thomas Stout
    /// Created: 2021/03/19
    /// 
    /// Model class for all Services
    /// </summary>
    public class Service
    {
        public int ServiceID { get; set; } // Primary key, unique identifier for each service.
        public string BusinessName { get; set; } // Name of the Service Provider's Business.
        public string ServiceName { get; set; } // Name of the Service.
        public string ServiceCategoryName { get; set; } // Type of Service.
        public bool Available { get; set; } // Is this service available or not?
        public bool ScheduleRequired { get; set; } // Is a schedule required for this service?
        public string ServiceDescription { get; set; } // Short description of the service.
        public string ServiceProviderFirstLast { get; set; } // First and last name of service provider.
    }
}
