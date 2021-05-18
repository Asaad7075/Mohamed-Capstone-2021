using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModels;
using DomainModels.Tickets;
using DataAccessInterfaces;
using System.Data.SqlClient;
using System.Data;

namespace DataAccessLayer
{
    /// <summary>
    /// Nate Hepker
    /// Created: 2021/03/07
    /// 
    /// Accessor class for the RideReview that interacts with DB using CRUD funtions
    /// </summary>
    public class RideReviewAccessor : IRideReviewAccessor
    {
        /// <summary>
        /// Nate Hepker
        /// Created: 2021//04/03
        /// 
        /// Deletes the RideRreview that was selected form the DB
        /// </summary>
        /// <param name="rideReview"></param>
        /// <returns></returns>
        public int DeleteRideReviewFromDriver(RideReviewVM rideReview)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_delete_ride_review_from_driver", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@RideReviewID", SqlDbType.Int);
            cmd.Parameters["@RideReviewID"].Value = rideReview.RideReviewID;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
                if (result != 1)
                {
                    throw new ApplicationException("Failed to delete the ride review");
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
        /// Nate Hepker
        /// Created: 2021/03/20
        /// 
        /// Inserts a new ride review into the DB
        /// </summary>
        public int InsertRideReview(RideReview rideReview)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_insert_ride_review", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@TicketID", SqlDbType.Int);
            cmd.Parameters.Add("@ClientID", SqlDbType.Int);
            cmd.Parameters.Add("@DriverID", SqlDbType.Int);
            cmd.Parameters.Add("@DriverClientRating", SqlDbType.Int);
            cmd.Parameters.Add("@DriverComment", SqlDbType.NVarChar, 500);

            cmd.Parameters["@TicketID"].Value = rideReview.TicketID;
            cmd.Parameters["@ClientID"].Value = rideReview.ClientID;
            cmd.Parameters["@DriverID"].Value = rideReview.DriverID;
            cmd.Parameters["@DriverClientRating"].Value = rideReview.DriverClientRating;
            cmd.Parameters["@DriverComment"].Value = rideReview.DriverComment;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
                if (result == 0)
                {
                    throw new ApplicationException("The ride review could not be added.");
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
        /// Nate Hepker
        /// Created: 2021/03/20
        /// updated: 2021/04/23
        /// 
        /// Selects all the RideReviews and returns it to the RideReviewManager
        /// </summary>
        public List<RideReviewVM> SelectAllRideReviews()
        {
            List<RideReviewVM> rideReviews = new List<RideReviewVM>();
            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_select_all_driver_ride_reviews", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var rideReview = new RideReviewVM()
                        {
                            RideReviewID = reader.GetInt32(0),
                            TicketID = reader.GetInt32(1),
                            DriverID = reader.GetInt32(2),
                            EmployeeFirstName = reader.GetString(3),
                            EmployeeLastName = reader.GetString(4),
                            DriverClientRating = reader.GetInt32(5),
                            DriverComment = reader.GetString(6),
                            ClientID = reader.GetInt32(7),
                            ClientFirstName = reader.GetString(8),
                            ClientLastName = reader.GetString(9),
                            //ClientComment = /*reader.IsDBNull(10) ? null :*/ reader.GetString(10)
                        };
                        rideReviews.Add(rideReview);
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
            return rideReviews;
        }

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/03/20
        /// 
        /// Selects the ride tickets for the driver that is actively logged in.
        /// Returns a list of the ride tickets.
        /// </summary>
        public List<RideReviewVM> SelectTicketsByEmployeeID(int employeeID)
        {
            List<RideReviewVM> rideTickets = new List<RideReviewVM>();
            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_select_tickets_by_employee_id", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmployeeID", employeeID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var rideTicket = new RideReviewVM()
                        {
                            TicketID = reader.GetInt32(0),
                            TicketType = reader.GetInt32(1),
                            DriverID = reader.GetInt32(2),
                            EmployeeFirstName = reader.GetString(3),
                            EmployeeLastName = reader.GetString(4),
                            ClientID = reader.GetInt32(5),
                            ClientFirstName = reader.GetString(6),
                            ClientLastName = reader.GetString(7),
                        };
                        rideTickets.Add(rideTicket);
                    }
                    reader.Close();
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
            return rideTickets;
        }

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/03/30
        /// 
        /// updates a reaview in the database passing the new review to overide the old review
        /// </summary>
        /// <param name="oldRideReview"></param>
        /// <param name="newRideReview"></param>
        /// <returns></returns>
        public int UpdateRideReviewFromDriver(RideReview oldRideReview, RideReview newRideReview)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_update_ride_review_from_driver", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // add id to identify review
            cmd.Parameters.Add("@RideReviewID", SqlDbType.Int);

