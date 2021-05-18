using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using DataAccessFakes;
using DomainModels;
using LogicInterfaces;
using LogicLayer;
using WpfPresentation.Validators;

namespace WpfPresentation
{
    /// <summary>
    /// Richard Schroeder
    /// Created: 2021/02/24
    /// 
    /// Displays new window for user to update password on either first login or as needed
    /// </summary>
    public partial class frmResetPassword : Window
    {
        
        private IEmployeeManager _userManager = new EmployeeManager(new EmployeeFake());
        private Employee _user;
        private bool _isNewUser;

        public frmResetPassword(IEmployeeManager userManager, Employee user,
            bool isNewUser = false)
        {
            _userManager = userManager;
            _user = user;
            _isNewUser = isNewUser;

            InitializeComponent();
        }

        public frmResetPassword()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Richard Schroeder
        /// Created: 2021/02/24
        /// 
        /// Logic for validating and updating a users password
        /// </summary>
        private void btnUpdatePassword_Click(object sender, RoutedEventArgs e)
        {
            string email = txtUserEmail.Text;
            string newPassword = pwdNewPassword.Password;
            string oldPassword = pwdOldPassword.Password;
            string retypePassword = pwdConfirmPassword.Password;

            //validation checks, can be appended as needed
            if (retypePassword != newPassword)
            {
                MessageBox.Show("Passwords must match.");
                pwdConfirmPassword.Clear();
                pwdNewPassword.Clear();
                pwdNewPassword.Focus();
                return;
            }

            if (!newPassword.isValidPassword()
                || newPassword == "newuser")
            {
                MessageBox.Show("Invalid Password");
                pwdConfirmPassword.Clear();
                pwdNewPassword.Clear();
                pwdNewPassword.Focus();
                return;
            }

            if (!oldPassword.isValidPassword())
            {
                MessageBox.Show("Incorrect Password");
                pwdOldPassword.Clear();
                pwdConfirmPassword.Clear();
                pwdNewPassword.Clear();
                pwdOldPassword.Focus();
                return;
            }

            try
            {
                if (_userManager.UpdatePassword(email,
                    oldPassword, newPassword) == true && newPassword == retypePassword)
                {
                    MessageBox.Show("Password Updated.");
                    this.DialogResult = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_isNewUser)
            {
                txtBlkHeaderResetPassword.Text = "On First Login " + txtBlkHeaderResetPassword.Text;
                txtUserEmail.Text = _user.Email;
                txtUserEmail.IsEnabled = false;
                pwdOldPassword.Password = "newuser";
                pwdOldPassword.IsEnabled = false;
                pwdNewPassword.Focus();
            }
            else
            {
                txtUserEmail.Text = _user.Email;
                txtUserEmail.IsEnabled = false;
                pwdOldPassword.IsEnabled = true;
                txtUserEmail.Focus();
            }
        }

        private void btnCancelUpdatePassword_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
