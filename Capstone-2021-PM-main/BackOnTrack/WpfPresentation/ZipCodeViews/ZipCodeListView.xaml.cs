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


namespace WpfPresentation.ZipCodeViews
{
    /// <summary>
    /// Chase Martin
    /// Created: 2021/2/13
    /// Interaction logic for ZipCodeListView.xaml
    /// </summary>

    public partial class ZipCodeListView : Page
    {

        private IZipCodeManager _zipCodeManager = new ZipCodeManager();
        private bool _zipCodesUpdated = false;

        private string pageName = "Zip Code List";
        public string PageName { get { return pageName; } }

        public ZipCodeListView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/2/19
        /// When called, shows tabs to select for zip code tasks.
        /// </summary>
        private void showUserTabs()
        {
            grdUserAdmin.Visibility = Visibility.Visible;
            tabZipCodes.Visibility = Visibility.Visible;
            tabZipCodes.IsSelected = true;
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/2/19
        /// When called resets window.
        /// </summary>
        private void resetWindow()
        {
            showUserTabs();
            dgZipCodeList.ItemsSource = null;
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/2/19
        /// When focused, displays zip code list along with CRUD btns.
        /// </summary>
        private void tabZipCodes_GotFocus(object sender, RoutedEventArgs e)
        {
            if (((TabItem)sender).Visibility == Visibility.Visible)
            {
                try
                {
                    if (dgZipCodeList.ItemsSource == null || _zipCodesUpdated == true)
                    {
                        dgZipCodeList.ItemsSource = _zipCodeManager.RetrieveAllZipCodes();

                        dgZipCodeList.Columns.Remove(dgZipCodeList.Columns[0]);
                        dgZipCodeList.Columns[0].Header = "Zip Code";
                        dgZipCodeList.Columns[1].Header = "City";
                        dgZipCodeList.Columns[2].Header = "State";
                        dgZipCodeList.Columns[3].Header = "Is Servicable";
                        dgZipCodeList.Columns.Remove(dgZipCodeList.Columns[4]);
                        _zipCodesUpdated = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                }
            }

        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/2/19
        /// When double clicked, calls EditZipCode to edit selected zip code.
        /// </summary>
        private void dgZipCodeList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EditZipCode();
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/2/19
        /// When called, brings up add zip code form.
        /// </summary>
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
        /// Created: 2021/2/19
        /// When called, brings up edit zip code form.
        /// </summary>
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

        //private void btnUpdate_Click(object sender, RoutedEventArgs e)
        //{
        //    var selectedItem = (ZipCodeFile)dgZipCodeList.SelectedItem;
        //    if (selectedItem == null)
        //    {
        //        MessageBox.Show("You need to select a zip code to edit!", "Invalid Operation",
        //            MessageBoxButton.OK, MessageBoxImage.Information);
        //        return;
        //    }
        //    AddZipCode();

        //}

        /// <summary>
        /// Chase Martin
        /// Created: 2021/2/19
        /// When clicked, saves changes to selected zip code withing the edit form.
        /// </summary>
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
        /// Created: 2021/2/19
        /// When clicked, brings up add zip code form.
        /// </summary>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var addEditWindow = new AddZipCodeView();
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
        /// Created: 2021/2/19
        /// When loaded, the list of zip codes is dsplayed.
        /// </summary>
        private void zipCodeListView_Loaded(object sender, RoutedEventArgs e)
        {

            dgZipCodeList.ItemsSource = _zipCodeManager.RetrieveAllZipCodes();

            dgZipCodeList.Columns[0].Header = "Zip Code";
            dgZipCodeList.Columns[1].Header = "City";
            dgZipCodeList.Columns[2].Header = "State";
            dgZipCodeList.Columns[3].Header = "Is Servicable";
            _zipCodesUpdated = false;


        }
    }
}


