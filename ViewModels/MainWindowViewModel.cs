using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using TaskPulse.Classes;
using TaskPulse.UserControls;

namespace TaskPulse.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            DashBoardLoadCommand = new RelayCommand(ExecuteDashBoardLoad, CanExecuteDashBoardLoad);
            LogoutCommand = new RelayCommand(ExecuteLogout, CanExecuteLogout);
        }

        private Uri dashboardButtonIcon = new Uri("pack://application:,,,/Resources/dashboard.png");
        public Uri DashBoardButtonIcon
        {
            get => dashboardButtonIcon;
            set
            {
                dashboardButtonIcon = value;
                OnPropertyChanged();
            }
        }

        private Uri tasksButtonIcon = new Uri("pack://application:,,,/Resources/tasks.png");
        public Uri TasksButtonIcon
        {
            get => tasksButtonIcon;
            set
            {
                tasksButtonIcon = value;
                OnPropertyChanged();
            }
        }

        private Uri projectsButtonIcon = new Uri("pack://application:,,,/Resources/projects.png");
        public Uri ProjectsButtonIcon
        {
            get => projectsButtonIcon;
            set
            {
                projectsButtonIcon = value;
                OnPropertyChanged();
            }
        }

        private Uri accountButtonIcon = new Uri("pack://application:,,,/Resources/account.png");
        public Uri AccountButtonIcon
        {
            get => accountButtonIcon;
            set
            {
                accountButtonIcon = value;
                OnPropertyChanged();
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
        private void ExecuteDashBoardLoad(object parameter)
        {
            CurrentView = ViewModelHelper.DashBoardControl;
        }

        // Условие, когда команда "Авторизоваться" может быть выполнена
        private bool CanExecuteDashBoardLoad()
        {
            // Для примера логика авторизации всегда доступна
            return true;
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
