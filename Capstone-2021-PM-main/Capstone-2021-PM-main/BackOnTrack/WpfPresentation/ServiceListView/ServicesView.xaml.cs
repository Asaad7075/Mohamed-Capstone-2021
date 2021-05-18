using DomainModels.Services;
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

namespace WpfPresentation.ServiceListView
{
    /// <summary>
    /// Interaction logic for ServicesView.xaml
    /// </summary>
    public partial class ServicesView : Page
    {
        private IServiceManager _serviceManager = new ServiceManager();
        private IServiceCategoriesManager _serviceCategoriesManager = new ServiceCategoriesManager();
        private string pageName = "Services";
        public string PageName { get { return pageName; } }
        public ServicesView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Thomas Stout
        /// Created: 2021/03/24
        /// 
        /// Attempts to load the datagrid with
        /// any existing Services, and load the 
        /// combobox with categories.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                dgViewServices.ItemsSource = _serviceManager.RetrieveAllServices();
                cboServiceCategory.ItemsSource = _serviceCategoriesManager.RetrieveAllServiceCategories();
                cboBusinessName.ItemsSource = _serviceManager.RetrieveAllBusinesses();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }

        /// <summary>
        /// Thomas Stout
        /// Created: 2021/03/24
        /// 
        /// Button logic for adding a service.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddService_Click(object sender, RoutedEventArgs e)
        {
            bool available;
            bool scheduleRequired;
            if (txtServicename.Text.ContainsEmptyString() || txtServiceDescription.Text.ContainsEmptyString() || cboServiceCategory.Text.ContainsEmptyString() ||
                    cboBusinessName.Text.ContainsEmptyString() || cboServiceProviderFirstLast.Text.ContainsEmptyString()) // Checks to make sure text isn't empty
            {
                MessageBox.Show("Missing information is required to submit form."); // Informs the user that they are missing required information
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

            string serviceName = txtServicename.Text;
            string serviceDescription = txtServiceDescription.Text;
            string serviceCategoryName = cboServiceCategory.Text;
            string serviceProvider = cboServiceProviderFirstLast.Text;
            string businessName = cboBusinessName.Text;
            Service service = new Service()
            {
                BusinessName = businessName,
                ServiceName = serviceName,
                ServiceCategoryName = serviceCategoryName,
                ScheduleRequired = scheduleRequired,
                Available = available,
                ServiceDescription = serviceDescription,
                ServiceProviderFirstLast = serviceProvider
            };
            try
            {
                _serviceManager.AddService(service); // Service is added
                MessageBox.Show("The service: '" + serviceName + "' was added!");
                clearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException ?? ex.InnerException.Message); // Service was not added
            }
            dgViewServices.ItemsSource = _serviceManager.RetrieveAllServices();
        }

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
        /// Thomas Stout
        /// Created: 2021/03/25
        /// 
        /// Sets all of the text boxes to empty strings.
        /// </summary>
        private void clearFields()
        {
            cboServiceCategory.SelectedItem = null;
            cboBusinessName.SelectedItem = null;
            cboServiceProviderFirstLast.SelectedItem = null;
            txtServicename.Text = "";
            txtServiceDescription.Text = "";
            btnAvailable.Content = "No";
            btnScheduleRequired.Content = "No";
        }

