using System;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonelTakipSistemi.Migrations
{
    [Migration("20260609000001_AddPersonelGorevTarihleri")]
    public partial class AddPersonelGorevTarihleri : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "GoreveBaslamaTarihi",
                table: "Personeller",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "GorevBitisTarihi",
                table: "Personeller",
                type: "date",
                nullable: true);

            migrationBuilder.Sql(
                """
                UPDATE [Personeller]
                SET [GorevBitisTarihi] = '2026-06-30'
                WHERE [GorevBitisTarihi] IS NULL;
                """);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GoreveBaslamaTarihi",
                table: "Personeller");

            migrationBuilder.DropColumn(
                name: "GorevBitisTarihi",
                table: "Personeller");
        }
    }
}
