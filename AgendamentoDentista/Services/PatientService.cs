using AgendamentoDentista.Models;
using AgendamentoDentista.Repositories;

namespace AgendamentoDentista.Services {
    public class PatientService {
        private readonly PatientRepository _patientRepository = new();

        public void Cadastrar(string nome, string telefone, string email, string dataNascimento) {
            if (!DateOnly.TryParseExact(dataNascimento, "dd/MM/yyyy", out DateOnly data)) {
                Console.WriteLine("Data de nascimento inválida!");
                return;
            }

            var patient = new Patient {
                Nome = nome,
                Telefone = telefone,
                Email = email,
                DataNascimento = data
            };

            _patientRepository.Criar(patient);
            Console.WriteLine("Paciente cadastrado com sucesso!");
        }

        public List<Patient> ListarTodos() {
            return _patientRepository.ListarTodos();
        }

        public Patient? BuscarPorId(int id) {
            return _patientRepository.BuscarPorId(id);
        }

        public List<Patient> BuscarPorNome(string nome) {
            return _patientRepository.BuscarPorNome(nome);
        }
    }
}