using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PersonelTakipSistemi.Migrations
{
    /// <inheritdoc />
    public partial class AddDaireBaskanligi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DaireBaskanliklari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DaireBaskanliklari", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "DaireBaskanliklari",
                columns: new[] { "Id", "Ad", "IsActive" },
                values: new object[,]
                {
                    { 1, "Araştırma Geliştirme ve Projeler Daire Başkanlığı", false },
                    { 2, "Eğitim Ortamlarının ve Öğrenme Süreçlerinin Geliştirilmesi Daire Başkanlığı", false },
                    { 3, "Eğitim Politikaları Daire Başkanlığı", false },
                    { 4, "Erken Çocukluk Eğitimi Daire Başkanlığı", false },
                    { 5, "İdari ve Mali İşler Daire Başkanlığı", false },
                    { 6, "İzleme ve Değerlendirme Daire Başkanlığı", false },
                    { 7, "Kültür, Sanat ve Spor Etkinlikleri Daire Başkanlığı", false },
                    { 8, "Öğrenci İşleri Daire Başkanlığı", false },
                    { 9, "Programlar ve Öğretim Materyalleri Daire Başkanlığı", true }
                });

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 12, 13, 59, 3, 980, DateTimeKind.Local).AddTicks(8011));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 12, 13, 59, 3, 980, DateTimeKind.Local).AddTicks(8019));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 12, 13, 59, 3, 980, DateTimeKind.Local).AddTicks(8020));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 12, 13, 59, 3, 980, DateTimeKind.Local).AddTicks(8021));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 12, 13, 59, 3, 981, DateTimeKind.Local).AddTicks(935));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 12, 13, 59, 3, 981, DateTimeKind.Local).AddTicks(949));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 12, 13, 59, 3, 981, DateTimeKind.Local).AddTicks(952));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 12, 13, 59, 3, 981, DateTimeKind.Local).AddTicks(955));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 12, 13, 59, 3, 981, DateTimeKind.Local).AddTicks(957));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 12, 13, 59, 3, 981, DateTimeKind.Local).AddTicks(960));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 12, 13, 59, 3, 981, DateTimeKind.Local).AddTicks(962));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 12, 13, 59, 3, 981, DateTimeKind.Local).AddTicks(964));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 12, 13, 59, 3, 981, DateTimeKind.Local).AddTicks(966));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 12, 13, 59, 3, 981, DateTimeKind.Local).AddTicks(969));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 12, 13, 59, 3, 981, DateTimeKind.Local).AddTicks(970));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 12, 13, 59, 3, 981, DateTimeKind.Local).AddTicks(972));

            migrationBuilder.UpdateData(
                table: "Komisyonlar",
                keyColumn: "KomisyonId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 12, 13, 59, 3, 977, DateTimeKind.Local).AddTicks(9301));

            migrationBuilder.UpdateData(
                table: "Komisyonlar",
                keyColumn: "KomisyonId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 12, 13, 59, 3, 977, DateTimeKind.Local).AddTicks(9305));

            migrationBuilder.UpdateData(
                table: "Komisyonlar",
                keyColumn: "KomisyonId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 12, 13, 59, 3, 977, DateTimeKind.Local).AddTicks(9306));

            migrationBuilder.UpdateData(
                table: "Komisyonlar",
                keyColumn: "KomisyonId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 12, 13, 59, 3, 977, DateTimeKind.Local).AddTicks(9307));

            migrationBuilder.UpdateData(
                table: "Komisyonlar",
                keyColumn: "KomisyonId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 12, 13, 59, 3, 977, DateTimeKind.Local).AddTicks(9308));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 12, 13, 59, 3, 977, DateTimeKind.Local).AddTicks(8426));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 12, 13, 59, 3, 977, DateTimeKind.Local).AddTicks(8432));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 12, 13, 59, 3, 977, DateTimeKind.Local).AddTicks(8433));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 12, 13, 59, 3, 977, DateTimeKind.Local).AddTicks(7297));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 12, 13, 59, 3, 977, DateTimeKind.Local).AddTicks(7309));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DaireBaskanliklari");

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 11, 17, 23, 49, 398, DateTimeKind.Local).AddTicks(4712));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 11, 17, 23, 49, 398, DateTimeKind.Local).AddTicks(4725));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 11, 17, 23, 49, 398, DateTimeKind.Local).AddTicks(4726));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 11, 17, 23, 49, 398, DateTimeKind.Local).AddTicks(4726));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 11, 17, 23, 49, 398, DateTimeKind.Local).AddTicks(7686));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 11, 17, 23, 49, 398, DateTimeKind.Local).AddTicks(7698));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 11, 17, 23, 49, 398, DateTimeKind.Local).AddTicks(7702));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 11, 17, 23, 49, 398, DateTimeKind.Local).AddTicks(7706));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 11, 17, 23, 49, 398, DateTimeKind.Local).AddTicks(7709));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 11, 17, 23, 49, 398, DateTimeKind.Local).AddTicks(7712));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 11, 17, 23, 49, 398, DateTimeKind.Local).AddTicks(7714));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 11, 17, 23, 49, 398, DateTimeKind.Local).AddTicks(7716));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 11, 17, 23, 49, 398, DateTimeKind.Local).AddTicks(7718));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 11, 17, 23, 49, 398, DateTimeKind.Local).AddTicks(7721));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 11, 17, 23, 49, 398, DateTimeKind.Local).AddTicks(7723));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 11, 17, 23, 49, 398, DateTimeKind.Local).AddTicks(7724));

            migrationBuilder.UpdateData(
                table: "Komisyonlar",
                keyColumn: "KomisyonId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 11, 17, 23, 49, 395, DateTimeKind.Local).AddTicks(1368));

            migrationBuilder.UpdateData(
                table: "Komisyonlar",
                keyColumn: "KomisyonId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 11, 17, 23, 49, 395, DateTimeKind.Local).AddTicks(1371));

            migrationBuilder.UpdateData(
                table: "Komisyonlar",
                keyColumn: "KomisyonId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 11, 17, 23, 49, 395, DateTimeKind.Local).AddTicks(1395));

            migrationBuilder.UpdateData(
                table: "Komisyonlar",
                keyColumn: "KomisyonId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 11, 17, 23, 49, 395, DateTimeKind.Local).AddTicks(1395));

            migrationBuilder.UpdateData(
                table: "Komisyonlar",
                keyColumn: "KomisyonId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 11, 17, 23, 49, 395, DateTimeKind.Local).AddTicks(1396));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 11, 17, 23, 49, 395, DateTimeKind.Local).AddTicks(501));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 11, 17, 23, 49, 395, DateTimeKind.Local).AddTicks(505));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 11, 17, 23, 49, 395, DateTimeKind.Local).AddTicks(506));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 11, 17, 23, 49, 394, DateTimeKind.Local).AddTicks(9325));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 11, 17, 23, 49, 394, DateTimeKind.Local).AddTicks(9335));
        }
    }
}
