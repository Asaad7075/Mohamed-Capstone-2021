using DomainModels;
using DomainModels.Tickets;
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

namespace WpfPresentation.LogisticsViews.Route
{
    /// <summary>
    /// Zach Stultz
    /// Created: 2021/02/26
    ///
    /// Add Route class.
    /// <summary>
    public partial class AddRoute : Page
    {
        private string pageName = "Route Calendar";
        private static int totalDeliveriesNum = 0;
        private static int totalShuttleRequestsNum = 0;
        private static int totalDonationPickupsNum = 0;

        private IRouteManager _routeManager;
        private IVehicleManager _vehicleManager;
        private IDeliveryTicketManager _deliveryTicketManager;
        private IPickUpTicketManager _pickUpTicketManager;
        private IRideTicketManager _rideTicketManager;
        private IEmployeeManager _employeeManager;

        private ObservableCollection<RouteVM> _routes = new ObservableCollection<RouteVM>();
        private List<DeliveryTicketVM> _deliveryTickets = new List<DeliveryTicketVM>();
        private List<PickUpTicketVM> _pickUpTickets = new List<PickUpTicketVM>();
        private List<RideTicketVM> _rideTickets  = new List<RideTicketVM>();
        private List<DomainModels.Vehicle> _vehicles = new List<DomainModels.Vehicle>();
        private List<EmployeeVM> _employees = new List<EmployeeVM>();



        /// <summary>
        /// Gets the page name.
        /// </summary>
        public string PageName { get { return pageName; } }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/02/26
        ///
        /// Default Constructor, sets stuff up.
        /// Initializes a new instance of the <see cref="AddRoute"/> class.
        /// </summary>
        public AddRoute()
        {
            InitializeComponent();
            this.DataContext = this;
            lstRoutes.ItemsSource = _routes;
            _routeManager = new RouteManager();
            _employeeManager = new EmployeeManager();

            _employees = _employeeManager.RetrieveEmployeesByActive(true);
            lblCurrentDate.Content = "Today's Date: " + DateTime.Now.ToString("MM/dd/yyyy");
            PopulateListBox();
        }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/04/01
        ///
        /// Transfers the user to the next page to create a route.
        /// Edit: Jakub Kawski
        /// 2021/04/26
        /// 
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void btnCreateRoute_Click(object sender, RoutedEventArgs e)
        {
            if(cDatePicker.SelectedDate != null)
            {
                //date is sleected
                DateTime selectedDate = (DateTime)cDatePicker.SelectedDate;
                this.NavigationService.Navigate(new AddRouteTwo(selectedDate));
            } else
            {
                //no date selected
                MessageBox.Show("Please select a date for the route");
            }
            
        }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/04/01
        ///
        /// Action taken when Edit button is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void btnEditRoute_Click(object sender, RoutedEventArgs e)
        {
            var selected = (RouteVM)lstRoutes.SelectedItem;
            if(selected != null)
            {
                this.NavigationService.Navigate(new AddRouteTwo(selected));
            }
            else
            {
                MessageBox.Show("Select a route to edit");
            }
            
        }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/04/01
        ///
        /// Action taken when Delete button is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void btnDeleteRoute_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selected = (RouteVM)lstRoutes.SelectedItem;
                if (selected != null)
                {
                    int result = _routeManager.DeleteRoute(selected);
                    PopulateListBox();
                }
                else
                {
                    MessageBox.Show("Select a route to delete");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Route could not be deleted." + "\n\n" + ex.Message);
            }
        }


        /// <summary>
        /// Zach Stultz
        /// Created: 2021/04/07
        ///
        /// Action taken when Mouse Down is pressed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void cDatePicker_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PopulateListBox();
        }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/04/07
        ///
        /// Populates the list box.
        /// </summary>
        private void PopulateListBox()
        {
            _routes.Clear();
            if (cDatePicker.SelectedDate.HasValue)
            {
                foreach (var route in _routeManager.RetrieveRoutesByDate(cDatePicker.SelectedDate.Value))
                {
                    _routes.Add(route);
                }
            }
            else
            {
                foreach (var route in _routeManager.RetrieveAllRoutes())
                {
                    _routes.Add(route);
                }
            }
            
            SetExtras();
        }

        /// <summary>
        /// Zach Stultz
        /// Created: 2021/04/09
        ///
        /// Sets the interface VM class extras if tickets were made and associated with a matching
        /// RouteID, but values were not set on the Route classes' extras upon time of creation.
        /// </summary>
        private void SetExtras()
        {
            try
            {
                TicketMetaData ticketMetaData = _routeManager.GetTicketMetaData();
                txtTotalDeliveries.Text = ticketMetaData.DeliveryUnassigned.ToString();
                txtTotalDonationPickups.Text = ticketMetaData.PickupUnassigned.ToString();
                txtTotalShuttleRequests.Text = ticketMetaData.RideUnassigned.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error setting data in SetExtras()" + "\n\n" + ex.Message);
            }
        }

        private void btnRefreshRoutes_Click(object sender, RoutedEventArgs e)
        {
            PopulateListBox();
        }
    }
}
