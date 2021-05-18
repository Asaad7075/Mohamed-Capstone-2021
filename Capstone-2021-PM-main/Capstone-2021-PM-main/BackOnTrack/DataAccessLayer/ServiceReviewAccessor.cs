using DataAccessInterfaces;
using DomainModels.Services;
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
    /// Chase Martin
    /// Created: 2021/04/09
    /// 
    /// Accessor class for the RideReview that interacts with DB using CRUD funtions
    /// </summary>
    public class ServiceReviewAccessor : IServiceReviewAccessor
    {
        public int DeleteServiceReview(ServiceReview serviceReview)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_delete_service_review", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ServiceReviewID", serviceReview.ServiceReviewID);

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
        /// Chase Martin
        /// Created: 2021/04/09
        /// 
        /// Inserts a new service review into DB
        /// </summary>
        public int InsertServiceReview(ServiceReview serviceReview)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_insert_service_review", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ServiceName", SqlDbType.NVarChar, 200);
            cmd.Parameters.Add("@ProviderFirstName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@ProviderLastName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@Rating", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@ClientComment", SqlDbType.NVarChar, 500);

            cmd.Parameters["@ServiceName"].Value = serviceReview.ServiceName;
            cmd.Parameters["@ProviderFirstName"].Value = serviceReview.ProviderFirstName;
            cmd.Parameters["@ProviderLastName"].Value = serviceReview.ProviderLastName;
            cmd.Parameters["@Rating"].Value = serviceReview.Rating;
            cmd.Parameters["@ClientComment"].Value = serviceReview.ClientComment;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
                if (result == 0)
                {
                    throw new ApplicationException("The service review could not be added.");
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
        /// Chase Martin
        /// Created: 2021/04/09
        /// 
        /// Selects all the ServiceReviews and returns it to the ServiceReviewManager
        /// </summary>
        public List<ServiceReview> SelectAllServiceReviews()
        {
            List<ServiceReview> serviceReview = new List<ServiceReview>();
            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_select_all_service_reviews", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        serviceReview.Add(new ServiceReview
                        {
                            ServiceReviewID = reader.GetInt32(0),
                            ServiceName = reader.GetString(1),
                            ProviderFirstName = reader.GetString(2),
                            ProviderLastName = reader.GetString(3),
                            Rating = reader.GetString(4),
                            ClientComment = reader.GetString(5)
                        });
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
            return serviceReview;
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/04/09
        /// 
        /// Selects the services for the user that is actively logged in.
        /// Returns a list of the services.
        /// </summary>
        public ServiceReview SelectServiceReviewsByServiceReviewID(int id)
        {
            //List<ServiceReview> serviceReviews = new List<ServiceReview>();
            ServiceReview serviceReview = null;

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_select_service_reviews_by_service_review_id", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ServiceReviewID", id);

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    serviceReview = new ServiceReview()
                    {
                        ServiceReviewID = reader.GetInt32(0),
                        ServiceName = reader.GetString(1),
                        ProviderFirstName = reader.GetString(2),
                        ProviderLastName = reader.GetString(3),
                        Rating = reader.GetString(4),
                        ClientComment = reader.GetString(5)
                    };
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
            return serviceReview;
        }

        //List<ServiceReview> services = new List<ServiceReview>();
        //var conn = DBConnection.GetDBConnection();
        //var cmd = new SqlCommand("sp_select_service_reviews_by_service_review_id", conn);
        //cmd.CommandType = CommandType.StoredProcedure;
        //cmd.Parameters.AddWithValue("@ServiceReviewID", serviceReviewID);

        //try
        //{
        //    conn.Open();
        //    var reader = cmd.ExecuteReader();
        //    if (reader.HasRows)
        //    {
        //        while (reader.Read())
        //        {
        //            var service = new ServiceReview()
        //            {
        //                ServiceReviewID = reader.GetInt32(0),
        //                ServiceName = reader.GetString(1),
        //                ProviderFirstName = reader.GetString(2),
        //                ProviderLastName = reader.GetString(3),
        //                Rating = reader.GetString(4),
        //                ClientComment = reader.GetString(5)
        //            };
        //            services.Add(service);
        //        }
        //        reader.Close();
        //    }
        //}
        //catch (Exception ex)
        //{
        //    throw ex;
        //}
        //finally
        //{
        //    conn.Close();
        //}
        //return services;


        /// Chase Martin
        /// Created: 2021/04/16
        /// 
        /// updates a review in the database passing the new review to overide the old review
        /// </summary>
        /// <param name="oldServiceReview"></param>
        /// <param name="newServiceReview"></param>
        /// <returns></returns>
        public int UpdateServiceReviewFromClient(ServiceReview oldServiceReview, ServiceReview newServiceReview)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_update_service_review_from_client", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // add id to identify review
            cmd.Parameters.Add("@ServiceReviewID", SqlDbType.Int);

            // add old review 
            cmd.Parameters.Add("@OldServiceName", SqlDbType.NVarChar, 200);
            cmd.Parameters.Add("@OldProviderFirstName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@OldProviderLastName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@OldRating", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@OldClientComment", SqlDbType.NVarChar, 500);
            //cmd.Parameters.Add("@")

            //add new review
            cmd.Parameters.Add("@NewServiceName", SqlDbType.NVarChar, 200);
            cmd.Parameters.Add("@NewProviderFirstName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@NewProviderLastName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@NewRating", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@NewClientComment", SqlDbType.NVarChar, 500);

            // set parameter id
            cmd.Parameters["@ServiceReviewID"].Value = oldServiceReview.ServiceReviewID;

            // set parameter values for old item
            cmd.Parameters["@OldServiceName"].Value = oldServiceReview.ServiceName;
            cmd.Parameters["@OldProviderFirstName"].Value = oldServiceReview.ProviderFirstName;
            cmd.Parameters["@OldProviderLastName"].Value = oldServiceReview.ProviderLastName;
            cmd.Parameters["@OldRating"].Value = oldServiceReview.Rating;
            cmd.Parameters["@OldClientComment"].Value = oldServiceReview.ClientComment;

            // set parameter values for new item
            cmd.Parameters["@NewServiceName"].Value = newServiceReview.ServiceName;
            cmd.Parameters["@NewProviderFirstName"].Value = newServiceReview.ProviderFirstName;
            cmd.Parameters["@NewProviderLastName"].Value = newServiceReview.ProviderLastName;
            cmd.Parameters["@NewRating"].Value = newServiceReview.Rating;
            cmd.Parameters["@NewClientComment"].Value = newServiceReview.ClientComment;

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
    }
}
  

    

