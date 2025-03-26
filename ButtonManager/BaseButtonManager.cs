using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using TaskPulse.ViewModels;

namespace TaskPulse.ButtonManager
{
    public class BaseButtonManager : ViewModelBase
    {
        public BaseButtonManager() 
        { 

        }

        private Style _currentStyle;
        public Style CurrentStyle
        {
            get => _currentStyle;
            set
            {
                _currentStyle = value;
                OnPropertyChanged();
            }
        }

        private Uri buttonIcon = new Uri("pack://application:,,,/Resources/dashboard.png");
        public Uri ButtonIcon
        {
            get => buttonIcon;
            set
            {
                buttonIcon = value;
                OnPropertyChanged();
            }
        }

        private Brush _foregroundColor;
        public Brush ForegroundColor
        {
            get => _foregroundColor;
            set
            {
                _foregroundColor = value;
                OnPropertyChanged(nameof(ForegroundColor));
            }
        }


    }
}
