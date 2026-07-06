using AgendamentoDentista.Database;
using AgendamentoDentista.Models;
using MySql.Data.MySqlClient;

namespace AgendamentoDentista.Repositories {
    public class ProcedureRepository {
        public void Criar(Procedure procedure) {
            using var conn = DatabaseConfig.GetConnection();
            var cmd = new MySqlCommand(
                "INSERT INTO Procedures (nome, duracao_minutos, preco) VALUES (@nome, @duracao, @preco)", conn);
            cmd.Parameters.AddWithValue("@nome", procedure.Nome);
            cmd.Parameters.AddWithValue("@duracao", procedure.DuracaoMinutos);
            cmd.Parameters.AddWithValue("@preco", procedure.Preco);
            cmd.ExecuteNonQuery();
        }

        public List<Procedure> ListarTodos() {
            using var conn = DatabaseConfig.GetConnection();
            var cmd = new MySqlCommand("SELECT * FROM Procedures ORDER BY nome", conn);

            var procedures = new List<Procedure>();
            using var reader = cmd.ExecuteReader();
            while (reader.Read()) {
                procedures.Add(new Procedure {
                    Id = reader.GetInt32("id"),
                    Nome = reader.GetString("nome"),
                    DuracaoMinutos = reader.GetInt32("duracao_minutos"),
                    Preco = reader.GetDecimal("preco")
                });
            }
            return procedures;
        }

        public Procedure? BuscarPorId(int id) {
            using var conn = DatabaseConfig.GetConnection();
            var cmd = new MySqlCommand("SELECT * FROM Procedures WHERE id = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);

            using var reader = cmd.ExecuteReader();
            if (reader.Read()) {
                return new Procedure {
                    Id = reader.GetInt32("id"),
                    Nome = reader.GetString("nome"),
                    DuracaoMinutos = reader.GetInt32("duracao_minutos"),
                    Preco = reader.GetDecimal("preco")
                };
            }
            return null;
        }
    }
}