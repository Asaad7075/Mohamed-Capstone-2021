﻿using DomainModels.Tickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicInterfaces
{
    /// <summary>
    /// Jakub Kawski
    /// 2021/03/01
    /// </summary>
    public interface ITicketStatusManager
    {
        List<TicketStatus> RetrieveAllTicketStatuses();
    }
}
