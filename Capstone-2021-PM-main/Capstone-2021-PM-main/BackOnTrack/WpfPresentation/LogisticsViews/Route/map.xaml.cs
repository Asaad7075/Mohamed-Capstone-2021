using BingMapsRESTToolkit;
using BingMapsRESTToolkit.Extensions;
using LogicInterfaces;
using LogicLayer;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
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

namespace WpfPresentation.LogisticsViews.Route
{
    /// <summary>
    /// Interaction logic for map.xaml
    /// </summary>
    public partial class map : Page
    {
        private string bingMapsKey = System.Configuration.ConfigurationManager.AppSettings.Get("BingMapKey");
        private string sessionKey;
        private Regex CoordinateRx = new Regex(@"^[\s\r\n\t]*(-?[0-9]{0,2}(\.[0-9]*)?)[\s\t]*,[\s\t]*(-?[0-9]{0,3}(\.[0-9]*)?)[\s\r\n\t]*$");
        public map()
        {
            InitializeComponent();

            myMap.CredentialsProvider = new ApplicationIdCredentialsProvider(bingMapsKey);
            myMap.CredentialsProvider.GetCredentials((c) =>
            {
                sessionKey = c.ApplicationId;

                //Generate a request URL for the Bing Maps REST services.  
                //Use the session key in the request as the Bing Maps key  
            });
            CalculateRoute();
        }

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
            IDeliveryTicketManager ticketManager = new DeliveryTicketManager();

            var tickets = ticketManager.RetrieveAllTickets();
            var waypoints = new List<SimpleWaypoint>();


            
            foreach (var t in tickets)
            {
                string location = t.StreetAddressLineOne + " " + t.StreetAddressLineTwo + ", " + t.State + ", " + t.ZipCode;
                waypoints.Add(new SimpleWaypoint(location));
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
    }
}
