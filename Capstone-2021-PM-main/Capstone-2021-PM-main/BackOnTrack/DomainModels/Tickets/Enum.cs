/// Jakub Kawski
/// Created: 2021/02/13
/// 
/// The tickettype enum is use for logic ticket manager to identify tickets
/// in human readable language.
/// </summary>
///
/// <remarks>
/// Updater Name
/// Updated: yyyy/mm/dd
/// </remarks>
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels.Tickets
{
    
    public enum TicketType
    {
        Delivery = 1,
        PickUp = 2,
        Ride = 3
    }
}
