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
        public static AuthModel AuthModel { get; set; } = new();

        public static AuthControlViewModel AuthControlViewModel { get; } = new();
        public static RegistControlViewModel RegistControlViewModel { get; } = new();

        public static AuthWindowViewModel AuthWindowViewModel { get; } = new();
        public static MainWindowViewModel MainWindowViewModel { get; } = new();

        public static DashBoardViewModel DashBoardViewModel { get; } = new();
        public static TasksViewModel TasksViewModel { get; } = new();
        public static ProjectsViewModel ProjectsViewModel { get; } = new();
        public static AccountViewModel AccountViewModel { get; } = new();

        public static NavigationService NavigationService { get; } = new();

        //создание контролов
        public static RegistrControl RegistrControl = new () { DataContext = RegistControlViewModel };
        public static AuthControl AuthControl = new () { DataContext = AuthControlViewModel };

        public static DashBoardControl DashBoardControl = new DashBoardControl() { DataContext = DashBoardViewModel };
        public static TasksUserControl TasksUserControl;
        public static ProjectsUserControl ProjectsUserControl;
        public static AccountUserControl AccountUserControl;

        static ViewModelHelper()
        {
            AuthModel = new AuthModel();

            AuthWindowViewModel = new();
            MainWindowViewModel = new();

            AuthControlViewModel = new();
            RegistControlViewModel = new();         
            DashBoardViewModel = new();
            TasksViewModel = new();
            ProjectsViewModel = new();
            AccountViewModel = new();

            NavigationService = new();

            RegistrControl = new RegistrControl() { DataContext = RegistControlViewModel };
            AuthControl = new AuthControl() { DataContext = AuthControlViewModel };

            DashBoardControl = new DashBoardControl() { DataContext = DashBoardViewModel };
            TasksUserControl = new TasksUserControl() { DataContext = TasksViewModel };
            ProjectsUserControl = new ProjectsUserControl() { DataContext = ProjectsViewModel };
            AccountUserControl = new AccountUserControl() { DataContext = AccountViewModel };

            // Если вдруг понадобится что-то сложное при старте
            Debug.Print("ViewModelHelper инициализирован");
        }




    }
}
