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
    /// Chantal Shirley
    /// Created: 2021/02/02
    /// 
    /// Fake drivers license test data.
    /// </summary>
    public class DriversLicenseFake : IDriversLicenseAccessor
    {
        private List<DriversLicense> _driversLicenses = new List<DriversLicense>();
        private List<string> _licenseTypes = new List<string>()
        {
            "A",
            "B",
            "C",
            "M"
        };

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/02/02
        /// 
        /// Generation of fake drivers license data for test cases.
        /// </summary>
        public DriversLicenseFake()
        {
            _driversLicenses.Add(new DriversLicense
            {
                LicenseNumber = "IA834343424",
                EmployeeID = 34234322,
                LicenseType = "C",
                LicenseIssuedDate = new DateTime(2018, 01, 15),
                LicenseExpiryDate = new DateTime(2025, 02, 13),
                IsActive = true
            });

            _driversLicenses.Add(new DriversLicense
            {
                LicenseNumber = "IA305894358",
                EmployeeID = 34234323,
                LicenseType = "A",
                LicenseIssuedDate = new DateTime(2015, 05, 13),
                LicenseExpiryDate = new DateTime(2022, 11, 04),
                IsActive = true
            });

            _driversLicenses.Add(new DriversLicense
            {
                LicenseNumber = "IA305894353",
                EmployeeID = 34234323,
                LicenseType = "C",
                LicenseIssuedDate = new DateTime(2020, 01, 20),
                LicenseExpiryDate = new DateTime(2026, 12, 12),
                IsActive = true
            });

            _driversLicenses.Add(new DriversLicense
            {
                LicenseNumber = "IA305895673",
                EmployeeID = 34534514,
                LicenseType = "C",
                LicenseIssuedDate = new DateTime(2021, 01, 06),
                LicenseExpiryDate = new DateTime(2029, 12, 12),
                IsActive = true
            });

            _driversLicenses.Add(new DriversLicense
            {
                LicenseNumber = "IA305892342",
                EmployeeID = 34536848,
                LicenseType = "A",
                LicenseIssuedDate = new DateTime(2019, 07, 08),
                LicenseExpiryDate = new DateTime(2027, 04, 18),
                IsActive = true
            });
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/02/02
        /// 
        /// Faux db insert method.
        /// </summary>
        /// <param name="driversLicense"></param>
        /// <returns></returns>
        public bool InsertDriversLicense(DriversLicense driversLicense)
        {
            bool result = false;

            if (_driversLicenses.Contains(driversLicense))
            {
                throw new Exception("Drivers license already exists in the database.");
            }
            else
            {
                _driversLicenses.Add(driversLicense);


                foreach (DriversLicense currLicense in _driversLicenses)
                {
                    if (currLicense.Equals(driversLicense))
                    {
                        result = true;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/02/19
        /// 
        /// Initial creation for the shell, will need to be tested eventually.
        /// </summary>
        /// <param name="LicenseNumber"></param>
        /// <returns></returns>
        public bool SelectDriversLicenseByLicenseNumber(string LicenseNumber)
        {
            foreach (DriversLicense driversLicense in _driversLicenses)
            {
                if (driversLicense.LicenseNumber.Equals(LicenseNumber))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/02/19
        /// 
        /// </summary>
        /// <returns></returns>
        public List<string> SelectDriversLicenseTypes()
        {
            return _licenseTypes;
        }
    }
}
