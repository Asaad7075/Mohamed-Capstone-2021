using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels.Services
{
    public class ServiceVM : Service
    {
        public string Business { get; set; }
        public string Service  { get; set; }
        public DateTime ServiceScheduleStart { get; set; }
        public DateTime ServiceScheduleEnd { get; set; }
        public int ScheduleID { get; set; }
        public int? ClientID { get; set; }
    }
}