            // add old review 
            cmd.Parameters.Add("@OldDriverClientRating", SqlDbType.Int);
            cmd.Parameters.Add("@OldDriverComment", SqlDbType.NVarChar, 500);
            //cmd.Parameters.Add("@")

            //add new review
            cmd.Parameters.Add("@NewDriverClientRating", SqlDbType.Int);
            cmd.Parameters.Add("@NewDriverComment", SqlDbType.NVarChar, 500);

            // set paramiter id
            cmd.Parameters["@RideReviewID"].Value = oldRideReview.RideReviewID;

            // set parameter values for old item
            cmd.Parameters["@OldDriverClientRating"].Value = oldRideReview.DriverClientRating;
            cmd.Parameters["@OldDriverComment"].Value = oldRideReview.DriverComment;

            // set parameter values for new item
            cmd.Parameters["@NewDriverClientRating"].Value = newRideReview.DriverClientRating;
            cmd.Parameters["@NewDriverComment"].Value = newRideReview.DriverComment;

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
        /// Nate Hepker
        /// Created: 2021/04/06
        /// 
        /// Gets the ride reviews from the database for a specific client 
        /// </summary>
        /// <param name="clientID"></param>
        /// <returns></returns>
        public List<RideReviewVM> SelectTicketsByClientID(int clientID)
        {
            List<RideReviewVM> rideTickets = new List<RideReviewVM>();
            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_select_tickets_by_employee_id", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmployeeID", clientID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var rideTicket = new RideReviewVM()
                        {
                            TicketID = reader.GetInt32(0),
                            TicketType = reader.GetInt32(1),
                            DriverID = reader.GetInt32(2),
                            EmployeeFirstName = reader.GetString(3),
                            EmployeeLastName = reader.GetString(4),
                            ClientID = reader.GetInt32(5),
                            ClientFirstName = reader.GetString(6),
                            ClientLastName = reader.GetString(7),
                        };
                        rideTickets.Add(rideTicket);
                    }
                    reader.Close();
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
            return rideTickets;
        }

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/04/09
        /// 
        /// will get all the ride tickets from the database that can be reviewed
        /// </summary>
        /// <returns></returns>
        public List<RideReviewVM> SelectAllTicketsToReview()
        {
            List<RideReviewVM> rideTickets = new List<RideReviewVM>();
            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_select_all_ride_tickets_to_review", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var rideTicket = new RideReviewVM()
                        {
                            TicketID = reader.GetInt32(0),
                            TicketType = reader.GetInt32(1),
                            DriverID = reader.GetInt32(2),
                            EmployeeFirstName = reader.GetString(3),
                            EmployeeLastName = reader.GetString(4),
                            ClientID = reader.GetInt32(5),
                            ClientFirstName = reader.GetString(6),
                            ClientLastName = reader.GetString(7),
                        };
                        rideTickets.Add(rideTicket);
                    }
                    reader.Close();
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
            return rideTickets;
        }



        /// <summary>
        /// Nate Hepker
        /// Created: 2021/04/10
        /// 
        /// Inserts a new ride review from a client into the DB
        /// </summary>
        /// <param name="rideReviewFromClient"></param>
        /// <returns></returns>
        public int InsertClientRideReview(RideReview rideReview)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_insert_client_ride_review", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@TicketID", SqlDbType.Int);
            cmd.Parameters.Add("@ClientID", SqlDbType.Int);
            cmd.Parameters.Add("@DriverID", SqlDbType.Int);
            cmd.Parameters.Add("@ClientDriverRating", SqlDbType.Int);
            cmd.Parameters.Add("@ClientComment", SqlDbType.NVarChar, 500);

            cmd.Parameters["@TicketID"].Value = rideReview.TicketID;
            cmd.Parameters["@ClientID"].Value = rideReview.ClientID;
            cmd.Parameters["@DriverID"].Value = rideReview.DriverID;
            cmd.Parameters["@ClientDriverRating"].Value = rideReview.ClientDriverRating;
            cmd.Parameters["@ClientComment"].Value = rideReview.ClientComment;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
                if (result == 0)
                {
                    throw new ApplicationException("The ride review could not be added.");
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
        /// Nate Hepker
        /// Created: 2021/04/16
        /// 
        /// Selects all the RideReviews and returns it to the RideReviewManager
        /// </summary>
        public List<RideReviewVM> SelectAllClientRideReviews()
        {
            List<RideReviewVM> rideReviews = new List<RideReviewVM>();
            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_select_all_client_ride_reviews", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        rideReviews.Add(new RideReviewVM
                        {
                            RideReviewID = reader.GetInt32(0),
                            TicketID = reader.GetInt32(1),
                            DriverID = reader.GetInt32(2),
                            EmployeeFirstName = reader.GetString(3),
                            EmployeeLastName = reader.GetString(4),
                            ClientID = reader.GetInt32(5),
                            ClientFirstName = reader.GetString(6),
                            ClientLastName = reader.GetString(7),
                            ClientDriverRating = reader.GetInt32(8),
                            ClientComment = reader.GetString(9),
                            DriverComment = reader.IsDBNull(10) ? null : reader.GetString(10) //test, delete in sql if not right aswell. this test worked 
                        });
                    }
                }
                reader.Close();
            }
            catch (Exception ex)//specified cast is not valid is throwing 
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return rideReviews;
        }

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/04/19
        /// 
        /// Updates the RideReview and returns and int of the row changed to confirm the ride review was
        /// updated
        /// </summary>
        public int UpdateRideReviewFromClient(RideReview oldRideReview, RideReview newRideReview)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_update_ride_review_from_client", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            // add id to identify review
            cmd.Parameters.Add("@RideReviewID", SqlDbType.Int);

            // add old review 
            cmd.Parameters.Add("@OldClientDriverRating", SqlDbType.Int);
            cmd.Parameters.Add("@OldClientComment", SqlDbType.NVarChar, 500);
            //add new review
            cmd.Parameters.Add("@NewClientDriverRating", SqlDbType.Int);
            cmd.Parameters.Add("@NewClientComment", SqlDbType.NVarChar, 500);

            // set paramiter id
            cmd.Parameters["@RideReviewID"].Value = oldRideReview.RideReviewID;

            // set parameter values for old item
            cmd.Parameters["@OldClientDriverRating"].Value = oldRideReview.ClientDriverRating;
            cmd.Parameters["@OldClientComment"].Value = oldRideReview.ClientComment;
            // set parameter values for new item
            cmd.Parameters["@NewClientDriverRating"].Value = newRideReview.ClientDriverRating;
            cmd.Parameters["@NewClientComment"].Value = newRideReview.ClientComment;

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
        /// Nate Hepker
        /// Created: 2021/04/23
        /// 
        /// Selects the ride review by its id for selecting an individual ride review.
        /// this is also used for getting the old data when updating a review before the update
        /// is made.
        /// </summary>
        public RideReviewVM SelectRideReviewsByReviewID(int rideReviewID)
        {
            RideReviewVM rideReview = new RideReviewVM();

            var conn = DBConnection.GetDBConnection();

            using (var cmd = new SqlCommand("sp_select_ride_reviews_by_review_id", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RideReviewID", rideReviewID);

                try
                {
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            rideReview = new RideReviewVM()
                            {
                                RideReviewID = reader.GetInt32(0),
                                TicketID = reader.GetInt32(1),
                                DriverID = reader.GetInt32(2),
                                EmployeeFirstName = reader.GetString(3),
                                EmployeeLastName = reader.GetString(4),
                                ClientID = reader.GetInt32(5),
                                ClientFirstName = reader.GetString(6),
                                ClientLastName = reader.GetString(7),
                                ClientDriverRating = reader.GetInt32(8),
                                ClientComment = reader.GetString(9)
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
            }
            return rideReview;
        }

    }
}
