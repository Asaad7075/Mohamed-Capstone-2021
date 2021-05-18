using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    /// <summary>
    /// Asaad Mohamed
    /// Created: 2021/03/01
    /// This is interface class for accessing donor information 
    /// </summary>
    ///
    /// <remarks>
    /// Updater Name
    /// Updated: yyyy/mm/dd
    /// </remarks>
    public interface IDonorAccessor
    {
        /// <summary>
        /// Asaad Mohamed
        /// Created: 2021/03/01
        /// This method for select all donor by active 
        /// </summary>
        /// <returns></returns>
        /// 
        List<Donor> SelectDonorByActive(bool active = true);

        int AddDonor(Donor donor);

        int UpdateDonor(Donor oldDonor, Donor newDonor);
        int DeleteDonor(int donorID);
        Donor SelectDonorById(int id);
        bool SelectDonorByEmail(string email);
        bool SelectDonorByEmailAndPassword(string email, string password);
        bool InsertDonorFromWeb(Donor donor, string password);
    }
}
