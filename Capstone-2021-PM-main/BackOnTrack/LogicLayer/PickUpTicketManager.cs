using DataAccessInterfaces;
using DataAccessLayer;
using DomainModels.Tickets;
using LogicInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class PickUpTicketManager : IPickUpTicketManager
    {
        private IPickUpTicketAccessor _ticketAccessor;
        private IGeoLocationManager _geoLocationManager;
        /// <summary>
        /// Jakub Kawski
        /// 2021/03/12
        /// 
        /// Regular construstor
        /// </summary>
        public PickUpTicketManager()
        {
            _ticketAccessor = new PickUpTicketAccessor();
            _geoLocationManager = new GeoLocationManager();
        }
        /// <summary>
        /// Jakub Kawski
        /// 2021/03/12
        /// 
        /// Constructor with reverse dependancy for datafakes and unit testing
        /// </summary>
        public PickUpTicketManager(IPickUpTicketAccessor pickUpTicketAccessor, IGeoLocationAccessor geoLocationAccessor)
        {
            _ticketAccessor = pickUpTicketAccessor;
            _geoLocationManager = new GeoLocationManager(geoLocationAccessor);
        }

        public bool AddTicket(PickUpTicketVM ticket)
        {
            bool result;
            try
            {
                ticket.GeoID = _geoLocationManager.RetrieveGeoLocation(
                    ticket.StreetAddressLineOne,
                    ticket.StreetAddressLineTwo,
                    ticket.ZipCode
                    ).GeoID;
                result = (0 != _ticketAccessor.InsertPickUpTicket(ticket));
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return result;
        }

        public bool DeleteTicket(PickUpTicketVM ticket)
        {
            bool result = false;
            try
            {
                result = (0 != _ticketAccessor.DeletePickUpTicket(ticket));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        /// <summary>
        /// Jakub Kawski
        /// 2021/12/03
        /// 
        /// Returns a list of all pickuptickets in the database
        /// </summary>
        /// <returns></returns>
        public List<PickUpTicketVM> RetrieveAllTickets()
        {
            List<PickUpTicketVM> tickets = new List<PickUpTicketVM>();
            try
            {
                tickets = _ticketAccessor.SelectAllPickUpTickets();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return tickets;
        }

        public bool UpdateTicket(PickUpTicketVM newTicket, PickUpTicketVM oldTicket)
        {
            bool result = false;
            try
            {
                newTicket.GeoID = _geoLocationManager.RetrieveGeoLocation(newTicket.StreetAddressLineOne, newTicket.StreetAddressLineTwo, newTicket.ZipCode).GeoID;
                result = !(1 > _ticketAccessor.UpdatePickUpTicket(newTicket, oldTicket));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        /// <summary>
        /// Jakub Kawski
        /// 2021/04/26
        /// 
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public List<PickUpTicketVM> RetrievePickUpTicketsByDate(DateTime dateTime)
        {
            List<PickUpTicketVM> tickets = new List<PickUpTicketVM>();
            try
            {
                tickets = _ticketAccessor.SelectPickUpTicketsByDate(dateTime);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return tickets;
        }
    }
}
