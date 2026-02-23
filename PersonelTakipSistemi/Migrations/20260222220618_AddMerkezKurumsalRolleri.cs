using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PersonelTakipSistemi.Migrations
{
    /// <inheritdoc />
    public partial class AddMerkezKurumsalRolleri : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "SadeceMerkezMi",
                table: "KurumsalRoller",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 1, 6, 17, 397, DateTimeKind.Local).AddTicks(7640));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 1, 6, 17, 397, DateTimeKind.Local).AddTicks(7653));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 1, 6, 17, 397, DateTimeKind.Local).AddTicks(7654));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 1, 6, 17, 397, DateTimeKind.Local).AddTicks(7655));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 1, 6, 17, 398, DateTimeKind.Local).AddTicks(549));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 1, 6, 17, 398, DateTimeKind.Local).AddTicks(579));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 1, 6, 17, 398, DateTimeKind.Local).AddTicks(583));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 1, 6, 17, 398, DateTimeKind.Local).AddTicks(585));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 1, 6, 17, 398, DateTimeKind.Local).AddTicks(587));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 1, 6, 17, 398, DateTimeKind.Local).AddTicks(591));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 1, 6, 17, 398, DateTimeKind.Local).AddTicks(594));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 1, 6, 17, 398, DateTimeKind.Local).AddTicks(596));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 1, 6, 17, 398, DateTimeKind.Local).AddTicks(637));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 1, 6, 17, 398, DateTimeKind.Local).AddTicks(640));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 1, 6, 17, 398, DateTimeKind.Local).AddTicks(641));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 1, 6, 17, 398, DateTimeKind.Local).AddTicks(643));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 1, 6, 17, 392, DateTimeKind.Local).AddTicks(3826));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 1, 6, 17, 392, DateTimeKind.Local).AddTicks(3827));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 1, 6, 17, 392, DateTimeKind.Local).AddTicks(3809));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 1, 6, 17, 392, DateTimeKind.Local).AddTicks(3813));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 1, 6, 17, 392, DateTimeKind.Local).AddTicks(3814));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 1, 6, 17, 392, DateTimeKind.Local).AddTicks(3815));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 1, 6, 17, 392, DateTimeKind.Local).AddTicks(3816));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 1, 6, 17, 392, DateTimeKind.Local).AddTicks(3817));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 1, 6, 17, 392, DateTimeKind.Local).AddTicks(3818));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 1, 6, 17, 392, DateTimeKind.Local).AddTicks(3819));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 1, 6, 17, 392, DateTimeKind.Local).AddTicks(3820));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 1, 6, 17, 392, DateTimeKind.Local).AddTicks(3821));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 1, 6, 17, 392, DateTimeKind.Local).AddTicks(3822));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 1, 6, 17, 392, DateTimeKind.Local).AddTicks(3822));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 16,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 1, 6, 17, 392, DateTimeKind.Local).AddTicks(3823));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 17,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 1, 6, 17, 392, DateTimeKind.Local).AddTicks(3824));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 18,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 1, 6, 17, 392, DateTimeKind.Local).AddTicks(3825));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 19,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 1, 6, 17, 392, DateTimeKind.Local).AddTicks(3825));

            migrationBuilder.UpdateData(
                table: "KurumsalRoller",
                keyColumn: "KurumsalRolId",
                keyValue: 1,
                column: "SadeceMerkezMi",
                value: false);

            migrationBuilder.UpdateData(
                table: "KurumsalRoller",
                keyColumn: "KurumsalRolId",
                keyValue: 2,
                column: "SadeceMerkezMi",
                value: false);

            migrationBuilder.UpdateData(
                table: "KurumsalRoller",
                keyColumn: "KurumsalRolId",
                keyValue: 3,
                column: "SadeceMerkezMi",
                value: false);

            migrationBuilder.UpdateData(
                table: "KurumsalRoller",
                keyColumn: "KurumsalRolId",
                keyValue: 4,
                column: "SadeceMerkezMi",
                value: true);

            migrationBuilder.UpdateData(
                table: "KurumsalRoller",
                keyColumn: "KurumsalRolId",
                keyValue: 5,
                column: "SadeceMerkezMi",
                value: true);

            migrationBuilder.InsertData(
                table: "KurumsalRoller",
                columns: new[] { "KurumsalRolId", "Ad", "SadeceMerkezMi" },
                values: new object[,]
                {
                    { 6, "Uzman", true },
                    { 7, "Şube Müdürü", true },
                    { 8, "Şef", true },
                    { 9, "Daire Başkanı", true },
                    { 10, "Genel Müdür", true }
                });

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 1, 6, 17, 392, DateTimeKind.Local).AddTicks(2727));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 23, 1, 6, 17, 392, DateTimeKind.Local).AddTicks(2743));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "KurumsalRoller",
                keyColumn: "KurumsalRolId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "KurumsalRoller",
                keyColumn: "KurumsalRolId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "KurumsalRoller",
                keyColumn: "KurumsalRolId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "KurumsalRoller",
                keyColumn: "KurumsalRolId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "KurumsalRoller",
                keyColumn: "KurumsalRolId",
                keyValue: 10);

            migrationBuilder.DropColumn(
                name: "SadeceMerkezMi",
                table: "KurumsalRoller");

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 22, 14, 8, 46, 145, DateTimeKind.Local).AddTicks(237));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 22, 14, 8, 46, 145, DateTimeKind.Local).AddTicks(244));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 22, 14, 8, 46, 145, DateTimeKind.Local).AddTicks(245));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 22, 14, 8, 46, 145, DateTimeKind.Local).AddTicks(246));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 22, 14, 8, 46, 145, DateTimeKind.Local).AddTicks(3072));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 22, 14, 8, 46, 145, DateTimeKind.Local).AddTicks(3084));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 22, 14, 8, 46, 145, DateTimeKind.Local).AddTicks(3087));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 22, 14, 8, 46, 145, DateTimeKind.Local).AddTicks(3089));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 22, 14, 8, 46, 145, DateTimeKind.Local).AddTicks(3091));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 22, 14, 8, 46, 145, DateTimeKind.Local).AddTicks(3094));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 22, 14, 8, 46, 145, DateTimeKind.Local).AddTicks(3096));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 22, 14, 8, 46, 145, DateTimeKind.Local).AddTicks(3098));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 22, 14, 8, 46, 145, DateTimeKind.Local).AddTicks(3100));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 22, 14, 8, 46, 145, DateTimeKind.Local).AddTicks(3102));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 22, 14, 8, 46, 145, DateTimeKind.Local).AddTicks(3104));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 22, 14, 8, 46, 145, DateTimeKind.Local).AddTicks(3106));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 22, 14, 8, 46, 140, DateTimeKind.Local).AddTicks(9904));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 22, 14, 8, 46, 140, DateTimeKind.Local).AddTicks(9905));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 22, 14, 8, 46, 140, DateTimeKind.Local).AddTicks(9889));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 22, 14, 8, 46, 140, DateTimeKind.Local).AddTicks(9893));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 22, 14, 8, 46, 140, DateTimeKind.Local).AddTicks(9894));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 22, 14, 8, 46, 140, DateTimeKind.Local).AddTicks(9895));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 22, 14, 8, 46, 140, DateTimeKind.Local).AddTicks(9896));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 22, 14, 8, 46, 140, DateTimeKind.Local).AddTicks(9896));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 22, 14, 8, 46, 140, DateTimeKind.Local).AddTicks(9897));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 22, 14, 8, 46, 140, DateTimeKind.Local).AddTicks(9898));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 22, 14, 8, 46, 140, DateTimeKind.Local).AddTicks(9899));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 22, 14, 8, 46, 140, DateTimeKind.Local).AddTicks(9900));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 22, 14, 8, 46, 140, DateTimeKind.Local).AddTicks(9900));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 22, 14, 8, 46, 140, DateTimeKind.Local).AddTicks(9901));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 16,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 22, 14, 8, 46, 140, DateTimeKind.Local).AddTicks(9902));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 17,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 22, 14, 8, 46, 140, DateTimeKind.Local).AddTicks(9902));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 18,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 22, 14, 8, 46, 140, DateTimeKind.Local).AddTicks(9903));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 19,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 22, 14, 8, 46, 140, DateTimeKind.Local).AddTicks(9904));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 22, 14, 8, 46, 140, DateTimeKind.Local).AddTicks(8910));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 22, 14, 8, 46, 140, DateTimeKind.Local).AddTicks(8925));
        }
    }
}
