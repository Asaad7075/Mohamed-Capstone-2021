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

namespace WpfPresentation
{
    /// <summary>
    /// Interaction logic for ResetPassword.xaml
    /// </summary>
    public partial class ResetPassword : Page
    {
        //private IUserManager _userManager = new UserManager(new UserFake());
        //public ResetPassword()
        //{
        //    //InitializeComponent();
        //}

        //private void btnUpdatePassword_Click(object sender, RoutedEventArgs e)
        //{
        //    string email = txtUserEmail.Text;
        //    string newPassword = pwdNewPassword.Password;
        //    string oldPassword = pwdOldPassword.Password;
        //    string retypePassword = pwdConfirmPassword.Password;

        //    try
        //    {
        //        if (_userManager.UpdatePassword(email,
        //            oldPassword, newPassword) == true && newPassword == retypePassword)
        //        {
        //            MessageBox.Show("Password Updated.");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
        //    }
        //}
    }
}
