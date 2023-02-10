namespace connect_dentes_API.DTOs
{
    public class AtendimentoCreateDto
    {
        public int? MedicoId { get; set; }
        public int? ClienteId { get; set; }
        public string? Detalhes { get; set; }
        public string? Observacoes { get; set; }
        public string? Dentes { get; set; }
        public DateTime? DataAtendimento { get; set; }
        public DateTime? DataRetorno { get; set; }
    }
}
