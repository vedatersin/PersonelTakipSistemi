using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonelTakipSistemi.Migrations
{
    /// <inheritdoc />
    public partial class AddMerkezBirimKoordinatorlugu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 10, 11, 36, 58, 458, DateTimeKind.Local).AddTicks(624));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 10, 11, 36, 58, 458, DateTimeKind.Local).AddTicks(630));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 10, 11, 36, 58, 458, DateTimeKind.Local).AddTicks(631));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 10, 11, 36, 58, 458, DateTimeKind.Local).AddTicks(632));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 10, 11, 36, 58, 458, DateTimeKind.Local).AddTicks(3745));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 10, 11, 36, 58, 458, DateTimeKind.Local).AddTicks(3758));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 10, 11, 36, 58, 458, DateTimeKind.Local).AddTicks(3761));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 10, 11, 36, 58, 458, DateTimeKind.Local).AddTicks(3763));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 10, 11, 36, 58, 458, DateTimeKind.Local).AddTicks(3767));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 10, 11, 36, 58, 458, DateTimeKind.Local).AddTicks(3770));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 10, 11, 36, 58, 458, DateTimeKind.Local).AddTicks(3772));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 10, 11, 36, 58, 458, DateTimeKind.Local).AddTicks(3774));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 10, 11, 36, 58, 458, DateTimeKind.Local).AddTicks(3776));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 10, 11, 36, 58, 458, DateTimeKind.Local).AddTicks(3779));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 10, 11, 36, 58, 458, DateTimeKind.Local).AddTicks(3781));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 10, 11, 36, 58, 458, DateTimeKind.Local).AddTicks(3783));

            migrationBuilder.UpdateData(
                table: "Komisyonlar",
                keyColumn: "KomisyonId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 10, 11, 36, 58, 454, DateTimeKind.Local).AddTicks(7667));

            migrationBuilder.UpdateData(
                table: "Komisyonlar",
                keyColumn: "KomisyonId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 10, 11, 36, 58, 454, DateTimeKind.Local).AddTicks(7670));

            migrationBuilder.UpdateData(
                table: "Komisyonlar",
                keyColumn: "KomisyonId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 10, 11, 36, 58, 454, DateTimeKind.Local).AddTicks(7671));

            migrationBuilder.UpdateData(
                table: "Komisyonlar",
                keyColumn: "KomisyonId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 10, 11, 36, 58, 454, DateTimeKind.Local).AddTicks(7672));

            migrationBuilder.UpdateData(
                table: "Komisyonlar",
                keyColumn: "KomisyonId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 10, 11, 36, 58, 454, DateTimeKind.Local).AddTicks(7673));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 10, 11, 36, 58, 454, DateTimeKind.Local).AddTicks(6664));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 10, 11, 36, 58, 454, DateTimeKind.Local).AddTicks(6669));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 10, 11, 36, 58, 454, DateTimeKind.Local).AddTicks(6670));

            migrationBuilder.InsertData(
                table: "KurumsalRoller",
                columns: new[] { "KurumsalRolId", "Ad" },
                values: new object[] { 5, "Merkez Birim Koordinatörlüğü" });

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 10, 11, 36, 58, 454, DateTimeKind.Local).AddTicks(5521));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 10, 11, 36, 58, 454, DateTimeKind.Local).AddTicks(5533));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "KurumsalRoller",
                keyColumn: "KurumsalRolId",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 6, 8, 4, 4, 255, DateTimeKind.Local).AddTicks(3856));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 6, 8, 4, 4, 255, DateTimeKind.Local).AddTicks(3865));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 6, 8, 4, 4, 255, DateTimeKind.Local).AddTicks(3867));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 6, 8, 4, 4, 255, DateTimeKind.Local).AddTicks(3868));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 6, 8, 4, 4, 255, DateTimeKind.Local).AddTicks(6922));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 6, 8, 4, 4, 255, DateTimeKind.Local).AddTicks(6934));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 6, 8, 4, 4, 255, DateTimeKind.Local).AddTicks(6937));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 6, 8, 4, 4, 255, DateTimeKind.Local).AddTicks(6941));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 6, 8, 4, 4, 255, DateTimeKind.Local).AddTicks(6946));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 6, 8, 4, 4, 255, DateTimeKind.Local).AddTicks(6949));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 6, 8, 4, 4, 255, DateTimeKind.Local).AddTicks(6971));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 6, 8, 4, 4, 255, DateTimeKind.Local).AddTicks(6973));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 6, 8, 4, 4, 255, DateTimeKind.Local).AddTicks(6975));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 6, 8, 4, 4, 255, DateTimeKind.Local).AddTicks(6979));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 6, 8, 4, 4, 255, DateTimeKind.Local).AddTicks(6981));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 6, 8, 4, 4, 255, DateTimeKind.Local).AddTicks(6984));

            migrationBuilder.UpdateData(
                table: "Komisyonlar",
                keyColumn: "KomisyonId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 6, 8, 4, 4, 252, DateTimeKind.Local).AddTicks(3332));

            migrationBuilder.UpdateData(
                table: "Komisyonlar",
                keyColumn: "KomisyonId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 6, 8, 4, 4, 252, DateTimeKind.Local).AddTicks(3336));

            migrationBuilder.UpdateData(
                table: "Komisyonlar",
                keyColumn: "KomisyonId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 6, 8, 4, 4, 252, DateTimeKind.Local).AddTicks(3337));

            migrationBuilder.UpdateData(
                table: "Komisyonlar",
                keyColumn: "KomisyonId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 6, 8, 4, 4, 252, DateTimeKind.Local).AddTicks(3338));

            migrationBuilder.UpdateData(
                table: "Komisyonlar",
                keyColumn: "KomisyonId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 6, 8, 4, 4, 252, DateTimeKind.Local).AddTicks(3339));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 6, 8, 4, 4, 252, DateTimeKind.Local).AddTicks(2260));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 6, 8, 4, 4, 252, DateTimeKind.Local).AddTicks(2266));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 6, 8, 4, 4, 252, DateTimeKind.Local).AddTicks(2267));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 6, 8, 4, 4, 252, DateTimeKind.Local).AddTicks(748));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 6, 8, 4, 4, 252, DateTimeKind.Local).AddTicks(761));
        }
    }
}
