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

namespace WpfPresentation.LogisticsViews.Tickets
{
    /// <summary>
    /// Interaction logic for pageTicketPortal.xaml
    /// </summary>
    public partial class pageTicketPortal : Page
    {
        private string pageName = "Ticket Portal";
        public string PageName { get { return pageName; } }
        public pageTicketPortal()
        {
            InitializeComponent();
        }

        private void btnViewDeliveryTickets_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new pageDeliveryTicketView());
        }

        private void btnViewPickUpTickets_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new pageViewPickUpTickets());
        }

        private void btnViewRideTickets_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new pageViewRideTickets());
        }

        private void btnDeliveryTicketForm_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new pageDeliveryTicketForm());
        }

        private void btnPickUpTicketForm_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new pagePickUpTicketForm());
        }

        private void btnRideTicketForm_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new pageRideTicketForm());
        }

    }
}
