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
    /// Interaction logic for ListFinancialChildcareTypesView.xaml
    /// </summary>
    public partial class ListChildcareTypesView : Page
    {
        private IChildcareManager _childcareManager = new ChildcareManager();

        private string childcarePageTypes = "Childcare Page Types";
        public string ChildcarePageTypes { get { return childcarePageTypes; } }

        public ListChildcareTypesView()
        {
            InitializeComponent();
        }

        private void btnTypes_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dgViewChildcareTypes.ItemsSource = _childcareManager.RetrieveAllChildcareTypes();
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
