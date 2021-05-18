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
    /// Chase Martin
    /// Created: 2021/03/04
    /// 
    /// Class for accessing zip code data.
    /// </summary>
    public class FinancialCounselingAccessor : IFinancialCounselingAccessor
    {
        public List<FinancialCounseling> SelectAllCounselingTypes()
        {
            List<FinancialCounseling> types = new List<FinancialCounseling>();

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_select_all_financial_counseling_types", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var counselingTypes = new FinancialCounseling()
                        {
                            ServiceID = reader.GetInt32(0),
                            ServiceProviderID = reader.GetInt32(1),
                            ServiceName = reader.GetString(2),
                            Available = reader.GetBoolean(3),
                            ScheduleRequired = reader.GetBoolean(4),
                            ServiceDescription = reader.GetString(5)
                        };
                        types.Add(counselingTypes);
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
            return types;
        }
    }
}

