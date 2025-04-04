using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskPulse.ConstList
{
    static class Errors
    {
        public const string

            // Ошибка
            ERROR = "Ошибка",

            // Общие ошибки
            FIELD_NOT_FILED = "Пожалуйста, заполните поле.",


            // Для Проверок
            SUCCESSFULLY_COMPLETED = "Успешно!",
            NOT_SUCCESSFULLY_COMPLETED = "Провально!",

            // Запросы к БД
            THE_PROJECT_EXISTS = "Проект с таким именем уже существует!",
            PROJECT_BEEN_CREATED = "Проект успешно создался!",

            // Добавление новой задачи
            NOT_PROJECT_SELECTED = "Не выбран проект для добавления задачи!",
            THE_TASK_EXISTS = "Задача успешно добавлена!"

            ;

    }
}
