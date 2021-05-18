using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels.Tickets
{
    public interface Ticket
    {
        int TicketID { get; set; }
        int TicketType { get; set; }
        int StatusID { get; set; }
        string Notes { get; set; }
        DateTime CreatedAt { get; set; }
        int StopNumber { get; set; }
        DateTime EstimatedArrival { get; set; }
        int? RouteID { get; set; }
        bool WasEdited { get; set; }
        Ticket OldTicketCopy { get; set; }
        void EnableCopy();
        void DisableCopy();
    }
}
