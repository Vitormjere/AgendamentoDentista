using AgendamentoDentista.Services;

namespace AgendamentoDentista.UI {
    public class MenuProcedimentos {
        private readonly ProcedureService _procedureService;

        public MenuProcedimentos(ProcedureService procedureService) {
            _procedureService = procedureService;
        }

        public void Exibir() {
            while (true) {
                Console.Clear();
                Console.WriteLine("===== PROCEDIMENTOS =====");
                Console.WriteLine("1. Novo procedimento");
                Console.WriteLine("2. Listar procedimentos");
                Console.WriteLine("0. Voltar");
                Console.Write("\nEscolha: ");

                var opcao = Console.ReadLine();

                switch (opcao) {
                    case "1":
                        NovoProcedimento();
                        break;
                    case "2":
                        ListarProcedimentos();
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

        private void NovoProcedimento() {
            Console.Clear();
            Console.WriteLine("===== NOVO PROCEDIMENTO =====");
            Console.Write("Nome: ");
            var nome = Console.ReadLine() ?? "";

            Console.Write("Duração (minutos): ");
            if (!int.TryParse(Console.ReadLine(), out int duracao)) {
                Console.WriteLine("Duração inválida!");
                Console.ReadKey();
                return;
            }

            Console.Write("Preço: R$");
            if (!decimal.TryParse(Console.ReadLine(), out decimal preco)) {
                Console.WriteLine("Preço inválido!");
                Console.ReadKey();
                return;
            }

            _procedureService.Cadastrar(nome, duracao, preco);
            Console.ReadKey();
        }

        private void ListarProcedimentos() {
            Console.Clear();
            var procedures = _procedureService.ListarTodos();

            if (procedures.Count == 0) {
                Console.WriteLine("Nenhum procedimento cadastrado.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("===== PROCEDIMENTOS =====");
            foreach (var p in procedures)
                Console.WriteLine($"{p.Id}. {p.Nome} | {p.DuracaoMinutos} min | R${p.Preco:F2}");

            Console.ReadKey();
        }
    }
}