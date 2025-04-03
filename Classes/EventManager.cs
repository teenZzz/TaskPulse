using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskPulse.Classes
{
    public static class EventManager
    {
        public static event Action ProjectCreated;    // Событие создания проекта

        public static void RaiseProjectCreated()
        {
            ProjectCreated?.Invoke();
        }
    }
}
