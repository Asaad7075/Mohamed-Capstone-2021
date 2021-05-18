/// Jakub Kawski
/// Created: 2021/02/18
/// 
/// 
/// </summary>
///
/// <remarks>
/// Updater Name
/// Updated: yyyy/mm/dd
/// </remarks>
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
    /// Jakub Kawski
    /// 2021/02/18
    /// 
    /// Uses and implements CRUD functions for delivery tickets.
    /// </summary>
    public class DeliveryTicketAccessor : IDeliveryTicketAccessor
    {
        /// Jakub Kawski
        /// Created: 2021/02/18
        /// 
        /// 
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// </remarks>
        public int DeleteDeliveryTicket(DeliveryTicketVM ticket)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_delete_delivery_ticket", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@TicketID", ticket.TicketID);

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
                if (result == 0)
                {
                    throw new ApplicationException("Deletion failed.");
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
        /// Jakub Kawski
        /// Created: 2021/02/18
        /// 
        /// Inserts a delivery ticket into the database and returns
        /// an int, int result is the delivery ticket's ticketID
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// </remarks>
        public int InsertDeliveryTicket(DeliveryTicketVM ticket)
        {

            int result = 0;

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_insert_delivery_ticket", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@OrderID", ticket.OrderID);
            cmd.Parameters.AddWithValue("@GeoID", ticket.GeoID);

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
                if (result == 0)
                {
                    throw new ApplicationException("Failed adding delivery ticket to DB.");
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
        /// Jakub Kawski
        /// Created: 2021/02/18
        /// 
        /// Gets all deliverytickets from the database.
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// </remarks>
        public List<DeliveryTicketVM> SelectAllDeliveryTickets()
        {
            List<DeliveryTicketVM> tickets = new List<DeliveryTicketVM>();

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_Select_All_Delivery_Tickets", conn);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                tickets = CreateListFromReader(reader);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Could not access data base for delivery tickets", ex);
            }
            finally
            {
                conn.Close();
            }
            return tickets;
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/12
        /// 
        /// Retrieves a delivery ticket by
        /// it's order id.
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public DeliveryTicketVM SelectDeliveryTicketByOrderID(int orderID)
        {
            DeliveryTicketVM deliveryTicket = null;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("dbo.sp_select_delivery_ticket_by_order_id", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@OrderID", SqlDbType.Int);
            cmd.Parameters["@OrderID"].Value = orderID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        deliveryTicket = new DeliveryTicketVM()
                        {
                            TicketID = reader.GetInt32(0),
                            OrderID = reader.GetInt32(1),
                            GeoID = reader.GetInt32(2),
                            RouteID = reader.IsDBNull(3) ? null : reader.GetInt32(3),
                            StopNumber = reader.GetInt32(4),
                            CreatedAt = reader.GetDateTime(5),
                            StatusID = reader.GetInt32(6),
                            StatusDescription = reader.GetString(7),
                            Notes = reader.GetString(8),
                            EstimatedArrival = reader.GetDateTime(9),
                            ZipCode = reader.GetString(10),
                            StreetAddressLineOne = reader.GetString(11),
                            StreetAddressLineTwo = reader.GetString(12),
                            City = reader.GetString(13),
                            State = reader.GetString(14),
                            ClientFirstName = reader.GetString(15),
                            ClientLastName = reader.GetString(16)
                        };
                    }
                }
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

            return deliveryTicket;
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/08
        /// 
        /// Retrieves tickets by clients id number.
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public List<DeliveryTicketVM> SelectDeliveryTicketsByClientId(int clientId)
        {
            List<DeliveryTicketVM> tickets = new List<DeliveryTicketVM>();

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("dbo.sp_select_delivery_tickets_by_client_id", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ClientId", SqlDbType.Int);
            cmd.Parameters["@ClientId"].Value = clientId;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                tickets = CreateListFromReader(reader);


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

            return tickets;
        }

        public List<DeliveryTicketVM> SelectDeliveryTicketsByRouteID(int routeID)
        {
            List<DeliveryTicketVM> tickets = new List<DeliveryTicketVM>();

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_select_delivery_ticket_by_routeID", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@RouteID", routeID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                tickets = CreateListFromReader(reader);


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

            return tickets;
        }

        /// Jakub Kawski
        /// Created: 2021/02/18
        /// 
        /// 
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// </remarks>
        public int UpdateDeliveryTicket(DeliveryTicketVM newTicket, DeliveryTicketVM oldTicket)
        {
            int result = 0;
            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_update_delivery_ticket", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@TicketID", oldTicket.TicketID);
            cmd.Parameters.AddWithValue("@OldOrderID", oldTicket.OrderID);
            cmd.Parameters.AddWithValue("@OldGeoID", oldTicket.GeoID);
            if(oldTicket.RouteID == null)
            {
                cmd.Parameters.AddWithValue("@OldRouteID", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@OldRouteID", oldTicket.RouteID);
            }          
            cmd.Parameters.AddWithValue("@OldStopNumber", oldTicket.StopNumber);
            cmd.Parameters.Add("@OldEstimatedArrival", SqlDbType.DateTime);
            cmd.Parameters["@OldEstimatedArrival"].Value = oldTicket.EstimatedArrival;
            cmd.Parameters.AddWithValue("OldStatusID", oldTicket.StatusID);
            cmd.Parameters.Add("@OldNotes", SqlDbType.NVarChar, 500);
            cmd.Parameters["@OldNotes"].Value = oldTicket.Notes;

            cmd.Parameters.AddWithValue("@NewOrderID", newTicket.OrderID);
            cmd.Parameters.AddWithValue("@NewGeoID", newTicket.GeoID);
            if (newTicket.RouteID == null)
            {
                cmd.Parameters.AddWithValue("@NewRouteID", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@NewRouteID", newTicket.RouteID);
            }          
            cmd.Parameters.AddWithValue("@NewStopNumber", newTicket.StopNumber);
            cmd.Parameters.Add("@NewEstimatedArrival", SqlDbType.DateTime);
            cmd.Parameters["@NewEstimatedArrival"].Value = newTicket.EstimatedArrival;
            cmd.Parameters.AddWithValue("NewStatusID", newTicket.StatusID);
            cmd.Parameters.Add("@NewNotes", SqlDbType.NVarChar, 500);
            cmd.Parameters["@NewNotes"].Value = newTicket.Notes;



            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
                if (result < 1)
                {
                    throw new ApplicationException("Failed updating delivery ticket.");
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

        private List<DeliveryTicketVM> CreateListFromReader(SqlDataReader reader)
        {
            List<DeliveryTicketVM> tickets = new List<DeliveryTicketVM>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    DeliveryTicketVM ticket = new DeliveryTicketVM()
                    {
                        TicketID = reader.GetInt32(0),
                        OrderID = reader.GetInt32(1),
                        GeoID = reader.GetInt32(2),
                        RouteID = reader.IsDBNull(3) ? null : reader.GetInt32(3),
                        StopNumber = reader.GetInt32(4),
                        CreatedAt = reader.GetDateTime(5),
                        StatusID = reader.GetInt32(6),
                        StatusDescription = reader.GetString(7),
                        Notes = reader.GetString(8),
                        EstimatedArrival = reader.GetDateTime(9),
                        ZipCode = reader.GetString(10),
                        StreetAddressLineOne = reader.GetString(11),
                        StreetAddressLineTwo = reader.GetString(12),
                        City = reader.GetString(13),
                        State = reader.GetString(14),
                        ClientFirstName = reader.GetString(15),
                        ClientLastName = reader.GetString(16)
                    };
                    tickets.Add(ticket);
                }
            }
            return tickets;
        }
    }
}
