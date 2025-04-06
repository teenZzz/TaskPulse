using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using System.Windows;
using TaskPulse.ConstList;
using TaskPulse.Models;

namespace TaskPulse.Classes
{
    public class DataBaseHelper
    {
        private static readonly string DbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TaskPulse.db");
        private static readonly string ConnectionString = $"Data Source={DbPath};Version=3;";

        private static SQLiteConnection GetConnection()
        {
            var connection = new SQLiteConnection(ConnectionString);
            connection.Open();
            return connection;
        }

        //Инициализация БД
        public static void InitializeDatabase()
        {
            using (var connection = GetConnection())
            {
                var createUsersTable = @"CREATE TABLE IF NOT EXISTS Users (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Username TEXT NOT NULL UNIQUE,
                    Password TEXT NOT NULL
                );";

                var createProjectsTable = @"CREATE TABLE IF NOT EXISTS Projects (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    UserId INTEGER NOT NULL,
                    Name TEXT NOT NULL,
                    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE
                );";

                var createStatusesTable = @"CREATE TABLE IF NOT EXISTS TaskStatuses (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL UNIQUE
                );";

                var createTasksTable = @"CREATE TABLE IF NOT EXISTS Tasks (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    ProjectId INTEGER NOT NULL,
                    StatusId INTEGER NOT NULL,
                    Name TEXT NOT NULL,
                    Description TEXT,
                    FOREIGN KEY (ProjectId) REFERENCES Projects(Id) ON DELETE CASCADE,
                    FOREIGN KEY (StatusId) REFERENCES TaskStatuses(Id)
                );";

                using (var command = new SQLiteCommand(createUsersTable, connection))
                {
                    command.ExecuteNonQuery();
                }
                using (var command = new SQLiteCommand(createProjectsTable, connection))
                {
                    command.ExecuteNonQuery();
                }
                using (var command = new SQLiteCommand(createStatusesTable, connection))
                {
                    command.ExecuteNonQuery();
                }
                using (var command = new SQLiteCommand(createTasksTable, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
            EnsureDefaultStatuses();
        }

        //Добавление стандартных статусов для доски
        private static void EnsureDefaultStatuses()
        {
            using (var connection = GetConnection())
            {
                string[] statuses = { "Нет статуса", "Запланировано", "В работе", "Готово" };
                foreach (var status in statuses)
                {
                    var query = "INSERT OR IGNORE INTO TaskStatuses (Name) VALUES (@Name);";
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", status);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        //Метод вывода всех статусов задач
        public static List<string> GetAllTaskStatuses()
        {
            List<string> statuses = new List<string>();
            using (var connection = GetConnection())
            {
                var query = "SELECT Name FROM TaskStatuses";
                using (var command = new SQLiteCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        statuses.Add(reader.GetString(0));
                    }
                }
            }
            return statuses;
        }

        //Метод добавления проекта
        public static void AddProject(int userId, string projectName)
        {
            using (var connection = GetConnection())
            {
                var query = "INSERT INTO Projects (UserId, Name) VALUES (@UserId, @Name)";
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@Name", projectName);
                    command.ExecuteNonQuery();
                }
            }
        }

        //Метод проверки существования проекта в БД
        public static bool CheckProjectInBD(int userId, string projectName)
        {
            using (var connection = GetConnection())
            {
                var queryCheck = "SELECT COUNT(1) FROM Projects WHERE UserId = @UserId and Name = @Name";
                using (var command = new SQLiteCommand(queryCheck, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@Name", projectName);
                    int result = Convert.ToInt32(command.ExecuteScalar());

                    if (result > 0)
                    {
                        MessageBox.Show(Errors.THE_PROJECT_EXISTS, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
                    else return true;

                }
            }
        }

        //Метод получения id Проекта
        public static int GetProjectId(int userId, string projectName)
        {
            using (var connection = GetConnection())
            {
                var queryCheck = "SELECT Id FROM Projects WHERE UserId = @UserId and Name = @Name";
                using (var command = new SQLiteCommand(queryCheck, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@Name", projectName);
                    int result = Convert.ToInt32(command.ExecuteScalar());
                        
                    return result;

                }
            }
        }


        //Метод добавления задачи
        public static void AddTask(int projectId, int statusId, string name, string description)
        {
            using (var connection = GetConnection())
            {
                var query = "INSERT INTO Tasks (ProjectId, StatusId, Name, Description) VALUES (@ProjectId, @StatusId, @Name, @Description)";
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProjectId", projectId);
                    command.Parameters.AddWithValue("@StatusId", statusId);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Description", description);
                    command.ExecuteNonQuery();
                }
            }
        }

        //Метод перемещения задачи
        public static void MoveTask(int taskId, int newStatusId)
        {
            using (var connection = GetConnection())
            {
                var query = "UPDATE Tasks SET StatusId = @NewStatusId WHERE Id = @TaskId";
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NewStatusId", newStatusId);
                    command.Parameters.AddWithValue("@TaskId", taskId);
                    command.ExecuteNonQuery();
                }
            }
        }

        //Метод проверки существования пользователя
        public static bool UserExists(string username)
        {
            using (var connection = GetConnection())
            {
                var query = "SELECT COUNT(1) FROM Users WHERE Username = @Username";
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    var result = command.ExecuteScalar();
                    return Convert.ToInt32(result) > 0;
                }
            }
        }

        //Метод добавления нового пользователя
        public static void AddUser(string username, string password)
        {
            string hashedPassword = HashPassword(password);
            using (var connection = GetConnection())
            {
                var query = "INSERT INTO Users (Username, Password) VALUES (@Username, @Password)";
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", hashedPassword);
                    command.ExecuteNonQuery();
                }
            }
        }

        //метод для выгрузки всех проектов
        public static List<string> GetUserProjects(int userId)
        {
            List<string> projects = new List<string>();
            using (var connection = GetConnection())
            {
                var query = "SELECT Name FROM Projects WHERE UserId = @UserId";
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            projects.Add(reader.GetString(0));
                        }
                    }
                }
            }
            return projects;
        }

        // Метод для получения задач по id проекта
        public static List<TaskModel> GetTasksByProject(int projectId)
        {
            List<TaskModel> tasks = new List<TaskModel>();
            using (var connection = GetConnection())
            {
                var query = "SELECT Name, Description, StatusId FROM Tasks WHERE ProjectId = @ProjectId";
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProjectId", projectId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tasks.Add(new TaskModel
                            {
                                Name = reader.GetString(0),
                                Description = reader.GetString(1),
                                StatusId = reader.GetInt32(2)
                            });
                        }
                    }
                }
            }
            return tasks;
        }

        //Метод хэширования пароля
        private static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

        //Метод получения пароля
        public static string GetHashedPassword(string username)
        {
            using (var connection = GetConnection())
            {
                var query = "SELECT Password FROM Users WHERE Username = @Username";
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    var result = command.ExecuteScalar();
                    return result?.ToString();
                }
            }
        }

        //Метод для подтверждения пароля
        public static bool VerifyPassword(string enteredPassword, string storedHashedPassword)
        {
            string hashedEnteredPassword = HashPassword(enteredPassword);
            return hashedEnteredPassword == storedHashedPassword;
        }
        public static int GetUserIdFromLogin(string username)
        {
            using (var connection = GetConnection())
            {
                var query = "SELECT Id FROM Users WHERE Username = @Username";
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    var result = command.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : 0; // Возвращает 0, если пользователя не найдено
                }
            }
        }
        public static void SaveUserSession(int userId)
        {
            Properties.Settings.Default.UserId = userId;
            Properties.Settings.Default.Save();
        }

        public static int? GetSavedUserSession()
        {
            int userId = Properties.Settings.Default.UserId;
            return userId > 0 ? userId : (int?)null;
        }

        public static void ClearUserSession()
        {
            Properties.Settings.Default.UserId = 0;
            Properties.Settings.Default.Save();
        }
    }
}

