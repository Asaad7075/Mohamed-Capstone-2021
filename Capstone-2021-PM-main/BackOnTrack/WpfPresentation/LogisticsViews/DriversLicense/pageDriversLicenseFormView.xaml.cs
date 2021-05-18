using LogicInterfaces;
using LogicLayer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace WpfPresentation.LogisticsViews.DriversLicense
{
    /// <summary>
    /// Chantal Shirley
    /// Interaction logic for pageDriversLicenseForm.xaml
    /// </summary>
    public partial class pageDriversLicenseFormView : Page
    {
        #region Page Properties
        private DomainModels.DriversLicense _driversLicense;
        private string pageName = "Drivers License Form";
        public string PageName { get { return pageName; } }
        // ValidationRules that contain properties indicating whether the user has resolved their errors
        private IDriversLicenseManager _driversLicenseManager;
        // private IDriversLicenseManager _driversLicenseManager = new DriversLicenseManager(new DriversLicenseFake()); // Tester code for the drivers license class
        #endregion

        #region Validation Properties
        private string _licenseNumber;
        public string LicenseNumber
        {
            get => _licenseNumber;
            set
            {
                if (licNoError.PropertyErrors) // If there are no property errors set property
                {
                    _driversLicense.LicenseNumber = value;
                    CheckSave();
                }
                _licenseNumber = value;
            }
        }

        private string _employeeID;
        public string EmployeeID
        {
            get => _employeeID;
            set
            {
                if (EmpIDError.PropertyErrors) // If there are no property errors set property
                {
                    _driversLicense.EmployeeID = Int32.Parse(value);
                    CheckSave();
                }
                _employeeID = value;
            }
        }

        DateTime? _licenseIssued;
        public DateTime? LicenseIssued
        {
            get => _licenseIssued;
            set
            {
                if (licIssuedError.PropertyErrors) // If there are no property errors set property
                {
                    _driversLicense.LicenseIssuedDate = (DateTime)value;
                    CheckSave();
                }
                _licenseIssued = value;
            }
        }

        DateTime? _licenseExpiry;
        public DateTime? LicenseExpiry
        {
            get => _licenseExpiry;
            set
            {
                if (licExpiryError.PropertyErrors) // If there are no property errors set property
                {
                    _driversLicense.LicenseExpiryDate = (DateTime)value;
                    CheckSave();
                }
                _licenseExpiry = value;
            }
        }

        private string _licType = "A";
        public string LicType
        {
            get => _licType;
            set
            {
                _driversLicense.LicenseType = value;
                CheckSave();
                _licType = value;
            }
        }

        public List<string> LicenseType
        {
            get
            {
                List<string> licenseType = new List<string>();
                try
                {
                    licenseType = _driversLicenseManager.RetreiveDriversLicenseTypes();
                    LicType = "A"; // Default License Type
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Display Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                return licenseType;
            }
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/03/10
        /// 
        /// Zero-Argument constructor for 
        /// drivers license form page.
        /// </summary>
        public pageDriversLicenseFormView()
        {
            InitializeComponent();
            DataContext = this;
            _driversLicense = new DomainModels.DriversLicense();
            _driversLicenseManager = new DriversLicenseManager();
        }
        #endregion

        #region Private Helper Methods
        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/03/12
        /// 
        /// Determines whether or not to enable
        /// save button.
        /// </summary>
        private void CheckSave()
        {
            btnAddDriversLicenseInformation.IsEnabled = ((licNoError.PropertyErrors && EmpIDError.PropertyErrors) &&
                (licExpiryError.PropertyErrors && licIssuedError.PropertyErrors)) ? true : false;
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
        private void DetermineLicenseUniqueness(DomainModels.DriversLicense driversLicense, bool isDuplicate)
        {
            if (isDuplicate == false)
            {
                try
                {
                    bool result = _driversLicenseManager.AddDriversLicense(driversLicense); // Add drivers license
                    if (result)
                    {
                        MessageBox.Show("Success! The drivers license has been added to the database!",
                        "Successful Additon", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        Reset();
                    }
                    else
                    {
                        MessageBox.Show("The drivers license was unable to be added at this time. Please" +
                            "try again later.", "Unable to Add", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("The drivers license was unable to be added at this time. Please" +
                            "try again later." + ex.Message, "Unable to Add", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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
        /// Created: 2021/02/19
        /// 
        /// Clears entries upon successful insertion of 
        /// drivers license data.
        /// </summary>
        /// <remarks>
        /// Chantal Shirley
        /// Updated: 2021/03/12
        /// </remarks>
        private void Reset()
        {
            _driversLicense = new DomainModels.DriversLicense();
            licNoError.PropertyErrors = false;
            EmpIDError.PropertyErrors = false;
            licIssuedError.PropertyErrors = false;
            licExpiryError.PropertyErrors = false;
            txtDriversLicNo.Text = null;
            txtEmployeeID.Text = null;
            datePickerLicenseExpiry.SelectedDate = null;
            datePickerLicenseIssued.SelectedDate = null;
            LicType = "A"; // Default license type
            comboBoxLicenseType.SelectedIndex = 0; // Default License Type Position
            CheckSave();
        }
        #endregion

        #region Event Methods
        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/02/19
        /// 
        /// Verifies users entry prior to insertion.
        /// </summary>
        /// <remarks>
        /// Chantal Shirley
        /// Updated: 2021/03/12
        /// </remarks>
        private void btnAddDriversLicenseInformation_Click(object sender, RoutedEventArgs e)
        {

            // Verify the user would like to insert the data
            string message = "Are you sure you would like to add the following drivers license?\n\n" +
                "License Number: " + _driversLicense.LicenseNumber + "\n" +
                "Employee ID: " + _driversLicense.EmployeeID + "\n" +
                "License Type: " + _driversLicense.LicenseType + "\n" +
                "License Expiry Date: " + _driversLicense.LicenseExpiryDate.ToString("MM/dd/yyyy") + "\n" +
                "License Issued Date: " + _driversLicense.LicenseIssuedDate.ToString("MM/dd/yyyy");

            string title = "Verify License";

            // Add Drivers License
            if (MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                // Check to make sure that the drivers license does not already exist
                bool isDuplicate = _driversLicenseManager.RetrieveDriversLicenseByLicenseNumber(_driversLicense.LicenseNumber);
                DetermineLicenseUniqueness(_driversLicense, isDuplicate);
            }
        }

        #endregion
    }
}
