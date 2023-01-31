namespace connect_dentes_API.DTOs
{
    public abstract class BaseDto
    {
        public int Id { get; set; }
        public DateTime DataCadastro { get; set; }
        public string? UsuarioCadastro { get; set; }
        public DateTime? DataEdicao { get; set; }
        public string? UsuarioEdicao { get; set; }
    }
}
