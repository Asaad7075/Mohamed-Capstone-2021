using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicInterfaces
{
    /// <summary>
    /// Chase Martin
    /// Created: 2021/03/09
    /// 
    /// Interface for Financial Counseling
    /// </summary>
    /// <returns></returns>
    public interface IFinancialCounselingManager
    {
        /// <summary>
        /// Chase Martin
        /// Created: 2021/03/09
        /// 
        /// Retrieves all types from Financial Counseling
        /// </summary>
        /// <returns></returns>
        List<FinancialCounseling> RetrieveAllCounselingTypes();
    }
}
