using AgendamentoDentista.Models;
using AgendamentoDentista.Repositories;

namespace AgendamentoDentista.Services {
    public class AppointmentService {
        private readonly AppointmentRepository _appointmentRepository = new();
        private readonly PatientRepository _patientRepository = new();
        private readonly ProcedureRepository _procedureRepository = new();

        public void Agendar(int patientId, int procedureId, int userId, string dataHora, string observacoes) {
            var patient = _patientRepository.BuscarPorId(patientId);
            if (patient == null) {
                Console.WriteLine("Paciente não encontrado!");
                return;
            }

            var procedure = _procedureRepository.BuscarPorId(procedureId);
            if (procedure == null) {
                Console.WriteLine("Procedimento não encontrado!");
                return;
            }

            if (!DateTime.TryParseExact(dataHora, "dd/MM/yyyy HH:mm",
                null, System.Globalization.DateTimeStyles.None, out DateTime data)) {
                Console.WriteLine("Data e hora inválidas! Use o formato dd/MM/yyyy HH:mm");
                return;
            }

            if (data < DateTime.Now) {
                Console.WriteLine("Não é possível agendar para uma data no passado!");
                return;
            }

            var appointment = new Appointment {
                PatientId = patientId,
                ProcedureId = procedureId,
                UserId = userId,
                DataHora = data,
                Observacoes = observacoes
            };

            _appointmentRepository.Criar(appointment);
            Console.WriteLine($"Consulta agendada com sucesso para {data:dd/MM/yyyy} às {data:HH:mm}!");
        }

        public void CancelarConsulta(int id) {
            var appointment = _appointmentRepository.BuscarPorId(id);
            if (appointment == null) {
                Console.WriteLine("Consulta não encontrada!");
                return;
            }

            if (appointment.Status == "cancelado") {
                Console.WriteLine("Essa consulta já está cancelada!");
                return;
            }

            _appointmentRepository.AtualizarStatus(id, "cancelado");
            Console.WriteLine("Consulta cancelada com sucesso!");
        }

        public void ConcluirConsulta(int id) {
            var appointment = _appointmentRepository.BuscarPorId(id);
            if (appointment == null) {
                Console.WriteLine("Consulta não encontrada!");
                return;
            }

            _appointmentRepository.AtualizarStatus(id, "concluido");
            Console.WriteLine("Consulta concluída com sucesso!");
        }

        public List<Appointment> ListarPorDia(DateTime data) {
            return _appointmentRepository.ListarPorDia(data);
        }

        public List<Appointment> ListarPorPaciente(int patientId) {
            return _appointmentRepository.ListarPorPaciente(patientId);
        }
    }
}