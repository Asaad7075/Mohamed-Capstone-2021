using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataAccessLayer;
using DomainModels;
using LogicInterfaces;

namespace LogicLayer
{
    /// <summary>
    /// Chase Martin
    /// Created: 2021/02/12
    /// 
    /// Control for directing the flow of zip code data.
    /// </summary>
    public class ZipCodeManager : IZipCodeManager
    {

        private IZipCodeAccessor _zipCodeAccessor = null;

        /// <summary>
        /// Chase Martin
        /// Created: 2021/02/12
        /// 
        /// Empty constructor for initializing the ZipCodeManager
        /// </summary>
        public ZipCodeManager()
        {
            _zipCodeAccessor = new ZipCodeAccessor();
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/02/12
        /// 
        /// Constructor for allowing the initialization of a ZipCodeAccessor with classes
        /// that inherit from IZipCodeAccessor at runtime
        /// </summary>
        /// <param name="zipCodeAccessor"></param>
        public ZipCodeManager(IZipCodeAccessor zipCodeAccessor) //Dependency Injection
        {
            _zipCodeAccessor = zipCodeAccessor;
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/02/12
        /// 
        /// Logic for adding zip code.
        /// </summary>
        /// <param name="zipCode"></param>
        /// <returns></returns>
        public bool AddZipCode(ZipCodeFile zipCode)
        {
            bool result = false;
            try
            {
                result = (1 == _zipCodeAccessor.InsertZipCode(zipCode));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Zip Code not added to database. Try again.", ex);
            }
            return result;
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/02/12
        /// 
        /// Logic for deleting zip code.
        /// </summary>
        /// <param name="zipCode"></param>
        /// <returns></returns>
        public bool DeleteZipCode(ZipCodeFile zipCode)
        {
            bool result = false;


            return result;
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/02/12
        /// 
        /// Logic for edit zip code.
        /// </summary>
        /// <param name="oldZipCode"></param><param name="newZipCode"></param>
        /// <returns></returns>
        public bool EditZipCodeFile(ZipCodeFile oldZipCode, ZipCodeFile newZipCode)
        {
            bool result = false;
            try
            {
                result = (1 == _zipCodeAccessor.UpdateZipCodeFile(oldZipCode, newZipCode));
                if (result == false)
                {
                    throw new ApplicationException("Zip Code data not changed.");
                }


            }
            catch (Exception ex)
            {
                throw new ApplicationException("Update Failed.", ex);
            }
            return result;
        }


        /// <summary>
        /// Chase Martin
        /// Created: 2021/02/12
        /// 
        /// Logic for retrieving all zip codes.
        /// </summary>
        public List<ZipCodeFile> RetrieveAllZipCodes()
        {
            List<ZipCodeFile> zipCodes = new List<ZipCodeFile>();
            try
            {
                zipCodes = _zipCodeAccessor.SelectAllZipCodes();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Could not retrieve data.", ex);
            }
            return zipCodes;
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/02/12
        /// 
        /// Logic for retrieving zip code(s) by isServicable.
        /// </summary>
        /// <param name="isServicable"></param>
        /// <returns></returns>
        public List<ZipCodeVM> RetrieveZipCodesByIsServicable(bool isServicable = true)
        {
            List<ZipCodeVM> zipCodes = null;

            try
            {
                zipCodes = _zipCodeAccessor.SelectZipCodesByIsServicable(isServicable);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Zip Code list not available.", ex);
            }

            return zipCodes;
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/02/12
        /// 
        /// Logic for editing zip code status
        /// </summary>
        /// <returns></returns>
        public bool EditZipCodeStatus(int cabinID, string oldStatus, string newStatus)
        {
            try
            {
                return (1 == _zipCodeAccessor.UpdateZipCodeStatus(cabinID,
                            oldStatus, newStatus));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Change status to '" +
                    newStatus + "' failed.", ex);
            }
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/02/12
        /// 
        /// Logic for retrieving zip code(s) by their status.
        /// </summary>
        /// <returns></returns>
        public List<ZipCodeFile> RetrieveZipCodesByStatus(string status)
        {
            try
            {
                return _zipCodeAccessor.SelectZipCodesByStatus(status);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data unavailable.", ex);
            }
        }
    }
}


