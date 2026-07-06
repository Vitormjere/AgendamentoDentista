using AgendamentoDentista.Services;

namespace AgendamentoDentista.UI {
    public class MenuConsultas {
        private readonly AppointmentService _appointmentService;
        private readonly PatientService _patientService;
        private readonly ProcedureService _procedureService;
        private readonly int _userId;

        public MenuConsultas(AppointmentService appointmentService, PatientService patientService, ProcedureService procedureService, int userId) {
            _appointmentService = appointmentService;
            _patientService = patientService;
            _procedureService = procedureService;
            _userId = userId;
        }

        public void Exibir() {
            while (true) {
                Console.Clear();
                Console.WriteLine("===== CONSULTAS =====");
                Console.WriteLine("1. Nova consulta");
                Console.WriteLine("2. Consultas do dia");
                Console.WriteLine("3. Consultas por paciente");
                Console.WriteLine("4. Cancelar consulta");
                Console.WriteLine("5. Concluir consulta");
                Console.WriteLine("0. Voltar");
                Console.Write("\nEscolha: ");

                var opcao = Console.ReadLine();

                switch (opcao) {
                    case "1":
                        NovaConsulta();
                        break;
                    case "2":
                        ConsultasDoDia();
                        break;
                    case "3":
                        ConsultasPorPaciente();
                        break;
                    case "4":
                        CancelarConsulta();
                        break;
                    case "5":
                        ConcluirConsulta();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Opção inválida!");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void NovaConsulta() {
            Console.Clear();
            Console.WriteLine("===== NOVA CONSULTA =====");

            var patients = _patientService.ListarTodos();
            if (patients.Count == 0) {
                Console.WriteLine("Nenhum paciente cadastrado. Cadastre um primeiro!");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Pacientes:");
            foreach (var p in patients)
                Console.WriteLine($"{p.Id}. {p.Nome}");

            Console.Write("\nID do paciente: ");
            if (!int.TryParse(Console.ReadLine(), out int patientId)) {
                Console.WriteLine("ID inválido!");
                Console.ReadKey();
                return;
            }

            var procedures = _procedureService.ListarTodos();
            if (procedures.Count == 0) {
                Console.WriteLine("Nenhum procedimento cadastrado. Cadastre um primeiro!");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\nProcedimentos:");
            foreach (var p in procedures)
                Console.WriteLine($"{p.Id}. {p.Nome} | {p.DuracaoMinutos} min | R${p.Preco:F2}");

            Console.Write("\nID do procedimento: ");
            if (!int.TryParse(Console.ReadLine(), out int procedureId)) {
                Console.WriteLine("ID inválido!");
                Console.ReadKey();
                return;
            }

            Console.Write("Data e hora (dd/MM/yyyy HH:mm): ");
            var dataHora = Console.ReadLine() ?? "";

            Console.Write("Observações (opcional): ");
            var observacoes = Console.ReadLine() ?? "";

            _appointmentService.Agendar(patientId, procedureId, _userId, dataHora, observacoes);
            Console.ReadKey();
        }

        private void ConsultasDoDia() {
            Console.Clear();
            var appointments = _appointmentService.ListarPorDia(DateTime.Today);

            if (appointments.Count == 0) {
                Console.WriteLine("Nenhuma consulta para hoje.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"===== CONSULTAS DE HOJE ({DateTime.Today:dd/MM/yyyy}) =====");
            foreach (var a in appointments)
                Console.WriteLine($"{a.Id}. {a.DataHora:HH:mm} | {a.PatientNome} | {a.ProcedureNome} | {a.Status}");

            Console.ReadKey();
        }

        private void ConsultasPorPaciente() {
            Console.Clear();
            var patients = _patientService.ListarTodos();

            if (patients.Count == 0) {
                Console.WriteLine("Nenhum paciente cadastrado.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Pacientes:");
            foreach (var p in patients)
                Console.WriteLine($"{p.Id}. {p.Nome}");

            Console.Write("\nID do paciente: ");
            if (!int.TryParse(Console.ReadLine(), out int patientId)) {
                Console.WriteLine("ID inválido!");
                Console.ReadKey();
                return;
            }

            var appointments = _appointmentService.ListarPorPaciente(patientId);

            if (appointments.Count == 0) {
                Console.WriteLine("Nenhuma consulta encontrada.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("===== CONSULTAS =====");
            foreach (var a in appointments)
                Console.WriteLine($"{a.Id}. {a.DataHora:dd/MM/yyyy HH:mm} | {a.ProcedureNome} | {a.Status}");

            Console.ReadKey();
        }

        private void CancelarConsulta() {
            Console.Clear();
            Console.Write("ID da consulta: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) {
                Console.WriteLine("ID inválido!");
                Console.ReadKey();
                return;
            }

            _appointmentService.CancelarConsulta(id);
            Console.ReadKey();
        }

        private void ConcluirConsulta() {
            Console.Clear();
            Console.Write("ID da consulta: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) {
                Console.WriteLine("ID inválido!");
                Console.ReadKey();
                return;
            }

            _appointmentService.ConcluirConsulta(id);
            Console.ReadKey();
        }
    }
}