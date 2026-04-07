using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using PersonelTakipSistemi.Models;

namespace PersonelTakipSistemi.Services
{
    public class PasswordService : IPasswordService
    {
        private const int Pbkdf2SaltSize = 128 / 8;
        private const int Pbkdf2HashSize = 256 / 8;
        private const int Pbkdf2Iterations = 10000;

        public PasswordHashResult HashPassword(string password)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(password);

            var salt = new byte[Pbkdf2SaltSize];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(salt);

            var hash = KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: Pbkdf2Iterations,
                numBytesRequested: Pbkdf2HashSize);

            return new PasswordHashResult(hash, salt);
        }

        public PasswordVerificationResult VerifyPassword(string password, Personel personel)
        {
            ArgumentNullException.ThrowIfNull(personel);

            if (VerifyCurrentHash(password, personel.SifreHash, personel.SifreSalt))
            {
                var hasPlainTextResidual = !string.IsNullOrEmpty(personel.Sifre);
                return new PasswordVerificationResult(true, hasPlainTextResidual, hasPlainTextResidual ? "PlainTextResidual" : "CurrentHash");
            }

            if (VerifyLegacyHash(password, personel.SifreHash, personel.SifreSalt))
            {
                return new PasswordVerificationResult(true, true, "LegacyHash");
            }

            if (!string.IsNullOrEmpty(personel.Sifre) && string.Equals(personel.Sifre, password, StringComparison.Ordinal))
            {
                return new PasswordVerificationResult(true, true, "PlainTextFallback");
            }

            return new PasswordVerificationResult(false, false, "InvalidPassword");
        }

        public bool VerifyPassword(string password, byte[]? storedHash, byte[]? storedSalt)
        {
            return VerifyCurrentHash(password, storedHash, storedSalt) || VerifyLegacyHash(password, storedHash, storedSalt);
        }

        public void SetPassword(Personel personel, string password)
        {
            ArgumentNullException.ThrowIfNull(personel);

            var result = HashPassword(password);
            personel.Sifre = null;
            personel.SifreHash = result.Hash;
            personel.SifreSalt = result.Salt;
        }

        private static bool VerifyCurrentHash(string password, byte[]? storedHash, byte[]? storedSalt)
        {
            if (string.IsNullOrEmpty(password) || storedHash == null || storedSalt == null)
            {
                return false;
            }

            if (storedHash.Length != Pbkdf2HashSize || storedSalt.Length != Pbkdf2SaltSize)
            {
                return false;
            }

            var computedHash = KeyDerivation.Pbkdf2(
                password: password,
                salt: storedSalt,
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: Pbkdf2Iterations,
                numBytesRequested: Pbkdf2HashSize);

            return CryptographicOperations.FixedTimeEquals(computedHash, storedHash);
        }

        private static bool VerifyLegacyHash(string password, byte[]? storedHash, byte[]? storedSalt)
        {
            if (string.IsNullOrEmpty(password) || storedHash == null || storedSalt == null)
            {
                return false;
            }

            if (storedHash.Length != 64 || storedSalt.Length == 0)
            {
                return false;
            }

            using var hmac = new HMACSHA512(storedSalt);
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return CryptographicOperations.FixedTimeEquals(computedHash, storedHash);
        }
    }
}
