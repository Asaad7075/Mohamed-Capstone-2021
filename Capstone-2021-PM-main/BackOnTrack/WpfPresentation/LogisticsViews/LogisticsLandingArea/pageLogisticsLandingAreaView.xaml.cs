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

namespace WpfPresentation.LogisticsViews.LogisticsLandingArea
{
    /// <summary>
    /// Interaction logic for pageLogisticsLandingAreaView.xaml
    /// </summary>
    public partial class pageLogisticsLandingAreaView : Page
    {
        private List<string> _roles;
        private List<Button> _actions;
        private MainWindow _mainWindow;

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/02/19
        /// 
        /// Initialized page which will show upon loading 
        /// after a role is passed to the landing area page.
        /// </summary>
        /// <param name="role"></param>
        public pageLogisticsLandingAreaView(MainWindow mainWindow, List<string> roles)
        {
            _roles = roles;
            _actions = new List<Button>();
            _mainWindow = mainWindow;
            InitializeComponent();
            DisplayAuthorizedActions();
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/02/19
        /// 
        /// Determines which permissions the user is entitled to
        /// based on their assigned roles.
        /// </summary>
        private void DisplayAuthorizedActions()
        {
            foreach (string role in _roles)
            {
                switch (role)
                {
                    case "Logistics Manager":
                        AccessManagerActions();
                        break;
                    case "Logistics Admin":
                        // TBD
                        break;
                    case "Logistics Maintenance":
                        AccessMaintenanceActions();
                        break;
                    case "Logistics Driver":
                        // TBD
                        break;
                    default:
                        UnauthorizedUser();
                        break;
                }
            }
            
            DisplayUserActions(_actions);
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/02/20
        /// 
        /// Dynamically generates actions authorized for
        /// maintenance workers.
        /// </summary>
        private void AccessMaintenanceActions()
        {
            var pages = Enum.GetValues(typeof(MaintenanceLogisticsViews));

            foreach (var LogisticsView in pages)
            {
                if (ButtonExist(LogisticsView)) // Avoid displaying buttons that the user already has added to their actions
                {
                    Button btnLogisticsView = new Button();
                    btnLogisticsView.Content = TextWrapping.Wrap;
                    btnLogisticsView.Width = 500;
                    btnLogisticsView.Height = 75;
                    btnLogisticsView.Padding = new Thickness(5);
                    btnLogisticsView.Margin = new Thickness(200, 25, 0, 25);
                    btnLogisticsView.Name = LogisticsView.ToString();
                    btnLogisticsView.FontSize = 25;
                    SetMaintenanceLogisticsButtonContent(LogisticsView, btnLogisticsView);

                    _actions.Add(btnLogisticsView);
                }
            }
        }

        /// <summary>
        /// Chantal Shirley
        /// Created 2/26/2021
        /// 
        /// Private helper method to avoid duplicate buttons for user's
        /// logistics actions.
        /// </summary>
        /// <param name="LogisticsView"></param>
        /// <returns></returns>
        private bool ButtonExist(object LogisticsView)
        {
            foreach (Button button in _actions)
            {
                if (button.Name == LogisticsView.ToString())
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/02/21
        /// 
        /// Determines which views are allowed and provides
        /// a human readable name for Maintenance-related button actions.
        /// </summary>
        /// <param name="logisticsView"></param>
        /// <param name="btnLogisticsView"></param>
        private void SetMaintenanceLogisticsButtonContent(object logisticsView, Button btnLogisticsView)
        {
            switch (logisticsView.ToString())
            {
                case nameof(MaintenanceLogisticsViews.pageRemoveVehicleView):
                    btnLogisticsView.Content = "Remove Vehicle";
                    btnLogisticsView.Click += removeVehicle_Click;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/02/20
        /// 
        /// Dynamically generates actions autorized for
        /// logistics managers.
        /// </summary>
        private void AccessManagerActions()
        {
            var pages = Enum.GetValues(typeof(ManagerLogisticsViews));

            foreach (var LogisticsView in pages)
            {
                if (ButtonExist(LogisticsView))// Avoid displaying buttons that the user already has added to their actions
                {
                    Button btnLogisticsView = new Button();
                    btnLogisticsView.Content = TextWrapping.Wrap;
                    btnLogisticsView.Width = 500;
                    btnLogisticsView.Height = 75;
                    btnLogisticsView.Padding = new Thickness(5);
                    btnLogisticsView.Margin = new Thickness(200, 25, 0, 25);
                    btnLogisticsView.Name = LogisticsView.ToString();
                    btnLogisticsView.FontSize = 25;
                    SetManagerLogisticsButtonContent(LogisticsView, btnLogisticsView);

                    _actions.Add(btnLogisticsView);
                }
            }
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/02/20
        /// 
        /// Determines which views are allowed and provides
        /// a human readable name for Manager-related button actions.
        /// </summary>
        /// <param name="logisticsView"></param>
        /// <param name="btnLogisticsView"></param>
        private void SetManagerLogisticsButtonContent(object logisticsView, Button btnLogisticsView)
        {
            switch (logisticsView.ToString())
            {
                case nameof(ManagerLogisticsViews.pageAddDriversLicenseView):
                    btnLogisticsView.Content = "Add Drivers License";
                    btnLogisticsView.Click += addDriversLicense_Click;
                    break;
                case nameof(ManagerLogisticsViews.pageRemoveVehicleView):
                    btnLogisticsView.Content = "Remove Vehicle";
                    btnLogisticsView.Click += removeVehicle_Click;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/02/21
        /// 
        /// Sets the click event for the button being dynamically generated
        /// for removing vehicles. Injects the page into the frame of the main window
        /// for removing vehicles in the logistics tab.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void removeVehicle_Click(object sender, RoutedEventArgs e)
        {
            ClearLogisticsFrameContent();
            _mainWindow.frameLogistics.Content = new pageRemoveVehicleView(_mainWindow);
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/02/21
        /// 
        /// Clears previous content from the logistics frame.
        /// </summary>
        private void ClearLogisticsFrameContent()
        {
            _mainWindow.frameLogistics.Content = null;
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/02/20
        /// 
        /// Sets the click event for the button being dynamically generated
        /// for adding drivers licenses. Injects the page into the frame of the main window for
        /// adding vehicles in the logistics tab.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addDriversLicense_Click(object sender, RoutedEventArgs e)
        {
            ClearLogisticsFrameContent();
            _mainWindow.frameLogistics.Content = new pageAddDriversLicenseView(_mainWindow);
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/02/20
        /// 
        /// A private helper method for adding buttons
        /// to the logistics landing area.
        /// </summary>
        /// <param name="actions"></param>
        private void DisplayUserActions(List<Button> actions)
        {
            foreach (Button button in actions)
            {
                wrapPanelLogisticsLandingArea.Children.Add(button);
            }
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/02/19
        /// 
        /// Displays text indicating that they do
        /// not have the correct permissions to take
        /// any actions related to logistics.
        /// </summary>
        private void UnauthorizedUser()
        {
            TextBlock txtBlockAccessViolation = new TextBlock();
            txtBlockAccessViolation.VerticalAlignment = VerticalAlignment.Center;
            txtBlockAccessViolation.HorizontalAlignment = HorizontalAlignment.Center;
            txtBlockAccessViolation.FontSize = 40;
            txtBlockAccessViolation.FontWeight = FontWeights.Bold;
            txtBlockAccessViolation.FontFamily = new FontFamily("Helvetica");
            txtBlockAccessViolation.Margin = new Thickness(275, 25, 0, 0);
            txtBlockAccessViolation.Text = "Sorry, you do not have access\nto Logistics Portal actions.";
            wrapPanelLogisticsLandingArea.Children.Add(txtBlockAccessViolation);
        }
    }

    /// <summary>
    /// Chantal Shirley
    /// Created: 2021/02/20
    /// 
    /// Allows for easy enumeration and assignment
    /// for Manager-exclusive pages.
    /// </summary>
    enum ManagerLogisticsViews
    {
        pageAddDriversLicenseView,
        pageRemoveVehicleView
    }

    /// <summary>
    /// Chantal Shirley
    /// Created: 2021/02/21
    /// 
    /// Allows for easy enumeration and assignment
    /// for Maintenance-exclusive pages.
    /// </summary>
    enum MaintenanceLogisticsViews
    {
        pageRemoveVehicleView
    }
}
