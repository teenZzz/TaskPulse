using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using TaskPulse.Models;
using TaskPulse.Classes;

namespace TaskPulse.ViewModels
{
    public class AuthControlViewModel : ViewModelBase
    {     
        public AuthControlViewModel() 
        {
            
            AuthCommand = new RelayCommand(ExecuteAuth, CanExecuteAuth);
        }
        public string Username
        {
            get => authModel.Username;
            set
            {
                authModel.Username = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => authModel.Password;
            set
            {
                authModel.Password = value;
                OnPropertyChanged();
            }
        }
        public ICommand AuthCommand { get; }
        private void ExecuteAuth(object parameter)
        {
            // Проверяем, что поля не пустые
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Проверяем, существует ли пользователь с таким именем
            bool userExists = DataBaseHelper.UserExists(Username);
            if (!userExists)
            {
                MessageBox.Show("Пользователь с таким именем не найден.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Получаем хэшированный пароль из базы данных
            string storedHashedPassword = DataBaseHelper.GetHashedPassword(Username);

            // Сравниваем введенный пароль с хэшированным паролем из базы данных
            bool passwordMatch = DataBaseHelper.VerifyPassword(Password, storedHashedPassword);

            if (!passwordMatch)
            {
                MessageBox.Show("Неверный пароль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Если логин успешный
            int userId = DataBaseHelper.GetUserIdFromLogin(Username);
            DataBaseHelper.SaveUserSession(userId);
            var navService = App.NavigationService;
            navService.NavigateToWindow("MainWindow");
        }

        private bool CanExecuteAuth()
        {
            return true;
        }      
    }
}
