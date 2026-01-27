using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PersonelTakipSistemi.Migrations
{
    /// <inheritdoc />
    public partial class AddGorevlerModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Birimler",
                columns: table => new
                {
                    BirimId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Birimler", x => x.BirimId);
                });

            migrationBuilder.CreateTable(
                name: "GorevKategorileri",
                columns: table => new
                {
                    GorevKategoriId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GorevKategorileri", x => x.GorevKategoriId);
                });

            migrationBuilder.CreateTable(
                name: "Gorevler",
                columns: table => new
                {
                    GorevId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KategoriId = table.Column<int>(type: "int", nullable: false),
                    PersonelId = table.Column<int>(type: "int", nullable: false),
                    BirimId = table.Column<int>(type: "int", nullable: true),
                    Durum = table.Column<byte>(type: "tinyint", nullable: false),
                    BaslangicTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BitisTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gorevler", x => x.GorevId);
                    table.ForeignKey(
                        name: "FK_Gorevler_Birimler_BirimId",
                        column: x => x.BirimId,
                        principalTable: "Birimler",
                        principalColumn: "BirimId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Gorevler_GorevKategorileri_KategoriId",
                        column: x => x.KategoriId,
                        principalTable: "GorevKategorileri",
                        principalColumn: "GorevKategoriId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Gorevler_Personeller_PersonelId",
                        column: x => x.PersonelId,
                        principalTable: "Personeller",
                        principalColumn: "PersonelId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Birimler",
                columns: new[] { "BirimId", "Ad" },
                values: new object[,]
                {
                    { 1, "Yazılım Birimi" },
                    { 2, "İçerik Birimi" },
                    { 3, "Grafik Birimi" }
                });

            migrationBuilder.InsertData(
                table: "GorevKategorileri",
                columns: new[] { "GorevKategoriId", "Aciklama", "Ad", "CreatedAt", "IsActive", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "Ders kitabı hazırlık işleri", "Ders Kitapları", new DateTime(2026, 1, 27, 13, 35, 37, 879, DateTimeKind.Local).AddTicks(1145), true, null },
                    { 2, "Soru bankası ve etkinlikler", "Yardımcı Kaynaklar", new DateTime(2026, 1, 27, 13, 35, 37, 879, DateTimeKind.Local).AddTicks(1157), true, null },
                    { 3, "Video ve animasyon işleri", "Dijital İçerik", new DateTime(2026, 1, 27, 13, 35, 37, 879, DateTimeKind.Local).AddTicks(1158), true, null },
                    { 4, "Müfredat çalışmaları", "Programlar", new DateTime(2026, 1, 27, 13, 35, 37, 879, DateTimeKind.Local).AddTicks(1159), true, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_GorevKategorileri_Ad",
                table: "GorevKategorileri",
                column: "Ad",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Gorevler_BaslangicTarihi",
                table: "Gorevler",
                column: "BaslangicTarihi");

            migrationBuilder.CreateIndex(
                name: "IX_Gorevler_BirimId",
                table: "Gorevler",
                column: "BirimId");

            migrationBuilder.CreateIndex(
                name: "IX_Gorevler_Durum",
                table: "Gorevler",
                column: "Durum");

            migrationBuilder.CreateIndex(
                name: "IX_Gorevler_KategoriId",
                table: "Gorevler",
                column: "KategoriId");

            migrationBuilder.CreateIndex(
                name: "IX_Gorevler_PersonelId",
                table: "Gorevler",
                column: "PersonelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Gorevler");

            migrationBuilder.DropTable(
                name: "Birimler");

            migrationBuilder.DropTable(
                name: "GorevKategorileri");
        }
    }
}
