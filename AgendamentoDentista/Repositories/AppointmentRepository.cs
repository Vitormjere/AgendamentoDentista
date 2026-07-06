using AgendamentoDentista.Database;
using AgendamentoDentista.Models;
using MySql.Data.MySqlClient;

namespace AgendamentoDentista.Repositories {
    public class AppointmentRepository {
        public void Criar(Appointment appointment) {
            using var conn = DatabaseConfig.GetConnection();
            var cmd = new MySqlCommand(
                "INSERT INTO Appointments (patient_id, procedure_id, user_id, data_hora, observacoes) VALUES (@patientId, @procedureId, @userId, @dataHora, @observacoes)", conn);
            cmd.Parameters.AddWithValue("@patientId", appointment.PatientId);
            cmd.Parameters.AddWithValue("@procedureId", appointment.ProcedureId);
            cmd.Parameters.AddWithValue("@userId", appointment.UserId);
            cmd.Parameters.AddWithValue("@dataHora", appointment.DataHora);
            cmd.Parameters.AddWithValue("@observacoes", appointment.Observacoes ?? "");
            cmd.ExecuteNonQuery();
        }

        public List<Appointment> ListarPorDia(DateTime data) {
            using var conn = DatabaseConfig.GetConnection();
            var cmd = new MySqlCommand(@"
                SELECT a.*, p.nome as patient_nome, pr.nome as procedure_nome
                FROM Appointments a
                JOIN Patients p ON a.patient_id = p.id
                JOIN Procedures pr ON a.procedure_id = pr.id
                WHERE DATE(a.data_hora) = @data
                ORDER BY a.data_hora", conn);
            cmd.Parameters.AddWithValue("@data", data.Date);

            return LerAppointments(cmd);
        }

        public List<Appointment> ListarPorPaciente(int patientId) {
            using var conn = DatabaseConfig.GetConnection();
            var cmd = new MySqlCommand(@"
                SELECT a.*, p.nome as patient_nome, pr.nome as procedure_nome
                FROM Appointments a
                JOIN Patients p ON a.patient_id = p.id
                JOIN Procedures pr ON a.procedure_id = pr.id
                WHERE a.patient_id = @patientId
                ORDER BY a.data_hora DESC", conn);
            cmd.Parameters.AddWithValue("@patientId", patientId);

            return LerAppointments(cmd);
        }

        public void AtualizarStatus(int id, string status) {
            using var conn = DatabaseConfig.GetConnection();
            var cmd = new MySqlCommand(
                "UPDATE Appointments SET status = @status WHERE id = @id", conn);
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        public Appointment? BuscarPorId(int id) {
            using var conn = DatabaseConfig.GetConnection();
            var cmd = new MySqlCommand(@"
                SELECT a.*, p.nome as patient_nome, pr.nome as procedure_nome
                FROM Appointments a
                JOIN Patients p ON a.patient_id = p.id
                JOIN Procedures pr ON a.procedure_id = pr.id
                WHERE a.id = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);

            var results = LerAppointments(cmd);
            return results.FirstOrDefault();
        }

        private List<Appointment> LerAppointments(MySqlCommand cmd) {
            var appointments = new List<Appointment>();
            using var reader = cmd.ExecuteReader();
            while (reader.Read()) {
                appointments.Add(new Appointment {
                    Id = reader.GetInt32("id"),
                    PatientId = reader.GetInt32("patient_id"),
                    ProcedureId = reader.GetInt32("procedure_id"),
                    UserId = reader.GetInt32("user_id"),
                    DataHora = reader.GetDateTime("data_hora"),
                    Status = reader.GetString("status"),
                    Observacoes = reader.IsDBNull(reader.GetOrdinal("observacoes")) ? "" : reader.GetString("observacoes"),
                    CriadoEm = reader.GetDateTime("criado_em"),
                    PatientNome = reader.GetString("patient_nome"),
                    ProcedureNome = reader.GetString("procedure_nome")
                });
            }
            return appointments;
        }
    }
}