using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels.Services
{
    /// <summary>
    /// Chase Martin
    /// Created: 2021/03/19
    /// 
    /// Model class for all Services
    /// </summary>
    public class ServiceReview
    {
        public int ServiceReviewID { get; set; }

        [Required(ErrorMessage = "Please enter the Service's name.")]
        [Display(Name = "Service Name:")]
        public string ServiceName { get; set; }

        [Required(ErrorMessage = "Please enter the Service Provider's first name")]
        [Display(Name = "Provider First Name:")]
        public string ProviderFirstName { get; set; }

        [Required(ErrorMessage = "Please enter the Service Provider's last name")]
        [Display(Name = "Provider Last Name:")]
        public string ProviderLastName { get; set; }

        [Required(ErrorMessage = "Please enter a rating")]
        [Display(Name = "Rating:")]
        public string Rating { get; set; }

        [Required(ErrorMessage = "Please enter a comment")]
        [StringLength(500)]
        [Display(Name = "Comment:")]
        public string ClientComment { get; set; }
    }
}
