using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TaskPulse.Classes;
using TaskPulse.Models;


namespace TaskPulse.ViewModels
{
    public class RegistControlViewModel : ViewModelBase
    {      
        private string _confirmPassword;
        public RegistControlViewModel() 
        {
            RegisterCommand = new RelayCommand(ExecuteRegister, CanExecuteRegister);
        }
        
        public string Username
        {
            get => ViewModelHelper.AuthModel.Username;
            set
            {
                ViewModelHelper.AuthModel.Username = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => ViewModelHelper.AuthModel.Password;
            set
            {
                ViewModelHelper.AuthModel.Password = value;
                OnPropertyChanged();
            }
        }

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                OnPropertyChanged();
            }
        }

        public ICommand RegisterCommand { get; }

        private void ExecuteRegister(object parameter)
        {
            // Логика регистрации
            // Проверяем, что пароли совпадают
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(ConfirmPassword))
            {
                MessageBox.Show("Вы не заполнили все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var usernameRegex = new Regex(@"^[a-zA-Z0-9_]{3,20}$");
            if (!usernameRegex.IsMatch(Username))
            {
                MessageBox.Show("Имя пользователя должно содержать только буквы, цифры и подчеркивания, и иметь длину от 3 до 20 символов.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (Password.Length < 6)
            {
                MessageBox.Show("Пароль должен содержать не менее 6 символов.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (Password != ConfirmPassword)
            {
                MessageBox.Show("Пароли не совпадают!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Проверяем, существует ли уже пользователь с таким именем
            bool exists = DataBaseHelper.UserExists(Username);
            if (exists)
            {
                MessageBox.Show("Пользователь с таким именем уже существует.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Добавляем нового пользователя в базу данных
            try
            {
                DataBaseHelper.AddUser(Username, Password);
                ViewModelHelper.NavigationService.NavigateToWindow("MainWindow");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при регистрации: {ex.Message}");
            }
        }

        private bool CanExecuteRegister()
        {
            return true;
        }

    }
}
