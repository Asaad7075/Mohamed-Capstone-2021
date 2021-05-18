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
    public class EmployeeManager : IEmployeeManager
    {
        private IEmployeeAccessor _employeeAccessor = new EmployeeAccessor();

        /// <summary>
        /// Richard Schroeder
        /// Created: 2021/02/19
        ///
        /// Empty constructor for initializing the User Manager
        /// </summary>
        public EmployeeManager()
        {
            _employeeAccessor = new EmployeeAccessor();
        }

        /// <summary>
        /// Richard Schroeder
        /// Created: 2021/02/19
        ///
        /// Constructor for allowing the initialization of a UserAccessor
        /// </summary>
        /// <param name="userAccessor"></param>
        public EmployeeManager(IEmployeeAccessor userAccessor) // Dependency Injection
        {
            _employeeAccessor = userAccessor;
        }

        /// <summary>
        /// Richard Schroeder
        /// Created: 2021/02/19
        /// function for authenticating a user login and returning role
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Employee AuthenticateUser(string email, string password)
        {
            Employee employee = null;

            // hash the password
            password = password.hashSHA256();

            // call the data access method
            try
            {
                if (1 == _employeeAccessor.AuthenticateUser(email, password))
                {
                    //call the methods to create an employee object
                    employee = _employeeAccessor.SelectEmployeeByEmail(email);
                }
                else
                {
                    throw new ApplicationException("Bad email, password, or account is inactive");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Login failed.", ex);
            }

            return employee;
        }

        /// <summary>
        /// Richard Schroeder
        /// Created: 2021/02/19
        /// updating a user's password
        /// </summary>
        /// <param name="email"></param>
        /// <param name="newPasswordHash"></param>
        /// <param name="oldPasswordHash"></param>
        /// <returns></returns>
        public bool UpdatePassword(string email, string oldPasswordHash, string newPasswordHash)
        {
            bool result = false;

            try
            {
                //hash passed string passwords
                oldPasswordHash = oldPasswordHash.hashSHA256();
                newPasswordHash = newPasswordHash.hashSHA256();

                // call accessor UpdatePassword() and pass email and hashed passwords
                // check if returned value is equal to 1 (if 1, result = true)
                result = (1 == _employeeAccessor.UpdatePassword(email, oldPasswordHash, newPasswordHash));

                // if result != 1, return false and throw exception
                if (!result)
                {
                    throw new ApplicationException("Update Failed.");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Password not changed.", ex);
            }

            return result;
        }

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/02/20
        ///
        /// Retrives all the roles
        /// </summary>
        /// <returns></returns>
        public List<string> RetrieveAllRoles()
        {
            List<string> roles = new List<string>();
            try
            {
                roles = _employeeAccessor.SelectAllRoles();
                if (roles == null)
                {
                    throw new ApplicationException("Select all roles returns null.");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data Unavailable.\n\n"
                    + ex.Message + "\n\n" + ex.InnerException);
            }
            return roles;
        }

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/02/20
        ///
        /// Edits the employees role.
        /// </summary>
        /// <param name="oldEmployee"></param>
        /// <param name="newEmployee"></param>
        /// <returns></returns>
        public bool EditEmployeeRole(EmployeeVM oldEmployee, EmployeeVM newEmployee)
        {
            bool result = false;
            bool insertResult = false;
            bool deleteResult = false;

            try
            {
                // first for loop adds inserts the new role
                foreach (var role in newEmployee.Roles)
                {
                    if (!oldEmployee.Roles.Contains(role))
                    {
                        insertResult = (1 == _employeeAccessor.InsertEmployeeRole(oldEmployee.EmployeeID, role));
                    }
                    else
                    {
                        insertResult = true;
                    }
                }
                // second for loop removes the old roles
                foreach (var role in oldEmployee.Roles)
                {
                    if (!newEmployee.Roles.Contains(role))
                    {
                        deleteResult = (1 == _employeeAccessor.DeleteEmployeeRole(oldEmployee.EmployeeID, role));
                    }
                    else
                    {
                        deleteResult = true;
                    }
                }
                // conditional ensuring both insert and delete have operated
                if (deleteResult && insertResult == true)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Update Failed.\n\n"
                    + ex.Message + "\n\n" + ex.InnerException);
            }
            return result;
        }

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/02/28
        ///
        /// retrives a list of active employees
        /// </summary>
        /// <returns></returns>
        public List<EmployeeVM> RetrieveEmployeesByActive(bool active)
        {
            List<EmployeeVM> employees = new List<EmployeeVM>();
            try
            {
                employees = _employeeAccessor.SelectEmployeesByActive(active);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Active employee could not be retrieved.\n\n"
                    + ex.Message + "\n\n" + ex.InnerException);
            }
            return employees;
        }

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/02/20
        ///
        /// Retrives all the roles by the employeeID
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public List<string> RetrieveRolesByEmployeeID(int employeeID)
        {
            List<string> roles = null;
            try
            {
                roles = _employeeAccessor.SelectEmployeeRolesByEmployeeID(employeeID);
                if (roles == null)
                {
                    roles = new List<string>();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Employee Roles could not be retrived\n\n" + ex.Message + "\n\n" + ex.InnerException);
            }
            return roles;
        }

        /// <summary>
        /// Richard Schroeder
        /// Created: 2021/03/16
        ///
        /// A method inserting a new employee object into the DB
        /// <param name="employee"></param>
        /// </summary>
        public bool InsertEmployee(EmployeeVM employee)
        {
            bool result = false;
            int newEmployeeID = 0; // invalid employeeID
            try
            {
                newEmployeeID = _employeeAccessor.InsertEmployee(employee);
                if (newEmployeeID == 0)
                {
                    throw new ApplicationException("New employee was not added.");
                }

                result = true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Add Employee Failed.", ex);
            }
            return result;
        }

        /// <summary>
        /// Richard Schroeder
        /// Created: 2021/03/16
        ///
        /// A method editing an existing employee information
        /// <param name="oldEmployee"></param>
        /// <param name="newEmployee"></param>
        /// </summary>
        public bool EditEmployee(EmployeeVM oldEmployee, EmployeeVM newEmployee)
        {
            bool result = false;
            try
            {
                result = (1 == _employeeAccessor.EditEmployee(oldEmployee, newEmployee));
                if (result == false)
                {
                    throw new ApplicationException("Profile data not changed.");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Update Failed.", ex);
            }
            return result;
        }


        /// <summary>
        /// Richard Schroeder
        /// Created: 2021/03/16
        ///
        /// A method for deactivating an employee
        /// <param name="employeeID"></param>
        /// </summary>
        public bool DeactivateEmployee(int employeeID)
        {
            bool result = false;
            try
            {
                _employeeAccessor.DeactivateEmployee(employeeID);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to deactivate user. " + ex.Message);
            }
            return result;
        }

        /// <summary>
        /// Richard Schroeder
        /// Created: 2021/03/16
        ///
        /// A method for reactivating an employee
        /// <param name="employeeID"></param>
        /// </summary>
        public bool ReactivateEmployee(int employeeID)
        {
            bool result = false;
            try
            {
                _employeeAccessor.ReactivateEmployee(employeeID);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to reactivate user. " + ex.Message);
            }
            return result;
        }
        /// <summary>
        /// Jakub Kawski
        /// 2021/04/24
        /// 
        /// Get a list of employess whom are drivers.
        /// </summary>
        /// <returns></returns>
        public List<EmployeeVM> RetrieveAllDrivers()
        {
            List<EmployeeVM> employees = new List<EmployeeVM>();
            try
            {
                employees = _employeeAccessor.SelectAllDrivers();
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to get drivers list." + ex.Message);
            }
            return employees;
        }
    }
}
