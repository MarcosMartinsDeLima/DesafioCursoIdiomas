using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CursoIdiomas.Migrations
{
    /// <inheritdoc />
    public partial class ajuste : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "DataNascimento",
                table: "Aluno",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DataNascimento",
                table: "Aluno",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");
        }
    }
}
