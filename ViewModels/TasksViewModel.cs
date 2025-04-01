using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskPulse.ViewModels
{
    public class TasksViewModel : ViewModelBase
    {
        public TasksViewModel()
        {
            
        }

        private Uri _viewerIcon = new Uri("pack://application:,,,/Resources/viewer2x.png");
        public Uri ViewerIcon
        {
            get => _viewerIcon;
            set
            {
                _viewerIcon = value;
                OnPropertyChanged();
            }
        }
    }
}
