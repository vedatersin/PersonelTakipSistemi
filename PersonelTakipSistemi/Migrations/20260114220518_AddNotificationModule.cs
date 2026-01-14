using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonelTakipSistemi.Migrations
{
    /// <inheritdoc />
    public partial class AddNotificationModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<int>(
                name: "BildirimGonderenId",
                table: "Bildirimler",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TopluBildirimId",
                table: "Bildirimler",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BildirimGonderenler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tip = table.Column<int>(type: "int", nullable: false),
                    PersonelId = table.Column<int>(type: "int", nullable: true),
                    GorunenAd = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AvatarUrl = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BildirimGonderenler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BildirimGonderenler_Personeller_PersonelId",
                        column: x => x.PersonelId,
                        principalTable: "Personeller",
                        principalColumn: "PersonelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TopluBildirimler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GonderenId = table.Column<int>(type: "int", nullable: false),
                    Baslik = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Icerik = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OlusturmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlanlananZaman = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GonderimZamani = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Durum = table.Column<int>(type: "int", nullable: false),
                    HedefKitleJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecipientIdsJson = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopluBildirimler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TopluBildirimler_BildirimGonderenler_GonderenId",
                        column: x => x.GonderenId,
                        principalTable: "BildirimGonderenler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bildirimler_BildirimGonderenId",
                table: "Bildirimler",
                column: "BildirimGonderenId");

            migrationBuilder.CreateIndex(
                name: "IX_Bildirimler_TopluBildirimId",
                table: "Bildirimler",
                column: "TopluBildirimId");

            migrationBuilder.CreateIndex(
                name: "IX_BildirimGonderenler_PersonelId",
                table: "BildirimGonderenler",
                column: "PersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_TopluBildirimler_GonderenId",
                table: "TopluBildirimler",
                column: "GonderenId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bildirimler_BildirimGonderenler_BildirimGonderenId",
                table: "Bildirimler",
                column: "BildirimGonderenId",
                principalTable: "BildirimGonderenler",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bildirimler_TopluBildirimler_TopluBildirimId",
                table: "Bildirimler",
                column: "TopluBildirimId",
                principalTable: "TopluBildirimler",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            // --- SEEDING "Sistem" SENDER ---
            // Id 1 will be reserved for System.
            migrationBuilder.Sql("SET IDENTITY_INSERT BildirimGonderenler ON");
            migrationBuilder.Sql("INSERT INTO BildirimGonderenler (Id, Tip, PersonelId, GorunenAd, AvatarUrl) VALUES (1, 1, NULL, 'Sistem', '/img/system_avatar.png')");
            migrationBuilder.Sql("SET IDENTITY_INSERT BildirimGonderenler OFF");

            // --- MIGRATING EXISTING NOTIFICATIONS ---
            // If we have notifications with GonderenPersonelId, we need corresponding BildirimGonderenler entries.

            // A. Insert missing Senders for existing GonderenPersonelId
            migrationBuilder.Sql(@"
                INSERT INTO BildirimGonderenler (Tip, PersonelId, GorunenAd, AvatarUrl)
                SELECT DISTINCT 
                    2 as Tip, 
                    p.PersonelId, 
                    p.Ad + ' ' + p.Soyad as GorunenAd, 
                    p.FotografYolu 
                FROM Bildirimler b
                JOIN Personeller p ON b.GonderenPersonelId = p.PersonelId
                WHERE b.GonderenPersonelId IS NOT NULL
                AND NOT EXISTS (SELECT 1 FROM BildirimGonderenler bg WHERE bg.PersonelId = p.PersonelId)
            ");

            // B. Update Bildirim.BildirimGonderenId
            // Case 1: GonderenPersonelId is NULL -> Set to System (1)
            migrationBuilder.Sql("UPDATE Bildirimler SET BildirimGonderenId = 1 WHERE GonderenPersonelId IS NULL");

            // Case 2: GonderenPersonelId is NOT NULL -> Set to coresponding Personel Sender
            migrationBuilder.Sql(@"
                UPDATE b
                SET b.BildirimGonderenId = bg.Id
                FROM Bildirimler b
                JOIN BildirimGonderenler bg ON b.GonderenPersonelId = bg.PersonelId
                WHERE b.GonderenPersonelId IS NOT NULL
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bildirimler_BildirimGonderenler_BildirimGonderenId",
                table: "Bildirimler");

            migrationBuilder.DropForeignKey(
                name: "FK_Bildirimler_TopluBildirimler_TopluBildirimId",
                table: "Bildirimler");

            migrationBuilder.DropTable(
                name: "TopluBildirimler");

            migrationBuilder.DropTable(
                name: "BildirimGonderenler");

            migrationBuilder.DropIndex(
                name: "IX_Bildirimler_BildirimGonderenId",
                table: "Bildirimler");

            migrationBuilder.DropIndex(
                name: "IX_Bildirimler_TopluBildirimId",
                table: "Bildirimler");

            migrationBuilder.DropColumn(
                name: "BildirimGonderenId",
                table: "Bildirimler");

            migrationBuilder.DropColumn(
                name: "TopluBildirimId",
                table: "Bildirimler");
        }
    }
}
