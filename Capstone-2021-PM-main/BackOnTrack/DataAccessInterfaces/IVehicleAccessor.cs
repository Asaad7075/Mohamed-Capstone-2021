using DomainModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace DataAccessInterfaces
{
    /// <summary>
    /// Chantal Shirley
    /// Created: 2021/02/09
    /// 
    /// Interface for vehicle accessor class.
    /// </summary>
    public interface IVehicleAccessor
    {
        bool DeleteVehicle(Vehicle vehicle);
        List<Vehicle> SelectAllVehicles();
        int InsertVehicle(Vehicle vehicle);
        ObservableCollection<VehicleVM> SelectAllVehiclesVMs();
        bool DeleteVehicleThroughVM(VehicleVM vehicle);
        bool UpdateVehicleThroughVMByVin(string vinNumber, string licensePlateNumber, 
            string mileage);
    }
}
