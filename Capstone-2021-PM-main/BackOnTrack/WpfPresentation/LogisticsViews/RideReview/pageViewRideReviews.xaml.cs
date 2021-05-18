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

namespace WpfPresentation
{
    /// <summary>
    /// Nate Hepker
    /// Created: 2021/03/23
    /// 
    /// Interaction logic for pageViewRideReviews.xaml
    /// </summary>
    public partial class pageViewRideReviews : Page
    {
        private string pageName = "View/Edit/Delete Ride Reviews";
        private IRideReviewManager _rideReviewManager;
        private RideReviewVM _rideReview;
        private RideReviewVM _oldRideReview;

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/03/23
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
        /// Created: 2021/03/23
        /// 
        /// Default constructor for viewing the ride reviews
        /// </summary>
        public pageViewRideReviews()
        {
            _rideReviewManager = new RideReviewManager(/*new RideReviewFake()*/);
            _rideReview = new RideReviewVM();
            InitializeComponent();
            setupView();
        }

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/03/23
        /// 
        /// sets up the initial page view
        /// </summary>
        private void setupView()
        {
            btnRefreshReviews.Visibility = Visibility.Visible;
            btnRefreshReviews.Content = "View Reviews";
            btnEdidRideReviews.Visibility = Visibility.Visible;
            btnDeleteReviews.Visibility = Visibility.Visible;
            dgRideReviews.Visibility = Visibility.Visible;
            LoadDataGrid();
        }

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/03/23
        /// 
        /// Loads the data grid with reviews
        /// </summary>
        public void LoadDataGrid()
        {
            try
            {
                if (dgRideReviews.ItemsSource == null)
                {
                    dgRideReviews.ItemsSource = _rideReviewManager.RetrieveAllRideReviews();
                }
                btnRefreshReviews.Content = "Refresh";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException ?? ex.InnerException.Message);
                throw;
            }
        }

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/03/23
        /// refreshes the view to reload any new reviews added
        /// Updated: 2021/04/03
        /// reloads the view to an initial view to begin using again
        /// </summary>
        private void btnRefreshReviews_Click(object sender, RoutedEventArgs e)
        {
            reloadReviews();
        }

        /// <summary>
        /// Nate Hepker
        /// Created: 2021/04/03
        /// 
        /// Helper method to reload the ride review page and refreash the page with updated data and information
        /// </summary>
        private void reloadReviews()
        {
            txtBlkHeaderViewRideReview.Text = "View Ride Reviews";
            txtBlkHeaderViewRideReviews.Text = "Ride Reviews";
            dgRideReviews.ItemsSource = null;
            dgRideReviews.Visibility = Visibility.Visible;
            grdStarRating.Visibility = Visibility.Hidden;
            lblRideReviewRating.Visibility = Visibility.Hidden;
            txtRideReviewFromDriverComment.Visibility = Visibility.Hidden;
            lblRideReviewFromDriverComment.Visibility = Visibility.Hidden;
            btnEdidRideReviews.Content = "Edit Reviews";
            btnDeleteReviews.Visibility = Visibility.Visible;
            LoadDataGrid();
        }
        
