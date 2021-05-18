using DataAccessInterfaces;
using DomainModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace DataAccessLayer
{
    /// <summary>
    /// Chantal Shirley
    /// Created: 2021/03/12
    /// 
    /// General Image Accessor Class
    /// </summary>
    public class VehicleImageAccessor : IVehicleImageAccessor
    {
        /// <summary>
        /// Chantal Shirley
        /// </summary>
        /// <param name="byteImage"></param>
        /// <param name="vinNumber"></param>
        /// <returns>The result of whether or not
        /// the image was saved. True representing that
        /// the insertion was successful, and false the
        /// opposite.</returns>
        public bool InsertImageByVin(byte[] byteImage, string vinNumber)
        {
            bool imageSaved = false;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_insert_image_by_vin", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@FilePhoto", byteImage);
            cmd.Parameters.Add("@VehicleVinNumber", SqlDbType.NVarChar, 17);

            cmd.Parameters["@VehicleVinNumber"].Value = vinNumber;

            try
            {
                conn.Open();
                if (cmd.ExecuteNonQuery() == 1)
                {
                    imageSaved = true;
                } 
                else
                {
                    throw new Exception("Vehicle image could not be inserted.");
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

            return imageSaved;
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/03/24
        /// 
        /// Image accessor for vehicles by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns>The selected image.</returns>
        public Byte[] SelectVehicleImageByName(string name)
        {
            byte[] initialImg = null;

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_get_image_by_name", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@PhotoName", SqlDbType.NVarChar, 200);

            cmd.Parameters["@PhotoName"].Value = name;

            try
            {
                conn.Open();

                // Read byte[] inspiration from: https://stackoverflow.com/questions/13332960/reading-binary-from-table-column-into-byte-array
                using (SqlDataReader dataReader = cmd.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        initialImg = (byte[])dataReader["FilePhoto"];
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

            return initialImg;
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/03/12
        /// 
        /// Image accessor for vehicles by vin
        /// and photo name.
        /// </summary>
        /// <param name="vinNumber"></param>
        /// <param name="photoName"></param>
        /// <returns>The selected image.</returns>
        public BitmapImage SelectImageByVin(string vinNumber)
        {
            BitmapImage imageResult = new BitmapImage();
            byte[] initialImg = null;

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_get_image_by_vin", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@VehicleVinNumber", SqlDbType.NVarChar, 17);

            cmd.Parameters["@VehicleVinNumber"].Value = vinNumber;

            try
            {
                conn.Open();

                // Read byte[] inspiration from: https://stackoverflow.com/questions/13332960/reading-binary-from-table-column-into-byte-array
                using (SqlDataReader dataReader = cmd.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        initialImg = (byte[])dataReader["FilePhoto"];
                    }
                }

                if (initialImg != null)
                {
                    // Convert to image, inspiration from https://stackoverflow.com/questions/14337071/convert-array-of-bytes-to-bitmapimage
                    using (var ms = new System.IO.MemoryStream(initialImg))
                    {
                        imageResult.BeginInit();
                        imageResult.CacheOption = BitmapCacheOption.OnLoad;
                        imageResult.StreamSource = ms;
                        imageResult.EndInit();
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

            return imageResult;
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/03/24
        /// 
        /// Updates image based on vin no.
        /// </summary>
        /// <param name="byteImage"></param>
        /// <param name="vinNumber"></param>
        /// <returns></returns>
        public bool UpdateImageByVin(byte[] byteImage, string vinNumber)
        {
            bool imageUpdated = false;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_update_image_by_vin", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@FilePhoto", byteImage);
            cmd.Parameters.Add("@VehicleVinNumber", SqlDbType.NVarChar, 17);

            cmd.Parameters["@VehicleVinNumber"].Value = vinNumber;

            try
            {
                conn.Open();
                if (cmd.ExecuteNonQuery() == 1)
                {
                    imageUpdated = true;
                }
                else
                {
                    throw new Exception("Vehicle image could not be updated.");
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

            return imageUpdated;
        }
    }
}
