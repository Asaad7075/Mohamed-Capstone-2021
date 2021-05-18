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
    /// Created: 2021/02/13
    /// 
    /// Class for accessing drivers license data. Not implemented yet.
    /// </summary>
    public class DriversLicenseAccessor : IDriversLicenseAccessor
    {
        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/02/13
        /// 
        /// Method for inserting a new drivers license into the 
        /// database.
        /// </summary>
        /// <param name="driversLicense"></param>
        /// <returns></returns>
        public bool InsertDriversLicense(DriversLicense driversLicense)
        {
            bool result = false;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_insert_drivers_license", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@LicenseNumber", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@LicenseType", SqlDbType.NVarChar, 15);
            cmd.Parameters.Add("@IsActive", SqlDbType.Bit);

            cmd.Parameters["@LicenseNumber"].Value = driversLicense.LicenseNumber;
            cmd.Parameters.AddWithValue("@EmployeeID", driversLicense.EmployeeID);
            cmd.Parameters["@LicenseType"].Value = driversLicense.LicenseType;
            cmd.Parameters.AddWithValue("@LicenseIssuedDate", driversLicense.LicenseIssuedDate);
            cmd.Parameters.AddWithValue("@LicenseExpirydate", driversLicense.LicenseExpiryDate);
            cmd.Parameters["@IsActive"].Value = Convert.ToByte(driversLicense.IsActive);

            try
            {
                conn.Open();
                int currResult = cmd.ExecuteNonQuery();
                if (currResult == 1)
                {
                    result = true;
                }
                else if (currResult == 0)
                {
                    throw new ApplicationException("The drivers license couldn't be added.");
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
        /// Created: 2021/02/13
        /// 
        /// Method for retrieving drivers licenses by license number.
        /// If it exists it will return true, if not it will return false;
        /// </summary>
        /// <param name="LicenseNumber"></param>
        /// <returns></returns>
        public bool SelectDriversLicenseByLicenseNumber(string LicenseNumber)
        {
            bool result = false;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_retrieve_drivers_license_by_license_number", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@LicenseNumber", SqlDbType.NVarChar, 100);

            cmd.Parameters["@LicenseNumber"].Value = LicenseNumber;

            try
            {
                conn.Open();
                int currResult = (int)cmd.ExecuteScalar();
                if (currResult == 1)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/03/10
        /// 
        /// Method for retrieving all drivers license types.
        /// </summary>
        /// <returns></returns>
        public List<string> SelectDriversLicenseTypes()
        {
            List<string> driversLicenseTypes = new List<string>();

            var conn = DBConnection.GetDBConnection(); // connection
            var cmd = new SqlCommand("sp_select_all_drivers_license_classes", conn); // command
            cmd.CommandType = CommandType.StoredProcedure; // define command

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        driversLicenseTypes.Add(reader.GetString(0)); // Add license types00020-324386277
                    }
                }

                reader.Close(); // Close readers
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return driversLicenseTypes;
        }
    }
}
