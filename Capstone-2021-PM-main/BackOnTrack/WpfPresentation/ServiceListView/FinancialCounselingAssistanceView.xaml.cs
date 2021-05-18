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
    /// Chase Martin
    /// Created: 2021/03/09
    /// 
    /// Interaction logic for FinancialCounselingAssistanceView.xaml
    /// </summary>



    public partial class FinancialCounselingAssistanceView : Page
    {
        private string financialCounselingPage = "Financial Counseling page";
        public string FinancialCounselingPage { get { return financialCounselingPage; } }
        public FinancialCounselingAssistanceView()
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

