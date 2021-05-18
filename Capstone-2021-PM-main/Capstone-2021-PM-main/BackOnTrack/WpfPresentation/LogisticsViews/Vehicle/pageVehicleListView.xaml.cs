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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfPresentation.LogisticsViews.Vehicle
{
    /// <summary>
    /// Chantal Shirley
    /// Redesigned: 2021/03/12
    /// Interaction logic for pageVehicleListView.xaml
    /// </summary>
    public partial class pageVehicleListView : Page, INotifyPropertyChanged
    {
        #region Properties
        // private IVehicleManager _vehicleManager = new VehicleManager(new VehicleFake()); // For Testing purposes
        private IVehicleManager _vehicleManager;
        private string pageName = "Vehicle Fleet";
        private ObservableCollection<VehicleVM> _vehicles;

        public event PropertyChangedEventHandler PropertyChanged;

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
                NotifyPropertyChanged("Vehicles");
            }
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/03/22
        /// </summary>
        /// <param name="v"></param>
        private void NotifyPropertyChanged(string v)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(v));
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/03/12
        /// </summary>
        public pageVehicleListView()
        {
            InitializeComponent();
            _vehicleManager = new VehicleManager();
            _vehicles = new ObservableCollection<VehicleVM>();
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
                if (Vehicles.Count > 0)
                {
                    lstViewVehicles.SelectedIndex = 0; // Default position
                }
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
                PopulateView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Vehicle could not be deleted." + "\n\n" + ex.Message,
                    "Loading Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/03/22
        ///
        /// Makes new call against database to refresh
        /// page to reflect most recent updates.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefreshList_Click(object sender, RoutedEventArgs e)
        {
            PopulateView();
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/03/22
        ///
        /// Allows the user to
        /// update restricted vehicle
        /// details.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnModifyVehicle_Click(object sender, RoutedEventArgs e)
        {
            if ((VehicleVM)lstViewVehicles.SelectedValue != null)
            {
                var editView = new EditVehicleView((VehicleVM)lstViewVehicles.SelectedValue);
                editView.ShowDialog();
                // Refresh List
                PopulateView();
            }
            else
            {
                MessageBox.Show("You must make a selection before modifying.",
                    "Selection error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

    }
}
