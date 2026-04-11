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

        private static readonly HashSet<string> AllowedImageMimeTypes = new(StringComparer.OrdinalIgnoreCase)
        {
            "image/jpeg",
            "image/pjpeg",
            "image/png"
        };

        private static readonly Dictionary<string, FileSignatureRule> ImageSignatureRules = new(StringComparer.OrdinalIgnoreCase)
        {
            [".jpg"] = new FileSignatureRule(
                "JPEG",
                [new byte[] { 0xFF, 0xD8, 0xFF }],
                [new byte[] { 0xFF, 0xD9 }]),
            [".jpeg"] = new FileSignatureRule(
                "JPEG",
                [new byte[] { 0xFF, 0xD8, 0xFF }],
                [new byte[] { 0xFF, 0xD9 }]),
            [".png"] = new FileSignatureRule(
                "PNG",
                [new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A }],
                [new byte[] { 0x49, 0x45, 0x4E, 0x44, 0xAE, 0x42, 0x60, 0x82 }])
        };

        private static readonly byte[] ZipSignature = { 0x50, 0x4B, 0x03, 0x04 }; // XLSX is a zip

        public (bool isValid, string message) ValidateExcel(IFormFile file)
        {
            if (file == null || file.Length == 0) return (false, "Dosya seçilmedi.");
            if (file.Length > MaxExcelSize) return (false, "Excel dosyası 50 MB'dan büyük olamaz.");

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (extension != ".xlsx" && extension != ".csv") return (false, "Sadece .xlsx ve .csv formatları kabul edilir.");

            if (extension == ".xlsx")
            {
                using var stream = file.OpenReadStream();
                using var reader = new BinaryReader(stream);
                var headerBytes = reader.ReadBytes(ZipSignature.Length);
                if (!headerBytes.SequenceEqual(ZipSignature))
                {
                    return (false, "Geçersiz Excel dosyası (magic byte hatası).");
                }
            }
            else
            {
                // Basic check for CSV: ensure it's not binary data.
                using var stream = file.OpenReadStream();
                var buffer = new byte[Math.Min(stream.Length, 4096)];
                _ = stream.Read(buffer, 0, buffer.Length);

                var binaryCount = 0;
                foreach (var value in buffer)
                {
                    if (value < 32 && value != 9 && value != 10 && value != 13)
                    {
                        binaryCount++;
                    }
                }

                if (buffer.Length > 0 && (double)binaryCount / buffer.Length > 0.05)
                {
                    return (false, "Geçersiz CSV dosyası (dosya içeriği metin tabanlı değil).");
                }
            }

            return (true, "Geçerli.");
        }

        public (bool isValid, string message) ValidateImage(IFormFile file)
        {
            if (file == null || file.Length == 0) return (false, "Dosya seçilmedi.");
            if (file.Length > MaxImageSize) return (false, "Profil fotoğrafı 5 MB'dan büyük olamaz.");

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!ImageSignatureRules.TryGetValue(extension, out var rule))
            {
                return (false, "Sadece .jpg, .jpeg ve .png formatları kabul edilir.");
            }

            if (!string.IsNullOrWhiteSpace(file.ContentType) && !AllowedImageMimeTypes.Contains(file.ContentType))
            {
                return (false, "Geçersiz içerik tipi. Sadece JPEG ve PNG yüklenebilir.");
            }

            using var stream = file.OpenReadStream();
            if (!HasValidHeader(stream, rule.HeaderSignatures))
            {
                return (false, $"Geçersiz fotoğraf dosyası (magic byte başlık hatası: {rule.DisplayName}).");
            }

            if (!HasValidFooter(stream, rule.FooterSignatures))
            {
                return (false, $"Geçersiz fotoğraf dosyası (magic byte kapanış hatası: {rule.DisplayName}).");
            }

            // Ensure file is not a tiny fake payload.
            if (stream.Length < 32)
            {
                return (false, "Geçersiz fotoğraf dosyası (içerik boyutu çok düşük).");
            }

            return (true, "Geçerli.");
        }

        private static bool HasValidHeader(Stream stream, IReadOnlyList<byte[]> signatures)
        {
            stream.Position = 0;
            var maxHeaderLength = signatures.Max(s => s.Length);
            var buffer = new byte[maxHeaderLength];
            var read = stream.Read(buffer, 0, buffer.Length);

            foreach (var signature in signatures)
            {
                if (read < signature.Length)
                {
                    continue;
                }

                if (buffer.Take(signature.Length).SequenceEqual(signature))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool HasValidFooter(Stream stream, IReadOnlyList<byte[]> signatures)
        {
            foreach (var signature in signatures)
            {
                if (stream.Length < signature.Length)
                {
                    continue;
                }

                stream.Position = stream.Length - signature.Length;
                var buffer = new byte[signature.Length];
                var read = stream.Read(buffer, 0, buffer.Length);
                if (read == signature.Length && buffer.SequenceEqual(signature))
                {
                    return true;
                }
            }

            return false;
        }

        private sealed record FileSignatureRule(string DisplayName, IReadOnlyList<byte[]> HeaderSignatures, IReadOnlyList<byte[]> FooterSignatures);
    }
}

