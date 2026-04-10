using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PersonelTakipSistemi.Migrations
{
    /// <inheritdoc />
    public partial class AddDeviceModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CihazTurleri",
                columns: table => new
                {
                    CihazTuruId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    KullanimAmaci = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    SistemSecenegiMi = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CihazTurleri", x => x.CihazTuruId);
                });

            migrationBuilder.CreateTable(
                name: "CihazMarkalari",
                columns: table => new
                {
                    CihazMarkaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CihazTuruId = table.Column<int>(type: "int", nullable: false),
                    Ad = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    SistemSecenegiMi = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CihazMarkalari", x => x.CihazMarkaId);
                    table.ForeignKey(
                        name: "FK_CihazMarkalari_CihazTurleri_CihazTuruId",
                        column: x => x.CihazTuruId,
                        principalTable: "CihazTurleri",
                        principalColumn: "CihazTuruId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cihazlar",
                columns: table => new
                {
                    CihazId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CihazTuruId = table.Column<int>(type: "int", nullable: false),
                    CihazMarkaId = table.Column<int>(type: "int", nullable: false),
                    DigerCihazTuruAd = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    DigerMarkaAd = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Model = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Ozellikler = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    SeriNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SahipPersonelId = table.Column<int>(type: "int", nullable: false),
                    KoordinatorlukId = table.Column<int>(type: "int", nullable: false),
                    IlkKayitTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AktifSahiplikBaslangicTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SonDevirTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OnayDurumu = table.Column<int>(type: "int", nullable: false),
                    OnaylayanPersonelId = table.Column<int>(type: "int", nullable: true),
                    OnayTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OlusturanPersonelId = table.Column<int>(type: "int", nullable: false),
                    KoordinatorTarafindanEklendi = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cihazlar", x => x.CihazId);
                    table.ForeignKey(
                        name: "FK_Cihazlar_CihazMarkalari_CihazMarkaId",
                        column: x => x.CihazMarkaId,
                        principalTable: "CihazMarkalari",
                        principalColumn: "CihazMarkaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cihazlar_CihazTurleri_CihazTuruId",
                        column: x => x.CihazTuruId,
                        principalTable: "CihazTurleri",
                        principalColumn: "CihazTuruId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cihazlar_Koordinatorlukler_KoordinatorlukId",
                        column: x => x.KoordinatorlukId,
                        principalTable: "Koordinatorlukler",
                        principalColumn: "KoordinatorlukId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cihazlar_Personeller_OlusturanPersonelId",
                        column: x => x.OlusturanPersonelId,
                        principalTable: "Personeller",
                        principalColumn: "PersonelId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cihazlar_Personeller_OnaylayanPersonelId",
                        column: x => x.OnaylayanPersonelId,
                        principalTable: "Personeller",
                        principalColumn: "PersonelId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cihazlar_Personeller_SahipPersonelId",
                        column: x => x.SahipPersonelId,
                        principalTable: "Personeller",
                        principalColumn: "PersonelId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CihazHareketleri",
                columns: table => new
                {
                    CihazHareketiId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CihazId = table.Column<int>(type: "int", nullable: false),
                    HareketTuru = table.Column<int>(type: "int", nullable: false),
                    OncekiSahipPersonelId = table.Column<int>(type: "int", nullable: true),
                    YeniSahipPersonelId = table.Column<int>(type: "int", nullable: true),
                    IslemYapanPersonelId = table.Column<int>(type: "int", nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DurumNotu = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Tarih = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CihazHareketleri", x => x.CihazHareketiId);
                    table.ForeignKey(
                        name: "FK_CihazHareketleri_Cihazlar_CihazId",
                        column: x => x.CihazId,
                        principalTable: "Cihazlar",
                        principalColumn: "CihazId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CihazHareketleri_Personeller_IslemYapanPersonelId",
                        column: x => x.IslemYapanPersonelId,
                        principalTable: "Personeller",
                        principalColumn: "PersonelId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CihazHareketleri_Personeller_OncekiSahipPersonelId",
                        column: x => x.OncekiSahipPersonelId,
                        principalTable: "Personeller",
                        principalColumn: "PersonelId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CihazHareketleri_Personeller_YeniSahipPersonelId",
                        column: x => x.YeniSahipPersonelId,
                        principalTable: "Personeller",
                        principalColumn: "PersonelId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "CihazTurleri",
                columns: new[] { "CihazTuruId", "Ad", "CreatedAt", "IsActive", "KullanimAmaci", "SistemSecenegiMi" },
                values: new object[,]
                {
                    { 1, "Masaüstü Bilgisayar", new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(5783), true, "Ofis ve içerik üretimi", false },
                    { 2, "Dizüstü Bilgisayar", new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(5792), true, "Mobil çalışma ve üretim", false },
                    { 3, "Monitör", new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(5793), true, "Görüntüleme ve çoklu ekran", false },
                    { 4, "Çizim Tableti", new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(5794), true, "Tasarım ve illüstrasyon", false },
                    { 5, "Fotoğraf Makinesi", new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(5896), true, "Fotoğraf çekimi", false },
                    { 6, "Video Kamera", new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(5897), true, "Video kayıt ve prodüksiyon", false },
                    { 7, "Mikrofon", new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(5898), true, "Ses kayıt", false },
                    { 8, "Ses Kartı", new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(5899), true, "Ses işleme", false },
                    { 9, "Kulaklık", new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(5900), true, "Kurgu ve monitörleme", false },
                    { 10, "Yazıcı", new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(5901), true, "Doküman çıktı alma", false },
                    { 11, "Tarayıcı", new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(5902), true, "Doküman dijitalleştirme", false },
                    { 12, "Projektör", new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(5902), true, "Sunum ve eğitim", false },
                    { 999, "Diğer", new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(5903), true, "Sistemde tanımlı olmayan cihaz türleri", true }
                });

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

            migrationBuilder.InsertData(
                table: "CihazMarkalari",
                columns: new[] { "CihazMarkaId", "Ad", "CihazTuruId", "CreatedAt", "IsActive", "SistemSecenegiMi" },
                values: new object[,]
                {
                    { 1001, "Dell", 1, new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7209), true, false },
                    { 1002, "HP", 1, new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7213), true, false },
                    { 1003, "Lenovo", 1, new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7215), true, false },
                    { 1099, "Diğer", 1, new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7215), true, true },
                    { 2001, "Apple", 2, new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7217), true, false },
                    { 2002, "Dell", 2, new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7217), true, false },
                    { 2003, "Lenovo", 2, new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7218), true, false },
                    { 2004, "HP", 2, new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7219), true, false },
                    { 2099, "Diğer", 2, new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7220), true, true },
                    { 3001, "LG", 3, new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7221), true, false },
                    { 3002, "Samsung", 3, new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7222), true, false },
                    { 3003, "BenQ", 3, new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7223), true, false },
                    { 3099, "Diğer", 3, new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7223), true, true },
                    { 4001, "Wacom", 4, new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7224), true, false },
                    { 4002, "Huion", 4, new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7225), true, false },
                    { 4003, "XP-Pen", 4, new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7226), true, false },
                    { 4099, "Diğer", 4, new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7226), true, true },
                    { 5001, "Canon", 5, new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7227), true, false },
                    { 5002, "Sony", 5, new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7228), true, false },
                    { 5003, "Nikon", 5, new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7228), true, false },
                    { 5099, "Diğer", 5, new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7229), true, true },
                    { 6001, "Sony", 6, new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7230), true, false },
                    { 6002, "Canon", 6, new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7231), true, false },
                    { 6003, "Panasonic", 6, new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7231), true, false },
                    { 6099, "Diğer", 6, new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7232), true, true },
                    { 7001, "Rode", 7, new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7233), true, false },
                    { 7002, "Shure", 7, new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7233), true, false },
                    { 7003, "Audio-Technica", 7, new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7234), true, false },
                    { 7099, "Diğer", 7, new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7235), true, true },
                    { 8001, "Focusrite", 8, new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7236), true, false },
                    { 8002, "Zoom", 8, new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7236), true, false },
                    { 8099, "Diğer", 8, new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7237), true, true },
                    { 9001, "Sony", 9, new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7238), true, false },
                    { 9002, "Sennheiser", 9, new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7238), true, false },
                    { 9099, "Diğer", 9, new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7239), true, true },
                    { 10001, "HP", 10, new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7240), true, false },
                    { 10002, "Canon", 10, new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7241), true, false },
                    { 10099, "Diğer", 10, new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7241), true, true },
                    { 11001, "Epson", 11, new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7242), true, false },
                    { 11002, "Canon", 11, new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7243), true, false },
                    { 11099, "Diğer", 11, new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7243), true, true },
                    { 12001, "Epson", 12, new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7244), true, false },
                    { 12002, "ViewSonic", 12, new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7245), true, false },
                    { 12099, "Diğer", 12, new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7246), true, true },
                    { 99999, "Diğer", 999, new DateTime(2026, 4, 10, 12, 24, 52, 816, DateTimeKind.Local).AddTicks(7246), true, true }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CihazHareketleri_CihazId_Tarih",
                table: "CihazHareketleri",
                columns: new[] { "CihazId", "Tarih" });

            migrationBuilder.CreateIndex(
                name: "IX_CihazHareketleri_IslemYapanPersonelId",
                table: "CihazHareketleri",
                column: "IslemYapanPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_CihazHareketleri_OncekiSahipPersonelId",
                table: "CihazHareketleri",
                column: "OncekiSahipPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_CihazHareketleri_YeniSahipPersonelId",
                table: "CihazHareketleri",
                column: "YeniSahipPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_Cihazlar_CihazMarkaId",
                table: "Cihazlar",
                column: "CihazMarkaId");

            migrationBuilder.CreateIndex(
                name: "IX_Cihazlar_CihazTuruId",
                table: "Cihazlar",
                column: "CihazTuruId");

            migrationBuilder.CreateIndex(
                name: "IX_Cihazlar_KoordinatorlukId_OnayDurumu",
                table: "Cihazlar",
                columns: new[] { "KoordinatorlukId", "OnayDurumu" });

            migrationBuilder.CreateIndex(
                name: "IX_Cihazlar_OlusturanPersonelId",
                table: "Cihazlar",
                column: "OlusturanPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_Cihazlar_OnaylayanPersonelId",
                table: "Cihazlar",
                column: "OnaylayanPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_Cihazlar_SahipPersonelId",
                table: "Cihazlar",
                column: "SahipPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_Cihazlar_SeriNo",
                table: "Cihazlar",
                column: "SeriNo");

            migrationBuilder.CreateIndex(
                name: "IX_CihazMarkalari_CihazTuruId_Ad",
                table: "CihazMarkalari",
                columns: new[] { "CihazTuruId", "Ad" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CihazTurleri_Ad",
                table: "CihazTurleri",
                column: "Ad",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CihazHareketleri");

            migrationBuilder.DropTable(
                name: "Cihazlar");

            migrationBuilder.DropTable(
                name: "CihazMarkalari");

            migrationBuilder.DropTable(
                name: "CihazTurleri");

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 0, 28, 24, 746, DateTimeKind.Local).AddTicks(8301));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 0, 28, 24, 746, DateTimeKind.Local).AddTicks(8325));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 0, 28, 24, 746, DateTimeKind.Local).AddTicks(8328));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 0, 28, 24, 746, DateTimeKind.Local).AddTicks(8330));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 0, 28, 24, 746, DateTimeKind.Local).AddTicks(8332));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 0, 28, 24, 746, DateTimeKind.Local).AddTicks(8334));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 0, 28, 24, 746, DateTimeKind.Local).AddTicks(8337));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 0, 28, 24, 746, DateTimeKind.Local).AddTicks(8338));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 0, 28, 24, 746, DateTimeKind.Local).AddTicks(8340));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 0, 28, 24, 746, DateTimeKind.Local).AddTicks(8343));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 0, 28, 24, 746, DateTimeKind.Local).AddTicks(8345));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 0, 28, 24, 746, DateTimeKind.Local).AddTicks(8347));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 0, 28, 24, 738, DateTimeKind.Local).AddTicks(2287));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 0, 28, 24, 738, DateTimeKind.Local).AddTicks(2288));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 0, 28, 24, 738, DateTimeKind.Local).AddTicks(2269));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 0, 28, 24, 738, DateTimeKind.Local).AddTicks(2274));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 0, 28, 24, 738, DateTimeKind.Local).AddTicks(2275));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 0, 28, 24, 738, DateTimeKind.Local).AddTicks(2276));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 0, 28, 24, 738, DateTimeKind.Local).AddTicks(2277));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 0, 28, 24, 738, DateTimeKind.Local).AddTicks(2278));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 0, 28, 24, 738, DateTimeKind.Local).AddTicks(2279));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 0, 28, 24, 738, DateTimeKind.Local).AddTicks(2280));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 0, 28, 24, 738, DateTimeKind.Local).AddTicks(2281));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 0, 28, 24, 738, DateTimeKind.Local).AddTicks(2281));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 0, 28, 24, 738, DateTimeKind.Local).AddTicks(2282));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 0, 28, 24, 738, DateTimeKind.Local).AddTicks(2283));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 16,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 0, 28, 24, 738, DateTimeKind.Local).AddTicks(2284));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 17,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 0, 28, 24, 738, DateTimeKind.Local).AddTicks(2285));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 18,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 0, 28, 24, 738, DateTimeKind.Local).AddTicks(2285));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 19,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 0, 28, 24, 738, DateTimeKind.Local).AddTicks(2286));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 0, 28, 24, 738, DateTimeKind.Local).AddTicks(1122));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 0, 28, 24, 738, DateTimeKind.Local).AddTicks(1137));
        }
    }
}
