using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace TaskPulse.ButtonManager
{
    public class ButtonAccount : BaseButtonManager
    {
        public ButtonAccount()
        {
            CurrentStyle = (Style)Application.Current.FindResource("navigationButtonDefault");
            ButtonIcon = new Uri("pack://application:,,,/Resources/account.png");
            ForegroundColor = Brushes.Black;
        }
    }
}
