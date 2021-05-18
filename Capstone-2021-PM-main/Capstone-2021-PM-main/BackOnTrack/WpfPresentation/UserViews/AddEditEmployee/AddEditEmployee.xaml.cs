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

namespace WpfPresentation.UserViews.AddEditEmployee
{
    /// <summary>
    /// Interaction logic for AddEditEmployee.xaml
    /// </summary>
    public partial class AddEditEmployee : Page
    {
        //private IEmployeeManager _employeeManager = new EmployeeManager(new EmployeeFake()); // manager to use fakes
        private IEmployeeManager _employeeManager = new EmployeeManager(); // manager to use DB data

        private string pageName = "Add/Edit Employee";
        public string PageName { get { return pageName; } }
        public AddEditEmployee()
        {
            InitializeComponent();
        }

        private void RefreshEmployeeList()
        {
            dgAddEmployeeReadOnly.ItemsSource = _employeeManager.RetrieveEmployeesByActive(true);
            dgAddEmployeeReadOnly.Columns[0].Visibility = Visibility.Hidden;
            dgAddEmployeeReadOnly.Columns[1].Visibility = Visibility.Hidden;
            chkShowActive.IsChecked = false;
        }

        private void FillEmployeeInfo(EmployeeVM selectedItem)
        {
            if (selectedItem.Active == true)
            {
                btnEditActiveStatus.Content = "Deactivate";
            }

            if (selectedItem.Active == false)
            {
                btnEditActiveStatus.Content = "Activate";
            }

            txtEmployeeFirstName.Text = selectedItem.FirstName;
            txtEmployeeLastName.Text = selectedItem.LastName;
            txtEmployeeEmail.Text = selectedItem.Email;
            txtEmployeePhoneNumber.Text = selectedItem.PhoneNumber;
            txtEmployeeAddress.Text = selectedItem.Address;
            txtEmployeeGender.Text = selectedItem.Gender;
        }
        private void ResetPage()
        {
            txtEmployeeFirstName.Text = "";
            txtEmployeeLastName.Text = "";
            txtEmployeeEmail.Text = "";
            txtEmployeePhoneNumber.Text = "";
            txtEmployeeAddress.Text = "";
            txtEmployeeGender.Text = "";
            btnEditSaveEmployee.Content = "Edit";
            btnAddEmployee.IsEnabled = true;
            btnEditActiveStatus.IsEnabled = false;
            txtEmployeeAddress.IsReadOnly = true;
            txtEmployeeFirstName.IsReadOnly = true;
            txtEmployeeEmail.IsReadOnly = true;
            txtEmployeeGender.IsReadOnly = true;
            txtEmployeeLastName.IsReadOnly = true;
            txtEmployeePhoneNumber.IsReadOnly = true;
            btnCancelEdit.IsEnabled = false;
            MakeTextFieldsEditable();
        }

        private void MakeTextFieldsEditable()
        {
            txtEmployeeAddress.IsReadOnly = false;
            txtEmployeeFirstName.IsReadOnly = false;
            txtEmployeeEmail.IsReadOnly = false;
            txtEmployeeGender.IsReadOnly = false;
            txtEmployeeLastName.IsReadOnly = false;
            txtEmployeePhoneNumber.IsReadOnly = false;
        }

        private void MakeTextFieldsUneditable()
        {
            txtEmployeeAddress.IsReadOnly = true;
            txtEmployeeFirstName.IsReadOnly = true;
            txtEmployeeEmail.IsReadOnly = true;
            txtEmployeeGender.IsReadOnly = true;
            txtEmployeeLastName.IsReadOnly = true;
            txtEmployeePhoneNumber.IsReadOnly = true;
        }



