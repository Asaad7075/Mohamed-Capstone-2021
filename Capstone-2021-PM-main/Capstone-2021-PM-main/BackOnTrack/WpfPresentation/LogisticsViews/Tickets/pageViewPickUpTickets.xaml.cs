using DataAccessFakes;
using DomainModels.Tickets;
using LogicInterfaces;
using LogicLayer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfPresentation.LogisticsViews.Tickets
{
    /// <summary>
    /// Interaction logic for pageViewPickUpTickets.xaml
    /// </summary>
    public partial class pageViewPickUpTickets : Page
    {
        private IPickUpTicketManager _pickupTicketManager;
        private string pageName = "View PickUp Tickets";


        /// <summary>
        /// Jakub Kawski 
        /// 2021/03/12
        /// 
        /// This property is used for navigation bindings.
        /// </summary>
        public string PageName
        {
            get
            {
                return pageName;
            }
        }
        public pageViewPickUpTickets()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Jakub Kawski
        /// 2021/03/12
        /// 
        /// Resets and loaded the datagrid
        /// </summary>
        public void LoadDataGrid()
        {
            dgPickUpTicket.ItemsSource = null;
            try
            {
                dgPickUpTicket.ItemsSource = _pickupTicketManager.RetrieveAllTickets();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message + "\n\n" + ex.InnerException ?? ex.InnerException.Message);
            }

        }

        /// <summary>
        /// Jakub Kawski
        /// 2021/03/12
        /// 
        /// Loading private variables when the page is loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _pickupTicketManager = new PickUpTicketManager();      
        }

        private void dgPickUpTicket_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDataGrid();
        }

        private void btnRefreshPickUpTicket_Click(object sender, RoutedEventArgs e)
        {
            LoadDataGrid();
        }

        private void btnDeletePickUpTicket_Click(object sender, RoutedEventArgs e)
        {
            DialogResult dialogResult = System.Windows.Forms.MessageBox.Show("Are you sure you want to delete this ticket forever ? ", "Delete Ticket", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    _pickupTicketManager.DeleteTicket((PickUpTicketVM)dgPickUpTicket.SelectedItem);
                    System.Windows.MessageBox.Show("Ticket deleted successfully");
                    LoadDataGrid();

                }
                catch (Exception ex)
                {

                    System.Windows.MessageBox.Show(ex.Message + "\n\n" + ex.InnerException ?? ex.InnerException.Message);
                }
            }
        }
        /// <summary>
        /// Jakub Kawski
        /// 2021/03/19
        /// 
        /// Button click event, navigates user to edit form page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditPickUpTicket_Click(object sender, RoutedEventArgs e)
        {
            if (dgPickUpTicket.SelectedItem == null)
            {
                System.Windows.Forms.MessageBox.Show("Please select a ticket.");
            }
            else
            {
                this.NavigationService.Navigate(new pagePickUpTicketForm((PickUpTicketVM)dgPickUpTicket.SelectedItem));
            }
        }
        /// <summary>
        /// Jakub Kawski
        /// 2021/03/19 
        /// 
        /// Navigate to the ticket form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddPickUpTicket_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new pagePickUpTicketForm());
        }
    }
}
