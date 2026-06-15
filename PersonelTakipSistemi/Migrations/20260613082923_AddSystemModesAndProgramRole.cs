using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonelTakipSistemi.Migrations
{
    /// <inheritdoc />
    public partial class AddSystemModesAndProgramRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "YetkiliModlar",
                table: "Personeller",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: true);

            migrationBuilder.InsertData(
                table: "SistemRoller",
                columns: new[] { "SistemRolId", "Ad" },
                values: new object[] { 5, "Program Geliştirme Uzmanı" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SistemRoller",
                keyColumn: "SistemRolId",
                keyValue: 5);

            migrationBuilder.DropColumn(
                name: "YetkiliModlar",
                table: "Personeller");
        }
    }
}
