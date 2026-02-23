using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonelTakipSistemi.Migrations
{
    /// <inheritdoc />
    public partial class AddGorevCreator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedByPersonelId",
                table: "Gorevler",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 12, 14, 40, 557, DateTimeKind.Local).AddTicks(7581));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 12, 14, 40, 557, DateTimeKind.Local).AddTicks(7588));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 12, 14, 40, 557, DateTimeKind.Local).AddTicks(7589));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 12, 14, 40, 557, DateTimeKind.Local).AddTicks(7590));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 1,
                columns: new[] { "CreatedAt", "CreatedByPersonelId" },
                values: new object[] { new DateTime(2026, 2, 23, 12, 14, 40, 558, DateTimeKind.Local).AddTicks(2174), null });

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 2,
                columns: new[] { "CreatedAt", "CreatedByPersonelId" },
                values: new object[] { new DateTime(2026, 2, 23, 12, 14, 40, 558, DateTimeKind.Local).AddTicks(2196), null });

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 3,
                columns: new[] { "CreatedAt", "CreatedByPersonelId" },
                values: new object[] { new DateTime(2026, 2, 23, 12, 14, 40, 558, DateTimeKind.Local).AddTicks(2199), null });

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 4,
                columns: new[] { "CreatedAt", "CreatedByPersonelId" },
                values: new object[] { new DateTime(2026, 2, 23, 12, 14, 40, 558, DateTimeKind.Local).AddTicks(2202), null });

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 5,
                columns: new[] { "CreatedAt", "CreatedByPersonelId" },
                values: new object[] { new DateTime(2026, 2, 23, 12, 14, 40, 558, DateTimeKind.Local).AddTicks(2204), null });

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 6,
                columns: new[] { "CreatedAt", "CreatedByPersonelId" },
                values: new object[] { new DateTime(2026, 2, 23, 12, 14, 40, 558, DateTimeKind.Local).AddTicks(2207), null });

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 7,
                columns: new[] { "CreatedAt", "CreatedByPersonelId" },
                values: new object[] { new DateTime(2026, 2, 23, 12, 14, 40, 558, DateTimeKind.Local).AddTicks(2209), null });

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 8,
                columns: new[] { "CreatedAt", "CreatedByPersonelId" },
                values: new object[] { new DateTime(2026, 2, 23, 12, 14, 40, 558, DateTimeKind.Local).AddTicks(2211), null });

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 9,
                columns: new[] { "CreatedAt", "CreatedByPersonelId" },
                values: new object[] { new DateTime(2026, 2, 23, 12, 14, 40, 558, DateTimeKind.Local).AddTicks(2213), null });

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 10,
                columns: new[] { "CreatedAt", "CreatedByPersonelId" },
                values: new object[] { new DateTime(2026, 2, 23, 12, 14, 40, 558, DateTimeKind.Local).AddTicks(2216), null });

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 11,
                columns: new[] { "CreatedAt", "CreatedByPersonelId" },
                values: new object[] { new DateTime(2026, 2, 23, 12, 14, 40, 558, DateTimeKind.Local).AddTicks(2217), null });

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 12,
                columns: new[] { "CreatedAt", "CreatedByPersonelId" },
                values: new object[] { new DateTime(2026, 2, 23, 12, 14, 40, 558, DateTimeKind.Local).AddTicks(2220), null });

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 12, 14, 40, 553, DateTimeKind.Local).AddTicks(6187));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 12, 14, 40, 553, DateTimeKind.Local).AddTicks(6187));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 12, 14, 40, 553, DateTimeKind.Local).AddTicks(6170));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 12, 14, 40, 553, DateTimeKind.Local).AddTicks(6174));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 12, 14, 40, 553, DateTimeKind.Local).AddTicks(6175));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 12, 14, 40, 553, DateTimeKind.Local).AddTicks(6176));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 12, 14, 40, 553, DateTimeKind.Local).AddTicks(6177));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 12, 14, 40, 553, DateTimeKind.Local).AddTicks(6178));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 12, 14, 40, 553, DateTimeKind.Local).AddTicks(6179));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 12, 14, 40, 553, DateTimeKind.Local).AddTicks(6179));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 12, 14, 40, 553, DateTimeKind.Local).AddTicks(6180));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 12, 14, 40, 553, DateTimeKind.Local).AddTicks(6181));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 12, 14, 40, 553, DateTimeKind.Local).AddTicks(6182));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 12, 14, 40, 553, DateTimeKind.Local).AddTicks(6183));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 16,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 12, 14, 40, 553, DateTimeKind.Local).AddTicks(6183));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 17,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 12, 14, 40, 553, DateTimeKind.Local).AddTicks(6184));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 18,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 12, 14, 40, 553, DateTimeKind.Local).AddTicks(6185));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 19,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 12, 14, 40, 553, DateTimeKind.Local).AddTicks(6186));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 12, 14, 40, 553, DateTimeKind.Local).AddTicks(5166));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 12, 14, 40, 553, DateTimeKind.Local).AddTicks(5180));

            migrationBuilder.CreateIndex(
                name: "IX_Gorevler_CreatedByPersonelId",
                table: "Gorevler",
                column: "CreatedByPersonelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Gorevler_Personeller_CreatedByPersonelId",
                table: "Gorevler",
                column: "CreatedByPersonelId",
                principalTable: "Personeller",
                principalColumn: "PersonelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gorevler_Personeller_CreatedByPersonelId",
                table: "Gorevler");

            migrationBuilder.DropIndex(
                name: "IX_Gorevler_CreatedByPersonelId",
                table: "Gorevler");

            migrationBuilder.DropColumn(
                name: "CreatedByPersonelId",
                table: "Gorevler");

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 1, 6, 17, 397, DateTimeKind.Local).AddTicks(7640));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 1, 6, 17, 397, DateTimeKind.Local).AddTicks(7653));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 1, 6, 17, 397, DateTimeKind.Local).AddTicks(7654));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 1, 6, 17, 397, DateTimeKind.Local).AddTicks(7655));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 1, 6, 17, 398, DateTimeKind.Local).AddTicks(549));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 1, 6, 17, 398, DateTimeKind.Local).AddTicks(579));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 1, 6, 17, 398, DateTimeKind.Local).AddTicks(583));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 1, 6, 17, 398, DateTimeKind.Local).AddTicks(585));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 1, 6, 17, 398, DateTimeKind.Local).AddTicks(587));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 1, 6, 17, 398, DateTimeKind.Local).AddTicks(591));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 1, 6, 17, 398, DateTimeKind.Local).AddTicks(594));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 1, 6, 17, 398, DateTimeKind.Local).AddTicks(596));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 1, 6, 17, 398, DateTimeKind.Local).AddTicks(637));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 1, 6, 17, 398, DateTimeKind.Local).AddTicks(640));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 1, 6, 17, 398, DateTimeKind.Local).AddTicks(641));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 1, 6, 17, 398, DateTimeKind.Local).AddTicks(643));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 1, 6, 17, 392, DateTimeKind.Local).AddTicks(3826));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 1, 6, 17, 392, DateTimeKind.Local).AddTicks(3827));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 1, 6, 17, 392, DateTimeKind.Local).AddTicks(3809));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 1, 6, 17, 392, DateTimeKind.Local).AddTicks(3813));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 1, 6, 17, 392, DateTimeKind.Local).AddTicks(3814));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 1, 6, 17, 392, DateTimeKind.Local).AddTicks(3815));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 1, 6, 17, 392, DateTimeKind.Local).AddTicks(3816));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 1, 6, 17, 392, DateTimeKind.Local).AddTicks(3817));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 1, 6, 17, 392, DateTimeKind.Local).AddTicks(3818));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 1, 6, 17, 392, DateTimeKind.Local).AddTicks(3819));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 1, 6, 17, 392, DateTimeKind.Local).AddTicks(3820));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 1, 6, 17, 392, DateTimeKind.Local).AddTicks(3821));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 1, 6, 17, 392, DateTimeKind.Local).AddTicks(3822));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 1, 6, 17, 392, DateTimeKind.Local).AddTicks(3822));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 16,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 1, 6, 17, 392, DateTimeKind.Local).AddTicks(3823));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 17,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 1, 6, 17, 392, DateTimeKind.Local).AddTicks(3824));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 18,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 1, 6, 17, 392, DateTimeKind.Local).AddTicks(3825));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 19,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 1, 6, 17, 392, DateTimeKind.Local).AddTicks(3825));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 1, 6, 17, 392, DateTimeKind.Local).AddTicks(2727));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 1, 6, 17, 392, DateTimeKind.Local).AddTicks(2743));
        }
    }
}
