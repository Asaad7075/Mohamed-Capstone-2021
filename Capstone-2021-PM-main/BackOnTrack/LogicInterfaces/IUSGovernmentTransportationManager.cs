using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicInterfaces
{
    /// <summary>
    /// Chantal Shirley
    /// Created: 2021/03/02
    /// 
    /// Interface for retrieiving API data from
    /// NHTSA.
    /// </summary>
    public interface IUSGovernmentTransportationManager
    {
        Task<IEnumerable<AutoDefectRecall>> RetrieveRecallsOnActiveVehicleFleet(List<Vehicle> vehicles);
    }
}