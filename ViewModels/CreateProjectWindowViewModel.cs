using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TaskPulse.Classes;
using TaskPulse.ConstList;
using TaskPulse.Models;

namespace TaskPulse.ViewModels
{
    public class CreateProjectWindowViewModel : ViewModelBase
    {
        private CreateProjectModel _createProjectModel;
        public CreateProjectWindowViewModel() 
        {
            _createProjectModel = new CreateProjectModel();
            CloseCommand = new RelayCommand(ExecuteClose, () => true);
            newProjectCommand = new RelayCommand(ExecuteNewProject, () => true);
        }

        public string ProjectName
        {
            get => _createProjectModel.ProjectName;
            set
            {
                _createProjectModel.ProjectName = value;
                OnPropertyChanged();
            }
        }

        public ICommand newProjectCommand { get; }
        public ICommand CloseCommand { get; }

        private void ExecuteClose(object parameter)
        {
            var navService = App.NavigationService;
            navService.CloseModalWindow();
            ProjectName = "";
        }

        private void ExecuteNewProject(object parameter)
        {
            if (string.IsNullOrEmpty(ProjectName) )
            {
                MessageBox.Show(Errors.FIELD_NOT_FILED, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            DataBaseHelper.AddProject(Properties.Settings.Default.UserId, ProjectName);
            MessageBox.Show(Errors.PROJECT_BEEN_CREATED);
            Classes.EventManager.RaiseProjectCreated();

        }

    }
}
