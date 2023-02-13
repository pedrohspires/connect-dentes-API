using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace connectdentesAPI.Migrations
{
    /// <inheritdoc />
    public partial class AgendamentoSemMedico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_agendamento_Usuario_id_medico",
                table: "agendamento");

            migrationBuilder.DropIndex(
                name: "IX_agendamento_id_medico",
                table: "agendamento");

            migrationBuilder.DropColumn(
                name: "id_medico",
                table: "agendamento");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "id_medico",
                table: "agendamento",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_agendamento_id_medico",
                table: "agendamento",
                column: "id_medico");

            migrationBuilder.AddForeignKey(
                name: "FK_agendamento_Usuario_id_medico",
                table: "agendamento",
                column: "id_medico",
                principalTable: "Usuario",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
