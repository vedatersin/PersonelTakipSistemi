using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PersonelTakipSistemi.Migrations
{
    /// <inheritdoc />
    public partial class SeedBranslar_Step2_1to25 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Branslar",
                columns: new[] { "BransId", "Ad" },
                values: new object[,]
                {
                    { 4, "Arapça" },
                    { 5, "Ayakkabı ve Saraciye Teknolojisi" },
                    { 6, "Beden Eğitimi" },
                    { 7, "Bilgisayar ve Öğretim Teknolojileri" },
                    { 8, "Bilişim Teknolojileri" },
                    { 9, "Biyoloji" },
                    { 10, "Büro Yönetimi / Büro Yönetimi ve Yönetici Asistanlığı" },
                    { 11, "Coğrafya" },
                    { 12, "Çocuk Gelişimi ve Eğitimi" },
                    { 13, "Denizcilik / Gemi Makineleri" },
                    { 14, "Denizcilik / Gemi Yönetimi" },
                    { 15, "Din Kültürü ve Ahlâk Bilgisi" },
                    { 16, "El Sanatları Teknolojisi / El Sanatları" },
                    { 17, "El Sanatları Teknolojisi / Nakış" },
                    { 18, "Elektrik-Elektronik Teknolojisi / Elektrik" },
                    { 19, "Elektrik-Elektronik Teknolojisi / Elektronik" },
                    { 20, "Endüstriyel Otomasyon Teknolojileri" },
                    { 21, "Farsça" },
                    { 22, "Felsefe" },
                    { 23, "Fen Bilimleri" },
                    { 24, "Fizik" },
                    { 25, "Gemi Yapımı / Yat İnşa" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 25);
        }
    }
}
