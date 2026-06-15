using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonelTakipSistemi.Migrations
{
    [Migration("20260610000001_AddPrivatePersonelTaskAssignmentFlags")]
    public partial class AddPrivatePersonelTaskAssignmentFlags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "PersonelGorevListesindenGizlensinMi",
                table: "GorevAtamaPersoneller",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SadeceAdminGorebilirMi",
                table: "GorevAtamaPersoneller",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PersonelGorevListesindenGizlensinMi",
                table: "GorevAtamaPersoneller");

            migrationBuilder.DropColumn(
                name: "SadeceAdminGorebilirMi",
                table: "GorevAtamaPersoneller");
        }
    }
}
