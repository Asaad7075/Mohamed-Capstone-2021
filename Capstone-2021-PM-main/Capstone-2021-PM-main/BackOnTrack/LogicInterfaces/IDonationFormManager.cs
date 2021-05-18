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
    /// 2021/02/04
    /// This is an interface class to retrive and edite donation form
    /// </summary>
    public interface IDonationFormManager
    {
        /// <summary>
        /// Asaad Mohamed
        /// 2021/02/04
        /// This method to retrive all donation form
        /// </summary>
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// </remarks>
        /// <returns></returns>
        List<DonationForm> RetrieveAllDonorFormList();
        /// <summary>
        /// Asaad Mohamed
        /// 2021/02/04
        /// This method to edit the status of donation form
        /// </summary>
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// </remarks>
        /// <param name="oldDonorForm"></param>
        /// <param name="newDonorForm"></param>
        /// <returns></returns>
        bool EditDonorForm(DonationForm oldDonorForm, DonationForm newDonorForm);

        /// <summary>
        /// Asaad Mohamed
        /// 2021/02/04
        /// This method to add new donation form
        /// </summary>
        /// <param name="donationForm"></param>
        /// <returns></returns>
        bool InsertDonationForm(DonationForm donationForm);

        /// <summary>
        /// Asaad Mohamed
        /// 2021/04/09
        /// This method to select donation form by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
       
        DonationForm GetDonationFormById(int id);
    }
}
