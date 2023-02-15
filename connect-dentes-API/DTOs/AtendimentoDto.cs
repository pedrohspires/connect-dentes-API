using connect_dentes_API.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace connect_dentes_API.DTOs
{
    public class AtendimentoDto : BaseDto
    {
        public int MedicoId { get; set; }

        public UsuarioDto? Medico { get; set; }

        public int ClienteId { get; set; }

        public Cliente? Cliente { get; set; }

        public int? AgendamentoId { get; set; }

        public Agendamento? Agendamento { get; set; }

        public string Detalhes { get; set; }

        public string? Observacoes { get; set; }

        public string? Dentes { get; set; }

        public DateTime DataAtendimento { get; set; }

        public DateTime? DataRetorno { get; set; }
    }
}
