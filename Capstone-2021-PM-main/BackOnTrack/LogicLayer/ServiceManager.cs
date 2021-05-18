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
    /// Thomas Stout
    /// Created: 2021/03/25
    /// 
    /// Class for Service Management
    /// </summary>
    public class ServiceManager : IServiceManager
    {
        private IServiceAccessor _serviceAccessor = null;

        /// <summary>
        /// Thomas Stout
        /// Created: 2021/03/24
        /// 
        /// Empty constructor that holds the ServiceAccessor
        /// </summary>
        public ServiceManager()
        {
            _serviceAccessor = new ServiceAccessor();
        }

        /// <summary>
        /// Thomas Stout
        /// Created: 2021/03/25
        /// 
        /// Dependency Inversion
        /// </summary>
        /// <param name="dataAccessor"></param>
        public ServiceManager(IServiceAccessor dataAccessor)
        {
            _serviceAccessor = dataAccessor;
        }

        /// <summary>
        /// Thomas Stout
        /// Created: 2021/04/24
        /// 
        /// Manager method for adding a client schedule.
        /// </summary>
        /// <param name="businessName"></param>
        /// <param name="serviceName"></param>
        /// <param name="serviceScheduleStart"></param>
        /// <param name="serviceScheduleEnd"></param>
        /// <returns></returns>
        public bool AddClientSchedule(string businessName, string serviceName, DateTime? serviceScheduleStart, DateTime? serviceScheduleEnd)
        {
            bool result = false;
            try
            {
                result = (1 == _serviceAccessor.InsertClientSchedule(businessName, serviceName, (DateTime)serviceScheduleStart, (DateTime)serviceScheduleEnd));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("The Schedule could not be added.", ex);
            }
            return result;
        }

        /// <summary>
        /// Thomas Stout
        /// Created: 2021/03/25
        /// 
        /// Calls the InsertService() method from the DataAccessLayer
        /// to add a new Service
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public bool AddService(Service service)
        {
            bool result = false;
            try
            {
                result = (1 == _serviceAccessor.InsertService(service));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("The Service could not be added.", ex);
            }
            return result;
        }

        /// <summary>
        /// Thomas Stout
        /// Created: 2021/03/25
        /// 
        /// Calls the DeleteService() method from the
        /// DataAccessLayer to delete a Service.
        /// </summary>
        /// <param name="serviceID"></param>
        /// <returns></returns>
        public bool DeleteService(int serviceID)
        {
            bool result = false;
            try
            {
                result = (1 == _serviceAccessor.DeleteService(serviceID));

            }
            catch (Exception ex)
            {
                throw new ApplicationException("Service could not be removed.", ex);
            }
            return result;
        }

        /// <summary>
        /// Thomas Stout
        /// Created: 2021/04/24
        /// 
        /// Manager method that allows
        /// a client to select a service
        /// date/time slot.
        /// </summary>
        /// <param name="clientID"></param>
        /// <param name="scheduleID"></param>
        /// <returns></returns>
        public bool EditClientSchedule(int clientID, int scheduleID)
        {
            bool result = false;
            try
            {
                result = (1 == _serviceAccessor.UpdateClientSchedule(clientID, scheduleID));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Schedule could not be edited/updated.", ex);
            }
            return result;
        }

        /// <summary>
        /// Thomas Stout
        /// Created: 2021/03/25
        /// 
        /// Calls the UpdateService() method from the
        /// DataAccessLayer to edit/update a Service.
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public bool EditService(Service service)
        {
            bool result = false;
            try
            {
                result = (1 == _serviceAccessor.UpdateService(service));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Service could not be edited/updated.", ex);
            }
            return result;
        }

        public List<Service> RetrieveAllBusinesses()
        {
            List<Service> businesses = null;

            try
            {
                businesses = _serviceAccessor.SelectAllBusinesses();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data Unavailable.", ex);
            }
            return businesses;
        }

        public List<Service> RetrieveAllProvidersByBusiness(Service service)
        {
            List<Service> providers = null;

            try
            {
                providers = _serviceAccessor.SelectAllProvidersByBusiness(service);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data Unavailable.", ex);
            }
            return providers;
        }

        /// <summary>
        /// Thomas Stout
        /// Created: 2021/04/30
        /// 
        /// Manager method for viewing saved
        /// schedules by client id.
        /// </summary>
        /// <param name="clientID"></param>
        /// <returns></returns>
        public List<ServiceVM> RetrieveAllSavedServiceSchedulesByClientID(int clientID)
        {
            List<ServiceVM> data = null;

            try
            {
                data = _serviceAccessor.SelectAllSavedServiceSchedulesByClientID(clientID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not available.", ex);
            }
            return data;
        }

        /// <summary>
        /// Thomas Stout
        /// Created: 2021/03/25
        /// 
        /// Calls the SelectAllServices() method
        /// from the DataAccessLayer to display all
        /// Services.
        /// </summary>
        /// <returns></returns>
        public List<Service> RetrieveAllServices()
        {
            List<Service> services = null;

            try
            {
                services = _serviceAccessor.SelectAllServices();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data Unavailable.", ex);
            }
            return services;
        }

        /// <summary>
        /// Thomas Stout
        /// Creaeted: 2021/04/24
        /// 
        /// Manager method that selects
        /// all service schedules by their id.
        /// </summary>
        /// <param name="serviceID"></param>
        /// <returns></returns>
        public List<ServiceVM> RetrieveAllServiceSchedulesByID(int serviceID)
        {
            List<ServiceVM> data = null;

            try
            {
                data = _serviceAccessor.SelectAllServiceSchedulesByID(serviceID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not available.", ex);
            }
            return data;
        }

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/04/30
        /// Retrieves a clients schedule by thier id
        /// </summary>
        /// <param name="clientID"></param>
        /// <returns></returns>
        public List<ServiceVM> RetrieveClientSchedulesByClientID(int clientID)
        {
            List<ServiceVM> serviceSchedules = null;
            try
            {
                serviceSchedules = _serviceAccessor.SelectClientSchedules(clientID);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Could not retrieve the clients schedules." + ex.InnerException + ex.Message);
            }
            return serviceSchedules;
        }

    }
}
