using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonelTakipSistemi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GorevTurleri",
                columns: table => new
                {
                    GorevTuruId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GorevTurleri", x => x.GorevTuruId);
                });

            migrationBuilder.CreateTable(
                name: "IsNitelikleri",
                columns: table => new
                {
                    IsNiteligiId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IsNitelikleri", x => x.IsNiteligiId);
                });

            migrationBuilder.CreateTable(
                name: "Personeller",
                columns: table => new
                {
                    PersonelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Soyad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TcKimlikNo = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Eposta = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PersonelCinsiyet = table.Column<int>(type: "int", nullable: false),
                    GorevliIl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Brans = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KadroKurum = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AktifMi = table.Column<bool>(type: "bit", nullable: false),
                    FotografYolu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SifreHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    SifreSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personeller", x => x.PersonelId);
                });

            migrationBuilder.CreateTable(
                name: "Uzmanliklar",
                columns: table => new
                {
                    UzmanlikId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uzmanliklar", x => x.UzmanlikId);
                });

            migrationBuilder.CreateTable(
                name: "Yazilimlar",
                columns: table => new
                {
                    YazilimId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Yazilimlar", x => x.YazilimId);
                });

            migrationBuilder.CreateTable(
                name: "PersonelGorevTurleri",
                columns: table => new
                {
                    PersonelId = table.Column<int>(type: "int", nullable: false),
                    GorevTuruId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonelGorevTurleri", x => new { x.PersonelId, x.GorevTuruId });
                    table.ForeignKey(
                        name: "FK_PersonelGorevTurleri_GorevTurleri_GorevTuruId",
                        column: x => x.GorevTuruId,
                        principalTable: "GorevTurleri",
                        principalColumn: "GorevTuruId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonelGorevTurleri_Personeller_PersonelId",
                        column: x => x.PersonelId,
                        principalTable: "Personeller",
                        principalColumn: "PersonelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonelIsNitelikleri",
                columns: table => new
                {
                    PersonelId = table.Column<int>(type: "int", nullable: false),
                    IsNiteligiId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonelIsNitelikleri", x => new { x.PersonelId, x.IsNiteligiId });
                    table.ForeignKey(
                        name: "FK_PersonelIsNitelikleri_IsNitelikleri_IsNiteligiId",
                        column: x => x.IsNiteligiId,
                        principalTable: "IsNitelikleri",
                        principalColumn: "IsNiteligiId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonelIsNitelikleri_Personeller_PersonelId",
                        column: x => x.PersonelId,
                        principalTable: "Personeller",
                        principalColumn: "PersonelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonelUzmanliklar",
                columns: table => new
                {
                    PersonelId = table.Column<int>(type: "int", nullable: false),
                    UzmanlikId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonelUzmanliklar", x => new { x.PersonelId, x.UzmanlikId });
                    table.ForeignKey(
                        name: "FK_PersonelUzmanliklar_Personeller_PersonelId",
                        column: x => x.PersonelId,
                        principalTable: "Personeller",
                        principalColumn: "PersonelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonelUzmanliklar_Uzmanliklar_UzmanlikId",
                        column: x => x.UzmanlikId,
                        principalTable: "Uzmanliklar",
                        principalColumn: "UzmanlikId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonelYazilimlar",
                columns: table => new
                {
                    PersonelId = table.Column<int>(type: "int", nullable: false),
                    YazilimId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonelYazilimlar", x => new { x.PersonelId, x.YazilimId });
                    table.ForeignKey(
                        name: "FK_PersonelYazilimlar_Personeller_PersonelId",
                        column: x => x.PersonelId,
                        principalTable: "Personeller",
                        principalColumn: "PersonelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonelYazilimlar_Yazilimlar_YazilimId",
                        column: x => x.YazilimId,
                        principalTable: "Yazilimlar",
                        principalColumn: "YazilimId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonelGorevTurleri_GorevTuruId",
                table: "PersonelGorevTurleri",
                column: "GorevTuruId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonelIsNitelikleri_IsNiteligiId",
                table: "PersonelIsNitelikleri",
                column: "IsNiteligiId");

            migrationBuilder.CreateIndex(
                name: "IX_Personeller_Eposta",
                table: "Personeller",
                column: "Eposta",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Personeller_TcKimlikNo",
                table: "Personeller",
                column: "TcKimlikNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonelUzmanliklar_UzmanlikId",
                table: "PersonelUzmanliklar",
                column: "UzmanlikId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonelYazilimlar_YazilimId",
                table: "PersonelYazilimlar",
                column: "YazilimId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonelGorevTurleri");

            migrationBuilder.DropTable(
                name: "PersonelIsNitelikleri");

            migrationBuilder.DropTable(
                name: "PersonelUzmanliklar");

            migrationBuilder.DropTable(
                name: "PersonelYazilimlar");

            migrationBuilder.DropTable(
                name: "GorevTurleri");

            migrationBuilder.DropTable(
                name: "IsNitelikleri");

            migrationBuilder.DropTable(
                name: "Uzmanliklar");

            migrationBuilder.DropTable(
                name: "Personeller");

            migrationBuilder.DropTable(
                name: "Yazilimlar");
        }
    }
}
