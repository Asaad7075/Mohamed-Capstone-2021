using DataAccessFakes;
using DomainModels;
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

namespace WpfPresentation.SupplyManagementViews.AddEditSupplyItem
{
    /// <summary>
    /// Interaction logic for pageAddEditSupplyItem.xaml
    /// </summary>
    public partial class pageAddEditSupplyItem : Page
    {
        //private SupplyInventoryManager _supplyInventoryManager = new SupplyInventoryManager(new SupplyItemFake()); // manager to test data fakes
        private SupplyInventoryManager _supplyInventoryManager = new SupplyInventoryManager(); // manager to test DB data
        private SupplyItem _supplyItem = new SupplyItem();
        private string pageName = "Add/Edit Supply Items";
        public string PageName { get { return pageName; } }

        /// <summary>
        /// Richard Schroeder
        /// Created: 2021/03/05
        /// 
        /// Page for managing supply inventory
        /// </summary>
        public pageAddEditSupplyItem()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Richard Schroeder
        /// Created: 2021/03/12
        /// 
        /// Method to refresh supply list
        /// </summary>
        private void RefreshSupplyList()
        {
            // Displays all SupplyItems to DataGrid, renames columns, hides DB itemID
            dgSupplyInventory.ItemsSource = _supplyInventoryManager.ShowSupplyInventory();

            dgSupplyInventory.Columns[0].Visibility = Visibility.Hidden;
            dgSupplyInventory.Columns[1].Header = "Serial Number";
            dgSupplyInventory.Columns[2].Header = "Material";
            dgSupplyInventory.Columns[3].Header = "Description";
            dgSupplyInventory.Columns[4].Header = "Quantity";
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshSupplyList();
        }

        /// <summary>
        /// Richard Schroeder
        /// Created: 2021/03/12
        /// 
        /// Logic to add supply item to DB
        /// </summary>
        private void btnAddSupplyItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // try to parse quantity and serial number as ints, returns false if input is not int
                // or parses to int if input is valid
                bool quantityCanParse = int.TryParse(txtSupplyInventoryQuantity.Text, out int parseQuantity);
                bool serialNumCanParse = int.TryParse(txtSupplySerialNumber.Text, out int parseSerialNum);

                // Checks if input is blank or not valid
                if (txtSupplyDescription.Text == "" ||
                    txtSupplyInventoryQuantity.Text == "" ||
                    txtSupplyMaterialName.Text == "" ||
                    txtSupplySerialNumber.Text == "" ||
                    parseSerialNum < 100000 ||
                    parseSerialNum > 999999)
                {
                    MessageBox.Show("Please enter valid data.");
                    return;
                }
                else
                {
                    //constructs new SupplyItem object and passes
                    SupplyItem newSupplyItem = new SupplyItem()
                    {
                        SupplySerialNumber = parseSerialNum,
                        MaterialName = txtSupplyMaterialName.Text,
                        SupplyDescription = txtSupplyDescription.Text,
                        SupplyInventoryQuantity = parseQuantity
                    };

                    _supplyInventoryManager.AddSupplyItem(newSupplyItem);
                    MessageBox.Show("Item added!");
                    RefreshSupplyList(); // refresh supply list
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Supply Item insertion failed" + ex.InnerException.Message);
            }

        }

        /// <summary>
        /// Richard Schroeder
        /// Created: 2021/03/12
        /// 
        /// Logic to edit and save changes to a supply item
        /// </summary>
        private void btnEditSupplyItem_Click(object sender, RoutedEventArgs e)
        {
            
            var selectedItem = (SupplyItem)dgSupplyInventory.SelectedItem;
            if (selectedItem == null)
            {
                MessageBox.Show("Must select an item to edit.");
                return;
            }
            
            // prepare item for edit
            if((string)btnEditSupplyItem.Content == "Edit")
            {
                btnEditSupplyItem.Content = "Save";
                txtSupplyItemId.Text = selectedItem.SupplyItemID.ToString();
                txtSupplySerialNumber.Text = selectedItem.SupplySerialNumber.ToString();
                txtSupplyInventoryQuantity.Text = selectedItem.SupplyInventoryQuantity.ToString();
                txtSupplyMaterialName.Text = selectedItem.MaterialName;
                txtSupplyDescription.Text = selectedItem.SupplyDescription;
                btnAddSupplyItem.IsEnabled = false; // disable add whilst editing
                btnDeleteSupplyItem.IsEnabled = false; // disable delete whilst editing
                return; // break out to incur "Save" check
            }

            // save edits
            if ((string)btnEditSupplyItem.Content == "Save")
            {
                try
                {
                    bool quantityCanParse = int.TryParse(txtSupplyInventoryQuantity.Text, out int parseQuantity);
                    bool serialNumCanParse = int.TryParse(txtSupplySerialNumber.Text, out int parseSerialNum);
                    SupplyItem newSupplyItem = new SupplyItem()
                    {
                        SupplySerialNumber = parseSerialNum,
                        MaterialName = txtSupplyMaterialName.Text,
                        SupplyDescription = txtSupplyDescription.Text,
                        SupplyInventoryQuantity = parseQuantity
                    };

                    _supplyInventoryManager.EditSupplyItem(selectedItem, newSupplyItem);
                    MessageBox.Show("Item updated!");

                    // clear text boxes
                    txtSupplyItemId.Text = "Added automatically";
                    txtSupplySerialNumber.Text = "";
                    txtSupplyInventoryQuantity.Text = "";
                    txtSupplyMaterialName.Text = "";
                    txtSupplyDescription.Text = "";
                    // switch function
                    btnEditSupplyItem.Content = "Edit";
                    //enable add and delete
                    btnAddSupplyItem.IsEnabled = true;
                    btnDeleteSupplyItem.IsEnabled = true;
                    // display updated list
                    RefreshSupplyList();
                    return; // break out to prep for edit
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Update failed" + ex.InnerException.Message);
                }
            }
        }

        /// <summary>
        /// Richard Schroeder
        /// Created: 2021/03/12
        /// 
        /// Logic move selected item from DG to text fields
        /// </summary>
        private void dgSupplyInventory_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = (SupplyItem)dgSupplyInventory.SelectedItem;
            
            txtSupplyItemId.Text = selectedItem.SupplyItemID.ToString();
            txtSupplySerialNumber.Text = selectedItem.SupplySerialNumber.ToString();
            txtSupplyInventoryQuantity.Text = selectedItem.SupplyInventoryQuantity.ToString();
            txtSupplyMaterialName.Text = selectedItem.MaterialName;
            txtSupplyDescription.Text = selectedItem.SupplyDescription;
        }

        private void btnDeleteSupplyItem_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = (SupplyItem)dgSupplyInventory.SelectedItem;
            if (selectedItem == null)
            {
                MessageBox.Show("Must select an item to edit.");
                return;
            }
            else
            {
                try
                {
                    _supplyInventoryManager.DeleteSupplyItem(selectedItem.SupplyItemID);
                    RefreshSupplyList();
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Deletion failed" + ex.InnerException.Message);
                }
            }
        }
    }
}
