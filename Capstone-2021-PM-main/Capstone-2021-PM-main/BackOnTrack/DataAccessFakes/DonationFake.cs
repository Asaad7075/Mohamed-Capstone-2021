using DataAccessInterfaces;
using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessFakes
{
    /// <summary>
    /// Asaad Mohamed
    /// Created: 2021/02/22
    /// 
    /// This is fake donation item test data.
    /// </summary>
    ///  /// <remarks>
    /// Updater Name
    /// Updated: yyyy/mm/dd
    /// </remarks>
    public class DonationFake : IDonationAccessor
    {
        private List<Donation> donation = null;
        private List<Donation> _donation = new List<Donation>();
        /// <summary>
        /// Asaad Mohamed
        /// Created: 2021/02/22
        /// 
        /// Generation of fake donation item data for test.
        /// </summary>

        public DonationFake()
        {
            donation = new List<Donation>()
            {
                new Donation()

            {
              DonationID = 1,
              DonorID =  2,
              NameOfItem  = "Shoe",
              Description = "Boot with size 9 USA",
              EstValue = 200.0M,
              AgeofItem = 2,
              DropOff = true,
              PickUp = true,
              PickUpDateTime = DateTime.Now,
              MailReceipt = true,
              EmailReceipt = true,
              DonationStatus = "approve"

            },

                new Donation()

            {
               DonationID = 2,
              DonorID =  2,
              NameOfItem  = "Laptop",
              Description = "computer php up to 1TB storage ",
              EstValue = 400.0M,
              AgeofItem = 5,
              DropOff = false,
              PickUp = true,
              PickUpDateTime = DateTime.Now,
              MailReceipt = true,
              EmailReceipt = true,
              DonationStatus = "pending",
              DonatedImage = new byte[50] 
            },

            };

        }
        /// <summary>
        ///  Asaad Mohamed
        /// Created: 2021/03/15
        ///  This is method to delete a doation 
        /// </summary>
        /// <param name="donationID"></param>
        /// <returns></returns>
        public int DeleteDonation(int donationID)
        {
            int donated = donation.Count;
            donated -= 1;
            return donated;
        }

        /// <summary>
        /// Asaad Mohamed
        /// Created: 2021/03/24
        ///  This is method to insert new donation  
        /// </summary>
        public int InsertDonation(Donation donation)
        {
            int result = 0;

            if (_donation.Contains(donation))
            {
                throw new Exception( "Donation already exists in the database.");
            }
            else
            {
                _donation.Add(donation);


                foreach (Donation currentDonation in _donation)
                {
                    if (currentDonation.Equals(donation))
                    {
                        result = 1;
                    }
                }
            }

            return result;
           
        }

        /// <summary>
        /// Asaad Mohamed
        /// Created: 2021/02/22
        ///  This is method to select all doation 
        /// </summary>
        public List<Donation> SelectAllDonation()
        {
            return donation;
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/14
        /// 
        /// </summary>
        /// <param name="donationID"></param>
        /// <returns></returns>
        public Donation SelectDonationByDonationId(int donationID)
        {
            foreach (var d in donation)
            {
                if (d.DonationID == donationID)
                {
                    return d;
                }
            }

            return null;
        }

        public Donation SelectDonationItemByID(int donationID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/09
        /// 
        /// </summary>
        /// <param name="donationID"></param>
        /// <returns></returns>
        public byte[] SelectImageByDonationId(int donationID)
        {
            foreach (var d in donation)
            {
                if (d.DonationID == donationID)
                {
                    return d.DonatedImage;
                }
            }

            return null;
        }

        /// <summary>
        ///  Asaad Mohamed
        /// Created: 2021/02/22
        ///  This update method
        /// </summary>
        /// <param name="oldDonationItem"></param>
        /// <param name="newDonationItem"></param>
        /// <returns></returns>
        public int UpdateDonationItemStatus(Donation oldDonationItem, Donation newDonationItem)
        {
            oldDonationItem = newDonationItem;

            if (oldDonationItem.Equals(newDonationItem))
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

    }
}
