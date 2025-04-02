using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskPulse.ViewModels;
using TaskPulse.Views;
using TaskPulse.Models;
using TaskPulse.UserControls;
using System.Security.Cryptography.X509Certificates;

namespace TaskPulse.Classes
{
    public static class ViewModelHelper
    {
        //Создание моделей
        public static AuthModel AuthModel { get; set; } = new();
        public static CreateProjectModel CreateProjectModel { get; set; } = new();
        public static CreateTaskModel CreateTaskModel { get; set; } = new();

        //Создание VM для контролов авторизации
        public static AuthControlViewModel AuthControlViewModel { get; } = new();
        public static RegistControlViewModel RegistControlViewModel { get; } = new();

        //Создание vm для окон
        public static AuthWindowViewModel AuthWindowViewModel { get; } = new();
        public static MainWindowViewModel MainWindowViewModel { get; } = new();

        // Создание vm для модальных окон
        public static CreateProjectWindowViewModel CreateProjectWindowViewModel { get; } = new();
        public static CreateTaskWindowViewModel CreateTaskWindowViewModel { get; } = new();

        //Создание VM для контролов основного окна
        public static DashBoardViewModel DashBoardViewModel { get; } = new();
        public static TasksViewModel TasksViewModel { get; } = new();
        public static ProjectsViewModel ProjectsViewModel { get; } = new();
        public static AccountViewModel AccountViewModel { get; } = new();

        //Классы
        public static NavigationService NavigationService { get; } = new();

        //создание контролов авторизации
        public static RegistrControl RegistrControl = new () { DataContext = RegistControlViewModel };
        public static AuthControl AuthControl = new () { DataContext = AuthControlViewModel };

        //создание контролов основного окна
        public static DashBoardControl DashBoardControl = new DashBoardControl() { DataContext = DashBoardViewModel };
        public static TasksUserControl TasksUserControl = new TasksUserControl() { DataContext = TasksViewModel };
        public static ProjectsUserControl ProjectsUserControl = new ProjectsUserControl() { DataContext = ProjectsViewModel };
        public static AccountUserControl AccountUserControl = new AccountUserControl() { DataContext = AccountViewModel };

        static ViewModelHelper()
        {
            //Модели
            AuthModel = new ();
            CreateProjectModel = new();

            //Окна
            AuthWindowViewModel = new();
            MainWindowViewModel = new();
            CreateProjectWindowViewModel = new();

            //Контролы VM авторизации
            AuthControlViewModel = new();
            RegistControlViewModel = new(); 
            
            //Контролы VM основного окна
            DashBoardViewModel = new();
            TasksViewModel = new();
            ProjectsViewModel = new();
            AccountViewModel = new();

            //Классы
            NavigationService = new();

            //Контролы авторизации
            RegistrControl = new RegistrControl() { DataContext = RegistControlViewModel };
            AuthControl = new AuthControl() { DataContext = AuthControlViewModel };

            //Контролы основного окна
            DashBoardControl = new DashBoardControl() { DataContext = DashBoardViewModel };
            TasksUserControl = new TasksUserControl() { DataContext = TasksViewModel };
            ProjectsUserControl = new ProjectsUserControl() { DataContext = ProjectsViewModel };
            AccountUserControl = new AccountUserControl() { DataContext = AccountViewModel };

            
            Debug.Print("ViewModelHelper инициализирован");
        }




    }
}
