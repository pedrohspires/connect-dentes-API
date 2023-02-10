namespace connect_dentes_API.DTOs
{
    public class UsuarioCadastroDto
    {
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Senha { get; set; }
        public string? Tipo { get; set; }
        public bool Ativo { get; set; }
    }
}
