using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskPulse.ViewModels;
using TaskPulse.Views;
using TaskPulse.Models;

namespace TaskPulse.Classes
{
    public static class ViewModelHelper
    {
        public static AuthModel AuthModel { get; set; } = new();
        public static AuthWindowViewModel AuthWindowViewModel { get; } = new();
        public static AuthControlViewModel AuthControlViewModel { get; } = new();
        public static RegistControlViewModel RegistControlViewModel { get; } = new();
        public static MainWindowViewModel MainWindowViewModel { get; } = new();
        public static NavigationService NavigationService { get; } = new();

        static ViewModelHelper()
        {
            AuthModel = new AuthModel();
            AuthWindowViewModel = new();
            AuthControlViewModel = new();
            RegistControlViewModel = new();
            MainWindowViewModel = new();
            NavigationService = new NavigationService();
            // Если вдруг понадобится что-то сложное при старте
            Debug.Print("ViewModelHelper инициализирован");
        }




    }
}
