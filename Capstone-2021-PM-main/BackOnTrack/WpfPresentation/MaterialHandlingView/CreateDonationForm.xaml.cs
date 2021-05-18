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
    ///  Asaad Mohamed
    /// Created: 2021/02/22
    /// Interaction logic for pgCreateDonationForm.xaml
    /// </summary>
    public partial class CreateDonationForm : Page
    {
        private IDonationFormManager _donationFormManager;
        private bool _addMode = true;
        private List<DonationForm> _donationForms = null;
        private IDonorManager _donorManager = null;
        private List<Donor> _donorsEmails = null;
        private DonationForm donationForm = null;
        private string pageName = "Create Donation Form";
        public string PageName { get { return pageName; } }
        public CreateDonationForm()
        {
            InitializeComponent();
            _donationFormManager = new DonationFormManager();
            _donorManager = new DonorManager();
        }
        public CreateDonationForm(IDonorManager donorManager)
        {
            InitializeComponent();
            _donorManager = donorManager;
        }

        /// <summary>
        /// Asaad Mohamed
        /// Created: 2021/02/22
        /// This is a constructor hold two parameters refernce to forLogicLayerTest  
        /// </summary>
        /// <param name="donationFormManager"></param>
        public CreateDonationForm(IDonationFormManager donationFormManager)
        {
            InitializeComponent();
            _donationFormManager = donationFormManager;

            _addMode = false;
        }
        /// <summary>
        /// Asaad Mohamed
        /// Created: 2021/02/22
        /// This is when the page is loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _donorsEmails = _donorManager.RetrieveAllDonorListByActive(true);
                _donationForms = _donationFormManager.RetrieveAllDonorFormList();
                if (_addMode)
                {
                    this.Title = "Create New Donation Form";
                    comDonerEmail.SelectedIndex = 0;
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("Lists could not be populated.\n" + ex.Message + "\n" + ex.StackTrace);
            }
            lblDonorFormID.Content = "Assigned Automatically";
            comDonerEmail.ItemsSource = _donorsEmails;
            txtPickDateCreated.Text = "";
            cboStatusCreate.Text = "select option";
            if (_addMode == false)
            {
                Donor donor = null;
                foreach (var item in _donorsEmails)
                {
                    if (item.DonorID == donationForm.DonorID)
                    {
                        donor = item;
                    }
                }
                comDonerEmail.SelectedItem = donor;
                txtPickDateCreated.IsEnabled = true;
                txtPickDateCreated.BorderBrush = Brushes.Black;
                cboStatusCreate.IsReadOnly = false;
                cboStatusCreate.BorderBrush = Brushes.Black;
            }
        }

        /// <summary>
        /// Asaad Mohamed
        /// Created: 2021/02/22
        /// This is Event listener for save when create the new form 
        /// </summary>
        /// 
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (comDonerEmail.Text.ToString() == "")
            {
                MessageBox.Show("You must select your an email.");
                comDonerEmail.Focus();
                return;
            }

            if (cboStatusCreate.Text.ToString() == "")
            {
                MessageBox.Show("You must select an option.");
                cboStatusCreate.Focus();
                return;
            }
            DateTime date;
            if (txtPickDateCreated.Text == null)
            {
                MessageBox.Show("Date can not be Empty");
                txtPickDateCreated.Focus();

                return;
            }
            else if (!DateTime.TryParse(txtPickDateCreated.Text, out date))
            {
                MessageBox.Show("Invalid Date you must select date");
                txtPickDateCreated.Focus();
                return;
            }

            if (_addMode)
            {


                try
                {
                    DonationForm newDonationForm = new DonationForm()
                    {

                        DonorID = ((Donor)comDonerEmail.SelectedItem).DonorID,
                        DateCreated = txtPickDateCreated.SelectedDate.Value,
                        Status = cboStatusCreate.Text
                    };

                    if
                        (!_addMode)
                    {
                        MessageBox.Show(" The aplication failed to create.");
                        this.Content = true;
                        this.NavigationService?.Navigate(new ViewDonationForms());

                    }
                    else
                    {
                        _donationFormManager.InsertDonationForm(newDonationForm);
                        MessageBox.Show("The aplication has been created Success!", "Successful created",
                                MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        this.Content = true;
                        this.NavigationService?.Navigate(new ViewDonationForms());
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\n"
                   + ex.InnerException?.Message);
                }
            }
        }
        /// <summary>
        /// Asaad Mohamed
        /// Created: 2021/02/22
        /// This is Event listener for cancel and navigate you to same page
        /// </summary>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            btnCancel.IsCancel = true;
            this.Content = true;
            this.NavigationService?.Navigate(new ViewDonationForms());
        }
    }
}
