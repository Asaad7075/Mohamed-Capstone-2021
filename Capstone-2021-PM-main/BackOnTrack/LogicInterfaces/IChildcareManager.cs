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
    /// Created: 2021/03/3
    /// 
    /// Interface for Childcare
    /// </summary>
    /// <returns></returns>
    public interface IChildcareManager
    {
        /// <summary>
        /// Chase Martin
        /// Created: 2021/03/3
        /// 
        /// Retrieves all types from Childcare
        /// </summary>
        /// <returns></returns>
        List<Childcare> RetrieveAllChildcareTypes();
    }
}
