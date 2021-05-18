using DataAccessInterfaces;
using DomainModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace DataAccessFakes
{
    /// <summary>
    /// Chantal Shirley
    /// Created: 2021/02/09
    /// 
    /// Fake vehicle test data.
    /// </summary>
    public class VehicleFake : IVehicleAccessor
    {
        private List<Vehicle> _vehicles = new List<Vehicle>();
        private ObservableCollection<VehicleVM> _vehicleVMs = new ObservableCollection<VehicleVM>();

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/02/10
        /// 
        /// Empty constructor that initializes fake vehicle data.
        /// </summary>
        public VehicleFake()
        {
            _vehicles.Add(new Vehicle
            {
                VinNumber = "2Z1UK8MES769P4MQM",
                LicensePlateNumber = "3ND893",
                VehicleMake = "Dodge",
                VehicleModel = "Stratus",
                VehicleYear = 2010,
                LicenseClass = "C",
                ADACompliant = false,
                IsActive = true,
                GeoID = null
            });

            _vehicles.Add(new Vehicle
            {
                VinNumber = "B90K1EQFB69RWHF4M",
                LicensePlateNumber = "492DF2",
                VehicleMake = "Honda",
                VehicleModel = "Civic",
                VehicleYear = 2017,
                LicenseClass = "C",
                ADACompliant = false,
                IsActive = true,
                GeoID = null
            });

            _vehicles.Add(new Vehicle
            {
                VinNumber = "9XV1BJ4XRUS9HFZL6",
                LicensePlateNumber = "930242",
                VehicleMake = "Mercedes-Benz",
                VehicleModel = "Sprinter",
                VehicleYear = 2020,
                LicenseClass = "C",
                ADACompliant = true,
                IsActive = true,
                GeoID = null
            });

            _vehicles.Add(new Vehicle
            {
                VinNumber = "5SX43TNWCO0Q4Y2N6",
                LicensePlateNumber = "3841JD",
                VehicleMake = "Mercedes-Benz",
                VehicleModel = "Sprinter",
                VehicleYear = 2021,
                LicenseClass = "C",
                ADACompliant = true,
                IsActive = true,
                GeoID = null
            });

            _vehicles.Add(new Vehicle
            {
                VinNumber = "0D537YGQFKPFM4JOO",
                LicensePlateNumber = "DI3942",
                VehicleMake = "Peterbilt",
                VehicleModel = "Model 579",
                VehicleYear = 2021,
                LicenseClass = "A",
                ADACompliant = false,
                IsActive = true,
                GeoID = null
            });

            _vehicleVMs.Add(new VehicleVM
            {
                VehicleImage = null,
                VinNumber = "0D537YGQFKPFM4JOO",
                LicensePlateNumber = "DI3942",
                VehicleMake = "Peterbilt",
                VehicleModel = "Model 579",
                VehicleYear = 2021,
                LicenseClass = "A",
                ADACompliant = false,
                IsActive = true,
                GeoID = null
            });

            _vehicleVMs.Add(new VehicleVM
            {
                VehicleImage = null,
                VinNumber = "5SX43TNWCO0Q4Y2N6",
                LicensePlateNumber = "3841JD",
                VehicleMake = "Mercedes-Benz",
                VehicleModel = "Sprinter",
                VehicleYear = 2021,
                LicenseClass = "C",
                ADACompliant = true,
                IsActive = true,
                GeoID = null
            });

        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/02/10
        /// 
        /// Method for deleting/"archiving" vehicle.
        /// </summary>
        /// <param name="vehicle"></param>
        /// <returns></returns>
        public bool DeleteVehicle(Vehicle vehicle)
        {
            bool result = false;

            // Logic to throw error for removal of vehicle that does not exist
            if (_vehicles.Contains(vehicle))
            {
                _vehicles.Remove(vehicle);
                result = true;
            }
            else
            {
                throw new ApplicationException("Requested vehicle does not exist.");
            }

            return result;
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/02/11
        /// 
        /// Method for returning all fake vehicles.
        /// </summary>
        /// <returns></returns>
        public List<Vehicle> SelectAllVehicles()
        {
            return _vehicles;
        }

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/02/12
        /// 
        /// Method for inserting fake vehicles.
        /// </summary>
        /// <param name="vehicle"></param>
        /// <returns></returns>
        public int InsertVehicle(Vehicle vehicle)
        {
            int result = 0;

            if (_vehicles.Contains(vehicle))
            {
                throw new Exception(vehicle.VehicleYear + " " + vehicle.VehicleMake + " " +
                    vehicle.VehicleModel + " \n" + vehicle.VinNumber + " "
                    + " Already exists in the database");
            }
            else
            {
                _vehicles.Add(vehicle);

                foreach (Vehicle currVehicle in _vehicles)
                {
                    if (currVehicle.Equals(vehicle))
                    {
                        result = 1;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 03/12/21
        /// </summary>
        /// <returns></returns>
        ObservableCollection<VehicleVM> IVehicleAccessor.SelectAllVehiclesVMs()
        {
            return _vehicleVMs;
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/03/12
        /// </summary>
        /// <param name="vehicle"></param>
        /// <returns></returns>
        public bool DeleteVehicleThroughVM(VehicleVM vehicle)
        {
            if (_vehicleVMs.Contains(vehicle))
            {
                _vehicleVMs.Remove(vehicle);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/03/24
        /// </summary>
        /// <param name="vinNumber"></param>
        /// <param name="licensePlateNumber"></param>
        /// <param name="mileage"></param>
        /// <returns></returns>
        public bool UpdateVehicleThroughVMByVin(string vinNumber, string licensePlateNumber, string mileage)
        {
            foreach (VehicleVM vehicle in _vehicleVMs)
            {
                if (vehicle.VinNumber.Equals(vinNumber))
                {
                    int convMileage = Int32.Parse(mileage);
                    vehicle.LicensePlateNumber = licensePlateNumber;
                    vehicle.Mileage = convMileage;
                    return true;
                }
            }

            return false;
        }

    }
}
