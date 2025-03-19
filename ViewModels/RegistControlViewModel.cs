using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TaskPulse.Classes;
using TaskPulse.Models;

namespace TaskPulse.ViewModels
{
    public class RegistControlViewModel : INotifyPropertyChanged
    {
        private AuthModel _authModel = new AuthModel();
        private string _confirmPassword;
        public RegistControlViewModel() 
        {
            RegisterCommand = new RelayCommand(ExecuteRegister, CanExecuteRegister);
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
            MessageBox.Show($"{Password}, {Username}");
        }

        private bool CanExecuteRegister()
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
