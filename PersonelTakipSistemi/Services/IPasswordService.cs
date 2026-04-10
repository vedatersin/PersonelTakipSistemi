using PersonelTakipSistemi.Models;

namespace PersonelTakipSistemi.Services
{
    public interface IPasswordService
    {
        PasswordHashResult HashPassword(string password);
        PasswordVerificationResult VerifyPassword(string password, Personel personel);
        bool VerifyPassword(string password, byte[]? storedHash, byte[]? storedSalt);
        void SetPassword(Personel personel, string password);
    }
}
