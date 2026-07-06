namespace AgendamentoDentista.Models {
    public class Patient {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public DateOnly DataNascimento { get; set; }
        public DateTime CriadoEm { get; set; }
    }
}