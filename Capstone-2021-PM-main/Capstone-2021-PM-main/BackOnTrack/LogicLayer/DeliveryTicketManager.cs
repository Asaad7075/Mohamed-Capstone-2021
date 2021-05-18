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
    /// <summary>
    /// Jakub Kawski
    /// 2020/02/21
    /// 
    /// Delivery ticket logic manager
    /// </summary>
    public class DeliveryTicketManager : IDeliveryTicketManager
    {
        private IDeliveryTicketAccessor _ticketAccessor;
        private IGeoLocationManager _geoLocationManager;
        public DeliveryTicketManager()
        {
            _ticketAccessor = new DeliveryTicketAccessor();
            _geoLocationManager = new GeoLocationManager();
        }
        public DeliveryTicketManager(IDeliveryTicketAccessor ticketAccessor, IGeoLocationAccessor geoLocationAccessor)
        {
            _ticketAccessor = ticketAccessor;
            _geoLocationManager = new GeoLocationManager(geoLocationAccessor);
        }
        /// <summary>
        /// Jakub Kawski
        /// 2020/02/21
        /// 
        /// Adds a new ticket to the db
        /// </summary>
        public bool AddTicket(DeliveryTicketVM ticket)
        {
            bool result = false;
            try
            {
                ticket.GeoID = _geoLocationManager.RetrieveGeoLocation(
                    ticket.StreetAddressLineOne, 
                    ticket.StreetAddressLineTwo, 
                    ticket.ZipCode
                    ).GeoID;
                result = (0 != _ticketAccessor.InsertDeliveryTicket(ticket));
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return result;
        }

        /// <summary>
        /// Jakub Kawski
        /// 2020/02/21
        /// 
        /// Removes a ticket form the db.
        /// </summary>
        public bool DeleteTicket(DeliveryTicketVM ticket)
        {
            bool result = false;
            try
            {
                result = (0 != _ticketAccessor.DeleteDeliveryTicket(ticket));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        /// <summary>
        /// Jakub Kawski
        /// 2020/02/21
        /// 
        /// Gets a list of all delivery tickets in db.
        /// </summary>
        public List<DeliveryTicketVM> RetrieveAllTickets()
        {
            List<DeliveryTicketVM> tickets = new List<DeliveryTicketVM>();
            try
            {
                tickets = _ticketAccessor.SelectAllDeliveryTickets();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return tickets;
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/12
        /// 
        /// Retrieves a delivery ticket
        /// by it's order id.
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public DeliveryTicketVM RetrieveTicketByOrderID(int orderID)
        {
            DeliveryTicketVM ticket = null;

            try
            {
                ticket = _ticketAccessor.SelectDeliveryTicketByOrderID(orderID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Delivery ticket could not be retrieved." +
                    "\n\n" + ex.Message);
            }

            return ticket;
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/08
        /// 
        /// Gets list of delivery tickets by client id.
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public List<DeliveryTicketVM> RetrieveTicketsByClientId(int clientId)
        {
            List<DeliveryTicketVM> tickets = new List<DeliveryTicketVM>();

            try
            {
                tickets = _ticketAccessor.SelectDeliveryTicketsByClientId(clientId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Delivery tickets could not be retrieved." + "\n\n"
                    + ex.Message);
            }

            return tickets;
        }

        /// <summary>
        /// Jakub Kawski
        /// 2020/02/21
        /// 
        /// Updates ticket.
        /// </summary>
        public bool UpdateTicket(DeliveryTicketVM newTicket, DeliveryTicketVM oldTicket)
        {
            bool result = false;
            try
            {
                newTicket.GeoID = _geoLocationManager.RetrieveGeoLocation(newTicket.StreetAddressLineOne, newTicket.StreetAddressLineTwo, newTicket.ZipCode).GeoID;
                result = (0 != _ticketAccessor.UpdateDeliveryTicket(newTicket, oldTicket));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        
    }
}
