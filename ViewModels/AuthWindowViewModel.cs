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
using TaskPulse.UserControls;

namespace TaskPulse.ViewModels
{
    public class AuthWindowViewModel : INotifyPropertyChanged
    {
        private RegistrControl _registrControl = new RegistrControl();
        private AuthControl _authControl = new AuthControl();
        public AuthWindowViewModel()
        {
            AuthCommand = new RelayCommand(ExecuteAuth, CanExecuteAuth);
            RegistrCommand = new RelayCommand(ExecuteRegistr, CanExecuteRegistr);
            CurrentView = _authControl;

        }

        private object _currentView;
        public object CurrentView
        {
            get => _currentView;
            set
            {
                if (_currentView != value)
                {
                    _currentView = value;
                    OnPropertyChanged();
                }
            }
        }       

        // Команда для авторизации
        public ICommand AuthCommand { get; }
        public ICommand RegistrCommand { get; }

        // Логика для авторизации
        private void ExecuteAuth(object parameter)
        {
            // Логика для авторизации
            CurrentView = _authControl;
        }

        // Логика для регистрации
        private void ExecuteRegistr(object parameter)
        {
            // Логика для регистрации
            CurrentView = _registrControl;
        }

        // Условие, когда команда "Авторизоваться" может быть выполнена
        private bool CanExecuteAuth()
        {
            // Для примера логика авторизации всегда доступна
            return true;
        }

        // Условие, когда команда "Зарегистрироваться" может быть выполнена
        private bool CanExecuteRegistr()
        {
            // Для примера регистрация всегда доступна
            return true;
        }

        // Реализуем интерфейс INotifyPropertyChanged для обновления UI
        public event PropertyChangedEventHandler PropertyChanged;

        // Метод для уведомления об изменении свойства
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
