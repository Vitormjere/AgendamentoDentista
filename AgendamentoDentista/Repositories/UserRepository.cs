using AgendamentoDentista.Database;
using AgendamentoDentista.Models;
using MySql.Data.MySqlClient;

namespace AgendamentoDentista.Repositories {
    public class UserRepository {
        public void Criar(User user) {
            using var conn = DatabaseConfig.GetConnection();
            var cmd = new MySqlCommand(
                "INSERT INTO Users (nome, email, senha_hash) VALUES (@nome, @email, @senha)", conn);
            cmd.Parameters.AddWithValue("@nome", user.Nome);
            cmd.Parameters.AddWithValue("@email", user.Email);
            cmd.Parameters.AddWithValue("@senha", user.SenhaHash);
            cmd.ExecuteNonQuery();
        }

        public User? BuscarPorEmail(string email) {
            using var conn = DatabaseConfig.GetConnection();
            var cmd = new MySqlCommand("SELECT * FROM Users WHERE email = @email", conn);
            cmd.Parameters.AddWithValue("@email", email);

            using var reader = cmd.ExecuteReader();
            if (reader.Read()) {
                return new User {
                    Id = reader.GetInt32("id"),
                    Nome = reader.GetString("nome"),
                    Email = reader.GetString("email"),
                    SenhaHash = reader.GetString("senha_hash"),
                    CriadoEm = reader.GetDateTime("criado_em")
                };
            }
            return null;
        }
    }
}