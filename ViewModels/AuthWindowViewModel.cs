using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace TaskPulse.ViewModels
{
    public class AuthWindowViewModel : INotifyPropertyChanged
    {
        public AuthWindowViewModel()
        {
            AuthCommand = new RelayCommand(ExecuteAuth, CanExecuteAuth);
            RegistrCommand = new RelayCommand(ExecuteRegistr, CanExecuteRegistr);

            // Изначально показываем AuthControl
            IsAuthVisible = true;
            IsRegistrVisible = false;
        }
        private bool _isAuthVisible;
        private bool _isRegistrVisible;

        // Свойство для видимости AuthControl
        public bool IsAuthVisible
        {
            get => _isAuthVisible;
            set
            {
                if (_isAuthVisible != value)
                {
                    _isAuthVisible = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(AuthVisibility)); // Уведомляем об изменении свойства
                }
            }
        }

        // Свойство для видимости RegistrControl
        public bool IsRegistrVisible
        {
            get => _isRegistrVisible;
            set
            {
                if (_isRegistrVisible != value)
                {
                    _isRegistrVisible = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(RegistrVisibility)); // Уведомляем об изменении свойства
                }
            }
        }

        // Свойство для конвертации bool в Visibility
        public Visibility AuthVisibility => IsAuthVisible ? Visibility.Visible : Visibility.Collapsed;
        public Visibility RegistrVisibility => IsRegistrVisible ? Visibility.Visible : Visibility.Collapsed;

        // Команда для авторизации
        public ICommand AuthCommand { get; }
        public ICommand RegistrCommand { get; }

        // Логика для авторизации
        private void ExecuteAuth(object parameter)
        {
            // Логика для авторизации
            IsAuthVisible = true;
            IsRegistrVisible = false; // Переход к форме авторизации
        }

        // Логика для регистрации
        private void ExecuteRegistr(object parameter)
        {
            // Логика для регистрации
            IsAuthVisible = false;
            IsRegistrVisible = true; // Переход к форме регистрации
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

        // Реализация RelayCommand
        public class RelayCommand : ICommand
        {
            private readonly Action<object> _execute;
            private readonly Func<bool> _canExecute;

            public RelayCommand(Action<object> execute, Func<bool> canExecute)
            {
                _execute = execute;
                _canExecute = canExecute;
            }

            public bool CanExecute(object parameter)
            {
                return _canExecute();
            }

            public void Execute(object parameter)
            {
                _execute(parameter);
            }

            public event EventHandler CanExecuteChanged;
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
