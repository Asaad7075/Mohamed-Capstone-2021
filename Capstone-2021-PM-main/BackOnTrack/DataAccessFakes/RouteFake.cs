using DataAccessInterfaces;
using DomainModels;
using DomainModels.Tickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessFakes
{
    /// <summary>
    /// Zach Stultz
    /// Created: 2021/02/24
    ///
    /// Route Fake Class.
    /// </summary>
    public class RouteFake : IRouteAccessor
    {
        private List<RouteVM> _routes = new List<RouteVM>();

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/02/24
        ///
        /// Fake data for test cases.
        /// </summary>
        public RouteFake()
        {
            _routes.Add(new RouteVM
            {
                RouteID = 100000,
                DateOfRoute = new DateTime(2020, 02, 05),
                DriverEmployeeID = 185215,
                Active = true,
                LicensePlateNumber = "L-527N"
            });
            _routes.Add(new RouteVM
            {
                RouteID = 100001,
                DateOfRoute = new DateTime(2020, 02, 04),
                DriverEmployeeID = 185216,
                Active = true,
                LicensePlateNumber = "L-932D"
            });
            _routes.Add(new RouteVM
            {
                RouteID = 100002,
                DateOfRoute = new DateTime(2020, 02, 03),
                DriverEmployeeID = 185217,
                Active = true,
                LicensePlateNumber = "L-595U"
            });
            _routes.Add(new RouteVM
            {
                RouteID = 100003,
                DateOfRoute = new DateTime(2020, 02, 02),
                DriverEmployeeID = 185218,
                Active = true,
                LicensePlateNumber = "L-5351"
            });
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
            int foundAt = 9999999;
            for (int i = 0; i < _routes.Count; i++)
            {
                if (
                    route.DateOfRoute == _routes[i].DateOfRoute &&
                    route.DriverEmployeeID == _routes[i].DriverEmployeeID &&
                    route.Active == _routes[i].Active &&
                    route.LicensePlateNumber == _routes[i].LicensePlateNumber)
                {
                    found = true;
                    foundAt = i;
                }
            }
            if (found)
            {
                _routes.RemoveAt(foundAt);
                result = 1;
            }
            else
            {
                throw new Exception("No matching route exists in the database.");
            }
            return result;
        }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/26
        ///
        /// Inserts the route.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <returns>A bool.</returns>
        public bool InsertRoute(RouteVM route)
        {
            bool result = false;
            bool duplicate = false;

            for (int i = 0; i < _routes.Count; i++)
            {
                if (
                    route.RouteID == _routes[i].RouteID)
                {
                    duplicate = true;
                }
            }
            if (duplicate.Equals(true))
            {
                throw new Exception("Route already exists in the database.");
            }
            else
            {
                _routes.Add(route);
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/26
        ///
        /// Selects the routes by date.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>A list of Routes.</returns>
        public List<RouteVM> SelectRoutesByDate(DateTime date)
        {
            List<RouteVM> data = new List<RouteVM>();
            foreach (RouteVM item in _routes)
            {
                if (item.DateOfRoute.Equals(date))
                {
                    data.Add(item);
                }
            }
            return data;
        }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/26
        ///
        /// Selects the routes by driver id and date.
        /// </summary>
        /// <param name="driverID">The driver id.</param>
        /// <param name="date">The date.</param>
        /// <returns>A list of Routes.</returns>
        public List<RouteVM> SelectRoutesByDriverID(int driverID)
        {
            List<RouteVM> data = new List<RouteVM>();
            foreach (RouteVM item in _routes)
            {
                if (item.DriverEmployeeID.Equals(driverID))
                {
                    data.Add(item);
                }
            }
            return data;
        }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/26
        ///
        /// Selects all the routes.
        /// </summary>
        /// <returns>A list of Routes.</returns>
        public List<RouteVM> SelectAllRoutes()
        {
            return _routes;
        }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/04/08
        ///
        /// Updates the route.
        /// </summary>
        /// <param name="oldRoute">The old route.</param>
        /// <param name="newRoute">The new route.</param>
        /// <returns>A bool.</returns>
        public int UpdateRoute(RouteVM oldRoute, RouteVM newRoute)
        {
            int result = 0;
            if (oldRoute.RouteID == newRoute.RouteID)
            {
                try
                {
                    foreach (var v in _routes)
                    {
                        if (oldRoute.RouteID == newRoute.RouteID)
                        {
                            oldRoute.DateOfRoute = newRoute.DateOfRoute;
                            oldRoute.DriverEmployeeID = newRoute.DriverEmployeeID;
                            oldRoute.Active = newRoute.Active;
                            oldRoute.LicensePlateNumber = newRoute.LicensePlateNumber;
                            result = 1;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return result;
        }
        /// <summary>
        /// Jakub Kawski
        /// 2021/04/28
        /// </summary>
        /// <param name="routeDate"></param>
        /// <returns></returns>
        public int SelectNextRouteID(DateTime routeDate)
        {
            int id = 100000 + _routes.Count;
            _routes.Add(new RouteVM
            {
                RouteID = id,
                DateOfRoute = routeDate
            });
            return id;
        }
        /// <summary>
        /// Jakub Kawski
        /// 2021/04/28
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RouteVM SelectRouteByID(int id)
        {
            return _routes.First(r => r.RouteID == id);
        }
        /// <summary>
        /// Jakub Kawski
        /// 2021/04/28
        /// </summary>
        /// <param name="routeDate"></param>
        /// <param name="licensePlateNumber"></param>
        /// <param name="driverID"></param>
        /// <returns></returns>
        public int InsertRoute(DateTime routeDate, string licensePlateNumber, int driverID)
        {
            int id = 100000 + _routes.Count;
            _routes.Add(new RouteVM
            {
                RouteID = id,
                DateOfRoute = routeDate,
                LicensePlateNumber = licensePlateNumber,
                DriverEmployeeID = driverID
            });
            return id;
        }
        /// <summary>
        /// Jakub Kawski
        /// 2021/04/28
        /// </summary>
        /// <returns></returns>
        public TicketMetaData GetTicketMetaData()
        {
            return new TicketMetaData()
            {
                TotalUnassigned = 15,
                DeliveryUnassigned = 5,
                PickupUnassigned = 5,
                RideUnassigned = 5
            };
        }
    }
}
