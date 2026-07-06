namespace AgendamentoDentista.Models {
    public class Appointment {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int ProcedureId { get; set; }
        public int UserId { get; set; }
        public DateTime DataHora { get; set; }
        public string Status { get; set; } // "agendado", "concluido", "cancelado"
        public string Observacoes { get; set; }
        public DateTime CriadoEm { get; set; }
        public string? PatientNome { get; set; }
        public string? ProcedureNome { get; set; }
    }
}