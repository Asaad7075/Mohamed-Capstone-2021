using DataAccessInterfaces;
using DomainModels.Tickets;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DataAccessLayer
{
    public class PickUpTicketAccessor : IPickUpTicketAccessor
    {
        /// <summary>
        /// Jakub Kawski
        /// 
        /// Delete a ticket
        /// 2021/03/19
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        public int DeletePickUpTicket(PickUpTicketVM ticket)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_delete_pickup_ticket", conn);

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
        /// 2021/03/15
        /// 
        /// Insert a new pickup ticket into
        /// the database.
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        public int InsertPickUpTicket(PickUpTicketVM ticket)
        {
            int result = 0;
            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_Insert_PickUp_Ticket", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@DonationID", ticket.DonationID);
            cmd.Parameters.AddWithValue("@GeoID", ticket.GeoID);
            cmd.Parameters.Add("@TimeRangeStart", SqlDbType.Time);
            cmd.Parameters.Add("@TimeRangeEnd", SqlDbType.Time);
            cmd.Parameters.Add("@RequestDateStart", SqlDbType.Date);
            cmd.Parameters.Add("@RequestDateEnd", SqlDbType.Date);
            cmd.Parameters["@TimeRangeStart"].Value = ticket.TimeRangeStart;
            cmd.Parameters["@TimeRangeEnd"].Value = ticket.TimeRangeEnd;
            cmd.Parameters["@RequestDateStart"].Value = ticket.RequestDateStart;
            cmd.Parameters["@RequestDateEnd"].Value = ticket.RequestDateEnd;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
                if(result < 1)
                {
                    throw new ApplicationException("The insertion failed to at to the database.");
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
        /// 2021/03/12
        /// 
        /// Selects all tickets from the database and returns them
        /// as a list collection.
        /// </summary>
        /// <returns></returns>
        public List<PickUpTicketVM> SelectAllPickUpTickets()
        {
            List<PickUpTicketVM> tickets = new List<PickUpTicketVM>();

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_Select_All_PickUp_Tickets", conn);

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

        /// <summary>
        /// Jakub Kawski
        /// 2021/19/03
        /// 
        /// Update a ticket;
        /// </summary>
        /// <param name="newTicket"></param>
        /// <param name="oldTicket"></param>
        /// <returns></returns>
        public int UpdatePickUpTicket(PickUpTicketVM newTicket, PickUpTicketVM oldTicket)
        {
            int result = 0;
            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_update_pickup_ticket", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@TicketID", oldTicket.TicketID);
            cmd.Parameters.AddWithValue("@OldDonationID", oldTicket.DonationID);
            cmd.Parameters.AddWithValue("@OldGeoID", oldTicket.GeoID);
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
            cmd.Parameters.Add("@OldRequestDateStart", SqlDbType.Date);
            cmd.Parameters["@OldRequestDateStart"].Value = oldTicket.RequestDateStart;
            cmd.Parameters.Add("@OldRequestDateEnd", SqlDbType.Date);
            cmd.Parameters["@OldRequestDateEnd"].Value = oldTicket.RequestDateEnd;

            cmd.Parameters.AddWithValue("@NewDonationID", newTicket.DonationID);
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
            cmd.Parameters.Add("@NewTimeRangeStart", SqlDbType.Time);
            cmd.Parameters["@NewTimeRangeStart"].Value = newTicket.TimeRangeStart;
            cmd.Parameters.Add("@NewTimeRangeEnd", SqlDbType.Time);
            cmd.Parameters["@NewTimeRangeEnd"].Value = newTicket.TimeRangeEnd;
            cmd.Parameters.Add("@NewRequestDateStart", SqlDbType.Date);
            cmd.Parameters["@NewRequestDateStart"].Value = newTicket.RequestDateStart;
            cmd.Parameters.Add("@NewRequestDateEnd", SqlDbType.Date);
            cmd.Parameters["@NewRequestDateEnd"].Value = newTicket.RequestDateEnd;



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
        /// <summary>
        /// Jakub Kawski
        /// 2021/04/16
        /// 
        /// Selects tickets from db bases on routeID
        /// </summary>
        /// <returns></returns>
        public List<PickUpTicketVM> SelectPickUpTicketsByRouteID(int routeID)
        {
            List<PickUpTicketVM> tickets = new List<PickUpTicketVM>();

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_select_pickup_ticket_by_routeID", conn);

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
        /// Jakub Kawski
        /// 2021/04/18
        /// 
        /// Get a list of pickup tickets where the date param fits between
        /// the tickets timerange.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<PickUpTicketVM> SelectPickUpTicketsByDate(DateTime date)
        {
            List<PickUpTicketVM> tickets;

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_select_pickup_ticket_by_date", conn);

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
        /// <summary>
        /// Jakub Kawski
        /// 2021/04/18
        /// 
        /// Gets list of pickup tickets from reader. Easier to edit.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private List<PickUpTicketVM> CreateListFromReader(SqlDataReader reader)
        {
            List<PickUpTicketVM> tickets = new List<PickUpTicketVM>();
            try
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        PickUpTicketVM ticket = new PickUpTicketVM()
                        {
                            TicketID = reader.GetInt32(0),
                            DonationID = reader.GetInt32(1),
                            GeoID = reader.GetInt32(2),
                            TimeRangeStart = reader.GetTimeSpan(3),
                            TimeRangeEnd = reader.GetTimeSpan(4),
                            RequestDateStart = reader.GetDateTime(5),
                            RequestDateEnd = reader.GetDateTime(6),
                            RouteID = reader.IsDBNull(7) ? null : reader.GetInt32(7),
                            StopNumber = reader.GetInt32(8),
                            CreatedAt = reader.GetDateTime(9),
                            StatusID = reader.GetInt32(10),
                            StatusDescription = reader.GetString(11),
                            Notes = reader.GetString(12),
                            EstimatedArrival = reader.GetDateTime(13),
                            ZipCode = reader.GetString(14),
                            StreetAddressLineOne = reader.GetString(15),
                            StreetAddressLineTwo = reader.GetString(16),
                            City = reader.GetString(17),
                            State = reader.GetString(18)
                        };
                        tickets.Add(ticket);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return tickets;
        }
    }
}
