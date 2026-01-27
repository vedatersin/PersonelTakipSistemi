using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonelTakipSistemi.Migrations
{
    /// <inheritdoc />
    public partial class AddGorevAssignments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GorevAtamaKomisyonlar",
                columns: table => new
                {
                    GorevId = table.Column<int>(type: "int", nullable: false),
                    KomisyonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GorevAtamaKomisyonlar", x => new { x.GorevId, x.KomisyonId });
                    table.ForeignKey(
                        name: "FK_GorevAtamaKomisyonlar_Gorevler_GorevId",
                        column: x => x.GorevId,
                        principalTable: "Gorevler",
                        principalColumn: "GorevId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GorevAtamaKomisyonlar_Komisyonlar_KomisyonId",
                        column: x => x.KomisyonId,
                        principalTable: "Komisyonlar",
                        principalColumn: "KomisyonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GorevAtamaKoordinatorlukler",
                columns: table => new
                {
                    GorevId = table.Column<int>(type: "int", nullable: false),
                    KoordinatorlukId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GorevAtamaKoordinatorlukler", x => new { x.GorevId, x.KoordinatorlukId });
                    table.ForeignKey(
                        name: "FK_GorevAtamaKoordinatorlukler_Gorevler_GorevId",
                        column: x => x.GorevId,
                        principalTable: "Gorevler",
                        principalColumn: "GorevId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GorevAtamaKoordinatorlukler_Koordinatorlukler_KoordinatorlukId",
                        column: x => x.KoordinatorlukId,
                        principalTable: "Koordinatorlukler",
                        principalColumn: "KoordinatorlukId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GorevAtamaPersoneller",
                columns: table => new
                {
                    GorevId = table.Column<int>(type: "int", nullable: false),
                    PersonelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GorevAtamaPersoneller", x => new { x.GorevId, x.PersonelId });
                    table.ForeignKey(
                        name: "FK_GorevAtamaPersoneller_Gorevler_GorevId",
                        column: x => x.GorevId,
                        principalTable: "Gorevler",
                        principalColumn: "GorevId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GorevAtamaPersoneller_Personeller_PersonelId",
                        column: x => x.PersonelId,
                        principalTable: "Personeller",
                        principalColumn: "PersonelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GorevAtamaTeskilatlar",
                columns: table => new
                {
                    GorevId = table.Column<int>(type: "int", nullable: false),
                    TeskilatId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GorevAtamaTeskilatlar", x => new { x.GorevId, x.TeskilatId });
                    table.ForeignKey(
                        name: "FK_GorevAtamaTeskilatlar_Gorevler_GorevId",
                        column: x => x.GorevId,
                        principalTable: "Gorevler",
                        principalColumn: "GorevId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GorevAtamaTeskilatlar_Teskilatlar_TeskilatId",
                        column: x => x.TeskilatId,
                        principalTable: "Teskilatlar",
                        principalColumn: "TeskilatId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 27, 21, 43, 3, 320, DateTimeKind.Local).AddTicks(1799));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 27, 21, 43, 3, 320, DateTimeKind.Local).AddTicks(1811));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 27, 21, 43, 3, 320, DateTimeKind.Local).AddTicks(1812));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 27, 21, 43, 3, 320, DateTimeKind.Local).AddTicks(1812));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 27, 21, 43, 3, 320, DateTimeKind.Local).AddTicks(4787));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 27, 21, 43, 3, 320, DateTimeKind.Local).AddTicks(4796));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 27, 21, 43, 3, 320, DateTimeKind.Local).AddTicks(4798));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 27, 21, 43, 3, 320, DateTimeKind.Local).AddTicks(4800));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 27, 21, 43, 3, 320, DateTimeKind.Local).AddTicks(4802));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 27, 21, 43, 3, 320, DateTimeKind.Local).AddTicks(4828));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 27, 21, 43, 3, 320, DateTimeKind.Local).AddTicks(4830));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 27, 21, 43, 3, 320, DateTimeKind.Local).AddTicks(4831));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 27, 21, 43, 3, 320, DateTimeKind.Local).AddTicks(4833));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 27, 21, 43, 3, 320, DateTimeKind.Local).AddTicks(4835));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 27, 21, 43, 3, 320, DateTimeKind.Local).AddTicks(4836));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 27, 21, 43, 3, 320, DateTimeKind.Local).AddTicks(4837));

            migrationBuilder.CreateIndex(
                name: "IX_GorevAtamaKomisyonlar_KomisyonId",
                table: "GorevAtamaKomisyonlar",
                column: "KomisyonId");

            migrationBuilder.CreateIndex(
                name: "IX_GorevAtamaKoordinatorlukler_KoordinatorlukId",
                table: "GorevAtamaKoordinatorlukler",
                column: "KoordinatorlukId");

            migrationBuilder.CreateIndex(
                name: "IX_GorevAtamaPersoneller_PersonelId",
                table: "GorevAtamaPersoneller",
                column: "PersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_GorevAtamaTeskilatlar_TeskilatId",
                table: "GorevAtamaTeskilatlar",
                column: "TeskilatId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GorevAtamaKomisyonlar");

            migrationBuilder.DropTable(
                name: "GorevAtamaKoordinatorlukler");

            migrationBuilder.DropTable(
                name: "GorevAtamaPersoneller");

            migrationBuilder.DropTable(
                name: "GorevAtamaTeskilatlar");

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 27, 14, 13, 20, 840, DateTimeKind.Local).AddTicks(8365));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 27, 14, 13, 20, 840, DateTimeKind.Local).AddTicks(8376));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 27, 14, 13, 20, 840, DateTimeKind.Local).AddTicks(8377));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 27, 14, 13, 20, 840, DateTimeKind.Local).AddTicks(8378));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 27, 14, 13, 20, 841, DateTimeKind.Local).AddTicks(453));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 27, 14, 13, 20, 841, DateTimeKind.Local).AddTicks(467));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 27, 14, 13, 20, 841, DateTimeKind.Local).AddTicks(469));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 27, 14, 13, 20, 841, DateTimeKind.Local).AddTicks(471));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 27, 14, 13, 20, 841, DateTimeKind.Local).AddTicks(473));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 27, 14, 13, 20, 841, DateTimeKind.Local).AddTicks(475));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 27, 14, 13, 20, 841, DateTimeKind.Local).AddTicks(477));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 27, 14, 13, 20, 841, DateTimeKind.Local).AddTicks(479));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 27, 14, 13, 20, 841, DateTimeKind.Local).AddTicks(480));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 27, 14, 13, 20, 841, DateTimeKind.Local).AddTicks(483));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 27, 14, 13, 20, 841, DateTimeKind.Local).AddTicks(486));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 27, 14, 13, 20, 841, DateTimeKind.Local).AddTicks(488));
        }
    }
}
