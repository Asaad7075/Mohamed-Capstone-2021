using DataAccessInterfaces;
using DataAccessLayer;
using DomainModels;
using LogicInterfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace LogicLayer
{
    public class VehicleManager : IVehicleManager
    {
        private IVehicleAccessor _vehicleAccessor = new VehicleAccessor();

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/02/10
        /// 
        /// Empty constructor for initializing the VehicleManager
        /// </summary>
        public VehicleManager()
        {
            _vehicleAccessor = new VehicleAccessor();
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/02/10
        /// 
        /// Constructor for allowing the initialization of a VehicleAccessor  with classes
        /// that inherit from IVehicle Accessor at runtime
        /// </summary>
        /// <param name="vehicleAccessor"></param>
        public VehicleManager(IVehicleAccessor vehicleAccessor) // Dependency Injection
        {
            _vehicleAccessor = vehicleAccessor;
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/02/11
        /// 
        /// Logic for sending retrieve vehicle action.
        /// </summary>
        /// <returns></returns>
        public List<Vehicle> RetrieveAllVehicles()
        {
            List<Vehicle> result = null;
            try
            {
                result = _vehicleAccessor.SelectAllVehicles();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Vehicles data could not be pulled at the moment.\n\n"
                    + ex.Message + (ex.InnerException == null ? "" : "\n\n" + ex.InnerException.Message));
            }
            return result;
        }

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/02/12
        /// 
        /// AddVehicle is the logic to add a vehicle.
        /// </summary>
        /// <param name="vehicle"></param>
        /// <returns></returns>
        public bool AddVehicle(Vehicle vehicle)
        {
            bool result = false;
            try
            {
                // vehicleAdded is a string that is the return value of InsertVehicle,
                // 0 for not added, or 1 for added.
                int vehicleAdded = _vehicleAccessor.InsertVehicle(vehicle);

                if (vehicleAdded.Equals(0))
                {
                    throw new ApplicationException("Vehicle could not be added to the vehicles DB.");
                }
                else
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Adding vehicle failed \n\n", ex);
            }
            return result;
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/03/12
        /// 
        /// Logic for retrieving all vehicle view models.
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<VehicleVM> RetrieveAllVehiclesVMs()
        {
            ObservableCollection<VehicleVM> result = null;
            try
            {
                result = _vehicleAccessor.SelectAllVehiclesVMs();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Vehicles data could not be pulled at the moment.\n\n"
                    + ex.Message + (ex.InnerException == null ? "" : "\n\n" + ex.InnerException.Message));
            }
            return result;
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/03/12
        /// </summary>
        /// <param name="vehicle"></param>
        public bool DeleteVehicleThroughVM(VehicleVM vehicle)
        {
            bool result = false;
            try
            {
                result = _vehicleAccessor.DeleteVehicleThroughVM(vehicle);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Vehicle could not be deleted.\n\n"
                    + ex.Message + (ex.InnerException == null ? "" : "\n\n" + ex.InnerException.Message));
            }

            return result;
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/03/24
        /// 
        /// Control logic for updating 
        /// </summary>
        /// <param name="vinNumber"></param>
        /// <param name="licensePlateNumber"></param>
        /// <param name="mileage"></param>
        /// <param name="bitmapImage"></param>
        public bool UpdateVehicleThroughVMByVin(string vinNumber, string licensePlateNumber, 
            string mileage)
        {
            bool result = false;
            try
            {
                result = _vehicleAccessor.UpdateVehicleThroughVMByVin(vinNumber, licensePlateNumber,
                    mileage);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Vehicle could not be updated.\n\n"
                    + ex.Message + (ex.InnerException == null ? "" : "\n\n" + ex.InnerException.Message));
            }

            return result;
        }
    }
}
