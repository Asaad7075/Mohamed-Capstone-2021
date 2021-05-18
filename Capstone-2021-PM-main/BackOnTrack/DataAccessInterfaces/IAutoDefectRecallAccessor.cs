using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    /// <summary>
    /// Chantal Shirley
    /// Created: 2021/03/02
    /// 
    /// Interface for accessing NHTSA auto defect
    /// recall information.
    /// </summary>
    public interface IAutoDefectRecallAccessor
    {
        Task<IEnumerable<AutoDefectRecall>> RetrieveAutoDefectRecallAsync(List<Vehicle> vehicles);
    }
}
