using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskPulse.ViewModels
{
    public class ProjectsViewModel : ViewModelBase
    {
        public ProjectsViewModel()
        {
            
        }

        private Uri _filesIcon = new Uri("pack://application:,,,/Resources/files2x.png");
        public Uri FilesIcon
        {
            get => _filesIcon;
            set
            {
                _filesIcon = value;
                OnPropertyChanged();
            }
        }
    }
}
