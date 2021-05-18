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
    /// Created: 2021/02/02
    /// 
    /// Interface for methods accessing drivers license data.
    /// </summary>
    /// <remarks>
    /// Chantal Shirley
    /// Updated: 2021/03/10
    /// </remarks>
    public interface IDriversLicenseAccessor
    {
        bool InsertDriversLicense(DriversLicense driversLicense);
        bool SelectDriversLicenseByLicenseNumber(string LicenseNumber);
        List<string> SelectDriversLicenseTypes();
    }
}
