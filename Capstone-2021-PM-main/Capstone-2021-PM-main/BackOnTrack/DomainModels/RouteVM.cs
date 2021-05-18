using DomainModels.Tickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels
{
    /// <summary>
    /// Zach Stultz
    /// Created: 2021/02/24
    ///
    /// The View Model for the Create a Route Interface.
    /// </summary>
    public class RouteVM : Route
    {
        public string DriverName { get; set; }
        public Vehicle Vehicle { get; set; }
        public List<Ticket> Tickets { get; set; }
        public Tickets.Ticket Ticket { get; set; }
    }
}
