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

namespace TaskPulse.Views
{
    /// <summary>
    /// Логика взаимодействия для CreateProjectWindow.xaml
    /// </summary>
    public partial class CreateProjectWindow : Window
    {
        public CreateProjectWindow()
        {
            InitializeComponent();
        }

        private void textProject_MouseDown(object sender, MouseButtonEventArgs e)
        {
            project.Focus();
        }

        private void project_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(project.Text) && project.Text.Length > 0)
            {
                textProject.Visibility = Visibility.Collapsed;
            }
            else
            {
                textProject.Visibility = Visibility.Visible;
            }
        }
    }
}
