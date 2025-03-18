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

namespace TaskPulse.UserControls
{
    /// <summary>
    /// Логика взаимодействия для AuthControl.xaml
    /// </summary>
    public partial class AuthControl : UserControl
    {
        public AuthControl()
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
        }

    }
}
