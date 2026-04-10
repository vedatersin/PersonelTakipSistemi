using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace PersonelTakipSistemi.Services
{
    public interface IFileValidationService
    {
        (bool isValid, string message) ValidateExcel(IFormFile file);
        (bool isValid, string message) ValidateImage(IFormFile file);
    }

    public class FileValidationService : IFileValidationService
    {
        private const long MaxExcelSize = 50 * 1024 * 1024; // 50 MB
        private const long MaxImageSize = 5 * 1024 * 1024; // 5 MB

        private readonly Dictionary<string, byte[]> _imageSignatures = new()
        {
            { ".jpg", new byte[] { 0xFF, 0xD8, 0xFF } },
            { ".jpeg", new byte[] { 0xFF, 0xD8, 0xFF } },
            { ".png", new byte[] { 0x89, 0x50, 0x4E, 0x47 } }
        };

        private readonly byte[] _zipSignature = { 0x50, 0x4B, 0x03, 0x04 }; // XLSX is a zip

        public (bool isValid, string message) ValidateExcel(IFormFile file)
        {
            if (file == null || file.Length == 0) return (false, "Dosya seçilmedi.");
            if (file.Length > MaxExcelSize) return (false, "Excel dosyası 50 MB'dan büyük olamaz.");

            var extension = Path.GetExtension(file.FileName).ToLower();
            if (extension != ".xlsx" && extension != ".csv") return (false, "Sadece .xlsx ve .csv formatları kabul edilir.");

            if (extension == ".xlsx")
            {
                using var stream = file.OpenReadStream();
                using var reader = new BinaryReader(stream);
                var headerBytes = reader.ReadBytes(_zipSignature.Length);
                if (!headerBytes.SequenceEqual(_zipSignature))
                    return (false, "Geçersiz Excel dosyası (Magic byte hatası).");
            }
            else if (extension == ".csv")
            {
                // Basic check for CSV: Ensure it's not binary data
                using var stream = file.OpenReadStream();
                var buffer = new byte[Math.Min(stream.Length, 4096)];
                stream.Read(buffer, 0, buffer.Length);

                // Count non-printable characters. If more than 5%, consider it binary.
                int binaryCount = 0;
                for (int i = 0; i < buffer.Length; i++)
                {
                    byte b = buffer[i];
                    // Standard printable ASCII + common control chars (tab, LF, CR)
                    if (b < 32 && b != 9 && b != 10 && b != 13)
                    {
                        binaryCount++;
                    }
                }

                if (buffer.Length > 0 && (double)binaryCount / buffer.Length > 0.05)
                {
                    return (false, "Geçersiz CSV dosyası (Dosya içeriği metin tabanlı değil).");
                }
            }
            
            return (true, "Geçerli.");
        }

        public (bool isValid, string message) ValidateImage(IFormFile file)
        {
            if (file == null || file.Length == 0) return (false, "Dosya seçilmedi.");
            if (file.Length > MaxImageSize) return (false, "Profil fotoğrafı 5 MB'dan büyük olamaz.");

            var extension = Path.GetExtension(file.FileName).ToLower();
            if (!_imageSignatures.ContainsKey(extension)) return (false, "Sadece .jpg, .jpeg ve .png formatları kabul edilir.");

            using var stream = file.OpenReadStream();
            using var reader = new BinaryReader(stream);
            var headerBytes = reader.ReadBytes(4); // Read first 4 bytes
            
            var signature = _imageSignatures[extension];
            if (!headerBytes.Take(signature.Length).SequenceEqual(signature))
                return (false, $"Geçersiz fotoğraf dosyası (Magic byte hatası: {extension}).");

            return (true, "Geçerli.");
        }
    }
}
