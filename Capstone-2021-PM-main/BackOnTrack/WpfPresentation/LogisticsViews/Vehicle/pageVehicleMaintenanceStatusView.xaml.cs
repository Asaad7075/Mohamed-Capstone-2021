using DataAccessFakes;
using DomainModels;
using LogicInterfaces;
using LogicLayer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for pageVehicleMaintenanceStatusView.xaml
    /// </summary>
    public partial class pageVehicleMaintenanceStatusView : Page
    {
        #region Properties
        // private IUSGovernmentTransportationManager _USGovernmentTransportationManager = new USGovernmentTransportationManager(new AutoDefectRecallFake());
        private IUSGovernmentTransportationManager _USGovernmentTransportationManager;
        private IVehicleManager _vehicleManager;
        private string pageName = "Maintenance Alerts";
        public string PageName { get { return pageName; } }
        private bool _pageLoaded;
        private bool _refreshRequested;

        private string _make;
        public string Make
        {
            get => _make;
            set
            {
                _make = value;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Chantal Shirley
        /// Created 2021/03/03
        /// 
        /// Zero-argument constructor to initialize the 
        /// vehicle maintenance status page.
        /// </summary>
        public pageVehicleMaintenanceStatusView()
        {
            InitializeComponent();
            _USGovernmentTransportationManager = new USGovernmentTransportationManager();
            _vehicleManager = new VehicleManager();
        }

        #endregion 

        #region Private Async Helper Methods
        /// <summary>
        /// Chantal Shirley
        /// Created 2021/03/03
        /// 
        /// Private helper method to help fill
        /// recall data on active vehicles in the fleet.
        /// Progress bar provides immediate feedback for user
        /// regarding loading state.
        /// </summary>
        private async void populateView()
        {
            LoadingMode();
            try
            {
                List<DomainModels.Vehicle> allVehicles = new List<DomainModels.Vehicle>();
                List<DomainModels.Vehicle> activeVehicles = new List<DomainModels.Vehicle>();
                allVehicles = _vehicleManager.RetrieveAllVehicles();

                activeVehicles = allVehicles.OrderByDescending(v => v.VehicleYear)
                                            .Where(v => v.IsActive == true).ToList();

                IEnumerable<AutoDefectRecall> autoDefectRecalls = await _USGovernmentTransportationManager.RetrieveRecallsOnActiveVehicleFleet(activeVehicles);

                lstViewDGAlerts.ItemsSource = autoDefectRecalls;
                if (lstViewDGAlerts.Items.Count > 0)
                {
                    lstViewDGAlerts.SelectedIndex = 0;
                    btnBuildReport.IsEnabled = true;
                }
                LoadedMode();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving vehicle recall data." + "\n\n" + ex.Message, "Error Pulling" +
                    "Recall Data", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/03/09
        /// 
        /// Let's user know when all vehicles have been loaded.
        /// </summary>
        private void LoadedMode()
        {
            txtBlkLoading.Visibility = Visibility.Hidden;
            pgBarStatus.IsIndeterminate = false;
            pgBarStatus.Value = 100;
            txtBlkComplete.Visibility = Visibility.Visible;
            var task = ReOrientFrameAsync();
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/03/12
        /// 
        /// Hides loading bar.
        /// </summary>
        private async Task ReOrientFrameAsync()
        {
            await Task.Delay(500);
            pgBarStatus.Visibility = Visibility.Hidden;
            Grid.SetRow(lstViewDGAlerts, 3);
            Grid.SetRowSpan(lstViewDGAlerts, 6);
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/02
        /// 
        /// Allows the user to see that their API call is
        /// requesting new data.
        /// </summary>
        private void ReverseFrame()
        {
            Grid.SetRow(lstViewDGAlerts, 4);
            Grid.SetRowSpan(lstViewDGAlerts, 5);
            txtBlkComplete.Visibility = Visibility.Hidden;
            pgBarStatus.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/03/09
        /// 
        /// Let's user know that data is being pulled.
        /// </summary>
        private void LoadingMode()
        {
            pgBarStatus.IsIndeterminate = true;
            txtBlkLoading.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/03/12
        /// 
        /// Private helper method to ensure that the 
        /// page does not load before the user is ready to
        /// interact with it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!_pageLoaded)
            {
                populateView();
                _pageLoaded = true;
            }
            else if (_refreshRequested)
            {
                populateView();
                _refreshRequested = false;
                lstViewDGAlerts.Visibility = Visibility.Visible;
            }
        }

        #endregion


        #region Event Helper Methods
        private void btnRefreshList_Click(object sender, RoutedEventArgs e)
        {
            lstViewDGAlerts.ItemsSource = null;
            ReverseFrame();
            _refreshRequested = true;
            NavigationService.Refresh();
        }
        
        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/02
        /// 
        /// Allows the maintenance worker to create 
        /// a maintenance request that is prepopulated.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBuildReport_Click(object sender, RoutedEventArgs e)
        {
            AutoDefectRecall selectedItem = (AutoDefectRecall)lstViewDGAlerts.SelectedItems[0];

            NavigationService.Navigate(new pageVehicleMaintenanceReportView(selectedItem.Make, selectedItem.Model, selectedItem.ModelYear, selectedItem.Conequence));
        }
        #endregion
    }
}
