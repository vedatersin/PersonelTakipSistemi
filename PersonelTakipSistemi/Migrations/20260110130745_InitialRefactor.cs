using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PersonelTakipSistemi.Migrations
{
    /// <inheritdoc />
    public partial class InitialRefactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Brans",
                table: "Personeller");

            migrationBuilder.DropColumn(
                name: "GorevliIl",
                table: "Personeller");

            migrationBuilder.AlterColumn<string>(
                name: "Telefon",
                table: "Personeller",
                type: "nchar(10)",
                fixedLength: true,
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(11)",
                oldMaxLength: 11);

            migrationBuilder.AlterColumn<bool>(
                name: "PersonelCinsiyet",
                table: "Personeller",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "AktifMi",
                table: "Personeller",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<int>(
                name: "BransId",
                table: "Personeller",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DogumTarihi",
                table: "Personeller",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "GorevliIlId",
                table: "Personeller",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Branslar",
                columns: table => new
                {
                    BransId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branslar", x => x.BransId);
                });

            migrationBuilder.CreateTable(
                name: "Iller",
                columns: table => new
                {
                    IlId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Iller", x => x.IlId);
                });

            migrationBuilder.InsertData(
                table: "Branslar",
                columns: new[] { "BransId", "Ad" },
                values: new object[,]
                {
                    { 1, "Bilişim Teknolojileri" },
                    { 2, "Matematik" },
                    { 3, "Fen Bilimleri" },
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

            migrationBuilder.InsertData(
                table: "Iller",
                columns: new[] { "IlId", "Ad" },
                values: new object[,]
                {
                    { 1, "Adana" },
                    { 2, "Adıyaman" },
                    { 3, "Afyonkarahisar" },
                    { 4, "Ağrı" },
                    { 5, "Amasya" },
                    { 6, "Ankara" },
                    { 7, "Antalya" },
                    { 8, "Artvin" },
                    { 9, "Aydın" },
                    { 10, "Balıkesir" },
                    { 11, "Bilecik" },
                    { 12, "Bingöl" },
                    { 13, "Bitlis" },
                    { 14, "Bolu" },
                    { 15, "Burdur" },
                    { 16, "Bursa" },
                    { 17, "Çanakkale" },
                    { 18, "Çankırı" },
                    { 19, "Çorum" },
                    { 20, "Denizli" },
                    { 21, "Diyarbakır" },
                    { 22, "Edirne" },
                    { 23, "Elazığ" },
                    { 24, "Erzincan" },
                    { 25, "Erzurum" },
                    { 26, "Eskişehir" },
                    { 27, "Gaziantep" },
                    { 28, "Giresun" },
                    { 29, "Gümüşhane" },
                    { 30, "Hakkari" },
                    { 31, "Hatay" },
                    { 32, "Isparta" },
                    { 33, "Mersin" },
                    { 34, "İstanbul" },
                    { 35, "İzmir" },
                    { 36, "Kars" },
                    { 37, "Kastamonu" },
                    { 38, "Kayseri" },
                    { 39, "Kırklareli" },
                    { 40, "Kırşehir" },
                    { 41, "Kocaeli" },
                    { 42, "Konya" },
                    { 43, "Kütahya" },
                    { 44, "Malatya" },
                    { 45, "Manisa" },
                    { 46, "Kahramanmaraş" },
                    { 47, "Mardin" },
                    { 48, "Muğla" },
                    { 49, "Muş" },
                    { 50, "Nevşehir" },
                    { 51, "Niğde" },
                    { 52, "Ordu" },
                    { 53, "Rize" },
                    { 54, "Sakarya" },
                    { 55, "Samsun" },
                    { 56, "Siirt" },
                    { 57, "Sinop" },
                    { 58, "Sivas" },
                    { 59, "Tekirdağ" },
                    { 60, "Tokat" },
                    { 61, "Trabzon" },
                    { 62, "Tunceli" },
                    { 63, "Şanlıurfa" },
                    { 64, "Uşak" },
                    { 65, "Van" },
                    { 66, "Yozgat" },
                    { 67, "Zonguldak" },
                    { 68, "Aksaray" },
                    { 69, "Bayburt" },
                    { 70, "Karaman" },
                    { 71, "Kırıkkale" },
                    { 72, "Batman" },
                    { 73, "Şırnak" },
                    { 74, "Bartın" },
                    { 75, "Ardahan" },
                    { 76, "Iğdır" },
                    { 77, "Yalova" },
                    { 78, "Karabük" },
                    { 79, "Kilis" },
                    { 80, "Osmaniye" },
                    { 81, "Düzce" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Personeller_BransId",
                table: "Personeller",
                column: "BransId");

            migrationBuilder.CreateIndex(
                name: "IX_Personeller_GorevliIlId",
                table: "Personeller",
                column: "GorevliIlId");

            migrationBuilder.CreateIndex(
                name: "IX_Branslar_Ad",
                table: "Branslar",
                column: "Ad",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Iller_Ad",
                table: "Iller",
                column: "Ad",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Personeller_Branslar_BransId",
                table: "Personeller",
                column: "BransId",
                principalTable: "Branslar",
                principalColumn: "BransId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Personeller_Iller_GorevliIlId",
                table: "Personeller",
                column: "GorevliIlId",
                principalTable: "Iller",
                principalColumn: "IlId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Personeller_Branslar_BransId",
                table: "Personeller");

            migrationBuilder.DropForeignKey(
                name: "FK_Personeller_Iller_GorevliIlId",
                table: "Personeller");

            migrationBuilder.DropTable(
                name: "Branslar");

            migrationBuilder.DropTable(
                name: "Iller");

            migrationBuilder.DropIndex(
                name: "IX_Personeller_BransId",
                table: "Personeller");

            migrationBuilder.DropIndex(
                name: "IX_Personeller_GorevliIlId",
                table: "Personeller");

            migrationBuilder.DropColumn(
                name: "BransId",
                table: "Personeller");

            migrationBuilder.DropColumn(
                name: "DogumTarihi",
                table: "Personeller");

            migrationBuilder.DropColumn(
                name: "GorevliIlId",
                table: "Personeller");

            migrationBuilder.AlterColumn<string>(
                name: "Telefon",
                table: "Personeller",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nchar(10)",
                oldFixedLength: true,
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<int>(
                name: "PersonelCinsiyet",
                table: "Personeller",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "AktifMi",
                table: "Personeller",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.AddColumn<string>(
                name: "Brans",
                table: "Personeller",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GorevliIl",
                table: "Personeller",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
