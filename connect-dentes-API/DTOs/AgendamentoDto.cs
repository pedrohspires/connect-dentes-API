using connect_dentes_API.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace connect_dentes_API.DTOs
{
    public class AgendamentoDto : BaseDto
    {
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public DateTime DataAgendada { get; set; }
        public string Status { get; set; }
    }
}
