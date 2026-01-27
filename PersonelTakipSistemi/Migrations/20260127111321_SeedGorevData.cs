using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PersonelTakipSistemi.Migrations
{
    /// <inheritdoc />
    public partial class SeedGorevData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 27, 14, 13, 20, 840, DateTimeKind.Local).AddTicks(8365));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 27, 14, 13, 20, 840, DateTimeKind.Local).AddTicks(8376));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 27, 14, 13, 20, 840, DateTimeKind.Local).AddTicks(8377));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 27, 14, 13, 20, 840, DateTimeKind.Local).AddTicks(8378));

            migrationBuilder.Sql(@"
                DECLARE @Pid INT = (SELECT TOP 1 PersonelId FROM Personeller);
                
                IF @Pid IS NOT NULL
                BEGIN
                    SET IDENTITY_INSERT Gorevler ON;
                    INSERT INTO Gorevler (GorevId, Ad, Aciklama, KategoriId, PersonelId, BirimId, Durum, BaslangicTarihi, BitisTarihi, IsActive, CreatedAt)
                    VALUES 
                    (1, 'Matematik 9 Kitap Dizgisi', 'Dizgi taslağını hazırla', 1, @Pid, 3, 1, '2025-11-01', '2025-11-20', 1, GETDATE()),
                    (2, 'Fizik 10 Kapak Tasarımı', 'Kapak görseli revizesi', 1, @Pid, 3, 2, '2025-11-05', '2025-11-08', 1, GETDATE()),
                    (3, 'Kimya 11 Yazım Denetimi', 'Yazım hatalarının kontrolü', 1, @Pid, 2, 0, '2025-12-01', NULL, 1, GETDATE()),
                    (4, 'LGS Soru Bankası', 'Soru girişleri', 2, @Pid, 2, 1, '2025-11-15', '2025-12-15', 1, GETDATE()),
                    (5, 'YKS Deneme Seti', 'Baskı öncesi kontrol', 2, @Pid, 3, 1, '2025-11-25', NULL, 1, GETDATE()),
                    (6, 'Etkinlik Yaprakları', 'İlkokul seviyesi görselleştirme', 2, @Pid, 3, 2, '2025-10-20', '2025-10-25', 1, GETDATE()),
                    (7, 'EBA Video Montaj', 'Ders videoları kurgusu', 3, @Pid, 1, 0, '2025-12-05', NULL, 1, GETDATE()),
                    (8, 'Animasyon Karakterleri', 'Karakter çizimleri', 3, @Pid, 3, 1, '2025-11-10', '2025-12-30', 1, GETDATE()),
                    (9, 'Seslendirme Kayıtları', 'Stüdyo planlaması', 3, @Pid, 2, 2, '2025-11-01', '2025-11-02', 1, GETDATE()),
                    (10, 'Müfredat İncelemesi', 'Talim Terbiye notları', 4, @Pid, 2, 1, '2025-12-10', NULL, 1, GETDATE()),
                    (11, 'Kazanım Eşleştirme', 'Excel tablosu hazırlığı', 4, @Pid, 2, 0, '2025-12-12', NULL, 1, GETDATE()),
                    (12, 'Haftalık Plan', '2. Dönem planlaması', 4, @Pid, 1, 2, '2025-11-28', '2025-11-30', 1, GETDATE());
                    SET IDENTITY_INSERT Gorevler OFF;
                END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 12);

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 27, 13, 35, 37, 879, DateTimeKind.Local).AddTicks(1145));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 27, 13, 35, 37, 879, DateTimeKind.Local).AddTicks(1157));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 27, 13, 35, 37, 879, DateTimeKind.Local).AddTicks(1158));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 27, 13, 35, 37, 879, DateTimeKind.Local).AddTicks(1159));
        }
    }
}
