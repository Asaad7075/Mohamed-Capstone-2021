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
    /// Created: 2021/02/22
    /// This is interface class for accessing dontion item 
    /// </summary>
    ///
    /// <remarks>
    /// Updater Name
    /// Updated: yyyy/mm/dd
    /// </remarks>
    public interface IDonationAccessor
    {
        /// <summary>
        /// Asaad Mohamed
        /// Created: 2021/02/22
        /// this method for select all donation  
        /// </summary>
        /// <returns></returns>
        List<Donation> SelectAllDonation();

        /// <summary>
        /// Asaad Mohamed
        /// Created: 2021/02/22
        /// This method for update status of donation
        /// </summary>
        /// <param name="oldDonationItem"></param>
        /// <param name="newDonationItem"></param>
        /// <returns></returns>
        int UpdateDonationItemStatus(Donation oldDonationItem, Donation newDonationItem);

        /// <summary>
        /// Asaad Mohamed
        /// Created: 2021/03/15
        /// This method for remove donation
        /// </summary>
        int DeleteDonation(int donationID);

        /// <summary>
        /// Asaad Mohamed
        /// Created: 2021/02/24
        /// This method for insert dontion  
        /// </summary>
        /// <param name="donation"></param>
        /// <returns></returns>
        int InsertDonation(Donation donation);

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/09
        /// 
        /// Retrieves image as a byte array.
        /// </summary>
        /// <param name="donationID"></param>
        /// <returns></returns>
        byte[] SelectImageByDonationId(int donationID);

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/14
        /// 
        /// Retrieves a donated item by
        /// its donation id.
        /// </summary>
        /// <param name="donationID"></param>
        /// <returns></returns>
        Donation SelectDonationByDonationId(int donationID);

        /// <summary>
        /// Richard Schroeder
        /// Created: 2021/04/14
        /// Retrieves donation by DonationID
        /// </summary>
        Donation SelectDonationItemByID(int donationID);
    }
}
