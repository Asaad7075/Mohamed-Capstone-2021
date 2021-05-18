/// Jakub Kawski
/// Created: 2021/02/13
/// 
/// Delivery tickets are used for items that clients request 
/// to be deliveried. 
/// </summary>
///
/// <remarks>
/// Updater Name
/// Updated: yyyy/mm/dd
/// </remarks>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels.Tickets
{
    public class DeliveryTicket : Ticket
    {
        public int TicketID { get; set; }
        public int TicketType { get; set; } = 1;
        public int StatusID { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public int OrderID { get; set; }
        public int GeoID { get; set; }
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
