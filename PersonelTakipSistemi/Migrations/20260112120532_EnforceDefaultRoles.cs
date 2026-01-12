using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonelTakipSistemi.Migrations
{
    /// <inheritdoc />
    public partial class EnforceDefaultRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Set default SistemRol for anyone missing it
            migrationBuilder.Sql("UPDATE Personeller SET SistemRol = 'Kullanıcı' WHERE SistemRol IS NULL OR SistemRol = ''");

            // 2. Ensure everyone has the 'Personel' (Id=1) KurumsalRol
            // Insert into assignment table if not exists
            migrationBuilder.Sql(@"
                INSERT INTO PersonelKurumsalRolAtamalari (PersonelId, KurumsalRolId, CreatedAt)
                SELECT p.PersonelId, 1, GETDATE()
                FROM Personeller p
                WHERE NOT EXISTS (
                    SELECT 1 FROM PersonelKurumsalRolAtamalari a 
                    WHERE a.PersonelId = p.PersonelId AND a.KurumsalRolId = 1
                )
            ");

            // 3. Re-assert Vedat is Admin (just in case)
            migrationBuilder.Sql("UPDATE Personeller SET SistemRol = 'Admin' WHERE TcKimlikNo = '12514374546'");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
