using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using DataAccessFakes;
using DomainModels;
using LogicInterfaces;
using LogicLayer;

namespace WpfPresentation
{
    /// <summary>
    /// Interaction logic for pageRemoveVehicleView.xaml
    /// </summary>
    public partial class pageRemoveVehicleView : Page
    {
        // private IVehicleManager _vehicleManager = new VehicleManager(new VehicleFake()); // For Testing purposes
        private IVehicleManager _vehicleManager;
        private List<Button> _carButtons;
        private Vehicle chosenVehicle;
        private List<Vehicle> _vehicles;
        private List<Button> _generalButtons;
        private MainWindow _mainWindow;

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/02/11
        /// 
        /// Single-Argument Constructor that allows the user to dynamically
        /// remove vehicles and return to the home screen.
        /// </summary>
        public pageRemoveVehicleView(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            _carButtons = new List<Button>();
            _generalButtons = new List<Button>();
            _vehicleManager = new VehicleManager();
            // Allow buttons to be accessible when dynamically generated
            EventManager.RegisterClassHandler(typeof(Button), Button.ClickEvent, new RoutedEventHandler(Button_Click));
            DisplayAllVehicles(); // Display vehicles and accompanying buttons
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/02/12
        /// 
        /// Logic for displaying all the vehicles.
        /// </summary>
        private void DisplayAllVehicles()
        {
            try
            {
                _vehicles = _vehicleManager.RetrieveAllVehicles(); // Get vehicles
            }
            catch (Exception ex)
            {
                MessageBox.Show("Vehicles data was not found. See the follwoing message:" +
                    "\n\n" + ex.Message, "Vehicle Data Not Found", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            int carCounter = 0; // A flag that determines whether any vehicles pulled were active vehicles

            // If there are vehicles to delete display them
            if (_vehicles.Count > 0)
            {
                // Convert active vehicles into dynamic buttons to remove
                foreach (Vehicle vehicle in _vehicles)
                {
                    if (vehicle.IsActive == true)
                    {
                        carCounter++;
                        CreateVehicleButton(carCounter, vehicle);
                    }
                }
                if (carCounter > 0)
                {
                    DisplayRemoveVehicleTitles();
                    // Set the delete button
                    Button delete = new Button();
                    CreateDeleteButton(delete);
                    delete.IsEnabled = true;
                    CreateCancelButton(new Button());
                }
                else
                {
                    DisplayAlternativeTextForNoVehicles();
                }
            }
            else // There are no vehicles to show; display alternative text
            {
                DisplayAlternativeTextForNoVehicles();
            }

        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/02/14
        /// 
        /// Helper method for creating unique vehicle buttons.
        /// </summary>
        /// <param name="carCounter"></param>
        /// <param name="vehicle"></param>
        private void CreateVehicleButton(int carCounter, Vehicle vehicle)
        {
            Button button = new Button();
            button.Name = "Car" + carCounter.ToString() + vehicle.VinNumber;
            button.Width = 500;
            button.Height = 75;
            button.Padding = new Thickness(5);
            button.Margin = new Thickness(200, 25, 0, 25);
            TextBlock tb = new TextBlock();
            tb.Inlines.Add(new Run("Year: " + vehicle.VehicleYear + " | Make and Model: " + vehicle.VehicleMake
                + " " + vehicle.VehicleModel + " | Vin: " + vehicle.VinNumber + " | License Plate: " + vehicle.LicensePlateNumber));
            tb.TextWrapping = TextWrapping.Wrap;
            tb.TextAlignment = TextAlignment.Center;
            button.Content = tb;
            //button.Content = "Year: " + vehicle.VehicleYear + " | Make and Model: " + vehicle.VehicleMake
            //    + " " + vehicle.VehicleModel + " | Vin: " + vehicle.VinNumber + " | License Plate: " + vehicle.LicensePlateNumber; 
            wrapPanelVehicleContainer.Children.Add(button);
            _carButtons.Add(button);
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/02/13
        /// 
        /// Helper method for creating a cancel button.
        /// </summary>
        /// <param name="cancel"></param>
        private void CreateCancelButton(Button cancel)
        {
            cancel.Name = "btnCancelRemoveVehicle";
            cancel.Content = "Cancel";
            cancel.Width = 200;
            cancel.Height = 50;
            cancel.Margin = new Thickness(25, 30, 0, 0);
            cancel.FontFamily = new FontFamily("Helvetica");
            cancel.Padding = new Thickness(5);
            cancel.FontSize = 25;
            cancel.HorizontalContentAlignment = HorizontalAlignment.Center;
            cancel.VerticalContentAlignment = VerticalAlignment.Center;
            cancel.Click += logisticscRemoveVehicleCancel_Click;
            wrapPanelVehicleContainer.Children.Add(cancel);
            _generalButtons.Add(cancel);
        }

        /// <summary>
        /// Chantal Shilrey
        /// 
        /// Helper Method for returning the user to the Maintenance landing page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void logisticscRemoveVehicleCancel_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.frameLogistics.Content = null;
            _mainWindow.TestLogisticsAccessAfterLogin();            
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/02/13
        /// 
        /// Helper method for creating a delete button.
        /// </summary>
        /// <param name="delete"></param>
        private void CreateDeleteButton(Button delete)
        {
            delete.Name = "btnDeleteRemoveVehicle";
            delete.Content = "Delete";
            delete.Width = 200;
            delete.Margin = new Thickness(450, 30, 0, 0);
            delete.FontFamily = new FontFamily("Helvetica");
            delete.Padding = new Thickness(5);
            delete.FontSize = 25;
            delete.IsEnabled = false;
            delete.HorizontalContentAlignment = HorizontalAlignment.Center;
            delete.VerticalContentAlignment = VerticalAlignment.Center;
            wrapPanelVehicleContainer.Children.Add(delete);
            _generalButtons.Add(delete);
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/02/13
        /// 
        /// Helper method to display alternative text when there are no vehicles to show.
        /// </summary>
        private void DisplayAlternativeTextForNoVehicles()
        {
            TextBlock textBlockNoVehicles = new TextBlock();
            textBlockNoVehicles.VerticalAlignment = VerticalAlignment.Center;
            textBlockNoVehicles.HorizontalAlignment = HorizontalAlignment.Center;
            textBlockNoVehicles.FontSize = 40;
            textBlockNoVehicles.FontWeight = FontWeights.Bold;
            textBlockNoVehicles.FontFamily = new FontFamily("Helvetica");
            textBlockNoVehicles.Margin = new Thickness(275, 25, 0, 0);
            textBlockNoVehicles.Text = "Sorry, there are no\n vehicles to remove!";
            wrapPanelVehicleContainer.Children.Add(textBlockNoVehicles);
            Button cancel = new Button();
            CreateCancelButton(cancel);
            cancel.Margin = new Thickness(500, 50, 0, 0);
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/02/12
        /// 
        /// Helper method to display remove vehicle instructions and title.
        /// </summary>
        private void DisplayRemoveVehicleTitles()
        {
            // Display relevant information
            txtBlkTitleRemoveVehicle.Visibility = Visibility.Visible;
            txtBlkBodyRemoveVehicle.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/02/12
        /// 
        /// Event handler for all buttons clicked; needed because buttons are dynamically
        /// generated depending ont the amount of the vehicles contained by the data warehouse.
        /// </summary>
        /// <param name="sender">Button clicked by the user</param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btnClicked = (Button)sender; // Button clicked by the user
            Button deletedCar = null;

            // Check cars chosen to be deleted and disables it, forcing user
            // to delete one car at a time
            if (btnClicked.Name.Contains("Car"))
            {
                DetermineCarRemoval(btnClicked);
            }

            // If the user has chosen to delete a vehicle ensure that they can do so
            if (btnClicked.Name.Contains("Delete") && _carButtons.Count > 0)
            {
                // Find the car they have chosen to delete, it will be the only car button disabled and visible
                foreach (Button carButton in _carButtons)
                {
                    if (carButton.IsEnabled == false && carButton.Visibility == Visibility.Visible)
                    {
                        deletedCar = carButton;
                        break;
                    }
                }
                // If they accidentally chose to delete a car without choosing one, guide the user by informing them
                if (deletedCar == null)
                {
                    MessageBox.Show("You have not selected a car to delete!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else // Otherwise allow user to delete car
                {
                    try
                    {
                        bool success = DeleteVehicle(deletedCar);
                        if (success) // No errors have been thrown, inform them of the successful deletion
                        {
                            MessageBox.Show("The vehicle with vin number: " + chosenVehicle.VinNumber + " has been deleted!", "Successful deletion!",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                        }

                    }
                    catch (Exception ex) // Inform user of errors
                    {
                        MessageBox.Show("Vehicle could not be deleted!\n\nSee error message: " + ex.Message + ex.InnerException,
                            "Deletion error!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            // If the user has just deleted the last car, remove the delete button
            if (_carButtons.Count == 0 && btnClicked.Name.Contains("Delete"))
            {
                ResetPageWithNoDeletOption();
            }
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/02/21
        /// 
        /// Resets the page after a check has been done
        /// to determine if there are no more cars to delete.
        /// </summary>
        private void ResetPageWithNoDeletOption()
        {
            HideTitleAndInstructions();
            DisplayAlternativeTextForNoVehicles();

            // Remove Delete button & cancel button
            foreach (Button button in _generalButtons)
            {
                if (wrapPanelVehicleContainer.Children.Contains(button))
                {
                    wrapPanelVehicleContainer.Children.Remove(button);
                }
            }

            // Insert new cancel button
            Button cancel = new Button();
            CreateCancelButton(cancel);
            cancel.Margin = new Thickness(500, 150, 0, 0);
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/02/21
        /// 
        /// Allows the user to attempt to delete a vehicle and
        /// returns a bool for whether or not the deletion was
        /// successful.
        /// </summary>
        /// <param name="deletedCar"></param>
        /// <returns></returns>
        private bool DeleteVehicle(Button deletedCar)
        {
            bool success = false; // Flag for successful deletion

            // Find the matching vehicle for the car chosen via the button
            foreach (Vehicle vehicle in _vehicles)
            {
                if (deletedCar.Name.Contains(vehicle.VinNumber))
                {
                    // Once found determine if user is sure that they would
                    // like to delete the car
                    if (MessageBox.Show("Are you sure you want to remove the following vehicle with Vin Number: " +
                        vehicle.VinNumber, "Are You Sure?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        bool result = _vehicleManager.DeleteVehicle(vehicle);
                        if (result == true) // If the deletion was successful
                        {
                            chosenVehicle = vehicle;
                            success = true;
                            _carButtons.Remove(deletedCar);
                            wrapPanelVehicleContainer.Children.Remove(deletedCar);
                            break;
                        }
                        else // Inform the user otherwise
                        {
                            MessageBox.Show("Your Vehicle was not deleted from the database.", "Unsuccessful Deletion", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else // Allow user to change their mind
                    {
                        break;
                    }
                }
            }

            return success;
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/02/12
        /// 
        /// Helper method to hide instructions and title.
        /// </summary>
        private void HideTitleAndInstructions()
        {
            // Hide relevant information
            txtBlkTitleRemoveVehicle.Visibility = Visibility.Hidden;
            txtBlkBodyRemoveVehicle.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/02/12
        /// 
        /// Helper method for determining whether or not a vehicle
        /// is available to be deleted.
        /// </summary>
        /// <param name="btnClicked"></param>
        private void DetermineCarRemoval(Button btnClicked)
        {
            // Set any previous available buttons to enabled
            foreach (Button button in _carButtons)
            {
                if (button.IsEnabled == false)
                {
                    button.IsEnabled = true;
                }
            }
            // Disable the chosen car button
            btnClicked.IsEnabled = false;
        }
    }
}
