using System.Configuration;
using System.Data;
using System.Windows;
using TaskPulse.Classes;
using TaskPulse.Interfaces;
using TaskPulse.Views;

namespace TaskPulse
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            
        }
        public static NavigationService NavigationService { get; } = new NavigationService();
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            DataBaseHelper.InitializeDatabase();

            int? savedUserId = DataBaseHelper.GetSavedUserSession();
            if (savedUserId.HasValue)
            {
                NavigationService.NavigateToWindow("MainWindow");
            }
            else
            {
                NavigationService.NavigateToWindow("AuthWindow");
            }

            
        }
    }

}
