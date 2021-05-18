using DataAccessInterfaces;
using DomainModels.Tickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessFakes
{
    /// <summary>
    /// Jakub Kawski
    /// 2021/03/01
    /// 
    /// Fakes for ticketstatus accessor
    /// </summary>
    public class TicketStatusAccessorFake : ITicketStatusAccessor
    {
        private List<TicketStatus> _statuses;
        public TicketStatusAccessorFake()
        {
            _statuses = new List<TicketStatus>();
            _statuses.Add(new TicketStatus() {
                StatusID = 0,
                StatusDescription = "Completed"
            });
            _statuses.Add(new TicketStatus()
            {
                StatusID = 1,
                StatusDescription = "Awaiting Assignment"
            });
            _statuses.Add(new TicketStatus()
            {
                StatusID = 2,
                StatusDescription = "On the way"
            });
            _statuses.Add(new TicketStatus()
            {
                StatusID = 3,
                StatusDescription = "Waiting to be delivered"
            });
        }
        public List<TicketStatus> SelectAllTicketStatuses()
        {
            return _statuses;
        }
    }
}
