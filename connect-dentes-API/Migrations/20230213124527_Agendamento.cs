using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace connectdentesAPI.Migrations
{
    /// <inheritdoc />
    public partial class Agendamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "agendamento",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idmedico = table.Column<int>(name: "id_medico", type: "integer", nullable: false),
                    idcliente = table.Column<int>(name: "id_cliente", type: "integer", nullable: false),
                    dataagendada = table.Column<DateTime>(name: "data_agendada", type: "timestamp without time zone", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false),
                    datacadastro = table.Column<DateTime>(name: "data_cadastro", type: "timestamp without time zone", nullable: false),
                    usuariocadastro = table.Column<string>(name: "usuario_cadastro", type: "text", nullable: true),
                    dataedicao = table.Column<DateTime>(name: "data_edicao", type: "timestamp without time zone", nullable: true),
                    usuarioedicao = table.Column<string>(name: "usuario_edicao", type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_agendamento", x => x.id);
                    table.ForeignKey(
                        name: "FK_agendamento_Cliente_id_cliente",
                        column: x => x.idcliente,
                        principalTable: "Cliente",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_agendamento_Usuario_id_medico",
                        column: x => x.idmedico,
                        principalTable: "Usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_agendamento_id_cliente",
                table: "agendamento",
                column: "id_cliente");

            migrationBuilder.CreateIndex(
                name: "IX_agendamento_id_medico",
                table: "agendamento",
                column: "id_medico");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "agendamento");
        }
    }
}
