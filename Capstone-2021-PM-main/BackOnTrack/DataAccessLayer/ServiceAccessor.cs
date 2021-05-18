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
    public class ServiceAccessor : IServiceAccessor
    {
        /// <summary>
        /// Thomas Stout
        /// Created: 2021/03/25
        /// 
        /// Accessor method for deleting a Service.
        /// </summary>
        /// <param name="serviceID"></param>
        /// <returns></returns>
        public int DeleteService(int serviceID)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_Delete_Service", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("@ServiceID", serviceID);

            cmd.Parameters.Add(param);
            cmd.Parameters["@ServiceID"].Value = serviceID;
            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
                Console.WriteLine("Service WAS deleted!");
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
        /// Thomas Stout
        /// Created: 2021/04/24
        /// 
        /// Accessor method for inserting
        /// a client schedule.
        /// </summary>
        /// <param name="businessName"></param>
        /// <param name="serviceName"></param>
        /// <param name="serviceScheduleStart"></param>
        /// <param name="serviceScheduleEnd"></param>
        /// <returns></returns>
        public int InsertClientSchedule(string businessName, string serviceName, DateTime serviceScheduleStart, DateTime serviceScheduleEnd)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_Insert_Client_Schedule", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@BusinessName", SqlDbType.NVarChar, 200);
            cmd.Parameters.Add("@ServiceName", SqlDbType.NVarChar, 200);
            cmd.Parameters.Add("@ServiceScheduleStart", SqlDbType.DateTime);
            cmd.Parameters.Add("@ServiceScheduleEnd", SqlDbType.DateTime);

            cmd.Parameters["@BusinessName"].Value = businessName;
            cmd.Parameters["@ServiceName"].Value = serviceName;
            cmd.Parameters["@ServiceScheduleStart"].Value = serviceScheduleStart;
            cmd.Parameters["@ServiceScheduleEnd"].Value = serviceScheduleEnd;
            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
                Console.WriteLine("Schedule WAS added!");
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
        /// Thomas Stout
        /// Created: 2021/03/19
        /// 
        /// Accessor Method for inserting a service
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public int InsertService(Service service)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_Insert_Service", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@BusinessName", SqlDbType.NVarChar, 200);
            cmd.Parameters.Add("@ServiceName", SqlDbType.NVarChar, 200);
            cmd.Parameters.Add("@ServiceCategoryName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@Available", SqlDbType.Bit);
            cmd.Parameters.Add("@ScheduleRequired", SqlDbType.Bit);
            cmd.Parameters.Add("@ServiceDescription", SqlDbType.NVarChar, 300);
            cmd.Parameters.Add("@ServiceProvider", SqlDbType.NVarChar, 500);

            cmd.Parameters["@BusinessName"].Value = service.BusinessName;
            cmd.Parameters["@ServiceName"].Value = service.ServiceName;
            cmd.Parameters["@ServiceCategoryName"].Value = service.ServiceCategoryName;
            cmd.Parameters["@Available"].Value = service.Available;
            cmd.Parameters["@ScheduleRequired"].Value = service.ScheduleRequired;
            cmd.Parameters["@ServiceDescription"].Value = service.ServiceDescription;
            cmd.Parameters["@ServiceProvider"].Value = service.ServiceProviderFirstLast;
            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
                Console.WriteLine("Service WAS added!");
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

        public List<Service> SelectAllBusinesses()
        {
            List<Service> businesses = new List<Service>();

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_select_all_businesses", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var business = new Service()
                        {
                            BusinessName = reader.GetString(0)
                        };
                        businesses.Add(business);
                    }
                }
                reader.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return businesses;
        }

        public List<Service> SelectAllProvidersByBusiness(Service service)
        {
            List<Service> providers = new List<Service>();

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_select_all_provider_names_by_business", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            // Parameter to query by
            cmd.Parameters.Add("@BusinessName", SqlDbType.NVarChar, 200);

            // Set parameter value
            cmd.Parameters["@BusinessName"].Value = service.BusinessName;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var provider = new Service()
                        {
                            ServiceProviderFirstLast = reader.GetString(0)
                        };
                        providers.Add(provider);
                    }
                }
                reader.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return providers;
        }

        /// <summary>
        /// Thomas Stout
        /// Created: 2021/04/30
        /// Accessor method that selects
        /// all saved schedules by client id
        /// </summary>
        /// <param name="clientID"></param>
        /// <returns></returns>
        public List<ServiceVM> SelectAllSavedServiceSchedulesByClientID(int clientID)
        {
            List<ServiceVM> schedules = new List<ServiceVM>();

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_select_all_saved_schedules_by_client_id", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            // Parameter to query by
            cmd.Parameters.Add("@ClientID", SqlDbType.Int);

            // Set parameter value
            cmd.Parameters["@ClientID"].Value = clientID;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var schedule = new ServiceVM()
                        {
                            Business = reader.GetString(0),
                            Service = reader.GetString(1),
                            ServiceScheduleStart = reader.GetDateTime(2),
                            ServiceScheduleEnd = reader.GetDateTime(3)
                        };
                        schedules.Add(schedule);
                    }
                }
                reader.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return schedules;
        }

        /// <summary>
        /// Thomas Stout
        /// Created: 2021/03/25
        /// 
        /// Accessor method for selecting all 
        /// services.
        /// </summary>
        /// <returns></returns>
        public List<Service> SelectAllServices()
        {
            List<Service> services = new List<Service>();

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_Select_All_Services", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var service = new Service()
                        {
                            ServiceID = reader.GetInt32(0),
                            BusinessName = reader.GetString(1),
                            ServiceName = reader.GetString(2),
                            ServiceCategoryName = reader.GetString(3),
                            Available = reader.GetBoolean(4),
                            ScheduleRequired = reader.GetBoolean(5),
                            ServiceDescription = reader.GetString(6),
                            ServiceProviderFirstLast = reader.GetString(7)
                        };
                        services.Add(service);
                    }
                }
                reader.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return services;
        }

        /// <summary>
        /// Thomas Stout
        /// Created: 2021/04/24
        /// 
        /// Accessor method for selecting
        /// all service schedules by their
        /// id.
        /// </summary>
        /// <param name="serviceID"></param>
        /// <returns></returns>
        public List<ServiceVM> SelectAllServiceSchedulesByID(int serviceID)
        {
            List<ServiceVM> schedules = new List<ServiceVM>();

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_Select_All_Service_Schedules_By_ID", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            // Parameter to query by
            cmd.Parameters.Add("@ServiceID", SqlDbType.Int);

            // Set parameter value
            cmd.Parameters["@ServiceID"].Value = serviceID;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var schedule = new ServiceVM()
                        {
                            BusinessName = reader.GetString(0),
                            ServiceName = reader.GetString(1),
                            ServiceScheduleStart = reader.GetDateTime(2),
                            ServiceScheduleEnd = reader.GetDateTime(3),
                            ScheduleID = reader.GetInt32(4),
                            ClientID = reader.IsDBNull(5) ? null : reader.GetInt32(5)
                        };
                        schedule.ServiceID = serviceID;
                        schedules.Add(schedule);
                    }
                }
                reader.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return schedules;
        }

        /// <summary>
        /// Thomas Stout
        /// Created: 2021/04/24
        /// 
        /// Accessor method that allows
        /// a client to select a service
        /// schedule date/time.
        /// </summary>
        /// <param name="clientID"></param>
        /// <param name="scheduleID"></param>
        /// <returns></returns>
        public int UpdateClientSchedule(int clientID, int scheduleID)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_Update_Client_Schedule", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ClientID", SqlDbType.Int);
            cmd.Parameters.Add("@ScheduleID", SqlDbType.Int);

            cmd.Parameters["@ClientID"].Value = clientID;
            cmd.Parameters["@ScheduleID"].Value = scheduleID;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
                Console.WriteLine("Schedule WAS updated!");
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
        /// Thomas Stout
        /// Created: 2021/03/25
        /// 
        /// Accessor method for updating a Service.
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public int UpdateService(Service service)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_Update_Service", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ServiceID", SqlDbType.Int);
            cmd.Parameters.Add("@BusinessName", SqlDbType.NVarChar, 200);
            cmd.Parameters.Add("@ServiceName", SqlDbType.NVarChar, 200);
            cmd.Parameters.Add("@ServiceCategoryName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@Available", SqlDbType.Bit);
            cmd.Parameters.Add("@ScheduleRequired", SqlDbType.Bit);
            cmd.Parameters.Add("@ServiceDescription", SqlDbType.NVarChar, 300);
            cmd.Parameters.Add("@ServiceProvider", SqlDbType.NVarChar, 500);

            cmd.Parameters["@ServiceID"].Value = service.ServiceID;
            cmd.Parameters["@BusinessName"].Value = service.BusinessName;
            cmd.Parameters["@ServiceName"].Value = service.ServiceName;
            cmd.Parameters["@ServiceCategoryName"].Value = service.ServiceCategoryName;
            cmd.Parameters["@Available"].Value = service.Available;
            cmd.Parameters["@ScheduleRequired"].Value = service.ScheduleRequired;
            cmd.Parameters["@ServiceDescription"].Value = service.ServiceDescription;
            cmd.Parameters["@ServiceProvider"].Value = service.ServiceProviderFirstLast;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
                Console.WriteLine("Service WAS updated!");
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
        /// Created: 2021/04/30
        /// 
        /// Gets the clients schedules from the database
        /// </summary>
        /// <param name="clientID"></param>
        /// <returns></returns>
        public List<ServiceVM> SelectClientSchedules(int clientID)
        {
            List<ServiceVM> scheduals = new List<ServiceVM>();

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_select_all_client_schedules", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ClientID", SqlDbType.Int);
            cmd.Parameters["@ClientID"].Value = clientID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var schedual = new ServiceVM()
                        {
                            ScheduleID = reader.GetInt32(0),
                            BusinessName = reader.GetString(1),
                            ServiceName = reader.GetString(2),
                            ServiceScheduleStart = reader.GetDateTime(3),
                            ServiceScheduleEnd = reader.GetDateTime(4),
                            ClientID = reader.GetInt32(5),
                            ServiceID = reader.GetInt32(6),
                        };
                        scheduals.Add(schedual);
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
            return scheduals;
        }

    }
}
