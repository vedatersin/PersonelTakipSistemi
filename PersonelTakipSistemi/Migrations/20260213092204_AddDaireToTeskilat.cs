using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonelTakipSistemi.Migrations
{
    /// <inheritdoc />
    public partial class AddDaireToTeskilat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DaireBaskanligiId",
                table: "Teskilatlar",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 12, 22, 3, 712, DateTimeKind.Local).AddTicks(6457));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 12, 22, 3, 712, DateTimeKind.Local).AddTicks(6463));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 12, 22, 3, 712, DateTimeKind.Local).AddTicks(6464));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 12, 22, 3, 712, DateTimeKind.Local).AddTicks(6465));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 12, 22, 3, 712, DateTimeKind.Local).AddTicks(9189));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 12, 22, 3, 712, DateTimeKind.Local).AddTicks(9201));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 12, 22, 3, 712, DateTimeKind.Local).AddTicks(9204));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 12, 22, 3, 712, DateTimeKind.Local).AddTicks(9206));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 12, 22, 3, 712, DateTimeKind.Local).AddTicks(9208));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 12, 22, 3, 712, DateTimeKind.Local).AddTicks(9210));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 12, 22, 3, 712, DateTimeKind.Local).AddTicks(9214));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 12, 22, 3, 712, DateTimeKind.Local).AddTicks(9215));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 12, 22, 3, 712, DateTimeKind.Local).AddTicks(9217));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 12, 22, 3, 712, DateTimeKind.Local).AddTicks(9220));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 12, 22, 3, 712, DateTimeKind.Local).AddTicks(9222));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 12, 22, 3, 712, DateTimeKind.Local).AddTicks(9223));

            migrationBuilder.UpdateData(
                table: "Komisyonlar",
                keyColumn: "KomisyonId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 12, 22, 3, 709, DateTimeKind.Local).AddTicks(7441));

            migrationBuilder.UpdateData(
                table: "Komisyonlar",
                keyColumn: "KomisyonId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 12, 22, 3, 709, DateTimeKind.Local).AddTicks(7445));

            migrationBuilder.UpdateData(
                table: "Komisyonlar",
                keyColumn: "KomisyonId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 12, 22, 3, 709, DateTimeKind.Local).AddTicks(7446));

            migrationBuilder.UpdateData(
                table: "Komisyonlar",
                keyColumn: "KomisyonId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 12, 22, 3, 709, DateTimeKind.Local).AddTicks(7447));

            migrationBuilder.UpdateData(
                table: "Komisyonlar",
                keyColumn: "KomisyonId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 12, 22, 3, 709, DateTimeKind.Local).AddTicks(7448));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 12, 22, 3, 709, DateTimeKind.Local).AddTicks(6442));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 12, 22, 3, 709, DateTimeKind.Local).AddTicks(6499));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 13, 12, 22, 3, 709, DateTimeKind.Local).AddTicks(6500));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 1,
                columns: new[] { "CreatedAt", "DaireBaskanligiId" },
                values: new object[] { new DateTime(2026, 2, 13, 12, 22, 3, 709, DateTimeKind.Local).AddTicks(5314), null });

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 2,
                columns: new[] { "CreatedAt", "DaireBaskanligiId" },
                values: new object[] { new DateTime(2026, 2, 13, 12, 22, 3, 709, DateTimeKind.Local).AddTicks(5327), null });

            migrationBuilder.CreateIndex(
                name: "IX_Teskilatlar_DaireBaskanligiId",
                table: "Teskilatlar",
                column: "DaireBaskanligiId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teskilatlar_DaireBaskanliklari_DaireBaskanligiId",
                table: "Teskilatlar",
                column: "DaireBaskanligiId",
                principalTable: "DaireBaskanliklari",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teskilatlar_DaireBaskanliklari_DaireBaskanligiId",
                table: "Teskilatlar");

            migrationBuilder.DropIndex(
                name: "IX_Teskilatlar_DaireBaskanligiId",
                table: "Teskilatlar");

            migrationBuilder.DropColumn(
                name: "DaireBaskanligiId",
                table: "Teskilatlar");

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 12, 13, 59, 3, 980, DateTimeKind.Local).AddTicks(8011));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 12, 13, 59, 3, 980, DateTimeKind.Local).AddTicks(8019));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 12, 13, 59, 3, 980, DateTimeKind.Local).AddTicks(8020));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 12, 13, 59, 3, 980, DateTimeKind.Local).AddTicks(8021));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 12, 13, 59, 3, 981, DateTimeKind.Local).AddTicks(935));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 12, 13, 59, 3, 981, DateTimeKind.Local).AddTicks(949));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 12, 13, 59, 3, 981, DateTimeKind.Local).AddTicks(952));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 12, 13, 59, 3, 981, DateTimeKind.Local).AddTicks(955));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 12, 13, 59, 3, 981, DateTimeKind.Local).AddTicks(957));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 12, 13, 59, 3, 981, DateTimeKind.Local).AddTicks(960));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 12, 13, 59, 3, 981, DateTimeKind.Local).AddTicks(962));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 12, 13, 59, 3, 981, DateTimeKind.Local).AddTicks(964));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 12, 13, 59, 3, 981, DateTimeKind.Local).AddTicks(966));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 12, 13, 59, 3, 981, DateTimeKind.Local).AddTicks(969));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 12, 13, 59, 3, 981, DateTimeKind.Local).AddTicks(970));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 12, 13, 59, 3, 981, DateTimeKind.Local).AddTicks(972));

            migrationBuilder.UpdateData(
                table: "Komisyonlar",
                keyColumn: "KomisyonId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 12, 13, 59, 3, 977, DateTimeKind.Local).AddTicks(9301));

            migrationBuilder.UpdateData(
                table: "Komisyonlar",
                keyColumn: "KomisyonId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 12, 13, 59, 3, 977, DateTimeKind.Local).AddTicks(9305));

            migrationBuilder.UpdateData(
                table: "Komisyonlar",
                keyColumn: "KomisyonId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 12, 13, 59, 3, 977, DateTimeKind.Local).AddTicks(9306));

            migrationBuilder.UpdateData(
                table: "Komisyonlar",
                keyColumn: "KomisyonId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 12, 13, 59, 3, 977, DateTimeKind.Local).AddTicks(9307));

            migrationBuilder.UpdateData(
                table: "Komisyonlar",
                keyColumn: "KomisyonId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 12, 13, 59, 3, 977, DateTimeKind.Local).AddTicks(9308));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 12, 13, 59, 3, 977, DateTimeKind.Local).AddTicks(8426));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 12, 13, 59, 3, 977, DateTimeKind.Local).AddTicks(8432));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 12, 13, 59, 3, 977, DateTimeKind.Local).AddTicks(8433));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 12, 13, 59, 3, 977, DateTimeKind.Local).AddTicks(7297));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 12, 13, 59, 3, 977, DateTimeKind.Local).AddTicks(7309));
        }
    }
}
