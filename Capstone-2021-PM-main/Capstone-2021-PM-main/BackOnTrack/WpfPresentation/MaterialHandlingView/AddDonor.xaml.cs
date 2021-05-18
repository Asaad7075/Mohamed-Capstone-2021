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
using WpfPresentation.Validators;

namespace WpfPresentation.MaterialHandlingView
{
    /// <summary>
    /// Interaction logic for AddDonor.xaml
    /// </summary>
    public partial class AddDonor : Page
    {
        
        private IDonorManager _donorManager;
        private bool _isAdd = true;
        private Donor _donor = new Donor();
        private string pageName = "Add Donor ";
        public string PageName { get { return pageName; } }
        public AddDonor()
        {
            _donorManager = new DonorManager();
            //setupAdd();
            InitializeComponent();
        }
        public AddDonor(Donor donor, IDonorManager donorManager)
        {
            InitializeComponent();
            _donor = donor;
            _donorManager = donorManager;
            _isAdd = true;

        }
        /// <summary>
        /// Asaad Mohamed 
        /// created: 2021/03/24
        /// Logic to add and save new donor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnAddDonor_Click(object sender, RoutedEventArgs e)
        {
            
                if (chkBusiness.IsChecked == false && chkIndividual.IsChecked == false)
                {
                    MessageBox.Show("You must Checked an option ", "Invalid Chick ", MessageBoxButton.OK);
                    chkBusiness.Focus();
                    return;
                }
                else if (chkBusiness.IsChecked == true && chkIndividual.IsChecked == true)
                {
                    MessageBox.Show("You must Checked one option ", "Invalid Chick ", MessageBoxButton.OK);
                    chkBusiness.Focus();
                    return;
                }
                if (txtBusinessName.Text == "")
                {
                    MessageBox.Show("You must Enter Business Name", "Invalid Business Name ", MessageBoxButton.OK);
                    txtBusinessName.Focus();
                    return;
                }

                if (txtFirstName.Text == "")
                {
                    MessageBox.Show("You must Enter Your first Name", "Invalid First Name ", MessageBoxButton.OK);
                    txtFirstName.Focus();
                    return;
                }
                if (txtLastName.Text == "")
                {
                    MessageBox.Show("You must Enter Your last Name", "Invalid last Name ", MessageBoxButton.OK);
                    txtLastName.Focus();
                    return;
                }
                if (txtAddress.Text == "")
                {
                    MessageBox.Show("You must Enter Your Address ", "Invalid Address ", MessageBoxButton.OK);
                    txtAddress.Focus();
                    return;
                }

                if (txtZipCode.Text == "")
                {
                    MessageBox.Show("You must Enter Your zip code ", "Invalid zip code ", MessageBoxButton.OK);
                    txtZipCode.Focus();
                    return;
                }
                else if (txtZipCode.Text.Length < 5 || txtZipCode.Text.Length > 10)
                {
                    MessageBox.Show("You enter Invalid zip code  ", "Invalid zip code ", MessageBoxButton.OK);
                    txtZipCode.Focus();
                    return;
                }
                if (txtPhoneNumber.Text == "")
                {
                    MessageBox.Show("You must Enter Your Phone Number  ", "Invalid phone number ", MessageBoxButton.OK);
                    txtPhoneNumber.Focus();
                    return;
                }

                else if (txtPhoneNumber.Text.Length != 10 || !txtPhoneNumber.Text.IsAnInteger())
                {
                    MessageBox.Show("Your phone number must be 10 characters  ", "Invalid phone number ", MessageBoxButton.OK);
                    txtPhoneNumber.Focus();
                    return;
                }
                if (!txtEmail.Text.isValidEmail())
                {
                    MessageBox.Show("You must Enter Your Email address  ", "Invalid email address ", MessageBoxButton.OK);
                    txtEmail.Focus();
                    return;
                }

                if ((string)btnAddDonor.Content == "Save")//(_isAdd)
                {
                    this.Title = " Add New Donor";
                }
                try
                {
                    Donor donor = new Donor()
                    {
                        Business = (bool)chkBusiness.IsChecked,
                        Individual = (bool)chkIndividual.IsChecked,
                        BusinessName = txtBusinessName.Text,
                        FirstName = txtFirstName.Text,
                        LastName = txtLastName.Text,
                        MiddleInitial = txtMiddelInitial.Text,
                        Address = txtAddress.Text,
                        ZipCode = txtZipCode.Text,
                        PhoneNumber = txtPhoneNumber.Text,
                        Email = txtEmail.Text,
                        SS = txtSocialSecurity.Text,
                        EIN = txtEmployerIdentification.Text,
                        Active = (bool)chkActive.IsChecked,  

                    };
                    if (_isAdd)
                    {
                        _donorManager.InsertDonor(donor);
                    MessageBox.Show("The donor has been added Success!", "Successful added",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    this.NavigationService?.Navigate(new DonorView());
                    ResetPage();
                       

                    }
                    else
                    {
                        MessageBox.Show("The aplication failed to add.");
                    }

                }
                catch (Exception)
                {
                    MessageBox.Show("Donor Failed to add.");
                }
        }
        /// <summary>
        /// Asaad Mohamed
        /// Created: 2021/04/01
        /// This is method to reset all the inputs
        /// </summary>
        private void ResetPage()
                {
                    
                    chkBusiness.IsChecked = false;
                    chkIndividual.IsChecked = false;
                    txtBusinessName.Text = "";
                    txtFirstName.Text = "";
                    txtLastName.Text = "";
                    txtMiddelInitial.Text = "";
                    txtAddress.Text = "";
                    txtZipCode.Text = "";
                    txtPhoneNumber.Text = "";
                    txtEmail.Text = "";
                    txtSocialSecurity.Text = "";
                    txtEmployerIdentification.Text = "";
                    chkActive.IsChecked = false;

                    lblActive.Visibility = Visibility.Hidden;
                    chkActive.Visibility = Visibility.Hidden;
                    lblDonorID.Visibility = Visibility.Hidden;
                    lblDonorLabel.Visibility = Visibility.Hidden;
        }

         private void Page_Loaded(object sender, RoutedEventArgs e)
                {

            if (_isAdd == false)
            {

                setupAdd();
                ResetPage();
            }
            else
            {
                btnAddDonor.Content = "Save";
                setupAdd();

            }
        }

        /// <summary>
        /// Asaad Mohamed
        /// Created: 2021/04/01
        /// This is method to set up all the input to false
        /// </summary>
        private void setupAdd()
        {
            //chkBusiness.IsChecked = false;
            chkIndividual.IsChecked = false;
            txtBusinessName.IsReadOnly = false;
            txtBusinessName.BorderBrush = Brushes.Black;
            txtFirstName.IsReadOnly = false;
            txtFirstName.BorderBrush = Brushes.Black;
            txtLastName.IsReadOnly = false;
            txtLastName.BorderBrush = Brushes.Black;
            txtMiddelInitial.IsReadOnly = false;
            txtMiddelInitial.BorderBrush = Brushes.Black;
            txtAddress.IsReadOnly = false;
            txtAddress.BorderBrush = Brushes.Black;
            txtZipCode.IsReadOnly = false;
            txtZipCode.BorderBrush = Brushes.Black;
            txtPhoneNumber.IsReadOnly = false;
            txtPhoneNumber.BorderBrush = Brushes.Black;
            txtEmail.IsReadOnly = false;
            txtEmail.BorderBrush = Brushes.Black;
            txtSocialSecurity.IsReadOnly = false;
            txtSocialSecurity.BorderBrush = Brushes.Black;
            txtEmployerIdentification.IsReadOnly = false;
            txtEmployerIdentification.BorderBrush = Brushes.Black;

            chkActive.Visibility = Visibility.Hidden;
            lblActive.Visibility = Visibility.Hidden;
            lblDonorID.Visibility = Visibility.Hidden;
            lblDonorLabel.Visibility = Visibility.Hidden;


        }
    }
}
