using System.ComponentModel.DataAnnotations.Schema;

namespace connect_dentes_API.Entities
{
    public abstract class BaseEntity
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("data_cadastro")]
        public DateTime DataCadastro { get; set; }

        [Column("usuario_cadastro")]
        public string? UsuarioCadastro { get; set; }

        [Column("data_edicao")]
        public DateTime? DataEdicao { get; set; }

        [Column("usuario_edicao")]
        public string? UsuarioEdicao { get; set; }
    }
}
