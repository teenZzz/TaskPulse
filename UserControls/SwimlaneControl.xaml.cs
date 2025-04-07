using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TaskPulse.Classes;
using TaskPulse.Models;

namespace TaskPulse.UserControls
{
    /// <summary>
    /// Логика взаимодействия для SwimlaneControl.xaml
    /// </summary>
    public partial class SwimlaneControl : UserControl
    {
        public SwimlaneControl()
        {
            InitializeComponent();
        }
        // 1. Создаем зависимое свойство для TextBlock
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(SwimlaneControl), new PropertyMetadata(string.Empty));
        // 2. Свойство для работы с Title
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // 3. Создаем зависимое свойство для ItemsSource (коллекция задач)
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(ObservableCollection<TaskModel>), typeof(SwimlaneControl), new PropertyMetadata(null));

        // 4. Свойство для работы с ItemsSource
        public ObservableCollection<TaskModel> ItemsSource
        {
            get { return (ObservableCollection<TaskModel>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        // Начало перетаскивания задачи
        private void StackPanel_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var stackPanel = sender as StackPanel;
            var task = stackPanel.DataContext as TaskModel;

            if (task != null)
            {
                // Начинаем перетаскивание с данными TaskModel
                DragDrop.DoDragDrop(stackPanel, task, DragDropEffects.Move);
            }
        }

        // Обработка перетаскивания на область
        #region старое но лучше оставить
        //private void StackPanel_DragOver(object sender, DragEventArgs e)
        //{
        //    if (!e.Data.GetDataPresent(typeof(TaskModel)))
        //    {
        //        e.Effects = DragDropEffects.None;  // Не разрешаем перетаскивание, если это не задача
        //    }
        //    else
        //    {
        //        e.Effects = DragDropEffects.Move;  // Разрешаем перетаскивание
        //    }
        //}
        #endregion
        private void ListView_DragOver(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(typeof(TaskModel)))
            {
                e.Effects = DragDropEffects.None;  // Не разрешаем перетаскивание, если это не задача
            }
            else
            {
                e.Effects = DragDropEffects.Move;  // Разрешаем перетаскивание
            }
        }



        private void ListView_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(TaskModel)))
            {
                var task = e.Data.GetData(typeof(TaskModel)) as TaskModel;

                // Получаем новый статус задачи из Tag
                var targetListView = sender as ListView;
                var targetStatus = targetListView.Tag as string; // Это будет значение Title

                // Определяем новый статус по имени
                int newStatusId = GetStatusIdByTitle(targetStatus);

                // Получаем коллекцию задач для текущего статуса
                var targetList = targetListView.ItemsSource as ObservableCollection<TaskModel>;

                if (task != null && targetList != null)
                {
                    // Перемещаем задачу в новый статус
                    targetList.Add(task);

                    string taskName = task.Name;

                    // Удаляем задачу из текущего списка (при условии, что мы знаем текущий список)
                    var currentList = (sender as ListView).DataContext as ObservableCollection<TaskModel>;
                    if (currentList != null)
                    {
                        currentList.Remove(task);
                    }
                    string projectName = EventHelper.GetProjectName();
                    int projectId = DataBaseHelper.GetProjectId(Properties.Settings.Default.UserId, projectName);
                    int taskId = DataBaseHelper.GetTaskId(projectId, taskName);

                    // Обновляем статус задачи в базе данных
                    DataBaseHelper.MoveTask(taskId, newStatusId);
                    EventHelper.GetTaskUpdate();
                }
            }
        }

        private int GetStatusIdByTitle(string title)
        {
            // Определяем ID статуса на основе имени
            switch (title)
            {
                case "Нет статуса":
                    return 1;
                case "Запланировано":
                    return 2;
                case "В работе":
                    return 3;
                case "Готово":
                    return 4;
                default:
                    return 0; // Статус по умолчанию
            }
        }



        #region старое но лучше оставить
        // Обработка отпускания задачи (перемещение)
        //private void StackPanel_Drop(object sender, DragEventArgs e)
        //{
        //    if (e.Data.GetDataPresent(typeof(TaskModel)))
        //    {
        //        var task = e.Data.GetData(typeof(TaskModel)) as TaskModel;

        //        // Находим ListView, являющийся родительским элементом для StackPanel
        //        var listView = FindParent<ListView>(sender as UIElement);

        //        if (task != null && listView != null)
        //        {
        //            // Получаем ItemsSource (ObservableCollection<TaskModel>) из ListView
        //            var targetList = listView.ItemsSource as ObservableCollection<TaskModel>;

        //            if (targetList != null)
        //            {
        //                // Перемещаем задачу в новый статус
        //                targetList.Add(task);

        //                // Удаляем задачу из текущего списка (предположим, что у нас есть доступ к текущей коллекции)
        //                var currentList = (sender as StackPanel).DataContext as ObservableCollection<TaskModel>;
        //                if (currentList != null)
        //                {
        //                    currentList.Remove(task);
        //                }
        //            }
        //        }
        //    }
        //}

        //// Метод для поиска родительского элемента типа T
        //private T FindParent<T>(UIElement element) where T : UIElement
        //{
        //    var parent = VisualTreeHelper.GetParent(element);
        //    while (parent != null && !(parent is T))
        //    {
        //        parent = VisualTreeHelper.GetParent(parent);
        //    }
        //    return parent as T;
        //}
        #endregion
    }
}
