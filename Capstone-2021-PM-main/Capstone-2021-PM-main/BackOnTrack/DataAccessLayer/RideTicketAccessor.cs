using DataAccessInterfaces;
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
    public class RideTicketAccessor : IRideTicketAccessor
    {
        /// <summary>
        /// Jakub Kawski
        /// 2021/03/26
        /// 
        /// Remove a ride ticket from the db
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        public int DeleteRideTicket(RideTicketVM ticket)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_delete_ride_ticket", conn);

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
        /// 2021/03/26
        /// 
        /// Insert a new ride ticket
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        public int InsertRideTicket(RideTicketVM ticket)
        {
            int result = 0;
            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_Insert_Ride_Ticket", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            
            cmd.Parameters.AddWithValue("@ClientID", ticket.ClientID);
            cmd.Parameters.AddWithValue("@FetchGeoID", ticket.FetchGeoID);
            cmd.Parameters.AddWithValue("@DestinationGeoID", ticket.DestinationGeoID);
            cmd.Parameters.Add("@TimeRangeStart", SqlDbType.Time);
            cmd.Parameters.Add("@TimeRangeEnd", SqlDbType.Time);
            cmd.Parameters.Add("@DateOfRide", SqlDbType.Date);
            cmd.Parameters.Add("@RequiresHandicapAccessibleVehicle", SqlDbType.Bit);
            cmd.Parameters["@TimeRangeStart"].Value = ticket.TimeRangeStart;
            cmd.Parameters["@TimeRangeEnd"].Value = ticket.TimeRangeEnd;
            cmd.Parameters["@DateOfRide"].Value = ticket.DateOfRide;
            cmd.Parameters["@RequiresHandicapAccessibleVehicle"].Value = ticket.RequiresHAV;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
                if (result < 1)
                {
                    throw new ApplicationException("The insertion failed to add to the database.");
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
        /// 2021/03/26
        /// 
        /// Select all ride tickets
        /// </summary>
        /// <returns></returns>
        public List<RideTicketVM> SelectAllRideTickets()
        {
            List<RideTicketVM> tickets;

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_Select_All_Ride_Tickets", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                tickets = CreateListFromReader(reader);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Could not access data base for pickup tickets", ex);
            }
            finally
            {
                conn.Close();
            }
            return tickets;
        }

        public List<RideTicketVM> SelectRideTicketsByDate(DateTime date)
        {
            List<RideTicketVM> tickets;

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_select_ride_ticket_by_date", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@SelectedDate", SqlDbType.Date);
            cmd.Parameters["@SelectedDate"].Value = date;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                tickets = CreateListFromReader(reader);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Could not access data base for pickup tickets", ex);
            }
            finally
            {
                conn.Close();
            }
            return tickets;
        }

        public List<RideTicketVM> SelectRideTicketsByRouteID(int routeID)
        {
            List<RideTicketVM> tickets;

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_select_ride_ticket_by_routeID", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@RouteID", routeID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                tickets = CreateListFromReader(reader);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Could not access data base for pickup tickets", ex);
            }
            finally
            {
                conn.Close();
            }
            return tickets;
        }



        /// <summary>
        /// Jakub KAwski
        /// 2021/03/26
        /// 
        /// Update a ride ticket with a new ticket
        /// </summary>
        /// <param name="newTicket"></param>
        /// <param name="oldTicket"></param>
        /// <returns></returns>
        public int UpdateRideTicket(RideTicketVM newTicket, RideTicketVM oldTicket)
        {
            int result = 0;
            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_update_ride_ticket", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@TicketID", oldTicket.TicketID);
            cmd.Parameters.AddWithValue("@OldClientID", oldTicket.ClientID);
            cmd.Parameters.AddWithValue("@OldFetchGeoID", oldTicket.FetchGeoID);
            cmd.Parameters.AddWithValue("@OldDestinationGeoID", oldTicket.DestinationGeoID);
            if (oldTicket.RouteID == null)
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
            cmd.Parameters.Add("@OldTimeRangeStart", SqlDbType.Time);
            cmd.Parameters["@OldTimeRangeStart"].Value = oldTicket.TimeRangeStart;
            cmd.Parameters.Add("@OldTimeRangeEnd", SqlDbType.Time);
            cmd.Parameters["@OldTimeRangeEnd"].Value = oldTicket.TimeRangeEnd;
            cmd.Parameters.Add("@OldDateOfRide", SqlDbType.Date);
            cmd.Parameters["@OldDateOfRide"].Value = oldTicket.DateOfRide;
            cmd.Parameters.Add("@OldRequiresHandicapAccessibleVehicle", SqlDbType.Bit);
            cmd.Parameters["@OldRequiresHandicapAccessibleVehicle"].Value = oldTicket.RequiresHAV;

            cmd.Parameters.AddWithValue("@NewClientID", newTicket.ClientID);
            cmd.Parameters.AddWithValue("@NewFetchGeoID", newTicket.FetchGeoID);
            cmd.Parameters.AddWithValue("@NewDestinationGeoID", newTicket.DestinationGeoID);
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
            cmd.Parameters.Add("@NewTimeRangeStart", SqlDbType.Time);
            cmd.Parameters["@NewTimeRangeStart"].Value = newTicket.TimeRangeStart;
            cmd.Parameters.Add("@NewTimeRangeEnd", SqlDbType.Time);
            cmd.Parameters["@NewTimeRangeEnd"].Value = newTicket.TimeRangeEnd;
            cmd.Parameters.Add("@NewDateOfRide", SqlDbType.Date);
            cmd.Parameters["@NewDateOfRide"].Value = newTicket.DateOfRide;
            cmd.Parameters.Add("@NewRequiresHandicapAccessibleVehicle", SqlDbType.Bit);
            cmd.Parameters["@NewRequiresHandicapAccessibleVehicle"].Value = oldTicket.RequiresHAV;



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
        private List<RideTicketVM> CreateListFromReader(SqlDataReader reader)
        {
            List<RideTicketVM> tickets = new List<RideTicketVM>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    RideTicketVM ticket = new RideTicketVM()
                    {
                        TicketID = reader.GetInt32(0),
                        ClientID = reader.GetInt32(1),
                        FetchGeoID = reader.GetInt32(2),
                        DestinationGeoID = reader.GetInt32(3),
                        TimeRangeStart = reader.GetTimeSpan(4),
                        TimeRangeEnd = reader.GetTimeSpan(5),
                        DateOfRide = reader.GetDateTime(6),
                        RequiresHAV = reader.GetBoolean(7),
                        RouteID = reader.IsDBNull(8) ? null : reader.GetInt32(8),
                        StopNumber = reader.GetInt32(9),
                        CreatedAt = reader.GetDateTime(10),
                        StatusID = reader.GetInt32(11),
                        StatusDescription = reader.GetString(12),
                        Notes = reader.GetString(13),
                        EstimatedArrival = reader.GetDateTime(14),
                        FetchZipCode = reader.GetString(15),
                        FetchStreetAddressLineOne = reader.GetString(16),
                        FetchStreetAddressLineTwo = reader.GetString(17),
                        FetchCity = reader.GetString(18),
                        FetchState = reader.GetString(19),
                        DestinationZipCode = reader.GetString(20),
                        DestinationStreetAddressLineOne = reader.GetString(21),
                        DestinationStreetAddressLineTwo = reader.GetString(22),
                        DestinationCity = reader.GetString(23),
                        DestinationState = reader.GetString(24),
                        ClientFirstName = reader.GetString(25),
                        ClientLastName = reader.GetString(26)
                    };
                    tickets.Add(ticket);
                }
            }
            return tickets;
        }
    }
}
