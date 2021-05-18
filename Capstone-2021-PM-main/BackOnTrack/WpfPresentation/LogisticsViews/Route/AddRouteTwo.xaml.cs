using BingMapsRESTToolkit;
using BingMapsRESTToolkit.Extensions;
using DataAccessFakes;
using DomainModels;
using DomainModels.Tickets;
using LogicInterfaces;
using LogicLayer;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using Point = System.Windows.Point;

namespace WpfPresentation.LogisticsViews.Route
{
    /// <summary>
    /// Zach Stultz
    /// Created: 2021/02/26
    ///
    /// Add Route Two.
    /// <summary>
    public partial class AddRouteTwo : Page, INotifyPropertyChanged
    {
        private IRouteManager _routeManager = new RouteManager();
        
        private IPickUpTicketManager _pickupTicketManager = new PickUpTicketManager();
        private IDeliveryTicketManager _deliveryTicketManager = new DeliveryTicketManager();
        private IRideTicketManager _rideTicketManager = new RideTicketManager();
        private bool isEditMode = false;
        private string bingMapsKey = System.Configuration.ConfigurationManager.AppSettings.Get("BingMapKey");
        private string sessionKey;
        private Regex CoordinateRx = new Regex(@"^[\s\r\n\t]*(-?[0-9]{0,2}(\.[0-9]*)?)[\s\t]*,[\s\t]*(-?[0-9]{0,3}(\.[0-9]*)?)[\s\r\n\t]*$");
        private string pageName = "Route Planner";
        public string PageName { get { return pageName; } }

        private RouteVM routeOldCopy;
        private RouteVM route;
        public RouteVM Route {
            get
            {
                if(route == null)
                {
                    route = new RouteVM();
                    route.RouteID = -1;//_routeManager.GetNextRouteID(DateOfRoute);
                }
                return route;
            }
            set
            {
                route = value;
                RaisePropertyChanged("Route");
            } 
        }
        private DateTime _dateOfRoute = DateTime.Today.AddDays(1);
        public DateTime DateOfRoute 
        {
            get
            {
                return _dateOfRoute;
            }
            set
            {
                _dateOfRoute = value;
                RaisePropertyChanged("DateOfRoute");
            }
        }
        private ObservableCollection<DomainModels.Vehicle> vehicles;
        public IEnumerable Vehicles
        {
            get
            {
                if (vehicles == null)
                {
                    IVehicleManager vehicleManager = new VehicleManager();
                    vehicles = new ObservableCollection<DomainModels.Vehicle>(vehicleManager.RetrieveAllVehicles());
                }
                return vehicles;
            }
        }
        private DomainModels.Vehicle selectedVehicle;
        public DomainModels.Vehicle SelectedVehicle
        {
            get
            {
                return selectedVehicle;
            }
            set
            {
                selectedVehicle = value;
                RaisePropertyChanged("SelectedVehicle");
            }
        }
        private ObservableCollection<EmployeeVM> drivers;
        public IEnumerable Drivers
        {
            get
            {
                if (drivers == null)
                {
                    IEmployeeManager employeeManager = new EmployeeManager();
                    drivers = new ObservableCollection<EmployeeVM>(employeeManager.RetrieveAllDrivers());
                }
                return drivers;
            }
        }
        private EmployeeVM selectedDriver;
        public EmployeeVM SelectedDriver
        {
            get
            {
                return selectedDriver;
            }
            set
            {
                selectedDriver = value;
                RaisePropertyChanged("SelectedDriver");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
        public AddRouteTwo()
        {
            StartUp();
            route = new RouteVM();
        }
        public AddRouteTwo(DateTime selectedDate)
        {
            StartUp();
            DateOfRoute = selectedDate;
        }
        public AddRouteTwo(RouteVM route)
        {
            Route = route;
            StartUp();           
            isEditMode = true;
            EnableOldCopy();
        }
        /// <summary>
        /// Jakub Kawski
        /// 2021/04/26
        /// 
        /// runs at start of every constructor for the page.
        /// </summary>
        private void StartUp()
        {
            InitializeComponent();
            DataContext = this;
            myMap.CredentialsProvider = new ApplicationIdCredentialsProvider(bingMapsKey);
            myMap.CredentialsProvider.GetCredentials((c) =>
            {
                sessionKey = c.ApplicationId;

                //Generate a request URL for the Bing Maps REST services.  
                //Use the session key in the request as the Bing Maps key  
            });
        }

        #region ~Route Stuff
        private void btnSaveRoute_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!isEditMode)
                {
                    if(SelectedDriver != null && SelectedVehicle != null)
                    {
                        Route = _routeManager.AddRoute(DateOfRoute, SelectedVehicle.LicensePlateNumber, SelectedDriver.EmployeeID);
                        isEditMode = true;
                        EnableOldCopy();
                        UpdateTickets();
                    }
                }
                else
                {
                    if(_routeManager.EditRoute(Route, routeOldCopy))
                    {
                        EnableOldCopy();
                        UpdateTickets();
                    }                  
                }
                MessageBox.Show("Route Save Succefull");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Save failed " + ex.Message);
            }
        }
        private void EnableOldCopy()
        {
            if(Route != null)
            {
                routeOldCopy = new RouteVM
                {
                    RouteID = route.RouteID,
                    LicensePlateNumber = route.LicensePlateNumber,
                    DateOfRoute = route.DateOfRoute,
                    Active = route.Active,
                    DriverEmployeeID = route.DriverEmployeeID
                };
            }
        }
        #endregion

