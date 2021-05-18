using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels.Tickets
{
    /// <summary>
    /// Jakub Kawski
    /// 2021/12/03
    /// 
    /// Domain model for pickup tickets, 
    /// pick tickets are for donations that 
    /// need to be picked up by drivers.
    /// </summary>
    public class PickUpTicket : Ticket
    {
        public int TicketID { get; set; }
        public int TicketType { get; set; } = 2;
        public int StatusID { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public int DonationID { get; set; }
        public int? GeoID { get; set; }
        public TimeSpan TimeRangeStart { get; set; }
        public TimeSpan TimeRangeEnd { get; set; }
        public DateTime RequestDateStart { get; set; }
        public DateTime RequestDateEnd { get; set; }
        public int StopNumber { get; set; }
        public DateTime EstimatedArrival { get; set; }
        public int? RouteID { get; set; }
        public bool WasEdited { get; set; } = false;
        public Ticket OldTicketCopy { get; set; }
        public virtual void EnableCopy()
        {
            if (WasEdited)
            {
                return;
            }
            WasEdited = true;
        }
        public void DisableCopy()
        {
            if (WasEdited)
            {
                OldTicketCopy = null;
                WasEdited = false;
            }
        }
    }
}
