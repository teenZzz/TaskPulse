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
        public static event Action TaskUpdate;        // События для обновления задач
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

        public static void GetTaskUpdate()
        {
            TaskUpdate?.Invoke();
        }
    }
}
