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
    /// 2021/02/22
    /// This is an interface class to retrive and edite donation item
    /// </summary>
    public interface IDonationManager
    {
        /// <summary>
        /// Asaad Mohamed
        /// 2021/02/04
        /// This method to retrive all donation
        /// </summary>
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// </remarks>
        /// <returns></returns>

        List<Donation> RetrieveAllDonationList();


        /// <summary>
        /// Asaad Mohamed
        /// 2021/02/04
        /// This method to edit the status of donation 
        /// </summary>
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// </remarks>
        /// </summary>
        /// <param name="oldDonationItem"></param>
        /// <param name="newDonationItem"></param>
        /// <returns></returns>
        bool EditDonation(Donation oldDonationItem, Donation newDonationItem);

        /// <summary>
        /// Asaad Mohamed
        /// 2021/02/04
        /// This method to remove the Donation from list 
        /// </summary>
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// </remarks>
        /// </summary>
        bool DeactivateDonation(int donationID);

        /// <summary>
        /// Asaad Mohamed
        /// 2021/02/24
        /// This method to add new donation
        /// </summary>
        /// <param name="donation"></param>
        /// <returns></returns>
        bool InsertDonation(Donation donation);

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/09
        /// 
        /// Retrieves donation image by donation ID.
        /// </summary>
        /// <param name="donationID"></param>
        /// <returns></returns>
        byte[] RetrieveDonationImageByDonationID(int donationID);

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/14
        /// 
        /// Retrieves donation object by donation ID.
        /// </summary>
        /// <param name="donationID"></param>
        /// <returns></returns>
        Donation RetrieveDonationByDonationID(int donationID);
    }
}
