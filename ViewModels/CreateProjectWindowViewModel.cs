using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TaskPulse.Classes;
using TaskPulse.ConstList;

namespace TaskPulse.ViewModels
{
    public class CreateProjectWindowViewModel : ViewModelBase
    {
        public CreateProjectWindowViewModel() 
        {
            CloseCommand = new RelayCommand(ExecuteClose, () => true);
            newProjectCommand = new RelayCommand(ExecuteNewProject, () => true);
        }

        public string ProjectName
        {
            get => ViewModelHelper.CreateProjectModel.ProjectName;
            set
            {
                ViewModelHelper.CreateProjectModel.ProjectName = value;
                OnPropertyChanged();
            }
        }

        public ICommand newProjectCommand { get; }
        public ICommand CloseCommand { get; }

        private void ExecuteClose(object parameter)
        {
            ViewModelHelper.NavigationService.CloseModalWindow("CreateProjectWindow");
        }

        private void ExecuteNewProject(object parameter)
        {
            if (string.IsNullOrEmpty(ProjectName) )
            {
                MessageBox.Show(Errors.FIELD_NOT_FILED, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBox.Show(Errors.SUCCESSFULLY_COMPLETED);
        }

    }
}
