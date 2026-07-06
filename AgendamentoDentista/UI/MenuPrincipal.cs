using AgendamentoDentista.Services;

namespace AgendamentoDentista.UI {
    public class MenuPrincipal {
        private readonly AuthService _authService;
        private readonly PatientService _patientService;
        private readonly ProcedureService _procedureService;
        private readonly AppointmentService _appointmentService;

        public MenuPrincipal(AuthService authService) {
            _authService = authService;
            _patientService = new PatientService();
            _procedureService = new ProcedureService();
            _appointmentService = new AppointmentService();
        }

        public void Exibir() {
            var userId = _authService.UsuarioLogado!.Id;
            var nome = _authService.UsuarioLogado!.Nome;

            while (true) {
                Console.Clear();
                Console.WriteLine($"===== BEM-VINDO, {nome.ToUpper()} =====");
                Console.WriteLine("1. Consultas");
                Console.WriteLine("2. Pacientes");
                Console.WriteLine("3. Procedimentos");
                Console.WriteLine("0. Sair");
                Console.Write("\nEscolha: ");

                var opcao = Console.ReadLine();

                switch (opcao) {
                    case "1":
                        new MenuConsultas(_appointmentService, _patientService, _procedureService, userId).Exibir();
                        break;
                    case "2":
                        new MenuPacientes(_patientService).Exibir();
                        break;
                    case "3":
                        new MenuProcedimentos(_procedureService).Exibir();
                        break;
                    case "0":
                        _authService.Logout();
                        return;
                    default:
                        Console.WriteLine("Opção inválida!");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}