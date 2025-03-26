using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace TaskPulse.ButtonManager
{
    public class ButtonDashboard : BaseButtonManager
    {
        public ButtonDashboard()
        {
            CurrentStyle = (Style)Application.Current.FindResource("navigationButtonDefault");
            ButtonIcon = new Uri("pack://application:,,,/Resources/dashboard.png");
            ForegroundColor = Brushes.Black;
        }
    }
}
