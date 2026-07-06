using MySql.Data.MySqlClient;
using DotNetEnv;

namespace AgendamentoDentista.Database {
    public class DatabaseConfig {
        private static string GetConnectionString() {
            Env.Load();

            string host = Env.GetString("DB_HOST");
            string port = Env.GetString("DB_PORT");
            string name = Env.GetString("DB_NAME");
            string user = Env.GetString("DB_USERNAME");
            string pass = Env.GetString("DB_PASSWORD");

            return $"Server={host};Port={port};Database={name};Uid={user};Pwd={pass};";
        }

        public static MySqlConnection GetConnection() {
            var connection = new MySqlConnection(GetConnectionString());
            connection.Open();
            return connection;
        }
    }
}