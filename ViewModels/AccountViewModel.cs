using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskPulse.ViewModels
{
    public class AccountViewModel : ViewModelBase
    {
        public AccountViewModel()
        {
            
        }
        private Uri _settingsIcon = new Uri("pack://application:,,,/Resources/settings2x.png");
        public Uri SettingsIcon
        {
            get => _settingsIcon;
            set
            {
                _settingsIcon = value;
                OnPropertyChanged();
            }
        }

        private Uri _telegramIcon = new Uri("pack://application:,,,/Resources/telegram2.png");
        public Uri TelegramIcon
        {
            get => _telegramIcon;
            set
            {
                _telegramIcon = value;
                OnPropertyChanged();
            }
        }

        private Uri _linkIcon = new Uri("pack://application:,,,/Resources/link.png");
        public Uri LinkIcon
        {
            get => _linkIcon;
            set
            {
                _linkIcon = value;
                OnPropertyChanged();
            }
        }
    }
}
