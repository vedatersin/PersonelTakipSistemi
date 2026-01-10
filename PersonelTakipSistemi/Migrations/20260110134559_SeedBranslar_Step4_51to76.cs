using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PersonelTakipSistemi.Migrations
{
    /// <inheritdoc />
    public partial class SeedBranslar_Step4_51to76 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Branslar",
                columns: new[] { "BransId", "Ad" },
                values: new object[,]
                {
                    { 51, "Muhasebe ve Finansman" },
                    { 52, "Müzik" },
                    { 53, "Okul Öncesi" },
                    { 54, "Özel Eğitim" },
                    { 55, "Pazarlama ve Perakende" },
                    { 56, "Plastik Teknolojisi" },
                    { 57, "Raylı Sistemler Teknolojisi / Raylı Sistemler Elektrik-Elektronik" },
                    { 58, "Rehberlik" },
                    { 59, "Rusça" },
                    { 60, "Sağlık / Sağlık Hizmetleri" },
                    { 61, "Sağlık Bilgisi" },
                    { 62, "Seramik ve Cam Teknolojisi" },
                    { 63, "Sınıf Öğretmenliği" },
                    { 64, "Sosyal Bilgiler" },
                    { 65, "Tarım Teknolojileri/Tarım" },
                    { 66, "Tarih" },
                    { 67, "Teknoloji ve Tasarım" },
                    { 68, "Tesisat Teknolojisi ve İklimlendirme" },
                    { 69, "Tiyatro" },
                    { 70, "Türk Dili ve Edebiyatı" },
                    { 71, "Türkçe" },
                    { 72, "Ulaştırma Hizmetleri / Lojistik" },
                    { 73, "Yaşayan Diller ve Lehçeler (Kürtçe / Kurmançi)" },
                    { 74, "Yaşayan Diller ve Lehçeler (Kürtçe / Zazaki)" },
                    { 75, "Yenilenebilir Enerji Teknolojileri" },
                    { 76, "Yiyecek İçecek Hizmetleri" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 76);
        }
    }
}
