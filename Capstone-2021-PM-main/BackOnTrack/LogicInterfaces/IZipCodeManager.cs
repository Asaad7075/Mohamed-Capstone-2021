using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModels;

namespace LogicInterfaces
{
    /// <summary>
    /// Chase Martin
    /// Created: 2021/02/12
    /// 
    /// Interface for managing zip code objects.
    /// </summary>
    public interface IZipCodeManager
    {
        List<ZipCodeVM> RetrieveZipCodesByIsServicable(bool isServicable = true);
        bool AddZipCode(ZipCodeFile zipCode);

        List<ZipCodeFile> RetrieveAllZipCodes();
        bool EditZipCodeFile(ZipCodeFile oldZipCode,
                               ZipCodeFile newZipCode);
        List<ZipCodeFile> RetrieveZipCodesByStatus(string status);
        bool EditZipCodeStatus(int zipCodeID,
            string oldStatus, string newStatus);
    }
}
