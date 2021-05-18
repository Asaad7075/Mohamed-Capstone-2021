using DataAccessInterfaces;
using DomainModels.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessFakes
{
    /// <summary>
    /// Chase Martin
    /// Created: 2021/03/26
    /// 
    /// Fake item test data.
    public class ServiceProviderFake : IServiceProviderAccessor
    {
        List<ServiceProvider> data = new List<ServiceProvider>();

        /// <summary>
        /// Chase Martin
        /// Created: 2021/03/26
        /// 
        /// Empty constructor with fake data
        /// </summary>
        public ServiceProviderFake()
        {
            data.Add(new ServiceProvider()
            {
                ServiceProviderID = 100000,
                //ServiceCategory = "Financial Counselor",
                BusinessName = "Alliant",
                FirstName = "Bob",
                LastName = "Martin",
                MiddleInitial = 'M',
                Address = "222 Wood Oak Dr. Marion, IA",
                PhoneNumber = "3194653452",
                Email = "bob@alliant.com",
                EIN = "101333225",
                ZipCode = "52314",
                //ServiceID = 101,
                Activated = true,
                Schedule = true
            });

            data.Add(new ServiceProvider()
            {
                ServiceProviderID = 100001,
                //ServiceCategory = "Financial Counselor",
                BusinessName = "Motely",
                FirstName = "Mike",
                LastName = "Stern",
                MiddleInitial = 'J',
                Address = "220 Red Oak Dr. Debuque, IA",
                PhoneNumber = "3194124452",
                Email = "mike@motely.com",
                EIN = "101333115",
                ZipCode = "52314",
                //ServiceID = 102,
                Activated = true,
                Schedule = true
            });

            data.Add(new ServiceProvider()
            {
                ServiceProviderID = 100002,
                //ServiceCategory = "Childcare",
                BusinessName = "Fisher's",
                FirstName = "Rich",
                LastName = "Bardo",
                MiddleInitial = 'D',
                Address = "242 Hard Oak Xavier, IA",
                PhoneNumber = "3190653000",
                Email = "rich@fishers.com",
                EIN = "101333220",
                ZipCode = "52314",
                //ServiceID = 103,
                Activated = true,
                Schedule = true
            });

            data.Add(new ServiceProvider()
            {
                ServiceProviderID = 100003,
                //ServiceCategory = "Financial Counselor",
                BusinessName = "Lynch",
                FirstName = "Wayne",
                LastName = "Johnson",
                MiddleInitial = 'W',
                Address = "322 Yellow Ln. Solon, IA",
                PhoneNumber = "3124600452",
                Email = "wayne@lynch.com",
                EIN = "101333225",
                ZipCode = "52314",
                //ServiceID = 104,
                Activated = true,
                Schedule = true
            });

            data.Add(new ServiceProvider()
            {
                ServiceProviderID = 100004,
                //ServiceCategory = "Childcare",
                BusinessName = "Davendale Daycare",
                FirstName = "Ricky",
                LastName = "Bobby",
                MiddleInitial = 'S',
                Address = "122 Shake Dr. Marion, IA",
                PhoneNumber = "3194653452",
                Email = "bob@davendale.com",
                EIN = "101331429",
                ZipCode = "52404",
                //ServiceID = 105,
                Activated = true,
                Schedule = true
            });


        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/03/26
        /// 
        /// Uses fake data to delete a service provider.
        /// </summary>
        public int DeleteServiceProvider(int serviceProviderID)
        {
            return data.Count - 1;
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/03/26
        /// 
        /// Uses fake data to inser a provider.
        /// </summary>
        public int InsertServiceProvider(ServiceProvider serviceProvider)
        {
            return data.Count + 1;
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/03/26
        /// 
        /// Selects all fake service providers.
        /// </summary>
        public List<ServiceProvider> SelectAllServiceProviders()
        {
            return data;
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/03/26
        /// 
        /// Uses provider zip code to return fake provider.
        /// </summary>
        public List<ServiceProvider> SelectProvidersByZipCode(string zipCode)
        {
            return data;
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/03/26
        /// 
        /// Uses fake provider and updates it.
        /// </summary>
        public int UpdateServiceProvider(ServiceProvider serviceProvider)
        {
            return data.Count;
        }
    }
}
