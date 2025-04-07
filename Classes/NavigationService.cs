using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TaskPulse.Interfaces;
using TaskPulse.UserControls;
using TaskPulse.ViewModels;
using TaskPulse.Views;

namespace TaskPulse.Classes
{
    public class NavigationService : INavigationService
    {
        private readonly Dictionary<string, Func<Window>> _windowFactories;
        private readonly Dictionary<string, Func<UserControl>> _controlFactories;
        private readonly Dictionary<string, UserControl> _createdControls;
        private Window _currentWindow;
        private Window _currentModalWindow;

        public NavigationService()
        {
            // Инициализация фабрик окон
            _windowFactories = new Dictionary<string, Func<Window>>()
            {
                ["AuthWindow"] = () => new AuthWindow() { DataContext = new AuthWindowViewModel() },
                ["MainWindow"] = () => new MainWindow() { DataContext = new MainWindowViewModel() },
                ["CreateProjectWindow"] = () => new CreateProjectWindow() { DataContext = new CreateProjectWindowViewModel() },
                ["CreateTaskWindow"] = () => new CreateTaskWindow() { DataContext = new CreateTaskWindowViewModel() }
            };

            // Инициализация фабрик контролов
            _controlFactories = new Dictionary<string, Func<UserControl>>()
            {
                ["AuthControl"] = () => new AuthControl() { DataContext = new AuthControlViewModel() },
                ["RegistrControl"] = () => new RegistrControl() { DataContext = new RegistControlViewModel() },
                ["DashBoardControl"] = () => new DashBoardControl() { DataContext = new DashBoardViewModel() },
                ["TasksUserControl"] = () => new TasksUserControl() { DataContext = new TasksViewModel() },
                ["ProjectsUserControl"] = () => new ProjectsUserControl() { DataContext = new ProjectsViewModel() },
                ["AccountUserControl"] = () => new AccountUserControl() { DataContext = new AccountViewModel() }
            };
            _createdControls = new Dictionary<string, UserControl>();
        }

        // Метод для получения UserControl (для CurrentView)
        public UserControl GetUserControl(string controlName)
        {
            // Для этих контролов всегда создаём новый экземпляр
            if (controlName == "AuthControl" || controlName == "RegistrControl")
            {
                if (_controlFactories.TryGetValue(controlName, out var factory))
                {
                    return factory(); // каждый раз создаём заново
                }
                throw new ArgumentException($"Control {controlName} not registered");
            }

            // Остальные кэшируем
            if (_createdControls.ContainsKey(controlName))
            {
                return _createdControls[controlName];
            }

            if (_controlFactories.TryGetValue(controlName, out var cachedFactory))
            {
                var control = cachedFactory();
                _createdControls[controlName] = control;
                return control;
            }

            throw new ArgumentException($"Control {controlName} not registered");
        }

        public void NavigateToWindow(string windowName)
        {
            try
            {
                if (!_windowFactories.TryGetValue(windowName, out var factory))
                {
                    throw new ArgumentException($"Window '{windowName}' is not registered");
                }

                var newWindow = factory();
                newWindow.Closed += (s, e) => _currentWindow = null; // Очищаем ссылку при закрытии

                _currentWindow?.Close();
                _currentWindow = newWindow;
                _currentWindow.Show();

            }
            catch (Exception ex)
            {
                // Логирование ошибки
                Debug.WriteLine($"Navigation failed: {ex.Message}");
                throw; // или показать MessageBox
            }
        }

        public void OpenModalWindow(string windowName)
        {
            try
            {
                if (!_windowFactories.TryGetValue(windowName, out var factory))
                {
                    MessageBox.Show($"Окно '{windowName}' не найдено", "Ошибка",
                                  MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Создаем новое окно
                _currentModalWindow = factory();
                _currentModalWindow.Owner = _currentWindow;
                _currentModalWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;

                // Очищаем DataContext при закрытии
                _currentModalWindow.Closed += (s, e) =>
                {
                    if (_currentModalWindow.DataContext is IDisposable disposableVm)
                    {
                        disposableVm.Dispose();
                    }
                    _currentModalWindow.DataContext = null;
                    _currentModalWindow = null;
                };

                _currentModalWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка открытия: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void CloseModalWindow()
        {
            try
            {
                _currentModalWindow?.Close();
                _currentModalWindow = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка закрытия: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
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
