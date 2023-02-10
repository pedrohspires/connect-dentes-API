using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace connectdentesAPI.Migrations
{
    /// <inheritdoc />
    public partial class UsuarioClienteAtendimento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nome = table.Column<string>(type: "text", nullable: false),
                    cpf = table.Column<string>(type: "text", nullable: false),
                    telefone = table.Column<string>(type: "text", nullable: false),
                    iswhatsapp = table.Column<bool>(name: "is_whatsapp", type: "boolean", nullable: false),
                    email = table.Column<string>(type: "text", nullable: true),
                    uf = table.Column<string>(type: "text", nullable: false),
                    cidade = table.Column<string>(type: "text", nullable: false),
                    bairro = table.Column<string>(type: "text", nullable: true),
                    rua = table.Column<string>(type: "text", nullable: true),
                    numero = table.Column<int>(type: "integer", nullable: true),
                    complemento = table.Column<string>(type: "text", nullable: true),
                    ultimoatendimento = table.Column<DateTime>(name: "ultimo_atendimento", type: "timestamp without time zone", nullable: true),
                    datacadastro = table.Column<DateTime>(name: "data_cadastro", type: "timestamp without time zone", nullable: false),
                    usuariocadastro = table.Column<string>(name: "usuario_cadastro", type: "text", nullable: true),
                    dataedicao = table.Column<DateTime>(name: "data_edicao", type: "timestamp without time zone", nullable: true),
                    usuarioedicao = table.Column<string>(name: "usuario_edicao", type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nome = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    ativo = table.Column<bool>(type: "boolean", nullable: false),
                    senha = table.Column<string>(type: "text", nullable: false),
                    salt = table.Column<string>(type: "text", nullable: true),
                    tipo = table.Column<string>(type: "text", nullable: false),
                    datacadastro = table.Column<DateTime>(name: "data_cadastro", type: "timestamp without time zone", nullable: false),
                    usuariocadastro = table.Column<string>(name: "usuario_cadastro", type: "text", nullable: true),
                    dataedicao = table.Column<DateTime>(name: "data_edicao", type: "timestamp without time zone", nullable: true),
                    usuarioedicao = table.Column<string>(name: "usuario_edicao", type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Atendimento",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idmedico = table.Column<int>(name: "id_medico", type: "integer", nullable: false),
                    idcliente = table.Column<int>(name: "id_cliente", type: "integer", nullable: false),
                    detalhes = table.Column<string>(type: "text", nullable: false),
                    observacoes = table.Column<string>(type: "text", nullable: true),
                    dentes = table.Column<string>(type: "text", nullable: true),
                    dataatendimento = table.Column<DateTime>(name: "data_atendimento", type: "timestamp without time zone", nullable: false),
                    dataretorno = table.Column<DateTime>(name: "data_retorno", type: "timestamp without time zone", nullable: true),
                    datacadastro = table.Column<DateTime>(name: "data_cadastro", type: "timestamp without time zone", nullable: false),
                    usuariocadastro = table.Column<string>(name: "usuario_cadastro", type: "text", nullable: true),
                    dataedicao = table.Column<DateTime>(name: "data_edicao", type: "timestamp without time zone", nullable: true),
                    usuarioedicao = table.Column<string>(name: "usuario_edicao", type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atendimento", x => x.id);
                    table.ForeignKey(
                        name: "FK_Atendimento_Cliente_id_cliente",
                        column: x => x.idcliente,
                        principalTable: "Cliente",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Atendimento_Usuario_id_medico",
                        column: x => x.idmedico,
                        principalTable: "Usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Atendimento_id_cliente",
                table: "Atendimento",
                column: "id_cliente");

            migrationBuilder.CreateIndex(
                name: "IX_Atendimento_id_medico",
                table: "Atendimento",
                column: "id_medico");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Atendimento");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
