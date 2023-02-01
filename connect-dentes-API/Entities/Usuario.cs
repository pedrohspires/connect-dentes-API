using System.ComponentModel.DataAnnotations.Schema;

namespace connect_dentes_API.Entities
{
    [Table("Usuario")]
    public class Usuario : BaseEntity
    {
        [Column("nome")]
        public string Nome { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("ativo")]
        public bool Ativo { get; set; }

        [Column("senha")]
        public string Senha { get; set; }

        [Column("salt")]
        public string? Salt { get; set; }

        [Column("tipo")]
        public string Tipo { get; set; }
    }
}
