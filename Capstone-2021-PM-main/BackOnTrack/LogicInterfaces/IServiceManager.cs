using DomainModels.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicInterfaces
{
    public interface IServiceManager
    {
        /// <summary>
        /// Thomas Stout
        /// Created: 2021/03/19
        /// 
        /// Interface logic method that adds
        /// a new service.
        /// </summary>
        /// <param name="inventory"></param>
        /// <returns></returns>
        bool AddService(Service service);
        /// <summary>
        /// Thomas Stout
        /// Created: 2021/03/25
        /// 
        /// Interface logic method that selects
        /// all of the services.
        /// </summary>
        /// <returns></returns>
        List<Service> RetrieveAllServices();
        /// <summary>
        /// Thomas Stout
        /// Created: 2021/02/25
        /// 
        /// Deletes a Service. Returns
        /// true if Service was deleted.
        /// </summary>
        /// <param name="serviceID"></param>
        /// <returns></returns>
        bool DeleteService(int serviceID);
        /// <summary>
        /// Thomas Stout
        /// Created: 2021/03/25
        /// 
        /// Edits a Service. Returns
        /// true if Service was edited/updated.
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        bool EditService(Service service);
        /// <summary>
        /// Thomas Stout
        /// Created: 2021/04/07
        /// 
        /// Interface logic method for selecting
        /// all service provider first and last names.
        /// </summary>
        /// <returns></returns>
        List<Service> RetrieveAllProvidersByBusiness(Service service);
        List<Service> RetrieveAllBusinesses();
        /// <summary>
        /// Thomas Stout
        /// Created: 2021/04/15
        /// 
        /// Interface logic method for selecting
        /// all service schedules.
        /// </summary>
        /// <returns></returns>
        List<ServiceVM> RetrieveAllServiceSchedulesByID(int serviceID);
        /// <summary>
        /// Thomas Stout
        /// Created: 2021/03/19
        /// 
        /// Interface logic method that adds
        /// a client schedule.
        /// </summary>
        /// <param name="schedules"></param>
        /// <returns></returns>
        bool AddClientSchedule(string businessName, string serviceName, DateTime? serviceScheduleStart, DateTime? serviceScheduleEnd);
        /// <summary>
        /// Thomas Stout
        /// Created: 2021/04/24
        /// 
        /// Interface manager method for allowing
        /// a client to select a service date/time
        /// slot.
        /// </summary>
        /// <param name="clientID"></param>
        /// <param name="scheduleID"></param>
        /// <returns></returns>
        bool EditClientSchedule(int clientID, int scheduleID);

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/04/30
        /// Interface method for retriving a clients schedule from the accessor layer
        /// </summary>
        /// <param name="clientID"></param>
        /// <returns></returns>
        List<ServiceVM> RetrieveClientSchedulesByClientID(int clientID);

        /// <summary>
        /// Thomas Stout
        /// Created: 2021/04/30
        /// 
        /// Interface manager method for viewing
        /// saved schedules by client id
        /// </summary>
        /// <param name="clientID"></param>
        /// <returns></returns>
        List<ServiceVM> RetrieveAllSavedServiceSchedulesByClientID(int clientID);
    }
}
