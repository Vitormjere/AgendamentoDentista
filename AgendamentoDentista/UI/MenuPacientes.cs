using AgendamentoDentista.Services;

namespace AgendamentoDentista.UI {
    public class MenuPacientes {
        private readonly PatientService _patientService;

        public MenuPacientes(PatientService patientService) {
            _patientService = patientService;
        }

        public void Exibir() {
            while (true) {
                Console.Clear();
                Console.WriteLine("===== PACIENTES =====");
                Console.WriteLine("1. Novo paciente");
                Console.WriteLine("2. Listar pacientes");
                Console.WriteLine("3. Buscar por nome");
                Console.WriteLine("0. Voltar");
                Console.Write("\nEscolha: ");

                var opcao = Console.ReadLine();

                switch (opcao) {
                    case "1":
                        NovoPaciente();
                        break;
                    case "2":
                        ListarPacientes();
                        break;
                    case "3":
                        BuscarPorNome();
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

        private void NovoPaciente() {
            Console.Clear();
            Console.WriteLine("===== NOVO PACIENTE =====");
            Console.Write("Nome: ");
            var nome = Console.ReadLine() ?? "";
            Console.Write("Telefone: ");
            var telefone = Console.ReadLine() ?? "";
            Console.Write("Email: ");
            var email = Console.ReadLine() ?? "";
            Console.Write("Data de nascimento (dd/MM/yyyy): ");
            var dataNascimento = Console.ReadLine() ?? "";

            _patientService.Cadastrar(nome, telefone, email, dataNascimento);
            Console.ReadKey();
        }

        private void ListarPacientes() {
            Console.Clear();
            var patients = _patientService.ListarTodos();

            if (patients.Count == 0) {
                Console.WriteLine("Nenhum paciente cadastrado.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("===== PACIENTES =====");
            foreach (var p in patients)
                Console.WriteLine($"{p.Id}. {p.Nome} | {p.Telefone} | {p.DataNascimento:dd/MM/yyyy}");

            Console.ReadKey();
        }

        private void BuscarPorNome() {
            Console.Clear();
            Console.Write("Nome: ");
            var nome = Console.ReadLine() ?? "";
            var patients = _patientService.BuscarPorNome(nome);

            if (patients.Count == 0) {
                Console.WriteLine("Nenhum paciente encontrado.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("===== RESULTADO =====");
            foreach (var p in patients)
                Console.WriteLine($"{p.Id}. {p.Nome} | {p.Telefone} | {p.DataNascimento:dd/MM/yyyy}");

            Console.ReadKey();
        }
    }
}