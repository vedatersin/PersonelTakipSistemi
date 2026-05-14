using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonelTakipSistemi.Migrations
{
    /// <inheritdoc />
    public partial class SeparateInventorySoftwareDefinitions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_YazilimKayitlari_Yazilimlar_YazilimId",
                table: "YazilimKayitlari");

            migrationBuilder.CreateTable(
                name: "YazilimTanimlari",
                columns: table => new
                {
                    YazilimId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    SistemSecenegiMi = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YazilimTanimlari", x => x.YazilimId);
                });

            migrationBuilder.Sql(
                """
                SET IDENTITY_INSERT [YazilimTanimlari] ON;

                INSERT INTO [YazilimTanimlari] ([YazilimId], [Ad], [SistemSecenegiMi], [CreatedAt])
                SELECT [YazilimId], [Ad], CAST(0 AS bit), GETDATE()
                FROM [Yazilimlar];

                SET IDENTITY_INSERT [YazilimTanimlari] OFF;

                UPDATE [YazilimTanimlari]
                SET [SistemSecenegiMi] = CAST(1 AS bit)
                WHERE [YazilimId] = 18 OR [Ad] = N'Diğer';

                IF NOT EXISTS (SELECT 1 FROM [YazilimTanimlari] WHERE [SistemSecenegiMi] = 1)
                BEGIN
                    INSERT INTO [YazilimTanimlari] ([Ad], [SistemSecenegiMi], [CreatedAt])
                    VALUES (N'Diğer', CAST(1 AS bit), GETDATE());
                END
                """);

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 1001,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 15, 0, DateTimeKind.Local).AddTicks(6409));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 1002,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 15, 0, DateTimeKind.Local).AddTicks(6413));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 1003,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 15, 0, DateTimeKind.Local).AddTicks(6415));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 2001,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 15, 0, DateTimeKind.Local).AddTicks(6416));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 2002,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 15, 0, DateTimeKind.Local).AddTicks(6417));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 2003,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 15, 0, DateTimeKind.Local).AddTicks(6417));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 2004,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 15, 0, DateTimeKind.Local).AddTicks(6418));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 3001,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 15, 0, DateTimeKind.Local).AddTicks(6419));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 3002,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 15, 0, DateTimeKind.Local).AddTicks(6420));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 3003,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 15, 0, DateTimeKind.Local).AddTicks(6420));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 4001,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 15, 0, DateTimeKind.Local).AddTicks(6421));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 4002,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 15, 0, DateTimeKind.Local).AddTicks(6422));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 4003,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 15, 0, DateTimeKind.Local).AddTicks(6422));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 5001,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 15, 0, DateTimeKind.Local).AddTicks(6423));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 5002,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 15, 0, DateTimeKind.Local).AddTicks(6424));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 5003,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 15, 0, DateTimeKind.Local).AddTicks(6425));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 6001,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 15, 0, DateTimeKind.Local).AddTicks(6425));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 6002,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 15, 0, DateTimeKind.Local).AddTicks(6426));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 6003,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 15, 0, DateTimeKind.Local).AddTicks(6427));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 7001,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 15, 0, DateTimeKind.Local).AddTicks(6427));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 7002,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 15, 0, DateTimeKind.Local).AddTicks(6428));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 7003,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 15, 0, DateTimeKind.Local).AddTicks(6429));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 8001,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 15, 0, DateTimeKind.Local).AddTicks(6429));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 8002,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 15, 0, DateTimeKind.Local).AddTicks(6430));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 9001,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 15, 0, DateTimeKind.Local).AddTicks(6431));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 9002,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 15, 0, DateTimeKind.Local).AddTicks(6431));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 10001,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 15, 0, DateTimeKind.Local).AddTicks(6432));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 10002,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 15, 0, DateTimeKind.Local).AddTicks(6433));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 11001,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 15, 0, DateTimeKind.Local).AddTicks(6433));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 11002,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 15, 0, DateTimeKind.Local).AddTicks(6434));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 12001,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 15, 0, DateTimeKind.Local).AddTicks(6435));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 12002,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 15, 0, DateTimeKind.Local).AddTicks(6435));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 99999,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 15, 0, DateTimeKind.Local).AddTicks(6436));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 15, 0, DateTimeKind.Local).AddTicks(5215));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 15, 0, DateTimeKind.Local).AddTicks(5224));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 15, 0, DateTimeKind.Local).AddTicks(5225));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 15, 0, DateTimeKind.Local).AddTicks(5226));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 15, 0, DateTimeKind.Local).AddTicks(5227));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 15, 0, DateTimeKind.Local).AddTicks(5228));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 15, 0, DateTimeKind.Local).AddTicks(5229));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 15, 0, DateTimeKind.Local).AddTicks(5230));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 15, 0, DateTimeKind.Local).AddTicks(5231));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 15, 0, DateTimeKind.Local).AddTicks(5231));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 15, 0, DateTimeKind.Local).AddTicks(5233));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 15, 0, DateTimeKind.Local).AddTicks(5234));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 999,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 15, 0, DateTimeKind.Local).AddTicks(5236));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 14, 999, DateTimeKind.Local).AddTicks(2278));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 14, 999, DateTimeKind.Local).AddTicks(2308));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 14, 999, DateTimeKind.Local).AddTicks(2311));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 14, 999, DateTimeKind.Local).AddTicks(2314));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 14, 999, DateTimeKind.Local).AddTicks(2316));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 14, 999, DateTimeKind.Local).AddTicks(2320));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 14, 999, DateTimeKind.Local).AddTicks(2322));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 14, 999, DateTimeKind.Local).AddTicks(2324));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 14, 999, DateTimeKind.Local).AddTicks(2327));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 14, 999, DateTimeKind.Local).AddTicks(2330));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 14, 999, DateTimeKind.Local).AddTicks(2332));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 14, 999, DateTimeKind.Local).AddTicks(2334));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 14, 987, DateTimeKind.Local).AddTicks(4796));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 14, 987, DateTimeKind.Local).AddTicks(4797));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 14, 987, DateTimeKind.Local).AddTicks(4776));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 14, 987, DateTimeKind.Local).AddTicks(4782));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 14, 987, DateTimeKind.Local).AddTicks(4783));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 14, 987, DateTimeKind.Local).AddTicks(4784));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 14, 987, DateTimeKind.Local).AddTicks(4785));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 14, 987, DateTimeKind.Local).AddTicks(4786));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 14, 987, DateTimeKind.Local).AddTicks(4787));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 14, 987, DateTimeKind.Local).AddTicks(4788));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 14, 987, DateTimeKind.Local).AddTicks(4789));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 14, 987, DateTimeKind.Local).AddTicks(4790));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 14, 987, DateTimeKind.Local).AddTicks(4791));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 14, 987, DateTimeKind.Local).AddTicks(4792));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 16,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 14, 987, DateTimeKind.Local).AddTicks(4793));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 17,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 14, 987, DateTimeKind.Local).AddTicks(4794));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 18,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 14, 987, DateTimeKind.Local).AddTicks(4795));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 19,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 14, 987, DateTimeKind.Local).AddTicks(4795));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 14, 987, DateTimeKind.Local).AddTicks(3564));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 42, 14, 987, DateTimeKind.Local).AddTicks(3584));

            migrationBuilder.CreateIndex(
                name: "IX_YazilimTanimlari_Ad",
                table: "YazilimTanimlari",
                column: "Ad",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_YazilimKayitlari_YazilimTanimlari_YazilimId",
                table: "YazilimKayitlari",
                column: "YazilimId",
                principalTable: "YazilimTanimlari",
                principalColumn: "YazilimId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_YazilimKayitlari_YazilimTanimlari_YazilimId",
                table: "YazilimKayitlari");

            migrationBuilder.DropTable(
                name: "YazilimTanimlari");

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 1001,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 976, DateTimeKind.Local).AddTicks(5062));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 1002,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 976, DateTimeKind.Local).AddTicks(5066));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 1003,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 976, DateTimeKind.Local).AddTicks(5068));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 2001,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 976, DateTimeKind.Local).AddTicks(5068));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 2002,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 976, DateTimeKind.Local).AddTicks(5117));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 2003,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 976, DateTimeKind.Local).AddTicks(5118));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 2004,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 976, DateTimeKind.Local).AddTicks(5119));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 3001,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 976, DateTimeKind.Local).AddTicks(5120));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 3002,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 976, DateTimeKind.Local).AddTicks(5121));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 3003,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 976, DateTimeKind.Local).AddTicks(5122));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 4001,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 976, DateTimeKind.Local).AddTicks(5123));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 4002,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 976, DateTimeKind.Local).AddTicks(5124));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 4003,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 976, DateTimeKind.Local).AddTicks(5125));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 5001,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 976, DateTimeKind.Local).AddTicks(5126));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 5002,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 976, DateTimeKind.Local).AddTicks(5127));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 5003,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 976, DateTimeKind.Local).AddTicks(5127));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 6001,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 976, DateTimeKind.Local).AddTicks(5128));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 6002,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 976, DateTimeKind.Local).AddTicks(5129));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 6003,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 976, DateTimeKind.Local).AddTicks(5130));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 7001,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 976, DateTimeKind.Local).AddTicks(5131));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 7002,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 976, DateTimeKind.Local).AddTicks(5132));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 7003,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 976, DateTimeKind.Local).AddTicks(5132));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 8001,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 976, DateTimeKind.Local).AddTicks(5133));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 8002,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 976, DateTimeKind.Local).AddTicks(5134));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 9001,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 976, DateTimeKind.Local).AddTicks(5135));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 9002,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 976, DateTimeKind.Local).AddTicks(5136));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 10001,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 976, DateTimeKind.Local).AddTicks(5136));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 10002,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 976, DateTimeKind.Local).AddTicks(5137));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 11001,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 976, DateTimeKind.Local).AddTicks(5138));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 11002,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 976, DateTimeKind.Local).AddTicks(5139));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 12001,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 976, DateTimeKind.Local).AddTicks(5140));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 12002,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 976, DateTimeKind.Local).AddTicks(5140));

            migrationBuilder.UpdateData(
                table: "CihazMarkalari",
                keyColumn: "CihazMarkaId",
                keyValue: 99999,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 976, DateTimeKind.Local).AddTicks(5141));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 976, DateTimeKind.Local).AddTicks(3795));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 976, DateTimeKind.Local).AddTicks(3806));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 976, DateTimeKind.Local).AddTicks(3807));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 976, DateTimeKind.Local).AddTicks(3808));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 976, DateTimeKind.Local).AddTicks(3809));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 976, DateTimeKind.Local).AddTicks(3810));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 976, DateTimeKind.Local).AddTicks(3811));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 976, DateTimeKind.Local).AddTicks(3812));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 976, DateTimeKind.Local).AddTicks(3813));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 976, DateTimeKind.Local).AddTicks(3814));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 976, DateTimeKind.Local).AddTicks(3815));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 976, DateTimeKind.Local).AddTicks(3816));

            migrationBuilder.UpdateData(
                table: "CihazTurleri",
                keyColumn: "CihazTuruId",
                keyValue: 999,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 976, DateTimeKind.Local).AddTicks(3817));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 974, DateTimeKind.Local).AddTicks(8097));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 974, DateTimeKind.Local).AddTicks(8129));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 974, DateTimeKind.Local).AddTicks(8132));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 974, DateTimeKind.Local).AddTicks(8160));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 974, DateTimeKind.Local).AddTicks(8162));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 974, DateTimeKind.Local).AddTicks(8166));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 974, DateTimeKind.Local).AddTicks(8168));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 974, DateTimeKind.Local).AddTicks(8170));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 974, DateTimeKind.Local).AddTicks(8172));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 974, DateTimeKind.Local).AddTicks(8177));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 974, DateTimeKind.Local).AddTicks(8179));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 974, DateTimeKind.Local).AddTicks(8181));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 963, DateTimeKind.Local).AddTicks(9699));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 963, DateTimeKind.Local).AddTicks(9700));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 963, DateTimeKind.Local).AddTicks(9650));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 963, DateTimeKind.Local).AddTicks(9656));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 963, DateTimeKind.Local).AddTicks(9658));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 963, DateTimeKind.Local).AddTicks(9659));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 963, DateTimeKind.Local).AddTicks(9660));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 963, DateTimeKind.Local).AddTicks(9661));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 963, DateTimeKind.Local).AddTicks(9662));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 963, DateTimeKind.Local).AddTicks(9664));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 963, DateTimeKind.Local).AddTicks(9665));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 963, DateTimeKind.Local).AddTicks(9666));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 963, DateTimeKind.Local).AddTicks(9667));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 963, DateTimeKind.Local).AddTicks(9668));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 16,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 963, DateTimeKind.Local).AddTicks(9669));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 17,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 963, DateTimeKind.Local).AddTicks(9670));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 18,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 963, DateTimeKind.Local).AddTicks(9696));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 19,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 963, DateTimeKind.Local).AddTicks(9698));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 963, DateTimeKind.Local).AddTicks(8308));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 8, 50, 35, 963, DateTimeKind.Local).AddTicks(8326));

            migrationBuilder.AddForeignKey(
                name: "FK_YazilimKayitlari_Yazilimlar_YazilimId",
                table: "YazilimKayitlari",
                column: "YazilimId",
                principalTable: "Yazilimlar",
                principalColumn: "YazilimId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
