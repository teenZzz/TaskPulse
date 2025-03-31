using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskPulse.Classes;

namespace TaskPulse.ViewModels
{
    public class DashBoardViewModel : ViewModelBase
    {
        public ObservableCollection<string> TaskStatuses { get; set; }
        public DashBoardViewModel()
        {
            TaskStatuses = new ObservableCollection<string>(DataBaseHelper.GetAllTaskStatuses());
        }
        
    }
}
