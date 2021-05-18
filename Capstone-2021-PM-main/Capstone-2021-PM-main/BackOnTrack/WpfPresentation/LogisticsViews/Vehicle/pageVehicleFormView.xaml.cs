using LogicInterfaces;
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
using LogicLayer;
using DomainModels;
using DataAccessFakes;
using WpfPresentation.Validators;

namespace WpfPresentation
{
    /// <summary>
    /// Nate Hepker
    /// Created:2021/02/13
    /// 
    /// Interaction logic for AddVehicleView.xaml
    /// </summary>
    public partial class pageVehicleFormView : Page
    {
        //this is used for testing
        //private IVehicleManager _vehicleManager = new VehicleManager(new VehicleFake());
        private IVehicleManager _vehicleManager;
        //private MainWindow _mainwindow;
        private string _pageName = "Vehicle Form";
        private IVehicleImageManager _vehicleImageManager;// Chantal Shirley, Update 2021/03/22
        private string _defaultVehicleImage = "DefaultVehicleImg.png";

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
                return _pageName;
            }
        }

        /// <summary>
        /// Chantal Shirley
        /// Updated: 2021/03/20
        /// </summary>
        private string _vehicleImage { get; set; }

        /// <summary>
        /// Nate Hepker
        /// Created:2021/02/13
        /// 
        /// View class for adding a vehicle
        /// </summary>
        public pageVehicleFormView()
        {
            //_mainwindow = mainwindow;
            _vehicleImageManager = new VehicleImageManager();
            _vehicleManager = new VehicleManager();
            ClearImageUploader();
            InitializeComponent();
            this.txtVinNumber.Focus();
        }


        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/03/23
        /// 
        /// Checks to make sure the 
        /// file uploader is clear and read for use.
        /// </summary>
        private void ClearImageUploader()
        {
            if (FileUploader != null)
            {
                FileUploader.ImageUploader.GarbageCollectImage(); // Reset ImgUploader before using to ensure it is free to use
            }
        }

        /// <summary>
        /// Nate Hepker
        /// Created:2021/02/13
        /// 
        /// Event listener for a vehicle being added
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddVehicle_Click(object sender, RoutedEventArgs e)
        {
            validateAddVehicle();
        }

        /// <summary>
        /// Nate Hepker
        /// Created:2021/02/13
        /// 
        /// Validates the addVehicle input
        /// </summary>
        private void validateAddVehicle()
        {

            string[] userInputs =
            {
                txtVinNumber.Text.Trim(),
                txtLicensePlate.Text.Trim(),
                txtVehicleMake.Text.Trim(),
                txtVehicleModel.Text.Trim(),
                txtVehicleYear.Text.Trim(),
                txtMileage.Text.Trim()
            };

            //string geoID = txtGeoID.Text.Trim();
            string licenseClass = cobLicenseClass.Text.Trim();
            bool adaCompliant = setADAComplientTrueOrFalse();

            // validates the text fields are filled
            if (userInputs.containsEmptyString())
            {
                MessageBox.Show("Add vehicle form is not completed", "Incomplete Form", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }
            //// validates the VinNumber must be 17 char in length)
            //if (userInputs.GetValue(0).ToString().Length != 17)
            //{
            //    MessageBox.Show("VinNumber must be 17 characters in length", "Invalid VinNumber",
            //        MessageBoxButton.OK, MessageBoxImage.Error);
            //    txtVinNumber.Focus();
            //    return;
            //}
            // Chantal Shirley
            // Updated: 2021/03/23
            // It is helpful to let the user know if there string is to short or too long
            if (userInputs.GetValue(0).ToString().Length > 17)
            {
                MessageBox.Show("VinNumber is too long, it must be 17 characters in length", "Invalid VinNumber",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                txtVinNumber.Focus();
                return;
            }
            if (userInputs.GetValue(0).ToString().Length < 17)
            {
                MessageBox.Show("VinNumber is too short, it must be 17 characters in length", "Invalid VinNumber",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                txtVinNumber.Focus();
                return;
            }
            // Chantal Shirley
            // Updated: 2021/03/23
            // Providing slightly more specific information
            // validates the year is integer
            if (!userInputs.GetValue(4).ToString().isAnInteger())
            {
                MessageBox.Show("Vehicle year must be a 4-digit integer year", "Invalid Vehicle Year",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                txtVehicleYear.Focus();
                return;
            }
            // validates the year is a number exactly 4 digits long
            if (userInputs.GetValue(4).ToString().Length != 4)
            {
                MessageBox.Show("Vehicle year must be a year in 'YYYY' format", "Invalid Vehicle Year",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                txtVehicleYear.Focus();
                return;
            }
            // validates the year of the vehicle is after the first car production date.
            if (Int32.Parse(userInputs.GetValue(4).ToString()) <= 1900)
            {
                MessageBox.Show("Vehicle date must be newer than 1900",
                    "Invalid Vehicle Year", MessageBoxButton.OK, MessageBoxImage.Error);
                txtVehicleYear.Focus();
                return;
            }
            // validates the year of the vehicle is not after this year.
            // Chantal Shirley
            // Updated: 2021/03/21
            // Added a year plus one, because of newer vehicle years
            if (Int32.Parse(userInputs.GetValue(4).ToString()) >= DateTime.Now.Year+1)
            {
                MessageBox.Show("Vehicle date must be for next year or older " + DateTime.Now.Year,
                    "Invalid Vehicle Year", MessageBoxButton.OK, MessageBoxImage.Error);
                txtVehicleYear.Focus();
                return;
            }
            // Chantal Shirley
            // Updated: 2021/03/23
            // validates the mileage enterd is a number
            if (!userInputs.GetValue(5).ToString().isAnInteger())
            {
                MessageBox.Show("Vehicle mileage must be a valid integer number.", "Invalid Vehicle Mileage",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                txtMileage.Focus();
                return;
            }
            // validates the mileage enterd is a positive number
            if (Int32.Parse(userInputs.GetValue(5).ToString()) < 0)
            {
                MessageBox.Show("Vehicle mileage must be a positive number", "Invalid Vehicle Mileage",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                txtMileage.Focus();
                return;
            }
            // validates a licenseClass was selected
            if (licenseClass == "-- Select License --")
            {
                MessageBox.Show("Minimum license class required\n\"-- Select License--\" " +
                    "is not a valid selection.\nPlease make a diffrent selection from the drop down.",
                    "Invalid Selection", MessageBoxButton.OK, MessageBoxImage.Error);
                cobLicenseClass.Focus();
                return;
            }
            // validates a checkBox was selected
            if (this.chkADAComplientNo.IsChecked == false && this.chkADAComplientYes.IsChecked == false)
            {
                MessageBox.Show("Must select if the vehicle is handicap accessible or not",
                    "Invalid Selection", MessageBoxButton.OK, MessageBoxImage.Error);
                chkADAComplientYes.Focus();
                return;
            }
            // Chantal Shirley
            // Updated: 2021/03/20
            // Geolocation should not be set manually
            // validates GeoID is number and XXXXX in length or more
            //if ((geoID.isAnInteger()
            //    && Int32.Parse(geoID) < 10000)
            //    && !(geoID == null))
            //{
            //    MessageBox.Show("GeoID must be a number and be 5 digits in length or greater",
            //        "Invalid GeoID", MessageBoxButton.OK, MessageBoxImage.Error);
            //    txtGeoID.Focus();
            //    return;
            //}
            // All validations passed, vehicle can now be added
            addValidatedVehicle();
        }

        /// <summary>
        /// Nate Hepker
        /// Created:2021/02/14
        /// 
        /// Adds the validated input
        /// </summary>
        /// <remarks>
        /// Chantal Shirley
        /// Updated: 2021/03/23
        /// 
        /// Introduced text formatting for
        /// standardized data entry.
        /// </remarks>
        private void addValidatedVehicle()
        {
            try
            {
                Vehicle vehicle = new Vehicle()
                {
                    VinNumber = txtVinNumber.Text.ToUpper(),
                    //GeoID = Int32.Parse(txtGeoID.Text),
                    LicensePlateNumber = txtLicensePlate.Text.ToUpper(),
                    VehicleMake = txtVehicleMake.Text,
                    VehicleModel = txtVehicleModel.Text,
                    VehicleYear = Int32.Parse(txtVehicleYear.Text),
                    LicenseClass = cobLicenseClass.Text,
                    Mileage = Int32.Parse(txtMileage.Text),
                    ADACompliant = setADAComplientTrueOrFalse(),
                };

                // Chantal Shirley
                // Updated: 2021/03/24
                // Getting result will be helpful in determining operation of other methods.
                bool result = _vehicleManager.AddVehicle(vehicle);

                // Chantal Shirley
                // Updated: 2021/03/22
                // Supplementary method to insert image into database
                if (result)
                {
                    if (FileUploader.ImageUploader != null) // Make sure ImgUploader has an image
                    {
                        bool saved = _vehicleImageManager.AddVehicleImageByVin(FileUploader.ImageUploader.ByteImage, vehicle.VinNumber);
                        if (!saved)
                        {
                            MessageBox.Show("The vehicle image was unable to be saved.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        FileUploader.ImageUploader.GarbageCollectImage(); // Reset ImgUploader
                    }
                    else // Upload default image
                    {
                        // Get Default Image
                        Byte[] defaultCar = _vehicleImageManager.RetrieveVehicleImageByName(_defaultVehicleImage);

                        // Add Default Image as new entry
                        bool saved = _vehicleImageManager.AddVehicleImageByVin(defaultCar, vehicle.VinNumber);
                        if (!saved)
                        {
                            MessageBox.Show("The vehicle image was unable to be saved.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }

                MessageBox.Show("Success! " + vehicle.VehicleYear + " " + vehicle.VehicleMake + " "
                    + vehicle.VehicleModel + " " + vehicle.VinNumber + "\n\nHas been added to the database!",
                    "Vehicle Add Successful", MessageBoxButton.OK, MessageBoxImage.Information);
                resetAddVehicleWindow();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Vehicle enterd was unable to be added to the database. \n\n"
                    + ex.Message + "\n\n" + ex.InnerException ?? ex.InnerException.Message, "Insert Vehicle Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Nate Hepker
        /// Created:2021/02/14
        /// 
        /// ADAComplient Yes event handler for toggling CheckBoxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkADAComplientYes_Checked(object sender, RoutedEventArgs e)
        {
            this.chkADAComplientNo.IsChecked = false;
        }

        /// <summary>
        /// Nate Hepker
        /// Created:2021/02/14
        /// 
        /// ADAComplient No event handler for toggling CheckBoxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkADAComplientNo_Checked(object sender, RoutedEventArgs e)
        {
            this.chkADAComplientYes.IsChecked = false;
        }

        /// <summary>
        /// Nate Hepker
        /// Created:2021/02/14
        /// 
        /// Sets the ADAComplient CheckBoxes to true or false based on whats selected
        /// </summary>
        /// <returns></returns>
        private bool setADAComplientTrueOrFalse()
        {
            bool result = false;
            if (chkADAComplientNo.IsChecked == true)
            {
                result = false;
            }
            else if (chkADAComplientYes.IsChecked == true)
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Nate Hepker
        /// Created:2021/02/14
        /// 
        /// Resets the AddVehicle Window
        /// </summary>
        private void resetAddVehicleWindow()
        {
            this.txtVinNumber.Text = "";
            this.txtVehicleMake.Text = "";
            this.txtVehicleModel.Text = "";
            this.txtVehicleYear.Text = "";
            this.txtLicensePlate.Text = "";
            this.txtMileage.Text = "";
            //this.txtGeoID.Text = "";
            this.chkADAComplientNo.IsChecked = false;
            this.chkADAComplientYes.IsChecked = false;
            this.cobLicenseClass.Text = "-- Select License --";
        }
    }
}
