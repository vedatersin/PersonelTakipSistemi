using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PersonelTakipSistemi.Migrations
{
    /// <inheritdoc />
    public partial class RefactorGorevDurumToTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Gorevler_Durum",
                table: "Gorevler");

            migrationBuilder.DropColumn(
                name: "Durum",
                table: "Gorevler");

            migrationBuilder.AddColumn<int>(
                name: "GorevDurumId",
                table: "Gorevler",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "GorevDurumlari",
                columns: table => new
                {
                    GorevDurumId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Sira = table.Column<int>(type: "int", nullable: false),
                    RenkSinifi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GorevDurumlari", x => x.GorevDurumId);
                });

            migrationBuilder.InsertData(
                table: "GorevDurumlari",
                columns: new[] { "GorevDurumId", "Ad", "RenkSinifi", "Sira" },
                values: new object[,]
                {
                    { 1, "Atanmayı Bekliyor", "bg-warning", 1 },
                    { 2, "Devam Ediyor", "bg-primary", 2 },
                    { 3, "Kontrolde", "bg-info", 3 },
                    { 4, "Tamamlandı", "bg-success", 4 },
                    { 5, "İptal", "bg-secondary", 5 }
                });

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 28, 13, 13, 25, 960, DateTimeKind.Local).AddTicks(3031));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 28, 13, 13, 25, 960, DateTimeKind.Local).AddTicks(3044));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 28, 13, 13, 25, 960, DateTimeKind.Local).AddTicks(3045));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 28, 13, 13, 25, 960, DateTimeKind.Local).AddTicks(3046));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 1,
                columns: new[] { "CreatedAt", "GorevDurumId" },
                values: new object[] { new DateTime(2026, 1, 28, 13, 13, 25, 960, DateTimeKind.Local).AddTicks(5680), 2 });

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 2,
                columns: new[] { "CreatedAt", "GorevDurumId" },
                values: new object[] { new DateTime(2026, 1, 28, 13, 13, 25, 960, DateTimeKind.Local).AddTicks(5691), 3 });

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 3,
                columns: new[] { "CreatedAt", "GorevDurumId" },
                values: new object[] { new DateTime(2026, 1, 28, 13, 13, 25, 960, DateTimeKind.Local).AddTicks(5693), 1 });

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 4,
                columns: new[] { "CreatedAt", "GorevDurumId" },
                values: new object[] { new DateTime(2026, 1, 28, 13, 13, 25, 960, DateTimeKind.Local).AddTicks(5695), 2 });

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 5,
                columns: new[] { "CreatedAt", "GorevDurumId" },
                values: new object[] { new DateTime(2026, 1, 28, 13, 13, 25, 960, DateTimeKind.Local).AddTicks(5697), 2 });

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 6,
                columns: new[] { "CreatedAt", "GorevDurumId" },
                values: new object[] { new DateTime(2026, 1, 28, 13, 13, 25, 960, DateTimeKind.Local).AddTicks(5700), 4 });

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 7,
                columns: new[] { "CreatedAt", "GorevDurumId" },
                values: new object[] { new DateTime(2026, 1, 28, 13, 13, 25, 960, DateTimeKind.Local).AddTicks(5702), 1 });

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 8,
                columns: new[] { "CreatedAt", "GorevDurumId" },
                values: new object[] { new DateTime(2026, 1, 28, 13, 13, 25, 960, DateTimeKind.Local).AddTicks(5726), 2 });

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 9,
                columns: new[] { "CreatedAt", "GorevDurumId" },
                values: new object[] { new DateTime(2026, 1, 28, 13, 13, 25, 960, DateTimeKind.Local).AddTicks(5728), 4 });

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 10,
                columns: new[] { "CreatedAt", "GorevDurumId" },
                values: new object[] { new DateTime(2026, 1, 28, 13, 13, 25, 960, DateTimeKind.Local).AddTicks(5730), 2 });

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 11,
                columns: new[] { "CreatedAt", "GorevDurumId" },
                values: new object[] { new DateTime(2026, 1, 28, 13, 13, 25, 960, DateTimeKind.Local).AddTicks(5732), 1 });

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 12,
                columns: new[] { "CreatedAt", "GorevDurumId" },
                values: new object[] { new DateTime(2026, 1, 28, 13, 13, 25, 960, DateTimeKind.Local).AddTicks(5734), 4 });

            migrationBuilder.CreateIndex(
                name: "IX_Gorevler_GorevDurumId",
                table: "Gorevler",
                column: "GorevDurumId");

            migrationBuilder.AddForeignKey(
                name: "FK_Gorevler_GorevDurumlari_GorevDurumId",
                table: "Gorevler",
                column: "GorevDurumId",
                principalTable: "GorevDurumlari",
                principalColumn: "GorevDurumId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gorevler_GorevDurumlari_GorevDurumId",
                table: "Gorevler");

            migrationBuilder.DropTable(
                name: "GorevDurumlari");

            migrationBuilder.DropIndex(
                name: "IX_Gorevler_GorevDurumId",
                table: "Gorevler");

            migrationBuilder.DropColumn(
                name: "GorevDurumId",
                table: "Gorevler");

            migrationBuilder.AddColumn<byte>(
                name: "Durum",
                table: "Gorevler",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 27, 23, 43, 23, 439, DateTimeKind.Local).AddTicks(2718));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 27, 23, 43, 23, 439, DateTimeKind.Local).AddTicks(2730));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 27, 23, 43, 23, 439, DateTimeKind.Local).AddTicks(2731));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 27, 23, 43, 23, 439, DateTimeKind.Local).AddTicks(2752));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Durum" },
                values: new object[] { new DateTime(2026, 1, 27, 23, 43, 23, 439, DateTimeKind.Local).AddTicks(4735), (byte)1 });

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Durum" },
                values: new object[] { new DateTime(2026, 1, 27, 23, 43, 23, 439, DateTimeKind.Local).AddTicks(4750), (byte)2 });

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 3,
                columns: new[] { "CreatedAt", "Durum" },
                values: new object[] { new DateTime(2026, 1, 27, 23, 43, 23, 439, DateTimeKind.Local).AddTicks(4753), (byte)0 });

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 4,
                columns: new[] { "CreatedAt", "Durum" },
                values: new object[] { new DateTime(2026, 1, 27, 23, 43, 23, 439, DateTimeKind.Local).AddTicks(4755), (byte)1 });

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 5,
                columns: new[] { "CreatedAt", "Durum" },
                values: new object[] { new DateTime(2026, 1, 27, 23, 43, 23, 439, DateTimeKind.Local).AddTicks(4757), (byte)1 });

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 6,
                columns: new[] { "CreatedAt", "Durum" },
                values: new object[] { new DateTime(2026, 1, 27, 23, 43, 23, 439, DateTimeKind.Local).AddTicks(4759), (byte)2 });

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 7,
                columns: new[] { "CreatedAt", "Durum" },
                values: new object[] { new DateTime(2026, 1, 27, 23, 43, 23, 439, DateTimeKind.Local).AddTicks(4761), (byte)0 });

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 8,
                columns: new[] { "CreatedAt", "Durum" },
                values: new object[] { new DateTime(2026, 1, 27, 23, 43, 23, 439, DateTimeKind.Local).AddTicks(4763), (byte)1 });

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 9,
                columns: new[] { "CreatedAt", "Durum" },
                values: new object[] { new DateTime(2026, 1, 27, 23, 43, 23, 439, DateTimeKind.Local).AddTicks(4764), (byte)2 });

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 10,
                columns: new[] { "CreatedAt", "Durum" },
                values: new object[] { new DateTime(2026, 1, 27, 23, 43, 23, 439, DateTimeKind.Local).AddTicks(4767), (byte)1 });

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 11,
                columns: new[] { "CreatedAt", "Durum" },
                values: new object[] { new DateTime(2026, 1, 27, 23, 43, 23, 439, DateTimeKind.Local).AddTicks(4769), (byte)0 });

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 12,
                columns: new[] { "CreatedAt", "Durum" },
                values: new object[] { new DateTime(2026, 1, 27, 23, 43, 23, 439, DateTimeKind.Local).AddTicks(4770), (byte)2 });

            migrationBuilder.CreateIndex(
                name: "IX_Gorevler_Durum",
                table: "Gorevler",
                column: "Durum");
        }
    }
}
