using connect_dentes_API.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace connect_dentes_API.DTOs
{
    public class AgendamentoCreateDto
    {
        public int? ClienteId { get; set; }
        public DateTime? DataAgendada { get; set; }
    }
}
