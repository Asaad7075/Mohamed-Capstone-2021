using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels.Tickets
{
    /// <summary>
    /// Jakub Kawski
    /// Created: 2021/03/
    /// Nate Hepker
    /// Collaborated: 2021/03/26
    /// 
    /// RideReviewVM object will be used for clients to request rides
    /// </summary>
    public class RideTicket : Ticket
    {
        public int TicketID { get; set; }
        public int TicketType { get; set; } = 3;
        public int StatusID { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        [Required]
        [Display(Name = "Client ID:")]
        public int ClientID { get; set; }
        public bool RequiresHAV { get; set; }
        public int FetchGeoID { get; set; }
        public int DestinationGeoID { get; set; }
        public TimeSpan TimeRangeStart { get; set; }
        public TimeSpan TimeRangeEnd { get; set; }
        public DateTime DateOfRide { get; set; }
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
