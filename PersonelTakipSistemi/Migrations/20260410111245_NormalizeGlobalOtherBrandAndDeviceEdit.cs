using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PersonelTakipSistemi.Migrations
{
    /// <inheritdoc />
    public partial class NormalizeGlobalOtherBrandAndDeviceEdit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                UPDATE C
                SET C.CihazMarkaId = 99999
                FROM Cihazlar AS C
                WHERE C.CihazMarkaId IN (1099, 2099, 3099, 4099, 5099, 6099, 7099, 8099, 9099, 10099, 11099, 12099);
                """);

            migrationBuilder.DeleteData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 1099);

            migrationBuilder.DeleteData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 2099);

            migrationBuilder.DeleteData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 3099);

            migrationBuilder.DeleteData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 4099);

            migrationBuilder.DeleteData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 5099);

            migrationBuilder.DeleteData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 6099);

            migrationBuilder.DeleteData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 7099);

            migrationBuilder.DeleteData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 8099);

            migrationBuilder.DeleteData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 9099);

            migrationBuilder.DeleteData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 10099);

            migrationBuilder.DeleteData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 11099);

            migrationBuilder.DeleteData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 12099);

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 1001,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 963, DateTimeKind.Local).AddTicks(1082));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 1002,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 963, DateTimeKind.Local).AddTicks(1086));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 1003,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 963, DateTimeKind.Local).AddTicks(1087));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 2001,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 963, DateTimeKind.Local).AddTicks(1088));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 2002,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 963, DateTimeKind.Local).AddTicks(1089));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 2003,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 963, DateTimeKind.Local).AddTicks(1090));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 2004,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 963, DateTimeKind.Local).AddTicks(1091));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 3001,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 963, DateTimeKind.Local).AddTicks(1091));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 3002,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 963, DateTimeKind.Local).AddTicks(1092));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 3003,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 963, DateTimeKind.Local).AddTicks(1093));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 4001,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 963, DateTimeKind.Local).AddTicks(1094));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 4002,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 963, DateTimeKind.Local).AddTicks(1094));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 4003,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 963, DateTimeKind.Local).AddTicks(1095));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 5001,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 963, DateTimeKind.Local).AddTicks(1096));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 5002,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 963, DateTimeKind.Local).AddTicks(1096));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 5003,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 963, DateTimeKind.Local).AddTicks(1097));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 6001,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 963, DateTimeKind.Local).AddTicks(1098));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 6002,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 963, DateTimeKind.Local).AddTicks(1098));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 6003,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 963, DateTimeKind.Local).AddTicks(1099));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 7001,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 963, DateTimeKind.Local).AddTicks(1100));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 7002,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 963, DateTimeKind.Local).AddTicks(1100));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 7003,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 963, DateTimeKind.Local).AddTicks(1101));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 8001,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 963, DateTimeKind.Local).AddTicks(1102));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 8002,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 963, DateTimeKind.Local).AddTicks(1102));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 9001,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 963, DateTimeKind.Local).AddTicks(1103));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 9002,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 963, DateTimeKind.Local).AddTicks(1104));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 10001,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 963, DateTimeKind.Local).AddTicks(1104));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 10002,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 963, DateTimeKind.Local).AddTicks(1105));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 11001,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 963, DateTimeKind.Local).AddTicks(1106));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 11002,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 963, DateTimeKind.Local).AddTicks(1106));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 12001,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 963, DateTimeKind.Local).AddTicks(1107));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 12002,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 963, DateTimeKind.Local).AddTicks(1108));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 99999,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 963, DateTimeKind.Local).AddTicks(1108));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 962, DateTimeKind.Local).AddTicks(9714));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 962, DateTimeKind.Local).AddTicks(9728));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 962, DateTimeKind.Local).AddTicks(9729));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 962, DateTimeKind.Local).AddTicks(9730));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 962, DateTimeKind.Local).AddTicks(9730));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 962, DateTimeKind.Local).AddTicks(9731));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 962, DateTimeKind.Local).AddTicks(9732));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 962, DateTimeKind.Local).AddTicks(9733));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 962, DateTimeKind.Local).AddTicks(9734));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 962, DateTimeKind.Local).AddTicks(9734));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 962, DateTimeKind.Local).AddTicks(9760));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 962, DateTimeKind.Local).AddTicks(9761));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 999,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 962, DateTimeKind.Local).AddTicks(9762));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 961, DateTimeKind.Local).AddTicks(2981));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 961, DateTimeKind.Local).AddTicks(3007));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 961, DateTimeKind.Local).AddTicks(3010));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 961, DateTimeKind.Local).AddTicks(3012));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 961, DateTimeKind.Local).AddTicks(3014));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 961, DateTimeKind.Local).AddTicks(3018));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 961, DateTimeKind.Local).AddTicks(3020));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 961, DateTimeKind.Local).AddTicks(3022));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 961, DateTimeKind.Local).AddTicks(3024));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 961, DateTimeKind.Local).AddTicks(3027));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 961, DateTimeKind.Local).AddTicks(3029));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 961, DateTimeKind.Local).AddTicks(3031));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 951, DateTimeKind.Local).AddTicks(7889));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 951, DateTimeKind.Local).AddTicks(7907));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 951, DateTimeKind.Local).AddTicks(7865));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 951, DateTimeKind.Local).AddTicks(7871));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 951, DateTimeKind.Local).AddTicks(7872));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 951, DateTimeKind.Local).AddTicks(7874));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 951, DateTimeKind.Local).AddTicks(7875));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 951, DateTimeKind.Local).AddTicks(7876));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 951, DateTimeKind.Local).AddTicks(7877));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 951, DateTimeKind.Local).AddTicks(7879));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 951, DateTimeKind.Local).AddTicks(7880));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 951, DateTimeKind.Local).AddTicks(7882));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 951, DateTimeKind.Local).AddTicks(7883));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 951, DateTimeKind.Local).AddTicks(7884));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 16,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 951, DateTimeKind.Local).AddTicks(7885));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 17,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 951, DateTimeKind.Local).AddTicks(7885));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 18,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 951, DateTimeKind.Local).AddTicks(7887));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 19,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 951, DateTimeKind.Local).AddTicks(7888));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 951, DateTimeKind.Local).AddTicks(6712));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 14, 12, 44, 951, DateTimeKind.Local).AddTicks(6725));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 1001,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7209));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 1002,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7213));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 1003,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7215));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 2001,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7217));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 2002,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7217));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 2003,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7218));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 2004,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7219));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 3001,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7221));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 3002,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7222));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 3003,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7223));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 4001,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7224));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 4002,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7225));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 4003,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7226));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 5001,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7227));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 5002,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7228));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 5003,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7228));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 6001,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7230));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 6002,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7231));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 6003,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7231));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 7001,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7233));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 7002,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7233));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 7003,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7234));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 8001,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7236));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 8002,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7236));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 9001,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7238));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 9002,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7238));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 10001,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7240));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 10002,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7241));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 11001,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7242));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 11002,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7243));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 12001,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7244));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 12002,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7245));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 99999,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7246));

            migrationBuilder.InsertData(
                table: "CihazMarkalari",
                columns: new[] { "CihazMarkaId", "Ad", "CihazTuruId", "CreatedAt", "IsActive", "SistemSecenegiMi" },
                values: new object[,]
                {
                    { 1099, "Diğer", 1, new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7215), true, true },
                    { 2099, "Diğer", 2, new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7220), true, true },
                    { 3099, "Diğer", 3, new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7223), true, true },
                    { 4099, "Diğer", 4, new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7226), true, true },
                    { 5099, "Diğer", 5, new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7229), true, true },
                    { 6099, "Diğer", 6, new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7232), true, true },
                    { 7099, "Diğer", 7, new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7235), true, true },
                    { 8099, "Diğer", 8, new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7237), true, true },
                    { 9099, "Diğer", 9, new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7239), true, true },
                    { 10099, "Diğer", 10, new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7241), true, true },
                    { 11099, "Diğer", 11, new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7243), true, true },
                    { 12099, "Diğer", 12, new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7246), true, true }
                });

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(5783));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(5792));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(5793));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(5794));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(5896));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(5897));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(5898));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(5899));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(5900));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(5901));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(5902));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(5902));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 999,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(5903));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 815, DateTimeKind.Local).AddTicks(2296));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 815, DateTimeKind.Local).AddTicks(2319));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 815, DateTimeKind.Local).AddTicks(2322));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 815, DateTimeKind.Local).AddTicks(2324));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 815, DateTimeKind.Local).AddTicks(2327));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 815, DateTimeKind.Local).AddTicks(2329));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 815, DateTimeKind.Local).AddTicks(2332));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 815, DateTimeKind.Local).AddTicks(2333));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 815, DateTimeKind.Local).AddTicks(2335));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 815, DateTimeKind.Local).AddTicks(2338));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 815, DateTimeKind.Local).AddTicks(2340));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 815, DateTimeKind.Local).AddTicks(2342));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 805, DateTimeKind.Local).AddTicks(5060));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 805, DateTimeKind.Local).AddTicks(5061));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 805, DateTimeKind.Local).AddTicks(4991));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 805, DateTimeKind.Local).AddTicks(4996));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 805, DateTimeKind.Local).AddTicks(4997));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 805, DateTimeKind.Local).AddTicks(4998));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 805, DateTimeKind.Local).AddTicks(4999));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 805, DateTimeKind.Local).AddTicks(5000));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 805, DateTimeKind.Local).AddTicks(5001));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 805, DateTimeKind.Local).AddTicks(5002));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 805, DateTimeKind.Local).AddTicks(5003));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 805, DateTimeKind.Local).AddTicks(5004));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 805, DateTimeKind.Local).AddTicks(5005));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 805, DateTimeKind.Local).AddTicks(5005));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 16,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 805, DateTimeKind.Local).AddTicks(5006));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 17,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 805, DateTimeKind.Local).AddTicks(5007));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 18,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 805, DateTimeKind.Local).AddTicks(5058));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 19,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 805, DateTimeKind.Local).AddTicks(5059));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 805, DateTimeKind.Local).AddTicks(3791));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 12, 24, 52, 805, DateTimeKind.Local).AddTicks(3805));
        }
    }
}
