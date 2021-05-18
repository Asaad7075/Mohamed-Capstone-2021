using DomainModels;
using LogicInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
//using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.IO;
using LogicLayer;

namespace WpfPresentation.MaterialHandlingView
{
    /// <summary>
    /// Interaction logic for AddDonationItem.xaml
    /// </summary>
    public partial class AddDonationItem : Page
    {
        //private Donation _donation;
        private IDonationManager _donationManager;
        private bool _isAdd = true;
        //private string strName;
        //private string imageName;

        private string pageName = "Add Donation ";
        public string PageName { get { return pageName; } }
        public AddDonationItem()
        {
            InitializeComponent();
            _donationManager = new DonationManager();
            
        }
        public AddDonationItem(IDonationManager donationManager)
        {
            InitializeComponent();
            _donationManager = donationManager;
            _isAdd = false;
        }
        /// <summary>
        /// Asaad Mohamed 
        /// created: 2021/03/24
        /// Logic to add and save new donaion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //insertImage();
                //validate all the inputs
                bool donorID = int.TryParse(txtDonorID.Text, out int parsDonorID);
               
                if (txtDonorID.Text == "")
                {
                    MessageBox.Show("Donor ID can not be null");
                    txtDonorID.Focus();
                    return;

                }
                else if (donorID == false)
                {
                    MessageBox.Show("You must enter a number between(100,0000 and  99,99999)");
                    txtDonorID.Focus();
                    return;
                }
                else if (parsDonorID < 1000000 ||
                        parsDonorID > 9999999)
                {
                    MessageBox.Show("You must enter valid number between(100,0000 and  99,99999)");
                    txtDonorID.Focus();
                    return;
                }
                if (txtNameItem.Text == "")
                {
                    MessageBox.Show("Name of item can not be null");
                    txtNameItem.Focus();
                    return;

                }
                if (txtDescription.Text == "")
                {
                    MessageBox.Show("Description can not be null");
                    txtDescription.Focus();
                    return;

                }
                bool estValue = decimal.TryParse(txtEstValue.Text, out decimal validEstValue);
                if (txtEstValue.Text == "")
                {
                    MessageBox.Show("estimate  Value can not be null");
                    txtEstValue.Focus();
                    return;
                }
                else if (estValue == false)
                {
                    MessageBox.Show(" You must enter a number");
                    txtEstValue.Focus();
                    return;
                }
                else if (validEstValue < 0.0M ||
                        validEstValue > 999999999.0M)
                {
                    MessageBox.Show(" You must enter valid estimate  Value");
                    txtEstValue.Focus();
                    return;
                }
                bool ageOfItem = int.TryParse(txtAgeOfItem.Text, out int validAgeItem);
                if (txtAgeOfItem.Text == "")
                {
                    MessageBox.Show("Age of Item can not be null");
                    txtAgeOfItem.Focus();
                    return;
                }
                else if (ageOfItem == false)
                {
                    MessageBox.Show(" You must enter a number of Age of item");
                    txtAgeOfItem.Focus();
                    return;
                }
                else if (validAgeItem < 0 ||
                        validAgeItem > 99999999)
                {
                    MessageBox.Show(" You must enter valid Age of item");
                    txtAgeOfItem.Focus();
                    return;
                }
                if (txtPickUpDate.Text == "")
                {
                    MessageBox.Show("You must select date");
                    txtPickUpDate.Focus();
                    return;

                }
                if (cboStatus.Text == "")
                {
                    MessageBox.Show("You must select status");
                    cboStatus.Focus();
                    return;

                }

