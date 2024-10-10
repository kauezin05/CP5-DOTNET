using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace plano_saude_CP4.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PacientesPlanos",
                table: "PacientesPlanos");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "PacientesPlanos",
                type: "NUMBER(10)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "NUMBER(10)")
                .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1");

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

            migrationBuilder.AddPrimaryKey(
                name: "PK_PacientesPlanos",
                table: "PacientesPlanos",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_PacientesPlanos_PacienteId",
                table: "PacientesPlanos",
                column: "PacienteId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PacientesPlanos_Pacientes_PacienteId1",
                table: "PacientesPlanos");

            migrationBuilder.DropForeignKey(
                name: "FK_PacientesPlanos_PlanosSaude_PlanoSaudeId1",
                table: "PacientesPlanos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PacientesPlanos",
                table: "PacientesPlanos");

            migrationBuilder.DropIndex(
                name: "IX_PacientesPlanos_PacienteId",
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

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "PacientesPlanos",
                type: "NUMBER(10)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "NUMBER(10)")
                .OldAnnotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PacientesPlanos",
                table: "PacientesPlanos",
                columns: new[] { "PacienteId", "PlanoSaudeId" });
        }
    }
}
