using DataAccessInterfaces;
using DomainModels.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessFakes
{
    public class ServiceFake : IServiceAccessor
    {
        List<Service> data = new List<Service>();

        /// <summary>
        /// Thomas Stout
        /// Created: 2021/03/19
        ///
        /// Empty constructor with fake data
        /// </summary>
        public ServiceFake()
        {
            data.Add(new Service()
            {
                ServiceID = 100000,
                BusinessName = "Tom's Barbershop",
                ServiceName = "Free Haircuts",
                ServiceCategoryName = "Haircuts",
                Available = true,
                ScheduleRequired = true,
                ServiceDescription = "A place to get your hair cut.",
                ServiceProviderFirstLast = "James, Ron"
            });
            data.Add(new Service()
            {
                ServiceID = 100001,
                BusinessName = "Hyvee",
                ServiceName = "Can drive",
                ServiceCategoryName = "Food",
                Available = true,
                ScheduleRequired = true,
                ServiceDescription = "A place to get a bite to eat.",
                ServiceProviderFirstLast = "Stout, Thomas"
            });
            data.Add(new Service()
            {
                ServiceID = 100002,
                BusinessName = "BioLife",
                ServiceName = "Blood Donation",
                ServiceCategoryName = "Donation",
                Available = true,
                ScheduleRequired = true,
                ServiceDescription = "Donate blood and get money.",
                ServiceProviderFirstLast = "White, Jamie"
            });
            data.Add(new Service()
            {
                ServiceID = 100003,
                BusinessName = "Rhonda's Daycare",
                ServiceName = "Rhonda's Daycare Service",
                ServiceCategoryName = "Childcare",
                Available = false,
                ScheduleRequired = true,
                ServiceDescription = "Free childcare services.",
                ServiceProviderFirstLast = "Stevens, Alexa"
            });
            data.Add(new Service()
            {
                ServiceID = 100004,
                BusinessName = "Alcoholics Anonymous",
                ServiceName = "AA meeting",
                ServiceCategoryName = "Rehabilitation",
                Available = false,
                ScheduleRequired = true,
                ServiceDescription = "AA meeting that is held every Wednesday, Friday," +
                " and Saturday night.",
                ServiceProviderFirstLast = "Garringer, Noel"
            });
        }

        public int DeleteService(int serviceID)
        {
            return data.Count - 1;
        }

        public int InsertClientSchedule(string businessName, string serviceName, DateTime serviceScheduleStart, DateTime serviceScheduleEnd)
        {
            return data.Count + 1;
        }

        public int InsertService(Service service)
        {
            return data.Count + 1;
        }

        public List<Service> SelectAllBusinesses()
        {
            return data;
        }

        public List<Service> SelectAllProvidersByBusiness(Service service)
        {
            return data;
        }

        public List<ServiceVM> SelectAllSavedServiceSchedulesByClientID(int clientID)
        {
            List<ServiceVM> schedules = new List<ServiceVM>();
            return schedules;
        }

        public List<Service> SelectAllServices()
        {
            return data;
        }

        public List<Service> SelectAllServiceSchedulesByID(int serviceID)
        {
            return data;
        }

        public List<ServiceVM> SelectClientSchedules(int clientID)
        {
            throw new NotImplementedException();
        }

        public int UpdateClientSchedule(int clientID, int scheduleID)
        {
            throw new NotImplementedException();
        }

        public int UpdateService(Service service)
        {
            return data.Count;
        }

        List<ServiceVM> IServiceAccessor.SelectAllServiceSchedulesByID(int serviceID)
        {
            List<ServiceVM> services = new List<ServiceVM>();
            return services;
        }
    }
}
