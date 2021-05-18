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
    public class SupplyInventoryAccessor : ISupplyInventoryAccessor
    {
        /// <summary>
        /// Richard Schroeder
        /// Created: 2021/03/12
        /// 
        /// Method for inserting a new supplyitem into the supply inventory table
        /// </summary>
        /// <returns></returns>
        public int AddSupplyItem(SupplyItem supplyItem)
        {
            int result = 0;// returns 1 if insert was successful

            var conn = DBConnection.GetDBConnection();// connection

            var cmd = new SqlCommand("sp_insert_supply_item", conn); // create stored procedure command

            cmd.CommandType = CommandType.StoredProcedure;// call stored procedure

            cmd.Parameters.Add("@SupplySerialNumber", SqlDbType.Int);
            cmd.Parameters.Add("@MaterialName", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@SupplyDescription", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@SupplyInventoryQuantity", SqlDbType.Int);

            cmd.Parameters["@SupplySerialNumber"].Value = supplyItem.SupplySerialNumber;
            cmd.Parameters["@MaterialName"].Value = supplyItem.MaterialName;
            cmd.Parameters["@SupplyDescription"].Value = supplyItem.SupplyDescription;
            cmd.Parameters["@SupplyInventoryQuantity"].Value = supplyItem.SupplyInventoryQuantity;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery(); // return 1 if result was successful
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
        /// Richard Schroeder
        /// Created: 2021/03/12
        /// 
        /// Method for deleting a supplyitem from the supply inventory table
        /// </summary>
        /// <returns></returns>
        public int DeleteSupplyItem(int supplyItemID)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection(); // connection
            var cmd = new SqlCommand("sp_delete_supply_item", conn); // command
            cmd.CommandType = CommandType.StoredProcedure; // define command

            cmd.Parameters.Add("@SupplyInventoryID", SqlDbType.Int);

            cmd.Parameters["@SupplyInventoryID"].Value = supplyItemID;

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
        /// Richard Schroeder
        /// Created: 2021/03/12
        /// 
        /// Method for updating existing supply items with new information
        /// </summary>
        /// <returns></returns>
        public int EditSupplyItem(SupplyItem oldSupplyItem, SupplyItem newSupplyItem)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection(); // connection
            var cmd = new SqlCommand("sp_update_supply_item", conn); // command
            cmd.CommandType = CommandType.StoredProcedure; // define command

            // add parameter for old item db ID
            cmd.Parameters.Add("@SupplyInventoryID", SqlDbType.Int);

            // add parameters for old item
            cmd.Parameters.Add("@OldSupplySerialNumber", SqlDbType.Int);
            cmd.Parameters.Add("@OldMaterialName", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@OldSupplyDescription", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@OldSupplyInventoryQuantity", SqlDbType.Int);

            // add parameters for new item
            cmd.Parameters.Add("@NewSupplySerialNumber", SqlDbType.Int);
            cmd.Parameters.Add("@NewMaterialName", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@NewSupplyDescription", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@NewSupplyInventoryQuantity", SqlDbType.Int);

            // set parameter values of old item db ID
            cmd.Parameters["@SupplyInventoryID"].Value = oldSupplyItem.SupplyItemID;

            // set parameter values for old item
            cmd.Parameters["@OldSupplySerialNumber"].Value = oldSupplyItem.SupplySerialNumber;
            cmd.Parameters["@OldMaterialName"].Value = oldSupplyItem.MaterialName;
            cmd.Parameters["@OldSupplyDescription"].Value = oldSupplyItem.SupplyDescription;
            cmd.Parameters["@OldSupplyInventoryQuantity"].Value = oldSupplyItem.SupplyInventoryQuantity;

            // set parameter values for new item
            cmd.Parameters["@NewSupplySerialNumber"].Value = newSupplyItem.SupplySerialNumber;
            cmd.Parameters["@NewMaterialName"].Value = newSupplyItem.MaterialName;
            cmd.Parameters["@NewSupplyDescription"].Value = newSupplyItem.SupplyDescription;
            cmd.Parameters["@NewSupplyInventoryQuantity"].Value = newSupplyItem.SupplyInventoryQuantity;

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
                conn.Close(); // close connection
            }
            return result;
        }

        /// <summary>
        /// Richard Schroeder
        /// Created: 2021/03/12
        /// 
        /// Method for retrieving all supply items in the database table
        /// </summary>
        /// <returns></returns>
        public List<SupplyItem> GetSupplyInventory()
        {
            List<SupplyItem> supplyInventory = new List<SupplyItem>();

            var conn = DBConnection.GetDBConnection(); // connection
            var cmd = new SqlCommand("sp_select_supply_inventory", conn); // command
            cmd.CommandType = CommandType.StoredProcedure; // define command

            try // execute
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        supplyInventory.Add(new SupplyItem // Instantiate vehicles
                        {
                            SupplyItemID = reader.GetInt32(0),
                            SupplySerialNumber = reader.GetInt32(1),
                            MaterialName = reader.GetString(2),
                            SupplyDescription = reader.GetString(3),
                            SupplyInventoryQuantity = reader.GetInt32(4)
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
            return supplyInventory;
        }
    }
}
