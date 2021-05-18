using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfPresentation.ServiceListView
{
    /// <summary>
    /// Interaction logic for ChildcareAssistanceView.xaml
    /// </summary>
    public partial class ChildcareAssistanceView : Page
    {
        private string childcareAssitancePage = "Childcare Assistance Page";
        public string ChildcareAssitancePage { get { return childcareAssitancePage; } }
        public ChildcareAssistanceView()
        {
            InitializeComponent();
        }

        private void btnLocalProviders_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void btnTypes_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
