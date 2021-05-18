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
    /// Interaction logic for AddZipCodeVIew.xaml
    /// </summary>
    public partial class AddZipCodeView : Window
    {

        private ZipCodeFile _zipCode;



        private bool _addZipCode = false;
        private IZipCodeManager _zipCodeManager = new ZipCodeManager();




        public AddZipCodeView()
        {
            _zipCode = new ZipCodeFile();
            _addZipCode = true;

            InitializeComponent();
        }
        public AddZipCodeView(ZipCodeVM zipCode)
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
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/2/19
        /// When clicked, saves created zip code file.
        /// </summary>
        private void btnUpdateSave_Click(object sender, RoutedEventArgs e)
        {
            performZipCodeValidation();

            if (((string)btnSave.Content) == "Edit")
            {
                //setUpAdd();
                //setUpEdit();
            }
            else
            {

                if (_addZipCode == false)
                {


                    //var newZipCode = new ZipCodeVM()
                    //{
                    //    ZipCode = txtZipCode.Text,
                    //    City = txtCity.Text,
                    //    State = txtState.Text,
                    //    isServicable = (bool)chkIsServicable.IsChecked
                    //};

                    //try
                    //{
                    //    _zipCodeManager.EditZipCodeFile(_zipCode, newZipCode);
                    //    this.DialogResult = true;
                    //}
                    //catch (Exception ex)
                    //{

                    //    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                    //}
                }
                else
                {

                    var newZipCode = new ZipCodeFile()//File?
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
        /// When called, sets up add form.
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
        /// Chase Martin/Chantal Shirley
        /// Created: 2021/2/19
        /// Borrowed from Chantal Shirley for validation.
        /// </summary>
        public void performZipCodeValidation()
        {


            string[] userInputs = {
                txtZipCode.Text.Trim(),
                txtCity.Text.Trim(),
                txtState.Text.Trim()
            };

            string zipCode = txtZipCode.Text.Trim();

            //if (!zipCode.isAnInteger())
            //{
            //    MessageBox.Show("Zip Codes must be valid numbers.", "Invalid Zip Code", MessageBoxButton.OK, MessageBoxImage.Error);
            //    txtZipCode.Focus();
            //    return;
            //}
            //if (userInputs.containsEmptyString())
            //{
            //    MessageBox.Show("Forms must be fully filled out to add Zip Code information.", "Incomplete" +
            //        " Form", MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;
            //}

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


