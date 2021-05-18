using DataAccessFakes;
using DomainModels;
using LogicInterfaces;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfPresentation.InventoryManagementViews;
using WpfPresentation.LogisticsViews.DriversLicense;
using WpfPresentation.LogisticsViews.Route;
using WpfPresentation.LogisticsViews.Tickets;
using WpfPresentation.LogisticsViews.Vehicle;
using WpfPresentation.MaterialHandlingView;
using WpfPresentation.ServiceListView;
using WpfPresentation.SupplyManagementViews.AddEditSupplyItem;
using WpfPresentation.UserViews.AddEditEmployee;
using WpfPresentation.ZipCodeViews;

namespace WpfPresentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private IEmployeeManager _userManager = new EmployeeManager(new EmployeeFake());
        private IEmployeeManager _userManager = new EmployeeManager();
        private Employee _user;
        public Employee User { get { return _user; } }


        public MainWindow()
        {
            InitializeComponent();
            //this.DataContext = this; // this has been moved to HideLoginShowUI() to load only after login to pass active user :Nate Hepker

        }
        /// <summary>
        /// Jakub Kawski
        /// 2021/03/08
        ///
        /// Quick login in for debug and testing
        /// Comment out/in in the Window_Loaded method to enable/disable
        /// </summary>
        private void AUTOLOGIN()
        {
            _user = _userManager.AuthenticateUser("brad@backontrack.com", "password");
            DisplayWelcomeTab();
            HideLoginShowUI();
        }

        /// <summary>
        /// Richard Schroeder
        /// Created: 2021/03/24
        /// 
        /// Initially hide all tabs, except Welcome
        /// can be appended as new tabs are added
        /// </summary>
        private void HideTabs()
        {
            tabItemMaterialHandling.Visibility = Visibility.Hidden;
            tabItemEmployeeManagement.Visibility = Visibility.Hidden;
            tabItemInventoryManagement.Visibility = Visibility.Hidden;
            tabItemLogistics.Visibility = Visibility.Hidden;
            tabItemServiceProvision.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Richard Schroeder
        /// Created: 2021/03/24
        /// 
        /// Method to hide/show tabs based on users role
        /// can be appended when new roles are explicitly added
        /// </summary>
		///
        /// <remarks>
        /// Zach Stultz
        /// Updated: 2021/04/30
        /// Re-organized the cases by group and
        /// added additional roles.
        /// </remarks>
      private void ShowRoleTabs(Employee _employee)
        {
            //get roles of passed Employee object and put them in a list
            List<string> roles = _userManager.RetrieveRolesByEmployeeID(_employee.EmployeeID);
            if (roles.Count == 0)
            {
                MessageBox.Show("User is missing a role(s).");
            }
            else
            {
                // cycle through list and show tabs based on roles
                foreach (var r in roles)
                {
                    switch (r)
                    {
                        // General
                        case "Admin":
                            tabItemEmployeeManagement.Visibility = Visibility.Visible;
                            tabItemInventoryManagement.Visibility = Visibility.Visible;
                            tabItemLogistics.Visibility = Visibility.Visible;
                            tabItemServiceProvision.Visibility = Visibility.Visible;
                            tabItemMaterialHandling.Visibility = Visibility.Visible;
                            break;
                        // Logistics
                        case "Logistics Admin":
                            tabItemLogistics.Visibility = Visibility.Visible;
                            break;
                        case "Logistics Driver":
                            tabItemLogistics.Visibility = Visibility.Visible;
                            break;
                        case "Logistics Maintenance":
                            tabItemLogistics.Visibility = Visibility.Visible;
                            break;
                        case "Logistics Manager":
                            tabItemLogistics.Visibility = Visibility.Visible;
                            break;
                        case "Manager":
                            tabItemLogistics.Visibility = Visibility.Visible;
                            break;
                        case "General Employee":
                            tabItemLogistics.Visibility = Visibility.Visible;
                            break;
                        // Inventory
                        case "Inventory Admin":
                            tabItemInventoryManagement.Visibility = Visibility.Visible;
                            break;
                        case "Inventory Maintenance":
                            tabItemInventoryManagement.Visibility = Visibility.Visible;
                            break;
                        case "Inventory Manager":
                            tabItemInventoryManagement.Visibility = Visibility.Visible;
                            break;
                        case "Inventory Specialist":
                            tabItemInventoryManagement.Visibility = Visibility.Visible;
                            break;
                        // Material Handling
                        case "Material Handling Admin":
                            tabItemMaterialHandling.Visibility = Visibility.Visible;
                            break;
                        case "Material Handling Maintenance":
                            tabItemMaterialHandling.Visibility = Visibility.Visible;
                            break;
                        case "Material Handling Manager":
                            tabItemMaterialHandling.Visibility = Visibility.Visible;
                            break;
                        case "Material Handling Item Inspector":
                            tabItemMaterialHandling.Visibility = Visibility.Visible;
                            break;
                        case "Material Handling Inventory Auditor":
                            tabItemMaterialHandling.Visibility = Visibility.Visible;
                            break;
                        case "Material Handling Donor":
                            tabItemMaterialHandling.Visibility = Visibility.Visible;
                            break;
                        // Service Provision
                        case "Service Provision Admin":
                            tabItemServiceProvision.Visibility = Visibility.Visible;
                            break;
                        case "Service Provision Manager":
                            tabItemServiceProvision.Visibility = Visibility.Visible;
                            break;
                        case "Service Provision Provider":
                            tabItemServiceProvision.Visibility = Visibility.Visible;
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Richard Schroeder
        /// Created: 2021/02/24
        ///
        /// Method to reset entire window upon logout or first password update cancellation
        /// </summary>
        private void ResetWindow()
        {

            //grdHyperlinkArea.Visibility = Visibility.Hidden;
            tabMain.Visibility = Visibility.Hidden;
            txtEmail.Visibility = Visibility.Visible;
            pwdPassword.Visibility = Visibility.Visible;
            btnLogin.Content = "Login";
            btnLogin.IsDefault = true;
            mnuMain.IsEnabled = false;
            HideTabs();
            tabWelcome.IsSelected = true;
        }

        /// <summary>
        /// Richard Schroeder
        /// Created: 2021/02/25
        ///
        /// Method to update the main window once the user successfully logs in, can be enhanced where necessary.
        /// </summary>
        private void HideLoginShowUI()
        {
            pwdPassword.Password = "";
            txtEmail.Text = "";
            //grdHyperlinkArea.Visibility = Visibility.Visible;
            tabMain.Visibility = Visibility.Visible;
            txtEmail.Visibility = Visibility.Hidden;
            pwdPassword.Visibility = Visibility.Hidden;
            lblEmail.Visibility = Visibility.Hidden;
            lblPassword.Visibility = Visibility.Hidden;
            btnLogin.Content = "Logout";
            btnLogin.IsDefault = false;
            mnuMain.IsEnabled = true;
            this.DataContext = this;
        }

        /// <summary>
        /// Richard Schroeder
        /// Created: 2021/02/24
        ///
        /// Pulls up the wpfResetPassword dialog box
        /// </summary>
        private void mnuUpdatePassword_Click(object sender, RoutedEventArgs e)
        {
            var updateForm = new frmResetPassword(_userManager, _user);
            updateForm.ShowDialog();
        }

        /// <summary>
        /// Richard Schroeder
        /// Created: 2021/02/24
        ///
        /// Logic to log in a user who is active, and has valid user credentials
        /// </summary>
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if ((string)btnLogin.Content == "Login")
            {
                try
                {
                    // check if email and password fields have content
                    if (txtEmail.Text == "" || pwdPassword.Password == "")
                    {
                        MessageBox.Show("Please enter valid credentials");
                        return;
                    }
                    else
                    {
                        _user = _userManager.AuthenticateUser(txtEmail.Text, pwdPassword.Password);
                    }

                    // check for new user
                    if (pwdPassword.Password == "newuser")
                    {
                        var updatePassword = new frmResetPassword(_userManager,
                            _user, true);
                        // if update is cancelled, keep user logged out and prompt to update password
                        if (!updatePassword.ShowDialog() == true)
                        {
                            _user = null;
                            ResetWindow();

                            MessageBox.Show("You must change your password" +
                                "\n on first login to continue.",
                                "Password Change Required", MessageBoxButton.OK,
                                MessageBoxImage.Warning);

                            return;
                        }
                        // the password was successfully changed.
                    }
                    // successful login
                    ShowRoleTabs(_user);

                    // Display welcome message
                    DisplayWelcomeTab();
                    
                    HideLoginShowUI();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                }
            }
            else // logout
            {
                _user = null;

                ResetWindow();
            }
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/30
        /// 
        /// Display and make welcome tab
        /// disappear;
        /// </summary>
        private async void DisplayWelcomeTab()
        {
            txtBlkWelcomeName.Text = "Welcome " + _user.FirstName + " " + _user.LastName;
            await Task.Delay(10000);
            tabMain.Items.Remove(tabWelcome);
        }

        /// <summary>
        /// Richard Schroeder
        /// Created: 2021/02/24
        ///
        /// Calls the ResetWindow() method and displays blank window until user logs in
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ResetWindow();
            HideTabs();          
            //AUTOLOGIN();
        }

        /// <summary>
        /// Jakub Kawski
        /// 2021/03/08
        /// Navigation Region
        ///
        /// Contains all properties, methods and event listeners involving
        /// loading nav bars, loadings pages, and navigating between pages.
        /// </summary>
        #region Navigation Region
        #region Logistic Pages
        /// <summary>
        /// Jakub Kawski
        /// 2021/03/08
        ///
        /// Properties need for naviagation {bindings}
        /// </summary>
        private ObservableCollection<Page> logisticPages; //Uses to manage navigation
        public ObservableCollection<Page> LogisticPages
        {
            get
            {
                if (logisticPages == null)
                {
                    logisticPages = new ObservableCollection<Page>();
                    LoadLogisticsSideBarNav();
                }
                return logisticPages;
            }
        }
        /// <summary>
        /// Jakub Kawski
        /// 2021/03/08
        ///
        /// Adds pages to the observable collection
        /// </summary>
        private void LoadLogisticsSideBarNav()
        {
            //logisticPages.Add(new pageDeliveryTicketView());
            //logisticPages.Add(new pageViewPickUpTickets());
            //logisticPages.Add(new pagePickUpTicketForm());
            logisticPages.Add(new pageDriversLicenseFormView());
            //logisticPages.Add(new pageDeliveryTicketForm());
            logisticPages.Add(new pageVehicleFormView());
            logisticPages.Add(new pageVehicleMaintenanceStatusView());
            logisticPages.Add(new AddRoute());
            logisticPages.Add(new AddRouteTwo());
            //logisticPages.Add(new pageRideTicketForm());
            //logisticPages.Add(new pageViewRideTickets());
            logisticPages.Add(new pageVehicleMaintenanceReportView());
            logisticPages.Add(new pageAddRideReviewClientFromDriver(_user));
            logisticPages.Add(new pageViewRideReviews());
            logisticPages.Add(new pageVehicleListView());
            logisticPages.Add(new ZipCodePage());
            logisticPages.Add(new pageDriverRouteList());
            logisticPages.Add(new pageTicketPortal());
        }
        /// <summary>
        /// Jakub Kawski
        /// 2021/03/08
        ///
        /// Navigates to selected page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstLogisticsSideBarNav_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            frameLogistics.Navigate(lstLogisticsSideBarNav.SelectedItem);
        }
        #endregion
        #region Invertory Management Pages
        private ObservableCollection<Page> inventoryManagementPages; //Uses to manage navigation
        public ObservableCollection<Page> InventoryManagementPages
        {
            get
            {
                if (inventoryManagementPages == null)
                {
                    inventoryManagementPages = new ObservableCollection<Page>();
                    LoadInventoryManagementSideBarNav();
                }
                return inventoryManagementPages;
            }
        }
        /// <summary>
        /// Jakub Kawski
        /// 2021/03/08
        ///
        /// Adds pages to the observable collection
        /// </summary>
        private void LoadInventoryManagementSideBarNav()
        {
            inventoryManagementPages.Add(new DeleteInventoryItemView());
            inventoryManagementPages.Add(new EditInventoryItemView());
            inventoryManagementPages.Add(new InsertInventoryItemView());
            inventoryManagementPages.Add(new ViewInventoryItemView());
            inventoryManagementPages.Add(new pageAddEditSupplyItem());
        }
        /// <summary>
        /// Jakub Kawski
        /// 2021/03/08
        ///
        /// Navigates to selected page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ///
        private void lstInventoryManagementSideBarNav_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            frameInventoryManagement.Navigate(lstInventoryManagementSideBarNav.SelectedItem);
        }

        #endregion
        #region ServiceProvisionPages
        private ObservableCollection<Page> serviceProvisionPages; //Uses to manage navigation
        public ObservableCollection<Page> ServiceProvisionPages
        {
            get
            {
                if (serviceProvisionPages == null)
                {
                    serviceProvisionPages = new ObservableCollection<Page>();
                    LoadServiceProvisionSideBarNav();
                }
                return serviceProvisionPages;
            }
        }
        /// <summary>
        /// Jakub Kawski
        /// 2021/03/08
        ///
        /// Adds pages to the observable collection
        /// </summary>
        private void LoadServiceProvisionSideBarNav()
        {
            //serviceProvisionPages.Add(new PAGENAME());
            serviceProvisionPages.Add(new ServiceProvidersView());
            serviceProvisionPages.Add(new ServicesView());
        }
        /// <summary>
        /// Jakub Kawski
        /// 2021/03/08
        ///
        /// Navigates to selected page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstServiceProvisionSideBarNav_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            frameServiceProvision.Navigate(lstServiceProvisionSideBarNav.SelectedItem);
        }
        #endregion
        #region EmployeeManagementPages
        /// <summary>
        /// Nate Hepker
        /// Created: 2021/03/09
        ///
        /// Properties need for naviagation {bindings}
        /// </summary>
        private ObservableCollection<Page> employeeManagementPages; //Uses to manage navigation
        public ObservableCollection<Page> EmployeeManagementPages
        {
            get
            {
                if (employeeManagementPages == null)
                {
                    employeeManagementPages = new ObservableCollection<Page>();
                    LoadEmployeeManagementSideBarNav();
                }
                return employeeManagementPages;
            }
        }
        /// <summary>
        /// Nate Hepker
        /// Created: 2021/03/09
        ///
        /// Adds pages to the observable collection
        /// </summary>
        private void LoadEmployeeManagementSideBarNav()
        {
            employeeManagementPages.Add(new pageEditUserRolesView());
            employeeManagementPages.Add(new AddEditEmployee());
        }
        /// <summary>
        /// Nate Hepker
        /// Created: 2021/03/09
        ///
        /// Navigates to the selected page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstEmployeeManagementSideBarNav_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            frameEmployeeManagement.Navigate(lstEmployeeManagementSideBarNav.SelectedItem);
        }

        #endregion

        #region MaterialHandlingPages
        private ObservableCollection<Page> materialHandlingPages;
        public ObservableCollection<Page> MaterialHandlingPages
        {
            get
            {
                if (materialHandlingPages == null)
                {
                    materialHandlingPages = new ObservableCollection<Page>();
                    LoadMaterialHandlingSideBarNav();
                }
                return materialHandlingPages;
            }
        }

        private void LoadMaterialHandlingSideBarNav()
        {

            materialHandlingPages.Add(new CreateDonationForm());
            materialHandlingPages.Add(new ViewDonation());
            materialHandlingPages.Add(new AddDonationItem());
            materialHandlingPages.Add(new ViewDonationForms());
            materialHandlingPages.Add(new AddDonor());
            materialHandlingPages.Add(new DonorView());

        }



        /// <summary>
        /// Asaad Mohamed
        /// Created: 2021/03/13
        ///
        /// Navigates to the selected page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        //frameMaterialHandling.Navigate(lstMaterialHandlingSideBarNav.SelectedItem);





        private void lstMaterialHandlingSideBarNav_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            frameMaterialHandling.Navigate(lstMaterialHandlingSideBarNav.SelectedItem);
        }
        #endregion

        #endregion
    }
}
