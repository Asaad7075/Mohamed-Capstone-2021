using DataAccessInterfaces;
using DataAccessLayer;
using DomainModels.Tickets;
using LogicInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class TicketStatusManager : ITicketStatusManager
    {
        private ITicketStatusAccessor _ticketStatusAccessor;
        public TicketStatusManager()
        {
            _ticketStatusAccessor = new TicketStatusAccessor();
        }
        public TicketStatusManager(ITicketStatusAccessor ticketStatusAccessor)
        {
            _ticketStatusAccessor = ticketStatusAccessor;
        }
        public List<TicketStatus> RetrieveAllTicketStatuses()
        {
            List<TicketStatus> statuses = new List<TicketStatus>();
            try
            {
                statuses = _ticketStatusAccessor.SelectAllTicketStatuses();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return statuses;
        }
    }
}
