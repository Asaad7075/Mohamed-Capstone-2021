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
    /// Chantal Shirley
    /// Created: 2021/02/21
    ///
    /// Fake Employee test data.
    /// </summary>
    ///
    /// <remarks>
    /// Zach Stultz
    /// Updated: 2021/04/30
    /// Added additional roles.
    /// </remarks>
    public class EmployeeFake : IEmployeeAccessor
    {
        private List<EmployeeVM> _employees = new List<EmployeeVM>();

        List<string> _roles = new List<string>() { { ("Admin") }, { ("Manager") }, { ("Employee") }, { ("Maintenance") },
            { ("Driver") }, { ("Specialist") }, { ("Item Inspector") }, { ("Inventory Auditor") }, { ("Donor") }, { ("Provider") } };

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/02/21
        ///
        /// Empty constructor that initializes fake employee data.
        /// </summary>
        public EmployeeFake()
        {

            _employees.Add(new EmployeeVM
            {
                EmployeeID = 100000,
                FirstName = "The",
                LastName = "Doctor",
                PhoneNumber = "1335048955",
                Email = "wibblywobbly@backontrack.com",
                //PasswordHash = "9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e",
                Active = false,
                Address = "352 Dola Mine Road, Morrisville, NC, 27560",
                Gender = "Time Lord",
                Roles = new List<string>()
                {
                    "Logistics Manager"
                }
            });

            _employees.Add(new EmployeeVM
            {
                EmployeeID = 185215,
                FirstName = "Janet",
                LastName = "Weiss",
                PhoneNumber = "832-123-2938",
                Email = "JanetW@company.com",
                Active = true,
                Roles = new List<string>()
                {
                    "Logistics Manager"
                }
            });

            _employees.Add(new EmployeeVM
            {
                EmployeeID = 248357,
                FirstName = "Brad",
                LastName = "Major",
                PhoneNumber = "832-123-2938",
                Email = "BradM@company.com",
                Active = true,
                Roles = new List<string>()
                {
                    "Logistics Maintenance"
                }
            });

            _employees.Add(new EmployeeVM
            {
                EmployeeID = 813149,
                FirstName = "Everett",
                LastName = "Scott",
                PhoneNumber = "726-145-2966",
                Email = "EverettM@company.com",
                Active = true,
                Roles = new List<string>()
                {
                    "Logistics Admin"
                }
            });

            _employees.Add(new EmployeeVM
            {
                EmployeeID = 813150,
                FirstName = "Debera",
                LastName = "Jensen",
                PhoneNumber = "6024235803",
                Email = "debera@backontrack.com",
                Active = true,
                Roles = new List<string>()
                {
                    "Logistics Driver"
                }
            });

            _employees.Add(new EmployeeVM
            {
                EmployeeID = 813151,
                FirstName = "Michael",
                LastName = "Bryant",
                PhoneNumber = "8474894015",
                Email = "michael@backontrack.com",
                Active = true,
                Roles = new List<string>()
                {
                    "Inventory Admin"
                }
            });

            _employees.Add(new EmployeeVM
            {
                EmployeeID = 813152,
                FirstName = "Jacob",
                LastName = "London",
                PhoneNumber = "8173274924",
                Email = "jacob@backontrack.com",
                Active = true,
                Roles = new List<string>()
                {
                    "Inventory Maintenance"
                }
            });

            _employees.Add(new EmployeeVM
            {
                EmployeeID = 813153,
                FirstName = "Susan",
                LastName = "Cheek",
                PhoneNumber = "5076762141",
                Email = "susan@backontrack.com",
                Active = true,
                Roles = new List<string>()
                {
                    "Inventory Manager"
                }
            });

            _employees.Add(new EmployeeVM
            {
                EmployeeID = 813154,
                FirstName = "Anthony",
                LastName = "Kennedy",
                PhoneNumber = "7017297065",
                Email = "anthony@backontrack.com",
                Active = true,
                Roles = new List<string>()
                {
                    "Inventory Specialist"
                }
            });

            _employees.Add(new EmployeeVM
            {
                EmployeeID = 813155,
                FirstName = "Lawrence",
                LastName = "Taylor",
                PhoneNumber = "4404797947",
                Email = "lawrence@backontrack.com",
                Active = true,
                Roles = new List<string>()
                {
                    "Material Handling Admin"
                }
            });

            _employees.Add(new EmployeeVM
            {
                EmployeeID = 813156,
                FirstName = "Ramona",
                LastName = "Casillas",
                PhoneNumber = "6317415195",
                Email = "ramona@backontrack.com",
                Active = true,
                Roles = new List<string>()
                {
                    "Material Handling Maintenance"
                }
            });

            _employees.Add(new EmployeeVM
            {
                EmployeeID = 813157,
                FirstName = "Nora",
                LastName = "Holzman",
                PhoneNumber = "2068196333",
                Email = "nora@backontrack.com",
                Active = true,
                Roles = new List<string>()
                {
                    "Material Handling Manager"
                }
            });

            _employees.Add(new EmployeeVM
            {
                EmployeeID = 813158,
                FirstName = "Kristin",
                LastName = "Trujillo",
                PhoneNumber = "5627865514",
                Email = "kristin@backontrack.com",
                Active = true,
                Roles = new List<string>()
                {
                    "Material Handling Item Inspector"
                }
            });

            _employees.Add(new EmployeeVM
            {
                EmployeeID = 813159,
                FirstName = "Jeffrey",
                LastName = "Carter",
                PhoneNumber = "4237918235",
                Email = "jeffrey@backontrack.com",
                Active = true,
                Roles = new List<string>()
                {
                    "Material Handling Inventory Auditor"
                }
            });

            _employees.Add(new EmployeeVM
            {
                EmployeeID = 813160,
                FirstName = "Trista",
                LastName = "Farmer",
                PhoneNumber = "3044392942",
                Email = "trista@backontrack.com",
                Active = true,
                Roles = new List<string>()
                {
                    "Material Handling Donor"
                }
            });

            _employees.Add(new EmployeeVM
            {
                EmployeeID = 813161,
                FirstName = "Joseph",
                LastName = "Ellis",
                PhoneNumber = "9518237158",
                Email = "joseph@backontrack.com",
                Active = true,
                Roles = new List<string>()
                {
                    "Service Provision Admin"
                }
            });

            _employees.Add(new EmployeeVM
            {
                EmployeeID = 813162,
                FirstName = "Rupert",
                LastName = "Lingle",
                PhoneNumber = "5153604555",
                Email = "rupert@backontrack.com",
                Active = true,
                Roles = new List<string>()
                {
                    "Service Provision Manager"
                }
            });

            _employees.Add(new EmployeeVM
            {
                EmployeeID = 813163,
                FirstName = "Barbara",
                LastName = "Rodriguez",
                PhoneNumber = "7276574777",
                Email = "barbara@backontrack.com",
                Active = true,
                Roles = new List<string>()
                {
                    "Service Provision Provider"
                }
            });


        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/02/21
        ///
        /// Method for retrieving employee by their employee ID.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public EmployeeVM SelectEmployeeByEmployeeID(int ID)
        {
            EmployeeVM employee = null;
            bool employeeExists = false;

            foreach (EmployeeVM emp in _employees)
            {
                if (emp.EmployeeID == ID)
                {
                    employee = emp;
                    employeeExists = true;
                    break;
                }
            }
            if (employeeExists != true)
            {
                throw new ApplicationException("Employee could not be found!");
            }

            return employee;
        }

        /// <summary>
        /// Chantal Shirley
        /// </summary>
        /// <param name="iD"></param>
        /// <returns></returns>
        public List<string> SelectEmployeeRolesByEmployeeID(int iD)
        {
            for (int i = 0; i < _employees.Count; i++)
            {
                if (_employees[i].EmployeeID == iD)
                {
                    _roles = _employees[i].Roles;
                }
            }

            return _roles;
        }


        /// <summary>
        /// Richard Schroeder
        /// Created: 2021/02/23
        ///
        /// A method for authenticating active and valid user login credentials
        /// </summary>
        public int AuthenticateUser(string email, string passwordHash)
        {
            int result = 0;

            //try
            //{

            //    foreach (Employee e in _employees)
            //    {
            //        if (e.Email == email && e.PasswordHash == passwordHash && e.Active == true)
            //        {
            //            result = 1;
            //        }
            //    }

            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}

            return result;
        }

        /// <summary>
        /// Richard Schroeder
        /// Created: 2021/02/23
        ///
        /// Method for selecting a user by their email record string
        /// </summary>
        public Employee SelectEmployeeByEmail(string email)
        {
            Employee user = null;

            try
            {

                foreach (Employee e in _employees)
                {
                    if (e.Email == email)
                    {
                        user = e;
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return user;
        }

        /// <summary>
        /// Richard Schroeder
        /// Created: 2021/02/19
        ///
        /// Method for updating and storing user passwords for login
        /// cycles through list of test clients and donors to find matching email and old password provided
        /// by user. replaces password hash
        /// </summary>
        public int UpdatePassword(string email, string oldPasswordHash, string newPasswordHash)
        {
            int result = 0;

            try
            {

                //foreach (Employee e in _employees)
                //{
                //    if (e.Email == email && e.PasswordHash == oldPasswordHash)
                //    {
                //        e.PasswordHash = newPasswordHash;
                //        result = 1;
                //    }
                //}

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/02/20
        ///
        /// Deletes an employee role from an employee
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="roleName"></param>
        /// <returns> int 1|0 for T|F </returns>
        public int DeleteEmployeeRole(int employeeID, string roleName)
        {
            int result = 0;
            for (int i = 0; i < _employees.Count; i++)
            {
                // checks for employeeID
                if (_employees[i].EmployeeID == employeeID)
                {
                    foreach (var role in _employees[i].Roles)
                    {
                        if (!_employees[i].Roles.Contains(roleName))
                        {
                            _employees[i].Roles.Remove(roleName);
                            result = 2;//given 2 for debugging for logical errors.
                        }
                    }
                    result = 1;
                }
            }
            return result;
        }

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/02/19
        ///
        /// Inserts a role into an employee that did not already exist.
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="roleName"></param>
        /// <returns> int 1|0 for T|F </returns>
        public int InsertEmployeeRole(int employeeID, string roleName)
        {
            int result = 0;
            // first loop searches the employees for the employeeID
            for (int i = 0; i < _employees.Count; i++)
            {
                // checks for employeeID
                if (_employees[i].EmployeeID == employeeID)
                {
                    // second loop searches through the roles
                    if (!_employees[i].Roles.Contains(roleName))
                    {
                        _employees[i].Roles.Add(roleName);
                        result = 1;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/02/19
        ///
        /// Returns all the roles
        /// </summary>
        /// <returns> list of roles </returns>
        ///
        /// <remarks>
        /// Zach Stultz
        /// Updated: 2021/04/30
        /// Added additional roles.
        /// </remarks>
        public List<string> SelectAllRoles()
        {
            List<string> _roles = new List<string>() { { ("Admin") }, { ("Manager") }, { ("Employee") }, { ("Maintenance") },
            { ("Driver") }, { ("Specialist") }, { ("Item Inspector") }, { ("Inventory Auditor") }, { ("Donor") }, { ("Provider") } };
            return _roles;
        }

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/02/19
        ///
        /// Gets a list of active employees
        /// </summary>
        /// <param name="active"></param>
        /// <returns></returns>
        public List<EmployeeVM> SelectEmployeesByActive(bool active)
        {
            List<EmployeeVM> employees = new List<EmployeeVM>();
            for (int i = 0; i < _employees.Count; i++)
            {
                if (_employees[i].Active == true)
                {
                    var employee = new EmployeeVM()
                    {
                        EmployeeID = _employees[i].EmployeeID,
                        FirstName = _employees[i].FirstName,
                        LastName = _employees[i].LastName,
                        PhoneNumber = _employees[i].PhoneNumber,
                        Email = _employees[i].Email,
                        Active = _employees[i].Active,
                        Roles = _employees[i].Roles
                    };
                    employees.Add(employee);
                }
            }
            return employees;
        }

        /// <summary>
        /// Richard Schroeder
        /// Created: 2021/03/16
        ///
        /// A method inserting a new employee object into the DB
        /// <param name="employee"></param>
        /// </summary>
        public int InsertEmployee(EmployeeVM employee)
        {
            int result = 0;
            try
            {
                _employees.Add(employee);
                foreach (EmployeeVM e in _employees)
                {
                    if (_employees.Contains(employee))
                    {
                        result = 1;
                    }
                    else
                    {
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
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
        public int EditEmployee(EmployeeVM oldEmployee, EmployeeVM newEmployee)
        {
            int result = 0;

            try
            {
                foreach (var e in _employees)
                {
                    if (oldEmployee.EmployeeID == newEmployee.EmployeeID)
                    {
                        oldEmployee.FirstName = newEmployee.FirstName;
                        oldEmployee.LastName = newEmployee.LastName;
                        oldEmployee.Email = newEmployee.Email;
                        oldEmployee.Address = newEmployee.Address;
                        oldEmployee.PhoneNumber = newEmployee.PhoneNumber;
                        oldEmployee.Gender = newEmployee.Gender;
                        result = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        /// <summary>
        /// Richard Schroeder
        /// Created: 2021/03/16
        ///
        /// A method editing an existing employee information
        /// <param name="employeeID"></param>
        /// </summary>
        public int DeactivateEmployee(int employeeID)
        {
            int result = 0;

            try
            {
                foreach (var e in _employees)
                {
                    if (e.EmployeeID == employeeID)
                    {
                        if (e.Active == false)
                        {
                            return result;
                        }
                        else
                        {

                            e.Active = false;
                            result = 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        /// <summary>
        /// Richard Schroeder
        /// Created: 2021/03/16
        ///
        /// A method editing an existing employee information
        /// <param name="employeeID"></param>
        /// </summary>
        public int ReactivateEmployee(int employeeID)
        {
            int result = 0;

            try
            {
                foreach (var e in _employees)
                {
                    if (e.Active == true)
                    {
                        return result;
                    }
                    else
                    {
                        e.Active = true;
                        result = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
        /// <summary>
        /// Jakub Kawski
        /// 2021/04/28
        /// </summary>
        /// <returns></returns>
        public List<EmployeeVM> SelectAllDrivers()
        {
            //In the real accessor a join table is used to find any employees that have
            //their employeeID in the driver license table
            return _employees;
        }
    }
}
