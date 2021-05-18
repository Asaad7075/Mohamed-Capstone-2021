using DataAccessInterfaces;
using DomainModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DataAccessLayer
{
    public class DonorAccessor : IDonorAccessor
    {
        /// <summary>
        /// Asaad Mohamed
        /// Created: 2021/04/01
        /// This method for insert new donor  
        /// </summary>
        /// <param name="donor"></param>
        /// <returns></returns>
        public int AddDonor(Donor donor)
        {
            int result = 0;

          
            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_insert_donor", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            //cmd.Parameters.AddWithValue("@Business", donor.Business);
            //cmd.Parameters.AddWithValue("@Individual", donor.Individual);
            //cmd.Parameters.AddWithValue("@BusinessName", donor.BusinessName);
            //cmd.Parameters.AddWithValue("@FirstName", donor.FirstName);
            //cmd.Parameters.AddWithValue("@LastName", donor.LastName);
            //cmd.Parameters.AddWithValue("@MiddleInitial", donor.MiddleInitial);
            //cmd.Parameters.AddWithValue("@Address", donor.Address);
            //cmd.Parameters.AddWithValue("@ZipCode", donor.ZipCode);
            //cmd.Parameters.AddWithValue("@PhoneNumber", donor.PhoneNumber);
            //cmd.Parameters.AddWithValue("@Email", donor.Email);
            //cmd.Parameters.AddWithValue("@SS", donor.SS);
            //cmd.Parameters.AddWithValue("@EIN", donor.EIN);
            cmd.Parameters.Add("@Business", SqlDbType.Bit);
            cmd.Parameters.Add("@Individual", SqlDbType.Bit);
            cmd.Parameters.Add("@BusinessName", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@MiddleInitial", SqlDbType.NVarChar, 1);
            cmd.Parameters.Add("@Address", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@ZipCode", SqlDbType.NVarChar, 10);
            cmd.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar, 11);
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 250);
            cmd.Parameters.Add("@SS", SqlDbType.NVarChar, 11);
            cmd.Parameters.Add("@EIN", SqlDbType.NVarChar, 11);



            cmd.Parameters["@Business"].Value = donor.Business;
            cmd.Parameters["@Individual"].Value = donor.Individual;
            cmd.Parameters["@BusinessName"].Value = donor.BusinessName;
            cmd.Parameters["@FirstName"].Value = donor.FirstName;
            cmd.Parameters["@LastName"].Value = donor.LastName;
            cmd.Parameters["@MiddleInitial"].Value = donor.MiddleInitial;
            cmd.Parameters["@Address"].Value = donor.Address;
            cmd.Parameters["@ZipCode"].Value = donor.ZipCode;
            cmd.Parameters["@PhoneNumber"].Value = donor.PhoneNumber;
            cmd.Parameters["@Email"].Value = donor.Email;
            cmd.Parameters["@SS"].Value = donor.SS;
            cmd.Parameters["@EIN"].Value = donor.EIN;


            try
            {
                conn.Open();
                result = Convert.ToInt32(cmd.ExecuteScalar());
                //result = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw(ex);
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
        /// <summary>
        /// Asaad Mohamed
        /// Created: 2021/04/01
        /// This method for remove a donor 
        /// </summary>
        /// <param name="donorID"></param>
        /// <returns></returns>
        public int DeleteDonor(int donorID)
        {

            int rows = 0;
            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_delete_Donor", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DonorID", donorID);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
               throw(ex);
            }
            finally
            {
                conn.Close();
            }
            return rows;
        }

        /// <summary>
        /// Asaad Mohamed
        /// 2021/03/01
        /// This method for select all donor by active from database 
        ///  <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// </summary>
        /// <param name="active"></param>
        /// <returns></returns>
        public List<Donor> SelectDonorByActive(bool active = true)
        {
            List<Donor> donors = new List<Donor>();
            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_select_all_Donor_by_active");
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Active", SqlDbType.Bit);
            cmd.Parameters["@Active"].Value = active;
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {

                        var donor = new Donor()
                        {

                            DonorID = reader.GetInt32(0),
                            Business = reader.GetBoolean(1),
                            Individual = reader.GetBoolean(2),
                            BusinessName = reader.GetString(3),
                            FirstName = reader.GetString(4),
                            LastName = reader.GetString(5),
                            MiddleInitial = reader.GetString(6),
                            Address = reader.GetString(7),
                            ZipCode = reader.GetString(8),
                            PhoneNumber = reader.GetString(9),
                            Email = reader.GetString(10),
                            SS = reader.GetString(11),
                            EIN = reader.GetString(12),
                            Active = reader.GetBoolean(13)


                        };
                        donors.Add(donor);

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

            return donors;

        }

        /// <summary>
        /// Asaad Mohamed
        /// 2021/04/01
        /// This method for update all donor 
        /// </summary>
        /// <param name="oldDonor"></param>
        /// <param name="newDonor"></param>
        /// <returns></returns>
        public int UpdateDonor(Donor oldDonor, Donor newDonor)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_update_donor", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@DonorID", SqlDbType.Int);
            cmd.Parameters.Add("@OldBusiness", SqlDbType.Bit);
            cmd.Parameters.Add("@OldIndividual", SqlDbType.Bit);
            cmd.Parameters.Add("@OldBusinessName", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@OldFirstName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@OldLastName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@OldMiddleInitial", SqlDbType.NVarChar, 1);
            cmd.Parameters.Add("@OldAddress", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@OldZipCode", SqlDbType.NVarChar, 10);
            cmd.Parameters.Add("@OldPhoneNumber", SqlDbType.NVarChar, 11);
            cmd.Parameters.Add("@OldEmail", SqlDbType.NVarChar, 250);
            cmd.Parameters.Add("@OldSS", SqlDbType.NVarChar, 11);
            cmd.Parameters.Add("@OldEIN", SqlDbType.NVarChar, 11);



            cmd.Parameters.Add("@NewBusiness", SqlDbType.Bit);
            cmd.Parameters.Add("@NewIndividual", SqlDbType.Bit);
            cmd.Parameters.Add("@NewBusinessName", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@NewFirstName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@NewLastName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@NewMiddleInitial", SqlDbType.NVarChar, 1);
            cmd.Parameters.Add("@NewAddress", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@NewZipCode", SqlDbType.NVarChar, 10);
            cmd.Parameters.Add("@NewPhoneNumber", SqlDbType.NVarChar, 11);
            cmd.Parameters.Add("@NewEmail", SqlDbType.NVarChar, 250);
            cmd.Parameters.Add("@NewSS", SqlDbType.NVarChar, 11);
            cmd.Parameters.Add("@NewEIN", SqlDbType.NVarChar, 11);

            cmd.Parameters["@DonorID"].Value = oldDonor.DonorID;
            cmd.Parameters["@OldBusiness"].Value = oldDonor.Business;
            cmd.Parameters["@OldIndividual"].Value = oldDonor.Individual;
            cmd.Parameters["@OldBusinessName"].Value = oldDonor.BusinessName;
            cmd.Parameters["@OldFirstName"].Value = oldDonor.FirstName;
            cmd.Parameters["@OldLastName"].Value = oldDonor.LastName;
            cmd.Parameters["@OldMiddleInitial"].Value = oldDonor.MiddleInitial;
            cmd.Parameters["@OldAddress"].Value = oldDonor.Address;
            cmd.Parameters["@OldZipCode"].Value = oldDonor.ZipCode;
            cmd.Parameters["@OldPhoneNumber"].Value = oldDonor.PhoneNumber;
            cmd.Parameters["@OldEmail"].Value = oldDonor.Email;
            cmd.Parameters["@OldSS"].Value = oldDonor.SS;
            cmd.Parameters["@OldEIN"].Value = oldDonor.EIN;

            cmd.Parameters["@NewBusiness"].Value = newDonor.Business;
            cmd.Parameters["@NewIndividual"].Value = newDonor.Individual;
            cmd.Parameters["@NewBusinessName"].Value = newDonor.BusinessName;
            cmd.Parameters["@NewFirstName"].Value = newDonor.FirstName;
            cmd.Parameters["@NewLastName"].Value = newDonor.LastName;
            cmd.Parameters["@NewMiddleInitial"].Value = newDonor.MiddleInitial;
            cmd.Parameters["@NewAddress"].Value = newDonor.Address;
            cmd.Parameters["@NewZipCode"].Value = newDonor.ZipCode;
            cmd.Parameters["@NewPhoneNumber"].Value = newDonor.PhoneNumber;
            cmd.Parameters["@NewEmail"].Value = newDonor.Email;
            cmd.Parameters["@NewSS"].Value = newDonor.SS;
            cmd.Parameters["@NewEIN"].Value = newDonor.EIN;


            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw(ex);
                
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        /// <summary>
        /// Asaad Mohamed
        /// 2021/04/09
        /// This method for select donor by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Donor SelectDonorById(int id)
        {
            Donor donor = null;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_select_all_Donor_by_id", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DonorID", id);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    donor = new Donor()
                    {

                        DonorID = reader.GetInt32(0),
                        Business = reader.GetBoolean(1),
                        Individual = reader.GetBoolean(2),
                        BusinessName = reader.GetString(3),
                        FirstName = reader.GetString(4),
                        LastName = reader.GetString(5),
                        MiddleInitial = reader.GetString(6),
                        Address = reader.GetString(7),
                        ZipCode = reader.GetString(8),
                        PhoneNumber = reader.GetString(9),
                        Email = reader.GetString(10),
                        SS = reader.GetString(11),
                        EIN = reader.GetString(12)
                       

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

            return donor;
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/25
        /// 
        /// Returns true if the donor's
        /// email exists in the db.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool SelectDonorByEmail(string email)
        {
            bool result = false;

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_select_donor_by_email", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);

            cmd.Parameters["@Email"].Value = email;


            try
            {
                conn.Open();
                int rawResult = 0;
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        rawResult = reader.GetInt32(0); 
                    }
                }
                if (rawResult == 1)
                {
                    result = true;
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
        /// Chantal Shirley
        /// Created: 2021/04/25
        /// 
        /// Returns true if the donor is
        /// already registered.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool SelectDonorByEmailAndPassword(string email, string password)
        {
            bool result = false;

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_select_donor_by_email_and_password", conn);

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
        /// Created: 2021/04/25
        /// 
        /// Returns true if the donor 
        /// was inserted into the database.
        /// Slightly modified Asaad's code.
        /// </summary>
        /// <param name="donor"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool InsertDonorFromWeb(Donor donor, string password)
        {
            bool result = false;
            int rawResultResult = 0;


            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_insert_donor_from_web", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            //cmd.Parameters.AddWithValue("@Business", donor.Business);
            //cmd.Parameters.AddWithValue("@Individual", donor.Individual);
            //cmd.Parameters.AddWithValue("@BusinessName", donor.BusinessName);
            //cmd.Parameters.AddWithValue("@FirstName", donor.FirstName);
            //cmd.Parameters.AddWithValue("@LastName", donor.LastName);
            //cmd.Parameters.AddWithValue("@MiddleInitial", donor.MiddleInitial);
            //cmd.Parameters.AddWithValue("@Address", donor.Address);
            //cmd.Parameters.AddWithValue("@ZipCode", donor.ZipCode);
            //cmd.Parameters.AddWithValue("@PhoneNumber", donor.PhoneNumber);
            //cmd.Parameters.AddWithValue("@Email", donor.Email);
            //cmd.Parameters.AddWithValue("@SS", donor.SS);
            //cmd.Parameters.AddWithValue("@EIN", donor.EIN);
            cmd.Parameters.Add("@Business", SqlDbType.Bit);
            cmd.Parameters.Add("@Individual", SqlDbType.Bit);
            cmd.Parameters.Add("@BusinessName", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@MiddleInitial", SqlDbType.NVarChar, 1);
            cmd.Parameters.Add("@Address", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@ZipCode", SqlDbType.NVarChar, 10);
            cmd.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar, 11);
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 250);
            cmd.Parameters.Add("@SS", SqlDbType.NVarChar, 11);
            cmd.Parameters.Add("@EIN", SqlDbType.NVarChar, 11);
            cmd.Parameters.Add("@Password", SqlDbType.NVarChar, 100);



            cmd.Parameters["@Business"].Value = donor.Business;
            cmd.Parameters["@Individual"].Value = donor.Individual;
            cmd.Parameters["@BusinessName"].Value = donor.BusinessName;
            cmd.Parameters["@FirstName"].Value = donor.FirstName;
            cmd.Parameters["@LastName"].Value = donor.LastName;
            cmd.Parameters["@MiddleInitial"].Value = donor.MiddleInitial;
            cmd.Parameters["@Address"].Value = donor.Address;
            cmd.Parameters["@ZipCode"].Value = donor.ZipCode;
            cmd.Parameters["@PhoneNumber"].Value = donor.PhoneNumber;
            cmd.Parameters["@Email"].Value = donor.Email;
            cmd.Parameters["@SS"].Value = donor.SS;
            cmd.Parameters["@EIN"].Value = donor.EIN;
            cmd.Parameters["@Password"].Value = password;

            try
            {
                conn.Open();
                rawResultResult = Convert.ToInt32(cmd.ExecuteScalar());
                //result = cmd.ExecuteNonQuery();
                if (rawResultResult == 1)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
    }
    
}
