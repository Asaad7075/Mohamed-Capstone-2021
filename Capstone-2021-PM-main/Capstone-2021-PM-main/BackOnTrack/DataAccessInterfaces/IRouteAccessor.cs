using DomainModels;
using DomainModels.Tickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    /// <summary>
    /// Zach Stultz
    /// Created: 2021/02/26
    ///
    /// Route Data Access Layer Interface for Back-On-track.
    /// <summary>
    public interface IRouteAccessor
    {
        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/16
        ///
        /// Adds the route.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <returns>A bool.</returns>
        bool InsertRoute(RouteVM route);

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
        List<RouteVM> SelectRoutesByDate(DateTime date);

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/26
        ///
        /// Retrieves the routes by driver id and date.
        /// </summary>
        /// <param name="driverID">The driver id.</param>
        /// <param name="date">The date.</param>
        /// <returns>A list of Routes.</returns>
        List<RouteVM> SelectRoutesByDriverID(int driverID);

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/26
        ///
        /// Selects the all routes.
        /// </summary>
        /// <returns>A list of Routes.</returns>
        List<RouteVM> SelectAllRoutes();

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/04/08
        ///
        /// Updates the route.
        /// </summary>
        /// <param name="oldRoute">The old route.</param>
        /// <param name="newRoute">The new route.</param>
        /// <returns>A bool.</returns>
        int UpdateRoute(RouteVM oldRoute, RouteVM newRoute);
        int SelectNextRouteID(DateTime routeDate);
        RouteVM SelectRouteByID(int id);

        int InsertRoute(DateTime routeDate, string licensePlateNumber, int driverID);
        TicketMetaData GetTicketMetaData();
    }
}
