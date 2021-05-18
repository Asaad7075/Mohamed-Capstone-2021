using DataAccessInterfaces;
using DomainModels;
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
    /// Chantal Shirley
    /// Created: 2021/04/09
    /// 
    /// Accessor for client data.
    /// </summary>
    public class ClientAccessor : IClientAccessor
    {
        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/24
        /// 
        /// Creates new client accounts.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="passwordHash"></param>
        /// <returns></returns>
        public bool InsertNewClientAccount(Client client, string passwordHash)
        {
            bool result = false;

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_insert_new_client_from_web", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@Password", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 100);

            cmd.Parameters["@Email"].Value = client.Email.ToLower();
            cmd.Parameters["@Password"].Value = passwordHash.ToUpper();
            cmd.Parameters["@FirstName"].Value = client.FirstName;
            cmd.Parameters["@LastName"].Value = client.LastName;

            try
            {
                conn.Open();
                int rawResult = Convert.ToInt32(cmd.ExecuteScalar());
                if (rawResult != 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/24
        /// 
        /// Selects client by email
        /// and password.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool SelectClientByEmailAndPassword(string email, string password)
        {
            bool result = false;

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_select_client_by_email_and_password", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@Password", SqlDbType.NVarChar, 100);

            cmd.Parameters["@Email"].Value = email.ToLower();
            cmd.Parameters["@Password"].Value = password.ToUpper();

            try
            {
                conn.Open();

                int rawResult = Convert.ToInt32(cmd.ExecuteScalar());
                if (rawResult == 1)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/14
        /// 
        /// Retrieves client name by 
        /// email address.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public string[] SelectClientFirstLastNameByEmail(string email)
        {
            string[] clientname = new string[2];

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_select_client_first_and_last_name_by_email", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);

            cmd.Parameters["@Email"].Value = email;


            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        clientname[0] = reader.GetString(0);
                        clientname[1] = reader.GetString(1);
                    }
                    reader.Close();
                }
                else
                {
                    clientname = null;
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

            return clientname;
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/09
        /// 
        /// Retrieves client id by email address.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public int SelectClientIdByEmail(string email)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_select_client_id_by_email", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);

            cmd.Parameters["@Email"].Value = email;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result = reader.GetInt32(0);
                    }
                    reader.Close();
                }
                else
                {
                    throw new ApplicationException("User not found.");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }
    }
}
