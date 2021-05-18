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
    /// Created: 2021/03/08
    /// 
    /// Empty constructor that initializes fake financial counseling data
    /// </summary>
    public class FinancialCounselingFake : IFinancialCounselingAccessor
    {
        List<FinancialCounseling> data = new List<FinancialCounseling>();

        /// <summary>
        /// Chase Martin
        /// Created: 2021/03/09
        /// 
        /// Empty constructor with fake data
        /// </summary>
        public FinancialCounselingFake()
        {
            data.Add(new FinancialCounseling()
            {
                ServiceID = 0,
                ServiceProviderID = 0,
                ServiceName = "Budget",
                ServiceDescription = "Type of Financial Counseling",
                Available = true,
                ScheduleRequired = true
            });
            data.Add(new FinancialCounseling()
            {
                ServiceID = 1,
                ServiceProviderID = 1,
                ServiceName = "Debt",
                ServiceDescription = "Type of Financial Counseling",
                Available = true,
                ScheduleRequired = true
            });
            data.Add(new FinancialCounseling()
            {
                ServiceID = 2,
                ServiceProviderID = 2,
                ServiceName = "Credit",
                ServiceDescription = "Type of Financial Counseling",
                Available = true,
                ScheduleRequired = true
            });

        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/03/09
        /// 
        /// Selects all financial counseling types
        /// </summary>
        public List<FinancialCounseling> SelectAllCounselingTypes()
        {
            return data;
        }
    }
}

