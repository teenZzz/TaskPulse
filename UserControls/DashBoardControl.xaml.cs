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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TaskPulse.Classes;

namespace TaskPulse.UserControls
{
    /// <summary>
    /// Логика взаимодействия для DashBoardControl.xaml
    /// </summary>
    public partial class DashBoardControl : UserControl
    {
        public DashBoardControl()
        {
            InitializeComponent();
        }

        private void ProjectComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (projectBox.SelectedItem is string selectedProject)
            {
                EventHelper.SetProjectName(selectedProject); // сохраняем выбранное имя
            }
        }
    }
}
