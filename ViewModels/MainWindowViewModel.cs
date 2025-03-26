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
                    OnPropertyChanged();
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
            CurrentView = ViewModelHelper.DashBoardControl;
            ActiveButton = ButtonDashboard;
        }

        // Условие вывова Dashboard
        private bool CanExecuteDashBoardLoad()
        {
            if (CurrentView != ViewModelHelper.DashBoardControl)
                return true;
            return false;
        }

        //Вызов Tasks
        private void ExecuteTasksBoardLoad(object parameter)
        {
            CurrentView = ViewModelHelper.TasksUserControl;
            ActiveButton = ButtonTasks;
        }

        // Условие вывова Tasks
        private bool CanExecuteTasksBoardLoad()
        {
            if (CurrentView != ViewModelHelper.TasksUserControl)
                return true;
            return false;
        }

        //Вызов Projects
        private void ExecuteProjectsLoad(object parameter)
        {
            CurrentView = ViewModelHelper.ProjectsUserControl;
            ActiveButton = ButtonProjects;
        }

        // Условие вывова Projects
        private bool CanExecuteProjectsLoad()
        {
            if (CurrentView != ViewModelHelper.ProjectsUserControl)
                return true;
            return false;
        }

        //Вызов Account
        private void ExecuteAccountLoad(object parameter)
        {
            CurrentView = ViewModelHelper.AccountUserControl;
            ActiveButton = ButtonAccount;
        }

        // Условие вывова Dashboard
        private bool CanExecuteAccountLoad()
        {
            if (CurrentView != ViewModelHelper.AccountUserControl)
                return true;
            return false;
        }




        private void ExecuteLogout(object parameter)
        {
            ViewModelHelper.NavigationService.NavigateToWindow("AuthWindow");
        }

        // Условие, когда команда "Авторизоваться" может быть выполнена
        private bool CanExecuteLogout()
        {
            // Для примера логика авторизации всегда доступна
            return true;
        }
    }
}
