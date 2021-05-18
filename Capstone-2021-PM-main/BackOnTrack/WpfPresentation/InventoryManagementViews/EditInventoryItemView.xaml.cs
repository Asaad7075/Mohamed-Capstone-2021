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
using WpfPresentation.Validators;

namespace WpfPresentation.InventoryManagementViews
{
    /// <summary>
    /// Interaction logic for EditInventoryItemView.xaml
    /// </summary>
    public partial class EditInventoryItemView : Page
    {
        private IInventoryManager _inventoryManager = new InventoryManager();
        private string pageName = "Edit Inventory Item";
        public string PageName { get { return pageName; } }
        public EditInventoryItemView()
        {
            InitializeComponent();
        }

        //private void editItem()
        //{
        //    var selectedItem = (Inventory)dgViewItemsToEdit.SelectedItem;
        //    if (selectedItem == null)
        //    {
        //        return;
        //    }

        //    MessageBox.Show(selectedItem.ItemName);
        //    //var addEditPage = new ActualItemEditorView(selectedItem);
        //    //if (addEditPage.IsInitialized)
        //    //{
        //    //    var inventoryManager = new InventoryManager();
        //    //    dgViewItemsToEdit.ItemsSource =
        //    //        inventoryManager.RetrieveAllInventoryItems();
                
        //    //}

            
        //}

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                dgViewItemsToEdit.ItemsSource = _inventoryManager.RetrieveAllInventoryItems();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }
        // Look up isNullOrWhiteSpace to have better string validation!
        private void btnEditItemInformation_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = (Inventory)dgViewItemsToEdit.SelectedItem;
            if (selectedItem == null)
            {
                MessageBox.Show("You must select an item first!", "Invalid Operation",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            int quantity;
            if (txtNewItemQuantity.Text.isAnInteger())
            {
                quantity = int.Parse(txtNewItemQuantity.Text);
            }
            else
            {
                MessageBox.Show("Item Quantity must be a valid integer (1 or greater)!");
                return;
            }
            if (selectedItem != null && txtNewItemDescription.Text == "" && txtNewItemName.Text == "" && txtNewItemQuantity.Text == "")
            {
                MessageBox.Show("You must enter the new item information before clicking 'Edit Item'");
                return;
            }
            string oldItem = selectedItem.ItemName;
            string itemName = txtNewItemName.Text;
            string itemDescription = txtNewItemDescription.Text;
            Inventory inventory = new Inventory()
            {
                InventoryID = selectedItem.InventoryID,
                InventoryQuantity = quantity,
                ItemName = itemName,
                ItemDescription = itemDescription
            };
            try
            {
                _inventoryManager.EditInventoryItem(inventory);
                MessageBox.Show("The item: '" + oldItem + "' was edited!");
                dgViewItemsToEdit.ItemsSource = _inventoryManager.RetrieveAllInventoryItems();
                clearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException ?? ex.InnerException.Message); // Item was not edited
            }
        }
        private void clearFields()
        {
            txtNewItemDescription.Text = "";
            txtNewItemName.Text = "";
            txtNewItemQuantity.Text = "";
        }

        private void btnCancelEditItemInformation_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = (Inventory)dgViewItemsToEdit.SelectedItem;
            selectedItem = null;
            clearFields();
        }
    }
}
