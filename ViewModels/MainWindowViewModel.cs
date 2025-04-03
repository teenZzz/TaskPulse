using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TaskPulse.ButtonManager;
using TaskPulse.Classes;
using TaskPulse.UserControls;

namespace TaskPulse.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public BaseButtonManager ButtonDashboard { get; set; } =new ButtonDashboard();
        public BaseButtonManager ButtonTasks { get; set; } = new ButtonTasks();
        public BaseButtonManager ButtonProjects { get; set; } = new ButtonProjects();
        public BaseButtonManager ButtonAccount { get; set; } = new ButtonAccount();
        public MainWindowViewModel()
        {
            var navService = App.NavigationService;
            // Устанавливаем начальные значения
            CurrentView = navService.GetUserControl("DashBoardControl"); // Показываем DashBoard при старте
            ActiveButton = ButtonDashboard; // Активируем кнопку DashBoard

            DashBoardLoadCommand = new RelayCommand(ExecuteDashBoardLoad, CanExecuteDashBoardLoad);
            TasksLoadCommand = new RelayCommand(ExecuteTasksBoardLoad, CanExecuteTasksBoardLoad);
            ProjectsLoadCommand = new RelayCommand(ExecuteProjectsLoad, CanExecuteProjectsLoad);
            AccountLoadCommand = new RelayCommand(ExecuteAccountLoad,CanExecuteAccountLoad);
            LogoutCommand = new RelayCommand(ExecuteLogout, CanExecuteLogout);           
        }      

        private BaseButtonManager _activeButton;
        public BaseButtonManager ActiveButton
        {
            get => _activeButton;
            set
            {
                if (_activeButton != value)
                {
                    // Деактивируем текущую активную кнопку
                    DeactivateButton(_activeButton);

                    // Активируем новую кнопку
                    _activeButton = value;
                    ActivateButton(_activeButton);
                    
                }
            }
        }

        private Uri logoutButtonIcon = new Uri("pack://application:,,,/Resources/logout.png");
        public Uri LogoutButtonIcon
        {
            get => logoutButtonIcon;
            set
            {
                logoutButtonIcon = value;
                OnPropertyChanged();
            }
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

        public ICommand LogoutCommand { get; }
        public ICommand DashBoardLoadCommand { get; }
        public ICommand TasksLoadCommand { get; }
        public ICommand ProjectsLoadCommand { get; }
        public ICommand AccountLoadCommand { get; }


        private void ActivateButton(BaseButtonManager button)
        {
            if (button != null)
            {
                // Активируем стиль и устанавливаем цвет
                button.CurrentStyle = (Style)Application.Current.Resources["navigationButtonActive"]; // Стиль активной кнопки
                button.ForegroundColor = Brushes.White; // Пример: изменяем цвет на белый

                // Устанавливаем активную иконку с добавлением "_white"
                string activeIconPath = button.ButtonIcon.ToString().Replace(".png", "_white.png");
                button.ButtonIcon = new Uri(activeIconPath); // Иконка для активной кнопки
            }
        }

        private void DeactivateButton(BaseButtonManager button)
        {
            if (button != null)
            {
                // Возвращаем дефолтный стиль и цвет
                button.CurrentStyle = (Style)Application.Current.Resources["navigationButtonDefault"]; // Дефолтный стиль кнопки
                button.ForegroundColor = Brushes.Black; // Дефолтный цвет

                // Устанавливаем дефолтную иконку (без "_white")
                string defaultIconPath = button.ButtonIcon.ToString().Replace("_white.png", ".png");
                button.ButtonIcon = new Uri(defaultIconPath); // Дефолтная иконка
            }
        }
       
        //Вызов Dashboard
        private void ExecuteDashBoardLoad(object parameter)
        {
            var navService = App.NavigationService;
            // Устанавливаем начальные значения
            CurrentView = navService.GetUserControl("DashBoardControl");
            ActiveButton = ButtonDashboard;
        }

        // Условие вывова Dashboard
        private bool CanExecuteDashBoardLoad()
        {
            return true;
        }

        //Вызов Tasks
        private void ExecuteTasksBoardLoad(object parameter)
        {
            var navService = App.NavigationService;
            // Устанавливаем начальные значения
            CurrentView = navService.GetUserControl("TasksUserControl");
            ActiveButton = ButtonTasks;
        }

        // Условие вывова Tasks
        private bool CanExecuteTasksBoardLoad()
        {
            return true;
        }

        //Вызов Projects
        private void ExecuteProjectsLoad(object parameter)
        {
            var navService = App.NavigationService;
            // Устанавливаем начальные значения
            CurrentView = navService.GetUserControl("ProjectsUserControl");
            ActiveButton = ButtonProjects;
        }

        // Условие вывова Projects
        private bool CanExecuteProjectsLoad()
        {
            return true;
        }

        //Вызов Account
        private void ExecuteAccountLoad(object parameter)
        {
            var navService = App.NavigationService;
            // Устанавливаем начальные значения
            CurrentView = navService.GetUserControl("AccountUserControl");
            ActiveButton = ButtonAccount;
        }

        // Условие вывова Dashboard
        private bool CanExecuteAccountLoad()
        {
            return true;
        }




        private void ExecuteLogout(object parameter)
        {
            var navService = App.NavigationService;
            DataBaseHelper.ClearUserSession();
            navService.NavigateToWindow("AuthWindow");
        }

        // Условие, когда команда "Авторизоваться" может быть выполнена
        private bool CanExecuteLogout()
        {
            // Для примера логика авторизации всегда доступна
            return true;
        }
    }
}
