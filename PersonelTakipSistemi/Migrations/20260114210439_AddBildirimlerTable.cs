using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonelTakipSistemi.Migrations
{
    /// <inheritdoc />
    public partial class AddBildirimlerTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bildirimler",
                columns: table => new
                {
                    BildirimId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AliciPersonelId = table.Column<int>(type: "int", nullable: false),
                    GonderenPersonelId = table.Column<int>(type: "int", nullable: true),
                    Baslik = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OlusturmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    OkunduMu = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    OkunmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Tip = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RefType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RefId = table.Column<int>(type: "int", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bildirimler", x => x.BildirimId);
                    table.ForeignKey(
                        name: "FK_Bildirimler_Personeller_AliciPersonelId",
                        column: x => x.AliciPersonelId,
                        principalTable: "Personeller",
                        principalColumn: "PersonelId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bildirimler_Personeller_GonderenPersonelId",
                        column: x => x.GonderenPersonelId,
                        principalTable: "Personeller",
                        principalColumn: "PersonelId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bildirimler_AliciPersonelId_OkunduMu_OlusturmaTarihi",
                table: "Bildirimler",
                columns: new[] { "AliciPersonelId", "OkunduMu", "OlusturmaTarihi" });

            migrationBuilder.CreateIndex(
                name: "IX_Bildirimler_AliciPersonelId_OlusturmaTarihi",
                table: "Bildirimler",
                columns: new[] { "AliciPersonelId", "OlusturmaTarihi" });

            migrationBuilder.CreateIndex(
                name: "IX_Bildirimler_GonderenPersonelId",
                table: "Bildirimler",
                column: "GonderenPersonelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bildirimler");
        }
    }
}
