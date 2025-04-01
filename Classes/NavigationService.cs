using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TaskPulse.Interfaces;
using TaskPulse.Views;

namespace TaskPulse.Classes
{
    public class NavigationService : INavigationService
    {
        private readonly Dictionary<string, Window> _windowRegistry = new();
        private Window _currentWindow;

        public NavigationService()
        {
            // Регистрируем окна
            _windowRegistry.Add("AuthWindow", new AuthWindow() { DataContext = ViewModelHelper.AuthWindowViewModel });
            _windowRegistry.Add("MainWindow", new MainWindow() { DataContext = ViewModelHelper.MainWindowViewModel });
            _windowRegistry.Add("CreateProjectWindow", new CreateProjectWindow() { DataContext= ViewModelHelper.CreateProjectWindowViewModel });
        }

        public void NavigateToWindow(string windowName)
        {
            if (_windowRegistry.ContainsKey(windowName))
            {
                try
                {
                    // Получаем нужное окно из пула
                    var window = _windowRegistry[windowName];
                    // Если окно не было загружено, показываем его
                    if (!window.IsVisible)
                    {
                        window.Show(); // Показываем окно
                    }

                    // Закрытие текущего окна, если оно открыто
                    if (_currentWindow != null && _currentWindow.IsLoaded)
                    {
                        _currentWindow.Hide(); // Скрываем текущее окно
                    }

                    _currentWindow = window;

                    
                }
                catch (Exception ex)
                {
                    // Логируем ошибку или выводим сообщение пользователю
                    MessageBox.Show($"Ошибка при переключении окна: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Окно не найдено в реестре!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void OpenModalWindow(string windowName)
        {
            if (_windowRegistry.ContainsKey(windowName))
            {
                try
                {
                    // Получаем нужное окно из пула
                    var window = _windowRegistry[windowName];
                    // Если окно не было загружено, показываем его
                    if (!window.IsLoaded || !window.IsVisible)
                    {
                        window.ShowDialog(); // Показываем окно
                    }
                    if (window.IsLoaded || window.IsVisible)
                    {
                        window.Hide(); // Показываем окно
                    }
                    //_currentWindow = window;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при откритие окна: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void Close()
        {
            // Закрываем текущее окно, если оно открыто
            if (_currentWindow != null && _currentWindow.IsLoaded)
            {
                _currentWindow.Close();
            }
        }
    }
}
