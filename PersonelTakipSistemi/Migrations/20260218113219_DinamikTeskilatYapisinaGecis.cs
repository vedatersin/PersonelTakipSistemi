using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonelTakipSistemi.Migrations
{
    /// <inheritdoc />
    public partial class DinamikTeskilatYapisinaGecis : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BagliMerkezTeskilatId",
                table: "Teskilatlar",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TasraOrgutlenmesiVarMi",
                table: "Teskilatlar",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Tur",
                table: "Teskilatlar",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 14, 32, 19, 421, DateTimeKind.Local).AddTicks(1744));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 14, 32, 19, 421, DateTimeKind.Local).AddTicks(1756));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 14, 32, 19, 421, DateTimeKind.Local).AddTicks(1758));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 14, 32, 19, 421, DateTimeKind.Local).AddTicks(1759));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 14, 32, 19, 421, DateTimeKind.Local).AddTicks(4595));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 14, 32, 19, 421, DateTimeKind.Local).AddTicks(4613));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 14, 32, 19, 421, DateTimeKind.Local).AddTicks(4616));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 14, 32, 19, 421, DateTimeKind.Local).AddTicks(4618));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 14, 32, 19, 421, DateTimeKind.Local).AddTicks(4620));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 14, 32, 19, 421, DateTimeKind.Local).AddTicks(4623));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 14, 32, 19, 421, DateTimeKind.Local).AddTicks(4625));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 14, 32, 19, 421, DateTimeKind.Local).AddTicks(4627));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 14, 32, 19, 421, DateTimeKind.Local).AddTicks(4630));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 14, 32, 19, 421, DateTimeKind.Local).AddTicks(4646));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 14, 32, 19, 421, DateTimeKind.Local).AddTicks(4649));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 14, 32, 19, 421, DateTimeKind.Local).AddTicks(4651));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 14, 32, 19, 416, DateTimeKind.Local).AddTicks(2786));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 14, 32, 19, 416, DateTimeKind.Local).AddTicks(2787));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 14, 32, 19, 416, DateTimeKind.Local).AddTicks(2746));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 14, 32, 19, 416, DateTimeKind.Local).AddTicks(2750));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 14, 32, 19, 416, DateTimeKind.Local).AddTicks(2751));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 14, 32, 19, 416, DateTimeKind.Local).AddTicks(2752));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 14, 32, 19, 416, DateTimeKind.Local).AddTicks(2753));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 14, 32, 19, 416, DateTimeKind.Local).AddTicks(2777));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 14, 32, 19, 416, DateTimeKind.Local).AddTicks(2778));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 14, 32, 19, 416, DateTimeKind.Local).AddTicks(2779));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 14, 32, 19, 416, DateTimeKind.Local).AddTicks(2780));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 14, 32, 19, 416, DateTimeKind.Local).AddTicks(2781));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 14, 32, 19, 416, DateTimeKind.Local).AddTicks(2782));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 14, 32, 19, 416, DateTimeKind.Local).AddTicks(2782));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 16,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 14, 32, 19, 416, DateTimeKind.Local).AddTicks(2783));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 17,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 14, 32, 19, 416, DateTimeKind.Local).AddTicks(2784));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 18,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 14, 32, 19, 416, DateTimeKind.Local).AddTicks(2785));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 19,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 14, 32, 19, 416, DateTimeKind.Local).AddTicks(2785));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 1,
                columns: new[] { "BagliMerkezTeskilatId", "CreatedAt", "TasraOrgutlenmesiVarMi", "Tur" },
                values: new object[] { null, new DateTime(2026, 2, 18, 14, 32, 19, 416, DateTimeKind.Local).AddTicks(1467), true, "Merkez" });

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 2,
                columns: new[] { "BagliMerkezTeskilatId", "CreatedAt", "TasraOrgutlenmesiVarMi", "Tur" },
                values: new object[] { 1, new DateTime(2026, 2, 18, 14, 32, 19, 416, DateTimeKind.Local).AddTicks(1482), false, "Taşra" });

            migrationBuilder.CreateIndex(
                name: "IX_Teskilatlar_BagliMerkezTeskilatId",
                table: "Teskilatlar",
                column: "BagliMerkezTeskilatId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teskilatlar_Teskilatlar_BagliMerkezTeskilatId",
                table: "Teskilatlar",
                column: "BagliMerkezTeskilatId",
                principalTable: "Teskilatlar",
                principalColumn: "TeskilatId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teskilatlar_Teskilatlar_BagliMerkezTeskilatId",
                table: "Teskilatlar");

            migrationBuilder.DropIndex(
                name: "IX_Teskilatlar_BagliMerkezTeskilatId",
                table: "Teskilatlar");

            migrationBuilder.DropColumn(
                name: "BagliMerkezTeskilatId",
                table: "Teskilatlar");

            migrationBuilder.DropColumn(
                name: "TasraOrgutlenmesiVarMi",
                table: "Teskilatlar");

            migrationBuilder.DropColumn(
                name: "Tur",
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
        }
    }
}
