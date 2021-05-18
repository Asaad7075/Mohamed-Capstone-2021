using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DomainModels;

namespace DataAccessFakes
{
    /// <summary>
    /// Chase Martin
    /// Created: 2021/03/04
    /// 
    /// Empty constructor that initializes fake childcare data
    /// </summary>
    public class ChildcareFake : IChildcareAccessor
    {
        List<Childcare> data = new List<Childcare>();

        /// <summary>
        /// Chase Martin
        /// Created: 2021/03/09
        /// 
        /// Empty constructor with fake data
        /// </summary>
        public ChildcareFake()
        {
            data.Add(new Childcare()
            {
                ServiceID = 0,
                ServiceProviderID = 0,
                ServiceName = "Babysitter",
                ServiceDescription = "Type of Childcare",
                Available = true,
                ScheduleRequired = true
            });
            data.Add(new Childcare()
            {
                ServiceID = 1,
                ServiceProviderID = 1,
                ServiceName = "Nanny",
                ServiceDescription = "Type of Childcare",
                Available = true,
                ScheduleRequired = true
            });
            data.Add(new Childcare()
            {
                ServiceID = 2,
                ServiceProviderID = 2,
                ServiceName = "Daycare",
                ServiceDescription = "Type of Childcare",
                Available = true,
                ScheduleRequired = true
            });

        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/03/09
        /// 
        /// Selects all childcare types
        /// </summary>
        public List<Childcare> SelectAllChildcareTypes()
        {
            return data;
        }
    }
}
