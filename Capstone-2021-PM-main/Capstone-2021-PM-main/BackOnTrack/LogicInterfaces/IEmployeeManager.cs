using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DomainModels;

namespace LogicInterfaces
{
    public interface IEmployeeManager
    {
        Employee AuthenticateUser(string username, string password);
        bool UpdatePassword(string email, string oldPasswordHash, string newPasswordHash);
        List<string> RetrieveAllRoles();
        List<EmployeeVM> RetrieveEmployeesByActive(bool active);
        bool EditEmployeeRole(EmployeeVM oldEmployee, EmployeeVM newEmployee);
        List<string> RetrieveRolesByEmployeeID(int employeeID);

        bool InsertEmployee(EmployeeVM employee);
        bool EditEmployee(EmployeeVM oldEmployee, EmployeeVM newEmployee);

        bool DeactivateEmployee(int employeeID);
        bool ReactivateEmployee(int employeeID);
        List<EmployeeVM> RetrieveAllDrivers();
    }
}
