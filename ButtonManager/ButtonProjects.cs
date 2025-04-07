using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace TaskPulse.ButtonManager
{
    public class ButtonProjects : BaseButtonManager
    { 
        public ButtonProjects()
        {
            CurrentStyle = (Style)Application.Current.FindResource("navigationButtonDefault");
            ButtonIcon = new Uri("pack://application:,,,/Resources/projects.png");
            ForegroundColor = Brushes.Black;
        }
    }
}
