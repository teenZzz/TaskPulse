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
            if (string.IsNullOrEmpty(TaskName))
            {
                MessageBox.Show(Errors.FIELD_NOT_FILED, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            MessageBox.Show(Errors.SUCCESSFULLY_COMPLETED);
        }
    }
}
