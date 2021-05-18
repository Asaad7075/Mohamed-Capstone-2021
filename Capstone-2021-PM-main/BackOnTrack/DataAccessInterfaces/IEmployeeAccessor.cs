using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    /// <summary>
    /// Chantal Shirley
    /// Created: 2021/02/21
    /// 
    /// Interface for employee accessor class.
    /// </summary>
    public interface IEmployeeAccessor
    {
        EmployeeVM SelectEmployeeByEmployeeID(int ID);
        List<string> SelectEmployeeRolesByEmployeeID(int iD);
		int UpdatePassword(string email, string oldPasswordHash, string newPasswordHash);

        int AuthenticateUser(string username, string passwordHash);

        Employee SelectEmployeeByEmail(string email);

        List<string> SelectAllRoles();
        List<EmployeeVM> SelectEmployeesByActive(bool active);
        int InsertEmployeeRole(int employeeID, string roleName);
        int DeleteEmployeeRole(int employeeID, string roleName);

        int InsertEmployee(EmployeeVM employee);
        int EditEmployee(EmployeeVM oldEmployee, EmployeeVM newEmployee);

        int DeactivateEmployee(int employeeID);
        int ReactivateEmployee(int employeeID);

        List<EmployeeVM> SelectAllDrivers();

    }
}
