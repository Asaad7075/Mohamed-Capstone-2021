using DomainModels;
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
    /// Interaction logic for DeleteInventoryItemView.xaml
    /// </summary>
    public partial class DeleteInventoryItemView : Page
    {
        private IInventoryManager _inventoryManager = new InventoryManager();
        private string pageName = "Delete Inventory Item";
        public string PageName { get { return pageName; } }
        public DeleteInventoryItemView()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                dgViewItemsToDelete.ItemsSource = _inventoryManager.RetrieveAllInventoryItems();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }

        private void btnDeleteItem_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = (Inventory)dgViewItemsToDelete.SelectedItem;
            if (selectedItem == null)
            {
                MessageBox.Show("You must select an item first!");
            }
            else
            {
                try
                {
                    _inventoryManager.DeleteInventoryItem(selectedItem.InventoryID);
                    MessageBox.Show("Item: " + selectedItem.ItemName + " was deleted");
                    dgViewItemsToDelete.ItemsSource = _inventoryManager.RetrieveAllInventoryItems();
                }
                catch (Exception)
                {
                    MessageBox.Show("Item could not be deleted");
                }
            }
        }
    }
}
