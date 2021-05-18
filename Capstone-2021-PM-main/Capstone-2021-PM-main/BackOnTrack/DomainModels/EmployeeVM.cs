using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels
{
    /// <summary>
    /// Nate Hepker
    /// Created: 2021/03/09
    /// 
    /// View model for the employee objects to get the roles with the employee
    /// 
    /// This has been moved to its own class, It will not work in Employee
    /// due to the open and empty constructor that is requird for the unit test.
    /// </summary>
    public class EmployeeVM : Employee
    {
        public EmployeeVM(int employeeID, string firstName, string lastName, string phoneNumber,
            string email, bool active, string address, string gender, List<string> roles)
        {
            EmployeeID = employeeID;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Email = email;
            Active = active;
            Address = address;
            Gender = gender;
            Roles = roles;
        }

        public EmployeeVM()
        {

        }

        public List<string> Roles { get; set; }
    }
}
