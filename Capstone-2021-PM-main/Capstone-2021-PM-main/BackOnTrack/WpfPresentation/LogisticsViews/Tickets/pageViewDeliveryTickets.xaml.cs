using DomainModels.Tickets;
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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfPresentation.LogisticsViews.Tickets
{
    /// <summary>
    /// Interaction logic for pageDeliveryTicketView.xaml
    /// </summary>
    public partial class pageDeliveryTicketView : Page
    {
        private IDeliveryTicketManager _deliveryTicketManager;
        private List<DeliveryTicketVM> _deliveryTickets;
        public bool editTicket = false;
        private string pageName = "View Delivery Tickets";
        /// <summary>
        /// Jakub Kawski 
        /// 2021/02/28
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
        public pageDeliveryTicketView()
        {
            InitializeComponent();         
        }
        /// <summary>
        /// Jakub Kawski
        /// 2021/02/28
        /// 
        /// Get delivery tickets and fill table with data.
        /// </summary>
        public void LoadDataGrid()
        {
            dgDeliveryTicket.ItemsSource = null;
            try
            {
                dgDeliveryTicket.ItemsSource = _deliveryTicketManager.RetrieveAllTickets();
            }
            catch (Exception ex)
            {

                System.Windows.MessageBox.Show(ex.Message + "\n\n" + ex.InnerException ?? ex.InnerException.Message);
            }
            
        }
        /// <summary>
        /// Jakub Kawski
        /// 2021/02/28
        /// 
        /// Generic page loaded event. Get page objects ready.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _deliveryTicketManager = new DeliveryTicketManager();
            _deliveryTickets = new List<DeliveryTicketVM>();
            LoadDataGrid();
        }
        /// <summary>
        /// Jakub Kawski
        /// 2021/02/28
        /// 
        /// Button click event, navigates user to edit form page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditDeliveryTicket_Click(object sender, RoutedEventArgs e)
        {
            if(dgDeliveryTicket.SelectedItem == null)
            {
                System.Windows.Forms.MessageBox.Show("Please select a ticket.");
            }
            else
            {
                this.NavigationService.Navigate(new pageDeliveryTicketForm((DeliveryTicketVM)dgDeliveryTicket.SelectedItem));
            }
            
        }
        /// <summary>
        /// Jakub Kawski
        /// 2021/02/28
        /// 
        /// Button click event, navigates user to add ticket form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddDeliveryTicket_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new pageDeliveryTicketForm());           
        }
        /// <summary>
        /// Jakub Kawski
        /// 2021/03/19
        /// 
        /// Deletes selected ticket
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteDeliveryTicket_Click(object sender, RoutedEventArgs e)
        {
            DialogResult dialogResult = System.Windows.Forms.MessageBox.Show("Are you sure you want to delete this ticket forever ? ", "Delete Ticket", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    _deliveryTicketManager.DeleteTicket((DeliveryTicketVM)dgDeliveryTicket.SelectedItem);
                    System.Windows.MessageBox.Show("Ticket deleted successfully");
                    LoadDataGrid();

                }
                catch (Exception ex)
                {

                    System.Windows.MessageBox.Show(ex.Message + "\n\n" + ex.InnerException ?? ex.InnerException.Message);
                }
            }
        }

        private void btnRefreshDeliveryTicket_Click(object sender, RoutedEventArgs e)
        {
            LoadDataGrid();
        }
    }
}
