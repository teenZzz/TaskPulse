using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace TaskPulse.Classes
{
    public class DataBaseHelper
    {
        private const string ConnectionString = "Data Source=TaskPulse.db;Version=3;";

        // Метод для подключения к базе данных
        private static SQLiteConnection GetConnection()
        {
            var connection = new SQLiteConnection(ConnectionString);
            connection.Open();
            return connection;
        }

        // Метод для инициализации базы данных (создание таблицы Users)
        public static void InitializeDatabase()
        {
            using (var connection = GetConnection())
            {
                var createTableQuery = @"
                CREATE TABLE IF NOT EXISTS Users (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Username TEXT NOT NULL UNIQUE,
                    Password TEXT NOT NULL
                );";

                using (var command = new SQLiteCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        // Метод для проверки существования пользователя
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

        // Метод для добавления пользователя в БД с хэшированием пароля
        public static void AddUser(string username, string password)
        {
            // Хэшируем пароль
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

        // Метод для хэширования пароля с использованием SHA-256
        private static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

        // Метод для получения хэшированного пароля (для проверки при логине)
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

        // Метод для сравнения пароля с хэшированным значением из БД
        public static bool VerifyPassword(string enteredPassword, string storedHashedPassword)
        {
            string hashedEnteredPassword = HashPassword(enteredPassword);
            return hashedEnteredPassword == storedHashedPassword;
        }

        // Метод для проверки подключения
        public static bool TestConnection()
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка подключения: {ex.Message}");
                return false;
            }
        }
    }
}
