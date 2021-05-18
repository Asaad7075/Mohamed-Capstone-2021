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
using LogicInterfaces;
using LogicLayer;

namespace WpfPresentation.ServiceListView
{
    /// <summary>
    /// Chase Martin
    /// Created: 2021/03/09
    /// 
    /// Interaction logic for ListFinancialCounselingTypesView.xaml
    /// </summary>
    public partial class ListFinancialCounselingTypesView : Page
    {
        private IFinancialCounselingManager _financialCounselingManager = new FinancialCounselingManager();

        private string financialCounselingTypes = "Financial Counseling Types";
        public string FinancialCounselingTypes { get { return financialCounselingTypes; } }
        public ListFinancialCounselingTypesView()
        {
            InitializeComponent();
        }

        private void btnTypes_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dgViewFinancialCounselingTypes.ItemsSource = _financialCounselingManager.RetrieveAllCounselingTypes();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }

        private void btnLocalProviders_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }


        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
