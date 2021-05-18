using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels
{
    public class Employee
    {
        /// <summary>
        /// Richard Schroeder
        /// Created: 2021/02/19
        ///
        /// Constructor for creating new production Employee objects
        /// </summary>
        public Employee(int employeeID, string firstName, string lastName, string phoneNumber,
            string email, bool active, string address, string gender)
        {
            EmployeeID = employeeID;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Email = email;
            Active = active;
            Address = address;
            Gender = gender;
        }

        public Employee()
        {

        }

        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public bool Active { get; set; }
        public string FullName 
        { 
            get 
            {
                return FirstName + " " + LastName;
            } 
        }
    }

    //EmployeeVM moved to own class to not infringe on tests
}
