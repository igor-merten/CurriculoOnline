using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CurriculoOnline.Migrations
{
    public partial class PossibilitandodatasaidanullaExperiencia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DataFim",
                table: "Experiencia",
                nullable: true,
                oldClrType: typeof(DateTime));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DataFim",
                table: "Experiencia",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
