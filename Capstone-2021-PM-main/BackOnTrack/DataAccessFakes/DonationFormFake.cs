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
    /// Created: 2021/02/04
    /// 
    /// this is fake donor form test data.
    /// </summary>
    ///  /// <remarks>
    /// Updater Name
    /// Updated: yyyy/mm/dd
    /// </remarks>
    public class DonationFormFake : IDonationFormAccessor
    {
        private List<DonationForm> donationForms = null;
        private List<DonationForm> _donationForm = new List<DonationForm> ();

        /// <summary>
        /// Asaad Mohamed
        /// Created: 2021/02/04
        /// 
        /// Generation of fake donor form data for test.
        /// </summary>
        public DonationFormFake()
        {
            donationForms = new List<DonationForm>()
            {
                new DonationForm()

            {
              DonorFormID = 1,
              DonorID =  2,
              DateCreated  = DateTime.Now,
              Status = "pending"

            },

                new DonationForm()

            {
              DonorFormID = 2,
              DonorID =  4,
              DateCreated  = DateTime.Now,
              Status = "submitted"

            },

            };

        }

        public int InsertDonationForm(DonationForm donationForm)
        {
            //return 0;
            List<DonationForm> result = new List<DonationForm>();
            foreach (var x in _donationForm)
            {
                result.Add(x);
            }
            return result.Count();

        }

        /// <summary>
        /// Asaad Mohamed
        /// Created: 2021/02/04
        ///  This is method to select all donor form 
        /// </summary>

        public List<DonationForm> SelectAllDonationForm()
        {
            return donationForms;

        }
        /// <summary>
        /// Asaad Mohamed
        /// Created: 2021/04/09
        ///  This is method to select all donor by id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DonationForm SelectDonationFormById(int id)
        {
            return (from x in donationForms where x.DonorFormID == id select x).Single();
        }

        /// <summary>
        /// Asaad Mohamed
        /// Created: 2021/02/04
        ///  This update method
        /// </summary>
        /// <param name="oldDonorForm"></param>
        /// <param name="newDonorForm"></param>
        /// <returns></returns>
        public int UpdateDonorFormStatus(DonationForm oldDonorForm, DonationForm newDonorForm)
        {
            oldDonorForm = newDonorForm;

            if (oldDonorForm.Equals(newDonorForm))
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
