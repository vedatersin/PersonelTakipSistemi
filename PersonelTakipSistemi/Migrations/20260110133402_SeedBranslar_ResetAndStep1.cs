using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PersonelTakipSistemi.Migrations
{
    /// <inheritdoc />
    public partial class SeedBranslar_ResetAndStep1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.UpdateData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 1,
                column: "Ad",
                value: "Adalet");

            migrationBuilder.UpdateData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 2,
                column: "Ad",
                value: "Aile ve Tüketici Hizmetleri");

            migrationBuilder.UpdateData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 3,
                column: "Ad",
                value: "Almanca");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 1,
                column: "Ad",
                value: "Bilişim Teknolojileri");

            migrationBuilder.UpdateData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 2,
                column: "Ad",
                value: "Matematik");

            migrationBuilder.UpdateData(
                table: "Branslar",
                keyColumn: "BransId",
                keyValue: 3,
                column: "Ad",
                value: "Fen Bilimleri");

            migrationBuilder.InsertData(
                table: "Branslar",
                columns: new[] { "BransId", "Ad" },
                values: new object[,]
                {
                    { 4, "Türkçe" },
                    { 5, "İngilizce" },
                    { 6, "Sosyal Bilgiler" },
                    { 7, "Din Kültürü ve Ahlak Bilgisi" },
                    { 8, "Görsel Sanatlar" },
                    { 9, "Müzik" },
                    { 10, "Beden Eğitimi" },
                    { 11, "Okul Öncesi" },
                    { 12, "Rehberlik" },
                    { 13, "Sınıf Öğretmenliği" },
                    { 14, "Diğer" }
                });
        }
    }
}
