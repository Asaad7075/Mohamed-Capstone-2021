using DomainModels.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicInterfaces
{
    public interface IServiceCategoriesManager
    {
        /// <summary>
        /// Thomas Stout
        /// Created: 2021/04/01
        /// 
        /// Interface logic method that selects
        /// all of the categories.
        /// </summary>
        /// <returns></returns>
        List<ServiceCategories> RetrieveAllServiceCategories();
    }
}
