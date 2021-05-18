using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModels.Services;

namespace DataAccessInterfaces
{
    public interface IServiceProviderAccessor
    {
        /// <summary>
        /// Chase Martin
        /// Created: 2021/03/26
        /// 
        /// Interface accessor method for inserting 
        /// a new service provider.
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        int InsertServiceProvider(ServiceProvider serviceProvider);

        /// <summary>
        /// Chase Martin
        /// Created: 2021/03/26
        /// 
        /// Interface accessor method for selecting
        /// all service providers.
        /// </summary>
        /// <returns></returns>
        List<ServiceProvider> SelectAllServiceProviders();

        /// <summary>
        /// Chase martin
        /// Created: 2021/03/26
        /// 
        /// Interface method for deleting a Service Provider.
        /// </summary>
        /// <param name="serviceProviderID"></param>
        /// <returns></returns>
        int DeleteServiceProvider(int serviceProviderID); 

        /// <summary>
        /// Chase Martin
        /// Created 2021/03/26
        /// 
        /// Interface method for updating/editing
        /// a Service Provider. Returns the number 
        /// of rows affected.
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        int UpdateServiceProvider(ServiceProvider serviceProvider);

        /// <summary>
        /// Chase Martin
        /// Created 2021/03/26
        /// 
        /// Interface method for selecting
        /// a Service Provider by zip code. 
        /// </summary>
        /// <returns></returns>
        List<ServiceProvider> SelectProvidersByZipCode(string zipCode);
    }
}
