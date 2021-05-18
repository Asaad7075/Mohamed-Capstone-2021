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

    /// <summary>
    ///  Asaad Mohamed
    /// 2021/02/04
    /// This class  for Control  for directing  the flow update the status of donor form 
    /// </summary>
    ///  <remarks>
    /// Updater Name
    /// Updated: yyyy/mm/dd 
    /// </remarks>
    /// </summary>
    public class DonationFormManager : IDonationFormManager
    {
        private IDonationFormAccessor _donorFormAccessor;
        /// <summary>
        /// /// <summary>
        /// Asaad Mohamed
        /// 2021/02/04
        /// This  method for test set up
        /// </summary>
        /// <param name="donorFormAccessor"></param>

        public DonationFormManager(IDonationFormAccessor donorFormAccessor)
        {
            _donorFormAccessor = donorFormAccessor;
        }

        /// <summary>
        ///  Asaad Mohamed
        /// 2021/02/04
        /// Empty constructor for initializing the DonorFormManager
        /// </summary>
        public DonationFormManager()
        {
            _donorFormAccessor = new DonationFormAccessor();
        }
        /// <summary>
        ///  Asaad Mohamed
        /// 2021/02/04
        /// this method for update the status of donor form from  
        /// </summary>
        ///  <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// </remarks>
        /// </summary>
        /// </summary>
        /// <param name="oldDonorForm"></param>
        /// <param name="newDonorForm"></param>
        /// <returns></returns>
        public bool EditDonorForm(DonationForm oldDonorForm, DonationForm newDonorForm)
        {
            bool result = false;

            try
            {
                result = _donorFormAccessor.UpdateDonorFormStatus(oldDonorForm, newDonorForm) > 0;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Change Status failed.", ex);
            }

            return result;
        }

        /// <summary>
        /// Asaad Mohamed
        /// 2021/02/04
        /// This method for insert new data into database 
        /// </summary>
        /// <param name="donationForm"></param>
        /// <returns></returns>
        public bool InsertDonationForm(DonationForm donationForm)
        {
            bool result = false;
            try
            {
                //  return _donorFormAccessor.InsertDonationForm(donationForm) == 1;
                int donorForm = 0;
                donorForm = _donorFormAccessor.InsertDonationForm(donationForm);
                if (donorForm == 0)
                {
                    throw new ApplicationException("Donor not created.");
                }
            }
            catch (Exception ex)
            {

                throw new ApplicationException("created Failed.", ex);
            }
            return result;
        }
        /// <summary>
        ///  Asaad Mohamed
        /// 2021/02/04
        /// This method for retrive all donation form from database 
        /// </summary>
        ///  <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// </remarks>
        /// </summary>
        /// </summary>
        /// <returns></returns>
        public List<DonationForm> RetrieveAllDonorFormList()
        {
            List<DonationForm> forms = null;

            try
            {
                forms = _donorFormAccessor.SelectAllDonationForm();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("List Not Available", ex);
            }
            return forms;
        }
        /// <summary>
        ///  Asaad Mohamed
        /// 2021/02/04
        /// This method for retrive donation form by id from database 
        /// </summary>
        ///  <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// </remarks>
        /// </summary>
        /// </summary>
        /// <returns></returns>
        public DonationForm GetDonationFormById(int id)
        {
            try
            {
                return _donorFormAccessor.SelectDonationFormById(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
