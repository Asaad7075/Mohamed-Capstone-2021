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
using DataAccessFakes;
using DomainModels;
using LogicInterfaces;
using LogicLayer;


namespace WpfPresentation
{
    /// <summary>
    /// Zach Stultz
    /// Created: 2021/02/19
    ///
    /// Vehicle Maintenance Report XML cs class.
    /// <summary>
    /// <remarks>
    /// Chantal Shirley
    /// Updated: 2021/04/07
    /// Cleaned up page name to be succinct.
    /// </remarks>
    public partial class pageVehicleMaintenanceReportView : Page
    {
        // used for fakes
        //private IVehicleMaintenanceReportManager _vehicleMaintenanceReportManager = new VehicleMaintenanceReportManager(new VehicleMaintenanceReportFake());
        private IVehicleMaintenanceReportManager _vehicleMaintenanceReportManager;
        private string pageName = "Maintenance Reports";
        private List<VehicleMaintenanceReportVM> _reports;
        private VehicleMaintenanceReportVM _oldReport;

        /// <summary>
        /// Gets the page name.
        /// </summary>
        public string PageName { get { return pageName; } }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/02/19
        ///
        /// Initializes a new instance of the <see cref="AddVehicleMaintenanceReportView"/> class.
        /// </summary>
        /// <remarks>
        /// Chantal Shirley
        /// Updated: 2021/04/02
        /// </remarks>
        public pageVehicleMaintenanceReportView()
        {
            _vehicleMaintenanceReportManager = new VehicleMaintenanceReportManager();
            InitializeComponent();
            InsertMaintenanceTypeComboBoxItems();
            this.txtVehicleVin.Focus();
            PopulateMaintenanceReportsComboBox();
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/02
        /// 
        /// Initializes a new instance with information from API calls
        /// </summary>
        /// 
        /// <remarks>
        /// Zach Stultz
        /// Updated: 2021/04/02
        /// Commeneted out make, model, and Maintenance Text assignment.
        /// </remarks>
        /// <remarks>
        /// Chantal Shirley
        /// Updated: 2021/04/07
        /// Brought back code with validators in the constructor.
        /// </remarks>
        public pageVehicleMaintenanceReportView(string make, string model, string year, string notes)
        {
            _vehicleMaintenanceReportManager = new VehicleMaintenanceReportManager();
            InitializeComponent();
            InsertMaintenanceTypeComboBoxItems();
            this.txtVehicleVin.Focus();
            if (notes.Length < 150)
            {
                tbMaintenanceNotes.Text = "Vehicle Make: " + make + "\n\n" +
                    "Vehicle Model: " + model + "\n\n" +
                    "Vehicle Year: " + year + "\n\n" + notes;
            } else
            {
                tbMaintenanceNotes.Text = "Vehicle Make: " + make + "\n\n" +
                    "Vehicle Model: " + model + "\n\n" +
                    "Vehicle Year: " + year + "\n\n" + notes.Substring(0, 150);
            }
            PopulateMaintenanceReportsComboBox();
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/02
        /// </summary>
        private void PopulateMaintenanceReportsComboBox()
        {
            try
            {
                _reports = _vehicleMaintenanceReportManager.RetrieveAllActiveMaintenanceReports();

                if (_reports.Count > 0)
                {
                    foreach (var report in _reports)
                    {
                        cbMaintenanceReports.Items.Add(report.VehicleMaintenanceReportID.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error while pulling maintenance reports." + ex.Message);
            }
        }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/18
        ///
        /// Inserts the maintenance type items.
        /// </summary>
        public void InsertMaintenanceTypeComboBoxItems()
        {
            cbMaintenanceFinished.Items.Insert(0, "Yes");
            cbMaintenanceFinished.Items.Insert(1, "No");

            cbMaintenenceType.Items.Insert(0, "OIL AND COOLANT LEVELS");
            cbMaintenenceType.Items.Insert(1, "AIR FILTER");
            cbMaintenenceType.Items.Insert(2, "TIRE PRESSURE AND TREAD DEPTH");
            cbMaintenenceType.Items.Insert(3, "HEADLIGHTS, TURN SIGNALS, BRAKE, AND PARKING LIGHTS");
            cbMaintenenceType.Items.Insert(4, "OIL & FILTER");
            cbMaintenenceType.Items.Insert(5, "ROTATE TIRES");
            cbMaintenenceType.Items.Insert(6, "WAX VEHICLE");
        }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/02/26
        ///
        /// Calls our Vehicle Validation method.
        /// <summary>
        private void btnAddMaintenanceReport_Click(object sender, RoutedEventArgs e)
        {
            performVehicleMaintenanceReportValidation();
        }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/25
        ///
        /// Calls our validation methods.
        /// <summary>
        public void performVehicleMaintenanceReportValidation()
        {
            VehicleMaintenanceType vehicleMaintenenceType = new VehicleMaintenanceType();
            vehicleMaintenenceType.VehicleMaintenanceTypeName = cbMaintenenceType.Text.Trim();
            vehicleMaintenenceType.VehicleMaintenanceTypeDescription = "Placeholder";
            string vehicleVin = txtVehicleVin.Text.Trim();
            string vehicleMake = txtVehicleMake.Text.Trim();
            string vehicleModel = txtVehicleModel.Text.Trim();
            string licensePlate = txtLicensePlate.Text.Trim();
            DateTime? maintenanceServiceDate = datePickerMaintenanceServiceDate.SelectedDate;
            string maintenanceNotes = tbMaintenanceNotes.Text.Trim();
            bool isQueued = false;

            if (cbMaintenanceFinished.SelectedIndex.Equals(0).Equals(false))
            {
                isQueued = true;
            }
            List<bool> isValid = new List<bool>();
            // Individual elements for easier debugging.
            bool isValidVehicleVin = ValidateVehicleVin(vehicleVin);
            isValid.Add(isValidVehicleVin);
            //bool isValidVehicleMake = ValidateVehicleMake(vehicleMake);
            //isValid.Add(isValidVehicleMake);
            //bool isValidVehicleModel = ValidateVehicleModel(vehicleModel);
            //isValid.Add(isValidVehicleModel);
            //bool isValidVehicleLicensePlate = ValidateLicensePlate(licensePlate);
            //isValid.Add(isValidVehicleLicensePlate);
            bool isValidMaintenanceType = ValidateMaintenanceType(vehicleMaintenenceType.VehicleMaintenanceTypeName);
            isValid.Add(isValidMaintenanceType);
            bool isValidMaintenanceServiceDate = ValidateMaintenanceServiceDate(maintenanceServiceDate);
            isValid.Add(isValidMaintenanceServiceDate);
            bool isValidMaintenanceFinished = ValidateMaintenanceFinished(cbMaintenanceFinished.SelectedIndex);
            bool isValidVehicleMaintenanceNotes = ValidateMaintenanceNotes(maintenanceNotes);

            if (isValidVehicleVin &&/*isValidVehicleMake&&isValidVehicleModel&&isValidVehicleLicensePlate&&*/ isValidMaintenanceType&&isValidMaintenanceServiceDate&&isValidMaintenanceFinished && btnAddMaintenanceReport.IsEnabled == true && isValidVehicleMaintenanceNotes)
            {
                AddVehicleMaintenanceReport(vehicleVin, vehicleMake, vehicleModel, licensePlate, vehicleMaintenenceType,
                    maintenanceServiceDate, cbMaintenanceFinished.SelectedIndex.Equals(0), maintenanceNotes, isQueued);
            } // Chantal Shirley; Update: 2021/04/02
            else if (isValidVehicleVin && /*isValidVehicleMake&&isValidVehicleModel&& isValidVehicleLicensePlate&&*/ isValidMaintenanceType && isValidMaintenanceServiceDate && isValidMaintenanceFinished && btnAddMaintenanceReport.IsEnabled == false && isValidVehicleMaintenanceNotes)
            {
                UpdateVehicleMaintenanceReport(vehicleVin, vehicleMake, vehicleModel, licensePlate, vehicleMaintenenceType,
                    maintenanceServiceDate, cbMaintenanceFinished.SelectedIndex.Equals(0), maintenanceNotes, isQueued);
            }
        }

        /// <summary>
        /// Chantal Shirley
        /// Modified to fit Zach's code
        /// </summary>
        /// <param name="vehicleVin"></param>
        /// <param name="vehicleMake"></param>
        /// <param name="vehicleModel"></param>
        /// <param name="licensePlate"></param>
        /// <param name="vehicleMaintenenceType"></param>
        /// <param name="maintenanceServiceDate"></param>
        /// <param name="v"></param>
        /// <param name="maintenanceNotes"></param>
        /// <param name="isQueued"></param>
        private void UpdateVehicleMaintenanceReport(string vehicleVin, string vehicleMake, string vehicleModel, string licensePlate, VehicleMaintenanceType vehicleMaintenenceType, DateTime? maintenanceServiceDate, bool v, string maintenanceNotes, bool isQueued)
        {
            try
            {
                VehicleMaintenanceReportVM newReport = new VehicleMaintenanceReportVM()
                {
                    VinNumber = vehicleVin,
                    VehicleMake = vehicleMake,
                    VehicleModel = vehicleModel,
                    LicensePlate = licensePlate,
                    VehicleMaintenanceTypeName = vehicleMaintenenceType.VehicleMaintenanceTypeName,
                    VehicleMaintenanceServiceDate = (DateTime)maintenanceServiceDate,
                    MaintenanceFinished = v,
                    VehicleMaintenanceNotes = maintenanceNotes,
                    IsQueued = isQueued
                };

                _vehicleMaintenanceReportManager.EditVehicleMaintenanceReport(_oldReport, newReport);
                MessageBox.Show("Vehicle Maintenance Report was added to the database.", "Successful Submit", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                // Clear page
                NavigationService.Navigate(new pageVehicleMaintenanceReportView());
            } catch (Exception ex)
            {
                MessageBox.Show("Vehicle Maintenance Report was unable to be added to the database.\n ERROR: " + ex.Message, "Failed Attempt", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/25
        ///
        /// Adds the vehicle maintenance report.
        /// </summary>
        /// <param name="vehicleMaintenanceReport">The vehicle maintenance report.</param>
        /// <returns>A bool.</returns>
        public void AddVehicleMaintenanceReport(string vehicleVin, string vehicleMake, string vehicleModel, string licensePlate,
                    VehicleMaintenanceType vehicleMaintenenceType, DateTime? maintenanceServiceDate, bool maintenanceFinished,
                    string maintenanceNotes, bool isQueued)
        {
            try
            {
                VehicleMaintenanceReportVM vehicleMaintenanceReport = new VehicleMaintenanceReportVM()
                {
                    VinNumber = vehicleVin,
                    VehicleMake = vehicleMake,
                    VehicleModel = vehicleModel,
                    LicensePlate = licensePlate,
                    VehicleMaintenanceTypeName = vehicleMaintenenceType.VehicleMaintenanceTypeName,
                    VehicleMaintenanceServiceDate = (DateTime)maintenanceServiceDate,
                    MaintenanceFinished = maintenanceFinished,
                    VehicleMaintenanceNotes = maintenanceNotes,
                    IsQueued = isQueued
                };
                _vehicleMaintenanceReportManager.AddVehicleMaintenanceReport(vehicleMaintenanceReport);
                MessageBox.Show("Vehicle Maintenance Report was added to the database.", "Successful Submit", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Vehicle Maintenance Report was unable to be added to the database.\n ERROR: " + ex.Message, "Failed Attempt", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/12
        ///
        /// Validates the Vehicle Vin field.
        /// <summary>
        /// <param name="vehicleMake"></param>
        /// <returns></returns>
        private bool ValidateVehicleVin(string vehicleVin)
        {
            bool result = false;
            if (String.IsNullOrWhiteSpace(vehicleVin))
            {
                MessageBox.Show("Vehicle Vin Number cannot be empty.", "Empty Vin Number Field Error");
                result = false;
            }
            else if (vehicleVin.Length > 17)
            {
                MessageBox.Show("Vehicle Vin Number cannot be longer than 17 characters.", "Vehicle Vin Number Length Error");
                result = false;
            }
            else
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/05
        ///
        /// Validates the Vehicle Make field.
        /// <summary>
        /// <param name="vehicleMake"></param>
        /// <returns></returns>
        public bool ValidateVehicleMake(string vehicleMake)
        {
            bool result = false;
            if (String.IsNullOrWhiteSpace(vehicleMake))
            {
                MessageBox.Show("Vehicle Make cannot be empty.", "Empty Vehicle Make Field Error");
                result = false;
            }
            else if (vehicleMake.Length > 50)
            {
                MessageBox.Show("Vehicle Make cannot be longer than 50 characters.", "Vehicle Make Length Error");
                result = false;
            }
            else
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/04/02
        ///
        /// Validates the maintenance notes.
        /// </summary>
        /// <param name="vehicleMaintenanceNotes">The vehicle maintenance notes.</param>
        /// <returns>A bool.</returns>
        public bool ValidateMaintenanceNotes(string vehicleMaintenanceNotes)
        {
            bool result = false;
            if (vehicleMaintenanceNotes.Length > 250)
            {
                MessageBox.Show("Vehicle Maintenance Notes cannot be longer than 250 characters.", "Vehicle Maintenace Notes Length Error");
                result = false;
            }
            else
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/05
        ///
        /// Validates the Vehicle Make field.
        /// <summary>
        /// <param name="vehicleModel"></param>
        /// <returns></returns>
        public bool ValidateVehicleModel(string vehicleModel)
        {
            bool result = false;
            if (String.IsNullOrWhiteSpace(vehicleModel))
            {
                MessageBox.Show("Vehicle Model cannot be empty.", "Empty Vehicle Model Field Error");
                result = false;
            }
            else if (vehicleModel.Length > 50)
            {
                MessageBox.Show("Vehicle Model cannot be longer than 50 characters.", "Vehicle Model Length Error");
                result = false;
            }
            else
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/05
        ///
        /// Validates the Vehicle Make field.
        /// <summary>
        /// <param name="licensePlate"></param>
        /// <returns></returns>
        public bool ValidateLicensePlate(string licensePlate)
        {
            bool result = false;
            if (String.IsNullOrWhiteSpace(licensePlate))
            {
                MessageBox.Show("License Plate cannot be empty.", "Empty License Plate Field Error");
                result = false;
            }
            else if (licensePlate.Length > 10)
            {
                MessageBox.Show("License Plate cannot be longer than 10 characters.", "License Plate Length Error");
                result = false;
            }
            else
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/05
        ///
        /// Validates the Vehicle Make field.
        /// <summary>
        /// <param name="maintenenceType"></param>
        /// <returns></returns>
        public bool ValidateMaintenanceType(string maintenenceType)
        {
            bool result = false;
            if (String.IsNullOrWhiteSpace(maintenenceType))
            {
                MessageBox.Show("Vehicle Maintenance Type cannot be empty.", "Empty Vehicle Make Field Error");
                result = false;
            }
            else if (maintenenceType.Length > 100)
            {
                MessageBox.Show("Vehicle Maintenance Type cannot be longer than 100 characters.", "Vehicle Maintenance Type Length Error");
                result = false;
            }
            else
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/05
        ///
        /// Validates the Vehicle Make field.
        /// <summary>
        /// <param name="maintenanceServiceDate"></param>
        /// <returns></returns>
        public bool ValidateMaintenanceServiceDate(DateTime? maintenanceServiceDate)
        {
            bool result;
            if (maintenanceServiceDate.Equals(null))
            {
                MessageBox.Show("Please enter a date.", "Empty Maintenance Service Date Field Error");
                result = false;
            }
            else if (maintenanceServiceDate.Value > DateTime.Now)
            {
                MessageBox.Show("Date cannot be in the future.", "Maintenance Service Date Is In Future Error");
                result = false;
            }
            else
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/05
        ///
        /// Validates the Vehicle Make field.
        /// <summary>
        /// <param name="maintenanceFinished"></param>
        /// <returns></returns>
        public bool ValidateMaintenanceFinished(int maintenanceFinished)
        {
            bool result = false;
            if (maintenanceFinished < 0)
            {
                MessageBox.Show("Please select a Maintenance Finished option.", "Empty Maintenance Finished Field Error");
                result = false;
            } else
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/03/05
        ///
        /// Checks our collection for a duplicate entry.
        /// <summary>
        /// <param name="vehicleMaintenanceReport"></param>
        /// <param name="_vehicleMaintenanceReports"></param>
        /// <returns></returns>
        public bool DuplicateCheck(List<VehicleMaintenanceReportVM> vehicleMaintenanceReportVMs, VehicleMaintenanceReportVM vehicleMaintenanceReport)
        {
            bool result = false;
            for (int i = 0; i < vehicleMaintenanceReportVMs.Count; i++)
            {
                if (
                    vehicleMaintenanceReport.VehicleMake == vehicleMaintenanceReportVMs[i].VehicleMake &&
                    vehicleMaintenanceReport.VehicleModel == vehicleMaintenanceReportVMs[i].VehicleModel &&
                    vehicleMaintenanceReport.LicensePlate == vehicleMaintenanceReportVMs[i].LicensePlate &&
                    vehicleMaintenanceReport.VehicleMaintenanceTypeName == vehicleMaintenanceReportVMs[i].VehicleMaintenanceTypeName &&
                    vehicleMaintenanceReport.VehicleMaintenanceServiceDate == vehicleMaintenanceReportVMs[i].VehicleMaintenanceServiceDate &&
                    vehicleMaintenanceReport.MaintenanceFinished == vehicleMaintenanceReportVMs[i].MaintenanceFinished &&
                    vehicleMaintenanceReport.VehicleMaintenanceNotes == vehicleMaintenanceReportVMs[i].VehicleMaintenanceNotes)
                {
                    result = true;
                }
            }
            return result;
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/02
        /// 
        /// Maintenance status report gets populated
        /// for auto-update.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbMaintenanceReports_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbMaintenanceReports.SelectedItem.ToString() != "Choose a Maintenance Report...")
            {
                btnAddMaintenanceReport.IsEnabled = false;
                btnUpdateMaintenanceReport.IsEnabled = true;

                VehicleMaintenanceReportVM currReport = null;
                foreach (var report in _reports)
                {
                    if (report.VehicleMaintenanceReportID == Int64.Parse(cbMaintenanceReports.SelectedItem.ToString()))
                    {
                        currReport = report;
                        break;
                    }
                }
                if (currReport != null)
                {
                    txtVehicleVin.Text = currReport.VinNumber;
                    txtVehicleMake.Text = currReport.VehicleMake;
                    txtVehicleModel.Text = currReport.VehicleModel;
                    txtLicensePlate.Text = currReport.LicensePlate;
                    cbMaintenenceType.Text = currReport.VehicleMaintenanceTypeName;
                    datePickerMaintenanceServiceDate.SelectedDate = currReport.VehicleMaintenanceServiceDate;
                    cbMaintenanceFinished.Text = currReport.MaintenanceFinished.ToString().Equals("True") ? "Yes" : "No";
                    tbMaintenanceNotes.Text = currReport.VehicleMaintenanceNotes;
                }

                _oldReport = currReport;

            }
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/02
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelAddMaintenanceReport_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new pageVehicleMaintenanceReportView());
        }

        private void btnUpdateMaintenanceReport_Click(object sender, RoutedEventArgs e)
        {
            performVehicleMaintenanceReportValidation();
        }
    }
}
