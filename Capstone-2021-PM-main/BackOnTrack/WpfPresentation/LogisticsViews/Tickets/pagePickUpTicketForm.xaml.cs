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
    /// Interaction logic for pagePickUpTicketForm.xaml
    /// </summary>
    public partial class pagePickUpTicketForm : Page, INotifyPropertyChanged
    {
        private IPickUpTicketManager _pickUpTicketManager;
        private DateTime tomorrowDateTime;
        private TimePicker timePickerStart, timePickerEnd;
        private PickUpTicketVM oldPickUpTicket = new PickUpTicketVM();
        private bool IsEditMode = false;
        private string pageName = "PickUp Ticket Form";
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
        private PickUpTicketVM pickupTicket;
        public PickUpTicketVM PickUpTicket
        {
            get
            {
                if (pickupTicket == null)
                {
                    pickupTicket = new PickUpTicketVM();
                }
                return pickupTicket;
            }
            set
            {
                pickupTicket = value;
                RaisePropertyChanged("pickupTicket");
            }
        }
        public string City
        {
            get { return pickupTicket.City; }
            set
            {
                pickupTicket.City = value;
                RaisePropertyChanged("City");
            }
        }
        public string ZipCode
        {
            get { return pickupTicket.ZipCode; }
            set
            {
                pickupTicket.ZipCode = value;
                RaisePropertyChanged("ZipCode");
            }
        }
        public int DonationID
        {
            get { return pickupTicket.DonationID; }
            set
            {
                pickupTicket.DonationID = value;
                RaisePropertyChanged("DonationID");
            }
        }
        public int TicketID
        {
            get { return pickupTicket.TicketID; }
            set
            {
                pickupTicket.TicketID = value;
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
                    StatusDescription = pickupTicket.StatusDescription,
                    StatusID = pickupTicket.StatusID
                };


                //selectedStatus; 
            }
            set
            {
                if (pickupTicket.StatusDescription != null)
                {
                    pickupTicket.StatusID = value.StatusID;
                    pickupTicket.StatusDescription = value.StatusDescription;
                    RaisePropertyChanged("SelectedStatus");
                }
            }
        }
        public string Notes
        {
            get
            {
                return pickupTicket.Notes;
            }
            set
            {
                pickupTicket.Notes = value;
                RaisePropertyChanged("Notes");
            }
        }
        public string StreetAddressLineOne
        {
            get
            {
                return pickupTicket.StreetAddressLineOne;
            }
            set
            {
                pickupTicket.StreetAddressLineOne = value;
                RaisePropertyChanged("StreetAddressLineOne");
            }
        }
        public string StreetAddressLineTwo
        {
            get
            {
                return pickupTicket.StreetAddressLineTwo;
            }
            set
            {
                pickupTicket.StreetAddressLineTwo = value;
                RaisePropertyChanged("StreetAddressLineTwo");
            }
        }
        public string State
        {
            get { return pickupTicket.State; }
            set
            {
                pickupTicket.State = value;
                RaisePropertyChanged("State");
            }
        }
        public DateTime RequestDateStart 
        { 
            get 
            {
                return pickupTicket.RequestDateStart;
            }
            set
            {
                if(value.CompareTo(tomorrowDateTime) < 0)
                {
                    MessageBox.Show("Cannot schedule times earlier then tomorrow.");
                    return;
                }
                pickupTicket.RequestDateStart = value;
                RaisePropertyChanged("RequestDateStart");
            }
        }
        public DateTime RequestDateEnd 
        {
            get
            {
                return pickupTicket.RequestDateEnd;
            }
            set
            {
                if(value.CompareTo(pickupTicket.RequestDateStart) < 0)
                {
                    MessageBox.Show("End date cannot be before start date.");
                    return;
                }
                pickupTicket.RequestDateEnd = value;
                RaisePropertyChanged("RequestDateEnd");
            }
        }


        #endregion
        public pagePickUpTicketForm()
        {
            InitializeComponent();
            DataContext = this;           
        }
        public pagePickUpTicketForm(PickUpTicketVM ticket)
        {
            InitializeComponent();
            oldPickUpTicket = new PickUpTicketVM(ticket);
            PickUpTicket = ticket;
            cboTicketStatus.SelectedIndex = ticket.StatusID;
            DataContext = this;
            btnBack.Visibility = Visibility.Visible;
            IsEditMode = true;
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _pickUpTicketManager = new PickUpTicketManager();

            timePickerStart = new TimePicker();
            timePickerEnd = new TimePicker();
            tomorrowDateTime = DateTime.Today.AddDays(1);
            ucPickUpTimePickerStart.Content = timePickerStart;
            ucPickUpTimePickerEnd.Content = timePickerEnd;
            if (IsEditMode)
            {
                timePickerStart.SelectedTime = pickupTicket.TimeRangeStart;
                timePickerEnd.SelectedTime = pickupTicket.TimeRangeEnd;
            }
            else
            {             
                // only need to set for new tickets
                RequestDateStart = tomorrowDateTime;
                RequestDateEnd = tomorrowDateTime;
            }
            
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UpdateTimeRange();
                if (IsEditMode)
                {
                    if (_pickUpTicketManager.UpdateTicket(PickUpTicket, oldPickUpTicket))
                    {
                        MessageBox.Show("Ticket successfully updated.");
                        this.NavigationService.GoBack();
                    } else
                    {
                        MessageBox.Show("Failed to update ticket fully.");
                    }
                    
                }
                else
                {                 
                    if (_pickUpTicketManager.AddTicket(pickupTicket))
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
            pickupTicket.TimeRangeStart = timePickerStart.SelectedTime;
            pickupTicket.TimeRangeEnd = timePickerEnd.SelectedTime;
        }
    }
}
