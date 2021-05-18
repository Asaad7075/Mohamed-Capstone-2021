using DataAccessInterfaces;
using DomainModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    /// <summary>
    /// Chase Martin
    /// Created: 2021/02/12
    /// 
    /// Class for accessing zip code data.
    /// </summary>
    public class ZipCodeAccessor : IZipCodeAccessor
    {
        public int InsertZipCode(ZipCodeFile zipCode)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_add_zipcode", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ZipCode", SqlDbType.NVarChar, 25);
            cmd.Parameters.Add("@City", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@State", SqlDbType.NVarChar, 2);
            cmd.Parameters.Add("@isServicable", SqlDbType.Bit);

            cmd.Parameters["@ZipCode"].Value = zipCode.ZipCode;
            cmd.Parameters["@City"].Value = zipCode.City;
            cmd.Parameters["@State"].Value = zipCode.State;
            cmd.Parameters["@isServicable"].Value = zipCode.isServicable;

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
        /// Chase Martin
        /// Created: 2021/04/09
        /// 
        /// Returns true or false if a new zip code is inserted into DB
        /// </summary>
        public bool InsertZipCodeBool(ZipCodeFile zipCode)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/04/09
        /// 
        /// Selects all existing zip codes and returns.
        /// </summary>
        public List<ZipCodeFile> SelectAllZipCodes()
        {
            List<ZipCodeFile> zipCodes = new List<ZipCodeFile>();

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_select_all_zipcodes", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        var zipCode = new ZipCodeFile()
                        {
                            ZipCode = reader.GetString(0),
                            City = reader.GetString(1),
                            State = reader.GetString(2),
                            isServicable = reader.GetBoolean(3),
                        };
                        zipCodes.Add(zipCode);
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



            return zipCodes;
        }


        /// <summary>
        /// Chase Martin
        /// Created: 2021/04/09
        /// 
        /// Inserts zip code(s) whether or not it is in service or not.
        /// </summary>
        public List<ZipCodeVM> SelectZipCodesByIsServicable(bool isServicable = true)
        {

            List<ZipCodeVM> zipCodes = new List<ZipCodeVM>();

            // connection
            var conn = DBConnection.GetDBConnection();
            // command
            var cmd = new SqlCommand("sp_select_zipcodes_by_isServicable", conn);
            // command type
            cmd.CommandType = CommandType.StoredProcedure;
            // parameters with value the shortcut way
            cmd.Parameters.AddWithValue("@isServicable", isServicable);

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var zipCode = new ZipCodeVM()
                        {
                            ZipCode = reader.GetString(0),
                            City = reader.GetString(1),
                            State = reader.GetString(2),
                            isServicable = reader.GetBoolean(3),
                        };
                        zipCodes.Add(zipCode);
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
            return zipCodes;
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/04/10
        /// 
        /// Accesses stored procedure to update a zip code file
        /// </summary>
        public int UpdateZipCodeFile(ZipCodeFile oldZipCode, ZipCodeFile newZipCode)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_update_zipcode", conn);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.Add("@NewZipCode", SqlDbType.NVarChar, 25);
            cmd.Parameters.Add("@NewCity", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@NewState", SqlDbType.NVarChar, 2);
            cmd.Parameters.Add("@NewIsServicable", SqlDbType.Bit);
            cmd.Parameters.Add("@OldZipCode", SqlDbType.NVarChar, 25);
            cmd.Parameters.Add("@OldCity", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@OldState", SqlDbType.NVarChar, 2);
            cmd.Parameters.Add("@OldIsServicable", SqlDbType.Bit);

            cmd.Parameters["@NewZipCode"].Value = newZipCode.ZipCode;
            cmd.Parameters["@NewCity"].Value = newZipCode.City;
            cmd.Parameters["@NewState"].Value = newZipCode.State;
            cmd.Parameters["@NewIsServicable"].Value = newZipCode.isServicable;

            cmd.Parameters["@OldZipCode"].Value = oldZipCode.ZipCode;
            cmd.Parameters["@OldCity"].Value = oldZipCode.City;
            cmd.Parameters["@OldState"].Value = oldZipCode.State;
            cmd.Parameters["@OldIsServicable"].Value = oldZipCode.isServicable;

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
        /// Chase Martin
        /// Created: 2021/04/09
        /// 
        /// Returns true or false depending on whether or not the zip code
        /// file has been updated successfully.
        /// </summary>
        public bool UpdateZipCodeFileBool(ZipCodeFile oldZipCode, ZipCodeFile newZipCode)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/04/09
        /// 
        /// Selects zip codes by the given status.
        /// </summary>
        public List<ZipCodeFile> SelectZipCodesByStatus(string status)
        {
            List<ZipCodeFile> zipCodes = new List<ZipCodeFile>();

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_select_zipcodes_by_zipcode", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ZipCode", SqlDbType.NVarChar, 25);
            cmd.Parameters["@ZipCode"].Value = status;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        zipCodes.Add(new ZipCodeFile()
                        {
                            //ZipCodeID = reader.GetInt32(0),
                            ZipCode = reader.GetString(0),
                            City = reader.GetString(1),
                            State = reader.GetString(2),
                            isServicable = reader.GetBoolean(3),
                            //ZipCodeStatusID = reader.GetString(5),
                            //Active = reader.GetBoolean(4)
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
            return zipCodes;
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/04/09
        /// 
        /// Update zip code satus based off of selected ZipCodeID.
        /// </summary>
        public int UpdateZipCodeStatus(int zipCodeID, string oldStatus, string newStatus)
        {
            int rows = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_update_zipcode_status", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ZipCodeID", zipCodeID);

            cmd.Parameters.Add("@OldZipCodeStatusID", SqlDbType.NVarChar, 25);
            cmd.Parameters["@OldZipCodeStatusID"].Value = oldStatus;

            cmd.Parameters.Add("@NewZipCodeStatusID", SqlDbType.NVarChar, 25);
            cmd.Parameters["@NewZipCodeStatusID"].Value = newStatus;

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return rows;
        }
    }
}