        private void btnAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtEmployeeFirstName.Text == "" ||
                    txtEmployeeLastName.Text == "" ||
                    txtEmployeeEmail.Text == "" ||
                    txtEmployeePhoneNumber.Text == "" ||
                    txtEmployeeAddress.Text == "" ||
                    txtEmployeeGender.Text == "")
                {
                    MessageBox.Show("Please fill out all fields.");
                    return;
                }
                else
                {
                    EmployeeVM employee = new EmployeeVM()
                    {
                        //EmployeeID = 123456, //included for fake testing, SQL Identity, will be commented out for production
                        FirstName = txtEmployeeFirstName.Text,
                        LastName = txtEmployeeLastName.Text,
                        PhoneNumber = txtEmployeePhoneNumber.Text,
                        Email = txtEmployeeEmail.Text,
                        Address = txtEmployeeAddress.Text,
                        Gender = txtEmployeeGender.Text,
                        Roles = null,
                        //Active = true //included for fake testing, SQL 1 by default, will be commented out for production
                    };
                    _employeeManager.InsertEmployee(employee);
                    MessageBox.Show("Employee added!");
                    RefreshEmployeeList();
                    ResetPage();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Employee insert failed." + ex.InnerException.Message);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshEmployeeList();
        }

        private void btnEditSaveEmployee_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = (EmployeeVM)dgAddEmployeeReadOnly.SelectedItem;
            if (selectedItem == null)
            {
                MessageBox.Show("Must select an item to edit.");
                return;
            }


            // prepare item for edit
            if ((string)btnEditSaveEmployee.Content == "Edit")
            {
                btnEditSaveEmployee.Content = "Save";
                FillEmployeeInfo(selectedItem);
                btnAddEmployee.IsEnabled = false; // disable add whilst editing
                btnEditActiveStatus.IsEnabled = true; // enable to activate/deactivate
                btnCancelEdit.IsEnabled = true; // allow cancels
                MakeTextFieldsEditable();
                return; // break out to incur "Save" check 
            }

            if ((string)btnEditSaveEmployee.Content == "Save")
            {
                try
                {
                    EmployeeVM newEmployeeInfo = new EmployeeVM()
                    {
                        EmployeeID = selectedItem.EmployeeID,
                        FirstName = txtEmployeeFirstName.Text,
                        LastName = txtEmployeeLastName.Text,
                        Email = txtEmployeeEmail.Text,
                        PhoneNumber = txtEmployeePhoneNumber.Text,
                        Address = txtEmployeeAddress.Text,
                        Gender = txtEmployeeGender.Text
                    };
                    _employeeManager.EditEmployee(selectedItem, newEmployeeInfo);
                    MessageBox.Show("Employee updated!");

                    
                    ResetPage();
                    RefreshEmployeeList();
                    return; // break out to prep for edit
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Update failed. " + ex.InnerException.Message);
                }
            }
        }

        private void dgAddEmployeeReadOnly_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = (EmployeeVM)dgAddEmployeeReadOnly.SelectedItem;
            FillEmployeeInfo(selectedItem);
            MakeTextFieldsUneditable();
            btnCancelEdit.IsEnabled = true;
            btnAddEmployee.IsEnabled = false;
        }

        private void btnEditActiveStatus_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = (EmployeeVM)dgAddEmployeeReadOnly.SelectedItem;
            if ((string)btnEditActiveStatus.Content == "Activate")
            {
                _employeeManager.ReactivateEmployee(selectedItem.EmployeeID);
                MessageBox.Show("Employee reactivated.");
                ResetPage();
                RefreshEmployeeList();
                MakeTextFieldsEditable();
            }

            if ((string)btnEditActiveStatus.Content == "Deactivate")
            {
                _employeeManager.DeactivateEmployee(selectedItem.EmployeeID);
                MessageBox.Show("Employee deactivated.");
                ResetPage();
                RefreshEmployeeList();
                MakeTextFieldsEditable();
            }
        }

        private void chkShowActive_Click(object sender, RoutedEventArgs e)
        {
            var employeeManager = new EmployeeManager();

            dgAddEmployeeReadOnly.ItemsSource = employeeManager.RetrieveEmployeesByActive(!(bool)chkShowActive.IsChecked);

            dgAddEmployeeReadOnly.Columns[0].Visibility = Visibility.Hidden;
            dgAddEmployeeReadOnly.Columns[1].Visibility = Visibility.Hidden;

        }

        private void btnCancelEdit_Click(object sender, RoutedEventArgs e)
        {
            ResetPage();
        }
    }
}
