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
using System.Windows.Shapes;
using WpfPresentation.Validators;

namespace WpfPresentation.ZipCodeViews
{
    /// <summary>
    /// Chase Martin
    /// Created: 2021/2/19
    /// Interaction logic for ZipCodeListView.xaml
    /// </summary>
    public partial class EditZipCodeView : Window
    {

        private ZipCodeFile _zipCode;



        private bool _addZipCode = false;
        private IZipCodeManager _zipCodeManager = new ZipCodeManager();




        public EditZipCodeView()
        {
            _zipCode = new ZipCodeFile();
            _addZipCode = true;

            InitializeComponent();
        }
        public EditZipCodeView(ZipCodeFile zipCode)
        {
            _zipCode = zipCode;

            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_addZipCode)
            {
                txtZipCode.Text = "";
                txtCity.Text = "";
                txtState.Text = "";
                chkIsServicable.IsChecked = true;

                setUpAdd();
                chkIsServicable.IsEnabled = false;

            }
            else
            {
                txtZipCode.Text = _zipCode.ZipCode.ToString();
                txtCity.Text = _zipCode.City;
                txtState.Text = _zipCode.State;
                chkIsServicable.IsChecked = _zipCode.isServicable;
            }
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/2/19
        /// When clicked, saves updated zip code
        /// </summary>
        private void btnUpdateSave_Click(object sender, RoutedEventArgs e)
        {
            performZipCodeValidation();

            if (((string)btnSave.Content) == "Edit")
            {
                setUpAdd();
            }
            else
            {
                setUpEdit();

                if (_addZipCode == false)
                {


                    var newZipCode = new ZipCodeFile()
                    {
                        ZipCode = txtZipCode.Text,
                        City = txtCity.Text,
                        State = txtState.Text,
                        isServicable = (bool)chkIsServicable.IsChecked//_zipCode.isServicable
                    };

                    try
                    {
                        _zipCodeManager.EditZipCodeFile(_zipCode, newZipCode);
                        this.DialogResult = true;
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                    }
                }
                else
                {

                    var newZipCode = new ZipCodeFile()
                    {
                        ZipCode = txtZipCode.Text,
                        City = txtCity.Text,
                        State = txtState.Text,
                        isServicable = (bool)chkIsServicable.IsChecked
                    };

                    try
                    {
                        _zipCodeManager.AddZipCode(newZipCode);
                        this.DialogResult = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                    }
                }
            }
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/2/19
        /// Sets up blank Zip code form for adding
        /// a new zip code file.
        /// </summary>
        private void setUpAdd()
        {
            btnSave.Content = "Add";
            txtZipCode.IsReadOnly = false;
            txtCity.IsReadOnly = false;
            txtState.IsReadOnly = false;
            chkIsServicable.IsEnabled = true;
            txtZipCode.BorderBrush = Brushes.Black;
            txtCity.BorderBrush = Brushes.Black;
            txtState.BorderBrush = Brushes.Black;
            txtZipCode.Focus();
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/2/19
        /// Sets up exisiting zip code file for editing
        /// </summary>
        private void setUpEdit()
        {
            btnSave.Content = "Update";
            txtZipCode.IsReadOnly = false;
            txtCity.IsReadOnly = false;
            txtState.IsReadOnly = false;
            chkIsServicable.IsEnabled = true;
            txtZipCode.BorderBrush = Brushes.Black;
            txtCity.BorderBrush = Brushes.Black;
            txtState.BorderBrush = Brushes.Black;
            txtZipCode.Focus();
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/2/19
        /// Used validation method performZipCodeValidation() from Chantal Shirley.
        /// </summary>
        public void performZipCodeValidation()
        {


            string[] userInputs = {
                txtZipCode.Text.Trim(),
                txtCity.Text.Trim(),
                txtState.Text.Trim()
            };

            string zipCode = txtZipCode.Text.Trim();

            if (!zipCode.isAnInteger())
            {
                MessageBox.Show("Employee IDs must be valid numbers.", "Invalid Employee ID", MessageBoxButton.OK, MessageBoxImage.Error);
                txtZipCode.Focus();
                return;
            }
            if (userInputs.containsEmptyString())
            {
                MessageBox.Show("Forms must be fully filled out to add Driver's License information.", "Incomplete" +
                    " Form", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/2/19
        /// When clicked, cancels task at hand.
        /// </summary>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}

