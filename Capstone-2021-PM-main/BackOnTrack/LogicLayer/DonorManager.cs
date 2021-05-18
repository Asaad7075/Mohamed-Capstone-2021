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
    public class DonorManager : IDonorManager
    {
        private IDonorAccessor _donorAccessor;

        /// <summary>
        /// 
        /// This a constructor for test fake data
        /// </summary>
        /// <param name="donorAccessor"></param>
        public DonorManager(IDonorAccessor donorAccessor)
        {
            _donorAccessor = donorAccessor;
        }
        public DonorManager()
        {
            _donorAccessor = new DonorAccessor();

        }
        /// <summary>
        ///   Asaad Mohamed
        /// 2021/03/01
        /// This method for retrive all donation from database by active
        /// </summary>
        /// <param name="active"></param>
        /// <returns></returns>
        public List<Donor> RetrieveAllDonorListByActive(bool active = true)
        {
            try
            {
                return _donorAccessor.SelectDonorByActive(active);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("List Not Available", ex);
            }
        }
        /// <summary>
        /// Asaad Mohamed
        /// 2021/04/01
        /// This method for insert new donor 
        /// </summary>
        /// <param name="donor"></param>
        /// <returns></returns>
        public bool InsertDonor(Donor donor)
        {

            bool result = false;
            
            try
            {
                int newDonor = 0;
                newDonor = _donorAccessor.AddDonor(donor);
                if (newDonor == 0)
                {
                    throw new ApplicationException("New Donor Faild to added.");
                }

                result = true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Add Donor Failed.", ex);
            }
            return result;
        }
        /// <summary>
        /// Asaad Mohamed
        /// 2021/04/01
        /// This method for update old donor 
        /// </summary>
        /// <param name="oldDonor"></param>
        /// <param name="newDonor"></param>
        /// <returns></returns>
        public bool EditDonor(Donor oldDonor, Donor newDonor)
        {
            bool result = false;

            try
            {
                result = (1 == _donorAccessor.UpdateDonor(oldDonor, newDonor));

                if (result == false)
                {
                    throw new ApplicationException("Data not updated.");
                } 
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Update Failed.", ex);
            }
            return result;
        }
        /// <summary>
        /// Asaad Mohamed
        /// created: 2021/04/01
        /// </summary>
        /// <param name="donorID"></param>
        /// <returns></returns>
        public bool DeactivateDonor(int donorID)
        {
            bool result = false;
            try
            {
                result = (_donorAccessor.DeleteDonor(donorID) > 0);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Delete failed." + ex.Message);
            }
            return result;
        }
        /// <summary>
        /// Asaad Mohamed
        /// created: 2021/04/09
        /// This method to select donor by Id from data accesslayer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Donor GetDonorById(int id)
        {
            try
            {
                return _donorAccessor.SelectDonorById(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/25
        /// 
        /// Returns true if donor
        /// exists in the db.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool SelectDonorByDonorEmail(string email)
        {
            bool result = false;

            try
            {
                return _donorAccessor.SelectDonorByEmail(email);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return false;
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/25
        /// 
        /// Returns true if the donor has an account established.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool AuthenticateUser(string email, string password)
        {
            bool result = false;

            password = password.hashSHA256().ToUpper();

            try
            {
                return _donorAccessor.SelectDonorByEmailAndPassword(email, password);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return false;
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/25
        /// 
        /// Returns true if the donor was added
        /// to the application database successfully.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool InsertDonorFromWeb(Donor donor, string password)
        {
            bool result = false;

            password = password.hashSHA256().ToUpper();

            try
            {
                return _donorAccessor.InsertDonorFromWeb(donor, password);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return false;
        }
    }
}
