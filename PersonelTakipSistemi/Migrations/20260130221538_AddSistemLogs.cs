using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonelTakipSistemi.Migrations
{
    /// <inheritdoc />
    public partial class AddSistemLogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SistemLoglar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonelId = table.Column<int>(type: "int", nullable: true),
                    TcKimlikNo = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    KullaniciAdSoyad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IpAdresi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Tarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IslemTuru = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Detay = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SistemLoglar", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SistemLoglar");

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
                column: "CreatedAt",
                value: new DateTime(2026, 1, 29, 14, 16, 56, 391, DateTimeKind.Local).AddTicks(8319));

            migrationBuilder.UpdateData(
                table: "Komisyonlar",
                keyColumn: "KomisyonId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 29, 14, 16, 56, 391, DateTimeKind.Local).AddTicks(8324));

            migrationBuilder.UpdateData(
                table: "Komisyonlar",
                keyColumn: "KomisyonId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 29, 14, 16, 56, 391, DateTimeKind.Local).AddTicks(8325));

            migrationBuilder.UpdateData(
                table: "Komisyonlar",
                keyColumn: "KomisyonId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 29, 14, 16, 56, 391, DateTimeKind.Local).AddTicks(8326));

            migrationBuilder.UpdateData(
                table: "Komisyonlar",
                keyColumn: "KomisyonId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 29, 14, 16, 56, 391, DateTimeKind.Local).AddTicks(8327));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 29, 14, 16, 56, 391, DateTimeKind.Local).AddTicks(7204));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 29, 14, 16, 56, 391, DateTimeKind.Local).AddTicks(7209));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 29, 14, 16, 56, 391, DateTimeKind.Local).AddTicks(7210));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 29, 14, 16, 56, 391, DateTimeKind.Local).AddTicks(6042));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 29, 14, 16, 56, 391, DateTimeKind.Local).AddTicks(6054));
        }
    }
}
