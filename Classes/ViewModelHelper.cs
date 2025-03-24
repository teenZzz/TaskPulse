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

namespace TaskPulse.Classes
{
    public static class ViewModelHelper
    {
        public static AuthModel AuthModel { get; set; } = new();
        public static AuthWindowViewModel AuthWindowViewModel { get; } = new();
        public static AuthControlViewModel AuthControlViewModel { get; } = new();
        public static RegistControlViewModel RegistControlViewModel { get; } = new();
        public static MainWindowViewModel MainWindowViewModel { get; } = new();
        public static DashBoardViewModel DashBoardViewModel { get; } = new();
        public static NavigationService NavigationService { get; } = new();

        //создание контролов
        public static RegistrControl RegistrControl = new () { DataContext = RegistControlViewModel };
        public static AuthControl AuthControl = new () { DataContext = AuthControlViewModel };
        public static DashBoardControl DashBoardControl;

        static ViewModelHelper()
        {
            AuthModel = new AuthModel();
            AuthWindowViewModel = new();
            AuthControlViewModel = new();
            RegistControlViewModel = new();
            MainWindowViewModel = new();
            NavigationService = new ();
            RegistrControl = new RegistrControl() {DataContext = RegistControlViewModel };
            AuthControl = new AuthControl() { DataContext = AuthControlViewModel};
            DashBoardControl = new DashBoardControl() { DataContext = DashBoardViewModel};
            DashBoardViewModel = new();

            // Если вдруг понадобится что-то сложное при старте
            Debug.Print("ViewModelHelper инициализирован");
        }




    }
}
