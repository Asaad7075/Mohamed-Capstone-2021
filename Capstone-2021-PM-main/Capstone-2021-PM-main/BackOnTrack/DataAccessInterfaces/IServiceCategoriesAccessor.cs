using DomainModels.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    public interface IServiceCategoriesAccessor
    {
        /// <summary>
        /// Thomas Stout
        /// Created: 2021/04/02
        /// 
        /// Interface accessor method for selecting
        /// all Service Categories.
        /// </summary>
        /// <returns></returns>
        List<ServiceCategories> SelectAllServiceCategories();
    }
}
