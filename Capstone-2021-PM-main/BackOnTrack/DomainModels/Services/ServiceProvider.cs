using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels.Services
{
    /// <summary>
    /// Chase Martin
    /// Created: 2021/03/24
    /// 
    /// Model class for all Service providers
    /// </summary>
    public class ServiceProvider
    {
        public int ServiceProviderID { get; set; }
        //public string ServiceCategory { get; set; }
        public string BusinessName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public char MiddleInitial { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string ZipCode { get; set; }
        public string EIN { get; set; }
        //public int ServiceID { get; set; }
        public bool Activated { get; set; }
        public bool Schedule { get; set; }
    }
}
