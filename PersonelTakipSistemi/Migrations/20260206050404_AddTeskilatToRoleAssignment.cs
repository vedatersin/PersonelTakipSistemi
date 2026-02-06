using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonelTakipSistemi.Migrations
{
    /// <inheritdoc />
    public partial class AddTeskilatToRoleAssignment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeskilatId",
                table: "PersonelKurumsalRolAtamalari",
                type: "int",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_PersonelKurumsalRolAtamalari_TeskilatId",
                table: "PersonelKurumsalRolAtamalari",
                column: "TeskilatId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonelKurumsalRolAtamalari_Teskilatlar_TeskilatId",
                table: "PersonelKurumsalRolAtamalari",
                column: "TeskilatId",
                principalTable: "Teskilatlar",
                principalColumn: "TeskilatId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonelKurumsalRolAtamalari_Teskilatlar_TeskilatId",
                table: "PersonelKurumsalRolAtamalari");

            migrationBuilder.DropIndex(
                name: "IX_PersonelKurumsalRolAtamalari_TeskilatId",
                table: "PersonelKurumsalRolAtamalari");

            migrationBuilder.DropColumn(
                name: "TeskilatId",
                table: "PersonelKurumsalRolAtamalari");

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 31, 1, 15, 38, 400, DateTimeKind.Local).AddTicks(488));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 31, 1, 15, 38, 400, DateTimeKind.Local).AddTicks(501));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 31, 1, 15, 38, 400, DateTimeKind.Local).AddTicks(503));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 31, 1, 15, 38, 400, DateTimeKind.Local).AddTicks(504));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 31, 1, 15, 38, 400, DateTimeKind.Local).AddTicks(5368));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 31, 1, 15, 38, 400, DateTimeKind.Local).AddTicks(5390));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 31, 1, 15, 38, 400, DateTimeKind.Local).AddTicks(5395));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 31, 1, 15, 38, 400, DateTimeKind.Local).AddTicks(5452));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 31, 1, 15, 38, 400, DateTimeKind.Local).AddTicks(5455));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 31, 1, 15, 38, 400, DateTimeKind.Local).AddTicks(5458));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 31, 1, 15, 38, 400, DateTimeKind.Local).AddTicks(5460));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 31, 1, 15, 38, 400, DateTimeKind.Local).AddTicks(5462));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 31, 1, 15, 38, 400, DateTimeKind.Local).AddTicks(5464));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 31, 1, 15, 38, 400, DateTimeKind.Local).AddTicks(5468));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 31, 1, 15, 38, 400, DateTimeKind.Local).AddTicks(5470));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 31, 1, 15, 38, 400, DateTimeKind.Local).AddTicks(5471));

            migrationBuilder.UpdateData(
                table: "Komisyonlar",
                keyColumn: "KomisyonId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 31, 1, 15, 38, 396, DateTimeKind.Local).AddTicks(5221));

            migrationBuilder.UpdateData(
                table: "Komisyonlar",
                keyColumn: "KomisyonId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 31, 1, 15, 38, 396, DateTimeKind.Local).AddTicks(5224));

            migrationBuilder.UpdateData(
                table: "Komisyonlar",
                keyColumn: "KomisyonId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 31, 1, 15, 38, 396, DateTimeKind.Local).AddTicks(5225));

            migrationBuilder.UpdateData(
                table: "Komisyonlar",
                keyColumn: "KomisyonId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 31, 1, 15, 38, 396, DateTimeKind.Local).AddTicks(5226));

            migrationBuilder.UpdateData(
                table: "Komisyonlar",
                keyColumn: "KomisyonId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 31, 1, 15, 38, 396, DateTimeKind.Local).AddTicks(5227));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 31, 1, 15, 38, 396, DateTimeKind.Local).AddTicks(4240));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 31, 1, 15, 38, 396, DateTimeKind.Local).AddTicks(4245));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 31, 1, 15, 38, 396, DateTimeKind.Local).AddTicks(4246));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 31, 1, 15, 38, 396, DateTimeKind.Local).AddTicks(2998));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 31, 1, 15, 38, 396, DateTimeKind.Local).AddTicks(3010));
        }
    }
}
