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


namespace WpfPresentation
{
    /// <summary>
    /// Interaction logic for pageAddDriversLicenseView.xaml
    /// </summary>
    public partial class pageAddDriversLicenseView : Page
    {
        // private IDriversLicenseManager _driversLicenseManager = new DriversLicenseManager(new DriversLicenseFake()); // Tester code for the drivers license class
        private IDriversLicenseManager _driversLicenseManager;
        private MainWindow _mainWindow;

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/02/02
        /// 
        /// Single-Argument Constructur for creating a dynamic page
        /// that allows the appropriate user to add drivers license views.
        /// </summary>
        /// <param name="mainWindow"></param>
        public pageAddDriversLicenseView(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            _driversLicenseManager = new DriversLicenseManager();
            InitializeComponent();
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/02/02
        /// 
        /// Event listener for insertion of driver's license data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddDriversLicenseInformation_Click(object sender, RoutedEventArgs e)
        {
            // Perform Validations for Drivers License Information
            PerformDriversLicenseValidation();
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/02/02
        /// 
        /// Helper method for performing validations before inserting drivers license data.
        /// </summary>
        public void PerformDriversLicenseValidation()
        {
            // Get form values from drivers license form
            string[] userInputs = {
                txtDriversLicNo.Text.Trim(),
                txtLicenseType.Text.Trim()
            };

            string empID = txtEmployeeID.Text.Trim(); // Make sure there's no empty space

            // Validate checks against Drivers License information
            bool validEntry = ValidateDriversLicense(userInputs, empID);

            // All validation has passed, now attempt to insert drivers license information
            if (validEntry)
            {
                try
                {
                    AddNewDriversLicense(); // Attempt to add drivers license
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Drivers license was unable to be added to the database.\n See: " +
                        ex.Message, "Failed Attempt", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/02/21
        /// 
        /// Allows user to add a new drivers license, after validation
        /// checks have passed.
        /// </summary>
        private void AddNewDriversLicense()
        {
            DriversLicense driversLicense = new DriversLicense() // Initialize Drivers license
            {
                LicenseNumber = txtDriversLicNo.Text,
                EmployeeID = Int32.Parse(txtEmployeeID.Text),
                LicenseType = txtLicenseType.Text,
                LicenseExpiryDate = (DateTime)datePickerLicenseExpiry.SelectedDate,
                LicenseIssuedDate = (DateTime)datePickerLicenseIssued.SelectedDate,
                IsActive = true
            };

            // Verify the user would like to insert the data
            string message = "Are you sure you would like to add the following drivers license?\n\n" +
                "License Number: " + driversLicense.LicenseNumber + "\n" +
                "Employee ID: " + driversLicense.EmployeeID + "\n" +
                "License Type: " + driversLicense.LicenseType + "\n" +
                "License Expiry Date: " + driversLicense.LicenseExpiryDate.ToString("MM/dd/yyyy") + "\n" +
                "License Issued Date: " + driversLicense.LicenseIssuedDate.ToString("MM/dd/yyyy");

            string title = "Verify License";

            // Determine if the license is as expected
            if (MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                // Check to make sure that the drivers license does not already exist
                bool isDuplicate = _driversLicenseManager.RetrieveDriversLicenseByLicenseNumber(driversLicense.LicenseNumber);
                DetermineLicenseUniqueness(driversLicense, isDuplicate);
            }
            else
            {
                // Start at the beginning of the form again
                txtDriversLicNo.Focus();
            }
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/02/21
        /// 
        /// Determines whether if the drivers license 
        /// is a duplicate and whether or not the user
        /// should be notified.
        /// </summary>
        /// <param name="driversLicense"></param>
        /// <param name="isDuplicate"></param>
        private void DetermineLicenseUniqueness(DriversLicense driversLicense, bool isDuplicate)
        {
            if (isDuplicate == false)
            {
                bool result = _driversLicenseManager.AddDriversLicense(driversLicense); // Add drivers license
                if (result)
                {
                    MessageBox.Show("Success! The drivers license has been added to the database!",
                    "Successful Additon", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    ClearEntries();
                }
                else
                {
                    MessageBox.Show("The drivers license was unable to be added at this time. Please" +
                        "try again later.", "Unable to Add", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("A duplicate of the drivers license already exists",
                    "Unable to Add", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/02/21
        /// 
        /// Helps validate the user's inputs for 
        /// their drivers license.
        /// </summary>
        /// <param name="userInputs"></param>
        /// <param name="empID"></param>
        private bool ValidateDriversLicense(string[] userInputs, string empID)
        {
            bool validEntry = true;
            if (!empID.IsAnInteger())
            {
                MessageBox.Show("Employee IDs must be valid numbers.", "Invalid Employee ID",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                txtEmployeeID.Focus();
                validEntry = false;
            }
            if (userInputs.ContainsEmptyString())
            {
                MessageBox.Show("Forms must be fully filled out to add Driver's License information.", "Incomplete" +
                    " Form", MessageBoxButton.OK, MessageBoxImage.Error);
                validEntry = false;
            }
            if (datePickerLicenseExpiry.DateTodayOrInPast())
            {
                MessageBox.Show("Expiry dates must be greater than today's date.",
                    "License Expired", MessageBoxButton.OK, MessageBoxImage.Error);
                datePickerLicenseExpiry.Focus();
                validEntry = false;
            }
            if (datePickerLicenseIssued.DateInFuture())
            {
                MessageBox.Show("Licenses issue dates cannot be in the future.",
                    "Invalid Date Input", MessageBoxButton.OK, MessageBoxImage.Error);
                datePickerLicenseIssued.Focus();
                validEntry = false;
            }

            return validEntry;
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/02/19
        /// 
        /// Clears entries upon successful insertion of 
        /// drivers license data.
        /// </summary>
        private void ClearEntries()
        {
            txtLicenseType.Clear();
            txtEmployeeID.Clear();
            txtDriversLicNo.Clear();
            datePickerLicenseExpiry.SelectedDate = null;
            datePickerLicenseIssued.SelectedDate = null;
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/02/21
        /// 
        /// Returns user to Logistics Portal.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelAddDriversLicenseInformation_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.frameLogistics.Content = null;
            _mainWindow.TestLogisticsAccessAfterLogin();
        }
    }
}
