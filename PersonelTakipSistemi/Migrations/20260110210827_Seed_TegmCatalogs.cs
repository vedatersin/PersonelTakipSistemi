using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PersonelTakipSistemi.Migrations
{
    /// <inheritdoc />
    public partial class Seed_TegmCatalogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Cleanup existing data to ensure clean seed
            migrationBuilder.Sql("DELETE FROM PersonelYazilimlar");
            migrationBuilder.Sql("DELETE FROM PersonelUzmanliklar");
            migrationBuilder.Sql("DELETE FROM PersonelGorevTurleri");
            migrationBuilder.Sql("DELETE FROM PersonelIsNitelikleri");
            
            migrationBuilder.Sql("DELETE FROM Yazilimlar");
            migrationBuilder.Sql("DBCC CHECKIDENT ('Yazilimlar', RESEED, 0)");
            
            migrationBuilder.Sql("DELETE FROM Uzmanliklar");
            migrationBuilder.Sql("DBCC CHECKIDENT ('Uzmanliklar', RESEED, 0)");
            
            migrationBuilder.Sql("DELETE FROM GorevTurleri");
            migrationBuilder.Sql("DBCC CHECKIDENT ('GorevTurleri', RESEED, 0)");
            
            migrationBuilder.Sql("DELETE FROM IsNitelikleri");
            migrationBuilder.Sql("DBCC CHECKIDENT ('IsNitelikleri', RESEED, 0)");

            migrationBuilder.AlterColumn<string>(
                name: "Ad",
                table: "Yazilimlar",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Ad",
                table: "Uzmanliklar",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Ad",
                table: "IsNitelikleri",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Ad",
                table: "GorevTurleri",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "GorevTurleri",
                columns: new[] { "GorevTuruId", "Ad" },
                values: new object[,]
                {
                    { 1, "Dizgi" },
                    { 2, "Görsel Üretim" },
                    { 3, "Çizer" },
                    { 4, "Çizgi Roman Renklendirme" },
                    { 5, "Görsel Kontrolü" },
                    { 6, "Çoklu Ortam Tasarımı" },
                    { 7, "Video/Kurgu" },
                    { 8, "Fotoğraf Çekimi" },
                    { 9, "Açıklama" }
                });

            migrationBuilder.InsertData(
                table: "IsNitelikleri",
                columns: new[] { "IsNiteligiId", "Ad" },
                values: new object[,]
                {
                    { 1, "Ders Kitabı" },
                    { 2, "Çalışma Kitabı" },
                    { 3, "Hikâye Kitabı" },
                    { 4, "Çizgi Roman" },
                    { 5, "Çoklu Ortam" },
                    { 6, "MEBİ Mümtaz Şahsiyetler" },
                    { 7, "Dergi" },
                    { 8, "E Bülten" },
                    { 9, "E Dergi" },
                    { 10, "Öğretim Programı" },
                    { 11, "Açıklama" }
                });

            migrationBuilder.InsertData(
                table: "Uzmanliklar",
                columns: new[] { "UzmanlikId", "Ad" },
                values: new object[,]
                {
                    { 1, "Bilişim Uzmanı" },
                    { 2, "Görsel Tasarım Uzmanı" },
                    { 3, "Yazar" },
                    { 4, "Dil Uzmanı" },
                    { 5, "Rehberlik" }
                });

            migrationBuilder.InsertData(
                table: "Yazilimlar",
                columns: new[] { "YazilimId", "Ad" },
                values: new object[,]
                {
                    { 1, "Photoshop" },
                    { 2, "İllüstrator" },
                    { 3, "InDesign" },
                    { 4, "Camtasia" },
                    { 5, "Premiere" },
                    { 6, "After Effects" },
                    { 7, "Cinema 4D" },
                    { 8, "Blender" },
                    { 9, "Maya" },
                    { 10, "Procreate" },
                    { 11, "Construct" },
                    { 12, "Articulate" },
                    { 13, "Unity" },
                    { 14, "Unreal Engine" },
                    { 15, "PHP" },
                    { 16, "Java" },
                    { 17, ".NET" },
                    { 18, "Diğer" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Yazilimlar_Ad",
                table: "Yazilimlar",
                column: "Ad",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Uzmanliklar_Ad",
                table: "Uzmanliklar",
                column: "Ad",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IsNitelikleri_Ad",
                table: "IsNitelikleri",
                column: "Ad",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GorevTurleri_Ad",
                table: "GorevTurleri",
                column: "Ad",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Yazilimlar_Ad",
                table: "Yazilimlar");

            migrationBuilder.DropIndex(
                name: "IX_Uzmanliklar_Ad",
                table: "Uzmanliklar");

            migrationBuilder.DropIndex(
                name: "IX_IsNitelikleri_Ad",
                table: "IsNitelikleri");

            migrationBuilder.DropIndex(
                name: "IX_GorevTurleri_Ad",
                table: "GorevTurleri");

            migrationBuilder.DeleteData(
                table: "GorevTurleri",
                keyColumn: "GorevTuruId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "GorevTurleri",
                keyColumn: "GorevTuruId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "GorevTurleri",
                keyColumn: "GorevTuruId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "GorevTurleri",
                keyColumn: "GorevTuruId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "GorevTurleri",
                keyColumn: "GorevTuruId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "GorevTurleri",
                keyColumn: "GorevTuruId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "GorevTurleri",
                keyColumn: "GorevTuruId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "GorevTurleri",
                keyColumn: "GorevTuruId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "GorevTurleri",
                keyColumn: "GorevTuruId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "IsNitelikleri",
                keyColumn: "IsNiteligiId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "IsNitelikleri",
                keyColumn: "IsNiteligiId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "IsNitelikleri",
                keyColumn: "IsNiteligiId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "IsNitelikleri",
                keyColumn: "IsNiteligiId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "IsNitelikleri",
                keyColumn: "IsNiteligiId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "IsNitelikleri",
                keyColumn: "IsNiteligiId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "IsNitelikleri",
                keyColumn: "IsNiteligiId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "IsNitelikleri",
                keyColumn: "IsNiteligiId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "IsNitelikleri",
                keyColumn: "IsNiteligiId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "IsNitelikleri",
                keyColumn: "IsNiteligiId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "IsNitelikleri",
                keyColumn: "IsNiteligiId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Uzmanliklar",
                keyColumn: "UzmanlikId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Uzmanliklar",
                keyColumn: "UzmanlikId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Uzmanliklar",
                keyColumn: "UzmanlikId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Uzmanliklar",
                keyColumn: "UzmanlikId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Uzmanliklar",
                keyColumn: "UzmanlikId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Yazilimlar",
                keyColumn: "YazilimId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Yazilimlar",
                keyColumn: "YazilimId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Yazilimlar",
                keyColumn: "YazilimId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Yazilimlar",
                keyColumn: "YazilimId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Yazilimlar",
                keyColumn: "YazilimId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Yazilimlar",
                keyColumn: "YazilimId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Yazilimlar",
                keyColumn: "YazilimId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Yazilimlar",
                keyColumn: "YazilimId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Yazilimlar",
                keyColumn: "YazilimId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Yazilimlar",
                keyColumn: "YazilimId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Yazilimlar",
                keyColumn: "YazilimId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Yazilimlar",
                keyColumn: "YazilimId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Yazilimlar",
                keyColumn: "YazilimId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Yazilimlar",
                keyColumn: "YazilimId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Yazilimlar",
                keyColumn: "YazilimId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Yazilimlar",
                keyColumn: "YazilimId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Yazilimlar",
                keyColumn: "YazilimId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Yazilimlar",
                keyColumn: "YazilimId",
                keyValue: 18);

            migrationBuilder.AlterColumn<string>(
                name: "Ad",
                table: "Yazilimlar",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Ad",
                table: "Uzmanliklar",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Ad",
                table: "IsNitelikleri",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Ad",
                table: "GorevTurleri",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
