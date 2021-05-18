using DataAccessInterfaces;
using DataAccessLayer;
using DomainModels;
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
    /// Zach Stultz
    /// Created: 2021/02/24
    ///
    /// Route manager class.
    /// <summary>
    public class RouteManager : IRouteManager
    {
        private IRouteAccessor _routeAccessor = null;
        private IPickUpTicketAccessor _pickupTicketAccessor = null;
        private IDeliveryTicketAccessor _deliveryTicketAccessor = null;
        private IRideTicketAccessor _rideTicketAccessor = null;

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/02/24
        ///
        /// Jakub Kawski
        /// Edited: 2021/04/16
        /// Empty Constructor
        /// </summary>
        /// <param name="routeAccessor"></param>
        public RouteManager()
        {
            _routeAccessor = new RouteAccessor();
            _pickupTicketAccessor = new PickUpTicketAccessor();
            _deliveryTicketAccessor = new DeliveryTicketAccessor();
            _rideTicketAccessor = new RideTicketAccessor();
        }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/02/24
        ///
        /// Jakub Kawski
        /// Edited: 2021/04/16
        /// Full Constructor
        /// </summary>
        public RouteManager(IRouteAccessor routeAccessor)
        {
            _routeAccessor = routeAccessor;
            _pickupTicketAccessor = new PickUpTicketAccessor();
            _deliveryTicketAccessor = new DeliveryTicketAccessor();
            _rideTicketAccessor = new RideTicketAccessor();
        }
        /// <summary>
        /// Jakub Kawski
        /// Created: 2021/04/16
        ///
        /// Made this so the other constuctor works in tests
        ///
        /// Full Constructor
        /// </summary>
        public RouteManager(IRouteAccessor routeAccessor, IRideTicketAccessor rideTicketAccessor,
                            IDeliveryTicketAccessor deliveryTicketAccessor, IPickUpTicketAccessor pickUpTicketAccessor)
        {
            _routeAccessor = routeAccessor;
            _rideTicketAccessor = rideTicketAccessor;
            _deliveryTicketAccessor = deliveryTicketAccessor;
            _pickupTicketAccessor = pickUpTicketAccessor;
        }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/26
        ///
        /// Adds the route.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <returns>A bool.</returns>
        public bool AddRoute(RouteVM route)
        {
            bool result = false;
            bool duplicate = false;
            List<RouteVM> data = _routeAccessor.SelectAllRoutes();

            foreach (RouteVM item in data)
            {
                if (route.RouteID == item.RouteID)
                {
                    duplicate = true;
                }
            }
            if (!duplicate)
            {
                try
                {
                    result = _routeAccessor.InsertRoute(route);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                result = false;
                throw new Exception("Route already exists in the database.");
            }
            return result;
        }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/26
        ///
        /// Deletes the route.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <returns>A bool.</returns>
        public int DeleteRoute(RouteVM route)
        {
            int result = 0;
            bool found = false;
            List<RouteVM> data = _routeAccessor.SelectAllRoutes();
            foreach (Route item in data)
            {
                if (route.RouteID == item.RouteID)
                {
                    found = true;
                }
            }
            if (found)
            {
                try
                {
                    result = _routeAccessor.DeleteRoute(route);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                result = 0;
                throw new Exception("No matching item found in database.");
            }
            return result;
        }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/26
        ///
        /// Retrieves the routes by date.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>A list of Routes.</returns>
        public List<RouteVM> RetrieveRoutesByDate(DateTime date)
        {
            List<RouteVM> routes = new List<RouteVM>();
            try
            {
                routes = _routeAccessor.SelectRoutesByDate(date);
                foreach (var route in routes)
                {
                    route.Tickets = RetrieveRouteTickets(route.RouteID);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data could not be pulled at the moment\n\n"
                    + ex.Message + "\n\n" + ex.InnerException);
            }
            return routes;
        }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/26
        ///
        /// Retrieves the routes by driver id and date.
        /// </summary>
        /// <param name="driverID">The driver i d.</param>
        /// <param name="date">The date.</param>
        /// <returns>A list of Routes.</returns>
        public List<RouteVM> RetrieveRoutesByDriverID(int driverID)
        {
            List<RouteVM> routes = new List<RouteVM>();
            try
            {
                routes = _routeAccessor.SelectRoutesByDriverID(driverID);
                foreach (var route in routes)
                {
                    route.Tickets = RetrieveRouteTickets(route.RouteID);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data could not be pulled at the moment\n\n"
                    + ex.Message + "\n\n" + ex.InnerException);
            }
            return routes;
        }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/26
        ///
        /// Retrieves the all routes.
        /// </summary>
        /// <returns>A list of Routes.</returns>
        public List<RouteVM> RetrieveAllRoutes()
        {
            List<RouteVM> routes = null;

            try
            {
                routes = _routeAccessor.SelectAllRoutes();
                foreach (var route in routes)
                {
                    route.Tickets = RetrieveRouteTickets(route.RouteID);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not available.", ex);
            }
            return routes;
        }

        /// <summary>
        /// Jakub Kawski
        /// 2021/04/18
        ///
        /// Get the tickets on the route.
        /// </summary>
        /// <param name="routeID"></param>
        /// <returns></returns>
        private List<Ticket> RetrieveRouteTickets(int routeID)
        {
            List<Ticket> tickets = new List<Ticket>();

            try
            {
                tickets.AddRange(_pickupTicketAccessor.SelectPickUpTicketsByRouteID(routeID));
                tickets.AddRange(_deliveryTicketAccessor.SelectDeliveryTicketsByRouteID(routeID));
                tickets.AddRange(_rideTicketAccessor.SelectRideTicketsByRouteID(routeID));
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return tickets;
		}
        /// <summary>
        /// Jakub Kawski
        /// 2021/04/18
        ///
        /// Will update the tickets in the db.
        /// Tickets must have OldTicketCopy set for proper updates.
        /// </summary>
        /// <param name="tickets"></param>
        /// <returns></returns>
        public void UpdateTickets(List<Ticket> tickets)
        {
            try
            {
                foreach (var ticket in tickets)
                {
                    if (ticket.WasEdited)
                    {
                        switch (ticket.TicketType)
                        {

                            case (int)TicketType.Delivery:
                                var newDevliveryTicket = (DeliveryTicketVM)ticket;
                                var oldDevliveryTicket = (DeliveryTicketVM)ticket.OldTicketCopy;
                                _deliveryTicketAccessor.UpdateDeliveryTicket(newDevliveryTicket, oldDevliveryTicket);
                                break;
                            case (int)TicketType.PickUp:
                                var newPickUpTicket = (PickUpTicketVM)ticket;
                                var oldPickUpTicket = (PickUpTicketVM)ticket.OldTicketCopy;
                                _pickupTicketAccessor.UpdatePickUpTicket(newPickUpTicket, oldPickUpTicket);
                                break;
                            case (int)TicketType.Ride:
                                var newRideTicket = (RideTicketVM)ticket;
                                var oldRideTicket = (RideTicketVM)ticket.OldTicketCopy;
                                _rideTicketAccessor.UpdateRideTicket(newRideTicket, oldRideTicket);
                                break;
                            default:
                                throw new ApplicationException("RouteManager Error: Ticket missing TicketType");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/04/08
        ///
        /// Edits the route.
        /// </summary>
        /// <param name="oldRoute">The old route.</param>
        /// <param name="newRoute">The new route.</param>
        /// <returns>A bool.</returns>
        public bool EditRoute(RouteVM oldRoute, RouteVM newRoute)
        {
            bool result = false;

            try
            {
                result = (0 != _routeAccessor.UpdateRoute(oldRoute, newRoute));

                if (!result)
                {
                    throw new ApplicationException("Data not updated.");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Update Failed.", ex);
            }
            return result;
        }

        

        /// <summary>
        /// Jakub Kawski
        /// 2021/04/24
        /// 
        /// Get the next route ID in the database.
        /// </summary>
        /// <param name="routeDate"></param>
        /// <returns></returns>
        private int GetNextRouteID(DateTime routeDate)
        {
            int id;

            try
            {
                id = _routeAccessor.SelectNextRouteID(routeDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return id;
        }
        /// <summary>
        /// Jakub Kawski
        /// 2021/04/28
        /// 
        /// </summary>
        /// <param name="routeDate"></param>
        /// <param name="licensePlateNumber"></param>
        /// <param name="driverID"></param>
        /// <returns></returns>
        public RouteVM AddRoute(DateTime routeDate, string licensePlateNumber, int driverID)
        {
            RouteVM result = null;

            try
            {
                int id = _routeAccessor.InsertRoute(routeDate, licensePlateNumber, driverID);
                result = _routeAccessor.SelectRouteByID(id);
                if(result == null)
                {
                    throw new ApplicationException("Error: AddRoute in RouteManager; result was null");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return result;
        }
        /// <summary>
        /// Jakub Kawski
        /// 2021/28/04
        /// 
        /// Get Route By ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RouteVM RetrieveRouteByID(int id)
        {
            RouteVM result = null;

            try
            {
                result = _routeAccessor.SelectRouteByID(id);
                result.Tickets = RetrieveRouteTickets(id);
                if (result == null)
                {
                    throw new ApplicationException("Error: RetrieveRouteByID; result was null");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return result;
        }
        /// <summary>
        /// Jakub KAwski
        /// 2021/04/28
        /// 
        /// Get meta data 
        /// </summary>
        /// <returns></returns>
        public TicketMetaData GetTicketMetaData()
        {
            TicketMetaData ticketMetaData = null;
            try
            {
                ticketMetaData = _routeAccessor.GetTicketMetaData();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ticketMetaData;
        }
    }
}
