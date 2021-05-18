using DataAccessInterfaces;
using DomainModels.Tickets;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class TicketStatusAccessor : ITicketStatusAccessor
    {
        public List<TicketStatus> SelectAllTicketStatuses()
        {
            List<TicketStatus> statuses = new List<TicketStatus>();

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_select_all_ticket_statuses", conn);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        TicketStatus status = new TicketStatus()
                        {
                            StatusID = reader.GetInt32(0),
                            StatusDescription = reader.GetString(1)
                        };
                        statuses.Add(status);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Could not get ticket statuses from database", ex);
            }
            finally
            {
                conn.Close();
            }
            return statuses;
        }
    }
}
