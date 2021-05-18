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
    public class InventoryAccessor : IInventoryAccessor
    {
        /// <summary>
        /// Thomas Stout
        /// Created: 2021/02/27
        /// 
        /// Accessor method for deleting an inventory item.
        /// </summary>
        /// <param name="inventoryID"></param>
        /// <returns></returns>
        public int DeleteInventoryItem(int inventoryID)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_Delete_Inventory_Item", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("@InventoryID", inventoryID);

            cmd.Parameters.Add(param);
            cmd.Parameters["@InventoryID"].Value = inventoryID;
            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
                Console.WriteLine("Item WAS deleted!");
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
        /// Thomas Stout
        /// Created: 2021/02/17
        /// 
        /// Accessor method for inserting an inventory item.
        /// </summary>
        /// <param name="inventory"></param>
        /// <returns></returns>
        public int InsertInventoryItem(Inventory inventory)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_Insert_Inventory_Item", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@InventoryQuantity", SqlDbType.Int);
            cmd.Parameters.Add("@ItemName", SqlDbType.NVarChar, 250);
            cmd.Parameters.Add("@ItemDescription", SqlDbType.NVarChar, 500);

            cmd.Parameters["@InventoryQuantity"].Value = inventory.InventoryQuantity;
            cmd.Parameters["@ItemName"].Value = inventory.ItemName;
            cmd.Parameters["@ItemDescription"].Value = inventory.ItemDescription;
            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
                Console.WriteLine("Item WAS added!");
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
        /// Thomas Stout
        /// Created: 2021/02/25
        /// 
        /// Accessor method for selecting all inventory items.
        /// </summary>
        /// <returns></returns>
        public List<Inventory> SelectAllInventoryItems()
        {
            List<Inventory> items = new List<Inventory>();

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_Select_All_Inventory_Items", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var inventory = new Inventory()
                        {
                            InventoryID = reader.GetInt32(0),
                            InventoryQuantity = reader.GetInt32(1),
                            ItemName = reader.GetString(2),
                            ItemDescription = reader.GetString(3)
                        };
                        items.Add(inventory);
                    }
                }
                reader.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return items;
        }

        /// <summary>
        /// Thomas Stout
        /// Created: 2021/02/25
        /// 
        /// Accessor method for updating an inventory item.
        /// </summary>
        /// <param name="inventory"></param>
        /// <returns></returns>
        public int UpdateInventoryItem(Inventory inventory)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_Update_Inventory_Item", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@InventoryID", SqlDbType.Int);
            cmd.Parameters.Add("@InventoryQuantity", SqlDbType.Int);
            cmd.Parameters.Add("@ItemName", SqlDbType.NVarChar, 250);
            cmd.Parameters.Add("@ItemDescription", SqlDbType.NVarChar, 500);

            cmd.Parameters["@InventoryID"].Value = inventory.InventoryID;
            cmd.Parameters["@InventoryQuantity"].Value = inventory.InventoryQuantity;
            cmd.Parameters["@ItemName"].Value = inventory.ItemName;
            cmd.Parameters["@ItemDescription"].Value = inventory.ItemDescription;
            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
                Console.WriteLine("Item WAS updated!");
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
