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
    /// Created: 2021/03/04
    /// 
    /// Interface for childcare.
    /// </summary>
    /// <returns></returns>
    public interface IChildcareAccessor
    {
        /// <summary>
        /// Chase Martin
        /// Created: 2021/03/04
        /// 
        /// Method for selecting all childcare types.
        /// </summary>
        /// <returns></returns>
        List<Childcare> SelectAllChildcareTypes();
    }
}
