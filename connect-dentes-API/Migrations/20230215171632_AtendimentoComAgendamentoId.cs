using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace connectdentesAPI.Migrations
{
    /// <inheritdoc />
    public partial class AtendimentoComAgendamentoId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "id_agendamento",
                table: "Atendimento",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Atendimento_id_agendamento",
                table: "Atendimento",
                column: "id_agendamento");

            migrationBuilder.AddForeignKey(
                name: "FK_Atendimento_agendamento_id_agendamento",
                table: "Atendimento",
                column: "id_agendamento",
                principalTable: "agendamento",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Atendimento_agendamento_id_agendamento",
                table: "Atendimento");

            migrationBuilder.DropIndex(
                name: "IX_Atendimento_id_agendamento",
                table: "Atendimento");

            migrationBuilder.DropColumn(
                name: "id_agendamento",
                table: "Atendimento");
        }
    }
}
