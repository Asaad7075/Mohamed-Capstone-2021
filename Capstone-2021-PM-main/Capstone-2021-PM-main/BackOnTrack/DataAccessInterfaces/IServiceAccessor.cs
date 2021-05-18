using DomainModels.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    public interface IServiceAccessor
    {
        /// <summary>
        /// Thomas Stout
        /// Created: 2021/03/19
        /// 
        /// Interface accessor method for inserting 
        /// a new service.
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        int InsertService(Service service);
        /// <summary>
        /// Thomas Stout
        /// Created: 2021/03/25
        /// 
        /// Interface accessor method for selecting
        /// all services.
        /// </summary>
        /// <returns></returns>
        List<Service> SelectAllServices();
        /// <summary>
        /// Thomas Stout
        /// Created: 2021/03/25
        /// 
        /// Interface method for deleting a Service.
        /// </summary>
        /// <param name="serviceID"></param>
        /// <returns></returns>
        int DeleteService(int serviceID); // Returns rows affected
        /// <summary>
        /// Thomas Stout
        /// Created 2021/03/25
        /// 
        /// Interface method for updating/editing
        /// a Service. Returns the number 
        /// of rows affected.
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        int UpdateService(Service service);
        /// <summary>
        /// Thomas Stout
        /// Created: 2021/04/07
        /// 
        /// Interface accessor method for selecting
        /// all service providers by their business.
        /// </summary>
        /// <returns></returns>
        List<Service> SelectAllProvidersByBusiness(Service service);
        List<Service> SelectAllBusinesses();
        /// <summary>
        /// Thomas Stout
        /// Created: 2021/04/15
        /// 
        /// Interface accessor method for selecting
        /// all service schedules.
        /// </summary>
        /// <returns></returns>
        List<ServiceVM> SelectAllServiceSchedulesByID(int serviceID);
        /// <summary>
        /// Thomas Stout
        /// Created: 2021/04/21
        /// 
        /// Interface accessor method for inserting
        /// a client saved schedule.
        /// </summary>
        /// <param name="schedules"></param>
        /// <returns></returns>
        int InsertClientSchedule(string businessName, string serviceName, DateTime serviceScheduleStart, DateTime serviceScheduleEnd);
        /// <summary>
        /// Thomas Stout
        /// Created: 2021/04/24
        /// 
        /// Interface accessor method for 
        /// selecting a service time slot as a
        /// client.
        /// </summary>
        /// <param name="clientID"></param>
        /// <param name="scheduleID"></param>
        /// <returns></returns>
        int UpdateClientSchedule(int clientID, int scheduleID);
        /// <summary>
        /// Nate Hepker
        /// Created: 2021/04/30
        /// Interface accessor for selecting a clients service schedules from the database
        /// </summary>
        /// <param name="clientID"></param>
        /// <returns></returns>
        List<ServiceVM> SelectClientSchedules(int clientID);
        /// <summary>
        /// Thomas Stout
        /// Created: 2021/04/27
        /// 
        /// Interface accessor method for 
        /// viewing all of a client's saved schedules.
        /// </summary>
        /// <param name="clientID"></param>
        /// <returns></returns>
        List<ServiceVM> SelectAllSavedServiceSchedulesByClientID(int clientID);
    }
}
