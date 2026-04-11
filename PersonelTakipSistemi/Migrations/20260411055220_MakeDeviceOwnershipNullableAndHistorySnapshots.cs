using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonelTakipSistemi.Migrations
{
    /// <inheritdoc />
    public partial class MakeDeviceOwnershipNullableAndHistorySnapshots : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CihazHareketleri_Personeller_IslemYapanPersonelId",
                table: "CihazHareketleri");

            migrationBuilder.DropForeignKey(
                name: "FK_Cihazlar_Personeller_SahipPersonelId",
                table: "Cihazlar");

            migrationBuilder.AlterColumn<int>(
                name: "SahipPersonelId",
                table: "Cihazlar",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "OlusturanPersonelId",
                table: "Cihazlar",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "IslemYapanPersonelId",
                table: "CihazHareketleri",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "IslemYapanAdSoyad",
                table: "CihazHareketleri",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OncekiSahipAdSoyad",
                table: "CihazHareketleri",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "YeniSahipAdSoyad",
                table: "CihazHareketleri",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 1001,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 916, DateTimeKind.Local).AddTicks(4243));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 1002,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 916, DateTimeKind.Local).AddTicks(4247));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 1003,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 916, DateTimeKind.Local).AddTicks(4248));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 2001,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 916, DateTimeKind.Local).AddTicks(4248));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 2002,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 916, DateTimeKind.Local).AddTicks(4249));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 2003,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 916, DateTimeKind.Local).AddTicks(4250));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 2004,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 916, DateTimeKind.Local).AddTicks(4251));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 3001,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 916, DateTimeKind.Local).AddTicks(4252));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 3002,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 916, DateTimeKind.Local).AddTicks(4252));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 3003,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 916, DateTimeKind.Local).AddTicks(4253));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 4001,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 916, DateTimeKind.Local).AddTicks(4254));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 4002,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 916, DateTimeKind.Local).AddTicks(4254));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 4003,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 916, DateTimeKind.Local).AddTicks(4255));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 5001,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 916, DateTimeKind.Local).AddTicks(4256));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 5002,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 916, DateTimeKind.Local).AddTicks(4257));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 5003,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 916, DateTimeKind.Local).AddTicks(4257));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 6001,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 916, DateTimeKind.Local).AddTicks(4258));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 6002,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 916, DateTimeKind.Local).AddTicks(4259));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 6003,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 916, DateTimeKind.Local).AddTicks(4259));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 7001,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 916, DateTimeKind.Local).AddTicks(4260));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 7002,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 916, DateTimeKind.Local).AddTicks(4261));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 7003,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 916, DateTimeKind.Local).AddTicks(4261));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 8001,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 916, DateTimeKind.Local).AddTicks(4262));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 8002,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 916, DateTimeKind.Local).AddTicks(4263));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 9001,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 916, DateTimeKind.Local).AddTicks(4263));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 9002,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 916, DateTimeKind.Local).AddTicks(4264));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 10001,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 916, DateTimeKind.Local).AddTicks(4265));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 10002,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 916, DateTimeKind.Local).AddTicks(4265));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 11001,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 916, DateTimeKind.Local).AddTicks(4266));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 11002,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 916, DateTimeKind.Local).AddTicks(4266));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 12001,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 916, DateTimeKind.Local).AddTicks(4267));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 12002,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 916, DateTimeKind.Local).AddTicks(4268));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 99999,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 916, DateTimeKind.Local).AddTicks(4268));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 916, DateTimeKind.Local).AddTicks(3145));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 916, DateTimeKind.Local).AddTicks(3154));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 916, DateTimeKind.Local).AddTicks(3155));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 916, DateTimeKind.Local).AddTicks(3156));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 916, DateTimeKind.Local).AddTicks(3157));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 916, DateTimeKind.Local).AddTicks(3157));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 916, DateTimeKind.Local).AddTicks(3158));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 916, DateTimeKind.Local).AddTicks(3159));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 916, DateTimeKind.Local).AddTicks(3160));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 916, DateTimeKind.Local).AddTicks(3161));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 916, DateTimeKind.Local).AddTicks(3162));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 916, DateTimeKind.Local).AddTicks(3162));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 999,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 916, DateTimeKind.Local).AddTicks(3163));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 915, DateTimeKind.Local).AddTicks(1718));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 915, DateTimeKind.Local).AddTicks(1742));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 915, DateTimeKind.Local).AddTicks(1745));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 915, DateTimeKind.Local).AddTicks(1747));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 915, DateTimeKind.Local).AddTicks(1749));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 915, DateTimeKind.Local).AddTicks(1751));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 915, DateTimeKind.Local).AddTicks(1753));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 915, DateTimeKind.Local).AddTicks(1755));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 915, DateTimeKind.Local).AddTicks(1757));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 915, DateTimeKind.Local).AddTicks(1759));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 915, DateTimeKind.Local).AddTicks(1761));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 915, DateTimeKind.Local).AddTicks(1763));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 906, DateTimeKind.Local).AddTicks(1575));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 906, DateTimeKind.Local).AddTicks(1576));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 906, DateTimeKind.Local).AddTicks(1550));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 906, DateTimeKind.Local).AddTicks(1557));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 906, DateTimeKind.Local).AddTicks(1558));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 906, DateTimeKind.Local).AddTicks(1560));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 906, DateTimeKind.Local).AddTicks(1561));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 906, DateTimeKind.Local).AddTicks(1562));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 906, DateTimeKind.Local).AddTicks(1563));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 906, DateTimeKind.Local).AddTicks(1565));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 906, DateTimeKind.Local).AddTicks(1566));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 906, DateTimeKind.Local).AddTicks(1567));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 906, DateTimeKind.Local).AddTicks(1568));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 906, DateTimeKind.Local).AddTicks(1569));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 16,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 906, DateTimeKind.Local).AddTicks(1570));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 17,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 906, DateTimeKind.Local).AddTicks(1571));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 18,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 906, DateTimeKind.Local).AddTicks(1572));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 19,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 906, DateTimeKind.Local).AddTicks(1574));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 906, DateTimeKind.Local).AddTicks(205));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 8, 52, 19, 906, DateTimeKind.Local).AddTicks(226));

            migrationBuilder.AddForeignKey(
                name: "FK_CihazHareketleri_Personeller_IslemYapanPersonelId",
                table: "CihazHareketleri",
                column: "IslemYapanPersonelId",
                principalTable: "Personeller",
                principalColumn: "PersonelId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Cihazlar_Personeller_SahipPersonelId",
                table: "Cihazlar",
                column: "SahipPersonelId",
                principalTable: "Personeller",
                principalColumn: "PersonelId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CihazHareketleri_Personeller_IslemYapanPersonelId",
                table: "CihazHareketleri");

            migrationBuilder.DropForeignKey(
                name: "FK_Cihazlar_Personeller_SahipPersonelId",
                table: "Cihazlar");

            migrationBuilder.DropColumn(
                name: "IslemYapanAdSoyad",
                table: "CihazHareketleri");

            migrationBuilder.DropColumn(
                name: "OncekiSahipAdSoyad",
                table: "CihazHareketleri");

            migrationBuilder.DropColumn(
                name: "YeniSahipAdSoyad",
                table: "CihazHareketleri");

            migrationBuilder.AlterColumn<int>(
                name: "SahipPersonelId",
                table: "Cihazlar",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OlusturanPersonelId",
                table: "Cihazlar",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IslemYapanPersonelId",
                table: "CihazHareketleri",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_CihazHareketleri_Personeller_IslemYapanPersonelId",
                table: "CihazHareketleri",
                column: "IslemYapanPersonelId",
                principalTable: "Personeller",
                principalColumn: "PersonelId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cihazlar_Personeller_SahipPersonelId",
                table: "Cihazlar",
                column: "SahipPersonelId",
                principalTable: "Personeller",
                principalColumn: "PersonelId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
