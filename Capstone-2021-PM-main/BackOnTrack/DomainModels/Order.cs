using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels
{
    /// <summary>
    /// Chantal Shirley
    /// Created: 2021/04/20
    /// 
    /// Represents orders
    /// made by clients.
    /// </summary>
    public class Order
    {
        public int OrderID { get; set; }
        public Dictionary<int, Donation> Items { get; set; } // int is representative of the donation id
        public string DateRequested { get; set; }
        public int? DonationID { get; set; }
        public int ClientID { get; set; }

        public Order()
        {

        }
    }
}
