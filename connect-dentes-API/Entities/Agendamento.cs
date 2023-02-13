using System.ComponentModel.DataAnnotations.Schema;

namespace connect_dentes_API.Entities
{
    [Table("agendamento")]
    public class Agendamento : BaseEntity
    {
        [Column("id_cliente")]
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        [Column("data_agendada")]
        public DateTime DataAgendada { get; set; }

        [Column("status")]
        public string Status { get; set; }
    }
}
