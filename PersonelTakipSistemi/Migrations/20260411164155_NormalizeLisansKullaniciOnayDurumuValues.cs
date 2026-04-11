using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonelTakipSistemi.Migrations
{
    /// <inheritdoc />
    public partial class NormalizeLisansKullaniciOnayDurumuValues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                UPDATE YazilimLisansKullanicilar
                SET OnayDurumu = 3
                WHERE OnayDurumu = 2;

                UPDATE YazilimLisansKullanicilar
                SET OnayDurumu = 2
                WHERE OnayDurumu = 1;
            ");

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 1001,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 981, DateTimeKind.Local).AddTicks(1228));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 1002,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 981, DateTimeKind.Local).AddTicks(1231));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 1003,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 981, DateTimeKind.Local).AddTicks(1232));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 2001,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 981, DateTimeKind.Local).AddTicks(1233));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 2002,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 981, DateTimeKind.Local).AddTicks(1233));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 2003,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 981, DateTimeKind.Local).AddTicks(1234));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 2004,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 981, DateTimeKind.Local).AddTicks(1235));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 3001,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 981, DateTimeKind.Local).AddTicks(1236));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 3002,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 981, DateTimeKind.Local).AddTicks(1236));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 3003,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 981, DateTimeKind.Local).AddTicks(1237));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 4001,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 981, DateTimeKind.Local).AddTicks(1238));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 4002,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 981, DateTimeKind.Local).AddTicks(1238));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 4003,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 981, DateTimeKind.Local).AddTicks(1239));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 5001,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 981, DateTimeKind.Local).AddTicks(1240));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 5002,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 981, DateTimeKind.Local).AddTicks(1240));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 5003,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 981, DateTimeKind.Local).AddTicks(1241));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 6001,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 981, DateTimeKind.Local).AddTicks(1242));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 6002,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 981, DateTimeKind.Local).AddTicks(1242));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 6003,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 981, DateTimeKind.Local).AddTicks(1243));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 7001,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 981, DateTimeKind.Local).AddTicks(1244));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 7002,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 981, DateTimeKind.Local).AddTicks(1244));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 7003,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 981, DateTimeKind.Local).AddTicks(1245));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 8001,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 981, DateTimeKind.Local).AddTicks(1246));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 8002,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 981, DateTimeKind.Local).AddTicks(1246));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 9001,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 981, DateTimeKind.Local).AddTicks(1247));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 9002,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 981, DateTimeKind.Local).AddTicks(1248));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 10001,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 981, DateTimeKind.Local).AddTicks(1248));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 10002,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 981, DateTimeKind.Local).AddTicks(1249));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 11001,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 981, DateTimeKind.Local).AddTicks(1250));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 11002,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 981, DateTimeKind.Local).AddTicks(1251));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 12001,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 981, DateTimeKind.Local).AddTicks(1251));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 12002,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 981, DateTimeKind.Local).AddTicks(1252));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 99999,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 981, DateTimeKind.Local).AddTicks(1253));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 980, DateTimeKind.Local).AddTicks(9949));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 980, DateTimeKind.Local).AddTicks(9959));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 980, DateTimeKind.Local).AddTicks(9961));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 980, DateTimeKind.Local).AddTicks(9962));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 980, DateTimeKind.Local).AddTicks(9963));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 980, DateTimeKind.Local).AddTicks(9963));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 980, DateTimeKind.Local).AddTicks(9964));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 980, DateTimeKind.Local).AddTicks(9965));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 980, DateTimeKind.Local).AddTicks(9966));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 980, DateTimeKind.Local).AddTicks(9967));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 980, DateTimeKind.Local).AddTicks(9968));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 980, DateTimeKind.Local).AddTicks(9968));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 999,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 980, DateTimeKind.Local).AddTicks(9969));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 979, DateTimeKind.Local).AddTicks(6839));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 979, DateTimeKind.Local).AddTicks(6863));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 979, DateTimeKind.Local).AddTicks(6866));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 979, DateTimeKind.Local).AddTicks(6868));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 979, DateTimeKind.Local).AddTicks(6870));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 979, DateTimeKind.Local).AddTicks(6875));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 979, DateTimeKind.Local).AddTicks(6877));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 979, DateTimeKind.Local).AddTicks(6879));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 979, DateTimeKind.Local).AddTicks(6881));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 979, DateTimeKind.Local).AddTicks(6884));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 979, DateTimeKind.Local).AddTicks(6885));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 979, DateTimeKind.Local).AddTicks(6904));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 970, DateTimeKind.Local).AddTicks(2417));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 970, DateTimeKind.Local).AddTicks(2418));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 970, DateTimeKind.Local).AddTicks(2378));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 970, DateTimeKind.Local).AddTicks(2381));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 970, DateTimeKind.Local).AddTicks(2382));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 970, DateTimeKind.Local).AddTicks(2383));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 970, DateTimeKind.Local).AddTicks(2384));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 970, DateTimeKind.Local).AddTicks(2385));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 970, DateTimeKind.Local).AddTicks(2386));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 970, DateTimeKind.Local).AddTicks(2387));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 970, DateTimeKind.Local).AddTicks(2388));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 970, DateTimeKind.Local).AddTicks(2388));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 970, DateTimeKind.Local).AddTicks(2389));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 970, DateTimeKind.Local).AddTicks(2413));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 16,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 970, DateTimeKind.Local).AddTicks(2414));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 17,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 970, DateTimeKind.Local).AddTicks(2415));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 18,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 970, DateTimeKind.Local).AddTicks(2416));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 19,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 970, DateTimeKind.Local).AddTicks(2417));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 970, DateTimeKind.Local).AddTicks(1246));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 19, 41, 54, 970, DateTimeKind.Local).AddTicks(1262));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                UPDATE YazilimLisansKullanicilar
                SET OnayDurumu = 2
                WHERE OnayDurumu = 3;

                UPDATE YazilimLisansKullanicilar
                SET OnayDurumu = 1
                WHERE OnayDurumu = 2;
            ");

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 1001,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 583, DateTimeKind.Local).AddTicks(1243));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 1002,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 583, DateTimeKind.Local).AddTicks(1251));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 1003,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 583, DateTimeKind.Local).AddTicks(1253));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 2001,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 583, DateTimeKind.Local).AddTicks(1254));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 2002,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 583, DateTimeKind.Local).AddTicks(1255));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 2003,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 583, DateTimeKind.Local).AddTicks(1256));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 2004,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 583, DateTimeKind.Local).AddTicks(1257));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 3001,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 583, DateTimeKind.Local).AddTicks(1258));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 3002,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 583, DateTimeKind.Local).AddTicks(1259));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 3003,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 583, DateTimeKind.Local).AddTicks(1260));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 4001,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 583, DateTimeKind.Local).AddTicks(1261));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 4002,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 583, DateTimeKind.Local).AddTicks(1261));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 4003,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 583, DateTimeKind.Local).AddTicks(1262));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 5001,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 583, DateTimeKind.Local).AddTicks(1263));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 5002,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 583, DateTimeKind.Local).AddTicks(1264));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 5003,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 583, DateTimeKind.Local).AddTicks(1265));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 6001,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 583, DateTimeKind.Local).AddTicks(1266));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 6002,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 583, DateTimeKind.Local).AddTicks(1267));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 6003,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 583, DateTimeKind.Local).AddTicks(1268));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 7001,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 583, DateTimeKind.Local).AddTicks(1269));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 7002,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 583, DateTimeKind.Local).AddTicks(1270));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 7003,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 583, DateTimeKind.Local).AddTicks(1270));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 8001,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 583, DateTimeKind.Local).AddTicks(1271));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 8002,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 583, DateTimeKind.Local).AddTicks(1272));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 9001,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 583, DateTimeKind.Local).AddTicks(1273));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 9002,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 583, DateTimeKind.Local).AddTicks(1274));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 10001,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 583, DateTimeKind.Local).AddTicks(1275));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 10002,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 583, DateTimeKind.Local).AddTicks(1276));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 11001,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 583, DateTimeKind.Local).AddTicks(1277));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 11002,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 583, DateTimeKind.Local).AddTicks(1278));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 12001,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 583, DateTimeKind.Local).AddTicks(1279));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 12002,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 583, DateTimeKind.Local).AddTicks(1279));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 99999,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 583, DateTimeKind.Local).AddTicks(1280));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 582, DateTimeKind.Local).AddTicks(9446));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 582, DateTimeKind.Local).AddTicks(9461));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 582, DateTimeKind.Local).AddTicks(9462));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 582, DateTimeKind.Local).AddTicks(9463));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 582, DateTimeKind.Local).AddTicks(9464));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 582, DateTimeKind.Local).AddTicks(9466));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 582, DateTimeKind.Local).AddTicks(9467));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 582, DateTimeKind.Local).AddTicks(9468));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 582, DateTimeKind.Local).AddTicks(9469));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 582, DateTimeKind.Local).AddTicks(9470));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 582, DateTimeKind.Local).AddTicks(9471));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 582, DateTimeKind.Local).AddTicks(9472));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 999,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 582, DateTimeKind.Local).AddTicks(9473));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 581, DateTimeKind.Local).AddTicks(1503));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 581, DateTimeKind.Local).AddTicks(1532));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 581, DateTimeKind.Local).AddTicks(1535));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 581, DateTimeKind.Local).AddTicks(1537));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 581, DateTimeKind.Local).AddTicks(1540));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 581, DateTimeKind.Local).AddTicks(1543));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 581, DateTimeKind.Local).AddTicks(1545));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 581, DateTimeKind.Local).AddTicks(1546));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 581, DateTimeKind.Local).AddTicks(1549));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 581, DateTimeKind.Local).AddTicks(1552));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 581, DateTimeKind.Local).AddTicks(1554));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 581, DateTimeKind.Local).AddTicks(1555));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 570, DateTimeKind.Local).AddTicks(1040));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 570, DateTimeKind.Local).AddTicks(1041));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 570, DateTimeKind.Local).AddTicks(1019));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 570, DateTimeKind.Local).AddTicks(1024));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 570, DateTimeKind.Local).AddTicks(1025));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 570, DateTimeKind.Local).AddTicks(1027));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 570, DateTimeKind.Local).AddTicks(1028));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 570, DateTimeKind.Local).AddTicks(1029));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 570, DateTimeKind.Local).AddTicks(1030));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 570, DateTimeKind.Local).AddTicks(1031));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 570, DateTimeKind.Local).AddTicks(1032));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 570, DateTimeKind.Local).AddTicks(1033));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 570, DateTimeKind.Local).AddTicks(1034));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 570, DateTimeKind.Local).AddTicks(1035));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 16,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 570, DateTimeKind.Local).AddTicks(1036));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 17,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 570, DateTimeKind.Local).AddTicks(1037));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 18,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 570, DateTimeKind.Local).AddTicks(1038));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 19,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 570, DateTimeKind.Local).AddTicks(1039));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 569, DateTimeKind.Local).AddTicks(9680));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 56, 39, 569, DateTimeKind.Local).AddTicks(9696));
        }
    }
}
