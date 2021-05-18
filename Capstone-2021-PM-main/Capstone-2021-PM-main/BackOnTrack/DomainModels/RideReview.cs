using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels
{
    /// <summary>
    /// Nate Hepker
    /// Created: 2021/03/07
    /// 
    /// RideReview object to be used for all ride reviews.
    /// </summary>
    public class RideReview
    {
        public int RideReviewID { get; set; }

        [Required(ErrorMessage = "Ticket ID is blank.")]
        [Display(Name = "Ticket ID:")]
        public int TicketID { get; set; }

        public int ClientID { get; set; }

        [Required(ErrorMessage = "Please enter a rating")]
        [Range(1, 5)]
        [Display(Name = "Rating:")]
        public int ClientDriverRating { get; set; }

        [Required(ErrorMessage = "Please enter a comment")]
        [StringLength(500)]
        [Display(Name = "Comment:")]
        public string ClientComment { get; set; }

        public int DriverID { get; set; }

        public int DriverClientRating { get; set; }

        public string DriverComment { get; set; }
    }
}
