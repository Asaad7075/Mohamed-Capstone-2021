using DomainModels;
using LogicInterfaces;
using LogicLayer;
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

namespace WpfPresentation.LogisticsViews.Route
{
    /// <summary>
    /// Interaction logic for pageDriverRouteList.xaml
    /// </summary>
    public partial class pageDriverRouteList : Page
    {
        private IRouteManager _routeManager = new RouteManager();
        private string pageName = "Driver Route List";
        public string PageName { get { return pageName; } }
        public Employee _user;
        public pageDriverRouteList()
        {
            InitializeComponent();

        }

        private void dgDriverRoutes_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
                _user = mainWindow.User;
                dgDriverRoutes.ItemsSource = _routeManager.RetrieveRoutesByDriverID(_user.EmployeeID);
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex.Message);
            }
            
        }

        private void btnStartRoute_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new pageRouteNav((RouteVM)dgDriverRoutes.SelectedItem));
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            dgDriverRoutes.ItemsSource = _routeManager.RetrieveRoutesByDriverID(_user.EmployeeID);
        }
    }
}
