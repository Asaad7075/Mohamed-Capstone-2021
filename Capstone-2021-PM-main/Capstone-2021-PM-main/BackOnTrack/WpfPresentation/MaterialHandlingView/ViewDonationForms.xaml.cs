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
  /// Created: 2021/02/04
  /// Interaction logic for pgDonationForms.xaml
  /// </summary>
    public partial class ViewDonationForms : Page
    {
        private IDonationFormManager _donationFormManager;
        private string pageName = "View Donation Form";
        public string PageName { get { return pageName; } }
        public ViewDonationForms()
        {
            InitializeComponent();
            _donationFormManager = new DonationFormManager();
        }

        /// <summary>
        /// Asaad Mohamed
        /// Created: 2021/02/04
        /// This helper method to refersh donationForm form when retrive all data from database
        /// </summary>
        private void refreshDonorFormList()
        {
            try
            {
                var donationForm = new DonationFormManager();

                dgDonationForm.ItemsSource = donationForm.RetrieveAllDonorFormList();



                dgDonationForm.Columns[0].Header = "Donor Form ID";
                dgDonationForm.Columns[1].Header = "Donor ID";
                dgDonationForm.Columns[2].Header = "Date Created";
                dgDonationForm.Columns[3].Header = "Status";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            refreshDonorFormList();
        }

        private void dgDonationForm_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EditSelectedDonorForm();
        }

        /// <summary>
        /// Asaad Mohamed
        /// Created: 2021/02/04
        /// This is Event listener when you need to  click on the list that you want to edit the status
        /// </summary>
        /// 
        private void btnEditOption_Click(object sender, RoutedEventArgs e)
        {
            EditSelectedDonorForm();
        }
        /// <summary>
        /// Asaad Mohamed
        /// Created: 2021/02/04
        /// This is method  when you need to  click on the list that you want to change the status
        /// </summary>
        /// 
        private void EditSelectedDonorForm()
        {

            DonationForm selectedForm = (DonationForm)dgDonationForm.SelectedItem;


            if (selectedForm == null)
            {
                MessageBox.Show("You need to select an option form a list to change.",
                    "Change Operation Not Available", MessageBoxButton.OK,
                    MessageBoxImage.Information);
                return;
            }


            this.NavigationService?.Navigate(new EditDonationForm
                (selectedForm, _donationFormManager));
        }
        /// <summary>
        /// Asaad Mohamed
        /// Created: 2021/02/04
        /// This is Event listener when you need to  insert new donation 
        /// </summary>
        /// 
        private void btnCreateDonorForm_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService?.Navigate(new CreateDonationForm());
        }

        
    }
}
