using DataAccessInterfaces;
using DomainModels;
using DomainModels.Tickets;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    /// <summary>
    /// Zach Stultz
    /// Created: 2021/03/26
    ///
    /// The Route Data Accessors class.
    /// </summary>
    public class RouteAccessor : IRouteAccessor
    {
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
            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_delete_route", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@RouteID", SqlDbType.Int);
            cmd.Parameters["@RouteID"].Value = route.RouteID;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
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

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_insert_route", conn);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.Add("@DateOfRoute", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@DriverEmployeeID", SqlDbType.Int);
            cmd.Parameters.Add("@Active", SqlDbType.Bit);
            cmd.Parameters.Add("@LicensePlateNumber", SqlDbType.NVarChar, 10);

            cmd.Parameters["@DateOfRoute"].Value = route.DateOfRoute;
            cmd.Parameters["@DriverEmployeeID"].Value = route.DriverEmployeeID;
            cmd.Parameters["@Active"].Value = route.Active;
            cmd.Parameters["@LicensePlateNumber"].Value = route.LicensePlateNumber;

            try
            {
                conn.Open();
                int currResult = cmd.ExecuteNonQuery();
                if (currResult == 1)
                {
                    result = true;
                }
                else if (currResult == 0)
                {
                    throw new ApplicationException("The route could not be added.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/26
        ///
        /// Selects the all routes.
        /// </summary>
        /// <returns>A list of Routes.</returns>
        public List<RouteVM> SelectAllRoutes()
        {
            List<RouteVM> data = new List<RouteVM>();

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_select_all_routes", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                data = CreateListFromReader(reader);
                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return data;
        }
        /// <summary>
        /// Jakub Kawski
        /// 2021/24/04
        /// 
        /// get the next route
        /// </summary>
        /// <param name="routeDate"></param>
        /// <returns></returns>
        public int SelectNextRouteID(DateTime routeDate)
        {
            int id = -1;
            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_select_next_routeID", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@DateOfRoute", SqlDbType.DateTime);
            cmd.Parameters["@DateOfRoute"].Value = routeDate;

            List<RouteVM> data = new List<RouteVM>();

            try
            {
                conn.Open();
                var ob = cmd.ExecuteScalar();
                id = Int32.Parse(ob.ToString());
                if(id < 2)
                {
                    throw new ApplicationException("Could not select next RouteID");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return id;
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
            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_select_route_by_date", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@DateOfRoute", SqlDbType.NVarChar);
            cmd.Parameters["@DateOfRoute"].Value = date;

            List<RouteVM> data = new List<RouteVM>();

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                data = CreateListFromReader(reader);
                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return data;
        }


        /// <summary>
        /// Jakub Kawski
        /// Created: 2021/04/28
        ///
        /// Select route by id
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>A list of Routes.</returns>
        public RouteVM SelectRouteByID(int id)
        {
            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_select_route_by_id", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RouteID", id);

            RouteVM route;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                var result = CreateListFromReader(reader);              
                reader.Close();
                if (result.Count == 1)
                {
                    route = result[0];
                }
                else
                {
                    throw new ApplicationException("Error: SelectRouteByID failed");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return route;
        }



        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/26
        ///
        /// Selects the routes by driver id.
        /// </summary>
        /// <param name="driverID">The driver id.</param>
        /// <param name="date">The date.</param>
        /// <returns>A list of Routes.</returns>
        public List<RouteVM> SelectRoutesByDriverID(int driverID)
        {
            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_select_route_by_driver_id", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@DriverEmployeeID", SqlDbType.Int);
            cmd.Parameters["@DriverEmployeeID"].Value = driverID;

            List<RouteVM> data = new List<RouteVM>();

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                data = CreateListFromReader(reader);
                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return data;
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

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_update_route", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@RouteID", SqlDbType.Int);

            cmd.Parameters.Add("@OldDateOfRoute", SqlDbType.DateTime);
            cmd.Parameters.Add("@OldDriverEmployeeID", SqlDbType.Int);
            cmd.Parameters.Add("@OldActive", SqlDbType.Bit);
            cmd.Parameters.Add("@OldLicensePlateNumber", SqlDbType.NVarChar);

            cmd.Parameters.Add("@NewDateOfRoute", SqlDbType.DateTime);
            cmd.Parameters.Add("@NewDriverEmployeeID", SqlDbType.Int);
            cmd.Parameters.Add("@NewActive", SqlDbType.Bit);
            cmd.Parameters.Add("@NewLicensePlateNumber", SqlDbType.NVarChar);

            cmd.Parameters["@RouteID"].Value = oldRoute.RouteID;

            cmd.Parameters["@OldDateOfRoute"].Value = oldRoute.DateOfRoute;
            cmd.Parameters["@OldDriverEmployeeID"].Value = oldRoute.DriverEmployeeID;
            cmd.Parameters["@OldActive"].Value = oldRoute.Active;
            cmd.Parameters["@OldLicensePlateNumber"].Value = oldRoute.LicensePlateNumber;

            cmd.Parameters["@NewDateOfRoute"].Value = newRoute.DateOfRoute;
            cmd.Parameters["@NewDriverEmployeeID"].Value = newRoute.DriverEmployeeID;
            cmd.Parameters["@NewActive"].Value = newRoute.Active;
            cmd.Parameters["@NewLicensePlateNumber"].Value = newRoute.LicensePlateNumber;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
        private List<RouteVM> CreateListFromReader(SqlDataReader reader)
        {
            List<RouteVM> routes = new List<RouteVM>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    RouteVM route = new RouteVM()
                    {
                        RouteID = reader.GetInt32(0),
                        DateOfRoute = reader.GetDateTime(1),
                        DriverEmployeeID = reader.IsDBNull(2) ? null : reader.GetInt32(2),
                        Active = reader.GetBoolean(3),
                        LicensePlateNumber = reader.IsDBNull(4) ? "" : reader.GetString(4),
                        DriverName = reader.GetString(5)
                    };
                    routes.Add(route);
                }
            }
            return routes;
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
            int id = -1;
            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_insert_route", conn);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.Add("@DateOfRoute", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@DriverEmployeeID", SqlDbType.Int);
            cmd.Parameters.Add("@Active", SqlDbType.Bit);
            cmd.Parameters.Add("@LicensePlateNumber", SqlDbType.NVarChar, 10);

            cmd.Parameters["@DateOfRoute"].Value = routeDate;
            cmd.Parameters["@DriverEmployeeID"].Value = driverID;
            cmd.Parameters["@Active"].Value = 1;
            cmd.Parameters["@LicensePlateNumber"].Value = licensePlateNumber;

            try
            {
                conn.Open();
                var ob = cmd.ExecuteScalar();
                id = Int32.Parse(ob.ToString());
                if (id < 2)
                {
                    throw new ApplicationException("Could not select next RouteID");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return id;
        }
        /// <summary>
        /// Jakub Kawski
        /// 2021/04/28
        /// </summary>
        /// <returns></returns>
        public TicketMetaData GetTicketMetaData()
        {
            TicketMetaData ticketMetaData = null;
            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_get_ticket_meta_data", conn);
            cmd.CommandType = CommandType.StoredProcedure;


            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ticketMetaData = new TicketMetaData()
                        {
                            TotalUnassigned = reader.GetInt32(0),
                            DeliveryUnassigned = reader.GetInt32(1),
                            PickupUnassigned = reader.GetInt32(2),
                            RideUnassigned = reader.GetInt32(3)
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return ticketMetaData;
        }
    }
}