        /// <summary>
        /// Thomas Stout
        /// Created: 2021/03/24
        /// 
        /// Button logic for deleting a service.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteService_Click(object sender, RoutedEventArgs e)
        {
            var selectedService = (Service)dgViewServices.SelectedItem;
            if (selectedService == null)
            {
                MessageBox.Show("You must select a service first!");
            }
            else
            {
                try
                {
                    if (MessageBox.Show("Are you sure you would like to delete this?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                    {
                        // Do NOT delete the service
                        MessageBox.Show("Service: " + selectedService.ServiceName + " was NOT deleted");
                    }
                    else
                    {
                        // Delete the service
                        _serviceManager.DeleteService(selectedService.ServiceID);
                        MessageBox.Show("Service: " + selectedService.ServiceName + " was deleted");
                    }
                    dgViewServices.ItemsSource = _serviceManager.RetrieveAllServices();
                }
                catch (Exception)
                {
                    MessageBox.Show("Service could not be deleted");
                }
            }
            dgViewServices.ItemsSource = _serviceManager.RetrieveAllServices();
        }

        /// <summary>
        /// Thomas Stout
        /// Created: 2021/03/24
        /// 
        /// Button logic for editing a service.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditService_Click(object sender, RoutedEventArgs e)
        {
            var selectedService = (Service)dgViewServices.SelectedItem;
            if (selectedService == null)
            {
                MessageBox.Show("You must select a Service first!", "Invalid Operation",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            txtServicename.Text = selectedService.ServiceName;
            cboServiceCategory.Text = selectedService.ServiceCategoryName;
            cboBusinessName.Text = selectedService.BusinessName;
            //cboServiceProviderFirstLast.Text = selectedService.ServiceProviderFirstLast;
            Service service = new Service()
            {
                BusinessName = (string)cboBusinessName.Text
            };
            cboServiceProviderFirstLast.ItemsSource = _serviceManager.RetrieveAllProvidersByBusiness(service);
            txtServiceDescription.Text = selectedService.ServiceDescription;
            if (selectedService.Available == true)
            {
                btnAvailable.Content = "Yes";
            }
            else
            {
                btnAvailable.Content = "No";
            }
            if (selectedService.ScheduleRequired == true)
            {
                btnScheduleRequired.Content = "Yes";
            }
            else
            {
                btnScheduleRequired.Content = "No";
            }
            btnAddService.Visibility = Visibility.Collapsed;
            btnEditService.Visibility = Visibility.Collapsed;
            btnDeleteService.Visibility = Visibility.Collapsed;
            btnSaveEdit.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Thomas Stout
        /// Created: 2021/03/26
        /// 
        /// Set all fields to their default values.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelServiceManager_Click(object sender, RoutedEventArgs e)
        {
            var selectedService = (Service)dgViewServices.SelectedItem;
            selectedService = null;
            clearFields();
            btnAddService.Visibility = Visibility.Visible;
            btnEditService.Visibility = Visibility.Visible;
            btnDeleteService.Visibility = Visibility.Visible;
            btnSaveEdit.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Thomas Stout
        /// Created: 2021/03/26
        /// 
        /// Searches through the data grid to see
        /// if a serviceName can be found that matches
        /// the serviceName in the textbox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            bool found = false;
            string[] strings = { txtBoxSearchItem.Text };
            List<Service> list = new List<Service>();

            if (strings.containsEmptyString())
            {
                dgViewServices.ItemsSource = _serviceManager.RetrieveAllServices();
            }
            foreach (Service service in dgViewServices.Items)
            {
                if (service.ServiceName.ToLower().Contains(txtBoxSearchItem.Text.ToLower()))
                {
                    list.Add(service);
                    found = true;
                }
            }
            if (found == false)
            {
                MessageBox.Show("Service '" + txtBoxSearchItem.Text + "' does not exist.");
                txtBoxSearchItem.Text = "";
                return;
            }
            dgViewServices.ItemsSource = list;
        }

        /// <summary>
        /// Thomas Stout
        /// Created: 2021/03/26
        /// 
        /// Returns the data grid to its 
        /// original state. Clears the text
        /// field.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEmptySearch_Click(object sender, RoutedEventArgs e)
        {
            txtBoxSearchItem.Text = "";
            dgViewServices.ItemsSource = _serviceManager.RetrieveAllServices();
        }

        private void btnSaveEdit_Click(object sender, RoutedEventArgs e)
        {
            var selectedService = (Service)dgViewServices.SelectedItem;
            bool available;
            bool scheduleRequired;

            string oldServiceName = selectedService.ServiceName;
            string businessName = cboBusinessName.Text;
            string serviceProvider = cboServiceProviderFirstLast.Text;
            string serviceName = txtServicename.Text;
            string serviceDescription = txtServiceDescription.Text;
            string serviceCategory = cboServiceCategory.Text;
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
            Service service = new Service()
            {
                ServiceID = selectedService.ServiceID,
                BusinessName = businessName,
                ServiceName = serviceName,
                ServiceCategoryName = serviceCategory,
                Available = available,
                ScheduleRequired = scheduleRequired,
                ServiceDescription = serviceDescription,
                ServiceProviderFirstLast = serviceProvider

            };
            if (txtServicename.Text.ContainsEmptyString() || txtServiceDescription.Text.ContainsEmptyString() || cboServiceProviderFirstLast.Text.ContainsEmptyString())
            {
                MessageBox.Show("Missing information is required to submit form.");
                return;
            }
            try
            {
                _serviceManager.EditService(service);
                MessageBox.Show("The Service: '" + oldServiceName + "' was edited!");
                dgViewServices.ItemsSource = _serviceManager.RetrieveAllServices();
                clearFields();
                btnAddService.Visibility = Visibility.Visible;
                btnEditService.Visibility = Visibility.Visible;
                btnDeleteService.Visibility = Visibility.Visible;
                btnSaveEdit.Visibility = Visibility.Collapsed;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException ?? ex.InnerException.Message); // Service was not edited
            }
        }

        private void cboBusinessName_DropDownClosed(object sender, EventArgs e)
        {
            string business = cboBusinessName.Text;
            Service service = new Service()
            {
                BusinessName = business
            };
            cboServiceProviderFirstLast.ItemsSource = _serviceManager.RetrieveAllProvidersByBusiness(service);
        }
    }
}
