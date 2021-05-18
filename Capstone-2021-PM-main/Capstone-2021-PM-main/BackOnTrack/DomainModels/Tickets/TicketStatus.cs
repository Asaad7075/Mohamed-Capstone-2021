/// Jakub Kawski
/// Created: 2021/02/13
/// 
/// Status for tickets
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
    public class TicketStatus
    {
        public int StatusID { get; set; }
        public string StatusDescription { get; set; }
    }
}
