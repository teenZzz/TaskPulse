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
            BackCommand = new RelayCommand(ExecuteBack, CanExecuteBack);

        }

        private Uri dashboardButtonIcon = new Uri("pack://application:,,,/Resources/dashboard.png");
        public Uri DashBoardButtonIcon
        {
            get => dashboardButtonIcon;
        }
        private Uri tasksButtonIcon = new Uri("pack://application:,,,/Resources/tasks.png");
        public Uri TasksButtonIcon
        {
            get => tasksButtonIcon;
        }
        private Uri projectsButtonIcon = new Uri("pack://application:,,,/Resources/projects.png");
        public Uri ProjectsButtonIcon
        {
            get => projectsButtonIcon;
        }
        private Uri accountButtonIcon = new Uri("pack://application:,,,/Resources/account.png");
        public Uri AccountButtonIcon
        {
            get => accountButtonIcon;
        }


        public ICommand BackCommand { get; }
        private void ExecuteBack(object parameter)
        {
            // Логика для авторизации
            ViewModelHelper.NavigationService.NavigateToWindow("AuthWindow");
        }

        // Условие, когда команда "Авторизоваться" может быть выполнена
        private bool CanExecuteBack()
        {
            // Для примера логика авторизации всегда доступна
            return true;
        }
    }
}
