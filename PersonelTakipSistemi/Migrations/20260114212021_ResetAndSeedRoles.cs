using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonelTakipSistemi.Migrations
{
    /// <inheritdoc />
    public partial class ResetAndSeedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Reset All Personnel Roles (SistemRolId -> NULL)
            migrationBuilder.Sql("UPDATE Personeller SET SistemRolId = NULL");

            // 2. Clear All Authorization Records
            migrationBuilder.Sql("DELETE FROM PersonelKurumsalRolAtamalari");

            // 3. Set Specific Users Roles
            
            // Vedat Ersin Ceviz (12514374546) -> Admin (1)
            migrationBuilder.Sql(@"
                UPDATE Personeller 
                SET SistemRolId = 1 
                WHERE TcKimlikNo = '12514374546'
            ");

            // Sevilay Seryol Şen -> Admin (1) (Searching by Name/Surname fuzzy)
            migrationBuilder.Sql(@"
                UPDATE Personeller 
                SET SistemRolId = 1 
                WHERE Ad LIKE '%Sevilay%' AND Soyad LIKE '%Şen%'
            ");

            // 22222222222 -> Kullanıcı (4)
            migrationBuilder.Sql(@"
                UPDATE Personeller 
                SET SistemRolId = 4 
                WHERE TcKimlikNo = '22222222222'
            ");

            // 4. Assign 'Personel' (1) Kurumsal Rol to these 3 users
            
            // Vedat
            migrationBuilder.Sql(@"
                INSERT INTO PersonelKurumsalRolAtamalari (PersonelId, KurumsalRolId, CreatedAt)
                SELECT PersonelId, 1, GETDATE() 
                FROM Personeller 
                WHERE TcKimlikNo = '12514374546'
            ");

            // Sevilay
            migrationBuilder.Sql(@"
                INSERT INTO PersonelKurumsalRolAtamalari (PersonelId, KurumsalRolId, CreatedAt)
                SELECT PersonelId, 1, GETDATE() 
                FROM Personeller 
                WHERE Ad LIKE '%Sevilay%' AND Soyad LIKE '%Şen%'
            ");

            // 22222222222
            migrationBuilder.Sql(@"
                INSERT INTO PersonelKurumsalRolAtamalari (PersonelId, KurumsalRolId, CreatedAt)
                SELECT PersonelId, 1, GETDATE() 
                FROM Personeller 
                WHERE TcKimlikNo = '22222222222'
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
