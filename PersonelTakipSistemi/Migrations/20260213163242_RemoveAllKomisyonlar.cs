using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PersonelTakipSistemi.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAllKomisyonlar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Delete all Komisyon data and related dependencies
            migrationBuilder.Sql("DELETE FROM PersonelKomisyonlar");
            migrationBuilder.Sql("DELETE FROM PersonelKurumsalRolAtamalari WHERE KomisyonId IS NOT NULL");
            migrationBuilder.Sql("DELETE FROM GorevAtamaKomisyonlar"); // Just in case
            migrationBuilder.Sql("DELETE FROM Komisyonlar");

            migrationBuilder.DeleteData(
                table: "Komisyonlar",
                keyColumn: "KomisyonId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Komisyonlar",
                keyColumn: "KomisyonId",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 19, 32, 41, 930, DateTimeKind.Local).AddTicks(4868));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 19, 32, 41, 930, DateTimeKind.Local).AddTicks(4882));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 19, 32, 41, 930, DateTimeKind.Local).AddTicks(4883));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 19, 32, 41, 930, DateTimeKind.Local).AddTicks(4884));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 19, 32, 41, 930, DateTimeKind.Local).AddTicks(8399));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 19, 32, 41, 930, DateTimeKind.Local).AddTicks(8419));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 19, 32, 41, 930, DateTimeKind.Local).AddTicks(8423));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 19, 32, 41, 930, DateTimeKind.Local).AddTicks(8425));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 19, 32, 41, 930, DateTimeKind.Local).AddTicks(8428));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 19, 32, 41, 930, DateTimeKind.Local).AddTicks(8431));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 19, 32, 41, 930, DateTimeKind.Local).AddTicks(8433));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 19, 32, 41, 930, DateTimeKind.Local).AddTicks(8435));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 19, 32, 41, 930, DateTimeKind.Local).AddTicks(8437));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 19, 32, 41, 930, DateTimeKind.Local).AddTicks(8441));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 19, 32, 41, 930, DateTimeKind.Local).AddTicks(8442));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 19, 32, 41, 930, DateTimeKind.Local).AddTicks(8444));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 19, 32, 41, 926, DateTimeKind.Local).AddTicks(4410));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 19, 32, 41, 926, DateTimeKind.Local).AddTicks(4411));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 19, 32, 41, 926, DateTimeKind.Local).AddTicks(4288));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 19, 32, 41, 926, DateTimeKind.Local).AddTicks(4293));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 19, 32, 41, 926, DateTimeKind.Local).AddTicks(4294));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 19, 32, 41, 926, DateTimeKind.Local).AddTicks(4398));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 19, 32, 41, 926, DateTimeKind.Local).AddTicks(4400));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 19, 32, 41, 926, DateTimeKind.Local).AddTicks(4401));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 19, 32, 41, 926, DateTimeKind.Local).AddTicks(4402));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 19, 32, 41, 926, DateTimeKind.Local).AddTicks(4403));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 19, 32, 41, 926, DateTimeKind.Local).AddTicks(4404));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 19, 32, 41, 926, DateTimeKind.Local).AddTicks(4405));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 19, 32, 41, 926, DateTimeKind.Local).AddTicks(4406));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 19, 32, 41, 926, DateTimeKind.Local).AddTicks(4406));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 16,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 19, 32, 41, 926, DateTimeKind.Local).AddTicks(4407));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 17,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 19, 32, 41, 926, DateTimeKind.Local).AddTicks(4408));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 18,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 19, 32, 41, 926, DateTimeKind.Local).AddTicks(4409));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 19,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 19, 32, 41, 926, DateTimeKind.Local).AddTicks(4410));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 19, 32, 41, 926, DateTimeKind.Local).AddTicks(2675));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 19, 32, 41, 926, DateTimeKind.Local).AddTicks(2720));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 916, DateTimeKind.Local).AddTicks(4503));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 916, DateTimeKind.Local).AddTicks(4515));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 916, DateTimeKind.Local).AddTicks(4517));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 916, DateTimeKind.Local).AddTicks(4518));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 916, DateTimeKind.Local).AddTicks(7857));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 916, DateTimeKind.Local).AddTicks(7873));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 916, DateTimeKind.Local).AddTicks(7876));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 916, DateTimeKind.Local).AddTicks(7878));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 916, DateTimeKind.Local).AddTicks(7881));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 916, DateTimeKind.Local).AddTicks(7884));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 916, DateTimeKind.Local).AddTicks(7888));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 916, DateTimeKind.Local).AddTicks(7890));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 916, DateTimeKind.Local).AddTicks(7892));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 916, DateTimeKind.Local).AddTicks(7895));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 916, DateTimeKind.Local).AddTicks(7896));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 916, DateTimeKind.Local).AddTicks(7899));

            migrationBuilder.InsertData(
                table: "Komisyonlar",
                columns: new[] { "KomisyonId", "Aciklama", "Ad", "BaskanPersonelId", "CreatedAt", "IsActive", "KoordinatorlukId" },
                values: new object[,]
                {
                    { 4, null, "Türkçe Komisyonu", null, new DateTime(2026, 2, 13, 18, 24, 18, 912, DateTimeKind.Local).AddTicks(9918), true, 2 },
                    { 5, null, "Matematik Komisyonu", null, new DateTime(2026, 2, 13, 18, 24, 18, 912, DateTimeKind.Local).AddTicks(9924), true, 2 }
                });

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 912, DateTimeKind.Local).AddTicks(8389));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 912, DateTimeKind.Local).AddTicks(8391));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 912, DateTimeKind.Local).AddTicks(8335));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 912, DateTimeKind.Local).AddTicks(8341));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 912, DateTimeKind.Local).AddTicks(8343));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 912, DateTimeKind.Local).AddTicks(8344));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 912, DateTimeKind.Local).AddTicks(8346));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 912, DateTimeKind.Local).AddTicks(8347));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 912, DateTimeKind.Local).AddTicks(8348));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 912, DateTimeKind.Local).AddTicks(8350));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 912, DateTimeKind.Local).AddTicks(8351));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 912, DateTimeKind.Local).AddTicks(8352));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 912, DateTimeKind.Local).AddTicks(8354));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 912, DateTimeKind.Local).AddTicks(8355));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 16,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 912, DateTimeKind.Local).AddTicks(8356));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 17,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 912, DateTimeKind.Local).AddTicks(8357));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 18,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 912, DateTimeKind.Local).AddTicks(8358));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 19,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 912, DateTimeKind.Local).AddTicks(8359));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 912, DateTimeKind.Local).AddTicks(6480));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 912, DateTimeKind.Local).AddTicks(6496));
        }
    }
}
