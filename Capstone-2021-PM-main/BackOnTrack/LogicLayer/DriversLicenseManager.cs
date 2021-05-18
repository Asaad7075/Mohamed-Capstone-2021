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
    /// Chantal Shriley
    /// Created: 2021/02/03
    /// 
    /// Control for directing the flow of drivers license data.
    /// </summary>
    public class DriversLicenseManager : IDriversLicenseManager
    {
        private IDriversLicenseAccessor _driversLicenseAccessor;

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/02/03
        /// 
        /// Empty constructor for initializing the DriversLicenseManager
        /// </summary>
        public DriversLicenseManager()
        {
            _driversLicenseAccessor = new DriversLicenseAccessor();
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/02/03
        /// 
        /// Constructor for allowing the initialization of a DriversLicenseManager with classes
        /// that inherit from IDriversLicenseAccessor at runtime
        /// </summary>
        public DriversLicenseManager(IDriversLicenseAccessor driversLicenseAccessor) // Dependency Inversion
        {
            _driversLicenseAccessor = driversLicenseAccessor;
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/02/03
        /// 
        /// Logic for allowing for the creation of a drivers license to be stored in the DriversLicense table
        /// </summary>
        /// <param name="driversLicense"></param>
        /// <returns></returns>
        public bool AddDriversLicense(DriversLicense driversLicense)
        {
            bool result = false;

            try
            {
                result = _driversLicenseAccessor.InsertDriversLicense(driversLicense);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Insertion of drivers " +
                    "license information failed." + "\n\n" + ex.Message +
                    (ex.InnerException == null ? "" : "\n\n" + ex.InnerException));
            }

            return result;
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/03/10
        /// 
        /// Logic for retrieving drivers license
        /// types information.
        /// </summary>
        /// <returns></returns>
        public List<string> RetreiveDriversLicenseTypes()
        {
            List<string> licenseTypes = new List<string>();

            try
            {
                licenseTypes = _driversLicenseAccessor.SelectDriversLicenseTypes();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Drivers license types data could not be pulled at the moment.\n\n"
                    + ex.Message + (ex.InnerException == null ? "" : "\n\n" + ex.InnerException));
            }

            return licenseTypes;
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/02/14
        /// 
        /// Logic for retrieving drivers license information.
        /// </summary>
        /// <param name="licenseNumber"></param>
        /// <returns></returns>
        public bool RetrieveDriversLicenseByLicenseNumber(string licenseNumber)
        {
            bool result = false;

            try
            {
                result = _driversLicenseAccessor.SelectDriversLicenseByLicenseNumber(licenseNumber);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Drivers License data could not be pulled at the moment.\n\n"
                    + ex.Message + (ex.InnerException == null ? "" : "\n\n" + ex.InnerException));
            }

            return result;
        }
    }
}
