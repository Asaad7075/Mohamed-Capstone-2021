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
using DomainModels.Services;
using LogicInterfaces;
using LogicLayer;
using WpfPresentation.Validators;

namespace WpfPresentation.ServiceListView
{
    /// <summary>
    /// Interaction logic for ServiceProvidersView.xaml
    /// </summary>
    public partial class ServiceProvidersView : Page
    {

        private IServiceProviderManager _serviceProviderManager = new ServiceProviderManager();
        private string pageName = "Service Providers";
        public string PageName { get { return pageName; } }
        public ServiceProvidersView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/2/19
        /// Page loaded displays providers in a data grid. 
        /// </summary>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                dgViewServiceProviders.ItemsSource = _serviceProviderManager.RetrieveAllServiceProviders();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/2/19
        /// When clicked, btn adds service provider to existing list 
        /// </summary>
        private void btnAddProvider_Click(object sender, RoutedEventArgs e)
        {

            bool available;
            bool scheduleRequired;
            if (/*txtServiceCategory.Text.ContainsEmptyString() ||*/ txtBusinessName.Text.ContainsEmptyString() || txtFirstName.Text.ContainsEmptyString() || txtLastName.Text.ContainsEmptyString()
                || txtAddress.Text.ContainsEmptyString() || txtPhoneNumber.Text.ContainsEmptyString() || txtEmail.Text.ContainsEmptyString() || txtZipCode.Text.ContainsEmptyString()
                || txtEIN.Text.ContainsEmptyString())
            {
                MessageBox.Show("You are missing information that is required to submit form.");
                return;
            }
            if (txtBusinessName.Text.isAnInteger())
            {
                MessageBox.Show("You must enter letters only!", "Invalid Operation",
                   MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (txtFirstName.Text.isAnInteger())
            {
                MessageBox.Show("You must enter letters only!", "Invalid Operation",
                  MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (txtLastName.Text.isAnInteger())
            {
                MessageBox.Show("You must enter letters only!", "Invalid Operation",
                MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if ((string)btnAvailable.Content == "Yes")
            {
                available = true;
            }
            else
            {
                available = false;
            }
            if ((string)btnScheduleRequired.Content == "Yes")
            {
                scheduleRequired = true;
            }
            else
            {
                scheduleRequired = false;
            }

            //string serviceCategory = txtServiceCategory.Text;
            string businessName = txtBusinessName.Text;
            string firstName = txtFirstName.Text;
            string lastName = txtLastName.Text;
            string address = txtAddress.Text;
            string phoneNumber = txtPhoneNumber.Text;
            string email = txtEmail.Text;
            string zipCode = txtZipCode.Text;
            string eIN = txtEIN.Text;

            ServiceProvider serviceProvider = new ServiceProvider()
            {
                //ServiceCategory = serviceCategory,
                BusinessName = businessName,
                FirstName = firstName,
                LastName = lastName,
                Address = address,
                PhoneNumber = phoneNumber,
                Email = email,
                ZipCode = zipCode,
                EIN = eIN,
                Activated = available,
                Schedule = scheduleRequired
            };
            try
            {
                _serviceProviderManager.AddServiceProvider(serviceProvider);
                MessageBox.Show("The provider: '" + firstName + "' was added!");
                clearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException ?? ex.InnerException.Message); // Service was not added
            }
            dgViewServiceProviders.ItemsSource = _serviceProviderManager.RetrieveAllServiceProviders();

        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/2/19
        /// When clicked, marks whether or not provider is available.
        /// </summary>
        private void btnAvailable_Click(object sender, RoutedEventArgs e)
        {
            if ((string)btnAvailable.Content == "No")
            {
                btnAvailable.Content = "Yes";
            }
            else
            {
                btnAvailable.Content = "No";
            }
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/2/19
        /// When clicked, signifies whether or not a schedule is required.
        /// </summary>
        private void btnScheduleRequired_Click(object sender, RoutedEventArgs e)
        {
            if ((string)btnScheduleRequired.Content == "No")
            {
                btnScheduleRequired.Content = "Yes";
            }
            else
            {
                btnScheduleRequired.Content = "No";
            }
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/2/19
        /// Called to renew data fields for new input.
        /// </summary>
        private void clearFields()
        {
            //txtServiceCategory.Text = "";
            txtBusinessName.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtAddress.Text = "";
            txtPhoneNumber.Text = "";
            txtEmail.Text = "";
            txtZipCode.Text = "";
            txtEIN.Text = "";
            btnAvailable.Content = "No";
            btnScheduleRequired.Content = "No";
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/2/19
        /// When clicked provider info. is auto poulated to eaze editing.
        /// </summary>
        private void btnEditProvider_Click(object sender, RoutedEventArgs e)
        {
            var selectedProvider = (ServiceProvider)dgViewServiceProviders.SelectedItem;
            if (selectedProvider == null)
            {
                MessageBox.Show("You must select a Provider first!", "Invalid Operation",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            txtBusinessName.Text = selectedProvider.BusinessName;
            txtFirstName.Text = selectedProvider.FirstName;
            txtLastName.Text = selectedProvider.LastName;
            txtAddress.Text = selectedProvider.Address;
            txtPhoneNumber.Text = selectedProvider.PhoneNumber;
            txtEmail.Text = selectedProvider.Email;
            txtZipCode.Text = selectedProvider.ZipCode;
            txtEIN.Text = selectedProvider.EIN;

            if (selectedProvider.Activated == true)
            {
                btnAvailable.Content = "Yes";
            }
            else
            {
                btnAvailable.Content = "No";
            }
            if (selectedProvider.Schedule == true)
            {
                btnScheduleRequired.Content = "Yes";
            }
            else
            {
                btnScheduleRequired.Content = "No";
            }
            //if (txtServiceCategory.Text.ContainsEmptyString())
            //{
            //    txtServiceCategory.Text = selectedProvider.ServiceCategory;
            //}
            if (txtBusinessName.Text.isAnInteger())
            {
                MessageBox.Show("You must enter letters only!", "Invalid Operation",
                   MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (txtFirstName.Text.isAnInteger())
            {
                MessageBox.Show("You must enter letters only!", "Invalid Operation",
                  MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (txtLastName.Text.isAnInteger())
            {
                MessageBox.Show("You must enter letters only!", "Invalid Operation",
                MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (txtAddress.Text.ContainsEmptyString())
            {
                txtAddress.Text = selectedProvider.Address;
            }
            if (txtPhoneNumber.Text.ContainsEmptyString())
            {
                txtPhoneNumber.Text = selectedProvider.PhoneNumber;
            }
            if (txtEmail.Text.ContainsEmptyString())
            {
                txtEmail.Text = selectedProvider.Email;
            }
            if (txtZipCode.Text.ContainsEmptyString())
            {
                txtZipCode.Text = selectedProvider.ZipCode;
            }
            if (txtEIN.Text.ContainsEmptyString())
            {
                txtEIN.Text = selectedProvider.EIN;
            }
            btnAddProvider.Visibility = Visibility.Collapsed;
            btnEditProvider.Visibility = Visibility.Collapsed;
            btnDeleteProvider.Visibility = Visibility.Collapsed;
            btnSaveEdit.Visibility = Visibility.Visible;
            //var selectedProvider = (ServiceProvider)dgViewServiceProviders.SelectedItem;
            //bool activated;
            //bool scheduleRequired;

            //if (selectedProvider == null)
            //{
            //    MessageBox.Show("You must select a Provider first!", "Invalid Operation",
            //        MessageBoxButton.OK, MessageBoxImage.Information);
            //    return;
            //}
            //if (selectedProvider != null /*&& txtServiceCategory.Text.ContainsEmptyString()*/ && txtBusinessName.Text.ContainsEmptyString() && txtFirstName.Text.ContainsEmptyString()
            //    && txtLastName.Text.ContainsEmptyString() && txtAddress.Text.ContainsEmptyString() && txtPhoneNumber.Text.ContainsEmptyString() && txtEmail.Text.ContainsEmptyString()
            //    && txtZipCode.Text.ContainsEmptyString() && txtEIN.Text.ContainsEmptyString())
            //{
            //    MessageBox.Show("You must enter the new provider information before clicking 'Edit Provider'");
            //    return;
            //}
            ////if (txtServiceCategory.Text.ContainsEmptyString())
            ////{
            ////    txtServiceCategory.Text = selectedProvider.ServiceCategory;
            ////}
            //if (txtBusinessName.Text.ContainsEmptyString())
            //{
            //    txtBusinessName.Text = selectedProvider.BusinessName;
            //}
            //if (txtFirstName.Text.ContainsEmptyString())
            //{
            //    txtFirstName.Text = selectedProvider.FirstName;
            //}
            //if (txtLastName.Text.ContainsEmptyString())
            //{
            //    txtLastName.Text = selectedProvider.LastName;
            //}
            //if (txtAddress.Text.ContainsEmptyString())
            //{
            //    txtAddress.Text = selectedProvider.Address;
            //}
            //if (txtPhoneNumber.Text.ContainsEmptyString())
            //{
            //    txtPhoneNumber.Text = selectedProvider.PhoneNumber;
            //}
            //if (txtEmail.Text.ContainsEmptyString())
            //{
            //    txtEmail.Text = selectedProvider.Email;
            //}
            //if (txtZipCode.Text.ContainsEmptyString())
            //{
            //    txtZipCode.Text = selectedProvider.ZipCode;
            //}
            //if (txtEIN.Text.ContainsEmptyString())
            //{
            //    txtEIN.Text = selectedProvider.EIN;
            //}

            //string oldServiceName = selectedProvider.BusinessName;
            //string serviceName = txtBusinessName.Text;


            //string oldFirstName = selectedProvider.FirstName;
            ////string serviceCategory = txtServiceCategory.Text;
            //string businessName = txtBusinessName.Text;
            //string firstName = txtFirstName.Text;
            //string lastName = txtLastName.Text;
            //string address = txtAddress.Text;
            //string phoneNumber = txtPhoneNumber.Text;
            //string email = txtEmail.Text;
            //string zipCode = txtZipCode.Text;
            //string eIN = txtEIN.Text;


            //if ((string)btnAvailable.Content == "Yes")
            //{
            //    activated = true;
            //}
            //else
            //{
            //    activated = false;
            //}
            //if ((string)btnScheduleRequired.Content == "Yes")
            //{
            //    scheduleRequired = true;
            //}
            //else
            //{
            //    scheduleRequired = false;
            //}
            //ServiceProvider serviceProvider = new ServiceProvider()
            //{
            //    ServiceProviderID = selectedProvider.ServiceProviderID,
            //    //ServiceCategory = serviceCategory,
            //    BusinessName = businessName,
            //    FirstName = firstName,
            //    LastName = lastName,
            //    Address = address,
            //    PhoneNumber = phoneNumber,
            //    Email = email,
            //    ZipCode = zipCode,
            //    EIN = eIN,
            //    Activated = activated,
            //    Schedule = scheduleRequired
            //};
            //try
            //{
            //    _serviceProviderManager.EditServiceProvider(serviceProvider);
            //    MessageBox.Show("The Provider: '" + oldFirstName + "' was edited!");
            //    dgViewServiceProviders.ItemsSource = _serviceProviderManager.RetrieveAllServiceProviders();
            //    clearFields();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException ?? ex.InnerException.Message); 
            //}
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/2/19
        /// Saves edited provider when clicked.
        /// </summary>
        private void btnSaveEdit_Click(object sender, RoutedEventArgs e)
        {
            var selectedProvider = (ServiceProvider)dgViewServiceProviders.SelectedItem;
            bool available;
            bool scheduleRequired;

            string oldFirstName = selectedProvider.FirstName;
            //string serviceCategory = txtServiceCategory.Text;
            string businessName = txtBusinessName.Text;
            string firstName = txtFirstName.Text;
            string lastName = txtLastName.Text;
            string address = txtAddress.Text;
            string phoneNumber = txtPhoneNumber.Text;
            string email = txtEmail.Text;
            string zipCode = txtZipCode.Text;
            string eIN = txtEIN.Text;
            if ((string)btnAvailable.Content == "Yes")
            {
                available = true;
            }
            else
            {
                available = false;
            }
            if ((string)btnScheduleRequired.Content == "Yes")
            {
                scheduleRequired = true;
            }
            else
            {
                scheduleRequired = false;
            }
            if (txtBusinessName.Text.isAnInteger())
            {
                MessageBox.Show("You must enter letters only!", "Invalid Operation",
                   MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (txtFirstName.Text.isAnInteger())
            {
                MessageBox.Show("You must enter letters only!", "Invalid Operation",
                  MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (txtLastName.Text.isAnInteger())
            {
                MessageBox.Show("You must enter letters only!", "Invalid Operation",
                MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (txtAddress.Text.ContainsEmptyString())
            {
                txtAddress.Text = selectedProvider.Address;
            }
            if (txtPhoneNumber.Text.ContainsEmptyString())
            {
                txtPhoneNumber.Text = selectedProvider.PhoneNumber;
            }
            if (txtEmail.Text.ContainsEmptyString())
            {
                txtEmail.Text = selectedProvider.Email;
            }
            if (txtZipCode.Text.ContainsEmptyString())
            {
                txtZipCode.Text = selectedProvider.ZipCode;
            }
            if (txtEIN.Text.ContainsEmptyString())
            {
                txtEIN.Text = selectedProvider.EIN;
            }
            ServiceProvider serviceProvider = new ServiceProvider()
            {
                ServiceProviderID = selectedProvider.ServiceProviderID,
                //ServiceCategory = serviceCategory,
                BusinessName = businessName,
                FirstName = firstName,
                LastName = lastName,
                Address = address,
                PhoneNumber = phoneNumber,
                Email = email,
                ZipCode = zipCode,
                EIN = eIN,
                Activated = available,
                Schedule = scheduleRequired
            };
            try
            {
                _serviceProviderManager.EditServiceProvider(serviceProvider);
                MessageBox.Show("The Provider: '" + oldFirstName + "' was edited!");
                dgViewServiceProviders.ItemsSource = _serviceProviderManager.RetrieveAllServiceProviders();
                clearFields();
                btnAddProvider.Visibility = Visibility.Visible;
                btnEditProvider.Visibility = Visibility.Visible;
                btnDeleteProvider.Visibility = Visibility.Visible;
                btnSaveEdit.Visibility = Visibility.Collapsed;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException ?? ex.InnerException.Message); // Service was not edited
            }
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/2/19
        /// When clicked, selected provider is deleted. 
        /// </summary>
        private void btnDeleteProvider_Click(object sender, RoutedEventArgs e)
        {
            var selectedProvider = (ServiceProvider)dgViewServiceProviders.SelectedItem;
            if (selectedProvider == null)
            {
                MessageBox.Show("You must select a Provider first.");
            }
            else
            {
                try
                {
                    _serviceProviderManager.DeleteServiceProvider(selectedProvider.ServiceProviderID);
                    MessageBox.Show("Provider: " + selectedProvider.FirstName + " was deleted.");
                    dgViewServiceProviders.ItemsSource = _serviceProviderManager.RetrieveAllServiceProviders();
                }
                catch
                {
                    MessageBox.Show("Service Provider could not be deleted.");
                }
            }
            dgViewServiceProviders.ItemsSource = _serviceProviderManager.RetrieveAllServiceProviders();
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/2/19
        /// When working with a provider, you can always click cancel btn,
        /// calls clearFields();
        /// </summary>
        private void btnCancelServiceProviderManager_Click(object sender, RoutedEventArgs e)
        {
            var selectedProvider = (ServiceProvider)dgViewServiceProviders.SelectedItem;
            selectedProvider = null;
            clearFields();
            btnAddProvider.Visibility = Visibility.Visible;
            btnEditProvider.Visibility = Visibility.Visible;
            btnDeleteProvider.Visibility = Visibility.Visible;
            btnSaveEdit.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/2/19
        /// List View to show all providers in list/data grid. 
        /// </summary>
        private void ListViewItem_Focus(object sender, RoutedEventArgs e)
        {
            dgViewServiceProviders.ItemsSource = _serviceProviderManager.RetrieveAllServiceProviders();
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/2/19
        /// List View to show all providers with selected zip code in list/data grid. 
        /// </summary>
        private void ListViewItem1_Focus(object sender, RoutedEventArgs e)
        {

            dgViewServiceProviders.ItemsSource = _serviceProviderManager.RetrieveProvidersByZipCode("52314");
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/2/19
        /// List View to show all providers with selected zip code in list/data grid. 
        /// </summary>
        private void ListViewItem2_Focus(object sender, RoutedEventArgs e)
        {

            dgViewServiceProviders.ItemsSource = _serviceProviderManager.RetrieveProvidersByZipCode("52315");
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/2/19
        /// List View to show all providers with selected zip code in list/data grid. 
        /// </summary>
        private void ListViewItem3_Focus(object sender, RoutedEventArgs e)
        {
            dgViewServiceProviders.ItemsSource = _serviceProviderManager.RetrieveProvidersByZipCode("52404");
        }


    }
}
