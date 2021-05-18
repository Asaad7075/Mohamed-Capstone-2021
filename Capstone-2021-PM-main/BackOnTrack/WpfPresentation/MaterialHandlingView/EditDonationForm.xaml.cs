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
    /// Created: 2021/02/04
    /// Interaction logic for pgDonorFormDesition.xaml
    /// </summary>
    public partial class EditDonationForm : Page
    {
        private DonationForm _donorForm;
        private IDonationFormManager _donorFormManager = null;
        private bool _isChange = true;
        private string pageName = "Edit Donation Form";
        public string PageName { get { return pageName; } }

        /// <summary>
        /// Asaad Mohamed
        /// Created: 2021/02/04
        /// This is a constructor hold two parameters refernce to donorFormDesition page 
        /// </summary>
        /// <param name="donorForm"></param>
        /// <param name="donorFormManager"></param>
        public EditDonationForm(DonationForm donorForm, IDonationFormManager donorFormManager)
        {
            InitializeComponent();
            _donorFormManager = donorFormManager;
            _donorForm = donorForm;
            _isChange = false;
        }
        public EditDonationForm()
        {

            _isChange = true;
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (_isChange == false)
            {

                lblDonorFormID.Content = _donorForm.DonorFormID.ToString();
                txtDonorID.Text = _donorForm.DonorID.ToString();
                txtDateCreate.Text = _donorForm.DateCreated.ToString();
                cboStatus.Text = _donorForm.Status.ToString();
                cboStatus.IsReadOnly = true;

            }
            else
            {
                btnChange.Content = "Save";
            }
        }

        /// <summary>
        /// Asaad Mohamed
        /// Created: 2021/02/04
        /// This is Event listener for update the form status to approve or deny
        /// </summary>
        /// 

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            if ((string)btnChange.Content == "Change Status")
            {
                this.Title = " Change Donor Form Status";
                cboStatus.IsReadOnly = false;
                lblDonorFormID.IsEnabled = false;
                txtDonorID.IsReadOnly = true;
                txtDateCreate.IsReadOnly = true;
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

                //  attempt to update donor form information to approve or deny
                DonationForm newStatus = new DonationForm()
                {

                    DonorID = int.Parse(txtDonorID.Text),
                    DateCreated = DateTime.Parse(txtDateCreate.Text),
                    Status = cboStatus.Text,

                };
                if (!_isChange)
                {
                    newStatus.DonorFormID = _donorForm.DonorFormID;
                    try
                    {
                        if (
                       _donorFormManager.EditDonorForm(_donorForm, newStatus))
                        {
                            MessageBox.Show("The status of donor form has been update Success!", "Successful update",
                                MessageBoxButton.OK, MessageBoxImage.Exclamation);

                            this.Content = true;
                            this.NavigationService?.Navigate(new ViewDonationForms()); ;
                        }
                        else
                        {
                            MessageBox.Show("Status failed to update.");
                            this.Content = true;
                            this.NavigationService?.Navigate(new ViewDonationForms()); ;

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
        /// Created: 2021/02/04
        /// This is event for cancel the form and take you  back to main window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            btnCancel.IsCancel = true;
            this.Content = true;
            this.NavigationService?.Navigate(new ViewDonationForms());
        }
    }
}
