using System.ComponentModel.DataAnnotations.Schema;

namespace connect_dentes_API.Entities
{
    [Table("Cliente")]
    public class Cliente : BaseEntity
    {
        [Column("nome")]
        public string Nome { get; set; }

        [Column("cpf")]
        public string Cpf { get; set; }

        [Column("telefone")]
        public string Telefone { get; set; }

        [Column("is_whatsapp")]
        public bool IsWhatsapp { get; set; }

        [Column("email")]
        public string? Email { get; set; }
        
        [Column("uf")]
        public string UF { get; set; }
        
        [Column("cidade")]
        public string Cidade { get; set; }
        
        [Column("bairro")]
        public string? Bairro { get; set; }
        
        [Column("rua")]
        public string? Rua { get; set; }
        
        [Column("numero")]
        public int? Numero { get; set; }

        [Column("complemento")]
        public string? Complemento { get; set; }

        [Column("ultimo_atendimento")]
        public DateTime? UltimoAtendimento { get; set; }
    }
}
