using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels
{
    /// <summary>
    /// Chantal Shirley
    /// Created: 2021/04/16
    /// 
    /// Class that is representative
    /// of a client of the Back-On-Track
    /// system.
    /// </summary>
    public class Client
    {
        public int ClientID { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
