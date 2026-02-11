using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FuncionarioAPI.Migrations
{
    /// <inheritdoc />
    public partial class AtualizarCamposFuncionario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Salario",
                table: "Funcionarios",
                newName: "Trabalho");

            migrationBuilder.RenameColumn(
                name: "Cargo",
                table: "Funcionarios",
                newName: "SalarioAnual");

            migrationBuilder.AddColumn<int>(
                name: "Idade",
                table: "Funcionarios",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Pais",
                table: "Funcionarios",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Idade",
                table: "Funcionarios");

            migrationBuilder.DropColumn(
                name: "Pais",
                table: "Funcionarios");

            migrationBuilder.RenameColumn(
                name: "Trabalho",
                table: "Funcionarios",
                newName: "Salario");

            migrationBuilder.RenameColumn(
                name: "SalarioAnual",
                table: "Funcionarios",
                newName: "Cargo");
        }
    }
}
