using DataAccessInterfaces;
using DataAccessLayer;
using DomainModels;
using LogicInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    /// <summary>
    /// Chantal Shirley
    /// Created: 2021/03/02
    /// 
    /// Class for retrieiving API data from
    /// NHTSA Accessor.
    /// </summary>
    public class USGovernmentTransportationManager : IUSGovernmentTransportationManager
    {
        private IAutoDefectRecallAccessor _autoDefectRecallAccessor;

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/03/03
        /// 
        /// Zero-arguent constructor for the Manager.
        /// </summary>
        public USGovernmentTransportationManager()
        {
            _autoDefectRecallAccessor = new AutoDefectRecallAccessor();
        }

        /// <summary>
        /// Chantal Shirley
        /// Created:2021/03/03
        /// 
        /// Single argument constructor for dependency injection and testing.
        /// </summary>
        /// <param name="autoDefectRecallAccessor"></param>
        public USGovernmentTransportationManager(IAutoDefectRecallAccessor autoDefectRecallAccessor)
        {
            _autoDefectRecallAccessor = autoDefectRecallAccessor;
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/03/04
        /// 
        /// Retrieves recall data on active vehicle fleet.
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<AutoDefectRecall>> RetrieveRecallsOnActiveVehicleFleet(List<Vehicle> vehicles)
        {
            Task<IEnumerable<AutoDefectRecall>> results;

            try
            {
                // Get raw results
                results = _autoDefectRecallAccessor.RetrieveAutoDefectRecallAsync(vehicles);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Vehicle recall data could not be pulled at the moment.\n\n"
                    + ex.Message + (ex.InnerException == null ? "" : "\n\n" + ex.InnerException));
            }
            return results;
        }

    }
}
