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
using TaskPulse.Models;

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

        // Новые коллекции для задач по статусам
        private ObservableCollection<TaskModel> _notStartedTasks;
        public ObservableCollection<TaskModel> NotStartedTasks
        {
            get => _notStartedTasks;
            set => SetProperty(ref _notStartedTasks, value);
        }

        private ObservableCollection<TaskModel> _plannedTasks;
        public ObservableCollection<TaskModel> PlannedTasks
        {
            get => _plannedTasks;
            set => SetProperty(ref _plannedTasks, value);
        }

        private ObservableCollection<TaskModel> _inProgressTasks;
        public ObservableCollection<TaskModel> InProgressTasks
        {
            get => _inProgressTasks;
            set => SetProperty(ref _inProgressTasks, value);
        }

        private ObservableCollection<TaskModel> _completedTasks;
        public ObservableCollection<TaskModel> CompletedTasks
        {
            get => _completedTasks;
            set => SetProperty(ref _completedTasks, value);
        }

        public DashBoardViewModel()
        {
            EventHelper.TaskUpdate += OnTaskLoaded;
            LoadProjects();
            //LoadTasks();
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
                Classes.EventHelper.ProjectCreated -= OnProjectCreated; // здесь отписываюсь после обновления UI
            });
        }

        public ICommand CreateProject { get; }
        public ICommand CreateTask { get; }

        //Создание проекта
        private void ExecuteCreateProject(object parameter)
        {
            EventHelper.ProjectCreated += OnProjectCreated; // здесь подписываюсь 
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
            EventHelper.TaskUpdate += OnTaskLoaded;
            var navService = App.NavigationService;
            navService.OpenModalWindow("CreateTaskWindow");
        }
        private bool CanExecuteCreateTask()
        {
            return true;
        }

        private void OnTaskLoaded()
        {
            LoadTasks();
            //Application.Current.Dispatcher.InvokeAsync(() =>
            //{
            //    EventHelper.TaskUpdate -= OnTaskLoaded; // здесь отписываюсь после обновления UI
            //});
        }

        public void LoadTasks()
        {
            string projectName = EventHelper.GetProjectName();
            int projectId = DataBaseHelper.GetProjectId(Properties.Settings.Default.UserId, projectName);
            var tasks = DataBaseHelper.GetTasksByProject(projectId); // Получаем все задачи проекта
            NotStartedTasks = new ObservableCollection<TaskModel>(tasks.Where(t => t.StatusId == 1).ToList());
            PlannedTasks = new ObservableCollection<TaskModel>(tasks.Where(t => t.StatusId == 2).ToList());
            InProgressTasks = new ObservableCollection<TaskModel>(tasks.Where(t => t.StatusId == 3).ToList());
            CompletedTasks = new ObservableCollection<TaskModel>(tasks.Where(t => t.StatusId == 4).ToList());
        }
    }
}
