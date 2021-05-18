using BingMapsRESTToolkit;
using BingMapsRESTToolkit.Extensions;
using DomainModels;
using DomainModels.Tickets;
using LogicInterfaces;
using LogicLayer;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using WpfPresentation.LogisticsViews.Tickets;

namespace WpfPresentation.LogisticsViews.Route
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class pageRouteNav : Page
    {
        private IRouteManager _routeManager;
        private RouteVM _route;
        private string pageName = "Route Nav";
        public string PageName { get { return pageName; } }
        private string bingMapsKey = System.Configuration.ConfigurationManager.AppSettings.Get("BingMapKey");
        private string sessionKey;
        private Regex CoordinateRx = new Regex(@"^[\s\r\n\t]*(-?[0-9]{0,2}(\.[0-9]*)?)[\s\t]*,[\s\t]*(-?[0-9]{0,3}(\.[0-9]*)?)[\s\r\n\t]*$");
        private ObservableCollection<Ticket> _routeTickets;

        public pageRouteNav(RouteVM route)
        {
            InitializeComponent();
            _route = route;
            DataContext = this;
            myMap.CredentialsProvider = new ApplicationIdCredentialsProvider(bingMapsKey);
            myMap.CredentialsProvider.GetCredentials((c) =>
            {
                sessionKey = c.ApplicationId;

                //Generate a request URL for the Bing Maps REST services.  
                //Use the session key in the request as the Bing Maps key  
            });                     
        }
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

            var tickets = _routeTickets.ToList();
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
                            //coordinate logic here
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
                            //coordinate logic here
                        }

                        break;
                    case (int)TicketType.Ride:
                        var rideticket = (RideTicketVM)t;
                        if (rideticket.FetchCoordinate == null)
                        {
                            location = rideticket.FetchFullAddress;
                            waypoints.Add(new SimpleWaypoint(location));
                        }
                        if (rideticket.DestinationCoordinate == null)
                        {
                            location = rideticket.DestinationFullAddress;
                            waypoints.Add(new SimpleWaypoint(location));
                        }
                        else
                        {
                            //coordinate logic here
                        }

                        break;
                    default:
                        MessageBox.Show("Something went wrong and a ticket didnt not have a ticket type assigned to itself.");
                        break;
                }
            }
            return waypoints;
        }
        #endregion

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _routeManager = new RouteManager();
            _routeTickets = new ObservableCollection<Ticket>();
            lstTickets.ItemsSource = _routeTickets;
            try
            {
                var route = _routeManager.RetrieveRouteByID(_route.RouteID);
                foreach (var ticket in route.Tickets)
                {
                    _routeTickets.Add(ticket);
                }
                CalculateRoute();
            }
            catch (Exception ex)
            {
                MessageBox.Show("RefreshAssignedTickets Failed: " + ex.Message + ex.InnerException.Message);
            }
        }

        private void btnUpdateTicket_Click(object sender, RoutedEventArgs e)
        {
            if(lstTickets.SelectedItem != null)
            {
                Ticket ticket = (Ticket)lstTickets.SelectedItem;
                switch (ticket.TicketType)
                {
                    case (int)TicketType.Delivery:
                        this.NavigationService.Navigate(new pageDeliveryTicketForm((DeliveryTicketVM)ticket));
                        break;
                    case (int)TicketType.PickUp:
                        this.NavigationService.Navigate(new pagePickUpTicketForm((PickUpTicketVM)ticket));
                        break;
                    case (int)TicketType.Ride:
                        this.NavigationService.Navigate(new pageRideTicketForm((RideTicketVM)ticket));
                        break;
                    default:
                        MessageBox.Show("Something went wrong and a ticket didnt not have a ticket type assigned to itself.");
                        break;
                }
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
