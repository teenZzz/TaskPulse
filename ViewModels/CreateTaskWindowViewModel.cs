using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TaskPulse.Classes;
using TaskPulse.ConstList;
using TaskPulse.Models;
using TaskPulse.Views;

namespace TaskPulse.ViewModels
{
    public class CreateTaskWindowViewModel : ViewModelBase
    {
        private CreateTaskModel _createTaskModel;
        private ObservableCollection<string> _taskStatuses;
        public ObservableCollection<string> TaskStatuses
        {
            get => _taskStatuses;
            set => SetProperty(ref _taskStatuses, value);
        }
        public CreateTaskWindowViewModel() 
        {
            _createTaskModel = new CreateTaskModel();
            LoadTaskStatuses();
            CloseCommand = new RelayCommand(ExecuteClose, () => true);
            newTaskCommand = new RelayCommand(ExecuteNewTask, () => true);
        }
        private void LoadTaskStatuses()
        {
            int userId = DataBaseHelper.GetSavedUserSession() ?? 0;
            if (userId > 0)
            {
                TaskStatuses = new ObservableCollection<string>(DataBaseHelper.GetAllTaskStatuses());
            }
            else
            {
                TaskStatuses = new ObservableCollection<string>();
            }
        }
        
        public string TaskName
        {
            get => _createTaskModel.TaskName;
            set
            {
                _createTaskModel.TaskName = value;
                OnPropertyChanged();
            }
        }

        public string TaskDescription
        {
            get => _createTaskModel.TaskDescription;
            set
            {
                _createTaskModel.TaskDescription = value;
                OnPropertyChanged();
            }
        }

        public string TaskState
        {
            get => _createTaskModel.TaskState;
            set
            {
                _createTaskModel.TaskState = value;
                OnPropertyChanged();
            }
        }

        private int _selectedTaskIndex;
        public int SelectedTaskIndex
        {
            get => _selectedTaskIndex;
            set
            {
                if (_selectedTaskIndex != value)
                {
                    _selectedTaskIndex = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand newTaskCommand { get; }
        public ICommand CloseCommand { get; }

        private void ExecuteClose(object parameter)
        {
            var navService = App.NavigationService;
            navService.CloseModalWindow();
            TaskName = "";
            TaskDescription = "";
        }

        private void ExecuteNewTask(object parameter)
        {
            string projectName = "";
            if (string.IsNullOrEmpty(TaskName))
            {
                MessageBox.Show(Errors.FIELD_NOT_FILED, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            projectName = EventHelper.GetProjectName();
            if (string.IsNullOrEmpty(projectName))
            {
                MessageBox.Show(Errors.NOT_PROJECT_SELECTED, Errors.ERROR, MessageBoxButton.OK, MessageBoxImage.Error); return;
            }

            try
            {
                int projectId = DataBaseHelper.GetProjectId(Properties.Settings.Default.UserId, projectName);
                DataBaseHelper.AddTask(projectId, SelectedTaskIndex + 1, TaskName, TaskDescription);
                MessageBox.Show(Errors.THE_TASK_EXISTS, "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                var navService = App.NavigationService;
                navService.CloseModalWindow();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex}", Errors.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return ;
            }

        }
    }
}
