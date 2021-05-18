using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DomainModels;

namespace DataAccessFakes
{
    public class ZipCodeFake : IZipCodeAccessor
    {

        private List<ZipCodeFile> _zipCodes = new List<ZipCodeFile>();

        /// <summary>
        /// Chase Martin
        /// Created: 2021/02/12
        /// 
        /// Empty constructor that initializes fake zip data
        /// </summary>

        public ZipCodeFake()
        {
            _zipCodes.Add(new ZipCodeFile
            {
                ZipCode = "52314",
                City = "Solon",
                State = "IA",
                isServicable = true
            });


            _zipCodes.Add(new ZipCodeFile
            {
                ZipCode = "52404",
                City = "North Liberty",
                State = "IA",
                isServicable = true
            });

            _zipCodes.Add(new ZipCodeFile
            {
                ZipCode = "52404",
                City = "Marion",
                State = "IA",
                isServicable = false
            });

            _zipCodes.Add(new ZipCodeFile
            {
                ZipCode = "52314",
                City = "Cedar Rapids",
                State = "IA",
                isServicable = false
            });

        }

        public int DeactivateZipCode(string zipCode)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/02/12
        /// 
        /// Fake DB insert method.
        /// </summary>
        /// <param name="zipCode"></param>
        /// <returns></returns>
        public int InsertZipCode(ZipCodeFile zipCode)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/02/12
        /// 
        /// Fake DB insert method.
        /// </summary>
        /// <param name="zipCode"></param>
        /// <returns></returns>
        public bool InsertZipCodeBool(ZipCodeFile zipCode)
        {
            bool result = false;

            if (_zipCodes.Contains(zipCode))
            {
                throw new Exception("Zip code already exists in the database.");
            }
            else
            {
                _zipCodes.Add(zipCode);


                foreach (ZipCodeFile currZipCode in _zipCodes)
                {
                    if (currZipCode.Equals(zipCode))
                    {
                        result = true;
                    }
                }
            }

            return result;
        }

        public int ReactivateZipCode(string zipCode)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/02/12
        /// 
        /// Select all zip codes method.
        /// </summary>

        public List<ZipCodeFile> SelectAllZipCodes()
        {
            List<ZipCodeFile> zipCodes = new List<ZipCodeFile>();

            zipCodes.Add(new ZipCodeFile()
            {
                ZipCode = "52314",
                City = "Marion",
                State = "IA",
                isServicable = true
            });
            zipCodes.Add(new ZipCodeFile()
            {
                ZipCode = "52314",
                City = "Cedar Rapids",
                State = "IA",
                isServicable = true
            });
            zipCodes.Add(new ZipCodeFile()
            {
                ZipCode = "52314",
                City = "Hiawatha",
                State = "IA",
                isServicable = false
            });
            return zipCodes;
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/02/12
        /// 
        /// Selecting all zip codes by is servicable being true.
        /// </summary>
        public List<ZipCodeVM> SelectZipCodesByIsServicable(bool isServicable = true)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/02/12
        /// 
        /// Selecting all zip codes by their selected status.
        /// </summary>
        public List<ZipCodeFile> SelectZipCodesByStatus(string status)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/02/12
        /// 
        /// Selecting a zip code to update.
        /// </summary>
        public int UpdateZipCodeFile(ZipCodeFile oldZipCode, ZipCodeFile newZipCode)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/02/12
        /// 
        /// Selecting a zip code to update.
        /// </summary>
        public int UpdateZipCodeFile(ZipCodeFile zipCodeFile)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/02/12
        /// 
        /// Return zip code true or false if it has been successfully updated.
        /// </summary>
        public bool UpdateZipCodeFileBool(ZipCodeFile oldZipCode, ZipCodeFile newZipCode)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/02/12
        /// 
        /// Updates zip code status from old status to new status.
        /// </summary>
        public int UpdateZipCodeStatus(int zipCodeID, string oldStatus, string newStatus)
        {
            throw new NotImplementedException();
        }
    }
}
