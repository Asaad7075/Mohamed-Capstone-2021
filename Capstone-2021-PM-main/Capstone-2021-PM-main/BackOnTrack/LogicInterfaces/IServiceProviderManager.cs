using DomainModels.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicInterfaces
{
    public interface IServiceProviderManager
    {
        /// <summary>
        /// Chase Martin
        /// Created: 2021/03/26
        /// 
        /// Interface logic method that adds
        /// a new service provider.
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        bool AddServiceProvider(ServiceProvider serviceProvider);

        /// <summary>
        /// Chase Martin
        /// Created: 2021/03/26
        /// 
        /// Interface logic method that selects
        /// all of the service providers.
        /// </summary>
        /// <returns></returns>
        List<ServiceProvider> RetrieveAllServiceProviders();

        /// <summary>
        /// Chase Martin
        /// Created: 2021/02/26
        /// 
        /// Deletes a Service Provider. Returns
        /// true if Service Provider was deleted.
        /// </summary>
        /// <param name="serviceProviderID"></param>
        /// <returns></returns>
        bool DeleteServiceProvider(int serviceProviderID);

        /// <summary>
        /// Chase Martin
        /// Created: 2021/03/26
        /// 
        /// Edits a Service Provider. Returns
        /// true if Service Provider was edited/updated.
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        bool EditServiceProvider(ServiceProvider serviceProvider);

        /// <summary>
        /// Chase Martin
        /// Created: 2021/03/26
        /// 
        /// Retrieves service provider by zip code.
        /// </summary>
        /// <returns></returns>
        List<ServiceProvider> RetrieveProvidersByZipCode(string zipCode);
    }
}
