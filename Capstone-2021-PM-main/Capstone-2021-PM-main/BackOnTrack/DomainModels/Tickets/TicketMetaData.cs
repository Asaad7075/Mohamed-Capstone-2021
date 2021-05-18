using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels.Tickets
{
    public class TicketMetaData: INotifyPropertyChanged
    {
        private int _totalUnassigned;
        private int _deliveryUnassigned;
        private int _pickupUnassigned;
        private int _rideUnassigned;
        public int TotalUnassigned 
        { 
            get 
            {
                return _totalUnassigned;
            } 
            set 
            {
                _totalUnassigned = value;
                RaisePropertyChanged("TotalUnassigned");
            } 
        }
        public int DeliveryUnassigned
        {
            get
            {
                return _deliveryUnassigned;
            }
            set
            {
                _deliveryUnassigned = value;
                RaisePropertyChanged("DeliveryUnassigned");
            }
        }
        public int PickupUnassigned
        {
            get
            {
                return _pickupUnassigned;
            }
            set
            {
                _pickupUnassigned = value;
                RaisePropertyChanged("PickupUnassigned");
            }
        }
        public int RideUnassigned
        {
            get
            {
                return _rideUnassigned;
            }
            set
            {
                _rideUnassigned = value;
                RaisePropertyChanged("RideUnassigned");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