        #region Ticket ListBox Methods/Prop/Classes

        private ObservableCollection<Ticket> _unassignedTickets;
        private ObservableCollection<Ticket> _assignedTickets;
        private List<Ticket> _ticketsToBeUnassigned = new List<Ticket>();
        ListBox dragSource = null; //remember which Listbox Tickettile was dragged from
        private bool _isDragging = false;
        private Point _clickPoint;
        private Ticket _dragTicket;
        private int _dragSourceIndex;

        private void cboTicketType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PopulateUnassignedTickets();
        }
        private void UpdateTicketsToBeUnassigned()
        {
            //check for tickets to be unassigned
            foreach (var ticket in _unassignedTickets)
            {
                if (ticket.WasEdited)
                {
                    if (!_ticketsToBeUnassigned.Contains(ticket))
                    {
                        _ticketsToBeUnassigned.Add(ticket);
                    }
                }
            }
        }
        private void PopulateUnassignedTickets()
        {
            UpdateTicketsToBeUnassigned();
            //clear list
            _unassignedTickets.Clear();

            int ticketType = Int32.Parse(((ComboBoxItem)cboTicketType.SelectedItem).Tag.ToString());
            int count = _assignedTickets.Count();
            int[] ticketIDs = new int[count];

            //set array of ticketIDs on the route
            foreach (var ticket in _assignedTickets)
            {
                count--;
                ticketIDs[count] = ticket.TicketID;
            }
            switch (ticketType)
            {
                case (int)TicketType.Delivery:
                    foreach (var ticket in _deliveryTicketManager.RetrieveAllTickets())
                    {
                        if (ticket.RouteID == null && !ticketIDs.Contains(ticket.TicketID))
                        {
                            _unassignedTickets.Add(ticket);
                        }
                    }
                    break;
                case (int)TicketType.PickUp:
                    var pickupTickets = _pickupTicketManager.RetrievePickUpTicketsByDate(_dateOfRoute);
                    foreach (var ticket in pickupTickets)
                    {
                        if (ticket.RouteID == null && !ticketIDs.Contains(ticket.TicketID))
                        {
                            _unassignedTickets.Add(ticket);
                        }
                    }
                    break;
                case (int)TicketType.Ride:
                    foreach (var ticket in _rideTicketManager.RetrieveAllTickets())
                    {
                        if (ticket.RouteID == null && !ticketIDs.Contains(ticket.TicketID))
                        {
                            _unassignedTickets.Add(ticket);
                        }
                    }
                    break;
            }
        }
        private void RefreshAssignedTickets()
        {
            try
            {
                Route = _routeManager.RetrieveRouteByID(route.RouteID);
                _assignedTickets.Clear();
                foreach (var ticket in Route.Tickets)
                {
                    _assignedTickets.Add(ticket);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RefreshAssignedTickets Failed: " + ex.Message + ex.InnerException.Message);
            }
        }
        private void lstUnassassignedTickets_Loaded(object sender, RoutedEventArgs e)
        {
            _unassignedTickets = new ObservableCollection<Ticket>();
            lstUnassignedTickets.ItemsSource = _unassignedTickets;
        }
        private void lstAssignedTickets_Loaded(object sender, RoutedEventArgs e)
        {
            _assignedTickets = new ObservableCollection<Ticket>();
            lstAssignedTickets.ItemsSource = _assignedTickets;
            if (isEditMode)
            {
                RefreshAssignedTickets();
            }
        }

        private void UpdateTickets()
        {
            try
            {

                UpdateTicketsToBeUnassigned();
                foreach (var ticket in _ticketsToBeUnassigned)
                {
                    ticket.RouteID = null;
                    ticket.StatusID = 1; // "Waiting Assignment"
                }
                _routeManager.UpdateTickets(_ticketsToBeUnassigned);

                int stopNumber = 0;
                foreach (var ticket in _assignedTickets)
                {
                    ticket.RouteID = Route.RouteID;
                    if (ticket.StopNumber != stopNumber)
                    {
                        if (!ticket.WasEdited)
                        {
                            ticket.EnableCopy();
                        }
                        ticket.StopNumber = stopNumber;
                        ticket.StatusID = 2; // "Assigned"
                    }

                    stopNumber++;
                }
                _routeManager.UpdateTickets(_assignedTickets.ToList());
                RefreshAssignedTickets();

                PopulateUnassignedTickets();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Updating tickets threw an error: " + ex.Message + ex.InnerException);
            }

        }
        /// <summary>
        /// Jakub Kawski
        /// 2021/04/24
        /// 
        /// Code source: https://social.msdn.microsoft.com/Forums/vstudio/en-US/a2e48d73-76af-4385-9ffe-d98c0ae54288/double-click-drag-amp-drop-event-conflict?forum=wpf
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstTickets_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point currentPosition = e.GetPosition(this);
                double distanceX = Math.Abs(_clickPoint.X - currentPosition.X);
                double distanceY = Math.Abs(_clickPoint.Y - currentPosition.Y);
                if (distanceX > 30 || distanceY > 30)
                {
                    _isDragging = true;
                    Console.WriteLine("Dragging");
                }
            }
        }

        private void lstTickets_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && e.ClickCount == 2)
            {
                _isDragging = false;
            }
            _clickPoint = e.GetPosition(this);
        }
        private void lstTickets_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && sender is ListBox && _isDragging)
            {
                    ListBox listBox = (ListBox)sender;
                    dragSource = listBox;
                    _dragSourceIndex = listBox.SelectedIndex;
                    _dragTicket = (Ticket)listBox.SelectedItem;

                    DragDrop.DoDragDrop(dragSource, _dragTicket, DragDropEffects.Move);                             
            }
        }

        protected override void OnDrop(DragEventArgs e)
        {
            base.OnDrop(e);
        }

        private void lstTickets_Drop(object sender, DragEventArgs e)
        {
            ListBox dropBox = ((ListBox)(sender));

            _dragTicket.EnableCopy();
            
            if(dropBox.Name == "lstUnassignedTickets")
            {
                _dragTicket.RouteID = null;
            }
            if(dropBox.Name == "lstAssignedTickets")
            {
                _dragTicket.RouteID = Route.RouteID;
            }
            ((ObservableCollection<Ticket>)dropBox.ItemsSource).Add(_dragTicket);
            ((ObservableCollection<Ticket>)dragSource.ItemsSource).RemoveAt(_dragSourceIndex);
            _isDragging = false;
        }
        private void lstAssignedTickets_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(_assignedTickets != null && _assignedTickets.Count > 0 && !_isDragging)
            {
                int index = lstAssignedTickets.SelectedIndex;
                var ticket = (Ticket)lstAssignedTickets.SelectedItem;
                ticket.EnableCopy();
                _unassignedTickets.Add(ticket);
                _assignedTickets.RemoveAt(index);
            }         
        }
        private void lstUnassassignedTickets_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (_unassignedTickets != null && _unassignedTickets.Count > 0 && !_isDragging)
            {
                int index = lstUnassignedTickets.SelectedIndex;
                var ticket = (Ticket)lstUnassignedTickets.SelectedItem;
                ticket.EnableCopy();
                _assignedTickets.Add(ticket);
                _unassignedTickets.RemoveAt(index);
            }          
        }

        #endregion

        #region Map Methods
        private async void CalculateRoute()
        {
            myMap.Children.Clear(); //clear children in map

            var waypoints = GetWaypoints();

            if (waypoints.Count < 2)
            {
                MessageBox.Show("Need a minimum of 2 waypoints to calculate a route.");
                return;
            }


            try
            {
                var routeRequest = new RouteRequest()
                {
                    Waypoints = waypoints,
                    

                    WaypointOptimization = TspOptimizationType.TravelTime,

                    RouteOptions = new RouteOptions()
                    {
                        TravelMode = TravelModeType.Driving,
                        Optimize = RouteOptimizationType.TimeWithTraffic,
                        RouteAttributes = new List<RouteAttributeType>()
                        {
                            RouteAttributeType.RoutePath,
                            RouteAttributeType.ExcludeItinerary
                        }
                    },
                    BingMapsKey = bingMapsKey
                };

                var r = await routeRequest.Execute();

                RenderRouteResponse(routeRequest, r);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }
        /// <summary>
        /// Renders a route response on the map.
        /// </summary>
        private void RenderRouteResponse(RouteRequest routeRequest, Response response)
        {
            //Render the route on the map.
            if (response != null && response.ResourceSets != null && response.ResourceSets.Length > 0 &&
               response.ResourceSets[0].Resources != null && response.ResourceSets[0].Resources.Length > 0
               && response.ResourceSets[0].Resources[0] is BingMapsRESTToolkit.Route)
            {
                BingMapsRESTToolkit.Route route = response.ResourceSets[0].Resources[0] as BingMapsRESTToolkit.Route;
                var timeSpan = new TimeSpan(0, 0, (int)Math.Round(route.TravelDurationTraffic));
                /*
                if (timeSpan.Days > 0)
                {
                    OutputTbx.Text = string.Format("Travel Time: {3} days {0} hr {1} min {2} sec\r\n", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Days);
                }
                else
                {
                    OutputTbx.Text = string.Format("Travel Time: {0} hr {1} min {2} sec\r\n", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
                }
                */
                var routeLine = route.RoutePath.Line.Coordinates;
                var routePath = new LocationCollection();

                for (int i = 0; i < routeLine.Length; i++)
                {
                    routePath.Add(new Microsoft.Maps.MapControl.WPF.Location(routeLine[i][0], routeLine[i][1]));
                }

                var routePolyline = new MapPolyline()
                {
                    Locations = routePath,
                    Stroke = new SolidColorBrush(Colors.Red),
                    StrokeThickness = 3
                };

                myMap.Children.Add(routePolyline);

                var locs = new List<Microsoft.Maps.MapControl.WPF.Location>();

                //Create pushpins for the optimized waypoints.
                //The waypoints in the request were optimized for us.
                for (var i = 0; i < routeRequest.Waypoints.Count; i++)
                {
                    var loc = new Microsoft.Maps.MapControl.WPF.Location(routeRequest.Waypoints[i].Coordinate.Latitude, routeRequest.Waypoints[i].Coordinate.Longitude);

                    //Only render the last waypoint when it is not a round trip.
                    if (i < routeRequest.Waypoints.Count - 1)
                    {
                        myMap.Children.Add(new Pushpin()
                        {
                            Location = loc,
                            Content = i
                        });
                    }

                    locs.Add(loc);
                }

                myMap.SetView(locs, new Thickness(50), 0);
            }
            else if (response != null && response.ErrorDetails != null && response.ErrorDetails.Length > 0)
            {
                throw new Exception(String.Join("", response.ErrorDetails));
            }
        }
        private List<SimpleWaypoint> GetWaypoints()
        {

            var tickets = _assignedTickets.ToList();
            var waypoints = new List<SimpleWaypoint>();
            Coordinate coor = new Coordinate();
            var wp = new SimpleWaypoint();
            foreach (var t in tickets)
            {
                string location = "New York, New York";
                switch (t.TicketType)
                {
                    case (int)TicketType.Delivery:
                        var deliveryticket = (DeliveryTicketVM)t;
                        if (deliveryticket.Coordinate == null)
                        {
                            location = deliveryticket.FullAddress;
                            waypoints.Add(new SimpleWaypoint(location));
                        }
                        else
                        {
                            waypoints.Add(new SimpleWaypoint(deliveryticket.Coordinate));
                        }
                        
                        break;
                    case (int)TicketType.PickUp:
                        var pickupticket = (PickUpTicketVM)t;
                        if (pickupticket.Coordinate == null)
                        {
                            location = pickupticket.FullAddress;
                            waypoints.Add(new SimpleWaypoint(location));
                        }
                        else
                        {
                            waypoints.Add(new SimpleWaypoint(pickupticket.Coordinate));
                        }

                        break;
                    case (int)TicketType.Ride:
                        var rideticket = (RideTicketVM)t;
                        if(rideticket.FetchCoordinate == null)
                        {
                            location = rideticket.FetchFullAddress;
                            waypoints.Add(new SimpleWaypoint(location));
                        }
                        if(rideticket.DestinationCoordinate == null)
                        {
                            location = rideticket.DestinationFullAddress;
                            waypoints.Add(new SimpleWaypoint(location));
                        }
                        else
                        {
                            var swp = new SimpleWaypoint(rideticket.FetchCoordinate);
                            waypoints.Add(swp);
                            waypoints.Add(new SimpleWaypoint(rideticket.DestinationCoordinate));
                        }

                        break;
                    default:
                        MessageBox.Show("Something went wrong and a ticket didnt not have a ticket type assigned to itself.");
                        break;
                }
            }
            /*
            foreach (var p in places)
            {
                if (!string.IsNullOrWhiteSpace(p))
                {
                    var m = CoordinateRx.Match(p);

                    if (m.Success)
                    {
                        waypoints.Add(new SimpleWaypoint(double.Parse(m.Groups[1].Value), double.Parse(m.Groups[3].Value)));
                    }
                    else
                    {
                        waypoints.Add(new SimpleWaypoint(p));
                    }
                }
            }
            */
            return waypoints;
        }


        private void btnCalcRoute_Click(object sender, RoutedEventArgs e)
        {
            CalculateRoute();
        }


        #endregion

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (isEditMode)
            {
                DateOfRoute = Route.DateOfRoute;
                SelectedDriver = drivers.First(d => d.EmployeeID == Route.DriverEmployeeID);
                SelectedVehicle = vehicles.First(v => v.LicensePlateNumber == Route.LicensePlateNumber);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
