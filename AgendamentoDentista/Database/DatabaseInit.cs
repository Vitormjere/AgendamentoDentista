namespace AgendamentoDentista.Database {
    public class DatabaseInit {
        public static void Verificar() {
            try {
                using var conn = DatabaseConfig.GetConnection();
                Console.WriteLine("Conexão com banco de dados estabelecida com sucesso!");
            } catch (Exception ex) {
                Console.WriteLine($"Erro ao conectar ao banco: {ex.Message}");
                Environment.Exit(1);
            }
        }
    }
}