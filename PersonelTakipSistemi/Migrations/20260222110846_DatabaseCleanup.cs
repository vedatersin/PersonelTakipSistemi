using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonelTakipSistemi.Migrations
{
    /// <inheritdoc />
    public partial class DatabaseCleanup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Görevler Modülü Temizliği
            migrationBuilder.Sql("DELETE FROM GorevDurumGecmisleri");
            migrationBuilder.Sql("DELETE FROM GorevAtamaTeskilatlar");
            migrationBuilder.Sql("DELETE FROM GorevAtamaKoordinatorlukler");
            migrationBuilder.Sql("DELETE FROM GorevAtamaKomisyonlar");
            migrationBuilder.Sql("DELETE FROM GorevAtamaPersoneller");
            migrationBuilder.Sql("DELETE FROM Gorevler");

            // 2. Bildirim Modülü Temizliği
            migrationBuilder.Sql("DELETE FROM Bildirimler");
            migrationBuilder.Sql("DELETE FROM TopluBildirimler");
            migrationBuilder.Sql("DELETE FROM BildirimGonderenler");

            // 3. Personel Detayları ve Yetkiler
            migrationBuilder.Sql("DELETE FROM PersonelYazilimlar");
            migrationBuilder.Sql("DELETE FROM PersonelUzmanliklar");
            migrationBuilder.Sql("DELETE FROM PersonelGorevTurleri");
            migrationBuilder.Sql("DELETE FROM PersonelIsNitelikleri");
            migrationBuilder.Sql("DELETE FROM PersonelTeskilatlar");
            migrationBuilder.Sql("DELETE FROM PersonelKoordinatorlukler");
            migrationBuilder.Sql("DELETE FROM PersonelKomisyonlar");
            migrationBuilder.Sql("DELETE FROM PersonelKurumsalRolAtamalari");

            // 4. Personel Temizliği (Vedat Ersin Ceviz (12514374546) hariç)
            migrationBuilder.Sql("DELETE FROM Personeller WHERE TcKimlikNo != '12514374546'");

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 22, 14, 8, 46, 145, DateTimeKind.Local).AddTicks(237));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 22, 14, 8, 46, 145, DateTimeKind.Local).AddTicks(244));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 22, 14, 8, 46, 145, DateTimeKind.Local).AddTicks(245));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 22, 14, 8, 46, 145, DateTimeKind.Local).AddTicks(246));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 22, 14, 8, 46, 145, DateTimeKind.Local).AddTicks(3072));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 22, 14, 8, 46, 145, DateTimeKind.Local).AddTicks(3084));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 22, 14, 8, 46, 145, DateTimeKind.Local).AddTicks(3087));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 22, 14, 8, 46, 145, DateTimeKind.Local).AddTicks(3089));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 22, 14, 8, 46, 145, DateTimeKind.Local).AddTicks(3091));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 22, 14, 8, 46, 145, DateTimeKind.Local).AddTicks(3094));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 22, 14, 8, 46, 145, DateTimeKind.Local).AddTicks(3096));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 22, 14, 8, 46, 145, DateTimeKind.Local).AddTicks(3098));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 22, 14, 8, 46, 145, DateTimeKind.Local).AddTicks(3100));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 22, 14, 8, 46, 145, DateTimeKind.Local).AddTicks(3102));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 22, 14, 8, 46, 145, DateTimeKind.Local).AddTicks(3104));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 22, 14, 8, 46, 145, DateTimeKind.Local).AddTicks(3106));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 22, 14, 8, 46, 140, DateTimeKind.Local).AddTicks(9904));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 22, 14, 8, 46, 140, DateTimeKind.Local).AddTicks(9905));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 22, 14, 8, 46, 140, DateTimeKind.Local).AddTicks(9889));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 22, 14, 8, 46, 140, DateTimeKind.Local).AddTicks(9893));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 22, 14, 8, 46, 140, DateTimeKind.Local).AddTicks(9894));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 22, 14, 8, 46, 140, DateTimeKind.Local).AddTicks(9895));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 22, 14, 8, 46, 140, DateTimeKind.Local).AddTicks(9896));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 22, 14, 8, 46, 140, DateTimeKind.Local).AddTicks(9896));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 22, 14, 8, 46, 140, DateTimeKind.Local).AddTicks(9897));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 22, 14, 8, 46, 140, DateTimeKind.Local).AddTicks(9898));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 22, 14, 8, 46, 140, DateTimeKind.Local).AddTicks(9899));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 22, 14, 8, 46, 140, DateTimeKind.Local).AddTicks(9900));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 22, 14, 8, 46, 140, DateTimeKind.Local).AddTicks(9900));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 22, 14, 8, 46, 140, DateTimeKind.Local).AddTicks(9901));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 16,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 22, 14, 8, 46, 140, DateTimeKind.Local).AddTicks(9902));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 17,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 22, 14, 8, 46, 140, DateTimeKind.Local).AddTicks(9902));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 18,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 22, 14, 8, 46, 140, DateTimeKind.Local).AddTicks(9903));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 19,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 22, 14, 8, 46, 140, DateTimeKind.Local).AddTicks(9904));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 22, 14, 8, 46, 140, DateTimeKind.Local).AddTicks(8910));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 22, 14, 8, 46, 140, DateTimeKind.Local).AddTicks(8925));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 14, 23, 7, 498, DateTimeKind.Local).AddTicks(2445));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 14, 23, 7, 498, DateTimeKind.Local).AddTicks(2452));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 14, 23, 7, 498, DateTimeKind.Local).AddTicks(2453));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 14, 23, 7, 498, DateTimeKind.Local).AddTicks(2454));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 14, 23, 7, 498, DateTimeKind.Local).AddTicks(5168));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 14, 23, 7, 498, DateTimeKind.Local).AddTicks(5181));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 14, 23, 7, 498, DateTimeKind.Local).AddTicks(5226));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 14, 23, 7, 498, DateTimeKind.Local).AddTicks(5228));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 14, 23, 7, 498, DateTimeKind.Local).AddTicks(5230));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 14, 23, 7, 498, DateTimeKind.Local).AddTicks(5233));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 14, 23, 7, 498, DateTimeKind.Local).AddTicks(5235));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 14, 23, 7, 498, DateTimeKind.Local).AddTicks(5237));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 14, 23, 7, 498, DateTimeKind.Local).AddTicks(5239));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 14, 23, 7, 498, DateTimeKind.Local).AddTicks(5241));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 14, 23, 7, 498, DateTimeKind.Local).AddTicks(5243));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 14, 23, 7, 498, DateTimeKind.Local).AddTicks(5244));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 14, 23, 7, 493, DateTimeKind.Local).AddTicks(8244));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 14, 23, 7, 493, DateTimeKind.Local).AddTicks(8245));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 14, 23, 7, 493, DateTimeKind.Local).AddTicks(8214));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 14, 23, 7, 493, DateTimeKind.Local).AddTicks(8217));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 14, 23, 7, 493, DateTimeKind.Local).AddTicks(8218));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 14, 23, 7, 493, DateTimeKind.Local).AddTicks(8219));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 14, 23, 7, 493, DateTimeKind.Local).AddTicks(8220));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 14, 23, 7, 493, DateTimeKind.Local).AddTicks(8221));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 14, 23, 7, 493, DateTimeKind.Local).AddTicks(8221));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 14, 23, 7, 493, DateTimeKind.Local).AddTicks(8222));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 14, 23, 7, 493, DateTimeKind.Local).AddTicks(8223));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 14, 23, 7, 493, DateTimeKind.Local).AddTicks(8224));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 14, 23, 7, 493, DateTimeKind.Local).AddTicks(8225));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 14, 23, 7, 493, DateTimeKind.Local).AddTicks(8226));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 16,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 14, 23, 7, 493, DateTimeKind.Local).AddTicks(8227));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 17,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 14, 23, 7, 493, DateTimeKind.Local).AddTicks(8227));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 18,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 14, 23, 7, 493, DateTimeKind.Local).AddTicks(8242));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 19,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 14, 23, 7, 493, DateTimeKind.Local).AddTicks(8243));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 14, 23, 7, 493, DateTimeKind.Local).AddTicks(7210));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 14, 23, 7, 493, DateTimeKind.Local).AddTicks(7223));
        }
    }
}
