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
    /// Interaction logic for InsertInventoryItemView.xaml
    /// </summary>
    public partial class InsertInventoryItemView : Page
    {
        private IInventoryManager _inventoryManager = new InventoryManager();
        private string pageName = "Insert Inventory Item";
        public string PageName { get { return pageName; } }
        public InsertInventoryItemView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Thomas Stout
        /// Created: 2021/02/18
        /// 
        /// Saves the item to the Inventory table in the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddItemInformation_Click(object sender, RoutedEventArgs e) // Look up isNullOrWhiteSpace to have better string validation!
        {
            int inventoryQuantity;
            if (txtItemName.Text == "" || txtItemQuantity.Text == "" || txtItemDescription.Text == "") // Checks to make sure text isn't empty
            {
                MessageBox.Show("Missing information is required to submit form."); // Informs the user that they are missing required information
                return;
            }
            if ((txtItemQuantity.Text).isAnInteger()) // If the inputed text is a valid integer, the string is parsed
            {
                inventoryQuantity = int.Parse(txtItemQuantity.Text); // The inputed text is parsed to an int
            }
            else
            {
                MessageBox.Show("Item Quantity must be a valid number (1 or greater)."); // Displays if quantity inputed was not an integer
                return;
            }
            string itemName = txtItemName.Text;
            string itemDescription = txtItemDescription.Text;
            Inventory inventory = new Inventory()
            {
                InventoryQuantity = inventoryQuantity,  // Sets the InventoryQuantity
                ItemName = itemName,                    // Sets the ItemName
                ItemDescription = itemDescription       // Sets the ItemDescription
            };
            try
            {
                _inventoryManager.AddInventoryItem(inventory); // Item is added to inventory
                MessageBox.Show("The item: '" + itemName + "' was added to inventory!");
                clearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException ?? ex.InnerException.Message); // Item was not added to inventory
            }
        }

        /// <summary>
        /// Thomas Stout
        /// Created: 2021/02/19
        /// 
        /// Sets all of the text boxes to empty strings by calling clearFields().
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelAddItemInformation_Click(object sender, RoutedEventArgs e)
        {
            clearFields();
        }
        /// <summary>
        /// Thomas Stout
        /// Created: 2021/02/19
        /// 
        /// Sets all of the text boxes to empty strings.
        /// </summary>
        private void clearFields()
        {
            txtItemName.Text = "";
            txtItemQuantity.Text = "";
            txtItemDescription.Text = "";
        }
    }
}
