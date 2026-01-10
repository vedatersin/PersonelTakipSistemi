using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PersonelTakipSistemi.Migrations
{
    /// <inheritdoc />
    public partial class SeedBranslar_Step3_26to50 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Branslar",
                columns: new[] { "BransId", "Ad" },
                values: new object[,]
                {
                    { 26, "Gıda Teknolojisi" },
                    { 27, "Giyim Üretim Teknolojisi / Moda Tasarım Teknolojileri" },
                    { 28, "Görsel Sanatlar" },
                    { 29, "Grafik ve Fotoğraf / Grafik" },
                    { 30, "Harita-Tapu-Kadastro / Harita Kadastro" },
                    { 31, "Hasta ve Yaşlı Hizmetleri" },
                    { 32, "Hayvan Sağlığı / Hayvan Yetiştiriciliği ve Sağlığı / Hayvan Sağlığı" },
                    { 33, "Hayvan Sağlığı / Hayvan Yetiştiriciliği ve Sağlığı / Hayvan Yetiştiriciliği" },
                    { 34, "İlköğretim Matematik" },
                    { 35, "İmam-Hatip Lisesi Meslek Dersleri" },
                    { 36, "İngilizce" },
                    { 37, "İnşaat Teknolojisi / Yapı Dekorasyon" },
                    { 38, "İnşaat Teknolojisi / Yapı Tasarım" },
                    { 39, "İtfaiyecilik ve Yangın Güvenliği" },
                    { 40, "Kimya / Kimya Teknolojisi" },
                    { 41, "Konaklama ve Seyahat Hizmetleri / Konaklama ve Seyahat" },
                    { 42, "Kuyumculuk Teknolojisi" },
                    { 43, "Makine Teknolojisi / Makine ve Tasarım Teknolojisi / Makine Model" },
                    { 44, "Makine Teknolojisi / Makine ve Tasarım Teknolojisi / Makine Ressamlığı" },
                    { 45, "Makine Teknolojisi / Makine ve Tasarım Teknolojisi / Makine ve Kalıp" },
                    { 46, "Matbaa / Matbaa Teknolojisi" },
                    { 47, "Matematik" },
                    { 48, "Metal Teknolojisi" },
                    { 49, "Mobilya ve İç Mekan Tasarımı" },
                    { 50, "Motorlu Araçlar Teknolojisi" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 50);
        }
    }
}
