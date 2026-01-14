using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PersonelTakipSistemi.Migrations
{
    /// <inheritdoc />
    public partial class AddSistemRoller : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SistemRoller",
                columns: table => new
                {
                    SistemRolId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SistemRoller", x => x.SistemRolId);
                });

            migrationBuilder.InsertData(
                table: "SistemRoller",
                columns: new[] { "SistemRolId", "Ad" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "Yönetici" },
                    { 3, "Editör" },
                    { 4, "Kullanıcı" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SistemRoller");
        }
    }
}
