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
    /// Zach Stultz
    /// Created: 2021/03/19
    ///
    /// The vehicle maintenance status accessors class.
    /// </summary>
    public class VehicleMaintenanceStatusAccessor : IVehicleMaintenanceStatusAccessor
    {
        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/23
        ///
        /// Inserts the vehicle maintenance status.
        /// </summary>
        /// <param name="vehicleMaintenanceStatus">The vehicle maintenance status.</param>
        /// <returns>A bool.</returns>
        public bool InsertVehicleMaintenanceStatus(VehicleMaintenanceStatus vehicleMaintenanceStatus)
        {
            bool result = false;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_insert_vehicle_maintenance_status", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@VinNumber", SqlDbType.NVarChar, 17);
            cmd.Parameters.Add("@MaintenanceStatusType", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@HasMaintenanceReport", SqlDbType.Bit);
            cmd.Parameters.Add("@IdentifiedMaintenance", SqlDbType.NVarChar, 500);

            cmd.Parameters["@VinNumber"].Value = vehicleMaintenanceStatus.VinNumber;
            cmd.Parameters["@MaintenanceStatusType"].Value = vehicleMaintenanceStatus.MaintenanceStatusType;
            cmd.Parameters["@HasMaintenanceReport"].Value = vehicleMaintenanceStatus.HasMaintenanceReport;
            cmd.Parameters["@IdentifiedMaintenance"].Value = vehicleMaintenanceStatus.IdentifiedMaintenance;
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
                    throw new ApplicationException("The maintenance report could not be added.");
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
        /// Zach Stultz
        /// Created: 2021/03/23
        ///
        /// Inserts the vehicle maintenance status type.
        /// </summary>
        /// <param name="vehicleMaintenanceStatusType">The vehicle maintenance status type.</param>
        /// <returns>A bool.</returns>
        public bool InsertVehicleMaintenanceStatusType(VehicleMaintenanceStatusType vehicleMaintenanceStatusType)
        {
            bool result = false;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_insert_vehicle_maintenance_status_type", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@MaintenanceStatusType", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@MaintenanceStatusDescription", SqlDbType.NVarChar, 100);

            cmd.Parameters["@MaintenanceStatusType"].Value = vehicleMaintenanceStatusType.MaintenanceStatusType;
            cmd.Parameters["@MaintenanceStatusDescription"].Value = vehicleMaintenanceStatusType.MaintenanceStatusDescription;

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
                    throw new ApplicationException("The maintenance report could not be added.");
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
        /// Zach Stultz
        /// Created: 2021/03/25
        ///
        /// Selects the all vehicle maintenance statuses.
        /// </summary>
        /// <returns>A list of VehicleMaintenanceStatuses.</returns>
        public List<VehicleMaintenanceStatus> SelectAllVehicleMaintenanceStatuses()
        {
            List<VehicleMaintenanceStatus> data = new List<VehicleMaintenanceStatus>();

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_select_all_vehicle_maintenance_statuses", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        data.Add(new VehicleMaintenanceStatus
                        {
                            StatusID = reader.GetInt32(0),
                            VinNumber = reader.GetString(1),
                            MaintenanceStatusType = reader.GetString(2),
                            HasMaintenanceReport = reader.GetBoolean(3),
                            IdentifiedMaintenance = reader.GetString(4)
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

            return data;
        }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/25
        ///
        /// Selects the all vehicle maintenance status types.
        /// </summary>
        /// <returns>A list of VehicleMaintenanceStatusTypes.</returns>
        public List<VehicleMaintenanceStatusType> SelectAllVehicleMaintenanceStatusTypes()
        {
            List<VehicleMaintenanceStatusType> data = new List<VehicleMaintenanceStatusType>();

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_select_all_vehicle_maintenance_status_types", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        data.Add(new VehicleMaintenanceStatusType
                        {
                            MaintenanceStatusType = reader.GetString(0),
                            MaintenanceStatusDescription = reader.GetString(1)
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

            return data;
        }
    }
}
