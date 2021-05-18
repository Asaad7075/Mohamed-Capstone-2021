using DomainModels.Tickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    /// <summary>
    /// Interface for Ticket Status Accessor
    /// </summary>
    public interface ITicketStatusAccessor
    {
        List<TicketStatus> SelectAllTicketStatuses();
    }
}
