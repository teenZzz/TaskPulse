using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TaskPulse.ButtonManager;
using TaskPulse.Classes;

namespace TaskPulse.ViewModels
{
    public class DashBoardViewModel : ViewModelBase
    {
        public ObservableCollection<string> TaskStatuses { get; set; }

        private ObservableCollection<string> _projects;
        public ObservableCollection<string> Projects
        {
            get => _projects;
            set => SetProperty(ref _projects, value);
        }

        public DashBoardViewModel()
        {
            Classes.EventManager.ProjectCreated += OnProjectCreated; // хз думаю это можно убрать
            LoadProjects();
            TaskStatuses = new ObservableCollection<string>(DataBaseHelper.GetAllTaskStatuses());
            CreateProject = new RelayCommand(ExecuteCreateProject, CanExecuteCreateProject);
            CreateTask = new RelayCommand(ExecuteCreateTask,CanExecuteCreateTask);        
        }
        private void LoadProjects()
        {
            int userId = DataBaseHelper.GetSavedUserSession() ?? 0;
            if (userId > 0)
            {
                Projects = new ObservableCollection<string>(DataBaseHelper.GetUserProjects(userId));
            }
            else
            {
                Projects = new ObservableCollection<string>();
            }
        }

        private void OnProjectCreated()
        { 
            LoadProjects();
            Application.Current.Dispatcher.InvokeAsync(() =>
            {
                Classes.EventManager.ProjectCreated -= OnProjectCreated; // здесь отписываюсь после обновления UI
            });
        }

        public ICommand CreateProject { get; }
        public ICommand CreateTask { get; }

        //Создание проекта
        private void ExecuteCreateProject(object parameter)
        {
            Classes.EventManager.ProjectCreated += OnProjectCreated; // здесь подписываюсь 
            var navService = App.NavigationService;
            navService.OpenModalWindow("CreateProjectWindow");
        }
        private bool CanExecuteCreateProject()
        {
            return true;
        }

        //Создание задачи
        private void ExecuteCreateTask(object parameter)
        {
            var navService = App.NavigationService;
            navService.OpenModalWindow("CreateTaskWindow");
        }
        private bool CanExecuteCreateTask()
        {
            return true;
        }
    }
}
