using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonelTakipSistemi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUnitEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Aciklama",
                table: "Teskilatlar",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Teskilatlar",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Teskilatlar",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Aciklama",
                table: "Koordinatorlukler",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Koordinatorlukler",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "IlId",
                table: "Koordinatorlukler",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Koordinatorlukler",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Aciklama",
                table: "Komisyonlar",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Komisyonlar",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Komisyonlar",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 29, 14, 16, 56, 394, DateTimeKind.Local).AddTicks(9596));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 29, 14, 16, 56, 394, DateTimeKind.Local).AddTicks(9608));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 29, 14, 16, 56, 394, DateTimeKind.Local).AddTicks(9609));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 29, 14, 16, 56, 394, DateTimeKind.Local).AddTicks(9610));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 29, 14, 16, 56, 395, DateTimeKind.Local).AddTicks(3561));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 29, 14, 16, 56, 395, DateTimeKind.Local).AddTicks(3577));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 29, 14, 16, 56, 395, DateTimeKind.Local).AddTicks(3580));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 29, 14, 16, 56, 395, DateTimeKind.Local).AddTicks(3582));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 29, 14, 16, 56, 395, DateTimeKind.Local).AddTicks(3584));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 29, 14, 16, 56, 395, DateTimeKind.Local).AddTicks(3588));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 29, 14, 16, 56, 395, DateTimeKind.Local).AddTicks(3590));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 29, 14, 16, 56, 395, DateTimeKind.Local).AddTicks(3592));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 29, 14, 16, 56, 395, DateTimeKind.Local).AddTicks(3594));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 29, 14, 16, 56, 395, DateTimeKind.Local).AddTicks(3597));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 29, 14, 16, 56, 395, DateTimeKind.Local).AddTicks(3598));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 29, 14, 16, 56, 395, DateTimeKind.Local).AddTicks(3600));

            migrationBuilder.UpdateData(
                table: "Komisyonlar",
                keyColumn: "KomisyonId",
                keyValue: 1,
                columns: new[] { "Aciklama", "CreatedAt", "IsActive" },
                values: new object[] { null, new DateTime(2026, 1, 29, 14, 16, 56, 391, DateTimeKind.Local).AddTicks(8319), true });

            migrationBuilder.UpdateData(
                table: "Komisyonlar",
                keyColumn: "KomisyonId",
                keyValue: 2,
                columns: new[] { "Aciklama", "CreatedAt", "IsActive" },
                values: new object[] { null, new DateTime(2026, 1, 29, 14, 16, 56, 391, DateTimeKind.Local).AddTicks(8324), true });

            migrationBuilder.UpdateData(
                table: "Komisyonlar",
                keyColumn: "KomisyonId",
                keyValue: 3,
                columns: new[] { "Aciklama", "CreatedAt", "IsActive" },
                values: new object[] { null, new DateTime(2026, 1, 29, 14, 16, 56, 391, DateTimeKind.Local).AddTicks(8325), true });

            migrationBuilder.UpdateData(
                table: "Komisyonlar",
                keyColumn: "KomisyonId",
                keyValue: 4,
                columns: new[] { "Aciklama", "CreatedAt", "IsActive" },
                values: new object[] { null, new DateTime(2026, 1, 29, 14, 16, 56, 391, DateTimeKind.Local).AddTicks(8326), true });

            migrationBuilder.UpdateData(
                table: "Komisyonlar",
                keyColumn: "KomisyonId",
                keyValue: 5,
                columns: new[] { "Aciklama", "CreatedAt", "IsActive" },
                values: new object[] { null, new DateTime(2026, 1, 29, 14, 16, 56, 391, DateTimeKind.Local).AddTicks(8327), true });

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 1,
                columns: new[] { "Aciklama", "CreatedAt", "IlId", "IsActive" },
                values: new object[] { null, new DateTime(2026, 1, 29, 14, 16, 56, 391, DateTimeKind.Local).AddTicks(7204), null, true });

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 2,
                columns: new[] { "Aciklama", "CreatedAt", "IlId", "IsActive" },
                values: new object[] { null, new DateTime(2026, 1, 29, 14, 16, 56, 391, DateTimeKind.Local).AddTicks(7209), null, true });

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 3,
                columns: new[] { "Aciklama", "CreatedAt", "IlId", "IsActive" },
                values: new object[] { null, new DateTime(2026, 1, 29, 14, 16, 56, 391, DateTimeKind.Local).AddTicks(7210), null, true });

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 1,
                columns: new[] { "Aciklama", "CreatedAt", "IsActive" },
                values: new object[] { null, new DateTime(2026, 1, 29, 14, 16, 56, 391, DateTimeKind.Local).AddTicks(6042), true });

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 2,
                columns: new[] { "Aciklama", "CreatedAt", "IsActive" },
                values: new object[] { null, new DateTime(2026, 1, 29, 14, 16, 56, 391, DateTimeKind.Local).AddTicks(6054), true });

            migrationBuilder.CreateIndex(
                name: "IX_Koordinatorlukler_IlId",
                table: "Koordinatorlukler",
                column: "IlId");

            migrationBuilder.AddForeignKey(
                name: "FK_Koordinatorlukler_Iller_IlId",
                table: "Koordinatorlukler",
                column: "IlId",
                principalTable: "Iller",
                principalColumn: "IlId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Koordinatorlukler_Iller_IlId",
                table: "Koordinatorlukler");

            migrationBuilder.DropIndex(
                name: "IX_Koordinatorlukler_IlId",
                table: "Koordinatorlukler");

            migrationBuilder.DropColumn(
                name: "Aciklama",
                table: "Teskilatlar");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Teskilatlar");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Teskilatlar");

            migrationBuilder.DropColumn(
                name: "Aciklama",
                table: "Koordinatorlukler");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Koordinatorlukler");

            migrationBuilder.DropColumn(
                name: "IlId",
                table: "Koordinatorlukler");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Koordinatorlukler");

            migrationBuilder.DropColumn(
                name: "Aciklama",
                table: "Komisyonlar");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Komisyonlar");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Komisyonlar");

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
                column: "CreatedAt",
                value: new DateTime(2026, 1, 28, 15, 41, 28, 90, DateTimeKind.Local).AddTicks(3640));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 28, 15, 41, 28, 90, DateTimeKind.Local).AddTicks(3652));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 28, 15, 41, 28, 90, DateTimeKind.Local).AddTicks(3655));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 28, 15, 41, 28, 90, DateTimeKind.Local).AddTicks(3657));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 28, 15, 41, 28, 90, DateTimeKind.Local).AddTicks(3659));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 28, 15, 41, 28, 90, DateTimeKind.Local).AddTicks(3663));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 28, 15, 41, 28, 90, DateTimeKind.Local).AddTicks(3665));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 28, 15, 41, 28, 90, DateTimeKind.Local).AddTicks(3667));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 28, 15, 41, 28, 90, DateTimeKind.Local).AddTicks(3669));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 28, 15, 41, 28, 90, DateTimeKind.Local).AddTicks(3671));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 28, 15, 41, 28, 90, DateTimeKind.Local).AddTicks(3672));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 28, 15, 41, 28, 90, DateTimeKind.Local).AddTicks(3674));
        }
    }
}
