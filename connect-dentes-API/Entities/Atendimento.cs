using System.ComponentModel.DataAnnotations.Schema;

namespace connect_dentes_API.Entities
{
    [Table("Atendimento")]
    public class Atendimento : BaseEntity
    {
        [Column("id_medico")]
        public int MedicoId { get; set; }
        public Usuario? Medico { get; set; }

        [Column("id_cliente")]
        public int ClienteId{ get; set; }
        public Cliente? Cliente { get; set; }

        [Column("id_agendamento")]
        public int? AgendamentoId { get; set; }
        public Agendamento? Agendamento { get; set; }

        [Column("detalhes")]
        public string Detalhes { get; set; }

        [Column("observacoes")]
        public string? Observacoes { get; set; }

        [Column("dentes")]
        public string? Dentes { get; set; }

        [Column("data_atendimento")]
        public DateTime DataAtendimento { get; set; }

        [Column("data_retorno")]
        public DateTime? DataRetorno { get; set; }
    }
}
