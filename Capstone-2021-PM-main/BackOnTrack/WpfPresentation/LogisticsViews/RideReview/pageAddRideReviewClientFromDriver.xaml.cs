using LogicInterfaces;
using DomainModels;
using DataAccessFakes;
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
using DomainModels.Tickets;
using System.Collections;

namespace WpfPresentation
{
    /// <summary>
    /// Nate Hepker
    /// Created: 2021/03/09
    /// 
    /// Interaction logic for pageAddRideReviewClientFromDriver.xaml
    /// </summary>
    public partial class pageAddRideReviewClientFromDriver : Page
    {
        private string pageName = "Rate a Client";
        private IRideReviewManager _rideReviewManager;
        private RideReviewVM _rideReview;
        private RideReviewVM _ticket;
        private IEmployeeManager _userManager;
        private Employee _user;

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/03/09
        /// 
        /// This property is used for navigation bindings.
        /// </summary>
        public string PageName
        {
            get
            {
                return pageName;
            }
        }

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/03/09
        /// 
        /// Single-Argument Constructor that allows the user to select a 
        /// ride provided to review.
        /// </summary>
        public pageAddRideReviewClientFromDriver(Employee user)
        {
            _rideReviewManager = new RideReviewManager();
            _userManager = new EmployeeManager();
            _rideReview = new RideReviewVM();
            _ticket = new RideReviewVM();
            _user = user;
            InitializeComponent();
            setupReviewPage();
        }

        /// <summary>
        /// Nate Hepker
        /// Created 2021/03/09
        /// 
        /// Loads the data grid with tickets to review
        /// </summary>
        public void LoadDataGrid()
        {
            try
            {
                if (dgRideReviewFromDriver.ItemsSource == null)
                {
                    dgRideReviewFromDriver.ItemsSource = _rideReviewManager.RetrieveRideTicketsByEmployeeID(_user.EmployeeID);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException ?? ex.InnerException.Message);
                //throw;
            }
        }

        /// <summary>
        /// Nate Hepker
        /// Created 2021/03/09
        /// 
        /// sets up the initial review page display
        /// </summary>
        private void setupReviewPage()
        {
            resetRating();
            btnShowTicketsToReview.Visibility = Visibility.Visible;
            dgRideReviewFromDriver.Visibility = Visibility.Hidden;
            txtBlkBodyCreateRideReviewFromDriver.Visibility = Visibility.Hidden;
            lblRideReviewTicketID.Visibility = Visibility.Hidden;
            lblRideReviewTicketIDData.Visibility = Visibility.Hidden;
            lblRideReviewRating.Visibility = Visibility.Hidden;
            lblRideReviewFromDriverComment.Visibility = Visibility.Hidden;
            txtRideReviewFromDriverComment.Visibility = Visibility.Hidden;
            btnAddReviewFromDriver.Visibility = Visibility.Visible;
            grdStarRating.Visibility = Visibility.Hidden;
            btnAddReviewFromDriver.Visibility = Visibility.Hidden;
            btnCancelReviewFromDriver.Content = "Cancel";
        }

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/03/09
        /// 
        /// resets all the ratings to false and zero
        /// </summary>
        private void resetRating()
        {
            txtRideReviewFromDriverComment.Text = "";
            star5.IsChecked = false;
            star4.IsChecked = false;
            star3.IsChecked = false;
            star2.IsChecked = false;
            star1.IsChecked = false;
            _rideReview.DriverClientRating = 0;
        }

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/03/09
        /// 
        /// Shows all the ride reviews
        /// </summary>
        private void displayAllRidesToReview()
        {
            resetRating();
            btnShowTicketsToReview.Visibility = Visibility.Hidden;
            dgRideReviewFromDriver.Visibility = Visibility.Visible;
            txtBlkBodyCreateRideReviewFromDriver.Visibility = Visibility.Hidden;
            lblRideReviewTicketID.Visibility = Visibility.Hidden;
            lblRideReviewTicketIDData.Visibility = Visibility.Hidden;
            lblRideReviewRating.Visibility = Visibility.Hidden;
            lblRideReviewFromDriverComment.Visibility = Visibility.Hidden;
            txtRideReviewFromDriverComment.Visibility = Visibility.Hidden;
            btnAddReviewFromDriver.Visibility = Visibility.Visible;
            grdStarRating.Visibility = Visibility.Hidden;
            btnAddReviewFromDriver.Content = "Select";
            btnCancelReviewFromDriver.Content = "Cancel";
            LoadDataGrid();
        }

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/03/09
        /// 
        /// Action event for when the data grid is double clicked.
        /// </summary>
        private void dgRideReviewFromDriver_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            createRideReviewFromDriver();
            _ticket = (RideReviewVM)dgRideReviewFromDriver.SelectedItem;
        }

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/03/09
        /// 
        /// Creates the form view from the selected ticket to create a review
        /// </summary>
        private void createRideReviewFromDriver()
        {
            _ticket = (RideReviewVM)dgRideReviewFromDriver.SelectedItem;
            dgRideReviewFromDriver.Visibility = Visibility.Hidden;
            txtBlkBodyCreateRideReviewFromDriver.Visibility = Visibility.Visible;
            lblRideReviewTicketIDData.Content = _ticket.TicketID;
            lblRideReviewTicketID.Visibility = Visibility.Visible;
            lblRideReviewTicketIDData.Visibility = Visibility.Visible;
            lblRideReviewRating.Visibility = Visibility.Visible;
            lblRideReviewFromDriverComment.Visibility = Visibility.Visible;
            txtRideReviewFromDriverComment.Visibility = Visibility.Visible;
            btnAddReviewFromDriver.Visibility = Visibility.Visible;
            grdStarRating.Visibility = Visibility.Visible;
            btnAddReviewFromDriver.Content = "Save";
            btnCancelReviewFromDriver.Content = "Back";
        }

