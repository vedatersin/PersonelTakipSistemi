using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonelTakipSistemi.Migrations
{
    /// <inheritdoc />
    public partial class AddRenkToGorevEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Renk",
                table: "GorevKategorileri",
                type: "nvarchar(7)",
                maxLength: 7,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Renk",
                table: "GorevDurumlari",
                type: "nvarchar(7)",
                maxLength: 7,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "GorevDurumlari",
                keyColumn: "GorevDurumId",
                keyValue: 1,
                column: "Renk",
                value: "#F59E0B");

            migrationBuilder.UpdateData(
                table: "GorevDurumlari",
                keyColumn: "GorevDurumId",
                keyValue: 2,
                column: "Renk",
                value: "#3B82F6");

            migrationBuilder.UpdateData(
                table: "GorevDurumlari",
                keyColumn: "GorevDurumId",
                keyValue: 3,
                column: "Renk",
                value: "#06B6D4");

            migrationBuilder.UpdateData(
                table: "GorevDurumlari",
                keyColumn: "GorevDurumId",
                keyValue: 4,
                column: "Renk",
                value: "#10B981");

            migrationBuilder.UpdateData(
                table: "GorevDurumlari",
                keyColumn: "GorevDurumId",
                keyValue: 5,
                column: "Renk",
                value: "#6B7280");

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Renk" },
                values: new object[] { new DateTime(2026, 2, 13, 13, 43, 33, 946, DateTimeKind.Local).AddTicks(9355), "#3B82F6" });

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Renk" },
                values: new object[] { new DateTime(2026, 2, 13, 13, 43, 33, 946, DateTimeKind.Local).AddTicks(9364), "#10B981" });

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 3,
                columns: new[] { "CreatedAt", "Renk" },
                values: new object[] { new DateTime(2026, 2, 13, 13, 43, 33, 946, DateTimeKind.Local).AddTicks(9366), "#F59E0B" });

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 4,
                columns: new[] { "CreatedAt", "Renk" },
                values: new object[] { new DateTime(2026, 2, 13, 13, 43, 33, 946, DateTimeKind.Local).AddTicks(9368), "#8B5CF6" });

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 13, 43, 33, 947, DateTimeKind.Local).AddTicks(2586));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 13, 43, 33, 947, DateTimeKind.Local).AddTicks(2601));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 13, 43, 33, 947, DateTimeKind.Local).AddTicks(2604));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 13, 43, 33, 947, DateTimeKind.Local).AddTicks(2606));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 13, 43, 33, 947, DateTimeKind.Local).AddTicks(2609));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 13, 43, 33, 947, DateTimeKind.Local).AddTicks(2611));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 13, 43, 33, 947, DateTimeKind.Local).AddTicks(2615));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 13, 43, 33, 947, DateTimeKind.Local).AddTicks(2640));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 13, 43, 33, 947, DateTimeKind.Local).AddTicks(2642));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 13, 43, 33, 947, DateTimeKind.Local).AddTicks(2645));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 13, 43, 33, 947, DateTimeKind.Local).AddTicks(2648));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 13, 43, 33, 947, DateTimeKind.Local).AddTicks(2649));

            migrationBuilder.UpdateData(
                table: "Komisyonlar",
                keyColumn: "KomisyonId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 13, 43, 33, 941, DateTimeKind.Local).AddTicks(7633));

            migrationBuilder.UpdateData(
                table: "Komisyonlar",
                keyColumn: "KomisyonId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 13, 43, 33, 941, DateTimeKind.Local).AddTicks(7636));

            migrationBuilder.UpdateData(
                table: "Komisyonlar",
                keyColumn: "KomisyonId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 13, 43, 33, 941, DateTimeKind.Local).AddTicks(7638));

            migrationBuilder.UpdateData(
                table: "Komisyonlar",
                keyColumn: "KomisyonId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 13, 43, 33, 941, DateTimeKind.Local).AddTicks(7639));

            migrationBuilder.UpdateData(
                table: "Komisyonlar",
                keyColumn: "KomisyonId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 13, 43, 33, 941, DateTimeKind.Local).AddTicks(7640));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 13, 43, 33, 941, DateTimeKind.Local).AddTicks(6634));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 13, 43, 33, 941, DateTimeKind.Local).AddTicks(6639));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 13, 43, 33, 941, DateTimeKind.Local).AddTicks(6640));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 13, 43, 33, 941, DateTimeKind.Local).AddTicks(5478));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 13, 43, 33, 941, DateTimeKind.Local).AddTicks(5493));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Renk",
                table: "GorevKategorileri");

            migrationBuilder.DropColumn(
                name: "Renk",
                table: "GorevDurumlari");

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 12, 30, 27, 313, DateTimeKind.Local).AddTicks(3128));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 12, 30, 27, 313, DateTimeKind.Local).AddTicks(3136));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 12, 30, 27, 313, DateTimeKind.Local).AddTicks(3137));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 12, 30, 27, 313, DateTimeKind.Local).AddTicks(3138));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 12, 30, 27, 313, DateTimeKind.Local).AddTicks(6138));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 12, 30, 27, 313, DateTimeKind.Local).AddTicks(6177));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 12, 30, 27, 313, DateTimeKind.Local).AddTicks(6181));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 12, 30, 27, 313, DateTimeKind.Local).AddTicks(6183));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 12, 30, 27, 313, DateTimeKind.Local).AddTicks(6185));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 12, 30, 27, 313, DateTimeKind.Local).AddTicks(6188));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 12, 30, 27, 313, DateTimeKind.Local).AddTicks(6190));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 12, 30, 27, 313, DateTimeKind.Local).AddTicks(6193));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 12, 30, 27, 313, DateTimeKind.Local).AddTicks(6195));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 12, 30, 27, 313, DateTimeKind.Local).AddTicks(6198));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 12, 30, 27, 313, DateTimeKind.Local).AddTicks(6200));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 12, 30, 27, 313, DateTimeKind.Local).AddTicks(6202));

            migrationBuilder.UpdateData(
                table: "Komisyonlar",
                keyColumn: "KomisyonId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 12, 30, 27, 310, DateTimeKind.Local).AddTicks(3915));

            migrationBuilder.UpdateData(
                table: "Komisyonlar",
                keyColumn: "KomisyonId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 12, 30, 27, 310, DateTimeKind.Local).AddTicks(3918));

            migrationBuilder.UpdateData(
                table: "Komisyonlar",
                keyColumn: "KomisyonId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 12, 30, 27, 310, DateTimeKind.Local).AddTicks(3919));

            migrationBuilder.UpdateData(
                table: "Komisyonlar",
                keyColumn: "KomisyonId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 12, 30, 27, 310, DateTimeKind.Local).AddTicks(3920));

            migrationBuilder.UpdateData(
                table: "Komisyonlar",
                keyColumn: "KomisyonId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 12, 30, 27, 310, DateTimeKind.Local).AddTicks(3921));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 12, 30, 27, 310, DateTimeKind.Local).AddTicks(2954));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 12, 30, 27, 310, DateTimeKind.Local).AddTicks(2960));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 12, 30, 27, 310, DateTimeKind.Local).AddTicks(2961));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 12, 30, 27, 310, DateTimeKind.Local).AddTicks(1810));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 12, 30, 27, 310, DateTimeKind.Local).AddTicks(1824));
        }
    }
}
