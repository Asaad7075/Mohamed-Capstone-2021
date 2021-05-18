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
    /// <summary>
    /// Asaad Mohamed
    /// 2021/02/22
    /// This class implemente all the methods from data access Interface for accessing donation data
    /// </summary>
    public class DonationAccessor : IDonationAccessor
    {
        public int DeleteDonation(int donationID)
        {

            int rows = 0;
            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_delete_Donation", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DonationID", donationID);

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

        public int InsertDonation(Donation donation)
        {
            int result = 0;
            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_insert_donation", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@DonorID", donation.DonorID);
            cmd.Parameters.AddWithValue("@NameOfItem", donation.NameOfItem);
            cmd.Parameters.AddWithValue("@Description", donation.Description);
            cmd.Parameters.AddWithValue("@EstValue", donation.EstValue);
            cmd.Parameters.AddWithValue("@AgeofItem", donation.AgeofItem);
            cmd.Parameters.AddWithValue("@DropOff", donation.DropOff);
            cmd.Parameters.AddWithValue("@PickUp", donation.PickUp);
            cmd.Parameters.AddWithValue("@PickUpDateTime", donation.PickUpDateTime);
            cmd.Parameters.AddWithValue("@MailReceipt", donation.MailReceipt);
            cmd.Parameters.AddWithValue("@EmailReceipt", donation.EmailReceipt);
            cmd.Parameters.AddWithValue("@DonationStatus", donation.DonationStatus);
            //cmd.Parameters.AddWithValue("@DonatedImage", donation.DonatedImage);

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw new ApplicationException("The Donation couldn't be added.");
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        /// <summary>
        /// /// <summary>
        /// Asaad Mohamed
        /// 2021/02/22
        /// This method for select all donation from database 
        ///  <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// </remarks>
        /// </summary>
        /// <returns></returns>

        public List<Donation> SelectAllDonation()
        {
            List<Donation> donation = new List<Donation>();
            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_select_all_Donation", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {


                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var donationItem = new Donation()
                        {
                            DonationID = reader.GetInt32(0),
                            DonorID = reader.GetInt32(1),
                            NameOfItem = reader.GetString(2),
                            Description = reader.GetString(3),
                            EstValue = reader.GetDecimal(4),
                            AgeofItem = reader.GetInt32(5),
                            DropOff = reader.GetBoolean(6),
                            PickUp = reader.GetBoolean(7),
                            PickUpDateTime = reader.GetDateTime(8),
                            //DonatedImage = (Byte[])reader[9],
                            MailReceipt = reader.GetBoolean(9),
                            EmailReceipt = reader.GetBoolean(10),
                            DonationStatus = reader.GetString(11),


                        };
                        donation.Add(donationItem);
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
            return donation;
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/14
        /// 
        /// Retrieves a donated item by
        /// its donation id.
        /// 
        /// Change to work around bug.
        /// </summary>
        /// <param name="donationID"></param>
        /// <returns></returns>
        public Donation SelectDonationByDonationId(int donationID)
        {
            Donation result = null;
            byte[] image = null;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_select_donation_by_donation_id", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@DonationID", donationID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        SqlDataReader dataReader = reader;
                        try
                        {
                            var rawData = dataReader["DonatedImage"];
                            if (rawData != null)
                            {
                                image = (byte[])rawData;
                            }
                        }
                        catch (Exception)
                        {
                            // Ignore image
                        }

                        result = new Donation()
                        {
                            DonationID = reader.GetInt32(0),
                            DonorID = reader.GetInt32(1),
                            NameOfItem = reader.GetString(2),
                            Description = reader.GetString(3),
                            EstValue = reader.GetDecimal(4),
                            AgeofItem = reader.GetInt32(5),
                            DropOff = reader.GetBoolean(6),
                            PickUp = reader.GetBoolean(7),
                            PickUpDateTime = reader.GetDateTime(8),
                            DonatedImage = image,
                            MailReceipt = reader.GetBoolean(10),
                            EmailReceipt = reader.GetBoolean(11),
                            DonationStatus = reader.GetString(12),
                        };
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

            return result;
        }

        /// <summary>
        /// /// <summary>
        /// Richard Schroeder
        /// 2021/04/14
        /// Selects donation from DB by donationID 
        ///  <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// </remarks>
        /// </summary>
        /// <returns></returns>

        public Donation SelectDonationItemByID(int donationID)
        {
            Donation donationItem = null;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_select_donation_by_id", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@DonationID", SqlDbType.Int);
            cmd.Parameters["@DonationID"].Value = donationID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    var DonationID = reader.GetInt32(0);
                    var DonorID = reader.GetInt32(1);
                    var NameOfItem = reader.GetString(2);
                    var Description = reader.GetString(3);
                    var EstValue = reader.GetDecimal(4);
                    var AgeofItem = reader.GetInt32(5);
                    var DropOff = reader.GetBoolean(6);
                    var PickUp = reader.GetBoolean(7);
                    var PickUpDateTime = reader.GetDateTime(8);
                    //var DonatedImage = (Byte[])reader[9];
                    var MailReceipt = reader.GetBoolean(9);
                    var EmailReceipt = reader.GetBoolean(10);
                    var DonationStatus = reader.GetString(11);

                    donationItem = new Donation(DonationID, DonorID, NameOfItem,
                        Description, EstValue, AgeofItem, DropOff, PickUp, PickUpDateTime,
                        MailReceipt, EmailReceipt, DonationStatus);

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
            return donationItem;
        }

        /// <summary>
        /// Asaad Mohamed
        /// 2021/02/22
        /// This method for update the status of donation from database 
        /// </summary>
        ///  <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// </remarks>
        /// </summary>
        /// <param name="oldDonationItem"></param>
        /// <param name="newDonationItem"></param>
        /// <returns></returns>
        public int UpdateDonationItemStatus(Donation oldDonationItem, Donation newDonationItem)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_update_donation_item", conn);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@DonationID", oldDonationItem.DonationID);
            cmd.Parameters.AddWithValue("@DonorID", oldDonationItem.DonorID);



            cmd.Parameters.AddWithValue("@NewNameOfItem", newDonationItem.NameOfItem);
            cmd.Parameters.AddWithValue("@NewDescription", newDonationItem.Description);
            cmd.Parameters.AddWithValue("@NewEstValue", newDonationItem.EstValue);
            cmd.Parameters.AddWithValue("@NewAgeofItem", newDonationItem.AgeofItem);
            cmd.Parameters.AddWithValue("@NewDropOff", newDonationItem.DropOff);
            cmd.Parameters.AddWithValue("@NewPickUp", newDonationItem.PickUp);
            cmd.Parameters.AddWithValue("@NewPickUpDateTime", newDonationItem.PickUpDateTime);
            cmd.Parameters.AddWithValue("@NewMailReceipt", newDonationItem.MailReceipt);
            cmd.Parameters.AddWithValue("@NewEmailReceipt", newDonationItem.EmailReceipt);
            cmd.Parameters.AddWithValue("@NewDonationStatus", newDonationItem.DonationStatus);



            cmd.Parameters.AddWithValue("@OldNameOfItem", oldDonationItem.NameOfItem);
            cmd.Parameters.AddWithValue("@OldDescription", oldDonationItem.Description);
            cmd.Parameters.AddWithValue("@OldEstValue", oldDonationItem.EstValue);
            cmd.Parameters.AddWithValue("@OldAgeofItem", oldDonationItem.AgeofItem);
            cmd.Parameters.AddWithValue("@OldDropOff", oldDonationItem.DropOff);
            cmd.Parameters.AddWithValue("@OldPickUp", oldDonationItem.PickUp);
            cmd.Parameters.AddWithValue("@OldPickUpDateTime", oldDonationItem.PickUpDateTime);
            cmd.Parameters.AddWithValue("@OldMailReceipt", oldDonationItem.MailReceipt);
            cmd.Parameters.AddWithValue("@OldEmailReceipt", oldDonationItem.EmailReceipt);
            cmd.Parameters.AddWithValue("@OldDonationStatus", oldDonationItem.DonationStatus);

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
        /// Chantal Shirley
        /// Created: 2021/04/09
        /// 
        /// Retrieves image as a byte array.
        /// </summary>
        /// <param name="donationID"></param>
        /// <returns></returns>
        byte[] IDonationAccessor.SelectImageByDonationId(int donationID)
        {
            byte[] result = null;

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_select_donation_image_by_donation_id", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@DonationID", SqlDbType.NVarChar, 200);

            cmd.Parameters["@DonationID"].Value = donationID;

            try
            {
                conn.Open();

                // Read byte[] inspiration from: https://stackoverflow.com/questions/13332960/reading-binary-from-table-column-into-byte-array
                using (SqlDataReader dataReader = cmd.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        result = (byte[])dataReader["DonatedImage"];
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

            return result;
        }
    }
}
