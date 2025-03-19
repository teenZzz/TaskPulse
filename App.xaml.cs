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
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Привязываем все вью и вью-модели через статический класс
            var authWindow = new AuthWindow();
            authWindow.DataContext = ViewModelHelper.AuthWindowViewModel;
            DataBaseHelper.InitializeDatabase();
        }
    }

}