                 if (_isAdd)
                {

                    //new donation object 
                    Donation newDonation = new Donation()

                    {

                        DonorID = int.Parse(txtDonorID.Text),
                        NameOfItem = txtNameItem.Text,
                        Description = txtDescription.Text,
                        EstValue = decimal.Parse(txtEstValue.Text),
                        AgeofItem = int.Parse(txtAgeOfItem.Text),
                        DropOff = chkDropOff.IsChecked.Value,
                        PickUp = chkPickUp.IsChecked.Value,
                        PickUpDateTime = txtPickUpDate.SelectedDate.Value,
                        EmailReceipt = chkEmailReceipt.IsChecked.Value,
                        MailReceipt = chkMailReceipt.IsChecked.Value,
                        DonationStatus = cboStatus.Text


                    };
                    if (!_isAdd) 
                    {
                        MessageBox.Show(" The aplication failed to add.");
                        this.NavigationService?.Navigate(new ViewDonationForms());
                        clearFields();
                    }

                else
                {
                    _donationManager.InsertDonation(newDonation);

                    MessageBox.Show("The donation has been added Success!", "Successful added",
                       MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    this.NavigationService?.Navigate(new ViewDonation());
                     clearFields();
                }
                }
            
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + "\n\n"  + ex.InnerException.Message);
            }
        
    }
       
      
            //private void insertImage()
            //{
            //    try
            //    {
            //        if (imageName != "")
            //        {
            //            FileStream fileStream = new FileStream(imageName, FileMode.Open, FileAccess.Read);
            //            byte[] imageByte = new byte[fileStream.Length];

            //            //read data from the file stream and put into the byte array
            //            fileStream.Read(imageByte, 0, Convert.ToInt32(fileStream.Length));

            //            // Close file stream
            //            fileStream.Close();
            //        }

            //    }
            //    catch (Exception)
            //    {

            //        MessageBox.Show("Image failed to insert");
            //        imageList.Focus();
            //        return;
            //    }
            //}

            /// <summary>
            /// Asaad Mohamed
            /// Created: 2021/03/24
            /// event handler for cancel 
            /// </summary>
            private void btnCancel_Click(object sender, RoutedEventArgs e)
           {
            clearFields();
        }
        /// <summary>
        /// Asaad Mohamed
        /// created: 2021/03/24
        /// This method to clear all the fields after insert all the data
        /// </summary>
        private void clearFields()
        {
            lblDonationID.Content = "Assigned Automatically";
            txtDonorID.Text = "";
            txtAgeOfItem.Text = "";
            txtDescription.Text = "";
            txtEstValue.Text = "";
            txtNameItem.Text = "";
            chkDropOff.IsChecked = false;
            chkPickUp.IsChecked = false;
            txtPickUpDate.SelectedDate = txtPickUpDate.SelectedDate;
            chkEmailReceipt.IsChecked = false;
            chkMailReceipt.IsChecked = false;
            cboStatus.SelectedItem = false;
            cboStatus.Text = "Select the status";
            lblDonationID.Visibility = Visibility.Hidden;
            lblDonation.Visibility = Visibility.Hidden;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_isAdd == false)
                {
                    clearFields();
                }

                else
                {
                    btnSave.Content = "Save";
                    lblDonationID.Content = "Assigned Automatically";
                    lblDonationID.Visibility = Visibility.Hidden;
                    lblDonation.Visibility = Visibility.Hidden;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Lists could not be populated.\n" + ex.Message + "\n" + ex.StackTrace);
            }   
        }

        //private void btnBrowseButton_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {

        //        FileDialog filedlg = new OpenFileDialog();
        //        filedlg.InitialDirectory = Environment.SpecialFolder.MyDocuments.ToString();
        //        filedlg.Filter = "Image Files(*.jpg)|*.jpg|PNG Files(*.png)|*.png|All Files(*.*)|*.*";
        //        filedlg.ShowDialog();
        //        //Nullable<bool> result = filedlg.ShowDialog();

        //        //if (result == true)

        //        {
        //            strName = filedlg.SafeFileName;
        //            imageName = filedlg.FileName;
        //            ImageSourceConverter conve = new ImageSourceConverter();
        //            imageList.SetValue(Image.SourceProperty, conve.ConvertFromString(imageName));

        //            //string picpath = filedlg.FileName.ToString();
        //            //_donation.DonatedImage = System.IO.File.ReadAllBytes(picpath); //Image.FromFile(filedlg.FileName);
        //        }
        //        filedlg = null;
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message.ToString());
        //    }

        //}
    }
}
