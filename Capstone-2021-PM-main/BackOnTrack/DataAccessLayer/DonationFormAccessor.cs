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
    /// Asaad Mohamed
    /// 2021/02/04
    /// This class implemente all the methods from data access Interface for accessing donor form data
    /// </summary>
    public class DonationFormAccessor : IDonationFormAccessor
    {
        /// <summary>
        /// Asaad Mohamed
        /// 2021/02/22
        /// This method for insert some data to  donor form into database 
        ///  <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// </summary>
        /// <param name="donationForm"></param>
        /// <returns></returns>
        public int InsertDonationForm(DonationForm donationForm)
        {
            int result = 0;
            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_insert_donation_form", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@DonorID", donationForm.DonorID);
            cmd.Parameters.AddWithValue("@DateCreated", donationForm.DateCreated);
            cmd.Parameters.AddWithValue("@Status", donationForm.Status);

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
        /// Asaad Mohamed
        /// 2021/02/04
        /// This method for select all donor form from database 
        ///  <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// </remarks>
        /// </summary>
        /// <returns></returns>
        public List<DonationForm> SelectAllDonationForm()
        {
            List<DonationForm> donorForm = new List<DonationForm>();
            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_select_all_DonationForm", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var form = new DonationForm()
                        {
                            DonorFormID = reader.GetInt32(0),
                            DonorID = reader.GetInt32(1),
                            DateCreated = reader.GetDateTime(2),
                            Status = reader.GetString(3),



                        };
                        donorForm.Add(form);
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
            return donorForm;
        }

        /// <summary>
        /// Asaad Mohamed
        /// 2021/04/09
        /// This method for select all donation Form by ID from database 
        ///  <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DonationForm SelectDonationFormById(int id)
        {
            DonationForm donationForm = null;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_select_all_Donation_form_by_id", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DonorFormID", id);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    donationForm = new DonationForm()
                    {

                        DonorFormID = reader.GetInt32(0),
                        DonorID = reader.GetInt32(1),
                        DateCreated = reader.GetDateTime(2),
                        Status = reader.GetString(3),


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

            return donationForm;
        }

        /// <summary>
        /// Asaad Mohamed
        /// 2021/02/04
        /// This method for update the status of donor form from database 
        /// </summary>
        ///  <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// </remarks>
        /// </summary>
        /// <param name="oldDonorForm"></param>
        /// <param name="newDonorForm"></param>
        /// <returns></returns>
        public int UpdateDonorFormStatus(DonationForm oldDonorForm, DonationForm newDonorForm)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_update_donation_form", conn);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@DonorFormID", oldDonorForm.DonorFormID);
            cmd.Parameters.AddWithValue("@DonorID", oldDonorForm.DonorID);

            cmd.Parameters.AddWithValue("@NewDateCreated", newDonorForm.DateCreated);
            cmd.Parameters.AddWithValue("@NewStatus", newDonorForm.Status);

            cmd.Parameters.AddWithValue("@OldDateCreated", oldDonorForm.DateCreated);
            cmd.Parameters.AddWithValue("@OldStatus", oldDonorForm.Status);

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
