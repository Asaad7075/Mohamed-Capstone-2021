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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfPresentation.LogisticsViews.Tickets
{
    /// <summary>
    /// Interaction logic for pageDeliveryTicketForm.xaml
    /// </summary>
    public partial class pageDeliveryTicketForm : Page, INotifyPropertyChanged
    {
        
        private ITicketStatusManager _ticketStatusManager = new TicketStatusManager();
        private IDeliveryTicketManager _deliveryTicketManager = new DeliveryTicketManager();
        private DeliveryTicketVM deliveryTicketOld = new DeliveryTicketVM();
        private bool IsEditMode = false;
        /*The pageName is a name that is used when viewing in navigation*/

        private string pageName = "Delivery Ticket Form";
        public string PageName { get { return pageName; } }

        #region Properties for bindings/validation
        /// <summary>
        /// Jakub Kawski
        /// 2021/03/01
        /// 
        /// Everything in this region are used for binding objects to 
        /// the xaml, making direct assignments through code-behind less
        /// neccassary
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
        private DeliveryTicketVM deliveryTicket;
        public DeliveryTicketVM DeliveryTicket
        {
            get
            {
                if (deliveryTicket == null)
                {
                    deliveryTicket = new DeliveryTicketVM();
                }
                return deliveryTicket;
            }
            set
            {
                deliveryTicket = value;
                RaisePropertyChanged("DeliveryTicket");
            }
        }
        public string City
        {
            get { return deliveryTicket.City; }
            set
            {
                deliveryTicket.City = value;
                RaisePropertyChanged("City");
            }
        }
        public string ZipCode
        {
            get { return deliveryTicket.ZipCode; }
            set
            {
                deliveryTicket.ZipCode = value;
                RaisePropertyChanged("ZipCode");
            }
        }
        public int OrderID
        {
            get { return deliveryTicket.OrderID; }
            set
            {
                deliveryTicket.OrderID = value;
                RaisePropertyChanged("OrderID");
            }
        }
        public int TicketID
        {
            get { return deliveryTicket.TicketID; }
            set
            {
                deliveryTicket.TicketID = value;
                RaisePropertyChanged("TicketID");
            }
        }

        private ObservableCollection<TicketStatus> statuses;
        public IEnumerable Statuses
        {
            get 
            { 
                if(statuses == null)
                {
                    statuses = new ObservableCollection<TicketStatus>(_ticketStatusManager.RetrieveAllTicketStatuses());
                }
                return statuses; 
            }
        }
        public TicketStatus SelectedStatus
        {
            get {
                return new TicketStatus()
                {
                    StatusDescription = deliveryTicket.StatusDescription,
                    StatusID = deliveryTicket.StatusID
                };
                    
                    
                    //selectedStatus; 
            }
            set
            {
                if(deliveryTicket.StatusDescription != null)
                {
                    deliveryTicket.StatusID = value.StatusID;
                    deliveryTicket.StatusDescription = value.StatusDescription;
                    RaisePropertyChanged("SelectedStatus");
                }               
            }
        }
        public string Notes
        {
            get
            {
                return deliveryTicket.Notes;
            }
            set
            {
                deliveryTicket.Notes = value;
                RaisePropertyChanged("Notes");
            }
        }
        public string StreetAddressLineOne
        {
            get
            {
                return deliveryTicket.StreetAddressLineOne;
            }
            set
            {
                deliveryTicket.StreetAddressLineOne = value;
                RaisePropertyChanged("StreetAddressLineOne");
            }
        }
        public string StreetAddressLineTwo
        {
            get
            {
                return deliveryTicket.StreetAddressLineTwo;
            }
            set
            {
                deliveryTicket.StreetAddressLineTwo = value;
                RaisePropertyChanged("StreetAddressLineTwo");
            }
        }
        public string State { 
            get { return deliveryTicket.State; }
            set 
            {
                deliveryTicket.State = value;
                RaisePropertyChanged("State");
            } 
        }

        #endregion
        public pageDeliveryTicketForm()
        {
            InitializeComponent();
            DataContext = this;
            IsEditMode = false;
        }
        public pageDeliveryTicketForm(DeliveryTicketVM ticket)
        {
            InitializeComponent();
            deliveryTicketOld = new DeliveryTicketVM(ticket);
            DeliveryTicket = ticket;
            cboTicketStatus.SelectedIndex = ticket.StatusID;
            DataContext = this;
            btnBack.Visibility = Visibility.Visible;
            IsEditMode = true;
        }
        /// <summary>
        /// Jakub Kawski
        /// 2021/03/05
        /// 
        /// Navigate back to table view(provided that was the last view)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (IsEditMode)
                {
                    if(_deliveryTicketManager.UpdateTicket(deliveryTicket, deliveryTicketOld))
                    {
                        MessageBox.Show("Ticket successfully updated.");
                        this.NavigationService.GoBack();
                    }
                }
                else
                {
                    if (_deliveryTicketManager.AddTicket(deliveryTicket))
                    {
                        MessageBox.Show("Ticket successfully added");
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException ?? ex.InnerException.Message);
            }
        }
    }
}
