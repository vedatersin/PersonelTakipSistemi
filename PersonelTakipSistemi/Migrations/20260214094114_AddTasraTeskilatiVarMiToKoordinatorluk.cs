using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonelTakipSistemi.Migrations
{
    /// <inheritdoc />
    public partial class AddTasraTeskilatiVarMiToKoordinatorluk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "TasraTeskilatiVarMi",
                table: "Koordinatorlukler",
                type: "bit",
                nullable: false,
                defaultValue: false);

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
                columns: new[] { "CreatedAt", "TasraTeskilatiVarMi" },
                values: new object[] { new DateTime(2026, 2, 14, 12, 41, 14, 81, DateTimeKind.Local).AddTicks(2111), true });

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 3,
                columns: new[] { "CreatedAt", "TasraTeskilatiVarMi" },
                values: new object[] { new DateTime(2026, 2, 14, 12, 41, 14, 81, DateTimeKind.Local).AddTicks(2111), true });

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 4,
                columns: new[] { "CreatedAt", "TasraTeskilatiVarMi" },
                values: new object[] { new DateTime(2026, 2, 14, 12, 41, 14, 81, DateTimeKind.Local).AddTicks(2059), true });

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 5,
                columns: new[] { "CreatedAt", "TasraTeskilatiVarMi" },
                values: new object[] { new DateTime(2026, 2, 14, 12, 41, 14, 81, DateTimeKind.Local).AddTicks(2095), true });

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 6,
                columns: new[] { "CreatedAt", "TasraTeskilatiVarMi" },
                values: new object[] { new DateTime(2026, 2, 14, 12, 41, 14, 81, DateTimeKind.Local).AddTicks(2097), true });

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 7,
                columns: new[] { "CreatedAt", "TasraTeskilatiVarMi" },
                values: new object[] { new DateTime(2026, 2, 14, 12, 41, 14, 81, DateTimeKind.Local).AddTicks(2098), true });

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 8,
                columns: new[] { "CreatedAt", "TasraTeskilatiVarMi" },
                values: new object[] { new DateTime(2026, 2, 14, 12, 41, 14, 81, DateTimeKind.Local).AddTicks(2099), true });

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 9,
                columns: new[] { "CreatedAt", "TasraTeskilatiVarMi" },
                values: new object[] { new DateTime(2026, 2, 14, 12, 41, 14, 81, DateTimeKind.Local).AddTicks(2100), true });

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 10,
                columns: new[] { "CreatedAt", "TasraTeskilatiVarMi" },
                values: new object[] { new DateTime(2026, 2, 14, 12, 41, 14, 81, DateTimeKind.Local).AddTicks(2101), true });

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 11,
                columns: new[] { "CreatedAt", "TasraTeskilatiVarMi" },
                values: new object[] { new DateTime(2026, 2, 14, 12, 41, 14, 81, DateTimeKind.Local).AddTicks(2102), true });

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 12,
                columns: new[] { "CreatedAt", "TasraTeskilatiVarMi" },
                values: new object[] { new DateTime(2026, 2, 14, 12, 41, 14, 81, DateTimeKind.Local).AddTicks(2103), true });

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 13,
                columns: new[] { "CreatedAt", "TasraTeskilatiVarMi" },
                values: new object[] { new DateTime(2026, 2, 14, 12, 41, 14, 81, DateTimeKind.Local).AddTicks(2104), true });

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 14,
                columns: new[] { "CreatedAt", "TasraTeskilatiVarMi" },
                values: new object[] { new DateTime(2026, 2, 14, 12, 41, 14, 81, DateTimeKind.Local).AddTicks(2105), true });

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 15,
                columns: new[] { "CreatedAt", "TasraTeskilatiVarMi" },
                values: new object[] { new DateTime(2026, 2, 14, 12, 41, 14, 81, DateTimeKind.Local).AddTicks(2106), true });

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 16,
                columns: new[] { "CreatedAt", "TasraTeskilatiVarMi" },
                values: new object[] { new DateTime(2026, 2, 14, 12, 41, 14, 81, DateTimeKind.Local).AddTicks(2107), true });

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 17,
                columns: new[] { "CreatedAt", "TasraTeskilatiVarMi" },
                values: new object[] { new DateTime(2026, 2, 14, 12, 41, 14, 81, DateTimeKind.Local).AddTicks(2108), true });

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 18,
                columns: new[] { "CreatedAt", "TasraTeskilatiVarMi" },
                values: new object[] { new DateTime(2026, 2, 14, 12, 41, 14, 81, DateTimeKind.Local).AddTicks(2109), true });

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 19,
                columns: new[] { "CreatedAt", "TasraTeskilatiVarMi" },
                values: new object[] { new DateTime(2026, 2, 14, 12, 41, 14, 81, DateTimeKind.Local).AddTicks(2110), true });

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TasraTeskilatiVarMi",
                table: "Koordinatorlukler");

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 20, 7, 48, 875, DateTimeKind.Local).AddTicks(4687));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 20, 7, 48, 875, DateTimeKind.Local).AddTicks(4702));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 20, 7, 48, 875, DateTimeKind.Local).AddTicks(4704));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 20, 7, 48, 875, DateTimeKind.Local).AddTicks(4706));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 20, 7, 48, 875, DateTimeKind.Local).AddTicks(9857));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 20, 7, 48, 875, DateTimeKind.Local).AddTicks(9880));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 20, 7, 48, 875, DateTimeKind.Local).AddTicks(9884));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 20, 7, 48, 875, DateTimeKind.Local).AddTicks(9886));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 20, 7, 48, 875, DateTimeKind.Local).AddTicks(9889));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 20, 7, 48, 875, DateTimeKind.Local).AddTicks(9894));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 20, 7, 48, 875, DateTimeKind.Local).AddTicks(9896));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 20, 7, 48, 875, DateTimeKind.Local).AddTicks(9899));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 20, 7, 48, 875, DateTimeKind.Local).AddTicks(9902));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 20, 7, 48, 875, DateTimeKind.Local).AddTicks(9934));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 20, 7, 48, 875, DateTimeKind.Local).AddTicks(9937));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 20, 7, 48, 875, DateTimeKind.Local).AddTicks(9939));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 20, 7, 48, 868, DateTimeKind.Local).AddTicks(5201));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 20, 7, 48, 868, DateTimeKind.Local).AddTicks(5201));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 20, 7, 48, 868, DateTimeKind.Local).AddTicks(5155));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 20, 7, 48, 868, DateTimeKind.Local).AddTicks(5160));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 20, 7, 48, 868, DateTimeKind.Local).AddTicks(5161));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 20, 7, 48, 868, DateTimeKind.Local).AddTicks(5188));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 20, 7, 48, 868, DateTimeKind.Local).AddTicks(5189));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 20, 7, 48, 868, DateTimeKind.Local).AddTicks(5190));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 20, 7, 48, 868, DateTimeKind.Local).AddTicks(5191));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 20, 7, 48, 868, DateTimeKind.Local).AddTicks(5192));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 20, 7, 48, 868, DateTimeKind.Local).AddTicks(5193));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 20, 7, 48, 868, DateTimeKind.Local).AddTicks(5194));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 20, 7, 48, 868, DateTimeKind.Local).AddTicks(5195));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 20, 7, 48, 868, DateTimeKind.Local).AddTicks(5196));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 16,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 20, 7, 48, 868, DateTimeKind.Local).AddTicks(5197));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 17,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 20, 7, 48, 868, DateTimeKind.Local).AddTicks(5198));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 18,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 20, 7, 48, 868, DateTimeKind.Local).AddTicks(5199));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 19,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 20, 7, 48, 868, DateTimeKind.Local).AddTicks(5200));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 20, 7, 48, 868, DateTimeKind.Local).AddTicks(3643));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 20, 7, 48, 868, DateTimeKind.Local).AddTicks(3662));
        }
    }
}
