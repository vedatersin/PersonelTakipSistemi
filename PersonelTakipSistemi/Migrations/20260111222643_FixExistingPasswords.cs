using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonelTakipSistemi.Migrations
{
    /// <inheritdoc />
    public partial class FixExistingPasswords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Backfill existing NULL passwords with first 6 digits of TC
            migrationBuilder.Sql("UPDATE Personeller SET Sifre = SUBSTRING(TcKimlikNo, 1, 6) WHERE Sifre IS NULL OR Sifre = ''");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
