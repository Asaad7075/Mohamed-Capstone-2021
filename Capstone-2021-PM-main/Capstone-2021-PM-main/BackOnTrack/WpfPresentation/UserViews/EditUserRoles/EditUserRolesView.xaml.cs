using DataAccessFakes;
using DomainModels;
using LogicInterfaces;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfPresentation
{
    /// <summary>
    /// Interaction logic for EditUserRoles.xaml
    /// </summary>
    public partial class pageEditUserRolesView : Page
    {
        private EmployeeVM _employee; // hold the unedited employee data

        //private IEmployeeManager _employeeManager = new EmployeeManager(new EmployeeFake());
        private IEmployeeManager _employeeManager = new EmployeeManager();

        private List<string> _assignedRoles; // edited assigned roles
        private List<string> _unassignedRoles; // edited unassigned roles
        private List<string> _originalUnassignedRoles = new List<string>();
        private string pageName = "Apply/Unapply Employee Role";

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/03/09
        /// 
        /// This property is used for navigation bindings.
        /// </summary>
        public string PageName
        {
            get
            {
                return pageName;
            }
        }

        public pageEditUserRolesView()
        {
            InitializeComponent();
            employeesRolesToBeEdited();
        }

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/02/28
        /// 
        /// Displays the list of employees
        /// </summary>
        private void employeesRolesToBeEdited()
        {
            btnSaveEmployeeRole.Content = "Select";
            btnCancelEmployeeRole.Content = "Cancel";
            dgEditEmployeeRole.Visibility = Visibility.Visible;
            lstUnassignedEmployeeRole.Visibility = Visibility.Hidden;
            lstAssignedEmployeeRole.Visibility = Visibility.Hidden;
            try
            {
                if (dgEditEmployeeRole.ItemsSource == null)
                {
                    var empMgr = _employeeManager;
                    dgEditEmployeeRole.ItemsSource = empMgr.RetrieveEmployeesByActive(true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/02/28
        /// 
        /// Sets up the roles of the employees in the roles lists and allows for dynamic changes.
        /// </summary>
        private void setupRoles(EmployeeVM employee)
        {
            _employee = new EmployeeVM();
            _employee = employee;
            lstAssignedEmployeeRole.Visibility = Visibility.Visible;
            lstUnassignedEmployeeRole.Visibility = Visibility.Visible;
            dgEditEmployeeRole.Visibility = Visibility.Hidden;
            btnSaveEmployeeRole.Content = "Save";
            btnCancelEmployeeRole.Content = "Back";
            lblEditEmployeeRoleEmployee.Content = _employee.FirstName + " " + _employee.LastName + ",  ID:" + _employee.EmployeeID;

            try
            {
                // populate the _assignedRoles and save them
                _assignedRoles =
                    _employeeManager.RetrieveRolesByEmployeeID(_employee.EmployeeID);

                // make a copy of the _assignedRoles that will be preserved
                _employee.Roles = new List<string>();
                foreach (var r in _assignedRoles)
                {
                    _employee.Roles.Add(r); // holds the original state
                }

                // use the dynamically edited _assignedRoles as the list data source
                lstAssignedEmployeeRole.ItemsSource = _assignedRoles;

                // create the dynamic list of _unassigned roles
                _unassignedRoles = _employeeManager.RetrieveAllRoles();
                foreach (var role in _assignedRoles)
                {
                    _unassignedRoles.Remove(role);
                }

                // make a copy of _unassigned roles to preserve their original state
                foreach (var r in _unassignedRoles)
                {
                    _originalUnassignedRoles.Add(r);
                }

                // use the dynamic _unassignedRoles as the list items source
                lstUnassignedEmployeeRole.ItemsSource = _unassignedRoles;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n"
                    + ex.InnerException.Message);
            }

        }

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/03/05
        /// 
        /// Event handler for when an employee is double clicked. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgEditEmployeeRole_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            editSelectedEmployeeRole();
        }

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/03/05
        /// 
        /// Confirms an employee was selected and calls the method to set up the roles.
        /// </summary>
        private void editSelectedEmployeeRole()
        {
            EmployeeVM employee = (EmployeeVM)dgEditEmployeeRole.SelectedItem;
            if (employee == null)
            {
                MessageBox.Show("You need to select an employee to edit the role.",
                    "Edit Role Operation Not Available", MessageBoxButton.OK,
                    MessageBoxImage.Information);
                return;
            }
            setupRoles(employee);
        }

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/03/05
        /// 
        /// Event handler for when an assigned role is double clicked, this sends the assigned role to the unassigned list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstAssignedEmployeeRole_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedRole = lstAssignedEmployeeRole.SelectedItem;
            if (selectedRole == null)
            {
                return;
            }
            _assignedRoles.Remove((string)selectedRole);
            _unassignedRoles.Add((string)selectedRole);
            lstUnassignedEmployeeRole.ItemsSource = null;
            lstAssignedEmployeeRole.ItemsSource = null;
            lstAssignedEmployeeRole.ItemsSource = _assignedRoles;
            lstUnassignedEmployeeRole.ItemsSource = _unassignedRoles;
        }

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/03/05
        /// 
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstUnassignedEmployeeRole_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedRole = lstUnassignedEmployeeRole.SelectedItem;
            if (selectedRole == null)
            {
                return;
            }
            _unassignedRoles.Remove((string)selectedRole);
            _assignedRoles.Add((string)selectedRole);
            lstUnassignedEmployeeRole.ItemsSource = null;
            lstAssignedEmployeeRole.ItemsSource = null;
            lstAssignedEmployeeRole.ItemsSource = _assignedRoles;
            lstUnassignedEmployeeRole.ItemsSource = _unassignedRoles;
        }

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/03/05
        /// 
        /// Button handler if "Select" goes to selecting an employee if "save" employee roles are saved into their 
        /// respected columns
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveEmployeeRole_Click(object sender, RoutedEventArgs e)
        {
            if (btnSaveEmployeeRole.Content.Equals("Select"))
            {
                editSelectedEmployeeRole();
            }
            else if (btnSaveEmployeeRole.Content.Equals("Save"))
            {
                try
                {
                    var newEmployee = new EmployeeVM()
                    {
                        EmployeeID = _employee.EmployeeID,
                        Roles = _assignedRoles
                    };
                    _employeeManager.EditEmployeeRole(_employee, newEmployee);
                    employeesRolesToBeEdited();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                }
                MessageBox.Show("Success! " + _employee.FirstName + " " + _employee.LastName + " Successfully Updated" );
            }
        }

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/03/05
        /// 
        /// Cancle button will go back to prior operation if "Back" and will close the window if contense is "Cancle"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelEmployeeRole_Click(object sender, RoutedEventArgs e)
        {
            if ((string)btnCancelEmployeeRole.Content == "Back")
            {
                employeesRolesToBeEdited();
            }
            else if ((string)btnCancelEmployeeRole.Content == "Cancel")
            {
                this.NavigationService.Navigate(null);
            }
        }

    }
}
