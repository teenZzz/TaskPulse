using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
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
