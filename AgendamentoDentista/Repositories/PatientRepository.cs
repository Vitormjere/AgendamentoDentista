using AgendamentoDentista.Database;
using AgendamentoDentista.Models;
using MySql.Data.MySqlClient;

namespace AgendamentoDentista.Repositories {
    public class PatientRepository {
        public void Criar(Patient patient) {
            using var conn = DatabaseConfig.GetConnection();
            var cmd = new MySqlCommand(
                "INSERT INTO Patients (nome, telefone, email, data_nascimento) VALUES (@nome, @telefone, @email, @dataNascimento)", conn);
            cmd.Parameters.AddWithValue("@nome", patient.Nome);
            cmd.Parameters.AddWithValue("@telefone", patient.Telefone);
            cmd.Parameters.AddWithValue("@email", patient.Email);
            cmd.Parameters.AddWithValue("@dataNascimento", patient.DataNascimento.ToDateTime(TimeOnly.MinValue));
            cmd.ExecuteNonQuery();
        }

        public List<Patient> ListarTodos() {
            using var conn = DatabaseConfig.GetConnection();
            var cmd = new MySqlCommand("SELECT * FROM Patients ORDER BY nome", conn);

            var patients = new List<Patient>();
            using var reader = cmd.ExecuteReader();
            while (reader.Read()) {
                patients.Add(new Patient {
                    Id = reader.GetInt32("id"),
                    Nome = reader.GetString("nome"),
                    Telefone = reader.GetString("telefone"),
                    Email = reader.GetString("email"),
                    DataNascimento = DateOnly.FromDateTime(reader.GetDateTime("data_nascimento")),
                    CriadoEm = reader.GetDateTime("criado_em")
                });
            }
            return patients;
        }

        public Patient? BuscarPorId(int id) {
            using var conn = DatabaseConfig.GetConnection();
            var cmd = new MySqlCommand("SELECT * FROM Patients WHERE id = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);

            using var reader = cmd.ExecuteReader();
            if (reader.Read()) {
                return new Patient {
                    Id = reader.GetInt32("id"),
                    Nome = reader.GetString("nome"),
                    Telefone = reader.GetString("telefone"),
                    Email = reader.GetString("email"),
                    DataNascimento = DateOnly.FromDateTime(reader.GetDateTime("data_nascimento")),
                    CriadoEm = reader.GetDateTime("criado_em")
                };
            }
            return null;
        }

        public List<Patient> BuscarPorNome(string nome) {
            using var conn = DatabaseConfig.GetConnection();
            var cmd = new MySqlCommand("SELECT * FROM Patients WHERE nome LIKE @nome ORDER BY nome", conn);
            cmd.Parameters.AddWithValue("@nome", $"%{nome}%");

            var patients = new List<Patient>();
            using var reader = cmd.ExecuteReader();
            while (reader.Read()) {
                patients.Add(new Patient {
                    Id = reader.GetInt32("id"),
                    Nome = reader.GetString("nome"),
                    Telefone = reader.GetString("telefone"),
                    Email = reader.GetString("email"),
                    DataNascimento = DateOnly.FromDateTime(reader.GetDateTime("data_nascimento")),
                    CriadoEm = reader.GetDateTime("criado_em")
                });
            }
            return patients;
        }
    }
}