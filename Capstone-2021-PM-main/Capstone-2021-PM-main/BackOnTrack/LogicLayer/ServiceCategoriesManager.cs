using DataAccessInterfaces;
using DataAccessLayer;
using DomainModels.Services;
using LogicInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class ServiceCategoriesManager : IServiceCategoriesManager
    {
        private IServiceCategoriesAccessor _serviceCategoriesAccessor = null;

        /// <summary>
        /// Thomas Stout
        /// Created: 2021/04/01
        /// 
        /// Empty constructor that holds the ServiceCategoriesAccessor
        /// </summary>
        public ServiceCategoriesManager()
        {
            _serviceCategoriesAccessor = new ServiceCategoriesAccessor();
        }

        /// <summary>
        /// Thomas Stout
        /// Created: 2021/04/01
        /// 
        /// Dependency Inversion
        /// </summary>
        /// <param name="dataAccessor"></param>
        public ServiceCategoriesManager(IServiceCategoriesAccessor dataAccessor)
        {
            _serviceCategoriesAccessor = dataAccessor;
        }
        public List<ServiceCategories> RetrieveAllServiceCategories()
        {
            List<ServiceCategories> categories = null;

            try
            {
                categories = _serviceCategoriesAccessor.SelectAllServiceCategories();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data Unavailable.", ex);
            }
            return categories;
        }
    }
}
