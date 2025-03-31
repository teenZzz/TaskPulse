using System.Configuration;
using System.Data;
using System.Windows;
using TaskPulse.Classes;
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
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            int? savedUserId = DataBaseHelper.GetSavedUserSession();
            if (savedUserId.HasValue)
            {
                ViewModelHelper.NavigationService.NavigateToWindow("MainWindow");
            }
            else
            {
                ViewModelHelper.NavigationService.NavigateToWindow("AuthWindow");
            }

            DataBaseHelper.InitializeDatabase();
        }
    }

}
