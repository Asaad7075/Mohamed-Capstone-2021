using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DomainModels;

namespace DataAccessLayer
{
    /// <summary>
    /// Zach Stultz
    /// Created: 2021/02/12
    ///
    /// Vehicle Maintenance Report Accessors Class.
    /// <summary>
    public class VehicleMaintenanceReportAccessor : IVehicleMaintenanceReportAccessor
    {
        /// <summary>
        /// Zach Stultz
        /// Created: 2021/02/16
        ///
        /// Inserts the maintenance report.
        /// </summary>
        /// <param name="vehicleMaintenanceReport">The vehicle maintenance report.</param>
        /// <returns>A bool.</returns>
        public bool InsertVehicleMaintenanceReport(VehicleMaintenanceReportVM vehicleMaintenanceReport)
        {
            bool result = false;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_insert_vehicle_maintenance_report", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@VinNumber", SqlDbType.NVarChar, 17);
            cmd.Parameters.Add("@VehicleMaintenanceTypeName", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@VehicleMaintenanceServiceDate", SqlDbType.DateTime);
            cmd.Parameters.Add("@MaintenanceFinished", SqlDbType.Bit);
            cmd.Parameters.Add("@VehicleMaintenanceNotes", SqlDbType.NVarChar, 250);
            cmd.Parameters.Add("@isQueued", SqlDbType.Bit);

            cmd.Parameters["@VinNumber"].Value = vehicleMaintenanceReport.VinNumber;
            cmd.Parameters["@VehicleMaintenanceTypeName"].Value = vehicleMaintenanceReport.VehicleMaintenanceTypeName;
            cmd.Parameters["@VehicleMaintenanceServiceDate"].Value = vehicleMaintenanceReport.VehicleMaintenanceServiceDate;
            cmd.Parameters["@MaintenanceFinished"].Value = vehicleMaintenanceReport.MaintenanceFinished;
            cmd.Parameters["@VehicleMaintenanceNotes"].Value = vehicleMaintenanceReport.VehicleMaintenanceNotes;
            cmd.Parameters["@isQueued"].Value = vehicleMaintenanceReport.IsQueued;

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
        /// Created: 2021/03/19
        ///
        /// Updates the vehicle maintenance report.
        /// </summary>
        /// <param name="vehicleMaintenanceReport">The vehicle maintenance report.</param>
        /// <returns>An int.</returns>
        public int UpdateVehicleMaintenanceReport(VehicleMaintenanceReportVM oldVehicleMaintenanceReport, VehicleMaintenanceReportVM newVehicleMaintenanceReport)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_update_vehicle_maintenance_report", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@VehicleMaintenanceReportID", SqlDbType.Int);
            cmd.Parameters.Add("@OldVinNumber", SqlDbType.NVarChar, 17);
            cmd.Parameters.Add("@OldVehicleMaintenanceTypeName", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@OldVehicleMaintenanceServiceDate", SqlDbType.DateTime);
            cmd.Parameters.Add("@OldMaintenanceFinished", SqlDbType.Bit);
            cmd.Parameters.Add("@OldVehicleMaintenanceNotes", SqlDbType.NVarChar, 250);
            cmd.Parameters.Add("@OldisQueued", SqlDbType.Bit);

            cmd.Parameters.Add("@NewVinNumber", SqlDbType.NVarChar, 17);
            cmd.Parameters.Add("@NewVehicleMaintenanceTypeName", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@NewVehicleMaintenanceServiceDate", SqlDbType.DateTime);
            cmd.Parameters.Add("@NewMaintenanceFinished", SqlDbType.Bit);
            cmd.Parameters.Add("@NewVehicleMaintenanceNotes", SqlDbType.NVarChar, 250);
            cmd.Parameters.Add("@NewisQueued", SqlDbType.Bit);

            cmd.Parameters["@VehicleMaintenanceReportID"].Value = oldVehicleMaintenanceReport.VehicleMaintenanceReportID;
            cmd.Parameters["@OldVinNumber"].Value = oldVehicleMaintenanceReport.VinNumber;
            cmd.Parameters["@OldVehicleMaintenanceTypeName"].Value = oldVehicleMaintenanceReport.VehicleMaintenanceTypeName;
            cmd.Parameters["@OldVehicleMaintenanceServiceDate"].Value = oldVehicleMaintenanceReport.VehicleMaintenanceServiceDate;
            cmd.Parameters["@OldMaintenanceFinished"].Value = oldVehicleMaintenanceReport.MaintenanceFinished;
            cmd.Parameters["@OldVehicleMaintenanceNotes"].Value = oldVehicleMaintenanceReport.VehicleMaintenanceNotes;
            cmd.Parameters["@OldisQueued"].Value = oldVehicleMaintenanceReport.IsQueued;

            cmd.Parameters["@NewVinNumber"].Value = newVehicleMaintenanceReport.VinNumber;
            cmd.Parameters["@NewVehicleMaintenanceTypeName"].Value = newVehicleMaintenanceReport.VehicleMaintenanceTypeName;
            cmd.Parameters["@NewVehicleMaintenanceServiceDate"].Value = newVehicleMaintenanceReport.VehicleMaintenanceServiceDate;
            cmd.Parameters["@NewMaintenanceFinished"].Value = newVehicleMaintenanceReport.MaintenanceFinished;
            cmd.Parameters["@NewVehicleMaintenanceNotes"].Value = newVehicleMaintenanceReport.VehicleMaintenanceNotes;
            cmd.Parameters["@NewisQueued"].Value = newVehicleMaintenanceReport.IsQueued;

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
        /// Zach Stultz
        /// Created: 2021/03/19
        ///
        /// Selects all the active maintenance reports.
        /// </summary>
        /// <returns>A list of VehicleMaintenanceReports.</returns>
        public List<VehicleMaintenanceReportVM> SelectAllActiveMaintenanceReports()
        {
            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_retrieve_all_active_maintenance_reports", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@isQueued", SqlDbType.NVarChar, 17);
            cmd.Parameters["@isQueued"].Value = true;

            List<VehicleMaintenanceReportVM> vehicleMaintenanceReport = new List<VehicleMaintenanceReportVM>();

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        VehicleMaintenanceReportVM foundVehicleMaintenanceReport = new VehicleMaintenanceReportVM()
                        {
                            VehicleMaintenanceReportID = reader.GetInt32(0),
                            VinNumber = reader.GetString(1),
                            VehicleMaintenanceTypeName = reader.GetString(2),
                            VehicleMaintenanceServiceDate = reader.GetDateTime(3),
                            MaintenanceFinished = reader.GetBoolean(4),
                            VehicleMaintenanceNotes = reader.GetString(5),
                            IsQueued = reader.GetBoolean(6),
                        };
                        if (foundVehicleMaintenanceReport.IsQueued == true)
                        {
                            vehicleMaintenanceReport.Add(foundVehicleMaintenanceReport);
                        }
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

            return vehicleMaintenanceReport;
        }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/12
        ///
        /// Selects the vehicle maintenance report by Vin.
        /// </summary>
        /// <param name="VinNumber">The Vin number.</param>
        /// <returns>An int.</returns>
        public List<VehicleMaintenanceReportVM> SelectVehicleMaintenanceReportByVin(string vinNumber)
        {
            List<VehicleMaintenanceReportVM> vehicleMaintenanceReports = new List<VehicleMaintenanceReportVM>();
            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_retrieve_vehicle_maintenance_report_by_vin", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@VinNumber", SqlDbType.NVarChar, 17);
            cmd.Parameters["@VinNumber"].Value = vinNumber;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        VehicleMaintenanceReportVM foundVehicleMaintenanceReport = new VehicleMaintenanceReportVM()
                        {
                            VehicleMaintenanceReportID = reader.GetInt32(0),
                            VinNumber = reader.GetString(1),
                            VehicleMaintenanceTypeName = reader.GetString(2),
                            VehicleMaintenanceServiceDate = reader.GetDateTime(3),
                            MaintenanceFinished = reader.GetBoolean(4),
                            VehicleMaintenanceNotes = reader.GetString(5),
                            IsQueued = reader.GetBoolean(6),
                        };
                        vehicleMaintenanceReports.Add(foundVehicleMaintenanceReport);
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

            return vehicleMaintenanceReports;
        }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/19
        ///
        /// Retrieves all Vehicle Maintenance Reports
        /// <returns></returns>
        public List<VehicleMaintenanceReportVM> SelectAllVehicleMaintenanceReports()
        {
            List<VehicleMaintenanceReportVM> data = new List<VehicleMaintenanceReportVM>();

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_select_all_vehicle_maintenance_report", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        data.Add(new VehicleMaintenanceReportVM
                        {
                            VehicleMaintenanceReportID = reader.GetInt32(0),
                            VinNumber = reader.GetString(1),
                            VehicleMaintenanceTypeName = reader.GetString(2),
                            VehicleMaintenanceServiceDate = reader.GetDateTime(3),
                            MaintenanceFinished = reader.GetBoolean(4),
                            VehicleMaintenanceNotes = reader.GetString(5),
                            IsQueued = reader.GetBoolean(6),
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
