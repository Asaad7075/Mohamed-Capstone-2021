using DomainModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace LogicInterfaces
{
    /// <summary>
    /// Chantal Shirley
    /// Created: 2021/02/11
    /// 
    /// Interface for managing vehicles.
    /// </summary>
    public interface IVehicleManager
    {
        List<Vehicle> RetrieveAllVehicles();
        bool AddVehicle(Vehicle vehicle);
        ObservableCollection<VehicleVM> RetrieveAllVehiclesVMs();
        bool DeleteVehicleThroughVM(VehicleVM selectedValue);
        bool UpdateVehicleThroughVMByVin(string vinNumber, 
            string licensePlateNumber, string mileage);
    }
}
