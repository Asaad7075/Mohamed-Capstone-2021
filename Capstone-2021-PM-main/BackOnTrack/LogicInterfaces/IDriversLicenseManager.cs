using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicInterfaces
{
    /// <summary>
    /// Chantal Shirley
    /// Created: 2021/02/02
    /// 
    /// Interface for managing Drivers License objects.
    /// </summary>
    /// <remarks>
    /// Chantal Shirley
    /// Updated: 2021/03/10
    /// </remarks>
    public interface IDriversLicenseManager
    {
        bool AddDriversLicense(DriversLicense driversLicense);
        bool RetrieveDriversLicenseByLicenseNumber(string licenseNumber);
        List<string> RetreiveDriversLicenseTypes();
    }
}