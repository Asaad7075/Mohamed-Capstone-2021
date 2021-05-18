using DomainModels;
using LogicInterfaces;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace WpfPresentation.LogisticsViews.Vehicle
{
    /// <summary>
    /// Chantal Shirley
    /// Redesigned: 2021/03/12
    /// Interaction logic for RemoveVehicle.xaml
    /// </summary>
    public partial class pageRemoveVehicleListView : Page
    {
        #region Properties
        // private IVehicleManager _vehicleManager = new VehicleManager(new VehicleFake()); // For Testing purposes
        private IVehicleManager _vehicleManager;
        private IVehicleImageManager _imageManager;
        private DomainModels.VehicleVM _vehicle;
        private string pageName = "Remove Vehicles";
        private ObservableCollection<VehicleVM> _vehicles;
        public string PageName { get { return pageName; } }


        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/03/12
        /// 
        /// Representation of vehicles in fleet.
        /// </summary>
        public ObservableCollection<VehicleVM> Vehicles
        {
            get => _vehicles;
            private set
            {
                _vehicles = value;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/03/12
        /// </summary>
        public pageRemoveVehicleListView()
        {
            InitializeComponent();
            _vehicleManager = new VehicleManager();
            _vehicle = new VehicleVM();
            _vehicles = new ObservableCollection<VehicleVM>();
            _imageManager = new VehicleImageManager();
            PopulateView();
        }
        #endregion

        #region Helper Methods
        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/03/12
        /// 
        /// Populates initial view of vehicles
        /// in fleet that can be removed.
        /// </summary>
        private void PopulateView()
        {
            try
            {
                //Need to build view table
                ObservableCollection<VehicleVM> _vehiclesRawData = _vehicleManager.RetrieveAllVehiclesVMs();
                // Updating View inspiration from: https://stackoverflow.com/questions/26353919/wpf-listview-binding-itemssource-in-xaml
                this.Vehicles = _vehiclesRawData;
                this.DataContext = this;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Vehicle data could not be retrieved." + "\n\n" + ex.Message,
                    "Loading Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region Action Methods
        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/03/12
        /// 
        /// Allows for the deletion of vehicles
        /// from the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemoveVehicle_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool result = _vehicleManager.DeleteVehicleThroughVM((VehicleVM)lstViewVehicles.SelectedValue);
                if (result)
                {
                    _vehicles.Remove((VehicleVM)lstViewVehicles.SelectedValue); // View automatically updates
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Vehicle could not be deleted." + "\n\n" + ex.Message,
                    "Loading Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion
    }
}
