using DomainModels;
using LogicInterfaces;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;
using WpfPresentation.CustomUserControls;

namespace WpfPresentation.LogisticsViews.Vehicle
{
    /// <summary>
    /// Interaction logic for EditVehicleView.xaml
    /// </summary>
    public partial class EditVehicleView : Window
    {
        #region Properties & Bindings
        private BitmapImage _vehicleImage;
        private VehicleVM _vehicleVM;
        private IVehicleManager _vehicleManager = new VehicleManager();
        private IVehicleImageManager _vehicleImageManager = new VehicleImageManager();

        private string _vehicleMake;
        public string VehicleMake 
        {
            get => _vehicleMake;
            private set
            {
                _vehicleMake = value;
            }
        }

        private string _vehicleModel;
        public string VehicleModel 
        {
            get => _vehicleModel;
            private set
            {
                _vehicleModel = value;
            }
        }

        private string _vehicleYear;
        public string VehicleYear
        {
            get => _vehicleYear;
            private set
            {
                _vehicleYear = value;
            }
        }

        private string _vehicleVin;
        public string VinNumber
        {
            get => _vehicleVin;
            private set
            {
                _vehicleVin = value;
            }
        }

        private string _licensePlateNumber;
        public string LicensePlateNumber
        {
            get => _licensePlateNumber; 
            set
            {
                _licensePlateNumber = value.ToUpper();
            }
        }

        private string _mileage;
        public string Mileage
        {
            get => _mileage;
            set
            {
                _mileage = value;
            }
        }

        #endregion

        #region Constructors
        public EditVehicleView(VehicleVM vehicle)
        {
            InitializeComponent();
            _vehicleVM = vehicle;
            PopulateView();
        }
        #endregion

        #region Private Helper Methods

        private void PopulateView()
        {
            imgVehicle.Source = _vehicleVM.VehicleImage;
            VehicleMake = _vehicleVM.VehicleMake;
            VehicleModel = _vehicleVM.VehicleModel;
            VehicleYear = _vehicleVM.VehicleYear.ToString();
            VinNumber = _vehicleVM.VinNumber;
            LicensePlateNumber = _vehicleVM.LicensePlateNumber;
            Mileage = _vehicleVM.Mileage.ToString();
            this.DataContext = this;
        }

        private void CheckSave()
        {
            if (LicPlateNumberValidator.PropertyErrors == true && MileValidator.PropertyErrors == true)
            {
                btnUpdateVehicle.IsEnabled = true;
            }
            else
            {
                btnUpdateVehicle.IsEnabled = false;
            }
        }
        #endregion

        #region Private Event Helper Methods
        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/03/24
        /// 
        /// Allows user to exit out of vehicle view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/03/24
        /// 
        /// Allows user to update license plate no,
        /// mileage, and image of an existing vehicle
        /// in the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdateVehicle_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool result = _vehicleManager.UpdateVehicleThroughVMByVin(VinNumber, LicensePlateNumber, 
                    Mileage);
                if (!result)
                {
                    throw new Exception("Vehicle could not be updated.");
                }
                else
                {
                    MessageBox.Show("Vehicle update was a success!", "Success",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }

                if (_vehicleImage != null) // Only update image if needed
                {
                    bool imageResult = _vehicleImageManager.UpdateVehicleImageByVin(
                        FileUploader.ImageUploader.ByteImage, VinNumber);
                    this.Close();

                    if (!imageResult)
                    {
                        throw new Exception("Vehicle image could not be updated.");
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Vehicle update was unsuccessful.\n\n" + ex.Message);
            }
        }

        private void txtBoxMileage_Error(object sender, ValidationErrorEventArgs e)
        {
            CheckSave();
        }

        private void txtBoxLicPlateNo_Error(object sender, ValidationErrorEventArgs e)
        {
            CheckSave();
        }

        private void FileUploader_LayoutUpdated(object sender, EventArgs e)
        {
            if (FileUploader.ImageUploader != null)
            {
                imgVehicle.Source = FileUploader.ImageUploader.Image;
                _vehicleImage = FileUploader.ImageUploader.Image;
            }
        }

        #endregion

    }
}
