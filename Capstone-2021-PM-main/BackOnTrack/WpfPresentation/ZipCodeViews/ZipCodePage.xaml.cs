using DataAccessFakes;
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
using System.Windows.Input;
using System.Windows.Navigation;


namespace WpfPresentation.ZipCodeViews
{
    /// <summary>
    /// Chase Martin
    /// Created: 2021/2/13
    /// Interaction logic for ZipCodeListView.xaml
    /// </summary>
    public partial class ZipCodePage : Page
    {
        private IZipCodeManager _zipCodeManager = new ZipCodeManager();

        private string pageName = "Zip Code List";
        public string PageName { get { return pageName; } }

        public ZipCodePage()
        {
            InitializeComponent();
        }

        private void resetWindow()
        {
            dgZipCodeList.ItemsSource = null;
        }


        /// <summary>
        /// Chase Martin
        /// Created: 2021/03/26
        /// 
        /// Displays edit zip code window to edit zip code 
        /// when double cliked
        /// </summary>
        /// <returns></returns>
        private void dgZipCodeList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EditZipCode();
        }


        /// <summary>
        /// Chase Martin
        /// Created: 2021/03/26
        /// 
        /// Add Zip Code method for when a user
        /// clicks the add button
        /// </summary>
        /// <returns></returns>
        private void AddZipCode()
        {
            var selectedItem = (ZipCodeVM)dgZipCodeList.SelectedItem;
            if (selectedItem == null)
            {
                return;
            }

            var addEditWindow = new AddZipCodeView(selectedItem);
            if (addEditWindow.ShowDialog() == true)
            {
                var zipCodeManager = new ZipCodeManager();
                dgZipCodeList.ItemsSource =
                    zipCodeManager.RetrieveAllZipCodes();


                dgZipCodeList.Columns[0].Header = "Zip Code";
                dgZipCodeList.Columns[1].Header = "City";
                dgZipCodeList.Columns[2].Header = "State";
                dgZipCodeList.Columns[3].Header = "Is Servicable";
            }
        }


        /// <summary>
        /// Chase Martin
        /// Created: 2021/03/26
        /// 
        /// Edit Zip Code method for when a user
        /// clicks the edit button
        /// </summary>
        /// <returns></returns>
        private void EditZipCode()
        {
            var selectedItem = (ZipCodeFile)dgZipCodeList.SelectedItem;
            if (selectedItem == null)
            {
                return;
            }

            var addEditWindow = new EditZipCodeView(selectedItem);
            if (addEditWindow.ShowDialog() == true)
            {
                var zipCodeManager = new ZipCodeManager();
                dgZipCodeList.ItemsSource =
                    zipCodeManager.RetrieveAllZipCodes();


                dgZipCodeList.Columns[0].Header = "Zip Code";
                dgZipCodeList.Columns[1].Header = "City";
                dgZipCodeList.Columns[2].Header = "State";
                dgZipCodeList.Columns[3].Header = "Is Servicable";
            }
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/03/26
        /// 
        /// Update button calls EditZipCode() to edit selected zip code
        /// </summary>
        /// <returns></returns>
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = (ZipCodeFile)dgZipCodeList.SelectedItem;
            if (selectedItem == null)
            {
                MessageBox.Show("You need to select a zip code to edit!", "Invalid Operation",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            EditZipCode();

        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/03/26
        /// 
        /// Add button retrieves all exisiting zip codes in addition to 
        /// the one the user just added
        /// </summary>
        /// <returns></returns>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var addEditWindow = new AddZipCodeView();
            if (addEditWindow.ShowDialog() == true)
            {
                var zipCodeManager = new ZipCodeManager();
                dgZipCodeList.ItemsSource =
                    zipCodeManager.RetrieveAllZipCodes();//RetrieveZipCodesByIsServicable

                dgZipCodeList.Columns[0].Header = "Zip Code";
                dgZipCodeList.Columns[1].Header = "City";
                dgZipCodeList.Columns[2].Header = "State";
                dgZipCodeList.Columns[3].Header = "Is Servicable";

            }
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/03/26
        /// 
        /// Views the list of zip codes when page is loaded
        /// by calling RetrieveAllZipCodes()
        /// </summary>
        /// <returns></returns>
        private void zipCodeListView_Loaded(object sender, RoutedEventArgs e)
        {

            dgZipCodeList.ItemsSource = _zipCodeManager.RetrieveAllZipCodes();

            dgZipCodeList.Columns[0].Header = "Zip Code";
            dgZipCodeList.Columns[1].Header = "City";
            dgZipCodeList.Columns[2].Header = "State";
            dgZipCodeList.Columns[3].Header = "Is Servicable";


        }
    }
}