        /// <summary>
        /// Nate Hepker
        /// Created 2021/03/12
        /// 
        /// Click event handler for selecting a ride to review if the button says "Select"
        /// otherwise if the button says "Save", the review will be saved
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddReviewFromDriver_Click(object sender, RoutedEventArgs e)
        {
            if (dgRideReviewFromDriver.SelectedItem == null)
            {
                MessageBox.Show("You must choose a ride ticket to review, or go back.", "Invalid Ticket Selection", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (btnAddReviewFromDriver.Content.Equals("Select"))
                {
                    createRideReviewFromDriver();
                }
                else if (btnAddReviewFromDriver.Content.Equals("Save"))
                {
                    validateReviewFromDriver();
                }
            }
        }

        /// <summary>
        /// Nate Hepker
        /// Created 2021/03/12
        /// 
        /// Validates the input for a driver to review a ride(client)
        /// </summary>
        private void validateReviewFromDriver()
        {

            string comment = txtRideReviewFromDriverComment.Text.Trim();
            int rating = _rideReview.DriverClientRating;
            int clientID = _ticket.ClientID;
            int driverID = _user.EmployeeID;
            int ticketID = _ticket.TicketID;

            if (rating == 0)
            {
                MessageBox.Show("As much as you may want to, \nyou cant give a 0 star rating.\n\n" +
                    "Please select a rating.", "Invalid Rating", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }
            if (comment.Trim().Equals(""))
            {
                MessageBox.Show("Please leave a comment", "Invalid Comment", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                txtRideReviewFromDriverComment.Focus();
                return;
            }
            try
            {
                var newReview = new RideReviewVM()
                {
                    TicketID = ticketID,
                    ClientID = clientID,
                    DriverID = driverID,
                    DriverClientRating = rating,
                    DriverComment = comment
                };
                _rideReviewManager.AddRideReviewFromDriver(newReview);
                displayAllRidesToReview();
                MessageBox.Show("Success!\n\nReview for Ticket: " + ticketID + "\n For Client: " + clientID + "\n Was Successfully added");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }

        /// <summary>
        /// Nate Hepker
        /// Created 2021/03/09
        /// 
        /// Event handler for when the 5th star is selected to set the rating to 5 star
        /// </summary>
        private void star5_Click(object sender, RoutedEventArgs e)
        {
            _rideReview.DriverClientRating = 5;
            star5.IsChecked = true;
            star4.IsChecked = true;
            star3.IsChecked = true;
            star2.IsChecked = true;
            star1.IsChecked = true;
        }
        /// <summary>
        /// Nate Hepker
        /// Created 2021/03/09
        /// 
        /// Event handler for when the 4th star is selected to set the rating to 4 star
        /// </summary>
        private void star4_Click(object sender, RoutedEventArgs e)
        {
            _rideReview.DriverClientRating = 4;
            star5.IsChecked = false;
            star4.IsChecked = true;
            star3.IsChecked = true;
            star2.IsChecked = true;
            star1.IsChecked = true;
        }
        /// <summary>
        /// Nate Hepker
        /// Created 2021/03/09
        /// 
        /// Event handler for when the 3rd star is selected to set the rating to 3rd star
        /// </summary>
        private void star3_Click(object sender, RoutedEventArgs e)
        {
            _rideReview.DriverClientRating = 3;
            star5.IsChecked = false;
            star4.IsChecked = false;
            star3.IsChecked = true;
            star2.IsChecked = true;
            star1.IsChecked = true;
        }
        /// <summary>
        /// Nate Hepker
        /// Created 2021/03/09
        /// 
        /// Event handler for when the 2nd star is selected to set the rating to 2 star
        /// </summary>
        private void star2_Click(object sender, RoutedEventArgs e)
        {
            _rideReview.DriverClientRating = 2;
            star5.IsChecked = false;
            star4.IsChecked = false;
            star3.IsChecked = false;
            star2.IsChecked = true;
            star1.IsChecked = true;
        }
        /// <summary>
        /// Nate Hepker
        /// Created 2021/03/09
        /// 
        /// Event handler for when the 1st star is selected to set the rating to 1 star
        /// </summary>
        private void star1_Click(object sender, RoutedEventArgs e)
        {
            _rideReview.DriverClientRating = 1;
            star5.IsChecked = false;
            star4.IsChecked = false;
            star3.IsChecked = false;
            star2.IsChecked = false;
            star1.IsChecked = true;
        }

        /// <summary>
        /// Nate Hepker
        /// Created 2021/03/12
        /// 
        /// Button control to show the ride tickets an employee can review
        /// </summary>
        private void btnShowTiketsToReview_Click(object sender, RoutedEventArgs e)
        {
            displayAllRidesToReview();
        }

        /// <summary>
        /// Nate Hepker
        /// Created 2021/03/12
        /// 
        /// Event handler for when the Employee wants to cancel the review while its not submited
        /// </summary>
        private void btnCancelReviewFromDriver_Click(object sender, RoutedEventArgs e)
        {
            if (btnCancelReviewFromDriver.Content.ToString() == "Cancel")
            {
                setupReviewPage();
            }
            if(btnCancelReviewFromDriver.Content.ToString() == "Back")
            {
                displayAllRidesToReview();
            }
        }
    }
}
