using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using TaskPulse.ViewModels;

namespace TaskPulse.Views
{
    /// <summary>
    /// Логика взаимодействия для CreateTaskWindow.xaml
    /// </summary>
    public partial class CreateTaskWindow : Window
    {
        public CreateTaskWindow()
        {
            InitializeComponent();
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is CreateTaskWindowViewModel viewModel)
            {
                viewModel.CloseCommand.Execute(null); // Выполнить команду
            }
        }

        private void textTask_MouseDown(object sender, MouseButtonEventArgs e)
        {
            task.Focus();
        }

        private void task_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(task.Text) && task.Text.Length > 0)
            {
                textTask.Visibility = Visibility.Collapsed;
            }
            else
            {
                textTask.Visibility = Visibility.Visible;
            }
        }

        private void textDescription_MouseDown(object sender, MouseButtonEventArgs e)
        {
            description.Focus();
        }

        private void description_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(description.Text) && description.Text.Length > 0)
            {
                textDescription.Visibility = Visibility.Collapsed;
            }
            else
            {
                textDescription.Visibility = Visibility.Visible;
            }
        }
    }
}
