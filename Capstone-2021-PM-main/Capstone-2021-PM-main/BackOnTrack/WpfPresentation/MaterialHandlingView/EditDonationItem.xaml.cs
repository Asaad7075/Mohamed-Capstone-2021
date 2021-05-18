using DomainModels;
using LogicInterfaces;
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
    /// Interaction logic for EditDonationItem.xaml

    public partial class EditDonationItem : Page
    {
        private Donation _donation;
        private IDonationManager _donationManager = null;
        private bool _isChange = true;

        /// <summary>
        /// Asaad Mohamed
        /// Created: 2021/02/22
        /// This is a constructor hold two parameters refernce to donationDesition page 
        /// </summary>
        /// <param name="donation"></param>
        /// <param name="donationManager"></param>
        public EditDonationItem(Donation donation, IDonationManager donationManager)
        {
            InitializeComponent();
            _donationManager = donationManager;
            _donation = donation;
            _isChange = false;
        }
        public EditDonationItem()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            lblDonation.Visibility = Visibility.Hidden;
            lblDonationID.Visibility = Visibility.Hidden;
            btnBrowseButton.Visibility = Visibility.Hidden;
            imageList.Visibility = Visibility.Hidden;
            lblDonor.Visibility = Visibility.Hidden;
            txtDonorID.Visibility = Visibility.Hidden;


            if (_isChange == false)
            {

                lblDonationID.Content = _donation.DonationID.ToString();
                txtDonorID.Text = _donation.DonorID.ToString();
                txtNameItem.Text = _donation.NameOfItem.ToString();
                txtDescription.Text = _donation.Description.ToString();
                txtEstValue.Text = _donation.EstValue.ToString();
                txtAgeOfItem.Text = _donation.AgeofItem.ToString();
                chkDropOff.IsChecked = true;
                chkPickUp.IsChecked = true;
                txtPickUpDate.SelectedDate = _donation.PickUpDateTime;
                chkMailReceipt.IsChecked = true;
                chkEmailReceipt.IsChecked = true;
                cboStatus.Text = _donation.DonationStatus.ToString();
                cboStatus.IsReadOnly = true;

            }
            else
            {
                btnChange.Content = "Save";
            }
        }

        /// <summary>
        /// Asaad Mohamed
        /// Created: 2021/02/22
        /// This is Event listener for update the form status to approve or deny
        /// </summary>
        /// 
        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            if ((string)btnChange.Content == "Change Status")
            {
                this.Title = " Change Donation Item Status";

                btnChange.Content = "Save";
                cboStatus.Focus();

            }
            else if ((string)btnChange.Content == "Save")
            {
                if (cboStatus.Text == "")
                {
                    MessageBox.Show("Status can not be null");
                    cboStatus.Focus();
                    return;
                }

                //  attempt to update donation information to approve or deny
                Donation newStatus = new Donation()
                {

                    DonorID = int.Parse(txtDonorID.Text),
                    NameOfItem = txtNameItem.Text,
                    Description = txtDescription.Text,
                    EstValue = decimal.Parse(txtEstValue.Text),
                    AgeofItem = int.Parse(txtAgeOfItem.Text),
                    DropOff = (bool)chkDropOff.IsChecked,
                    PickUp = (bool)chkPickUp.IsChecked,
                    PickUpDateTime = txtPickUpDate.SelectedDate.Value,
                    MailReceipt = (bool)chkMailReceipt.IsChecked,
                    EmailReceipt = (bool)chkEmailReceipt.IsChecked,
                    DonationStatus = cboStatus.Text

                };
                if (!_isChange)
                {
                    newStatus.DonationID = _donation.DonationID;
                    try
                    {
                        if (
                       _donationManager.EditDonation(_donation, newStatus))
                        {
                            MessageBox.Show("The status of donation item  has been update Success!", "Successful update",
                                MessageBoxButton.OK, MessageBoxImage.Exclamation);
                            this.NavigationService?.Navigate(new ViewDonation());
                        }
                        else
                        {
                            MessageBox.Show("Status failed to update.");
                            this.NavigationService?.Navigate(new ViewDonation());

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + "\n\n"
                         + ex?.InnerException.Message);
                    }

                }

            }
        }
        /// <summary>
        /// Asaad Mohamed
        /// Created: 2021/02/22
        /// This is event for cancel the form and take you  back to ViewDonation page
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            btnCancel.IsCancel = true;
            this.Content = true;
            this.NavigationService?.Navigate(new ViewDonation());
        }
    }
}
