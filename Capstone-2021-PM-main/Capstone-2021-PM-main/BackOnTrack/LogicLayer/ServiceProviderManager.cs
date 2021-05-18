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
    /// <summary>
    /// Chase Martin
    /// Created: 2021/03/26
    /// 
    /// Class for Service Provider Management
    /// </summary>
    public class ServiceProviderManager : IServiceProviderManager
    {
        private IServiceProviderAccessor _serviceProviderAccessor = null;

        /// <summary>
        /// Chase Martin
        /// Created: 2021/03/26
        /// 
        /// Empty constructor that holds the ServiceProviderAccessor
        /// </summary>
        public ServiceProviderManager()
        {
            _serviceProviderAccessor = new ServiceProviderAccessor();
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/03/26
        /// 
        /// Dependency Inversion
        /// </summary>
        /// <param name="dataAccessor"></param>
        public ServiceProviderManager(IServiceProviderAccessor dataAccessor)
        {
            _serviceProviderAccessor = dataAccessor;
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/03/26
        /// 
        /// Calls the InsertServiceProvider() method from the DataAccessLayer
        /// to add a new Service Provider
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public bool AddServiceProvider(ServiceProvider serviceProvider)
        {
            bool result = false;
            try
            {
                result = (1 == _serviceProviderAccessor.InsertServiceProvider(serviceProvider));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("The Service Provider could not be added.", ex);
            }
            return result;
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/03/26
        /// 
        /// Calls the DeleteServiceProvider() method from the
        /// DataAccessLayer to delete a Service Provider.
        /// </summary>
        /// <param name="serviceProviderID"></param>
        /// <returns></returns>
        public bool DeleteServiceProvider(int serviceProviderID)
        {
            bool result = false;
            try
            {
                result = (1 == _serviceProviderAccessor.DeleteServiceProvider(serviceProviderID));

            }
            catch (Exception ex)
            {
                throw new ApplicationException("Service Provider could not be removed.", ex);
            }
            return result;
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/03/26
        /// 
        /// Calls the UpdateServiceProvider() method from the
        /// DataAccessLayer to edit/update a Service Provider.
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public bool EditServiceProvider(ServiceProvider serviceProvider)
        {
            bool result = false;
            try
            {
                result = (1 == _serviceProviderAccessor.UpdateServiceProvider(serviceProvider));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Service Provider could not be edited/updated.", ex);
            }
            return result;
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/03/26
        /// 
        /// Calls the RetrieveAllServiceProviders() method
        /// from the DataAccessLayer to display all
        /// Service Providers.
        /// </summary>
        /// <returns></returns>
        public List<ServiceProvider> RetrieveAllServiceProviders()
        {
            List<ServiceProvider> serviceProviders = null;

            try
            {
                serviceProviders = _serviceProviderAccessor.SelectAllServiceProviders();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data Unavailable.", ex);
            }
            return serviceProviders;
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/03/26
        /// 
        /// Calls the RetrieveProvidersByZipCode() method
        /// from the DataAccessLayer to display all
        /// Service Providers with given zip code.
        /// </summary>
        /// <returns></returns>
        public List<ServiceProvider> RetrieveProvidersByZipCode(string zipCode)
        {
            try
            {
                return _serviceProviderAccessor.SelectProvidersByZipCode(zipCode);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data unavailable.", ex);
            }
        }
    }
}
