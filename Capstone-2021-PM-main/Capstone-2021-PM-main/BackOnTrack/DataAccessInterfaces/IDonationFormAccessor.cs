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
    /// Created: 2021/02/04
    /// This is interface class for accessing donor form 
    /// </summary>
    ///
    /// <remarks>
    /// Updater Name
    /// Updated: yyyy/mm/dd
    /// </remarks>
   public interface IDonationFormAccessor
    {
        /// <summary>
        /// Asaad Mohamed
        /// Created: 2021/02/04
        /// This method for select all donor form 
        /// </summary>
        /// <returns></returns>
        List<DonationForm> SelectAllDonationForm();
        /// <summary>
        /// Asaad Mohamed
        /// Created: 2021/02/04
        /// This method for update status of donation form 
        /// </summary>
        /// <param name="oldDonorForm"></param>
        /// <param name="newDonorForm"></param>
        /// <returns></returns>
        int UpdateDonorFormStatus(DonationForm oldDonorForm, DonationForm newDonorForm);

        /// <summary>
        /// Asaad Mohamed
        /// Created: 2021/02/04
        /// This method for insert dontion form 
        /// </summary>
        /// <param name="donationForm"></param>
        /// <returns></returns>
        int InsertDonationForm(DonationForm donationForm);
        /// <summary>
        ///  Asaad Mohamed
        /// Created: 2021/04/09
        /// This method for insert dontion form 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        DonationForm SelectDonationFormById(int id);

    }
}
