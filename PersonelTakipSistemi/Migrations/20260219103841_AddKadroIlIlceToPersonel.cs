using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PersonelTakipSistemi.Migrations
{
    /// <inheritdoc />
    public partial class AddKadroIlIlceToPersonel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KadroIlId",
                table: "Personeller",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "KadroIlceId",
                table: "Personeller",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Ilceler",
                columns: table => new
                {
                    IlceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IlId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ilceler", x => x.IlceId);
                    table.ForeignKey(
                        name: "FK_Ilceler_Iller_IlId",
                        column: x => x.IlId,
                        principalTable: "Iller",
                        principalColumn: "IlId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 13, 38, 41, 175, DateTimeKind.Local).AddTicks(7441));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 13, 38, 41, 175, DateTimeKind.Local).AddTicks(7448));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 13, 38, 41, 175, DateTimeKind.Local).AddTicks(7449));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 13, 38, 41, 175, DateTimeKind.Local).AddTicks(7450));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 13, 38, 41, 176, DateTimeKind.Local).AddTicks(1141));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 13, 38, 41, 176, DateTimeKind.Local).AddTicks(1160));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 13, 38, 41, 176, DateTimeKind.Local).AddTicks(1163));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 13, 38, 41, 176, DateTimeKind.Local).AddTicks(1165));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 13, 38, 41, 176, DateTimeKind.Local).AddTicks(1167));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 13, 38, 41, 176, DateTimeKind.Local).AddTicks(1170));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 13, 38, 41, 176, DateTimeKind.Local).AddTicks(1229));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 13, 38, 41, 176, DateTimeKind.Local).AddTicks(1232));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 13, 38, 41, 176, DateTimeKind.Local).AddTicks(1234));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 13, 38, 41, 176, DateTimeKind.Local).AddTicks(1237));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 13, 38, 41, 176, DateTimeKind.Local).AddTicks(1239));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 13, 38, 41, 176, DateTimeKind.Local).AddTicks(1241));

            migrationBuilder.InsertData(
                table: "Ilceler",
                columns: new[] { "IlceId", "Ad", "IlId" },
                values: new object[,]
                {
                    { 1, "Çankaya", 6 },
                    { 2, "Keçiören", 6 },
                    { 3, "Yenimahalle", 6 },
                    { 4, "Mamak", 6 },
                    { 5, "Etimesgut", 6 },
                    { 6, "Sincan", 6 },
                    { 7, "Altındağ", 6 },
                    { 8, "Pursaklar", 6 },
                    { 9, "Gölbaşı", 6 },
                    { 10, "Esenyurt", 34 },
                    { 11, "Şahinbey", 34 },
                    { 12, "Çankaya", 34 },
                    { 13, "Üsküdar", 34 },
                    { 14, "Kadıköy", 34 },
                    { 15, "Beşiktaş", 34 },
                    { 16, "Şişli", 34 },
                    { 17, "Fatih", 34 },
                    { 18, "Beyoğlu", 34 },
                    { 19, "Bakırköy", 34 },
                    { 20, "Buca", 35 },
                    { 21, "Karabağlar", 35 },
                    { 22, "Bornova", 35 },
                    { 23, "Konak", 35 },
                    { 24, "Karşıyaka", 35 },
                    { 25, "Bayraklı", 35 }
                });

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 13, 38, 41, 171, DateTimeKind.Local).AddTicks(5207));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 13, 38, 41, 171, DateTimeKind.Local).AddTicks(5208));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 13, 38, 41, 171, DateTimeKind.Local).AddTicks(5189));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 13, 38, 41, 171, DateTimeKind.Local).AddTicks(5195));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 13, 38, 41, 171, DateTimeKind.Local).AddTicks(5196));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 13, 38, 41, 171, DateTimeKind.Local).AddTicks(5197));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 13, 38, 41, 171, DateTimeKind.Local).AddTicks(5198));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 13, 38, 41, 171, DateTimeKind.Local).AddTicks(5199));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 13, 38, 41, 171, DateTimeKind.Local).AddTicks(5199));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 13, 38, 41, 171, DateTimeKind.Local).AddTicks(5200));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 13, 38, 41, 171, DateTimeKind.Local).AddTicks(5201));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 13, 38, 41, 171, DateTimeKind.Local).AddTicks(5202));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 13, 38, 41, 171, DateTimeKind.Local).AddTicks(5203));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 13, 38, 41, 171, DateTimeKind.Local).AddTicks(5203));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 16,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 13, 38, 41, 171, DateTimeKind.Local).AddTicks(5204));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 17,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 13, 38, 41, 171, DateTimeKind.Local).AddTicks(5205));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 18,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 13, 38, 41, 171, DateTimeKind.Local).AddTicks(5206));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 19,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 13, 38, 41, 171, DateTimeKind.Local).AddTicks(5206));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 13, 38, 41, 171, DateTimeKind.Local).AddTicks(4013));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 13, 38, 41, 171, DateTimeKind.Local).AddTicks(4028));

            migrationBuilder.CreateIndex(
                name: "IX_Personeller_KadroIlceId",
                table: "Personeller",
                column: "KadroIlceId");

            migrationBuilder.CreateIndex(
                name: "IX_Personeller_KadroIlId",
                table: "Personeller",
                column: "KadroIlId");

            migrationBuilder.CreateIndex(
                name: "IX_Ilceler_IlId",
                table: "Ilceler",
                column: "IlId");

            migrationBuilder.AddForeignKey(
                name: "FK_Personeller_Ilceler_KadroIlceId",
                table: "Personeller",
                column: "KadroIlceId",
                principalTable: "Ilceler",
                principalColumn: "IlceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Personeller_Iller_KadroIlId",
                table: "Personeller",
                column: "KadroIlId",
                principalTable: "Iller",
                principalColumn: "IlId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Personeller_Ilceler_KadroIlceId",
                table: "Personeller");

            migrationBuilder.DropForeignKey(
                name: "FK_Personeller_Iller_KadroIlId",
                table: "Personeller");

            migrationBuilder.DropTable(
                name: "Ilceler");

            migrationBuilder.DropIndex(
                name: "IX_Personeller_KadroIlceId",
                table: "Personeller");

            migrationBuilder.DropIndex(
                name: "IX_Personeller_KadroIlId",
                table: "Personeller");

            migrationBuilder.DropColumn(
                name: "KadroIlId",
                table: "Personeller");

            migrationBuilder.DropColumn(
                name: "KadroIlceId",
                table: "Personeller");

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 14, 32, 19, 421, DateTimeKind.Local).AddTicks(1744));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 14, 32, 19, 421, DateTimeKind.Local).AddTicks(1756));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 14, 32, 19, 421, DateTimeKind.Local).AddTicks(1758));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 14, 32, 19, 421, DateTimeKind.Local).AddTicks(1759));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 14, 32, 19, 421, DateTimeKind.Local).AddTicks(4595));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 14, 32, 19, 421, DateTimeKind.Local).AddTicks(4613));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 14, 32, 19, 421, DateTimeKind.Local).AddTicks(4616));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 14, 32, 19, 421, DateTimeKind.Local).AddTicks(4618));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 14, 32, 19, 421, DateTimeKind.Local).AddTicks(4620));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 14, 32, 19, 421, DateTimeKind.Local).AddTicks(4623));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 14, 32, 19, 421, DateTimeKind.Local).AddTicks(4625));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 14, 32, 19, 421, DateTimeKind.Local).AddTicks(4627));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 14, 32, 19, 421, DateTimeKind.Local).AddTicks(4630));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 14, 32, 19, 421, DateTimeKind.Local).AddTicks(4646));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 14, 32, 19, 421, DateTimeKind.Local).AddTicks(4649));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 14, 32, 19, 421, DateTimeKind.Local).AddTicks(4651));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 14, 32, 19, 416, DateTimeKind.Local).AddTicks(2786));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 14, 32, 19, 416, DateTimeKind.Local).AddTicks(2787));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 14, 32, 19, 416, DateTimeKind.Local).AddTicks(2746));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 14, 32, 19, 416, DateTimeKind.Local).AddTicks(2750));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 14, 32, 19, 416, DateTimeKind.Local).AddTicks(2751));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 14, 32, 19, 416, DateTimeKind.Local).AddTicks(2752));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 14, 32, 19, 416, DateTimeKind.Local).AddTicks(2753));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 14, 32, 19, 416, DateTimeKind.Local).AddTicks(2777));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 14, 32, 19, 416, DateTimeKind.Local).AddTicks(2778));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 14, 32, 19, 416, DateTimeKind.Local).AddTicks(2779));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 14, 32, 19, 416, DateTimeKind.Local).AddTicks(2780));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 14, 32, 19, 416, DateTimeKind.Local).AddTicks(2781));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 14, 32, 19, 416, DateTimeKind.Local).AddTicks(2782));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 14, 32, 19, 416, DateTimeKind.Local).AddTicks(2782));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 16,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 14, 32, 19, 416, DateTimeKind.Local).AddTicks(2783));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 17,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 14, 32, 19, 416, DateTimeKind.Local).AddTicks(2784));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 18,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 14, 32, 19, 416, DateTimeKind.Local).AddTicks(2785));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 19,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 14, 32, 19, 416, DateTimeKind.Local).AddTicks(2785));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 14, 32, 19, 416, DateTimeKind.Local).AddTicks(1467));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 14, 32, 19, 416, DateTimeKind.Local).AddTicks(1482));
        }
    }
}
