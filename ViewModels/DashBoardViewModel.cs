using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TaskPulse.ButtonManager;
using TaskPulse.Classes;

namespace TaskPulse.ViewModels
{
    public class DashBoardViewModel : ViewModelBase
    {
        public ObservableCollection<string> TaskStatuses { get; set; }
        public DashBoardViewModel()
        {
            TaskStatuses = new ObservableCollection<string>(DataBaseHelper.GetAllTaskStatuses());
            CreateProject = new RelayCommand(ExecuteCreateProject, CanExecuteCreateProject);
            CreateTask = new RelayCommand(ExecuteCreateTask,CanExecuteCreateTask);
        }
        public ICommand CreateProject { get; }
        public ICommand CreateTask { get; }

        //Создание проекта
        private void ExecuteCreateProject(object parameter)
        {
            ViewModelHelper.NavigationService.OpenModalWindow("CreateProjectWindow");
        }
        private bool CanExecuteCreateProject()
        {
            return true;
        }

        //Создание задачи
        private void ExecuteCreateTask(object parameter)
        {
            ViewModelHelper.NavigationService.OpenModalWindow("CreateTaskWindow");
        }
        private bool CanExecuteCreateTask()
        {
            return true;
        }
    }
}
