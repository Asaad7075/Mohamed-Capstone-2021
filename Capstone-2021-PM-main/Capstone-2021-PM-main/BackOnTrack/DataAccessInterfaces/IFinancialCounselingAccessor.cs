using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModels;

namespace DataAccessInterfaces
{
    /// <summary>
    /// Chase Martin
    /// Created: 2021/03/08
    /// 
    /// Interface for financial counseling.
    /// </summary>
    /// <returns></returns>
    public interface IFinancialCounselingAccessor
    {
        /// <summary>
        /// Chase Martin
        /// Created: 2021/03/08
        /// 
        /// Interface method for selecting all counseling types.
        /// </summary>
        /// <returns></returns>
        List<FinancialCounseling> SelectAllCounselingTypes();
    }
}
