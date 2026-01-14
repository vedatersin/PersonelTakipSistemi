using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonelTakipSistemi.Migrations
{
    /// <inheritdoc />
    public partial class FixAuthorizationData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. CLEANUP ALL AUTHORIZATION DATA
            // Clear Many-to-Many and Association Tables
            migrationBuilder.Sql("DELETE FROM PersonelTeskilatlar");
            migrationBuilder.Sql("DELETE FROM PersonelKoordinatorlukler");
            migrationBuilder.Sql("DELETE FROM PersonelKomisyonlar");
            migrationBuilder.Sql("DELETE FROM PersonelKurumsalRolAtamalari");

            // 2. RESET ROLES FOR EVERYONE
            migrationBuilder.Sql("UPDATE Personeller SET SistemRolId = NULL");

            // 3. ASSIGN SPECIFIC ROLES
            
            // Vedat Ersin Ceviz (12514374546) -> Admin (1)
            migrationBuilder.Sql(@"
                UPDATE Personeller 
                SET SistemRolId = 1 
                WHERE TcKimlikNo = '12514374546'
            ");

            // Sevilay Seryol Şen -> Admin (1)
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

            // Zeynep Yılmaz (User mentioned "zeynep" possibly) -> Kullanıcı (4) 
            // Also checking by Name just in case the TC is different or name is relevant
            migrationBuilder.Sql(@"
                UPDATE Personeller 
                SET SistemRolId = 4 
                WHERE Ad LIKE '%Zeynep%' AND Soyad LIKE '%Yılmaz%'
            ");

            // NOTE: No KurumsalRol (Personel) assigned, as per request "kurumsal rolü null olsun".
            // So PersonelKurumsalRolAtamalari remains empty for them.
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
