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
    public class AuthWindowViewModel : ViewModelBase
    {
        
        public AuthWindowViewModel()
        {
            var navService = App.NavigationService;
            AuthCommand = new RelayCommand(ExecuteAuth, CanExecuteAuth);
            RegistrCommand = new RelayCommand(ExecuteRegistr, CanExecuteRegistr);
            CurrentView = navService.GetUserControl("AuthControl");

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
            var navService = App.NavigationService;
            // Логика для авторизации
            CurrentView = navService.GetUserControl("AuthControl"); ;
        }

        // Логика для регистрации
        private void ExecuteRegistr(object parameter)
        {
            var navService = App.NavigationService;
            // Логика для регистрации
            CurrentView = navService.GetUserControl("RegistrControl"); ;
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
    }
}
