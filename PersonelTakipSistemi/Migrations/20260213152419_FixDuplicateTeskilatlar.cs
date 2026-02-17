using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonelTakipSistemi.Migrations
{
    /// <inheritdoc />
    public partial class FixDuplicateTeskilatlar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Fix References: Move any child records from duplicate parents to the correct ones (ID 1 & 2)
            migrationBuilder.Sql(@"
                UPDATE Koordinatorlukler SET TeskilatId = 1 WHERE TeskilatId IN (SELECT TeskilatId FROM Teskilatlar WHERE Ad LIKE N'%Merkez%' AND TeskilatId <> 1);
                UPDATE Koordinatorlukler SET TeskilatId = 2 WHERE TeskilatId IN (SELECT TeskilatId FROM Teskilatlar WHERE (Ad LIKE N'%Taşra%' OR Ad LIKE N'%Tasra%') AND TeskilatId <> 2);
                
                UPDATE PersonelTeskilatlar SET TeskilatId = 1 WHERE TeskilatId IN (SELECT TeskilatId FROM Teskilatlar WHERE Ad LIKE N'%Merkez%' AND TeskilatId <> 1);
                UPDATE PersonelTeskilatlar SET TeskilatId = 2 WHERE TeskilatId IN (SELECT TeskilatId FROM Teskilatlar WHERE (Ad LIKE N'%Taşra%' OR Ad LIKE N'%Tasra%') AND TeskilatId <> 2);
            ");

            // 2. Delete Duplicates
            migrationBuilder.Sql(@"
                DELETE FROM Teskilatlar WHERE Ad LIKE N'%Merkez%' AND TeskilatId <> 1;
                DELETE FROM Teskilatlar WHERE (Ad LIKE N'%Taşra%' OR Ad LIKE N'%Tasra%') AND TeskilatId <> 2;
            ");

            // 3. Ensure Names are correct for ID 1 and 2
            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 1,
                column: "Ad",
                value: "Merkez");

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 2,
                column: "Ad",
                value: "Taşra");

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 916, DateTimeKind.Local).AddTicks(4503));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 916, DateTimeKind.Local).AddTicks(4515));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 916, DateTimeKind.Local).AddTicks(4517));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 916, DateTimeKind.Local).AddTicks(4518));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 916, DateTimeKind.Local).AddTicks(7857));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 916, DateTimeKind.Local).AddTicks(7873));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 916, DateTimeKind.Local).AddTicks(7876));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 916, DateTimeKind.Local).AddTicks(7878));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 916, DateTimeKind.Local).AddTicks(7881));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 916, DateTimeKind.Local).AddTicks(7884));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 916, DateTimeKind.Local).AddTicks(7888));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 916, DateTimeKind.Local).AddTicks(7890));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 916, DateTimeKind.Local).AddTicks(7892));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 916, DateTimeKind.Local).AddTicks(7895));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 916, DateTimeKind.Local).AddTicks(7896));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 916, DateTimeKind.Local).AddTicks(7899));

            migrationBuilder.UpdateData(
                table: "Komisyonlar",
                keyColumn: "KomisyonId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 912, DateTimeKind.Local).AddTicks(9918));

            migrationBuilder.UpdateData(
                table: "Komisyonlar",
                keyColumn: "KomisyonId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 912, DateTimeKind.Local).AddTicks(9924));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 912, DateTimeKind.Local).AddTicks(8389));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 912, DateTimeKind.Local).AddTicks(8391));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 912, DateTimeKind.Local).AddTicks(8335));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 912, DateTimeKind.Local).AddTicks(8341));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 912, DateTimeKind.Local).AddTicks(8343));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 912, DateTimeKind.Local).AddTicks(8344));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 912, DateTimeKind.Local).AddTicks(8346));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 912, DateTimeKind.Local).AddTicks(8347));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 912, DateTimeKind.Local).AddTicks(8348));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 912, DateTimeKind.Local).AddTicks(8350));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 912, DateTimeKind.Local).AddTicks(8351));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 912, DateTimeKind.Local).AddTicks(8352));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 912, DateTimeKind.Local).AddTicks(8354));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 912, DateTimeKind.Local).AddTicks(8355));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 16,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 912, DateTimeKind.Local).AddTicks(8356));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 17,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 912, DateTimeKind.Local).AddTicks(8357));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 18,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 912, DateTimeKind.Local).AddTicks(8358));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 19,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 912, DateTimeKind.Local).AddTicks(8359));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 912, DateTimeKind.Local).AddTicks(6480));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 18, 24, 18, 912, DateTimeKind.Local).AddTicks(6496));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 17, 6, 15, 595, DateTimeKind.Local).AddTicks(2978));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 17, 6, 15, 595, DateTimeKind.Local).AddTicks(2987));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 17, 6, 15, 595, DateTimeKind.Local).AddTicks(2988));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 17, 6, 15, 595, DateTimeKind.Local).AddTicks(2989));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 17, 6, 15, 595, DateTimeKind.Local).AddTicks(6325));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 17, 6, 15, 595, DateTimeKind.Local).AddTicks(6338));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 17, 6, 15, 595, DateTimeKind.Local).AddTicks(6364));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 17, 6, 15, 595, DateTimeKind.Local).AddTicks(6367));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 17, 6, 15, 595, DateTimeKind.Local).AddTicks(6369));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 17, 6, 15, 595, DateTimeKind.Local).AddTicks(6372));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 17, 6, 15, 595, DateTimeKind.Local).AddTicks(6374));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 17, 6, 15, 595, DateTimeKind.Local).AddTicks(6376));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 17, 6, 15, 595, DateTimeKind.Local).AddTicks(6378));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 17, 6, 15, 595, DateTimeKind.Local).AddTicks(6381));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 17, 6, 15, 595, DateTimeKind.Local).AddTicks(6382));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 17, 6, 15, 595, DateTimeKind.Local).AddTicks(6384));

            migrationBuilder.UpdateData(
                table: "Komisyonlar",
                keyColumn: "KomisyonId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 17, 6, 15, 591, DateTimeKind.Local).AddTicks(9372));

            migrationBuilder.UpdateData(
                table: "Komisyonlar",
                keyColumn: "KomisyonId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 17, 6, 15, 591, DateTimeKind.Local).AddTicks(9375));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 17, 6, 15, 591, DateTimeKind.Local).AddTicks(8368));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 17, 6, 15, 591, DateTimeKind.Local).AddTicks(8369));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 17, 6, 15, 591, DateTimeKind.Local).AddTicks(8352));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 17, 6, 15, 591, DateTimeKind.Local).AddTicks(8356));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 17, 6, 15, 591, DateTimeKind.Local).AddTicks(8357));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 17, 6, 15, 591, DateTimeKind.Local).AddTicks(8358));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 17, 6, 15, 591, DateTimeKind.Local).AddTicks(8359));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 17, 6, 15, 591, DateTimeKind.Local).AddTicks(8360));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 17, 6, 15, 591, DateTimeKind.Local).AddTicks(8361));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 17, 6, 15, 591, DateTimeKind.Local).AddTicks(8362));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 17, 6, 15, 591, DateTimeKind.Local).AddTicks(8362));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 17, 6, 15, 591, DateTimeKind.Local).AddTicks(8363));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 17, 6, 15, 591, DateTimeKind.Local).AddTicks(8364));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 17, 6, 15, 591, DateTimeKind.Local).AddTicks(8365));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 16,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 17, 6, 15, 591, DateTimeKind.Local).AddTicks(8365));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 17,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 17, 6, 15, 591, DateTimeKind.Local).AddTicks(8366));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 18,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 17, 6, 15, 591, DateTimeKind.Local).AddTicks(8367));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 19,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 17, 6, 15, 591, DateTimeKind.Local).AddTicks(8368));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 17, 6, 15, 591, DateTimeKind.Local).AddTicks(7176));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 17, 6, 15, 591, DateTimeKind.Local).AddTicks(7191));
        }
    }
}
