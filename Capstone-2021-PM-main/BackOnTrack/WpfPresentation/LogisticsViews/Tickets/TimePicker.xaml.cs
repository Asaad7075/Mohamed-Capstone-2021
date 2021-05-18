/// Jakub Kawski
/// Created: 2021/02/14
/// 
/// User Control Time Picker, TimeSelected is a timespan that can be used to
/// pass a time into the control or get the time currently entered in the control.
/// </summary>
///
/// <remarks>
/// Updater Name
/// Updated: yyyy/mm/dd
/// </remarks>
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
    /// Interaction logic for TimePicker.xaml
    /// </summary>
    public partial class TimePicker : UserControl, INotifyPropertyChanged
    {
        public TimePicker()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        private int _selectedHour;
        private int _selectedMinute;
        private string _selectedTimeOfDay = "AM";
        private bool hasLoaded = false;

        private ObservableCollection<int> _hours = new ObservableCollection<int>()
        {
            1,2,3,4,5,6,7,8,9,10,11,12
        };
        private ObservableCollection<int> _mins = new ObservableCollection<int>()
        {
            00,05,10,15,20,25,30,35,40,45,50,55
        };
        private ObservableCollection<string> _timeOfDay = new ObservableCollection<string>()
        {
            "AM","PM"
        };
        public IEnumerable Hours
        {
            get { return _hours; }
        }
        public IEnumerable Minutes
        {
            get { return _mins; }
        }
        public IEnumerable TimeOfDay
        {
            get { return _timeOfDay; }
        }

        public int SelectedHour
        {
            get { return _selectedHour; }
            set
            {
                if(value > 24)
                {
                    _selectedHour = 12;
                }
                else if (value > 12)
                {
                    _selectedHour = value - 12;
                    SelectedTimeOfDay = "PM";
                } else
                {
                    _selectedHour = value;                   
                }
                OnPropertyChanged("SelectedHour");
            }
        }
        public int SelectedMinute
        {
            get { return _selectedMinute; }
            set
            {
                if(value > 59)
                {
                    _selectedMinute = 00;
                }
                else
                {
                    int x = value % 5; //round down to nearest whole 5
                    _selectedMinute = value - x;
                }                             
                OnPropertyChanged("SelectedMinute");
            }
        }
        public string SelectedTimeOfDay
        {
            get { return _selectedTimeOfDay; }
            set
            {
                _selectedTimeOfDay = value;
                OnPropertyChanged("SelectedTimeOfDay");
            }
        }
        public TimeSpan SelectedTime
        {
            get 
            {
                int hour = _selectedHour;
                int min = _selectedMinute;
                if(_selectedTimeOfDay == "PM" && hour != 12)
                {
                    hour += 12;
                }else if(_selectedTimeOfDay == "AM" && hour == 12)
                {
                    hour -= 12;
                }
                return new TimeSpan(hour, min, 00);
            }
            set
            {
                if (!hasLoaded)
                {
                    hasLoaded = true;
                }
                SelectedHour = value.Hours;
                SelectedMinute = value.Minutes;
                Console.WriteLine(value);
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if(!hasLoaded)
            {
                this.SelectedTime = new TimeSpan(7, 30, 0);
                hasLoaded = true;
            }
            
        }
    }
}
