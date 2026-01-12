using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PersonelTakipSistemi.Migrations
{
    /// <inheritdoc />
    public partial class AddAuthorizationModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SistemRol",
                table: "Personeller",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "KurumsalRoller",
                columns: table => new
                {
                    KurumsalRolId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KurumsalRoller", x => x.KurumsalRolId);
                });

            migrationBuilder.CreateTable(
                name: "Teskilatlar",
                columns: table => new
                {
                    TeskilatId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teskilatlar", x => x.TeskilatId);
                });

            migrationBuilder.CreateTable(
                name: "Koordinatorlukler",
                columns: table => new
                {
                    KoordinatorlukId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    TeskilatId = table.Column<int>(type: "int", nullable: false),
                    BaskanPersonelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Koordinatorlukler", x => x.KoordinatorlukId);
                    table.ForeignKey(
                        name: "FK_Koordinatorlukler_Personeller_BaskanPersonelId",
                        column: x => x.BaskanPersonelId,
                        principalTable: "Personeller",
                        principalColumn: "PersonelId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Koordinatorlukler_Teskilatlar_TeskilatId",
                        column: x => x.TeskilatId,
                        principalTable: "Teskilatlar",
                        principalColumn: "TeskilatId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonelTeskilatlar",
                columns: table => new
                {
                    PersonelId = table.Column<int>(type: "int", nullable: false),
                    TeskilatId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonelTeskilatlar", x => new { x.PersonelId, x.TeskilatId });
                    table.ForeignKey(
                        name: "FK_PersonelTeskilatlar_Personeller_PersonelId",
                        column: x => x.PersonelId,
                        principalTable: "Personeller",
                        principalColumn: "PersonelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonelTeskilatlar_Teskilatlar_TeskilatId",
                        column: x => x.TeskilatId,
                        principalTable: "Teskilatlar",
                        principalColumn: "TeskilatId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Komisyonlar",
                columns: table => new
                {
                    KomisyonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    KoordinatorlukId = table.Column<int>(type: "int", nullable: false),
                    BaskanPersonelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Komisyonlar", x => x.KomisyonId);
                    table.ForeignKey(
                        name: "FK_Komisyonlar_Koordinatorlukler_KoordinatorlukId",
                        column: x => x.KoordinatorlukId,
                        principalTable: "Koordinatorlukler",
                        principalColumn: "KoordinatorlukId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Komisyonlar_Personeller_BaskanPersonelId",
                        column: x => x.BaskanPersonelId,
                        principalTable: "Personeller",
                        principalColumn: "PersonelId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "PersonelKoordinatorlukler",
                columns: table => new
                {
                    PersonelId = table.Column<int>(type: "int", nullable: false),
                    KoordinatorlukId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonelKoordinatorlukler", x => new { x.PersonelId, x.KoordinatorlukId });
                    table.ForeignKey(
                        name: "FK_PersonelKoordinatorlukler_Koordinatorlukler_KoordinatorlukId",
                        column: x => x.KoordinatorlukId,
                        principalTable: "Koordinatorlukler",
                        principalColumn: "KoordinatorlukId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonelKoordinatorlukler_Personeller_PersonelId",
                        column: x => x.PersonelId,
                        principalTable: "Personeller",
                        principalColumn: "PersonelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonelKomisyonlar",
                columns: table => new
                {
                    PersonelId = table.Column<int>(type: "int", nullable: false),
                    KomisyonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonelKomisyonlar", x => new { x.PersonelId, x.KomisyonId });
                    table.ForeignKey(
                        name: "FK_PersonelKomisyonlar_Komisyonlar_KomisyonId",
                        column: x => x.KomisyonId,
                        principalTable: "Komisyonlar",
                        principalColumn: "KomisyonId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonelKomisyonlar_Personeller_PersonelId",
                        column: x => x.PersonelId,
                        principalTable: "Personeller",
                        principalColumn: "PersonelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonelKurumsalRolAtamalari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonelId = table.Column<int>(type: "int", nullable: false),
                    KurumsalRolId = table.Column<int>(type: "int", nullable: false),
                    KoordinatorlukId = table.Column<int>(type: "int", nullable: true),
                    KomisyonId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonelKurumsalRolAtamalari", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonelKurumsalRolAtamalari_Komisyonlar_KomisyonId",
                        column: x => x.KomisyonId,
                        principalTable: "Komisyonlar",
                        principalColumn: "KomisyonId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonelKurumsalRolAtamalari_Koordinatorlukler_KoordinatorlukId",
                        column: x => x.KoordinatorlukId,
                        principalTable: "Koordinatorlukler",
                        principalColumn: "KoordinatorlukId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonelKurumsalRolAtamalari_KurumsalRoller_KurumsalRolId",
                        column: x => x.KurumsalRolId,
                        principalTable: "KurumsalRoller",
                        principalColumn: "KurumsalRolId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonelKurumsalRolAtamalari_Personeller_PersonelId",
                        column: x => x.PersonelId,
                        principalTable: "Personeller",
                        principalColumn: "PersonelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "KurumsalRoller",
                columns: new[] { "KurumsalRolId", "Ad" },
                values: new object[,]
                {
                    { 1, "Personel" },
                    { 2, "Komisyon Başkanı" },
                    { 3, "İl Koordinatörü" },
                    { 4, "Genel Koordinatör" }
                });

            migrationBuilder.InsertData(
                table: "Teskilatlar",
                columns: new[] { "TeskilatId", "Ad" },
                values: new object[,]
                {
                    { 1, "Merkez" },
                    { 2, "Taşra" }
                });

            migrationBuilder.InsertData(
                table: "Koordinatorlukler",
                columns: new[] { "KoordinatorlukId", "Ad", "BaskanPersonelId", "TeskilatId" },
                values: new object[,]
                {
                    { 1, "Ankara TEGM Koordinatörlüğü", null, 1 },
                    { 2, "Mardin İl Koordinatörlüğü", null, 2 },
                    { 3, "İzmir İl Koordinatörlüğü", null, 2 }
                });

            migrationBuilder.InsertData(
                table: "Komisyonlar",
                columns: new[] { "KomisyonId", "Ad", "BaskanPersonelId", "KoordinatorlukId" },
                values: new object[,]
                {
                    { 1, "Türkçe Komisyonu", null, 1 },
                    { 2, "Matematik Komisyonu", null, 1 },
                    { 3, "Fen Bilimleri Komisyonu", null, 1 },
                    { 4, "Türkçe Komisyonu", null, 2 },
                    { 5, "Matematik Komisyonu", null, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Komisyonlar_BaskanPersonelId",
                table: "Komisyonlar",
                column: "BaskanPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_Komisyonlar_KoordinatorlukId",
                table: "Komisyonlar",
                column: "KoordinatorlukId");

            migrationBuilder.CreateIndex(
                name: "IX_Koordinatorlukler_BaskanPersonelId",
                table: "Koordinatorlukler",
                column: "BaskanPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_Koordinatorlukler_TeskilatId",
                table: "Koordinatorlukler",
                column: "TeskilatId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonelKomisyonlar_KomisyonId",
                table: "PersonelKomisyonlar",
                column: "KomisyonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonelKoordinatorlukler_KoordinatorlukId",
                table: "PersonelKoordinatorlukler",
                column: "KoordinatorlukId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonelKurumsalRolAtamalari_KomisyonId",
                table: "PersonelKurumsalRolAtamalari",
                column: "KomisyonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonelKurumsalRolAtamalari_KoordinatorlukId",
                table: "PersonelKurumsalRolAtamalari",
                column: "KoordinatorlukId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonelKurumsalRolAtamalari_KurumsalRolId",
                table: "PersonelKurumsalRolAtamalari",
                column: "KurumsalRolId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonelKurumsalRolAtamalari_PersonelId",
                table: "PersonelKurumsalRolAtamalari",
                column: "PersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonelTeskilatlar_TeskilatId",
                table: "PersonelTeskilatlar",
                column: "TeskilatId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonelKomisyonlar");

            migrationBuilder.DropTable(
                name: "PersonelKoordinatorlukler");

            migrationBuilder.DropTable(
                name: "PersonelKurumsalRolAtamalari");

            migrationBuilder.DropTable(
                name: "PersonelTeskilatlar");

            migrationBuilder.DropTable(
                name: "Komisyonlar");

            migrationBuilder.DropTable(
                name: "KurumsalRoller");

            migrationBuilder.DropTable(
                name: "Koordinatorlukler");

            migrationBuilder.DropTable(
                name: "Teskilatlar");

            migrationBuilder.DropColumn(
                name: "SistemRol",
                table: "Personeller");
        }
    }
}