        /// <summary>
        /// Nate Hepker
        /// Created 2021/03/30
        /// 
        /// Edits a selected ride review by preloading the old review information and 
        /// allowing the user to change the contens with new rating and comment
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdidRideReviews_Click(object sender, RoutedEventArgs e)
        {
            // updating the ride review
            var selectedItem = (RideReviewVM)dgRideReviews.SelectedItem;

            // saves the updated changed of an edited review
            if (btnEdidRideReviews.Content.ToString() == "Update Review")
            {
                btnDeleteReviews.Visibility = Visibility.Hidden;

                int ticketID = selectedItem.TicketID;
                int clientID = selectedItem.ClientID;
                int driverID = selectedItem.DriverID;
                int driverClientRating = _rideReview.DriverClientRating;
                string driverComment = txtRideReviewFromDriverComment.Text;
                // if this rating == 0, that means there was no change
                // to the new rating and it needs to keep the same rating
                if (driverClientRating == 0)
                {
                    driverClientRating = selectedItem.DriverClientRating;
                }
                try
                {
                    var newRideReview = new RideReviewVM()
                    {
                        TicketID = ticketID,
                        ClientID = clientID,
                        DriverID = driverID,
                        DriverClientRating = driverClientRating,
                        DriverComment = driverComment
                    };
                    _rideReviewManager.EditRideReviewFromDriver(_oldRideReview, newRideReview);
                    MessageBox.Show("Success!\n\nReview for Ticket: " + ticketID + "\n For Client: " + clientID + "\n Was Successfully Updated");
                    reloadReviews();
                    btnEdidRideReviews.Content = "";//set empty to jump the edit reviews if block to avoid a false error message
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                }
            }

            //loading the ride review to edit
            if (btnEdidRideReviews.Content.ToString() == "Edit Reviews")
            {
                if (dgRideReviews.SelectedItem == null)
                {
                    MessageBox.Show("You must choose a Review to edit", "Invalid Ticket Selection", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    btnEdidRideReviews.Content = "Update Review";
                    btnDeleteReviews.Visibility = Visibility.Hidden;
                    txtBlkHeaderViewRideReview.Text = "Update Ride Review";
                    txtBlkHeaderViewRideReviews.Text = "Update Ride Review";
                    grdStarRating.Visibility = Visibility.Visible;
                    lblRideReviewRating.Visibility = Visibility.Visible;
                    lblRideReviewTicketID.Visibility = Visibility.Visible;
                    lblRideReviewTicketIDData.Content = selectedItem.TicketID;
                    txtRideReviewFromDriverComment.Visibility = Visibility.Visible;
                    lblRideReviewFromDriverComment.Visibility = Visibility.Visible;
                    dgRideReviews.Visibility = Visibility.Hidden;

                    //savers the old review
                    _oldRideReview = (RideReviewVM)dgRideReviews.SelectedItem;
                    //setsd the comment block
                    txtRideReviewFromDriverComment.Text = _oldRideReview.DriverComment;
                    //sets the star control
                    switch (_oldRideReview.DriverClientRating)
                    {
                        case 1:
                            star5.IsChecked = false;
                            star4.IsChecked = false;
                            star3.IsChecked = false;
                            star2.IsChecked = false;
                            star1.IsChecked = true;
                            break;
                        case 2:
                            star5.IsChecked = false;
                            star4.IsChecked = false;
                            star3.IsChecked = false;
                            star2.IsChecked = true;
                            star1.IsChecked = true;
                            break;
                        case 3:
                            star5.IsChecked = false;
                            star4.IsChecked = false;
                            star3.IsChecked = true;
                            star2.IsChecked = true;
                            star1.IsChecked = true;
                            break;
                        case 4:
                            star5.IsChecked = false;
                            star4.IsChecked = true;
                            star3.IsChecked = true;
                            star2.IsChecked = true;
                            star1.IsChecked = true;
                            break;
                        case 5:
                            star5.IsChecked = true;
                            star4.IsChecked = true;
                            star3.IsChecked = true;
                            star2.IsChecked = true;
                            star1.IsChecked = true;
                            break;
                        default:
                            star5.IsChecked = false;
                            star4.IsChecked = false;
                            star3.IsChecked = false;
                            star2.IsChecked = false;
                            star1.IsChecked = false;
                            break;
                    }
                }
            }
            //jump the edit reviews if block to avoid a false error message and reloads the reviews page.
            if (btnEdidRideReviews.Content.Equals(""))
            {
                reloadReviews();
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
        /// Created 2021/04/03
        /// 
        /// Deletes the selected ride review, and messages the user about what actions
        /// took place. Confirmed, Canceled, or Error
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteReviews_Click(object sender, RoutedEventArgs e)
        {
            RideReviewVM rideReview = (RideReviewVM)dgRideReviews.SelectedItem;

            if (dgRideReviews.SelectedItem == null)
            {
                MessageBox.Show("You must choose a Review to edit", "Invalid Ticket Selection", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                int ticketID = rideReview.TicketID;
                int driverClientRating = rideReview.DriverClientRating;
                string driverComment = rideReview.DriverComment;

                try
                {
                    var msgResult = MessageBox.Show("Are you sure you want to delete review:\nTicket:" + ticketID
                        + "\nRating:" + driverClientRating
                        + "\nComment:" + driverComment
                        , "Warning", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                    // confirming the delete
                    if (msgResult.ToString() == "OK")
                    {
                        try
                        {
                            _rideReviewManager.RemoveRideReviewFromDriver(rideReview);
                            MessageBox.Show("Deleting review:\nTicket:" + ticketID
                                + "\nRating:" + driverClientRating
                                + "\nComment:" + driverComment
                                , "Delete Confirmed"
                                , MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                        }
                    }
                    // delete was cancled by user
                    else
                    {
                        MessageBox.Show("Delete Canceled", "Canceled", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Delete ride review faild", "Delete Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            reloadReviews();
        }

    }
}
