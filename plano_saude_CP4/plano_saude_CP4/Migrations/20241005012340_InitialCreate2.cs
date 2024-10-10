using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace plano_saude_CP4.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PacientePlanos_Pacientes_PacienteId",
                table: "PacientePlanos");

            migrationBuilder.DropForeignKey(
                name: "FK_PacientePlanos_PlanosSaude_PlanoSaudeId",
                table: "PacientePlanos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PacientePlanos",
                table: "PacientePlanos");

            migrationBuilder.RenameTable(
                name: "PacientePlanos",
                newName: "PacientesPlanos");

            migrationBuilder.RenameIndex(
                name: "IX_PacientePlanos_PlanoSaudeId",
                table: "PacientesPlanos",
                newName: "IX_PacientesPlanos_PlanoSaudeId");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "PacientesPlanos",
                type: "NUMBER(10)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PacientesPlanos",
                table: "PacientesPlanos",
                columns: new[] { "PacienteId", "PlanoSaudeId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PacientesPlanos_Pacientes_PacienteId",
                table: "PacientesPlanos",
                column: "PacienteId",
                principalTable: "Pacientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PacientesPlanos_PlanosSaude_PlanoSaudeId",
                table: "PacientesPlanos",
                column: "PlanoSaudeId",
                principalTable: "PlanosSaude",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PacientesPlanos_Pacientes_PacienteId",
                table: "PacientesPlanos");

            migrationBuilder.DropForeignKey(
                name: "FK_PacientesPlanos_PlanosSaude_PlanoSaudeId",
                table: "PacientesPlanos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PacientesPlanos",
                table: "PacientesPlanos");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PacientesPlanos");

            migrationBuilder.RenameTable(
                name: "PacientesPlanos",
                newName: "PacientePlanos");

            migrationBuilder.RenameIndex(
                name: "IX_PacientesPlanos_PlanoSaudeId",
                table: "PacientePlanos",
                newName: "IX_PacientePlanos_PlanoSaudeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PacientePlanos",
                table: "PacientePlanos",
                columns: new[] { "PacienteId", "PlanoSaudeId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PacientePlanos_Pacientes_PacienteId",
                table: "PacientePlanos",
                column: "PacienteId",
                principalTable: "Pacientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PacientePlanos_PlanosSaude_PlanoSaudeId",
                table: "PacientePlanos",
                column: "PlanoSaudeId",
                principalTable: "PlanosSaude",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
