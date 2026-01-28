using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonelTakipSistemi.Migrations
{
    /// <inheritdoc />
    public partial class AddGorevDurumAciklamasiAndNullablePersonel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PersonelId",
                table: "Gorevler",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "DurumAciklamasi",
                table: "Gorevler",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 28, 15, 41, 28, 90, DateTimeKind.Local).AddTicks(907));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 28, 15, 41, 28, 90, DateTimeKind.Local).AddTicks(919));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 28, 15, 41, 28, 90, DateTimeKind.Local).AddTicks(920));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 28, 15, 41, 28, 90, DateTimeKind.Local).AddTicks(921));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 1,
                columns: new[] { "CreatedAt", "DurumAciklamasi" },
                values: new object[] { new DateTime(2026, 1, 28, 15, 41, 28, 90, DateTimeKind.Local).AddTicks(3640), null });

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 2,
                columns: new[] { "CreatedAt", "DurumAciklamasi" },
                values: new object[] { new DateTime(2026, 1, 28, 15, 41, 28, 90, DateTimeKind.Local).AddTicks(3652), null });

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 3,
                columns: new[] { "CreatedAt", "DurumAciklamasi" },
                values: new object[] { new DateTime(2026, 1, 28, 15, 41, 28, 90, DateTimeKind.Local).AddTicks(3655), null });

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 4,
                columns: new[] { "CreatedAt", "DurumAciklamasi" },
                values: new object[] { new DateTime(2026, 1, 28, 15, 41, 28, 90, DateTimeKind.Local).AddTicks(3657), null });

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 5,
                columns: new[] { "CreatedAt", "DurumAciklamasi" },
                values: new object[] { new DateTime(2026, 1, 28, 15, 41, 28, 90, DateTimeKind.Local).AddTicks(3659), null });

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 6,
                columns: new[] { "CreatedAt", "DurumAciklamasi" },
                values: new object[] { new DateTime(2026, 1, 28, 15, 41, 28, 90, DateTimeKind.Local).AddTicks(3663), null });

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 7,
                columns: new[] { "CreatedAt", "DurumAciklamasi" },
                values: new object[] { new DateTime(2026, 1, 28, 15, 41, 28, 90, DateTimeKind.Local).AddTicks(3665), null });

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 8,
                columns: new[] { "CreatedAt", "DurumAciklamasi" },
                values: new object[] { new DateTime(2026, 1, 28, 15, 41, 28, 90, DateTimeKind.Local).AddTicks(3667), null });

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 9,
                columns: new[] { "CreatedAt", "DurumAciklamasi" },
                values: new object[] { new DateTime(2026, 1, 28, 15, 41, 28, 90, DateTimeKind.Local).AddTicks(3669), null });

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 10,
                columns: new[] { "CreatedAt", "DurumAciklamasi" },
                values: new object[] { new DateTime(2026, 1, 28, 15, 41, 28, 90, DateTimeKind.Local).AddTicks(3671), null });

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 11,
                columns: new[] { "CreatedAt", "DurumAciklamasi" },
                values: new object[] { new DateTime(2026, 1, 28, 15, 41, 28, 90, DateTimeKind.Local).AddTicks(3672), null });

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 12,
                columns: new[] { "CreatedAt", "DurumAciklamasi" },
                values: new object[] { new DateTime(2026, 1, 28, 15, 41, 28, 90, DateTimeKind.Local).AddTicks(3674), null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DurumAciklamasi",
                table: "Gorevler");

            migrationBuilder.AlterColumn<int>(
                name: "PersonelId",
                table: "Gorevler",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 28, 13, 13, 25, 960, DateTimeKind.Local).AddTicks(3031));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 28, 13, 13, 25, 960, DateTimeKind.Local).AddTicks(3044));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 28, 13, 13, 25, 960, DateTimeKind.Local).AddTicks(3045));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 28, 13, 13, 25, 960, DateTimeKind.Local).AddTicks(3046));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 28, 13, 13, 25, 960, DateTimeKind.Local).AddTicks(5680));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 28, 13, 13, 25, 960, DateTimeKind.Local).AddTicks(5691));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 28, 13, 13, 25, 960, DateTimeKind.Local).AddTicks(5693));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 28, 13, 13, 25, 960, DateTimeKind.Local).AddTicks(5695));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 28, 13, 13, 25, 960, DateTimeKind.Local).AddTicks(5697));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 28, 13, 13, 25, 960, DateTimeKind.Local).AddTicks(5700));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 28, 13, 13, 25, 960, DateTimeKind.Local).AddTicks(5702));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 28, 13, 13, 25, 960, DateTimeKind.Local).AddTicks(5726));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 28, 13, 13, 25, 960, DateTimeKind.Local).AddTicks(5728));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 28, 13, 13, 25, 960, DateTimeKind.Local).AddTicks(5730));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 28, 13, 13, 25, 960, DateTimeKind.Local).AddTicks(5732));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 28, 13, 13, 25, 960, DateTimeKind.Local).AddTicks(5734));
        }
    }
}
