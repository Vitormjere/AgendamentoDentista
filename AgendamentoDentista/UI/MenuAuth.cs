using AgendamentoDentista.Services;

namespace AgendamentoDentista.UI {
    public class MenuAuth {
        private readonly AuthService _authService;

        public MenuAuth(AuthService authService) {
            _authService = authService;
        }

        public bool Exibir() {
            while (true) {
                Console.Clear();
                Console.WriteLine("===== AGENDAMENTO DENTISTA =====");
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Cadastrar");
                Console.WriteLine("0. Sair");
                Console.Write("\nEscolha: ");

                var opcao = Console.ReadLine();

                switch (opcao) {
                    case "1":
                        Console.Write("Email: ");
                        var email = Console.ReadLine() ?? "";
                        Console.Write("Senha: ");
                        var senha = Console.ReadLine() ?? "";
                        if (_authService.Login(email, senha))
                            return true;
                        Console.ReadKey();
                        break;

                    case "2":
                        Console.Write("Nome: ");
                        var nome = Console.ReadLine() ?? "";
                        Console.Write("Email: ");
                        var emailCad = Console.ReadLine() ?? "";
                        Console.Write("Senha: ");
                        var senhaCad = Console.ReadLine() ?? "";
                        _authService.Cadastrar(nome, emailCad, senhaCad);
                        Console.ReadKey();
                        break;

                    case "0":
                        return false;

                    default:
                        Console.WriteLine("Opção inválida!");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}