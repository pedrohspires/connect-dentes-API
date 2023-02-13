namespace connect_dentes_API.DTOs
{
    public class AgendamentoFiltroDto
    {
        public DateTime? DataInicio{ get; set; }
        public DateTime? DataFim { get; set; }
        public int? ClienteId { get; set; }
        public string? Status { get; set; }
    }
}
