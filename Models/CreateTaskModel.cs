using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskPulse.Models
{
    public class CreateTaskModel
    {
        public string TaskName { get; set; } = "";
        public string TaskDescription { get; set; } = string.Empty;
        public string TaskState { get; set; } = "Запланировано";
    }
}
