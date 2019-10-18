using Microsoft.EntityFrameworkCore.Migrations;

namespace CurriculoOnline.Migrations
{
    public partial class AcrescentandoEnderecoCandidato : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Endereco",
                table: "Candidato",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Endereco",
                table: "Candidato");
        }
    }
}
