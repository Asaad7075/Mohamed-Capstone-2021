using DataAccessInterfaces;
using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessFakes
{
    /// <summary>
    /// Zach Stultz
    /// Created: 2021/03/19
    ///
    /// Our Fakes Class for testing.
    /// </summary>
    public class VehicleMaintenanceStatusFake : IVehicleMaintenanceStatusAccessor
    {
        private List<VehicleMaintenanceStatus> _vehicleMaintenanceStatuses;
        private List<VehicleMaintenanceStatusType> _vehicleMaintenanceStatusTypes;


        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/19
        ///
        /// Initializes a new instance of the <see cref="VehicleMaintenanceStatusFake"/> class
        /// and creates some objects and adds them to a list.
        /// </summary>
        public VehicleMaintenanceStatusFake()
        {
            _vehicleMaintenanceStatuses = new List<VehicleMaintenanceStatus>();
            _vehicleMaintenanceStatusTypes = new List<VehicleMaintenanceStatusType>();

            _vehicleMaintenanceStatuses.Add(new VehicleMaintenanceStatus()
            {
                StatusID = 100000,
                VinNumber = "1FTFX1CF8DKE26122",
                MaintenanceStatusType = "Active",
                HasMaintenanceReport = true,
                IdentifiedMaintenance = "Tires need replacing"
            });
            _vehicleMaintenanceStatuses.Add(new VehicleMaintenanceStatus()
            {
                StatusID = 100001,
                VinNumber = "JM1CW2BLXC0108466",
                MaintenanceStatusType = "Paused",
                HasMaintenanceReport = false,
                IdentifiedMaintenance = "Engine"
            });
            _vehicleMaintenanceStatuses.Add(new VehicleMaintenanceStatus()
            {
                StatusID = 100002,
                VinNumber = "5N1AR1NBXBC649344",
                MaintenanceStatusType = "Complete",
                HasMaintenanceReport = true,
                IdentifiedMaintenance = "Oil"
            });
            _vehicleMaintenanceStatuses.Add(new VehicleMaintenanceStatus()
            {
                StatusID = 100003,
                VinNumber = "2T1BURHE5FC358890",
                MaintenanceStatusType = "Rejected",
                HasMaintenanceReport = false,
                IdentifiedMaintenance = "Tread"
            });
            _vehicleMaintenanceStatusTypes.Add(new VehicleMaintenanceStatusType()
            {
                MaintenanceStatusType = "Active", // a duplicate that already exists in the DB.
                MaintenanceStatusDescription = "An active maintenance."
            });
            _vehicleMaintenanceStatusTypes.Add(new VehicleMaintenanceStatusType()
            {
                MaintenanceStatusType = "Complete", // a duplicate that already exists in the DB.
                MaintenanceStatusDescription = "A complete maintenance."
            });
            _vehicleMaintenanceStatusTypes.Add(new VehicleMaintenanceStatusType()
            {
                MaintenanceStatusType = "Deferred", // a duplicate that already exists in the DB.
                MaintenanceStatusDescription = "A postponed maintenance."
            });
            _vehicleMaintenanceStatusTypes.Add(new VehicleMaintenanceStatusType()
            {
                MaintenanceStatusType = "Merged", // a duplicate that already exists in the DB.
                MaintenanceStatusDescription = "A combined maintenance."
            });
            _vehicleMaintenanceStatusTypes.Add(new VehicleMaintenanceStatusType()
            {
                MaintenanceStatusType = "New", // a duplicate that already exists in the DB.
                MaintenanceStatusDescription = "A new maintenance."
            });
            _vehicleMaintenanceStatusTypes.Add(new VehicleMaintenanceStatusType()
            {
                MaintenanceStatusType = "Paused", // a duplicate that already exists in the DB.
                MaintenanceStatusDescription = "A paused maintenance."
            });
            _vehicleMaintenanceStatusTypes.Add(new VehicleMaintenanceStatusType()
            {
                MaintenanceStatusType = "Rejected", // a duplicate that already exists in the DB.
                MaintenanceStatusDescription = "A rejected maintenance."
            });
            _vehicleMaintenanceStatusTypes.Add(new VehicleMaintenanceStatusType()
            {
                MaintenanceStatusType = "Rework", // a duplicate that already exists in the DB.
                MaintenanceStatusDescription = "A maintenance that needs to be reworked."
            });
            _vehicleMaintenanceStatusTypes.Add(new VehicleMaintenanceStatusType()
            {
                MaintenanceStatusType = "Verified", // a duplicate that already exists in the DB.
                MaintenanceStatusDescription = "A verified maintenance."
            });
        }



        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/19
        ///
        /// Inserts the vehicle maintenance status.
        /// </summary>
        /// <param name="vinNumber">The Vin number.</param>
        /// <param name="maintenanceStatusType">The maintenance status type.</param>
        /// <param name="hasMaintenanceReport">If true, has maintenance report.</param>
        /// <param name="identifiedMaintenance">The identified maintenance.</param>
        /// <returns>An int.</returns>
        public bool InsertVehicleMaintenanceStatus(VehicleMaintenanceStatus vehicleMaintenanceStatus)
        {
            bool result = false;
            bool duplicate = false;

            for (int i = 0; i < _vehicleMaintenanceStatuses.Count; i++)
            {
                if (
                    vehicleMaintenanceStatus.StatusID == _vehicleMaintenanceStatuses[i].StatusID)
                {
                    duplicate = true;
                }
            }
            if (duplicate.Equals(true))
            {
                throw new Exception("Vehicle Maintenance Report already exists in the database.");
            }
            else
            {
                _vehicleMaintenanceStatuses.Add(vehicleMaintenanceStatus);
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/19
        ///
        /// Inserts the vehicle maintenance status type.
        /// </summary>
        /// <param name="maintenanceStatusType">The maintenance status type.</param>
        /// <param name="maintenanceStatusTypeDescription">The maintenance status type description.</param>
        /// <returns>An int.</returns>
        public bool InsertVehicleMaintenanceStatusType(VehicleMaintenanceStatusType vehicleMaintenanceStatusType)
        {
            bool result = false;
            bool duplicate = false;

            for (int i = 0; i < _vehicleMaintenanceStatusTypes.Count; i++)
            {
                if (
                    vehicleMaintenanceStatusType.MaintenanceStatusType == _vehicleMaintenanceStatusTypes[i].MaintenanceStatusType)
                {
                    duplicate = true;
                }
            }
            if (duplicate.Equals(true))
            {
                throw new Exception("Vehicle Maintenance Report already exists in the database.");
            }
            else
            {
                _vehicleMaintenanceStatusTypes.Add(vehicleMaintenanceStatusType);
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/25
        ///
        /// Selects the all vehicle maintenance statuses.
        /// </summary>
        /// <returns>A list of VehicleMaintenanceStatuses.</returns>
        public List<VehicleMaintenanceStatus> SelectAllVehicleMaintenanceStatuses()
        {
            return _vehicleMaintenanceStatuses;
        }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/25
        ///
        /// Selects the all vehicle maintenance status types.
        /// </summary>
        /// <returns>A list of VehicleMaintenanceStatusTypes.</returns>
        public List<VehicleMaintenanceStatusType> SelectAllVehicleMaintenanceStatusTypes()
        {
            return _vehicleMaintenanceStatusTypes;
        }
    }
}
