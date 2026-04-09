using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonelTakipSistemi.Migrations
{
    /// <inheritdoc />
    public partial class AddGorevAtamaGorevTuru : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GorevTuruId",
                table: "GorevAtamaTeskilatlar",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "GorevTuruId",
                table: "GorevAtamaPersoneller",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "GorevTuruId",
                table: "GorevAtamaKoordinatorlukler",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "GorevTuruId",
                table: "GorevAtamaKomisyonlar",
                type: "int",
                nullable: false,
                defaultValue: 1);

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

            migrationBuilder.CreateIndex(
                name: "IX_GorevAtamaTeskilatlar_GorevTuruId",
                table: "GorevAtamaTeskilatlar",
                column: "GorevTuruId");

            migrationBuilder.CreateIndex(
                name: "IX_GorevAtamaPersoneller_GorevTuruId",
                table: "GorevAtamaPersoneller",
                column: "GorevTuruId");

            migrationBuilder.CreateIndex(
                name: "IX_GorevAtamaKoordinatorlukler_GorevTuruId",
                table: "GorevAtamaKoordinatorlukler",
                column: "GorevTuruId");

            migrationBuilder.CreateIndex(
                name: "IX_GorevAtamaKomisyonlar_GorevTuruId",
                table: "GorevAtamaKomisyonlar",
                column: "GorevTuruId");

            migrationBuilder.AddForeignKey(
                name: "FK_GorevAtamaKomisyonlar_GorevTurleri_GorevTuruId",
                table: "GorevAtamaKomisyonlar",
                column: "GorevTuruId",
                principalTable: "GorevTurleri",
                principalColumn: "GorevTuruId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GorevAtamaKoordinatorlukler_GorevTurleri_GorevTuruId",
                table: "GorevAtamaKoordinatorlukler",
                column: "GorevTuruId",
                principalTable: "GorevTurleri",
                principalColumn: "GorevTuruId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GorevAtamaPersoneller_GorevTurleri_GorevTuruId",
                table: "GorevAtamaPersoneller",
                column: "GorevTuruId",
                principalTable: "GorevTurleri",
                principalColumn: "GorevTuruId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GorevAtamaTeskilatlar_GorevTurleri_GorevTuruId",
                table: "GorevAtamaTeskilatlar",
                column: "GorevTuruId",
                principalTable: "GorevTurleri",
                principalColumn: "GorevTuruId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GorevAtamaKomisyonlar_GorevTurleri_GorevTuruId",
                table: "GorevAtamaKomisyonlar");

            migrationBuilder.DropForeignKey(
                name: "FK_GorevAtamaKoordinatorlukler_GorevTurleri_GorevTuruId",
                table: "GorevAtamaKoordinatorlukler");

            migrationBuilder.DropForeignKey(
                name: "FK_GorevAtamaPersoneller_GorevTurleri_GorevTuruId",
                table: "GorevAtamaPersoneller");

            migrationBuilder.DropForeignKey(
                name: "FK_GorevAtamaTeskilatlar_GorevTurleri_GorevTuruId",
                table: "GorevAtamaTeskilatlar");

            migrationBuilder.DropIndex(
                name: "IX_GorevAtamaTeskilatlar_GorevTuruId",
                table: "GorevAtamaTeskilatlar");

            migrationBuilder.DropIndex(
                name: "IX_GorevAtamaPersoneller_GorevTuruId",
                table: "GorevAtamaPersoneller");

            migrationBuilder.DropIndex(
                name: "IX_GorevAtamaKoordinatorlukler_GorevTuruId",
                table: "GorevAtamaKoordinatorlukler");

            migrationBuilder.DropIndex(
                name: "IX_GorevAtamaKomisyonlar_GorevTuruId",
                table: "GorevAtamaKomisyonlar");

            migrationBuilder.DropColumn(
                name: "GorevTuruId",
                table: "GorevAtamaTeskilatlar");

            migrationBuilder.DropColumn(
                name: "GorevTuruId",
                table: "GorevAtamaPersoneller");

            migrationBuilder.DropColumn(
                name: "GorevTuruId",
                table: "GorevAtamaKoordinatorlukler");

            migrationBuilder.DropColumn(
                name: "GorevTuruId",
                table: "GorevAtamaKomisyonlar");

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 23, 16, 45, 988, DateTimeKind.Local).AddTicks(1280));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 23, 16, 45, 988, DateTimeKind.Local).AddTicks(1303));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 23, 16, 45, 988, DateTimeKind.Local).AddTicks(1306));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 23, 16, 45, 988, DateTimeKind.Local).AddTicks(1308));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 23, 16, 45, 988, DateTimeKind.Local).AddTicks(1310));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 23, 16, 45, 988, DateTimeKind.Local).AddTicks(1313));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 23, 16, 45, 988, DateTimeKind.Local).AddTicks(1316));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 23, 16, 45, 988, DateTimeKind.Local).AddTicks(1317));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 23, 16, 45, 988, DateTimeKind.Local).AddTicks(1319));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 23, 16, 45, 988, DateTimeKind.Local).AddTicks(1322));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 23, 16, 45, 988, DateTimeKind.Local).AddTicks(1324));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 23, 16, 45, 988, DateTimeKind.Local).AddTicks(1325));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 23, 16, 45, 979, DateTimeKind.Local).AddTicks(3038));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 23, 16, 45, 979, DateTimeKind.Local).AddTicks(3039));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 23, 16, 45, 979, DateTimeKind.Local).AddTicks(2983));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 23, 16, 45, 979, DateTimeKind.Local).AddTicks(2987));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 23, 16, 45, 979, DateTimeKind.Local).AddTicks(2989));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 23, 16, 45, 979, DateTimeKind.Local).AddTicks(2990));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 23, 16, 45, 979, DateTimeKind.Local).AddTicks(2991));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 23, 16, 45, 979, DateTimeKind.Local).AddTicks(2992));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 23, 16, 45, 979, DateTimeKind.Local).AddTicks(2993));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 23, 16, 45, 979, DateTimeKind.Local).AddTicks(2994));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 23, 16, 45, 979, DateTimeKind.Local).AddTicks(2994));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 23, 16, 45, 979, DateTimeKind.Local).AddTicks(3032));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 23, 16, 45, 979, DateTimeKind.Local).AddTicks(3032));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 23, 16, 45, 979, DateTimeKind.Local).AddTicks(3033));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 16,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 23, 16, 45, 979, DateTimeKind.Local).AddTicks(3034));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 17,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 23, 16, 45, 979, DateTimeKind.Local).AddTicks(3035));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 18,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 23, 16, 45, 979, DateTimeKind.Local).AddTicks(3036));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 19,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 23, 16, 45, 979, DateTimeKind.Local).AddTicks(3037));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 23, 16, 45, 979, DateTimeKind.Local).AddTicks(1786));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 23, 16, 45, 979, DateTimeKind.Local).AddTicks(1801));
        }
    }
}
