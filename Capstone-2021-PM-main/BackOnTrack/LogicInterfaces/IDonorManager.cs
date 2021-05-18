using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicInterfaces
{
    /// <summary>
    /// Asaad Mohamed
    /// 2021/03/01
    /// This is an interface class to retrive  all donor list by active
    /// </summary>
    public interface IDonorManager
    {
        /// <summary>
        /// Asaad Mohamed
        /// 2021/03/01
        /// This method to retrive all donor
        /// </summary>
        /// <param name="active"></param>
        /// <returns></returns>
        List<Donor> RetrieveAllDonorListByActive(bool active = true);
        bool InsertDonor(Donor donor);
        bool EditDonor(Donor oldDonor, Donor newDonor);
        
        bool DeactivateDonor(int donorID);
        Donor GetDonorById(int id);
        bool SelectDonorByDonorEmail(string email);
        bool AuthenticateUser(string email, string password);
        bool InsertDonorFromWeb(Donor donor, string password);
    }
   
}
