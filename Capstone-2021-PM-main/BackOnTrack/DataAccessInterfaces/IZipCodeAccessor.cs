using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModels;

namespace DataAccessInterfaces
{
    /// <summary>
    /// Chase Martin
    /// Created: 2021/02/12
    /// 
    /// Interface for zip code accessor class.
    /// </summary>
    public interface IZipCodeAccessor
    {
        List<ZipCodeVM> SelectZipCodesByIsServicable(bool isServicable = true);
        int InsertZipCode(ZipCodeFile zipCodeFile);
        bool InsertZipCodeBool(ZipCodeFile zipCode);
        List<ZipCodeFile> SelectAllZipCodes();
        int UpdateZipCodeFile(ZipCodeFile oldZipCode, ZipCodeFile newZipCode);
        bool UpdateZipCodeFileBool(ZipCodeFile oldZipCode, ZipCodeFile newZipCode);
        List<ZipCodeFile> SelectZipCodesByStatus(string status);
        int UpdateZipCodeStatus(int zipCodeID,
            string oldStatus, string newStatus);
    }
}
