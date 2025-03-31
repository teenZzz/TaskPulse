using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TaskPulse.Classes;

namespace TaskPulse.ViewModels
{
    public class CreateProjectWindowViewModel : ViewModelBase
    {
        public CreateProjectWindowViewModel() 
        {

        }

        public string ProjectName
        {
            get => ViewModelHelper.CreateProjectModel.ProjectName;
            set
            {
                ViewModelHelper.CreateProjectModel.ProjectName = value;
                OnPropertyChanged();
            }
        }

        public ICommand newProjectCommand { get; }

    }
}
