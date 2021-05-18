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
    /// Zach Stultz
    /// Created: 2021/03/19
    ///
    /// The vehicle maintenance status manager.
    /// </summary>
    public class VehicleMaintenanceStatusManager : IVehicleMaintenanceStatusManager
    {
        private IVehicleMaintenanceStatusAccessor _vehicleMaintenanceStatusAccessor;

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/19
        ///
        /// Default Constructor
        /// </summary>
        public VehicleMaintenanceStatusManager()
        {
            _vehicleMaintenanceStatusAccessor = new VehicleMaintenanceStatusAccessor();
        }


        // <summary>
        // Zach Stultz
        // Created: 2021/03/19
        //
        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleMaintenanceStatusManager"/> class.
        /// </summary>
        /// <param name="vehicleMaintenanceStatusAccessor">The vehicle maintenance status accessor.</param>
        public VehicleMaintenanceStatusManager(IVehicleMaintenanceStatusAccessor vehicleMaintenanceStatusAccessor)
        {
            _vehicleMaintenanceStatusAccessor = vehicleMaintenanceStatusAccessor;
        }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/19
        ///
        /// Adds the vehicle maintenance status.
        /// </summary>
        /// <param name="vehicleMaintenanceStatus">The vehicle maintenance status.</param>
        /// <returns>An int.</returns>
        public bool AddVehicleMaintenanceStatus(VehicleMaintenanceStatus vehicleMaintenanceStatus)
        {
            bool result = false;
            bool duplicate = false;
            List<VehicleMaintenanceStatus> data = _vehicleMaintenanceStatusAccessor.SelectAllVehicleMaintenanceStatuses() ;

            foreach (VehicleMaintenanceStatus item in data)
            {
                if(vehicleMaintenanceStatus.StatusID == item.StatusID)
                {
                    duplicate = true;
                }
            }
            if (!duplicate)
            {
                try
                {
                    result = _vehicleMaintenanceStatusAccessor.InsertVehicleMaintenanceStatus(vehicleMaintenanceStatus);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            } else
            {
                result = false;
                throw new Exception("Vehicle Maintenance Status already exists in the database.");
            }
            return result;
        }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/19
        ///
        /// Adds the vehicle maintenance status type.
        /// </summary>
        /// <param name="vehicleMaintenanceStatusType">The vehicle maintenance status type.</param>
        /// <returns>An int.</returns>
        public bool AddVehicleMaintenanceStatusType(VehicleMaintenanceStatusType vehicleMaintenanceStatusType)
        {
            bool result = false;
            bool duplicate = false;
            List<VehicleMaintenanceStatusType> data = _vehicleMaintenanceStatusAccessor.SelectAllVehicleMaintenanceStatusTypes();

            foreach (VehicleMaintenanceStatusType item in data)
            {
                if (vehicleMaintenanceStatusType.MaintenanceStatusType == item.MaintenanceStatusType)
                {
                    duplicate = true;
                }
            }
            if (!duplicate)
            {
                try
                {
                    result = _vehicleMaintenanceStatusAccessor.InsertVehicleMaintenanceStatusType(vehicleMaintenanceStatusType);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                result = false;
                throw new Exception("Vehicle Maintenance Status already exists in the database.");
            }
            return result;
        }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/25
        ///
        /// Retrieves the all vehicle maintenance statuses.
        /// </summary>
        /// <returns>A list of VehicleMaintenanceStatuses.</returns>
        public List<VehicleMaintenanceStatus> RetrieveAllVehicleMaintenanceStatuses()
        {
            List<VehicleMaintenanceStatus> data = null;

            try
            {
                data = _vehicleMaintenanceStatusAccessor.SelectAllVehicleMaintenanceStatuses();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not available.", ex);
            }
            return data;
        }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/25
        ///
        /// Retrieves the all vehicle maintenance status types.
        /// </summary>
        /// <returns>A list of VehicleMaintenanceStatusTypes.</returns>
        public List<VehicleMaintenanceStatusType> RetrieveAllVehicleMaintenanceStatusTypes()
        {
            List<VehicleMaintenanceStatusType> data = null;

            try
            {
                data = _vehicleMaintenanceStatusAccessor.SelectAllVehicleMaintenanceStatusTypes();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not available.", ex);
            }
            return data;
        }
    }
}
