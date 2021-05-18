using LogicInterfaces;
using LogicLayer;
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

namespace WpfPresentation.InventoryManagementViews
{
    /// <summary>
    /// Interaction logic for ViewInventoryItemView.xaml
    /// </summary>
    public partial class ViewInventoryItemView : Page
    {
        private IInventoryManager _inventoryManager = new InventoryManager();
        private string pageName = "View Inventory Item";
        public string PageName { get { return pageName; } }
        public ViewInventoryItemView()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                dgViewItems.ItemsSource = _inventoryManager.RetrieveAllInventoryItems();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }
    }
}
