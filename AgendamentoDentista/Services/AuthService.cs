using AgendamentoDentista.Models;
using AgendamentoDentista.Repositories;

namespace AgendamentoDentista.Services {
    public class AuthService {
        private readonly UserRepository _userRepository = new();

        public User? UsuarioLogado { get; private set; }

        public bool Cadastrar(string nome, string email, string senha) {
            var existente = _userRepository.BuscarPorEmail(email);
            if (existente != null) {
                Console.WriteLine("Email já cadastrado!");
                return false;
            }

            var user = new User {
                Nome = nome,
                Email = email,
                SenhaHash = BCrypt.Net.BCrypt.HashPassword(senha)
            };

            _userRepository.Criar(user);
            Console.WriteLine("Cadastro realizado com sucesso!");
            return true;
        }

        public bool Login(string email, string senha) {
            var user = _userRepository.BuscarPorEmail(email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(senha, user.SenhaHash)) {
                Console.WriteLine("Email ou senha incorretos!");
                return false;
            }

            UsuarioLogado = user;
            Console.WriteLine($"Bem-vindo, {user.Nome}!");
            return true;
        }

        public void Logout() {
            UsuarioLogado = null;
        }
    }
}