using DataAccessFakes;
using DataAccessInterfaces;
using DomainModels;
using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicTests
{

    /// <summary>
    /// Richard Schroeder
    /// Created: 2021/02/19
    ///
    /// User test class to verify functionality of all public methods
    /// for user data objects
    /// </summary>
    [TestClass]
    public class EmployeeManagerTests
    {
        private IEmployeeAccessor _employeeAccessor;
        private Employee _employee;
        private EmployeeFake _employeeFake;

        [TestInitialize]
        public void TestSetup()
        {
            _employeeAccessor = new EmployeeFake();
        }


        /// <summary>
        /// Richard Schroeder
        /// Created: 2021/02/19
        ///
        /// Test to validate when a user enters a good password but bad email
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestUserChangePasswordBadEmailGoodOldPassword()
        {
            // Arrange
            EmployeeManager userManager = new EmployeeManager(_employeeAccessor);

            const string invalidEmail = "bademail@backontrack.com";
            const string validOldPassword = "newuser";
            const string newPassword = "password";

            // Act & Assert
            userManager.UpdatePassword(invalidEmail, validOldPassword, newPassword);


        }

        /// <summary>
        /// Richard Schroeder
        /// Created: 2021/02/19
        ///
        /// Test to validate when a user enters a bad password but good email
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestUserChangePasswordGoodEmailBadOldPassword()
        {
            // Arrange
            EmployeeManager userManager = new EmployeeManager(_employeeAccessor);

            const string validEmail = "wibblywobbly@backontrack.com";
            const string invalidOldPassword = "badpassword";
            const string newPassword = "password";

            // Act & Assert
            userManager.UpdatePassword(validEmail, invalidOldPassword, newPassword);

        }

        /// <summary>
        /// Richard Schroeder
        /// Created: 2021/02/25
        ///
        /// Test to return expected value from the SelectUserByEmail() method
        ///
        /// Test for Employee extends User objects
        ///
        /// </summary>
        [TestMethod]
        public void TestSelectUserByEmailEmployee()
        {
            // Arrange
            EmployeeManager userManager = new EmployeeManager(_employeeAccessor);

            const string validEmail = "wibblywobbly@backontrack.com";

            // Act
            Employee actual = (Employee)_employeeAccessor.SelectEmployeeByEmail(validEmail);
            Employee expected = new Employee
            {
                EmployeeID = 100000,
                FirstName = "The",
                LastName = "Doctor",
                PhoneNumber = "1335048955",
                Email = "wibblywobbly@backontrack.com",
                //PasswordHash = "9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e",
                Active = false,
                Address = "352 Dola Mine Road, Morrisville, NC, 27560",
                Gender = "Time Lord"
            };

            // Assert
            //comparing unique client ID's, cannot compare explicit objects without FluentAssertions NuGet Package
            Assert.AreEqual(actual.EmployeeID, expected.EmployeeID);
        }

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/02/19
        ///
        /// Test for retriving all the roles.
        /// </summary>
        ///
        /// <remarks>
        /// Zach Stultz
        /// Updated: 2021/04/30
        /// Remarks: Changed expectedRolesCount to 10
        /// since additional roles were added.
        /// </remarks>
        [TestMethod]
        public void TestRetriveAllRolesReturnsAllRoles()
        {
            // arrange
            EmployeeManager employeeManager = new EmployeeManager(_employeeAccessor);
            int expectedRolesCount = 10;
            int actualRolesCount;

            //act
            List<string> _roles = employeeManager.RetrieveAllRoles();
            actualRolesCount = _roles.Count;

            //assert
            Assert.AreEqual(expectedRolesCount, actualRolesCount);
        }

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/02/19
        ///
        /// Test for retiveing the rolled by the employeeID
        /// </summary>
        [TestMethod]
        public void TestRetriveEmployeeRolesByEmployeeID()
        {
            // arrange
            EmployeeManager employeeManager = new EmployeeManager(_employeeAccessor);
            int employeeID = 100000;
            int expectedRolesCount = 1;
            int actualRolesCount;

            // act
            List<string> _roles = employeeManager.RetrieveRolesByEmployeeID(employeeID); //_employeeAccessor.SelectEmployeeRolesByEmployeeID(employeeID);

            actualRolesCount = _roles.Count;

            //assert
            Assert.AreEqual(expectedRolesCount, actualRolesCount);
        }

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/02/20
        ///
        /// Tests the edit empoloyee role was good
        /// </summary>
        [TestMethod]
        public void TestEditEmployeeRoleGood()
        {
            //arrange
            EmployeeManager employeeManager = new EmployeeManager(_employeeAccessor);
            EmployeeVM oldEmployee = (new EmployeeVM()
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
            EmployeeVM newEmployee = (new EmployeeVM()
            {
                EmployeeID = 185215,
                FirstName = "Janet",
                LastName = "Weiss",
                PhoneNumber = "832-123-2938",
                Email = "JanetW@company.com",
                Active = true,
                Roles = new List<string>()
                {
                    "Logistics Maintenance"
                }
            });
            bool actualEditResult;
            bool expectedEditResult = true;

            //act
            actualEditResult = employeeManager.EditEmployeeRole(oldEmployee, newEmployee);

            //assert
            Assert.AreEqual(expectedEditResult, actualEditResult);
        }

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/02/28
        ///
        /// Tests retrieve employees by active is good
        /// </summary>
        ///
        /// <remarks>
        /// Zach Stultz
        /// Updated: 2021/04/30
        /// Remarks: Changed expectedResult to 17 because
        /// additional roles were added.
        /// </remarks>
        [TestMethod]
        public void TestRetrieveEmployeesByActiveGood()
        {
            //arrange
            EmployeeManager employeeManager = new EmployeeManager(_employeeAccessor);
            int expectedResult = 17;
            int actualResult;

            //act
            actualResult = employeeManager.RetrieveEmployeesByActive(true).Count;

            //assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// Richard Schroeder
        /// Created: 2021/03/19
        ///
        /// Test to insert new employee into the _employees fake list
        ///
        /// </summary>
        [TestMethod]
        public void TestInsertNewEmployee()
        {
            // Arrange
            EmployeeManager employeeManager = new EmployeeManager(_employeeAccessor);
            bool expected = true;
            EmployeeVM newEmployee = new EmployeeVM()
            {
                EmployeeID = 100001,
                FirstName = "River",
                LastName = "Song",
                PhoneNumber = "1135840752",
                Email = "spoilers@backontrack.com",
                Active = true,
                Address = "3270 Coolidge Street, Eureka Rural, MA, 59917",
                Gender = "Female",
                Roles = new List<string>()
                {
                    "Logistics Maintenance"
                }
            };

            // Act
            bool actual = employeeManager.InsertEmployee(newEmployee);
            // Assert

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Richard Schroeder
        /// Created: 2021/03/19
        ///
        /// Test to edit an existing employee andreplace with
        /// new employee info
        ///
        /// </summary>
        [TestMethod]
        public void TestEditOldEmployee()
        {
            // Arrange
            EmployeeManager employeeManager = new EmployeeManager(_employeeAccessor);
            bool expected = true;
            EmployeeVM oldEmployee = new EmployeeVM()
            {
                EmployeeID = 100001,
                FirstName = "River",
                LastName = "Song",
                PhoneNumber = "1135840752",
                Email = "spoilers@backontrack.com",
                Active = true,
                Address = "3270 Coolidge Street, Eureka Rural, MA, 59917",
                Gender = "Female"
            };

            EmployeeVM newEmployee = new EmployeeVM()
            {
                EmployeeID = 100001,
                FirstName = "River",
                LastName = "Song",
                PhoneNumber = "321654987", // new number
                Email = "spoilers@backontrack.com",
                Active = true,
                Address = "3270 Coolidge Street, Eureka Rural, MA, 59917",
                Gender = "Female"
            };

            // Act
            bool actual = employeeManager.EditEmployee(oldEmployee, newEmployee);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Richard Schroeder
        /// Created: 2021/03/19
        ///
        /// Test to deactivate an employee
        ///
        /// </summary>
        [TestMethod]
        public void TestDeactivateEmployee()
        {
            // Arrange
            EmployeeManager employeeManager = new EmployeeManager(_employeeAccessor);
            bool expected = false;


            // Act
            bool actual = employeeManager.DeactivateEmployee(185215);

            // Assert
            Assert.AreEqual(expected, actual);

        }

        /// <summary>
        /// Richard Schroeder
        /// Created: 2021/03/19
        ///
        /// Test to reactivate an employee
        ///
        /// </summary>
        [TestMethod]
        public void TestReactivateEmployee()
        {
            // Arrange
            EmployeeManager employeeManager = new EmployeeManager(_employeeAccessor);
            bool expected = false;


            // Act
            bool actual = employeeManager.ReactivateEmployee(185215);

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
