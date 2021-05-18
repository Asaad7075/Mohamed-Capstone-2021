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
    /// Created: 2021/04/11
    /// 
    /// Created to access order table.
    /// </summary>
    public class OrderAccessor : IOrderAccessor
    {
        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/11
        /// ----------------------
        /// Updated: 04/27/2021
        /// Updater: Rick Schroeder
        /// Notes: Added a dateRequested string of DateTime.Now.ToShortDateString()
        ///        to pull orders with like dates
        /// ----------------------
        /// Inserts order into order table
        /// </summary>
        /// <param name="donationID"></param>
        /// <param name="clientID"></param>
        /// <param name="dateRequested"></param>
        /// <returns></returns>
        public int InsertOrder(int clientID, int donationID, string dateRequested)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_insert_order", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ClientID", SqlDbType.Int);
            cmd.Parameters.Add("@DonationID", SqlDbType.Int);
            cmd.Parameters.Add("@DateOrdered", SqlDbType.NVarChar, 10);

            cmd.Parameters["@ClientID"].Value = clientID;
            cmd.Parameters["@DonationID"].Value = donationID;
            cmd.Parameters["@DateOrdered"].Value = dateRequested;


            try
            {
                conn.Open();

                result = Convert.ToInt32(cmd.ExecuteScalar());
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
        /// Created: 2021/04/25
        /// 
        /// Retrieves an order 
        /// based on the client and order id.
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="clientID"></param>
        /// <returns></returns>
        public bool SelectOrderByClientIDandOrderID(int orderID, int clientID)
        {
            bool result = false;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("dbo.sp_select_order_by_orderid_and_clientid", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@OrderID", SqlDbType.Int);
            cmd.Parameters.Add("@ClientID", SqlDbType.Int);

            cmd.Parameters["@OrderID"].Value = orderID;
            cmd.Parameters["@ClientID"].Value = clientID;

            try
            {
                conn.Open();

                int rawResult = 0;

                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        rawResult = reader.GetInt32(0);
                    }
                }

                if (rawResult == 1)
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
        /// Created: 2021/04/11
        /// 
        /// Retrieves order ids by client ids.
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        /// <remarks>
        /// Chantal Shirley
        /// Updated: 2021/04/20
        ///
        /// Needed to reflect multiple orders and was 
        /// removed to implement new feature.
        /// </remarks>
        public Order SelectOrderByOrderID(int ticketOrderId)
        {
            Order order = new Order();
            order.OrderID = ticketOrderId;
            order.Items = new Dictionary<int, Donation>();

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_select_order_by_order_id", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@OrderID", ticketOrderId);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        order.Items.Add
                        (
                            reader.GetInt32(2),
                            new Donation
                            {
                                DonationID = reader.GetInt32(2),
                                OrderQty = reader.GetInt32(3)
                            }
                        ); ;
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

            return order;
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/11
        /// 
        /// Retrieves order ids by client ids.
        /// </summary>
        /// <param name="clientID"></param>
        /// <returns></returns>
        public List<Order> SelectOrdersByClientID(int clientID)
        {
            List<Order> orders = new List<Order>();
            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_select_orders_by_client_id", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ClientID", SqlDbType.Int);
            cmd.Parameters["@ClientID"].Value = clientID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {

                        var order = new Order()
                        {
                            OrderID = reader.GetInt32(0),
                            DonationID = reader.GetInt32(1),
                            ClientID = reader.GetInt32(2),
                            DateRequested = reader.GetString(3)
                        };
                        orders.Add(order);
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

            return orders;
        }
    }
}
