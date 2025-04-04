using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskPulse.Classes
{
    public static class EventHelper
    {
        public static event Action ProjectCreated;    // Событие создания проекта
        private static string _currentProjectName;

        // Сохраняем выбранное имя проекта
        public static void SetProjectName(string name)
        {
            _currentProjectName = name;
        }

        // Получаем его по запросу
        public static string GetProjectName()
        {
            return _currentProjectName;
        }

        public static void RaiseProjectCreated()
        {
            ProjectCreated?.Invoke();
        }
    }
}
