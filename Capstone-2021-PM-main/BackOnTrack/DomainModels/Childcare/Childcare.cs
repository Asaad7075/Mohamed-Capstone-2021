using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels
{
    public class Childcare
    {
        /// <summary>
        /// Chase Martin
        /// Created: 2021/02/12
        /// 
        /// Storage Model for childcare objects.
        /// </summary>
        public int ServiceID { get; set; }
        public int ServiceProviderID { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }
        public bool Available { get; set; }
        public bool ScheduleRequired { get; set; }
    }
}
