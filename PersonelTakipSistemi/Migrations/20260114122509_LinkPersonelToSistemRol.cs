using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonelTakipSistemi.Migrations
{
    /// <inheritdoc />
    public partial class LinkPersonelToSistemRol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Add FK Column first (Default 4: Kullanıcı)
            migrationBuilder.AddColumn<int>(
                name: "SistemRolId",
                table: "Personeller",
                type: "int",
                nullable: false,
                defaultValue: 4);

            // 2. Data Migration: Map String Role to ID
            // For SQL Server
            migrationBuilder.Sql(@"
                UPDATE Personeller 
                SET SistemRolId = ISNULL((SELECT TOP 1 SistemRolId FROM SistemRoller WHERE Ad = Personeller.SistemRol), 4)
            ");

            // 3. Drop Old Column
            migrationBuilder.DropColumn(
                name: "SistemRol",
                table: "Personeller");

            migrationBuilder.CreateIndex(
                name: "IX_Personeller_SistemRolId",
                table: "Personeller",
                column: "SistemRolId");

            migrationBuilder.AddForeignKey(
                name: "FK_Personeller_SistemRoller_SistemRolId",
                table: "Personeller",
                column: "SistemRolId",
                principalTable: "SistemRoller",
                principalColumn: "SistemRolId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Personeller_SistemRoller_SistemRolId",
                table: "Personeller");

            migrationBuilder.DropIndex(
                name: "IX_Personeller_SistemRolId",
                table: "Personeller");

            migrationBuilder.DropColumn(
                name: "SistemRolId",
                table: "Personeller");

            migrationBuilder.AddColumn<string>(
                name: "SistemRol",
                table: "Personeller",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
