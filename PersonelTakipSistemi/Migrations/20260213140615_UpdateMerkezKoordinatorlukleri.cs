using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PersonelTakipSistemi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMerkezKoordinatorlukleri : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Ensure Daire exists
             migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT 1 FROM DaireBaskanliklari WHERE Id = 9)
                BEGIN
                    SET IDENTITY_INSERT DaireBaskanliklari ON;
                    INSERT INTO DaireBaskanliklari (Id, Ad, IsActive) VALUES (9, 'Programlar ve Öğretim Materyalleri Daire Başkanlığı', 1);
                    SET IDENTITY_INSERT DaireBaskanliklari OFF;
                END");

            // Ensure Teskilat exists
            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT 1 FROM Teskilatlar WHERE TeskilatId = 1)
                BEGIN
                    SET IDENTITY_INSERT Teskilatlar ON;
                    INSERT INTO Teskilatlar (TeskilatId, Ad, DaireBaskanligiId, IsActive, CreatedAt) VALUES (1, 'Merkez', 9, 1, GETDATE());
                    SET IDENTITY_INSERT Teskilatlar OFF;
                END");

            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT 1 FROM Teskilatlar WHERE TeskilatId = 2)
                BEGIN
                    SET IDENTITY_INSERT Teskilatlar ON;
                    INSERT INTO Teskilatlar (TeskilatId, Ad, DaireBaskanligiId, IsActive, CreatedAt) VALUES (2, 'Taşra', 9, 1, GETDATE());
                    SET IDENTITY_INSERT Teskilatlar OFF;
                END");

            migrationBuilder.Sql("DELETE FROM PersonelKurumsalRolAtamalari WHERE KoordinatorlukId = 1");

            migrationBuilder.DeleteData(
                table: "Komisyonlar",
                keyColumn: "KomisyonId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Komisyonlar",
                keyColumn: "KomisyonId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Komisyonlar",
                keyColumn: "KomisyonId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 1);

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

            migrationBuilder.InsertData(
                table: "Koordinatorlukler",
                columns: new[] { "KoordinatorlukId", "Aciklama", "Ad", "BaskanPersonelId", "CreatedAt", "IlId", "IsActive", "TeskilatId" },
                values: new object[,]
                {
                    { 4, null, "Fen Bilimleri Birim Koordinatörlüğü", null, new DateTime(2026, 2, 13, 17, 6, 15, 591, DateTimeKind.Local).AddTicks(8352), null, true, 1 },
                    { 5, null, "İngilizce Birim Koordinatörlüğü", null, new DateTime(2026, 2, 13, 17, 6, 15, 591, DateTimeKind.Local).AddTicks(8356), null, true, 1 },
                    { 6, null, "İlkokul Türkçe Birim Koordinatörlüğü", null, new DateTime(2026, 2, 13, 17, 6, 15, 591, DateTimeKind.Local).AddTicks(8357), null, true, 1 },
                    { 7, null, "İlkokul Hayat Bilgisi Birim Koordinatörlüğü", null, new DateTime(2026, 2, 13, 17, 6, 15, 591, DateTimeKind.Local).AddTicks(8358), null, true, 1 },
                    { 8, null, "Ortaokul Matematik Birim Koordinatörlüğü", null, new DateTime(2026, 2, 13, 17, 6, 15, 591, DateTimeKind.Local).AddTicks(8359), null, true, 1 },
                    { 9, null, "İlkokul Matematik Birim Koordinatörlüğü", null, new DateTime(2026, 2, 13, 17, 6, 15, 591, DateTimeKind.Local).AddTicks(8360), null, true, 1 },
                    { 10, null, "Ortaokul Türkçe Birim Koordinatörlüğü", null, new DateTime(2026, 2, 13, 17, 6, 15, 591, DateTimeKind.Local).AddTicks(8361), null, true, 1 },
                    { 11, null, "Sosyal Bilgiler Birim Koordinatörlüğü", null, new DateTime(2026, 2, 13, 17, 6, 15, 591, DateTimeKind.Local).AddTicks(8362), null, true, 1 },
                    { 12, null, "T.C. İnkılap Tarihi ve Atatürkçülük Birim Koordinatörlüğü", null, new DateTime(2026, 2, 13, 17, 6, 15, 591, DateTimeKind.Local).AddTicks(8362), null, true, 1 },
                    { 13, null, "Almanca Birim Koordinatörlüğü", null, new DateTime(2026, 2, 13, 17, 6, 15, 591, DateTimeKind.Local).AddTicks(8363), null, true, 1 },
                    { 14, null, "Görsel Tasarım Birim Koordinatörlüğü", null, new DateTime(2026, 2, 13, 17, 6, 15, 591, DateTimeKind.Local).AddTicks(8364), null, true, 1 },
                    { 15, null, "Mebi Dijital Birim Koordinatörlüğü", null, new DateTime(2026, 2, 13, 17, 6, 15, 591, DateTimeKind.Local).AddTicks(8365), null, true, 1 },
                    { 16, null, "Müzik Birim Koordinatörlüğü", null, new DateTime(2026, 2, 13, 17, 6, 15, 591, DateTimeKind.Local).AddTicks(8365), null, true, 1 },
                    { 17, null, "Uzmanlar Birim Koordinatörlüğü", null, new DateTime(2026, 2, 13, 17, 6, 15, 591, DateTimeKind.Local).AddTicks(8366), null, true, 1 },
                    { 18, null, "BÖTE Birim Koordinatörlüğü", null, new DateTime(2026, 2, 13, 17, 6, 15, 591, DateTimeKind.Local).AddTicks(8367), null, true, 1 },
                    { 19, null, "Dil İnceleme Birim Koordinatörlüğü", null, new DateTime(2026, 2, 13, 17, 6, 15, 591, DateTimeKind.Local).AddTicks(8368), null, true, 1 }
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 19);

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 13, 43, 33, 946, DateTimeKind.Local).AddTicks(9355));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 13, 43, 33, 946, DateTimeKind.Local).AddTicks(9364));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 13, 43, 33, 946, DateTimeKind.Local).AddTicks(9366));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 13, 43, 33, 946, DateTimeKind.Local).AddTicks(9368));

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
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 13, 43, 33, 941, DateTimeKind.Local).AddTicks(6639));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 13, 43, 33, 941, DateTimeKind.Local).AddTicks(6640));

            // Ghost Koordinatorluk Insert Removed manually


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

            // Ghost Komisyon Insert Removed manually

        }
    }
}
