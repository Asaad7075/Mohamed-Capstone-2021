using DataAccessInterfaces;
using DataAccessLayer;
using DomainModels;
using LogicInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class DonationManager : IDonationManager
    {
        private IDonationAccessor _donationAccessor;
        /// <summary>
        /// Asaad Mohamed
        /// 2021/02/22
        /// This This a constructor for test set up fake data  
        /// </summary>
        /// <param name="donationAccessor"></param>

        public DonationManager(IDonationAccessor donationAccessor)
        {
            _donationAccessor = donationAccessor;
        }
        /// <summary>
        ///  Asaad Mohamed
        /// 2021/02/22
        /// Empty constructor for initializing the Donation manager and refresh the donation list
        /// </summary>
        public DonationManager()
        {
            _donationAccessor = new DonationAccessor();
        }

        /// <summary>
        ///  Asaad Mohamed
        /// 2021/02/22
        /// This method for update the status of donation from database 
        /// </summary>
        ///  <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// </summary>
        /// <param name="oldDonationItem"></param>
        /// <param name="newDonationItem"></param>
        /// <returns></returns>
        public bool EditDonation(Donation oldDonationItem, Donation newDonationItem)
        {
            bool result = false;

            try
            {
                result = _donationAccessor.UpdateDonationItemStatus(oldDonationItem, newDonationItem) > 0;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Change Status failed.", ex);
            }

            return result;
        }
        /// <summary>
        ///   Asaad Mohamed
        /// 2021/02/22
        /// This method for retrive all donation from database 
        /// </summary>
        ///  <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// </remarks>
        /// </summary>
        /// </summary>
        /// <returns></returns>
        public List<Donation> RetrieveAllDonationList()
        {
            List<Donation> donations = null;

            try
            {
                donations = _donationAccessor.SelectAllDonation();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("List Not Available", ex);
            }
            return donations;
        }
        /// <summary>
        ///   Asaad Mohamed
        /// 2021/03/15
        /// This method for remove a donation from database 
        /// </summary>
        ///  <remarks>
        /// </summary>
        /// <param name="donationID"></param>
        /// <returns></returns>
        public bool DeactivateDonation(int donationID)
        {
            bool result = false;
            try
            {
                result = (_donationAccessor.DeleteDonation(donationID) > 0);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Delete failed.", ex);
            }
            return result;
        }
        /// <summary>
        /// Asaad Mohamed
        /// 2021/02/24
        /// This method for insert new data into database 
        /// </summary>
        /// <param name="donation"></param>
        /// <returns></returns>
        public bool InsertDonation(Donation donation)
        {
            bool result = false;
            try
            {
               
                int newdonation = 0;
                newdonation = _donationAccessor.InsertDonation(donation);
                if (newdonation == 0)
                {
                    throw new ApplicationException("Donation not created.");
                }
                else
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {

                throw new ApplicationException("created Failed.", ex);
            }
            return result;
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/09
        /// 
        /// Retrieves donation image by donation ID.
        /// </summary>
        /// <param name="donationID"></param>
        /// <returns></returns>
        public byte[] RetrieveDonationImageByDonationID(int donationID)
        {
            byte[] result = null;

            try
            {
                result = _donationAccessor.SelectImageByDonationId(donationID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Image could not be found."
                    + "\n\n" + ex.Message);
            }

            return result;
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/14
        /// 
        /// Retrieves donation object by donation id.
        /// </summary>
        /// <param name="donationID"></param>
        /// <returns></returns>
        public Donation RetrieveDonationByDonationID(int donationID)
        {
            Donation result = null;

            try
            {
                result = _donationAccessor.SelectDonationByDonationId(donationID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Donation could not be retrieved.\n\n"
                    + ex.Message);
            }

            return result;
        }
    }
}
