using DataAccessInterfaces;
using DomainModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace DataAccessLayer
{
    /// <summary>
    /// Chantal Shirley
    /// Created: 2021/02/10
    /// 
    /// Class for accessing vehicle data.
    /// </summary>
    public class VehicleAccessor : IVehicleAccessor
    {
        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/02/10
        /// 
        /// Method for calling a stored procedure that
        /// archives vehicles, by placing them in
        /// a vehicle archival table.
        /// </summary>
        /// <param name="vehicle"></param>
        /// <returns></returns>
        public bool DeleteVehicle(Vehicle vehicle)
        {
            bool result = false;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_archive_vehicle_by_vin_number", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@VinNumber", SqlDbType.NVarChar, 17);

            cmd.Parameters["@VinNumber"].Value = vehicle.VinNumber;

            try
            {
                conn.Open();
                if ((cmd.ExecuteNonQuery()) == 1)
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
        /// Created: 2021/02/10
        /// 
        /// Method for retrieving all vehicles from the
        /// database in the vehicles table.
        /// </summary>
        /// <returns></returns>
        public List<Vehicle> SelectAllVehicles()
        {
            List<Vehicle> vehicles = new List<Vehicle>();

            var conn = DBConnection.GetDBConnection(); // connection
            var cmd = new SqlCommand("sp_select_all_active_vehicles", conn); // command
            cmd.CommandType = CommandType.StoredProcedure; // define command

            try // execute
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        vehicles.Add(new Vehicle // Instantiate vehicles
                        {
                            VinNumber = reader.GetString(0),
                            LicensePlateNumber = reader.GetString(1),
                            VehicleMake = reader.GetString(2),
                            VehicleModel = reader.GetString(3),
                            VehicleYear = reader.GetInt16(4),
                            LicenseClass = reader.GetString(5),
                            Mileage = reader.GetInt32(6),
                            ADACompliant = reader.GetBoolean(7),
                            IsActive = reader.GetBoolean(8),
                            GeoID = reader.GetInt32(9)
                    });
                    }
                }
                reader.Close(); // close reader
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close(); // close connection
            }

            return vehicles;
        }

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/02/12
        /// 
        /// Method recives a valid vehicle object and inserts it into the BackOnTrack database.
        /// </summary>
        /// <param name="vehicle"></param>
        /// <returns></returns>
        public int InsertVehicle(Vehicle vehicle)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_insert_vehicle", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@VinNumber", SqlDbType.NVarChar, 17);
            cmd.Parameters.Add("@LicensePlateNumber", SqlDbType.NVarChar, 10);
            cmd.Parameters.Add("@VehicleMake", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@VehicleModel", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@VehicleYear", SqlDbType.Int);
            cmd.Parameters.Add("@LicenseClass", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@Mileage", SqlDbType.Int);
            cmd.Parameters.Add("@ADACompliant", SqlDbType.Bit);
            //cmd.Parameters.Add("@GeoID", SqlDbType.Int);


            cmd.Parameters["@VinNumber"].Value = vehicle.VinNumber;
            cmd.Parameters["@LicensePlateNumber"].Value = vehicle.LicensePlateNumber;
            cmd.Parameters["@VehicleMake"].Value = vehicle.VehicleMake;
            cmd.Parameters["@VehicleModel"].Value = vehicle.VehicleModel;
            cmd.Parameters["@VehicleYear"].Value = vehicle.VehicleYear;
            cmd.Parameters["@LicenseClass"].Value = vehicle.LicenseClass;
            cmd.Parameters["@Mileage"].Value = vehicle.Mileage;
            cmd.Parameters["@ADACompliant"].Value = vehicle.ADACompliant;
            //cmd.Parameters["@GeoID"].Value = vehicle.GeoID;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
                if (result == 0)
                {
                    throw new ApplicationException("The vehicle could not be added.");
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
        /// Created: 2021/03/13
        /// 
        /// Vehicle ViewModelAccessor
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<VehicleVM> SelectAllVehiclesVMs()
        {
            ObservableCollection<VehicleVM> vehicles = new ObservableCollection<VehicleVM>();
            BitmapImage convertedImage;

            var conn = DBConnection.GetDBConnection(); // connection
            var cmd = new SqlCommand("sp_get_all_vehicles_vms", conn); // command
            cmd.CommandType = CommandType.StoredProcedure; // define command

            try // execute
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        SqlDataReader dataReader = reader;
                        var tempImg = dataReader["FilePhoto"];
                        convertedImage = new BitmapImage();
                        string isEmpty = tempImg.ToString();
                        if (isEmpty == "")
                        {
                            tempImg = null;
                            convertedImage = null;
                        }

                        if (isEmpty != "" && convertedImage != null)
                        {
                            // Convert to image, inspiration from https://stackoverflow.com/questions/14337071/convert-array-of-bytes-to-bitmapimage
                            using (var ms = new System.IO.MemoryStream((byte[])tempImg))
                            {
                                convertedImage.BeginInit();
                                convertedImage.CacheOption = BitmapCacheOption.OnLoad;
                                convertedImage.StreamSource = ms;
                                convertedImage.EndInit();
                            }
                        }

                        vehicles.Add(new VehicleVM // Instantiate vehicles
                        {
                            VinNumber = reader.GetString(0),
                            VehicleImage = convertedImage,
                            LicensePlateNumber = reader.GetString(2),
                            VehicleMake = reader.GetString(3),
                            VehicleModel = reader.GetString(4),
                            VehicleYear = reader.GetInt16(5),
                            Mileage = reader.GetInt32(6)
                        });

                    }
                }
                reader.Close(); // close reader
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close(); // close connection
            }

            return vehicles;
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/03/12
        /// 
        /// Allows archiving of vehicles via
        /// vehicle view model.
        /// </summary>
        /// <param name="vehicle"></param>
        /// <returns></returns>
        public bool DeleteVehicleThroughVM(VehicleVM vehicle)
        {
            bool result = false;

            int rowsChanged = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_archive_vehicle_by_vin_number", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@VinNumber", SqlDbType.NVarChar, 17);

            cmd.Parameters["@VinNumber"].Value = vehicle.VinNumber;

            try
            {
                conn.Open();
                if ((rowsChanged = cmd.ExecuteNonQuery()) == 1)
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
        /// Created: 2021/03/24
        /// </summary>
        /// <param name="vinNumber"></param>
        /// <param name="licensePlateNumber"></param>
        /// <param name="mileage"></param>
        /// <param name="bitmapImage"></param>
        /// <returns></returns>
        public bool UpdateVehicleThroughVMByVin(string vinNumber, string licensePlateNumber, 
            string mileage)
        {
            bool result = false;
            ImageConverter imgConverter = new ImageConverter();
            int rowsChanged = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_update_vehicle_by_vin_number", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@VinNumber", SqlDbType.NVarChar, 17);
            cmd.Parameters.Add("@Mileage", SqlDbType.Int);
            cmd.Parameters.Add("@LicensePlateNumber", SqlDbType.NVarChar, 10);

            cmd.Parameters["@VinNumber"].Value = vinNumber;
            cmd.Parameters["@Mileage"].Value = mileage;
            cmd.Parameters["@LicensePlateNumber"].Value = licensePlateNumber;

            try
            {
                conn.Open();
                if ((rowsChanged = cmd.ExecuteNonQuery()) == 1)
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
    }
}
