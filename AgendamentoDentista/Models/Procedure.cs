namespace AgendamentoDentista.Models {
    public class Procedure {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int DuracaoMinutos { get; set; }
        public decimal Preco { get; set; }
    }
}