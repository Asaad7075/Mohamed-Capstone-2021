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
    /// Interaction logic for pageViewRideTickets.xaml
    /// </summary>
    public partial class pageViewRideTickets : Page
    {
        private IRideTicketManager _rideTicketManager;
        private string pageName = "View Ride Tickets";


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
        public pageViewRideTickets()
        {
            InitializeComponent();
        }
        public void LoadDataGrid()
        {
            dgRideTicket.ItemsSource = null;
            try
            {
                dgRideTicket.ItemsSource = _rideTicketManager.RetrieveAllTickets();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message + "\n\n" + ex.InnerException ?? ex.InnerException.Message);
            }

        }
        /// <summary>
        /// Jakub Kawski
        /// 2021/03/26
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteRideTicket_Click(object sender, RoutedEventArgs e)
        {
            DialogResult dialogResult = System.Windows.Forms.MessageBox.Show("Are you sure you want to delete this ticket forever ? ", "Delete Ticket", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    _rideTicketManager.DeleteTicket((RideTicketVM)dgRideTicket.SelectedItem);
                    System.Windows.MessageBox.Show("Ticket deleted successfully");
                    LoadDataGrid();

                }
                catch (Exception ex)
                {

                    System.Windows.MessageBox.Show(ex.Message + "\n\n" + ex.InnerException ?? ex.InnerException.Message);
                }
            }
        }

        private void btnRefreshRideTicket_Click(object sender, RoutedEventArgs e)
        {
            LoadDataGrid();
        }
        /// <summary>
        /// Jakub Kawski
        /// 2021/03/26
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditRideTicket_Click(object sender, RoutedEventArgs e)
        {
            if (dgRideTicket.SelectedItem == null)
            {
                System.Windows.Forms.MessageBox.Show("Please select a ticket.");
            }
            else
            {
                this.NavigationService.Navigate(new pageRideTicketForm((RideTicketVM)dgRideTicket.SelectedItem));
            }
        }
        /// <summary>
        /// Jakub Kawski
        /// 2021/03/26 
        /// 
        /// Navigate to the ticket form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddRideTicket_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new pagePickUpTicketForm());
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _rideTicketManager = new RideTicketManager();
        }

        private void dgRideTicket_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDataGrid();
        }
    }
}
