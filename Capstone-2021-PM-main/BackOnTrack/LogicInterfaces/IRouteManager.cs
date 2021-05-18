using DomainModels;
using DomainModels.Tickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicInterfaces
{
    /// <summary>
    /// Zach Stultz
    /// Created: 2021/02/12
    ///
    /// Route Logic Layer Interface for Back-On-track.
    /// <summary>
    public interface IRouteManager
    {
        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/16
        ///
        /// Adds the route.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <returns>A bool.</returns>
        bool AddRoute(RouteVM route);

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/26
        ///
        /// Removes the route.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <returns>A bool.</returns>
        int DeleteRoute(RouteVM route);

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/26
        ///
        /// Retrieves the routes by date.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>A list of Routes.</returns>
        List<RouteVM> RetrieveRoutesByDate(DateTime date);

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/26
        ///
        /// Retrieves the routes by driver id and date.
        /// </summary>
        /// <param name="driverID">The driver id.</param>
        /// <param name="date">The date.</param>
        /// <returns>A list of Routes.</returns>
        List<RouteVM> RetrieveRoutesByDriverID(int driverID);

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/26
        ///
        /// Retrieves the all routes.
        /// </summary>
        /// <returns>A list of Routes.</returns>
        List<RouteVM> RetrieveAllRoutes();

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/04/08
        ///
        /// Edits the route.
        /// </summary>
        /// <param name="oldRoute">The old route.</param>
        /// <param name="newRoute">The new route.</param>
        /// <returns>A bool.</returns>
        bool EditRoute(RouteVM oldRoute, RouteVM newRoute);
        void UpdateTickets(List<Ticket> tickets);
        RouteVM AddRoute(DateTime routeDate, string licensePlateNumber, int driverID);
        RouteVM RetrieveRouteByID(int id);
        TicketMetaData GetTicketMetaData();
    }
}
