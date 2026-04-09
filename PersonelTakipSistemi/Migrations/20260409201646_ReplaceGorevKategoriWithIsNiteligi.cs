using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PersonelTakipSistemi.Migrations
{
    /// <inheritdoc />
    public partial class ReplaceGorevKategoriWithIsNiteligi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gorevler_GorevKategorileri_KategoriId",
                table: "Gorevler");

            migrationBuilder.DropTable(
                name: "GorevKategorileri");

            migrationBuilder.RenameColumn(
                name: "KategoriId",
                table: "Gorevler",
                newName: "IsNiteligiId");

            migrationBuilder.RenameIndex(
                name: "IX_Gorevler_KategoriId",
                table: "Gorevler",
                newName: "IX_Gorevler_IsNiteligiId");

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 23, 16, 45, 988, DateTimeKind.Local).AddTicks(1280));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 23, 16, 45, 988, DateTimeKind.Local).AddTicks(1303));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 23, 16, 45, 988, DateTimeKind.Local).AddTicks(1306));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 23, 16, 45, 988, DateTimeKind.Local).AddTicks(1308));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 23, 16, 45, 988, DateTimeKind.Local).AddTicks(1310));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 23, 16, 45, 988, DateTimeKind.Local).AddTicks(1313));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 7,
                columns: new[] { "CreatedAt", "IsNiteligiId" },
                values: new object[] { new DateTime(2026, 4, 9, 23, 16, 45, 988, DateTimeKind.Local).AddTicks(1316), 9 });

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 8,
                columns: new[] { "CreatedAt", "IsNiteligiId" },
                values: new object[] { new DateTime(2026, 4, 9, 23, 16, 45, 988, DateTimeKind.Local).AddTicks(1317), 5 });

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 9,
                columns: new[] { "CreatedAt", "IsNiteligiId" },
                values: new object[] { new DateTime(2026, 4, 9, 23, 16, 45, 988, DateTimeKind.Local).AddTicks(1319), 8 });

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 10,
                columns: new[] { "CreatedAt", "IsNiteligiId" },
                values: new object[] { new DateTime(2026, 4, 9, 23, 16, 45, 988, DateTimeKind.Local).AddTicks(1322), 10 });

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 11,
                columns: new[] { "CreatedAt", "IsNiteligiId" },
                values: new object[] { new DateTime(2026, 4, 9, 23, 16, 45, 988, DateTimeKind.Local).AddTicks(1324), 10 });

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 12,
                columns: new[] { "CreatedAt", "IsNiteligiId" },
                values: new object[] { new DateTime(2026, 4, 9, 23, 16, 45, 988, DateTimeKind.Local).AddTicks(1325), 10 });

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 23, 16, 45, 979, DateTimeKind.Local).AddTicks(3038));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 23, 16, 45, 979, DateTimeKind.Local).AddTicks(3039));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 23, 16, 45, 979, DateTimeKind.Local).AddTicks(2983));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 23, 16, 45, 979, DateTimeKind.Local).AddTicks(2987));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 23, 16, 45, 979, DateTimeKind.Local).AddTicks(2989));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 23, 16, 45, 979, DateTimeKind.Local).AddTicks(2990));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 23, 16, 45, 979, DateTimeKind.Local).AddTicks(2991));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 23, 16, 45, 979, DateTimeKind.Local).AddTicks(2992));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 23, 16, 45, 979, DateTimeKind.Local).AddTicks(2993));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 23, 16, 45, 979, DateTimeKind.Local).AddTicks(2994));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 23, 16, 45, 979, DateTimeKind.Local).AddTicks(2994));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 23, 16, 45, 979, DateTimeKind.Local).AddTicks(3032));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 23, 16, 45, 979, DateTimeKind.Local).AddTicks(3032));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 23, 16, 45, 979, DateTimeKind.Local).AddTicks(3033));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 16,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 23, 16, 45, 979, DateTimeKind.Local).AddTicks(3034));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 17,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 23, 16, 45, 979, DateTimeKind.Local).AddTicks(3035));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 18,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 23, 16, 45, 979, DateTimeKind.Local).AddTicks(3036));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 19,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 23, 16, 45, 979, DateTimeKind.Local).AddTicks(3037));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 23, 16, 45, 979, DateTimeKind.Local).AddTicks(1786));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 23, 16, 45, 979, DateTimeKind.Local).AddTicks(1801));

            migrationBuilder.AddForeignKey(
                name: "FK_Gorevler_IsNitelikleri_IsNiteligiId",
                table: "Gorevler",
                column: "IsNiteligiId",
                principalTable: "IsNitelikleri",
                principalColumn: "IsNiteligiId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gorevler_IsNitelikleri_IsNiteligiId",
                table: "Gorevler");

            migrationBuilder.RenameColumn(
                name: "IsNiteligiId",
                table: "Gorevler",
                newName: "KategoriId");

            migrationBuilder.RenameIndex(
                name: "IX_Gorevler_IsNiteligiId",
                table: "Gorevler",
                newName: "IX_Gorevler_KategoriId");

            migrationBuilder.CreateTable(
                name: "GorevKategorileri",
                columns: table => new
                {
                    GorevKategoriId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Aciklama = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Ad = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Renk = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GorevKategorileri", x => x.GorevKategoriId);
                });

            migrationBuilder.InsertData(
                table: "GorevKategorileri",
                columns: new[] { "GorevKategoriId", "Aciklama", "Ad", "CreatedAt", "IsActive", "Renk", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "Ders kitabı hazırlık işleri", "Ders Kitapları", new DateTime(2026, 4, 8, 2, 24, 18, 42, DateTimeKind.Local).AddTicks(2203), true, "#3B82F6", null },
                    { 2, "Soru bankası ve etkinlikler", "Yardımcı Kaynaklar", new DateTime(2026, 4, 8, 2, 24, 18, 42, DateTimeKind.Local).AddTicks(2215), true, "#10B981", null },
                    { 3, "Video ve animasyon işleri", "Dijital İçerik", new DateTime(2026, 4, 8, 2, 24, 18, 42, DateTimeKind.Local).AddTicks(2217), true, "#F59E0B", null },
                    { 4, "Müfredat çalışmaları", "Programlar", new DateTime(2026, 4, 8, 2, 24, 18, 42, DateTimeKind.Local).AddTicks(2218), true, "#8B5CF6", null }
                });

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 8, 2, 24, 18, 42, DateTimeKind.Local).AddTicks(6081));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 8, 2, 24, 18, 42, DateTimeKind.Local).AddTicks(6094));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 8, 2, 24, 18, 42, DateTimeKind.Local).AddTicks(6097));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 8, 2, 24, 18, 42, DateTimeKind.Local).AddTicks(6099));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 8, 2, 24, 18, 42, DateTimeKind.Local).AddTicks(6101));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 8, 2, 24, 18, 42, DateTimeKind.Local).AddTicks(6104));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 7,
                columns: new[] { "CreatedAt", "KategoriId" },
                values: new object[] { new DateTime(2026, 4, 8, 2, 24, 18, 42, DateTimeKind.Local).AddTicks(6106), 3 });

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 8,
                columns: new[] { "CreatedAt", "KategoriId" },
                values: new object[] { new DateTime(2026, 4, 8, 2, 24, 18, 42, DateTimeKind.Local).AddTicks(6107), 3 });

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 9,
                columns: new[] { "CreatedAt", "KategoriId" },
                values: new object[] { new DateTime(2026, 4, 8, 2, 24, 18, 42, DateTimeKind.Local).AddTicks(6141), 3 });

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 10,
                columns: new[] { "CreatedAt", "KategoriId" },
                values: new object[] { new DateTime(2026, 4, 8, 2, 24, 18, 42, DateTimeKind.Local).AddTicks(6144), 4 });

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 11,
                columns: new[] { "CreatedAt", "KategoriId" },
                values: new object[] { new DateTime(2026, 4, 8, 2, 24, 18, 42, DateTimeKind.Local).AddTicks(6146), 4 });

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 12,
                columns: new[] { "CreatedAt", "KategoriId" },
                values: new object[] { new DateTime(2026, 4, 8, 2, 24, 18, 42, DateTimeKind.Local).AddTicks(6147), 4 });

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 8, 2, 24, 18, 33, DateTimeKind.Local).AddTicks(4045));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 8, 2, 24, 18, 33, DateTimeKind.Local).AddTicks(4046));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 8, 2, 24, 18, 33, DateTimeKind.Local).AddTicks(4003));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 8, 2, 24, 18, 33, DateTimeKind.Local).AddTicks(4008));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 8, 2, 24, 18, 33, DateTimeKind.Local).AddTicks(4009));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 8, 2, 24, 18, 33, DateTimeKind.Local).AddTicks(4010));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 8, 2, 24, 18, 33, DateTimeKind.Local).AddTicks(4011));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 8, 2, 24, 18, 33, DateTimeKind.Local).AddTicks(4012));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 8, 2, 24, 18, 33, DateTimeKind.Local).AddTicks(4013));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 8, 2, 24, 18, 33, DateTimeKind.Local).AddTicks(4014));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 8, 2, 24, 18, 33, DateTimeKind.Local).AddTicks(4039));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 8, 2, 24, 18, 33, DateTimeKind.Local).AddTicks(4040));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 8, 2, 24, 18, 33, DateTimeKind.Local).AddTicks(4041));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 8, 2, 24, 18, 33, DateTimeKind.Local).AddTicks(4042));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 16,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 8, 2, 24, 18, 33, DateTimeKind.Local).AddTicks(4042));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 17,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 8, 2, 24, 18, 33, DateTimeKind.Local).AddTicks(4043));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 18,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 8, 2, 24, 18, 33, DateTimeKind.Local).AddTicks(4044));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 19,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 8, 2, 24, 18, 33, DateTimeKind.Local).AddTicks(4045));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 8, 2, 24, 18, 33, DateTimeKind.Local).AddTicks(2769));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 8, 2, 24, 18, 33, DateTimeKind.Local).AddTicks(2784));

            migrationBuilder.CreateIndex(
                name: "IX_GorevKategorileri_Ad",
                table: "GorevKategorileri",
                column: "Ad",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Gorevler_GorevKategorileri_KategoriId",
                table: "Gorevler",
                column: "KategoriId",
                principalTable: "GorevKategorileri",
                principalColumn: "GorevKategoriId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
