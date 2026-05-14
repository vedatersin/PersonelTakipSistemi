using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonelTakipSistemi.Migrations
{
    /// <inheritdoc />
    public partial class AddYazilimEnvanterModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "YazilimKayitlari",
                columns: table => new
                {
                    YazilimKaydiId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YazilimId = table.Column<int>(type: "int", nullable: false),
                    DigerYazilimAd = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Surum = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Ozellikler = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    LisansAnahtari = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    LisansSuresiTuru = table.Column<int>(type: "int", nullable: false),
                    BaslangicTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BitisTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    KullaniciAdi = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Eposta = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    SahipPersonelId = table.Column<int>(type: "int", nullable: true),
                    KoordinatorlukId = table.Column<int>(type: "int", nullable: false),
                    IlkKayitTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AktifSahiplikBaslangicTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SonDevirTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OnayDurumu = table.Column<int>(type: "int", nullable: false),
                    OnaylayanPersonelId = table.Column<int>(type: "int", nullable: true),
                    OnayTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OlusturanPersonelId = table.Column<int>(type: "int", nullable: true),
                    KoordinatorTarafindanEklendi = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YazilimKayitlari", x => x.YazilimKaydiId);
                    table.ForeignKey(
                        name: "FK_YazilimKayitlari_Koordinatorlukler_KoordinatorlukId",
                        column: x => x.KoordinatorlukId,
                        principalTable: "Koordinatorlukler",
                        principalColumn: "KoordinatorlukId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_YazilimKayitlari_Personeller_OlusturanPersonelId",
                        column: x => x.OlusturanPersonelId,
                        principalTable: "Personeller",
                        principalColumn: "PersonelId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_YazilimKayitlari_Personeller_OnaylayanPersonelId",
                        column: x => x.OnaylayanPersonelId,
                        principalTable: "Personeller",
                        principalColumn: "PersonelId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_YazilimKayitlari_Personeller_SahipPersonelId",
                        column: x => x.SahipPersonelId,
                        principalTable: "Personeller",
                        principalColumn: "PersonelId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_YazilimKayitlari_Yazilimlar_YazilimId",
                        column: x => x.YazilimId,
                        principalTable: "Yazilimlar",
                        principalColumn: "YazilimId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "YazilimHareketleri",
                columns: table => new
                {
                    YazilimHareketiId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YazilimKaydiId = table.Column<int>(type: "int", nullable: false),
                    HareketTuru = table.Column<int>(type: "int", nullable: false),
                    OncekiSahipPersonelId = table.Column<int>(type: "int", nullable: true),
                    YeniSahipPersonelId = table.Column<int>(type: "int", nullable: true),
                    IslemYapanPersonelId = table.Column<int>(type: "int", nullable: true),
                    IslemYapanAdSoyad = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    OncekiSahipAdSoyad = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    YeniSahipAdSoyad = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Aciklama = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DurumNotu = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Tarih = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YazilimHareketleri", x => x.YazilimHareketiId);
                    table.ForeignKey(
                        name: "FK_YazilimHareketleri_Personeller_IslemYapanPersonelId",
                        column: x => x.IslemYapanPersonelId,
                        principalTable: "Personeller",
                        principalColumn: "PersonelId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_YazilimHareketleri_Personeller_OncekiSahipPersonelId",
                        column: x => x.OncekiSahipPersonelId,
                        principalTable: "Personeller",
                        principalColumn: "PersonelId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_YazilimHareketleri_Personeller_YeniSahipPersonelId",
                        column: x => x.YeniSahipPersonelId,
                        principalTable: "Personeller",
                        principalColumn: "PersonelId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_YazilimHareketleri_YazilimKayitlari_YazilimKaydiId",
                        column: x => x.YazilimKaydiId,
                        principalTable: "YazilimKayitlari",
                        principalColumn: "YazilimKaydiId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_YazilimHareketleri_IslemYapanPersonelId",
                table: "YazilimHareketleri",
                column: "IslemYapanPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_YazilimHareketleri_OncekiSahipPersonelId",
                table: "YazilimHareketleri",
                column: "OncekiSahipPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_YazilimHareketleri_YazilimKaydiId_Tarih",
                table: "YazilimHareketleri",
                columns: new[] { "YazilimKaydiId", "Tarih" });

            migrationBuilder.CreateIndex(
                name: "IX_YazilimHareketleri_YeniSahipPersonelId",
                table: "YazilimHareketleri",
                column: "YeniSahipPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_YazilimKayitlari_KoordinatorlukId_OnayDurumu",
                table: "YazilimKayitlari",
                columns: new[] { "KoordinatorlukId", "OnayDurumu" });

            migrationBuilder.CreateIndex(
                name: "IX_YazilimKayitlari_LisansAnahtari",
                table: "YazilimKayitlari",
                column: "LisansAnahtari");

            migrationBuilder.CreateIndex(
                name: "IX_YazilimKayitlari_OlusturanPersonelId",
                table: "YazilimKayitlari",
                column: "OlusturanPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_YazilimKayitlari_OnaylayanPersonelId",
                table: "YazilimKayitlari",
                column: "OnaylayanPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_YazilimKayitlari_SahipPersonelId",
                table: "YazilimKayitlari",
                column: "SahipPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_YazilimKayitlari_YazilimId",
                table: "YazilimKayitlari",
                column: "YazilimId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "YazilimHareketleri");

            migrationBuilder.DropTable(
                name: "YazilimKayitlari");
        }
    }
}
