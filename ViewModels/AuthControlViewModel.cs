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
    public class AuthControlViewModel : INotifyPropertyChanged
    {
        private AuthModel _authModel = new AuthModel();
        public AuthControlViewModel() 
        {
            AuthCommand = new RelayCommand(ExecuteAuth, CanExecuteAuth);
        }
        public string Username
        {
            get => _authModel.Username;
            set
            {
                _authModel.Username = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => _authModel.Password;
            set
            {
                _authModel.Password = value;
                OnPropertyChanged();
            }
        }
        public ICommand AuthCommand { get; }
        private void ExecuteAuth(object parameter)
        {
            // Логика регистрации
            MessageBox.Show($"{Password}, {Username}");
        }

        private bool CanExecuteAuth()
        {
            return true;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
