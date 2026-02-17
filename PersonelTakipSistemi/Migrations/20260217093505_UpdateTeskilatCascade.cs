using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonelTakipSistemi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTeskilatCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teskilatlar_DaireBaskanliklari_DaireBaskanligiId",
                table: "Teskilatlar");

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 17, 12, 35, 4, 511, DateTimeKind.Local).AddTicks(4011));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 17, 12, 35, 4, 511, DateTimeKind.Local).AddTicks(4019));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 17, 12, 35, 4, 511, DateTimeKind.Local).AddTicks(4021));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 17, 12, 35, 4, 511, DateTimeKind.Local).AddTicks(4021));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 17, 12, 35, 4, 511, DateTimeKind.Local).AddTicks(6734));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 17, 12, 35, 4, 511, DateTimeKind.Local).AddTicks(6751));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 17, 12, 35, 4, 511, DateTimeKind.Local).AddTicks(6754));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 17, 12, 35, 4, 511, DateTimeKind.Local).AddTicks(6756));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 17, 12, 35, 4, 511, DateTimeKind.Local).AddTicks(6758));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 17, 12, 35, 4, 511, DateTimeKind.Local).AddTicks(6761));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 17, 12, 35, 4, 511, DateTimeKind.Local).AddTicks(6763));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 17, 12, 35, 4, 511, DateTimeKind.Local).AddTicks(6765));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 17, 12, 35, 4, 511, DateTimeKind.Local).AddTicks(6768));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 17, 12, 35, 4, 511, DateTimeKind.Local).AddTicks(6771));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 17, 12, 35, 4, 511, DateTimeKind.Local).AddTicks(6772));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 17, 12, 35, 4, 511, DateTimeKind.Local).AddTicks(6774));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 17, 12, 35, 4, 507, DateTimeKind.Local).AddTicks(2902));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 17, 12, 35, 4, 507, DateTimeKind.Local).AddTicks(2903));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 17, 12, 35, 4, 507, DateTimeKind.Local).AddTicks(2885));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 17, 12, 35, 4, 507, DateTimeKind.Local).AddTicks(2889));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 17, 12, 35, 4, 507, DateTimeKind.Local).AddTicks(2890));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 17, 12, 35, 4, 507, DateTimeKind.Local).AddTicks(2891));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 17, 12, 35, 4, 507, DateTimeKind.Local).AddTicks(2892));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 17, 12, 35, 4, 507, DateTimeKind.Local).AddTicks(2893));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 17, 12, 35, 4, 507, DateTimeKind.Local).AddTicks(2894));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 17, 12, 35, 4, 507, DateTimeKind.Local).AddTicks(2895));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 17, 12, 35, 4, 507, DateTimeKind.Local).AddTicks(2896));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 17, 12, 35, 4, 507, DateTimeKind.Local).AddTicks(2897));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 17, 12, 35, 4, 507, DateTimeKind.Local).AddTicks(2897));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 17, 12, 35, 4, 507, DateTimeKind.Local).AddTicks(2898));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 16,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 17, 12, 35, 4, 507, DateTimeKind.Local).AddTicks(2899));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 17,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 17, 12, 35, 4, 507, DateTimeKind.Local).AddTicks(2900));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 18,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 17, 12, 35, 4, 507, DateTimeKind.Local).AddTicks(2900));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 19,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 17, 12, 35, 4, 507, DateTimeKind.Local).AddTicks(2901));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 17, 12, 35, 4, 507, DateTimeKind.Local).AddTicks(1783));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 17, 12, 35, 4, 507, DateTimeKind.Local).AddTicks(1795));

            migrationBuilder.AddForeignKey(
                name: "FK_Teskilatlar_DaireBaskanliklari_DaireBaskanligiId",
                table: "Teskilatlar",
                column: "DaireBaskanligiId",
                principalTable: "DaireBaskanliklari",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teskilatlar_DaireBaskanliklari_DaireBaskanligiId",
                table: "Teskilatlar");

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 14, 12, 41, 14, 86, DateTimeKind.Local).AddTicks(858));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 14, 12, 41, 14, 86, DateTimeKind.Local).AddTicks(870));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 14, 12, 41, 14, 86, DateTimeKind.Local).AddTicks(872));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 14, 12, 41, 14, 86, DateTimeKind.Local).AddTicks(873));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 14, 12, 41, 14, 86, DateTimeKind.Local).AddTicks(5466));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 14, 12, 41, 14, 86, DateTimeKind.Local).AddTicks(5484));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 14, 12, 41, 14, 86, DateTimeKind.Local).AddTicks(5488));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 14, 12, 41, 14, 86, DateTimeKind.Local).AddTicks(5491));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 14, 12, 41, 14, 86, DateTimeKind.Local).AddTicks(5494));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 14, 12, 41, 14, 86, DateTimeKind.Local).AddTicks(5497));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 14, 12, 41, 14, 86, DateTimeKind.Local).AddTicks(5500));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 14, 12, 41, 14, 86, DateTimeKind.Local).AddTicks(5530));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 14, 12, 41, 14, 86, DateTimeKind.Local).AddTicks(5534));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 14, 12, 41, 14, 86, DateTimeKind.Local).AddTicks(5538));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 14, 12, 41, 14, 86, DateTimeKind.Local).AddTicks(5541));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 14, 12, 41, 14, 86, DateTimeKind.Local).AddTicks(5543));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 14, 12, 41, 14, 81, DateTimeKind.Local).AddTicks(2111));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 14, 12, 41, 14, 81, DateTimeKind.Local).AddTicks(2111));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 14, 12, 41, 14, 81, DateTimeKind.Local).AddTicks(2059));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 14, 12, 41, 14, 81, DateTimeKind.Local).AddTicks(2095));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 14, 12, 41, 14, 81, DateTimeKind.Local).AddTicks(2097));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 14, 12, 41, 14, 81, DateTimeKind.Local).AddTicks(2098));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 14, 12, 41, 14, 81, DateTimeKind.Local).AddTicks(2099));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 14, 12, 41, 14, 81, DateTimeKind.Local).AddTicks(2100));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 14, 12, 41, 14, 81, DateTimeKind.Local).AddTicks(2101));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 14, 12, 41, 14, 81, DateTimeKind.Local).AddTicks(2102));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 14, 12, 41, 14, 81, DateTimeKind.Local).AddTicks(2103));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 14, 12, 41, 14, 81, DateTimeKind.Local).AddTicks(2104));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 14, 12, 41, 14, 81, DateTimeKind.Local).AddTicks(2105));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 14, 12, 41, 14, 81, DateTimeKind.Local).AddTicks(2106));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 16,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 14, 12, 41, 14, 81, DateTimeKind.Local).AddTicks(2107));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 17,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 14, 12, 41, 14, 81, DateTimeKind.Local).AddTicks(2108));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 18,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 14, 12, 41, 14, 81, DateTimeKind.Local).AddTicks(2109));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 19,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 14, 12, 41, 14, 81, DateTimeKind.Local).AddTicks(2110));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 14, 12, 41, 14, 81, DateTimeKind.Local).AddTicks(725));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 14, 12, 41, 14, 81, DateTimeKind.Local).AddTicks(740));

            migrationBuilder.AddForeignKey(
                name: "FK_Teskilatlar_DaireBaskanliklari_DaireBaskanligiId",
                table: "Teskilatlar",
                column: "DaireBaskanligiId",
                principalTable: "DaireBaskanliklari",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
