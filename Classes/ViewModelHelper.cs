using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskPulse.ViewModels;
using TaskPulse.Views;

namespace TaskPulse.Classes
{
    public static class ViewModelHelper
    {
        public static AuthWindowViewModel AuthWindowViewModel { get; set; }
        public static AuthControlViewModel AuthControlViewModel { get; set; }
        public static RegistControlViewModel RegistControlViewModel { get; set; }

        static ViewModelHelper()
        {
            AuthWindowViewModel = new AuthWindowViewModel();
            AuthControlViewModel = new AuthControlViewModel();
            RegistControlViewModel = new RegistControlViewModel();
        }
    }
}
