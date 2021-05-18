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
    public class ServiceProviderAccessor : IServiceProviderAccessor
    {
        /// <summary>
        /// Chase Martin
        /// Created: 2021/03/26
        /// 
        /// Accessor method for deleting a Service Provider.
        /// </summary>
        /// <param name="serviceProviderID"></param>
        /// <returns></returns>
        public int DeleteServiceProvider(int serviceProviderID)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_delete_provider", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("@ServiceProviderID", serviceProviderID);

            cmd.Parameters.Add(param);
            cmd.Parameters["@ServiceProviderID"].Value = serviceProviderID;
            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
                Console.WriteLine("Service Provider deleted!");
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
        /// Created: 2021/03/26
        /// 
        /// Accessor Method for inserting a service provider
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public int InsertServiceProvider(ServiceProvider serviceProvider)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_insert_provider", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            //cmd.Parameters.Add("@ServiceCategory", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@BusinessName", SqlDbType.NVarChar, 200);
            cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@Address", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar, 11);
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@ZipCode", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@EIN", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@Activated", SqlDbType.Bit);
            cmd.Parameters.Add("@Schedule", SqlDbType.Bit);

            //cmd.Parameters["@ServiceCategory"].Value = serviceProvider.ServiceCategory;
            cmd.Parameters["@BusinessName"].Value = serviceProvider.BusinessName;
            cmd.Parameters["@FirstName"].Value = serviceProvider.FirstName;
            cmd.Parameters["@LastName"].Value = serviceProvider.LastName;
            cmd.Parameters["@Address"].Value = serviceProvider.Address;
            cmd.Parameters["@PhoneNumber"].Value = serviceProvider.PhoneNumber;
            cmd.Parameters["@Email"].Value = serviceProvider.Email;
            cmd.Parameters["@ZipCode"].Value = serviceProvider.ZipCode;
            cmd.Parameters["@EIN"].Value = serviceProvider.EIN;
            cmd.Parameters["@Activated"].Value = serviceProvider.Activated;
            cmd.Parameters["@Schedule"].Value = serviceProvider.Schedule;


            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
                Console.WriteLine("Service Provider added!");
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
        /// Created: 2021/03/26
        /// 
        /// Accessor method for selecting all 
        /// service providers.
        /// </summary>
        /// <returns></returns>
        public List<ServiceProvider> SelectAllServiceProviders()
        {
            List<ServiceProvider> serviceProviders = new List<ServiceProvider>();

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_select_all_providers", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var serviceProvider = new ServiceProvider()
                        {
                            ServiceProviderID = reader.GetInt32(0),
                            //ServiceCategory = reader.GetString(1),
                            BusinessName = reader.GetString(1), // Check numbers
                            FirstName = reader.GetString(2),
                            LastName = reader.GetString(3),
                            Address = reader.GetString(4),
                            PhoneNumber = reader.GetString(5),
                            Email = reader.GetString(6),
                            ZipCode = reader.GetString(7),
                            EIN = reader.GetString(8),
                            Activated = reader.GetBoolean(9),
                            Schedule = reader.GetBoolean(10)
                        };
                        serviceProviders.Add(serviceProvider);
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
            return serviceProviders;
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/03/26
        /// 
        /// Accessor method for selecting a Service Provider by zip code.
        /// </summary>
        /// <returns></returns>
        public List<ServiceProvider> SelectProvidersByZipCode(string zipCode)
        {
            List<ServiceProvider> serviceProviders = new List<ServiceProvider>();

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_select_providers_by_zip_code", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ZipCode", SqlDbType.NVarChar, 50);
            cmd.Parameters["@ZipCode"].Value = zipCode;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        serviceProviders.Add(new ServiceProvider()
                        {
                            ServiceProviderID = reader.GetInt32(0),
                            //ServiceCategory = reader.GetString(1),
                            BusinessName = reader.GetString(1),
                            FirstName = reader.GetString(2),
                            LastName = reader.GetString(3),
                            Address = reader.GetString(4),
                            PhoneNumber = reader.GetString(5),
                            Email = reader.GetString(6),
                            ZipCode = reader.GetString(7),
                            EIN = reader.GetString(8),
                            Activated = reader.GetBoolean(9),
                            Schedule = reader.GetBoolean(10)
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
            return serviceProviders;
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/03/26
        /// 
        /// Accessor method for updating a Service Provider.
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public int UpdateServiceProvider(ServiceProvider serviceProvider)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_update_provider", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ServiceProviderID", SqlDbType.Int);
            //cmd.Parameters.Add("@ServiceCategory", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@BusinessName", SqlDbType.NVarChar, 200);
            cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@Address", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar, 11);
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@ZipCode", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@EIN", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@Activated", SqlDbType.Bit);
            cmd.Parameters.Add("@Schedule", SqlDbType.Bit);

            cmd.Parameters["@ServiceProviderID"].Value = serviceProvider.ServiceProviderID;
            //cmd.Parameters["@ServiceCategory"].Value = serviceProvider.ServiceCategory;
            cmd.Parameters["@BusinessName"].Value = serviceProvider.BusinessName;
            cmd.Parameters["@FirstName"].Value = serviceProvider.FirstName;
            cmd.Parameters["@LastName"].Value = serviceProvider.LastName;
            cmd.Parameters["@Address"].Value = serviceProvider.Address;
            cmd.Parameters["@PhoneNumber"].Value = serviceProvider.PhoneNumber;
            cmd.Parameters["@Email"].Value = serviceProvider.Email;
            cmd.Parameters["@ZipCode"].Value = serviceProvider.ZipCode;
            cmd.Parameters["@EIN"].Value = serviceProvider.EIN;
            cmd.Parameters["@Activated"].Value = serviceProvider.Activated;
            cmd.Parameters["@Schedule"].Value = serviceProvider.Schedule;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
                Console.WriteLine("Service Provider updated!");
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

