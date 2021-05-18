
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Web.Mvc;

namespace DomainModels
{
    /// <summary>
    /// Asaad Mohamed
    /// Created: 2021/03/01
    /// 
    /// Donor data storage model class for Back On Track
    ///  This information for the donor  
    /// </summary>
    /// 
   
    public class Donor
    {
        public int DonorID { get; set; } //  Primary Key
        [DefaultValue(false)]
        public bool Business { get; set; } // is a business 
        [DefaultValue(false)]
        public bool Individual { get; set; } // is an  Individual

        [StringLength(50, MinimumLength = 4, ErrorMessage = "Invalid Business Name")]
        public string BusinessName { get; set; } // the business name
        [Required(ErrorMessage = "You must enter your first name")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Invalid First Name")]
        public string FirstName { get; set; } // first name 
        [Required(ErrorMessage = "You must enter your last name")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Invalid Last Name")]
        public string LastName { get; set; } // last name
        public string MiddleInitial { get; set; } // middle name
        [Required(ErrorMessage = "You must enter your Address")]
        [StringLength(250, MinimumLength = 10, ErrorMessage = "Invalid Address")]
        public string Address { get; set; } // address of the donor
        [Required(ErrorMessage = "You must enter your zip code")]
        [MaxLength(10)]
        [MinLength(5)]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid zip code")]
        public string ZipCode { get; set; } // zip code
        [Required(ErrorMessage = "You must enter your phone number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "You enter invalid phone number")]
        public string PhoneNumber { get; set; } // phone number
        [Required(ErrorMessage = "You must enter your email address")]
        [EmailAddress(ErrorMessage = "You enter invalid email address ")]
        public string Email { get; set; } // email address

        public string SS { get; set; } // Social Security
       
        public string EIN { get; set; } // Employer Identification Number
        public bool Active { get; set; } // This if the donor is active

        public override string ToString() // this override the email to disply when create donation form
        {
            return Email;
        }
    }
}
