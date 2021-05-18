using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DomainModels
{
    /// <summary>
    /// Nate Hepker
    /// Created: 2021/03/09
    /// 
    /// RideReviewVM object will be able to use employee and client names.
    /// </summary>
    public class RideReviewVM : RideReview
    {
        public string  EmployeeFirstName { get; set; }

        [Required(ErrorMessage = "Drivers last name is required")]
        [Display(Name = "Drivers Last Name:")]
        public string EmployeeLastName { get; set; }

        [Required(ErrorMessage = "Your first name is required")]
        [Display(Name = "First Name:")]
        public string ClientFirstName { get; set; }

        [Required(ErrorMessage = "Your last name is required")]
        [Display(Name = "Last Name:")]
        public string ClientLastName { get; set; }

        public int TicketType { get; set; }
    }
}
