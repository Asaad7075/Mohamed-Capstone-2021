using DataAccessInterfaces;
using DomainModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class EmployeeAccessor : IEmployeeAccessor
    {
        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/02/21
        /// 
        /// Method for retrieving an Employee based on their
        /// employee ID.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public EmployeeVM SelectEmployeeByEmployeeID(int ID)
        {
            EmployeeVM employeeVM = null;

            var conn = DBConnection.GetDBConnection(); // connection 
            var cmd = new SqlCommand("sp_select_employee_by_id", conn);
            // parameters
            cmd.Parameters.AddWithValue("@EmployeeID", ID);

            cmd.CommandType = CommandType.StoredProcedure;

            try // execute
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        employeeVM = new EmployeeVM
                        {
                            EmployeeID = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            PhoneNumber = reader.GetString(3),
                            Email = reader.GetString(4),
                            Active = reader.GetBoolean(5),
                            Address = reader.GetString(6),
                            Gender = reader.GetString(7)
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return employeeVM;
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2/24/20
        /// 
        /// Method for retrieving employee roles by
        /// employee ID.
        /// </summary>
        /// <param name="iD"></param>
        /// <returns></returns>
        public List<string> SelectEmployeeRolesByEmployeeID(int ID)
        {
            List<string> roles = new List<string>();

            // get a connection
            var conn = DBConnection.GetDBConnection();

            // command
            var cmd = new SqlCommand("sp_select_employeeroles_by_employeeid", conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@EmployeeID", SqlDbType.Int);

            // values
            cmd.Parameters["@EmployeeID"].Value = ID;

            // execute the command
            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        roles.Add(reader.GetString(0));
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return roles;
        }
		
		/// <summary>
        /// Richard Schroeder
        /// Created: 2021/02/19
        /// 
        /// Method for checking for valid user email and password
        /// </summary>
        public int AuthenticateUser(string email, string passwordHash)
        {
            int result = 0; // once authenticated, this will be 1

            // we need a connection object
            var conn = DBConnection.GetDBConnection();

            // next, we need a command object
            var cmd = new SqlCommand("sp_Authenticate_User", conn);

            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;

            // set up the command parameters
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar, 100);

            // set the parameter values
            cmd.Parameters["@Email"].Value = email;
            cmd.Parameters["@PasswordHash"].Value = passwordHash;

            // now that the command is set up, we need to execute it
            // all database code is unsafe, so it goes in a try block

            try
            {
                // open the connection
                conn.Open();

                // execute the command and capture the result
                result = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        /// <summary>
        /// Richard Schroeder
        /// Created: 2021/02/19
        /// 
        /// Method for selecting an employee by their email
        /// </summary>
        public Employee SelectEmployeeByEmail(string employeeEmail)
        {
            Employee employee = null;

            // get a connection
            var conn = DBConnection.GetDBConnection();

            // get a command
            var cmd = new SqlCommand("sp_Select_User_By_Email", conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);

            // values
            cmd.Parameters["@Email"].Value = employeeEmail;

            // execute the command
            try
            {
                // open the connection
                conn.Open();

                // execute the command
                var reader = cmd.ExecuteReader();

                // process the results
                if (reader.HasRows)
                {
                    reader.Read();
                    var employeeID = reader.GetInt32(0);
                    var firstName = reader.GetString(1);
                    var lastName = reader.GetString(2);
                    var phoneNumber = reader.GetString(3);
                    var email = reader.GetString(4);
                    var active = reader.GetBoolean(5);
                    var address = reader.GetString(6);
                    var gender = reader.GetString(7);
                    reader.Close();

                    // get the user roles, if any
                    //IEmployeeAccessor employeeAccessor = new EmployeeAccessor();
                    //var roles = employeeAccessor.SelectRolesByEmployeeID(userID);

                    //employee = new Employee(userID, firstName, lastName, phoneNumber, email,
                    //     active, roles);

                    employee = new Employee(employeeID, firstName, lastName, phoneNumber, email,
                         active, address, gender);
                }
                else
                {
                    throw new ApplicationException("User not found.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return employee;
        }

        /// <summary>
        /// Richard Schroeder
        /// Created: 2021/02/19
        /// 
        /// Method for changing a users password
        /// </summary>

        public int UpdatePassword(string email, string oldPasswordHash, string newPasswordHash)
        {
            int result = 0;

            // connection
            var conn = DBConnection.GetDBConnection();
            // command
            var cmd = new SqlCommand("sp_Update_PasswordHash", conn);
            // command type
            cmd.CommandType = CommandType.StoredProcedure;
            // parameters
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@OldPasswordHash", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@NewPasswordHash", SqlDbType.NVarChar, 100);
            // values
            cmd.Parameters["@Email"].Value = email;
            cmd.Parameters["@OldPasswordHash"].Value = oldPasswordHash;
            cmd.Parameters["@NewPasswordHash"].Value = newPasswordHash;

            // run the command
            try
            {
                conn.Open();

                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/02/19
        /// 
        /// Gets all the Roles in the database and returns it as a list
        /// </summary>
        /// <returns> List<string> Roles </returns>
        public List<string> SelectAllRoles()
        {
            List<string> roles = new List<string>();
            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_Select_All_Roles", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        roles.Add(reader.GetString(0));
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return roles;
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

            // connection
            var conn = DBConnection.GetDBConnection();
            // command
            var cmd = new SqlCommand("sp_select_employees_by_active", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            // parameters short cut form
            cmd.Parameters.AddWithValue("@Active", active);

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var employee = new EmployeeVM()
                        {
                            EmployeeID = reader.GetInt32(0),
                            Email = reader.GetString(1),
                            FirstName = reader.GetString(2),
                            LastName = reader.GetString(3),
                            PhoneNumber = reader.GetString(4),
                            Active = reader.GetBoolean(5),
                            Address = reader.GetString(6),
                            Gender = reader.GetString(7)
                            //Roles = null
                        };
                        employees.Add(employee);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return employees;
        }

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/02/19
        /// 
        /// Method delets the employee role from the database.
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="roleName"></param>
        /// <returns> int 1|0 for successful or not </returns>
        public int DeleteEmployeeRole(int employeeID, string roleName)
        {
            int result = 0;
            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_remove_employee_role", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
            cmd.Parameters.Add("@RoleName", SqlDbType.NVarChar, 50);
            cmd.Parameters["@RoleName"].Value = roleName;
            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/02/19
        /// 
        /// inserts an employee role into the database.
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="roleName"></param>
        /// <returns> int 1|0 for successful or not </returns>
        public int InsertEmployeeRole(int employeeID, string roleName)
        {
            int result = 0;
            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_add_employee_role", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
            cmd.Parameters.Add("@RoleName", SqlDbType.NVarChar, 50);
            cmd.Parameters["@RoleName"].Value = roleName;
            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
                if (result == 0)
                {
                    throw new ApplicationException("The role could not be added.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return result;
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

            // connection
            var conn = DBConnection.GetDBConnection();
            // command
            var cmd = new SqlCommand("sp_Create_Employee", conn);
            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar, 11);
            cmd.Parameters.Add("@Gender", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@Address", SqlDbType.NVarChar, 100);

            cmd.Parameters["@Email"].Value = employee.Email;
            cmd.Parameters["@FirstName"].Value = employee.FirstName;
            cmd.Parameters["@LastName"].Value = employee.LastName;
            cmd.Parameters["@PhoneNumber"].Value = employee.PhoneNumber;
            cmd.Parameters["@Gender"].Value = employee.Gender;
            cmd.Parameters["@Address"].Value = employee.Address;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
                if (result == 0)
                {
                    throw new ApplicationException("Employee could not be added.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
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

            // connection
            var conn = DBConnection.GetDBConnection();
            // command
            var cmd = new SqlCommand("sp_update_employee_profile_data", conn);
            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@EmployeeID", SqlDbType.Int);

            cmd.Parameters.Add("@OldEmail", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@OldFirstName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@OldLastName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@OldPhoneNumber", SqlDbType.NVarChar, 11);
            cmd.Parameters.Add("@OldGender", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@OldAddress", SqlDbType.NVarChar, 100);

            cmd.Parameters.Add("@NewEmail", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@NewFirstName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@NewLastName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@NewPhoneNumber", SqlDbType.NVarChar, 11);
            cmd.Parameters.Add("@NewGender", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@NewAddress", SqlDbType.NVarChar, 100);

            cmd.Parameters["@EmployeeID"].Value = oldEmployee.EmployeeID;

            cmd.Parameters["@OldEmail"].Value = oldEmployee.Email;
            cmd.Parameters["@OldFirstName"].Value = oldEmployee.FirstName;
            cmd.Parameters["@OldLastName"].Value = oldEmployee.LastName;
            cmd.Parameters["@OldPhoneNumber"].Value = oldEmployee.PhoneNumber;
            cmd.Parameters["@OldGender"].Value = oldEmployee.Gender;
            cmd.Parameters["@OldAddress"].Value = oldEmployee.Address;

            cmd.Parameters["@NewEmail"].Value = newEmployee.Email;
            cmd.Parameters["@NewFirstName"].Value = newEmployee.FirstName;
            cmd.Parameters["@NewLastName"].Value = newEmployee.LastName;
            cmd.Parameters["@NewPhoneNumber"].Value = newEmployee.PhoneNumber;
            cmd.Parameters["@NewGender"].Value = newEmployee.Gender;
            cmd.Parameters["@NewAddress"].Value = newEmployee.Address;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
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
        public int DeactivateEmployee(int employeeID)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_deactivate_employee", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EmployeeID", employeeID);

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
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
        public int ReactivateEmployee(int employeeID)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_reactivate_employee", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EmployeeID", employeeID);

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
        /// <summary>
        /// Jakub Kawski
        /// 2021/04/24
        /// </summary>
        /// <returns></returns>
        public List<EmployeeVM> SelectAllDrivers()
        {
            // sp_select_all_drivers
            List<EmployeeVM> employees = new List<EmployeeVM>();

            // connection
            var conn = DBConnection.GetDBConnection();
            // command
            var cmd = new SqlCommand("sp_select_all_drivers", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            // parameters short cut form

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var employee = new EmployeeVM()
                        {
                            EmployeeID = reader.GetInt32(0),
                            Email = reader.GetString(1),
                            FirstName = reader.GetString(2),
                            LastName = reader.GetString(3),
                            PhoneNumber = reader.GetString(4),
                            Active = reader.GetBoolean(5),
                            Address = reader.GetString(6),
                            Gender = reader.GetString(7)
                        };
                        employees.Add(employee);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return employees;
        }
    }
}
