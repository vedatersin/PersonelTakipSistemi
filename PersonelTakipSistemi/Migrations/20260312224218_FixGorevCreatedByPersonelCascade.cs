using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonelTakipSistemi.Migrations
{
    /// <inheritdoc />
    public partial class FixGorevCreatedByPersonelCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gorevler_Personeller_CreatedByPersonelId",
                table: "Gorevler");

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 1, 42, 16, 29, DateTimeKind.Local).AddTicks(464));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 1, 42, 16, 29, DateTimeKind.Local).AddTicks(471));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 1, 42, 16, 29, DateTimeKind.Local).AddTicks(472));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 1, 42, 16, 29, DateTimeKind.Local).AddTicks(473));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 1, 42, 16, 29, DateTimeKind.Local).AddTicks(4707));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 1, 42, 16, 29, DateTimeKind.Local).AddTicks(4722));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 1, 42, 16, 29, DateTimeKind.Local).AddTicks(4724));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 1, 42, 16, 29, DateTimeKind.Local).AddTicks(4727));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 1, 42, 16, 29, DateTimeKind.Local).AddTicks(4729));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 1, 42, 16, 29, DateTimeKind.Local).AddTicks(4765));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 1, 42, 16, 29, DateTimeKind.Local).AddTicks(4769));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 1, 42, 16, 29, DateTimeKind.Local).AddTicks(4771));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 1, 42, 16, 29, DateTimeKind.Local).AddTicks(4773));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 1, 42, 16, 29, DateTimeKind.Local).AddTicks(4776));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 1, 42, 16, 29, DateTimeKind.Local).AddTicks(4778));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 1, 42, 16, 29, DateTimeKind.Local).AddTicks(4779));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 1, 42, 16, 24, DateTimeKind.Local).AddTicks(3413));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 1, 42, 16, 24, DateTimeKind.Local).AddTicks(3414));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 1, 42, 16, 24, DateTimeKind.Local).AddTicks(3347));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 1, 42, 16, 24, DateTimeKind.Local).AddTicks(3352));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 1, 42, 16, 24, DateTimeKind.Local).AddTicks(3353));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 1, 42, 16, 24, DateTimeKind.Local).AddTicks(3354));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 1, 42, 16, 24, DateTimeKind.Local).AddTicks(3355));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 1, 42, 16, 24, DateTimeKind.Local).AddTicks(3404));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 1, 42, 16, 24, DateTimeKind.Local).AddTicks(3405));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 1, 42, 16, 24, DateTimeKind.Local).AddTicks(3406));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 1, 42, 16, 24, DateTimeKind.Local).AddTicks(3407));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 1, 42, 16, 24, DateTimeKind.Local).AddTicks(3408));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 1, 42, 16, 24, DateTimeKind.Local).AddTicks(3408));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 1, 42, 16, 24, DateTimeKind.Local).AddTicks(3409));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 16,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 1, 42, 16, 24, DateTimeKind.Local).AddTicks(3410));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 17,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 1, 42, 16, 24, DateTimeKind.Local).AddTicks(3411));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 18,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 1, 42, 16, 24, DateTimeKind.Local).AddTicks(3411));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 19,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 1, 42, 16, 24, DateTimeKind.Local).AddTicks(3412));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 1, 42, 16, 24, DateTimeKind.Local).AddTicks(2034));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 1, 42, 16, 24, DateTimeKind.Local).AddTicks(2049));

            migrationBuilder.AddForeignKey(
                name: "FK_Gorevler_Personeller_CreatedByPersonelId",
                table: "Gorevler",
                column: "CreatedByPersonelId",
                principalTable: "Personeller",
                principalColumn: "PersonelId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gorevler_Personeller_CreatedByPersonelId",
                table: "Gorevler");

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 27, 19, 20, 54, 88, DateTimeKind.Local).AddTicks(8373));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 27, 19, 20, 54, 88, DateTimeKind.Local).AddTicks(8381));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 27, 19, 20, 54, 88, DateTimeKind.Local).AddTicks(8382));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 27, 19, 20, 54, 88, DateTimeKind.Local).AddTicks(8383));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 27, 19, 20, 54, 89, DateTimeKind.Local).AddTicks(1430));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 27, 19, 20, 54, 89, DateTimeKind.Local).AddTicks(1446));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 27, 19, 20, 54, 89, DateTimeKind.Local).AddTicks(1449));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 27, 19, 20, 54, 89, DateTimeKind.Local).AddTicks(1451));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 27, 19, 20, 54, 89, DateTimeKind.Local).AddTicks(1453));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 27, 19, 20, 54, 89, DateTimeKind.Local).AddTicks(1457));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 27, 19, 20, 54, 89, DateTimeKind.Local).AddTicks(1459));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 27, 19, 20, 54, 89, DateTimeKind.Local).AddTicks(1460));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 27, 19, 20, 54, 89, DateTimeKind.Local).AddTicks(1462));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 27, 19, 20, 54, 89, DateTimeKind.Local).AddTicks(1465));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 27, 19, 20, 54, 89, DateTimeKind.Local).AddTicks(1466));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 27, 19, 20, 54, 89, DateTimeKind.Local).AddTicks(1468));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 27, 19, 20, 54, 84, DateTimeKind.Local).AddTicks(7656));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 27, 19, 20, 54, 84, DateTimeKind.Local).AddTicks(7656));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 27, 19, 20, 54, 84, DateTimeKind.Local).AddTicks(7639));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 27, 19, 20, 54, 84, DateTimeKind.Local).AddTicks(7643));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 27, 19, 20, 54, 84, DateTimeKind.Local).AddTicks(7645));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 27, 19, 20, 54, 84, DateTimeKind.Local).AddTicks(7645));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 27, 19, 20, 54, 84, DateTimeKind.Local).AddTicks(7646));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 27, 19, 20, 54, 84, DateTimeKind.Local).AddTicks(7647));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 27, 19, 20, 54, 84, DateTimeKind.Local).AddTicks(7648));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 27, 19, 20, 54, 84, DateTimeKind.Local).AddTicks(7649));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 27, 19, 20, 54, 84, DateTimeKind.Local).AddTicks(7650));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 27, 19, 20, 54, 84, DateTimeKind.Local).AddTicks(7650));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 27, 19, 20, 54, 84, DateTimeKind.Local).AddTicks(7651));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 27, 19, 20, 54, 84, DateTimeKind.Local).AddTicks(7652));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 16,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 27, 19, 20, 54, 84, DateTimeKind.Local).AddTicks(7653));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 17,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 27, 19, 20, 54, 84, DateTimeKind.Local).AddTicks(7653));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 18,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 27, 19, 20, 54, 84, DateTimeKind.Local).AddTicks(7654));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 19,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 27, 19, 20, 54, 84, DateTimeKind.Local).AddTicks(7655));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 27, 19, 20, 54, 84, DateTimeKind.Local).AddTicks(6460));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 27, 19, 20, 54, 84, DateTimeKind.Local).AddTicks(6473));

            migrationBuilder.AddForeignKey(
                name: "FK_Gorevler_Personeller_CreatedByPersonelId",
                table: "Gorevler",
                column: "CreatedByPersonelId",
                principalTable: "Personeller",
                principalColumn: "PersonelId");
        }
    }
}
