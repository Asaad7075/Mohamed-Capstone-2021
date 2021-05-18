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
    /// Interaction logic for EditDonor.xaml
    /// </summary>
    public partial class EditDonor : Page
    {

        private Donor _donor;
        private IDonorManager _donorManager;
        private bool isEdit = true;
        public EditDonor()
        {
            _donorManager = new DonorManager();
            InitializeComponent();
        }
        /// <summary>
        /// Asaad Mohamed
        /// Created: 2021/04/01
        /// This is a constructor hold two parameters refernce to Main 
        /// </summary>
        /// <param name="donor"></param>
        /// <param name="donorManager"></param>
        public EditDonor(Donor donor, IDonorManager donorManager)
        {
            InitializeComponent();
            _donorManager = donorManager;
            _donor = donor;
            isEdit = false;
        }
        /// <summary>
        /// Asaad Mohamed
        /// Created: 2021/04/01
        /// This is Event listener for save when create the new donor 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            if ((string)btnSave.Content == "Edit")
            {
                this.Title = "Edit Donor Data";
                saveEdit();
               
            }
            else {

                if (chkBusiness.IsChecked == false && chkIndividual.IsChecked == false)
                {
                    MessageBox.Show("You must Checked an option ", "Invalid Checked ", MessageBoxButton.OK);
                    chkBusiness.Focus();
                    return;
                }
                else if (chkBusiness.IsChecked == true && chkIndividual.IsChecked == true)
                {
                    MessageBox.Show("You must Checked one option ", "Invalid Checked ", MessageBoxButton.OK);
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

                else if (txtPhoneNumber.Text.Length != 10 && !txtPhoneNumber.Text.IsAnInteger())
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


                if ((string)btnSave.Content == "Save")
                {
                    this.Title = " Edit Donor";


                }

                try { 
                    Donor newdonor = new Donor()
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

                    if (!isEdit)
                    {

                        _donorManager.EditDonor(_donor, newdonor);
                            
                                MessageBox.Show("The status of donor  has been update Success!", "Successful update",
                                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
                                this.NavigationService?.Navigate(new DonorView());
                            }
                            else
                            {
                                MessageBox.Show("Status failed to update.");
                                //this.NavigationService?.Navigate(new ViewDonationForms()); ;

                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Donor Failed to add." + ex.InnerException.Message);
                        }
                    
                }
            
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

                if (isEdit == false)
                {
                    setupEdit();
                lblDonorID.Content = _donor.DonorID;
                chkBusiness.IsChecked = _donor.Business;
                chkIndividual.IsChecked = _donor.Individual;
                txtBusinessName.Text = _donor.BusinessName;
                txtFirstName.Text = _donor.FirstName;
                txtLastName.Text = _donor.LastName;
                txtMiddelInitial.Text = _donor.MiddleInitial;
                txtAddress.Text = _donor.Address;
                txtZipCode.Text = _donor.ZipCode;
                txtPhoneNumber.Text = _donor.PhoneNumber;
                txtEmail.Text = _donor.Email;
                txtSocialSecurity.Text = _donor.SS;
                txtEmployerIdentification.Text = _donor.EIN;
                chkActive.IsChecked = _donor.Active;
                chkActive.Visibility = Visibility.Hidden;
                lblADActive.Visibility = Visibility.Hidden;
                

            }
                    else
                    {
                        btnSave.Content = "Save";
                
            }
    }
        /// <summary>
        /// Asaad Mohamed
        /// Created: 2021/04/01
        /// This is method to set up the inputs
        /// </summary>
        private void setupEdit()
             {

                        btnSave.Content = "Edit";
            //chkBusiness.IsChecked = chkBusiness.IsChecked.Value;
            //chkIndividual.IsChecked = chkIndividual.IsChecked.Value;
            chkBusiness.IsEnabled = false;
            chkIndividual.IsEnabled = false;
                        txtBusinessName.IsReadOnly = true;
                        txtBusinessName.BorderBrush = Brushes.Black;
                        txtFirstName.IsReadOnly = true;
                        txtFirstName.BorderBrush = Brushes.Black;
                        txtLastName.IsReadOnly = true;
                        txtLastName.BorderBrush = Brushes.Black;
                        txtMiddelInitial.IsReadOnly = true;
                        txtMiddelInitial.BorderBrush = Brushes.Black;
                        txtAddress.IsReadOnly = true;
                        txtAddress.BorderBrush = Brushes.Black;
                        txtZipCode.IsReadOnly = true;
                        txtZipCode.BorderBrush = Brushes.Black;
                        txtPhoneNumber.IsReadOnly = true;
                        txtPhoneNumber.BorderBrush = Brushes.Black;
                        txtEmail.IsReadOnly = true;
                        txtEmail.BorderBrush = Brushes.Black;
                        txtSocialSecurity.IsReadOnly = true;
                        txtSocialSecurity.BorderBrush = Brushes.Black;
                        txtEmployerIdentification.IsReadOnly = true;
                        txtEmployerIdentification.BorderBrush = Brushes.Black;
                        chkActive.IsEnabled = false;
                        chkActive.Visibility = Visibility.Hidden;
                        lblADActive.Visibility = Visibility.Hidden;
        }
        /// <summary>
        /// Asaad Mohamed
        /// Created: 2021/04/01
        /// This is method to cleen all the inputs
        /// </summary>
        private void saveEdit()
        {
            btnSave.Content = "Save";
            //chkBusiness.IsChecked = chkBusiness.IsChecked;
            //chkIndividual.IsChecked = chkIndividual.IsChecked;
            chkBusiness.IsEnabled = true;
            chkIndividual.IsEnabled = true;
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


            chkActive.IsEnabled = false;
        }
    }

   
}
