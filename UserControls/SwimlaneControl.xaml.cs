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
    }
}
