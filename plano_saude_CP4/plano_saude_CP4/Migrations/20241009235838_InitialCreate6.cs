using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace plano_saude_CP4.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PacientesPlanos_Pacientes_PacienteId1",
                table: "PacientesPlanos");

            migrationBuilder.DropForeignKey(
                name: "FK_PacientesPlanos_PlanosSaude_PlanoSaudeId1",
                table: "PacientesPlanos");

            migrationBuilder.DropIndex(
                name: "IX_PacientesPlanos_PacienteId1",
                table: "PacientesPlanos");

            migrationBuilder.DropIndex(
                name: "IX_PacientesPlanos_PlanoSaudeId1",
                table: "PacientesPlanos");

            migrationBuilder.DropColumn(
                name: "PacienteId1",
                table: "PacientesPlanos");

            migrationBuilder.DropColumn(
                name: "PlanoSaudeId1",
                table: "PacientesPlanos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PacienteId1",
                table: "PacientesPlanos",
                type: "NUMBER(10)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PlanoSaudeId1",
                table: "PacientesPlanos",
                type: "NUMBER(10)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PacientesPlanos_PacienteId1",
                table: "PacientesPlanos",
                column: "PacienteId1");

            migrationBuilder.CreateIndex(
                name: "IX_PacientesPlanos_PlanoSaudeId1",
                table: "PacientesPlanos",
                column: "PlanoSaudeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_PacientesPlanos_Pacientes_PacienteId1",
                table: "PacientesPlanos",
                column: "PacienteId1",
                principalTable: "Pacientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PacientesPlanos_PlanosSaude_PlanoSaudeId1",
                table: "PacientesPlanos",
                column: "PlanoSaudeId1",
                principalTable: "PlanosSaude",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
