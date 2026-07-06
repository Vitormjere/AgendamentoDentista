using AgendamentoDentista.Models;
using AgendamentoDentista.Repositories;

namespace AgendamentoDentista.Services {
    public class ProcedureService {
        private readonly ProcedureRepository _procedureRepository = new();

        public void Cadastrar(string nome, int duracaoMinutos, decimal preco) {
            var procedure = new Procedure {
                Nome = nome,
                DuracaoMinutos = duracaoMinutos,
                Preco = preco
            };

            _procedureRepository.Criar(procedure);
            Console.WriteLine("Procedimento cadastrado com sucesso!");
        }

        public List<Procedure> ListarTodos() {
            return _procedureRepository.ListarTodos();
        }

        public Procedure? BuscarPorId(int id) {
            return _procedureRepository.BuscarPorId(id);
        }
    }
}