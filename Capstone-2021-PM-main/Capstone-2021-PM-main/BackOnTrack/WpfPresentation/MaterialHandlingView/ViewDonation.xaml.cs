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

namespace WpfPresentation.MaterialHandlingView
{
    /// <summary>
    /// Asaad Mohamed
    /// Created: 2021/02/22
    /// Interaction logic for pgDonation.xaml
    /// </summary>
    public partial class ViewDonation : Page
    {
        private IDonationManager _donationManager;
        private string pageName = "View Donation ";
        public string PageName { get { return pageName; } }
        public ViewDonation()
        {
            InitializeComponent();
            _donationManager = new DonationManager();
        }

        /// <summary>
        /// Asaad Mohamed
        /// Created: 2021/02/04
        /// This helper method to refersh donation item when retrive all data from database
        /// </summary>
        private void refreshDonationList()
        {
            var donation = new DonationManager();


            dgDonationItem.ItemsSource = donation.RetrieveAllDonationList();

         
            dgDonationItem.Columns[0].Header = "Donation ID";

            dgDonationItem.Columns[1].Header = "Donor ID";

            dgDonationItem.Columns[2].Header = "Name Of Item";

            dgDonationItem.Columns[3].Header = "Description";

            dgDonationItem.Columns[4].Header = "Estimates Value";

            dgDonationItem.Columns[5].Header = "Age of Item";

            dgDonationItem.Columns[6].Header = "Droop off";

            dgDonationItem.Columns[7].Header = "Pick Up";

            dgDonationItem.Columns[8].Header = "Pick Up Date";

            dgDonationItem.Columns[9].Header = "Mail Receipt";

            dgDonationItem.Columns[10].Header = "Email Receipt";

            dgDonationItem.Columns[11].Header = "Donation Status";

            dgDonationItem.Columns[13].Header = "Donation Image";

        

          
            dgDonationItem.Columns.Remove(dgDonationItem.Columns[13]);
           


        }

        /// <summary>
        /// Asaad Mohamed
        /// Created: 2021/02/22
        /// This edite the status of donation when you select a list to edite
        /// </summary>

        private void EditSelectedDonationItem()
        {

            Donation selectedDonation = (Donation)dgDonationItem.SelectedItem;


            if (selectedDonation == null)
            {
                MessageBox.Show("You need to select Donation Item to change.",
                    "Change Operation Not Available", MessageBoxButton.OK,
                    MessageBoxImage.Information);
                return;
            }

            this.NavigationService?.Navigate(new EditDonationItem
                (selectedDonation, _donationManager));
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            refreshDonationList();
        }

        private void dgDonationItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EditSelectedDonationItem();
        }

        /// <summary>
        /// Asaad Mohamed
        /// Created: 2021/02/22
        /// This is Event listener when you need to  click on the list that you want to edit the status
        /// </summary>
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            EditSelectedDonationItem();
        }

        private void btnRemoveDonation_Click(object sender, RoutedEventArgs e)
        {
            var selected = (Donation)dgDonationItem.SelectedItem;
            if (selected == null)
            {
                MessageBox.Show("You need to select a donation to delete.");
            }
            else
            {
                var result = MessageBox.Show("Are you sure you want to delete this Donation?",
                "Delete Donation", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    _donationManager.DeactivateDonation(selected.DonationID);
                    refreshDonationList();
                }
            }
        }

        /// <summary>
        /// Asaad Mohamed
        /// Created: 2021/03/24
        /// This is Event listener when you need to  add new donation 
        /// </summary>
        private void btnAddDonation_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService?.Navigate(new AddDonationItem());
            refreshDonationList();
        }
    }
}
