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

namespace TaskPulse.ViewModels
{
    public class CreateTaskWindowViewModel : ViewModelBase
    {
        private ObservableCollection<string> _taskStatuses;
        public ObservableCollection<string> TaskStatuses
        {
            get => _taskStatuses;
            set => SetProperty(ref _taskStatuses, value);
        }
        public CreateTaskWindowViewModel() 
        {
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
            get => ViewModelHelper.CreateTaskModel.TaskName;
            set
            {
                ViewModelHelper.CreateTaskModel.TaskName = value;
                OnPropertyChanged();
            }
        }

        public string TaskDescription
        {
            get => ViewModelHelper.CreateTaskModel.TaskDescription;
            set
            {
                ViewModelHelper.CreateTaskModel.TaskDescription = value;
                OnPropertyChanged();
            }
        }

        public string TaskState
        {
            get => ViewModelHelper.CreateTaskModel.TaskState;
            set
            {
                ViewModelHelper.CreateTaskModel.TaskState = value;
                OnPropertyChanged();
            }
        }

        public ICommand newTaskCommand { get; }
        public ICommand CloseCommand { get; }

        private void ExecuteClose(object parameter)
        {
            ViewModelHelper.NavigationService.CloseModalWindow("CreateTaskWindow");
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
