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
    /// Interaction logic for DonorView.xaml
    /// </summary>
    public partial class DonorView : Page
    {
       
        private IDonorManager _donorManager;
        private bool donorUpdate = false;
        private string pageName = "View Donor ";
        public string PageName { get { return pageName; } }
        public DonorView()
        {
            InitializeComponent();
            _donorManager = new DonorManager();
        }

        private void refreshDonorList()
        {
           
            var donor = new DonorManager();
            dgDonorsView.ItemsSource = donor.RetrieveAllDonorListByActive();

                   // dgDonorsView.Columns.Remove(dgDonorsView.Columns[11]);
                    dgDonorsView.Columns[0].Header = "Donor ID";
                    dgDonorsView.Columns[1].Header = "Business";
                    dgDonorsView.Columns[2].Header = "Individual";
                    dgDonorsView.Columns[3].Header = "Business Name";
                    dgDonorsView.Columns[4].Header = "First Name";
                    dgDonorsView.Columns[5].Header = "Last Name";
                    dgDonorsView.Columns[6].Header = "Middel Intial";
                    dgDonorsView.Columns[7].Header = "Address";
                    dgDonorsView.Columns[8].Header = "Zip Code";
                    dgDonorsView.Columns[9].Header = "Phone";
                    dgDonorsView.Columns[10].Header = "Email";
                    dgDonorsView.Columns[11].Header = "SS";
                    dgDonorsView.Columns[12].Header = "EIN";
                    dgDonorsView.Columns[13].Header = "Active";

                    dgDonorsView.Columns.Remove(dgDonorsView.Columns[13]);
                    dgDonorsView.Columns.Remove(dgDonorsView.Columns[0]);
            
        }
                private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            refreshDonorList();

        }
        /// <summary>
        /// Asaad Mohamed
        ///  Created:2021/04/01
        ///  This event to edit a donor from list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            EditSelectedDonor();
        }
        private void EditSelectedDonor()
        {

            Donor selectedDonor = (Donor)dgDonorsView.SelectedItem;


            if (selectedDonor == null)
            {
                MessageBox.Show("You need to select Donor  to update.",
                    "Update Operation Not Available", MessageBoxButton.OK,
                    MessageBoxImage.Information);
                return;
            }

            this.NavigationService?.Navigate(new EditDonor
                (selectedDonor, _donorManager));
        }
        /// <summary>
        /// Asaad Mohamed
        ///  Created:2021/04/01
        ///  This event to add a new donor from list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemoveDonor_Click(object sender, RoutedEventArgs e)
        {
            var selected = (Donor)dgDonorsView.SelectedItem;
            if (selected == null)
            {
                MessageBox.Show("You need to select a donor to delete.");
            }
            else
            {
                var result = MessageBox.Show("Are you sure you want to delete this Donor?",
                "Delete Donor", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    _donorManager.DeactivateDonor(selected.DonorID);
                    refreshDonorList();

                }
            }
        }
        /// <summary>
        /// Asaad Mohamed
        ///  Created:2021/04/01
        ///  This event to remove a donor from list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddDonor_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService?.Navigate(new AddDonor());
            refreshDonorList();
        }
    }
}
