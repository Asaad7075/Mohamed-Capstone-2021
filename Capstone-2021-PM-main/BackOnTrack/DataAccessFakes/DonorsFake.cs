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
    /// Created: 2021/03/01
    /// 
    /// This is fake donor  test data.
    /// </summary>
    ///  /// <remarks>
    /// Updater Name
    /// Updated: yyyy/mm/dd
    /// </remarks>
    public class DonorsFake : IDonorAccessor
    {
        private List<Donor> _donor = null;
        private Dictionary<string, Donor> _passwordHashes = new Dictionary<string, Donor>();

        /// <summary>
        /// Asaad Mohamed
        /// Created: 2021/03/01
        /// 
        /// Generation of fake donor  data for test.
        /// </summary>
        /// 

        public DonorsFake()
        {
            _donor = new List<Donor>()
            {
                new Donor()

            {

              DonorID =  1,
              Business = true,
              Individual = false,
              BusinessName  = "Dream Center",
              FirstName = "Emma",
              LastName = "Noah",
              MiddleInitial = "A",
              Address = "3465 kirkwood blv sw ",
              ZipCode = "52404",
              PhoneNumber = "3184352634",
              Email = "emma@gmail.com",
              SS = "",
              EIN = "",
              Active = true

            },
                new Donor()

            {
              DonorID =  2,
              Business = true,
              Individual = false,
              BusinessName  = "Smaile Charities",
              FirstName = "Liam",
              LastName = "William",
              MiddleInitial = "A",
              Address = "4965 6th st sw ",
              ZipCode = "52404",
              PhoneNumber = "3184364634",
              Email = "liam@gmail.com",
              SS = "",
              EIN = "",
              Active = true
            },
            };
        }
        /// <summary>
        /// Asaad Mohamed
        /// 2021/04/01
        /// This method for insert new donor 
        /// </summary>
        /// <param name="donor"></param>
        /// <returns></returns>
        public int AddDonor(Donor donor)
        {
            int result = 0;

            if (_donor.Contains(donor))
            {
                throw new Exception("Donor already exists in the database.");
            }
            else
            {
                _donor.Add(donor);


                foreach (Donor currentDonor in _donor)
                {
                    if (currentDonor.Equals(donor))
                    {
                        result = 1;
                    }
                }
            }

            return result;
        }
        /// <summary>
        ///  Asaad Mohamed
        /// Created: 2021/04/01
        ///  This is method to delete a donor 
        /// </summary>
        /// <param name="donorID"></param>
        /// <returns></returns>
        public int DeleteDonor(int donorID)
        {
            int deletDonor = _donor.Count;
            deletDonor -= 1;
            return deletDonor;
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/25
        /// 
        /// </summary>
        /// <param name="donor"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool InsertDonorFromWeb(Donor donor, string passwordHash)
        {
            if (passwordHash.Equals("") || passwordHash.Equals(null) ||
                donor == null)
            {
                return false;
            }
            else
            {
                _donor.Add(donor);
                _passwordHashes.Add(passwordHash, donor);
                return true;
            }
        }

        /// <summary>
        /// Asaad Mohamed
        /// Created: 2021/03/01
        ///  This is method to select all doation 
        /// </summary>
        public List<Donor> SelectDonorByActive(bool active = true)
        {
            return _donor;
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/25
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool SelectDonorByEmail(string email)
        {
            foreach (Donor donor in _donor)
            {
                if (donor.Email.Equals(email))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/25
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool SelectDonorByEmailAndPassword(string email, string passwordHash)
        {
            Donor donor = _passwordHashes[passwordHash];
            if (donor != null && donor.Email.Equals(email))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Asaad Mohamed
        /// Created: 2021/04/09
        ///  This is method to select a donor by id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Donor SelectDonorById(int id)
        {
            return (from x in _donor where x.DonorID == id select x).Single();
        }

        /// <summary>
        /// Asaad Mohamed
        /// 2021/04/01
        /// This method for update the donor data 
        /// </summary>
        /// <param name="oldDonor"></param>
        /// <param name="newDonor"></param>
        /// <returns></returns>
        public int UpdateDonor(Donor oldDonor, Donor newDonor)
        {
            oldDonor = newDonor;

            if (oldDonor.Equals(newDonor))
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
