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
using TaskPulse.Classes;
using TaskPulse.ViewModels;

namespace TaskPulse.UserControls
{
    /// <summary>
    /// Логика взаимодействия для RegistrControl.xaml
    /// </summary>
    public partial class RegistrControl : UserControl
    {
        public RegistrControl()
        {
            InitializeComponent();
        }
        private void textLogin_MouseDown(object sender, MouseButtonEventArgs e)
        {
            login.Focus();
        }

        private void login_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(login.Text) && login.Text.Length > 0)
            {
                textLogin.Visibility = Visibility.Collapsed;
            }
            else
            {
                textLogin.Visibility = Visibility.Visible;
            }
        }

        private void textPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            password.Focus();
        }

        private void password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(password.Password) && password.Password.Length > 0)
            {
                textPassword.Visibility = Visibility.Collapsed;
            }
            else
            {
                textPassword.Visibility = Visibility.Visible;
            }

            if (DataContext is RegistControlViewModel vm)
            {
                vm.Password = ((PasswordBox)sender).Password;
            }
        }

        private void textPassword2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            password2.Focus();
        }

        private void password2_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(password2.Password) && password2.Password.Length > 0)
            {
                textPassword2.Visibility = Visibility.Collapsed;
            }
            else
            {
                textPassword2.Visibility = Visibility.Visible;
            }

            if (DataContext is RegistControlViewModel vm)
            {
                vm.ConfirmPassword = ((PasswordBox)sender).Password;
            }
        }


    }
}
