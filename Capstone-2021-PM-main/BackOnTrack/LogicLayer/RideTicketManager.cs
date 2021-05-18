using DataAccessInterfaces;
using DataAccessLayer;
using DomainModels;
using DomainModels.Services;
using DomainModels.Tickets;
using LogicInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class RideTicketManager : IRideTicketManager
    {
        private IServiceManager _serviceManager;
        private IClientManager _clientManager;
        private IServiceProviderManager _serviceProviderManager;
        private IZipCodeManager _zipCodeManager;
        private IRideTicketAccessor _ticketAccessor;
        private IGeoLocationManager _geoLocationManager;
        /// <summary>
        /// Jakub Kawski
        /// 2021/03/26
        /// 
        /// Regular construstor
        /// </summary>
        public RideTicketManager()
        {
            _ticketAccessor = new RideTicketAccessor();
            _geoLocationManager = new GeoLocationManager();
            _serviceManager = new ServiceManager();
            _clientManager = new ClientManager();
            _serviceProviderManager = new ServiceProviderManager();
            _zipCodeManager = new ZipCodeManager();
        }
        /// <summary>
        /// Jakub Kawski
        /// 2021/03/26
        /// 
        /// Constructor with reverse dependancy for datafakes and unit testing
        /// </summary>
        public RideTicketManager(IRideTicketAccessor rideTicketAccessor, IGeoLocationAccessor geoLocationAccessor)
        {
            _ticketAccessor = rideTicketAccessor;
            _geoLocationManager = new GeoLocationManager(geoLocationAccessor);
        }
        public bool AddTicket(RideTicketVM ticket)
        {
            bool result;
            try
            {
                ticket.FetchGeoID = _geoLocationManager.RetrieveGeoLocation(
                    ticket.FetchStreetAddressLineOne,
                    ticket.FetchStreetAddressLineTwo,
                    ticket.FetchZipCode
                    ).GeoID;
                ticket.DestinationGeoID = _geoLocationManager.RetrieveGeoLocation(
                    ticket.DestinationStreetAddressLineOne,
                    ticket.DestinationStreetAddressLineTwo,
                    ticket.DestinationZipCode
                    ).GeoID;
                result = (0 != _ticketAccessor.InsertRideTicket(ticket));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        /// <summary>
        /// Jakub Kawski
        /// 2021/03/26
        /// 
        /// Remove the param ticket from the db.
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        public bool DeleteTicket(RideTicketVM ticket)
        {
            bool result = false;
            try
            {
                result = (0 != _ticketAccessor.DeleteRideTicket(ticket));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        /// <summary>
        /// Jakub Kawski
        /// 
        /// 2021/03/26
        /// </summary>
        /// <returns></returns>
        public List<RideTicketVM> RetrieveAllTickets()
        {
            List<RideTicketVM> tickets = new List<RideTicketVM>();
            try
            {
                tickets = _ticketAccessor.SelectAllRideTickets();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return tickets;
        }

        public bool UpdateTicket(RideTicketVM newTicket, RideTicketVM oldTicket)
        {
            bool result = false;
            try
            { 
                newTicket.FetchGeoID = _geoLocationManager.RetrieveGeoLocation(
                    newTicket.FetchStreetAddressLineOne,
                    newTicket.FetchStreetAddressLineTwo,
                    newTicket.FetchZipCode
                    ).GeoID;
               newTicket.DestinationGeoID = _geoLocationManager.RetrieveGeoLocation(
                    newTicket.DestinationStreetAddressLineOne,
                    newTicket.DestinationStreetAddressLineTwo,
                    newTicket.DestinationZipCode
                    ).GeoID;
                result = !(1 > _ticketAccessor.UpdateRideTicket(newTicket, oldTicket));           
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        /// <summary>
        /// Nate Hepker
        /// Created:2021/04/30
        /// Collects data about the services, providers, client, schedules, and populates the 
        /// data into a rideTicket opject to be added to in the contoler for the client to add 
        /// their address to it too
        /// </summary>
        /// <param name="service"></param>
        /// <param name="clientID"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public RideTicketVM DataCollectionForRTVM(ServiceVM service, int clientID, string email)
        {
            RideTicketVM rideTicket = new RideTicketVM();//ride ticket to be populated and returned
            List<ServiceProvider> serviceProviders = new List<ServiceProvider>();//the service providers
            List<ServiceVM> services = new List<ServiceVM>();//list of services
            string[] clientname;//logged in clients name
            List<ServiceVM> sVM = new List<ServiceVM>();//serviceVM to be filterd by matching id
            ServiceVM serviceschedule = new ServiceVM(); ;//further filters serviceVM by the one matching the client id
            ServiceProvider serviceBusiness = new ServiceProvider();//gets the business name of the business
            List<Service> businesses = new List<Service>();//businesses
            Service business = new Service();
            List<ZipCodeFile> zcvms = new List<ZipCodeFile>();
            ZipCodeFile zcvm = new ZipCodeFile();
            List<ServiceVM> clientSchedule = new List<ServiceVM>();

            try
            {
                services = _serviceManager.RetrieveAllServiceSchedulesByID((int)service.ServiceID);
                clientname = _clientManager.RetrieveClientFirstLastNameByEmail(email);
                serviceProviders = _serviceProviderManager.RetrieveAllServiceProviders();
                businesses = _serviceManager.RetrieveAllBusinesses();
                zcvms = _zipCodeManager.RetrieveAllZipCodes();
                clientSchedule = _serviceManager.RetrieveClientSchedulesByClientID(clientID);

                sVM = services.FindAll(s => s.ServiceID == service.ServiceID);//filters by the service id
                serviceschedule = sVM.Find(s => s.ClientID == clientID);//finds the one that matches the client id
                serviceschedule = clientSchedule.Find(s => s.ClientID == clientID);
                business = businesses.Find(b => b.BusinessName == sVM[0].BusinessName);//sVM is a list by the business, so any index can get you the business name.
                serviceBusiness = serviceProviders.Find(sp => sp.BusinessName == business.BusinessName);//used to get the address of the selected business
                zcvm = zcvms.Find(z => z.ZipCode == serviceBusiness.ZipCode);


                rideTicket.ClientFirstName = clientname[0];
                rideTicket.ClientLastName = clientname[1];
                rideTicket.ClientID = clientID;

                rideTicket.DestinationStreetAddressLineOne = serviceBusiness.Address;
                rideTicket.DestinationCity = zcvm.City;
                rideTicket.DestinationState = zcvm.State;
                rideTicket.DestinationZipCode = serviceBusiness.ZipCode;

                rideTicket.CreatedAt = DateTime.Now;
                rideTicket.DateOfRide = serviceschedule.ServiceScheduleStart.Date;
                rideTicket.EstimatedArrival = service.ServiceScheduleStart;

                rideTicket.TicketType = 3;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return rideTicket;
        }

    }
}
