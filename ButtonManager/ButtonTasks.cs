using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace TaskPulse.ButtonManager
{
    public class ButtonTasks : BaseButtonManager
    {
        public ButtonTasks()
        {
            CurrentStyle = (Style)Application.Current.FindResource("navigationButtonDefault");
            ButtonIcon = new Uri("pack://application:,,,/Resources/tasks.png");
            ForegroundColor = Brushes.Black;
        }
    }
}
