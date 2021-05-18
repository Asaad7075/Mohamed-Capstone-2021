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
    /// Interaction logic for pageRideTicketForm.xaml
    /// </summary>
    public partial class pageRideTicketForm : Page, INotifyPropertyChanged
    {
        private IRideTicketManager _rideTicketManager;
        private DateTime tomorrowDateTime;
        private TimePicker timePickerStart, timePickerEnd;
        private RideTicketVM oldRideTicket = new RideTicketVM();
        private bool IsEditMode = false;
        private string pageName = "Ride Ticket Form";
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
        private RideTicketVM rideTicket;
        public RideTicketVM RideTicket
        {
            get
            {
                if (rideTicket == null)
                {
                    rideTicket = new RideTicketVM();
                }
                return rideTicket;
            }
            set
            {
                rideTicket = value;
                RaisePropertyChanged("RideTicket");
            }
        }
        public int ClientID
        {
            get { return rideTicket.ClientID; }
            set
            {
                rideTicket.ClientID = value;
                RaisePropertyChanged("ClientID");
            }
        }
        public int TicketID
        {
            get { return rideTicket.TicketID; }
            set
            {
                rideTicket.TicketID = value;
                RaisePropertyChanged("TicketID");
            }
        }

        private ObservableCollection<TicketStatus> statuses;
        public IEnumerable Statuses
        {
            get
            {
                if (statuses == null)
                {
                    ITicketStatusManager _ticketStatusManager = new TicketStatusManager();
                    statuses = new ObservableCollection<TicketStatus>(_ticketStatusManager.RetrieveAllTicketStatuses());
                }
                return statuses;
            }
        }
        public TicketStatus SelectedStatus
        {
            get
            {
                return new TicketStatus()
                {
                    StatusDescription = rideTicket.StatusDescription,
                    StatusID = rideTicket.StatusID
                };


                //selectedStatus; 
            }
            set
            {
                if (rideTicket.StatusDescription != null)
                {
                    rideTicket.StatusID = value.StatusID;
                    rideTicket.StatusDescription = value.StatusDescription;
                    RaisePropertyChanged("SelectedStatus");
                }
            }
        }
        public string Notes
        {
            get
            {
                return rideTicket.Notes;
            }
            set
            {
                rideTicket.Notes = value;
                RaisePropertyChanged("Notes");
            }
        }
        public string FetchCity
        {
            get { return rideTicket.FetchCity; }
            set
            {
                rideTicket.FetchCity = value;
                RaisePropertyChanged("FetchCity");
            }
        }
        public string FetchZipCode
        {
            get { return rideTicket.FetchZipCode; }
            set
            {
                rideTicket.FetchZipCode = value;
                RaisePropertyChanged("FetchZipCode");
            }
        }
        public string FetchStreetAddressLineOne
        {
            get
            {
                return rideTicket.FetchStreetAddressLineOne;
            }
            set
            {
                rideTicket.FetchStreetAddressLineOne = value;
                RaisePropertyChanged("FetchStreetAddressLineOne");
            }
        }
        public string FetchStreetAddressLineTwo
        {
            get
            {
                return rideTicket.FetchStreetAddressLineTwo;
            }
            set
            {
                rideTicket.FetchStreetAddressLineTwo = value;
                RaisePropertyChanged("FetchStreetAddressLineTwo");
            }
        }
        public string FetchState
        {
            get { return rideTicket.FetchState; }
            set
            {
                rideTicket.FetchState = value;
                RaisePropertyChanged("FetchState");
            }
        }
        public string DestinationCity
        {
            get { return rideTicket.DestinationCity; }
            set
            {
                rideTicket.DestinationCity = value;
                RaisePropertyChanged("DestinationCity");
            }
        }
        public string DestinationZipCode
        {
            get { return rideTicket.DestinationZipCode; }
            set
            {
                rideTicket.DestinationZipCode = value;
                RaisePropertyChanged("DestinationZipCode");
            }
        }
        public string DestinationStreetAddressLineOne
        {
            get
            {
                return rideTicket.DestinationStreetAddressLineOne;
            }
            set
            {
                rideTicket.DestinationStreetAddressLineOne = value;
                RaisePropertyChanged("DestinationStreetAddressLineOne");
            }
        }
        public string DestinationStreetAddressLineTwo
        {
            get
            {
                return rideTicket.DestinationStreetAddressLineTwo;
            }
            set
            {
                rideTicket.DestinationStreetAddressLineTwo = value;
                RaisePropertyChanged("DestinationStreetAddressLineTwo");
            }
        }
        public string DestinationState
        {
            get { return rideTicket.DestinationState; }
            set
            {
                rideTicket.DestinationState = value;
                RaisePropertyChanged("DestinationState");
            }
        }
        public DateTime DateOfRide
        {
            get
            {
                return rideTicket.DateOfRide;
            }
            set
            {
                if (value.CompareTo(tomorrowDateTime) < 0)
                {
                    MessageBox.Show("Cannot schedule times earlier then tomorrow.");
                    return;
                }
                rideTicket.DateOfRide = value;
                RaisePropertyChanged("DateOfRide");
            }
        }


        #endregion
        public pageRideTicketForm()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        public pageRideTicketForm(RideTicketVM ticket)
        {
            InitializeComponent();
            oldRideTicket = new RideTicketVM(ticket);
            RideTicket = ticket;
            cboTicketStatus.SelectedIndex = ticket.StatusID;
            DataContext = this;
            btnBack.Visibility = Visibility.Visible;
            IsEditMode = true;
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _rideTicketManager = new RideTicketManager();

            timePickerStart = new TimePicker();
            timePickerEnd = new TimePicker();
            tomorrowDateTime = DateTime.Today.AddDays(1);
            ucRideTimePickerStart.Content = timePickerStart;
            ucRideTimePickerEnd.Content = timePickerEnd;
            if (IsEditMode)
            {
                timePickerStart.SelectedTime = rideTicket.TimeRangeStart;
                timePickerEnd.SelectedTime = rideTicket.TimeRangeEnd;
            }
            else
            {
                // only need to set for new tickets
                DateOfRide = tomorrowDateTime;
            }

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UpdateTimeRange();
                if (IsEditMode)
                {
                    if (_rideTicketManager.UpdateTicket(rideTicket, oldRideTicket))
                    {
                        MessageBox.Show("Ticket successfully updated.");
                        this.NavigationService.GoBack();
                    }
                    else
                    {
                        MessageBox.Show("Failed to update ticket fully.");
                    }

                }
                else
                {
                    if (_rideTicketManager.AddTicket(RideTicket))
                    {
                        MessageBox.Show("Ticket successfully added");
                    }
                    else
                    {
                        MessageBox.Show("Failed to save ticket.");
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException ?? ex.InnerException.Message);
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void UpdateTimeRange()
        {
            rideTicket.TimeRangeStart = timePickerStart.SelectedTime;
            rideTicket.TimeRangeEnd = timePickerEnd.SelectedTime;
        }
    }
}
