using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonelTakipSistemi.Migrations
{
    /// <inheritdoc />
    public partial class MergeYazilimLisansKapasiteAlanlari : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HesapBasiOrtakKullanimBilgisi",
                table: "YazilimLisanslar",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.Sql(@"
UPDATE [YazilimLisanslar]
SET [HesapBasiOrtakKullanimBilgisi] =
    LTRIM(RTRIM(
        CONCAT(
            CASE WHEN [HesapBasiKisiSayisi] IS NOT NULL THEN CONCAT([HesapBasiKisiSayisi], ' kişi') ELSE '' END,
            CASE WHEN [HesapBasiKisiSayisi] IS NOT NULL AND [HesapBasiKrediMiktari] IS NOT NULL THEN ' / ' ELSE '' END,
            CASE WHEN [HesapBasiKrediMiktari] IS NOT NULL THEN CONCAT(CONVERT(nvarchar(50), [HesapBasiKrediMiktari]), ' kredi') ELSE '' END
        )
    ))
WHERE [HesapBasiKisiSayisi] IS NOT NULL OR [HesapBasiKrediMiktari] IS NOT NULL;
");

            migrationBuilder.DropColumn(
                name: "HesapBasiKisiSayisi",
                table: "YazilimLisanslar");

            migrationBuilder.DropColumn(
                name: "HesapBasiKrediMiktari",
                table: "YazilimLisanslar");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HesapBasiOrtakKullanimBilgisi",
                table: "YazilimLisanslar");

            migrationBuilder.AddColumn<int>(
                name: "HesapBasiKisiSayisi",
                table: "YazilimLisanslar",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "HesapBasiKrediMiktari",
                table: "YazilimLisanslar",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 1001,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 972, DateTimeKind.Local).AddTicks(5194));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 1002,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 972, DateTimeKind.Local).AddTicks(5197));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 1003,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 972, DateTimeKind.Local).AddTicks(5198));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 2001,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 972, DateTimeKind.Local).AddTicks(5199));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 2002,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 972, DateTimeKind.Local).AddTicks(5199));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 2003,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 972, DateTimeKind.Local).AddTicks(5200));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 2004,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 972, DateTimeKind.Local).AddTicks(5201));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 3001,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 972, DateTimeKind.Local).AddTicks(5202));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 3002,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 972, DateTimeKind.Local).AddTicks(5203));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 3003,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 972, DateTimeKind.Local).AddTicks(5203));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 4001,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 972, DateTimeKind.Local).AddTicks(5204));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 4002,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 972, DateTimeKind.Local).AddTicks(5205));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 4003,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 972, DateTimeKind.Local).AddTicks(5205));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 5001,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 972, DateTimeKind.Local).AddTicks(5206));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 5002,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 972, DateTimeKind.Local).AddTicks(5207));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 5003,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 972, DateTimeKind.Local).AddTicks(5207));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 6001,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 972, DateTimeKind.Local).AddTicks(5208));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 6002,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 972, DateTimeKind.Local).AddTicks(5209));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 6003,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 972, DateTimeKind.Local).AddTicks(5209));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 7001,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 972, DateTimeKind.Local).AddTicks(5210));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 7002,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 972, DateTimeKind.Local).AddTicks(5211));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 7003,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 972, DateTimeKind.Local).AddTicks(5211));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 8001,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 972, DateTimeKind.Local).AddTicks(5212));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 8002,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 972, DateTimeKind.Local).AddTicks(5213));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 9001,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 972, DateTimeKind.Local).AddTicks(5213));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 9002,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 972, DateTimeKind.Local).AddTicks(5214));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 10001,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 972, DateTimeKind.Local).AddTicks(5215));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 10002,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 972, DateTimeKind.Local).AddTicks(5215));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 11001,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 972, DateTimeKind.Local).AddTicks(5216));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 11002,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 972, DateTimeKind.Local).AddTicks(5217));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 12001,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 972, DateTimeKind.Local).AddTicks(5217));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 12002,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 972, DateTimeKind.Local).AddTicks(5218));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 99999,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 972, DateTimeKind.Local).AddTicks(5219));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 972, DateTimeKind.Local).AddTicks(3792));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 972, DateTimeKind.Local).AddTicks(3800));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 972, DateTimeKind.Local).AddTicks(3801));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 972, DateTimeKind.Local).AddTicks(3820));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 972, DateTimeKind.Local).AddTicks(3821));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 972, DateTimeKind.Local).AddTicks(3821));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 972, DateTimeKind.Local).AddTicks(3822));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 972, DateTimeKind.Local).AddTicks(3823));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 972, DateTimeKind.Local).AddTicks(3824));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 972, DateTimeKind.Local).AddTicks(3825));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 972, DateTimeKind.Local).AddTicks(3825));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 972, DateTimeKind.Local).AddTicks(3826));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 999,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 972, DateTimeKind.Local).AddTicks(3827));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 971, DateTimeKind.Local).AddTicks(1106));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 971, DateTimeKind.Local).AddTicks(1129));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 971, DateTimeKind.Local).AddTicks(1131));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 971, DateTimeKind.Local).AddTicks(1133));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 971, DateTimeKind.Local).AddTicks(1135));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 971, DateTimeKind.Local).AddTicks(1137));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 971, DateTimeKind.Local).AddTicks(1139));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 971, DateTimeKind.Local).AddTicks(1141));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 971, DateTimeKind.Local).AddTicks(1143));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 971, DateTimeKind.Local).AddTicks(1145));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 971, DateTimeKind.Local).AddTicks(1147));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 971, DateTimeKind.Local).AddTicks(1149));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 962, DateTimeKind.Local).AddTicks(3107));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 962, DateTimeKind.Local).AddTicks(3108));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 962, DateTimeKind.Local).AddTicks(3089));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 962, DateTimeKind.Local).AddTicks(3093));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 962, DateTimeKind.Local).AddTicks(3095));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 962, DateTimeKind.Local).AddTicks(3096));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 962, DateTimeKind.Local).AddTicks(3097));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 962, DateTimeKind.Local).AddTicks(3098));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 962, DateTimeKind.Local).AddTicks(3099));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 962, DateTimeKind.Local).AddTicks(3100));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 962, DateTimeKind.Local).AddTicks(3100));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 962, DateTimeKind.Local).AddTicks(3101));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 962, DateTimeKind.Local).AddTicks(3102));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 962, DateTimeKind.Local).AddTicks(3103));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 16,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 962, DateTimeKind.Local).AddTicks(3104));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 17,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 962, DateTimeKind.Local).AddTicks(3105));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 18,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 962, DateTimeKind.Local).AddTicks(3106));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 19,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 962, DateTimeKind.Local).AddTicks(3106));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 962, DateTimeKind.Local).AddTicks(1660));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 7, 46, 962, DateTimeKind.Local).AddTicks(1676));
        }
    }
}
