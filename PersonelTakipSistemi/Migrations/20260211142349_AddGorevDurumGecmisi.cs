using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonelTakipSistemi.Migrations
{
    /// <inheritdoc />
    public partial class AddGorevDurumGecmisi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GorevDurumGecmisleri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GorevId = table.Column<int>(type: "int", nullable: false),
                    GorevDurumId = table.Column<int>(type: "int", nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IslemYapanPersonelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GorevDurumGecmisleri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GorevDurumGecmisleri_GorevDurumlari_GorevDurumId",
                        column: x => x.GorevDurumId,
                        principalTable: "GorevDurumlari",
                        principalColumn: "GorevDurumId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GorevDurumGecmisleri_Gorevler_GorevId",
                        column: x => x.GorevId,
                        principalTable: "Gorevler",
                        principalColumn: "GorevId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GorevDurumGecmisleri_Personeller_IslemYapanPersonelId",
                        column: x => x.IslemYapanPersonelId,
                        principalTable: "Personeller",
                        principalColumn: "PersonelId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 11, 17, 23, 49, 398, DateTimeKind.Local).AddTicks(4712));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 11, 17, 23, 49, 398, DateTimeKind.Local).AddTicks(4725));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 11, 17, 23, 49, 398, DateTimeKind.Local).AddTicks(4726));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 11, 17, 23, 49, 398, DateTimeKind.Local).AddTicks(4726));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 11, 17, 23, 49, 398, DateTimeKind.Local).AddTicks(7686));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 11, 17, 23, 49, 398, DateTimeKind.Local).AddTicks(7698));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 11, 17, 23, 49, 398, DateTimeKind.Local).AddTicks(7702));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 11, 17, 23, 49, 398, DateTimeKind.Local).AddTicks(7706));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 11, 17, 23, 49, 398, DateTimeKind.Local).AddTicks(7709));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 11, 17, 23, 49, 398, DateTimeKind.Local).AddTicks(7712));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 11, 17, 23, 49, 398, DateTimeKind.Local).AddTicks(7714));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 11, 17, 23, 49, 398, DateTimeKind.Local).AddTicks(7716));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 11, 17, 23, 49, 398, DateTimeKind.Local).AddTicks(7718));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 11, 17, 23, 49, 398, DateTimeKind.Local).AddTicks(7721));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 11, 17, 23, 49, 398, DateTimeKind.Local).AddTicks(7723));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 11, 17, 23, 49, 398, DateTimeKind.Local).AddTicks(7724));

            migrationBuilder.UpdateData(
                table: "Komisyonlar",
                keyColumn: "KomisyonId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 11, 17, 23, 49, 395, DateTimeKind.Local).AddTicks(1368));

            migrationBuilder.UpdateData(
                table: "Komisyonlar",
                keyColumn: "KomisyonId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 11, 17, 23, 49, 395, DateTimeKind.Local).AddTicks(1371));

            migrationBuilder.UpdateData(
                table: "Komisyonlar",
                keyColumn: "KomisyonId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 11, 17, 23, 49, 395, DateTimeKind.Local).AddTicks(1395));

            migrationBuilder.UpdateData(
                table: "Komisyonlar",
                keyColumn: "KomisyonId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 11, 17, 23, 49, 395, DateTimeKind.Local).AddTicks(1395));

            migrationBuilder.UpdateData(
                table: "Komisyonlar",
                keyColumn: "KomisyonId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 11, 17, 23, 49, 395, DateTimeKind.Local).AddTicks(1396));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 11, 17, 23, 49, 395, DateTimeKind.Local).AddTicks(501));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 11, 17, 23, 49, 395, DateTimeKind.Local).AddTicks(505));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 11, 17, 23, 49, 395, DateTimeKind.Local).AddTicks(506));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 11, 17, 23, 49, 394, DateTimeKind.Local).AddTicks(9325));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 11, 17, 23, 49, 394, DateTimeKind.Local).AddTicks(9335));

            migrationBuilder.CreateIndex(
                name: "IX_GorevDurumGecmisleri_GorevDurumId",
                table: "GorevDurumGecmisleri",
                column: "GorevDurumId");

            migrationBuilder.CreateIndex(
                name: "IX_GorevDurumGecmisleri_GorevId",
                table: "GorevDurumGecmisleri",
                column: "GorevId");

            migrationBuilder.CreateIndex(
                name: "IX_GorevDurumGecmisleri_IslemYapanPersonelId",
                table: "GorevDurumGecmisleri",
                column: "IslemYapanPersonelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GorevDurumGecmisleri");

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
    }
}
