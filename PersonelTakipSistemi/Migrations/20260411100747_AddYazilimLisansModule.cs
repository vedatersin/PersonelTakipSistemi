using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonelTakipSistemi.Migrations
{
    /// <inheritdoc />
    public partial class AddYazilimLisansModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "YazilimLisanslar",
                columns: table => new
                {
                    YazilimLisansId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YazilimId = table.Column<int>(type: "int", nullable: false),
                    MaksimumLisansHesapAdedi = table.Column<int>(type: "int", nullable: false),
                    LisansSuresiTuru = table.Column<int>(type: "int", nullable: false),
                    BaslangicTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BitisTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HesapBasiKisiSayisi = table.Column<int>(type: "int", nullable: true),
                    HesapBasiKrediMiktari = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    KullanimAmaci = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    OlusturanPersonelId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YazilimLisanslar", x => x.YazilimLisansId);
                    table.ForeignKey(
                        name: "FK_YazilimLisanslar_Personeller_OlusturanPersonelId",
                        column: x => x.OlusturanPersonelId,
                        principalTable: "Personeller",
                        principalColumn: "PersonelId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_YazilimLisanslar_Yazilimlar_YazilimId",
                        column: x => x.YazilimId,
                        principalTable: "Yazilimlar",
                        principalColumn: "YazilimId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "YazilimLisansKullanicilar",
                columns: table => new
                {
                    YazilimLisansKullaniciId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YazilimLisansId = table.Column<int>(type: "int", nullable: false),
                    PersonelId = table.Column<int>(type: "int", nullable: false),
                    KullaniciAdi = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Eposta = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    OnayDurumu = table.Column<int>(type: "int", nullable: false),
                    OnaylayanPersonelId = table.Column<int>(type: "int", nullable: true),
                    OnayTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    KaydiOlusturanPersonelId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YazilimLisansKullanicilar", x => x.YazilimLisansKullaniciId);
                    table.ForeignKey(
                        name: "FK_YazilimLisansKullanicilar_Personeller_KaydiOlusturanPersonelId",
                        column: x => x.KaydiOlusturanPersonelId,
                        principalTable: "Personeller",
                        principalColumn: "PersonelId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_YazilimLisansKullanicilar_Personeller_OnaylayanPersonelId",
                        column: x => x.OnaylayanPersonelId,
                        principalTable: "Personeller",
                        principalColumn: "PersonelId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_YazilimLisansKullanicilar_Personeller_PersonelId",
                        column: x => x.PersonelId,
                        principalTable: "Personeller",
                        principalColumn: "PersonelId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_YazilimLisansKullanicilar_YazilimLisanslar_YazilimLisansId",
                        column: x => x.YazilimLisansId,
                        principalTable: "YazilimLisanslar",
                        principalColumn: "YazilimLisansId",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_YazilimLisansKullanicilar_KaydiOlusturanPersonelId",
                table: "YazilimLisansKullanicilar",
                column: "KaydiOlusturanPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_YazilimLisansKullanicilar_OnaylayanPersonelId",
                table: "YazilimLisansKullanicilar",
                column: "OnaylayanPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_YazilimLisansKullanicilar_PersonelId",
                table: "YazilimLisansKullanicilar",
                column: "PersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_YazilimLisansKullanicilar_YazilimLisansId_PersonelId",
                table: "YazilimLisansKullanicilar",
                columns: new[] { "YazilimLisansId", "PersonelId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_YazilimLisanslar_OlusturanPersonelId",
                table: "YazilimLisanslar",
                column: "OlusturanPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_YazilimLisanslar_YazilimId",
                table: "YazilimLisanslar",
                column: "YazilimId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "YazilimLisansKullanicilar");

            migrationBuilder.DropTable(
                name: "YazilimLisanslar");

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
        }
    }
}
