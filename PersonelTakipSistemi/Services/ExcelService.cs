using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.DataValidation;
using OfficeOpenXml.Style;
using PersonelTakipSistemi.Data;
using PersonelTakipSistemi.Models;

namespace PersonelTakipSistemi.Services
{
    public class ExcelService : IExcelService
    {
        private readonly TegmPersonelTakipDbContext _context;

        public ExcelService(TegmPersonelTakipDbContext context)
        {
            _context = context;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        public async Task<byte[]> GeneratePersonelTemplateAsync()
        {
            using (var package = new ExcelPackage())
            {
                var workSheet = package.Workbook.Worksheets.Add("Personel Listesi");
                var hiddenSheet = package.Workbook.Worksheets.Add("ReferenceData");
                hiddenSheet.Hidden = eWorkSheetHidden.Hidden;

                // 1. Fetch Reference Data
                var iller = await _context.Iller.OrderBy(x => x.Ad).ToListAsync();
                var branslar = await _context.Branslar.OrderBy(x => x.Ad).ToListAsync();
                var yazilimlar = await _context.Yazilimlar.OrderBy(x => x.Ad).ToListAsync();
                var uzmanliklar = await _context.Uzmanliklar.OrderBy(x => x.Ad).ToListAsync();
                var gorevTurleri = await _context.GorevTurleri.OrderBy(x => x.Ad).ToListAsync();
                var isNitelikleri = await _context.IsNitelikleri.OrderBy(x => x.Ad).ToListAsync();

                // 2. Populate Reference Data (Hidden Sheet)
                for (int i = 0; i < iller.Count; i++) hiddenSheet.Cells[i + 1, 1].Value = iller[i].Ad;
                for (int i = 0; i < branslar.Count; i++) hiddenSheet.Cells[i + 1, 2].Value = branslar[i].Ad;
                for (int i = 0; i < yazilimlar.Count; i++) hiddenSheet.Cells[i + 1, 3].Value = yazilimlar[i].Ad;
                for (int i = 0; i < uzmanliklar.Count; i++) hiddenSheet.Cells[i + 1, 4].Value = uzmanliklar[i].Ad;
                for (int i = 0; i < gorevTurleri.Count; i++) hiddenSheet.Cells[i + 1, 5].Value = gorevTurleri[i].Ad;
                for (int i = 0; i < isNitelikleri.Count; i++) hiddenSheet.Cells[i + 1, 6].Value = isNitelikleri[i].Ad;

                // Define Named Ranges for Dropdowns
                if (iller.Any()) package.Workbook.Names.Add("IllerList", hiddenSheet.Cells[1, 1, iller.Count, 1]);
                if (branslar.Any()) package.Workbook.Names.Add("BranslarList", hiddenSheet.Cells[1, 2, branslar.Count, 2]);
                if (yazilimlar.Any()) package.Workbook.Names.Add("YazilimlarList", hiddenSheet.Cells[1, 3, yazilimlar.Count, 3]);
                if (uzmanliklar.Any()) package.Workbook.Names.Add("UzmanliklarList", hiddenSheet.Cells[1, 4, uzmanliklar.Count, 4]);
                if (gorevTurleri.Any()) package.Workbook.Names.Add("GorevTurleriList", hiddenSheet.Cells[1, 5, gorevTurleri.Count, 5]);
                if (isNitelikleri.Any()) package.Workbook.Names.Add("IsNitelikleriList", hiddenSheet.Cells[1, 6, isNitelikleri.Count, 6]);

                // 3. Set Headers
                string[] headers = {
                    "Ad *", "Soyad *", "TC Kimlik No *", "E-posta *", "Telefon *",
                    "Doğum Tarihi (GG.AA.YYYY) *", "Cinsiyet (E/K) *", "İl *", 
                    "Kadro İl", "Kadro İlçe", // New Columns
                    "Branş *", "Kadro Kurum *", "Yazılımlar (Seç/Yaz)", "Uzmanlıklar (Seç/Yaz)",
                    "Görev Türleri (Seç/Yaz)", "İş Nitelikleri (Seç/Yaz)"
                };

                for (int i = 0; i < headers.Length; i++)
                {
                    workSheet.Cells[1, i + 1].Value = headers[i];
                    workSheet.Cells[1, i + 1].Style.Font.Bold = true;
                    workSheet.Cells[1, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    workSheet.Cells[1, i + 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                    workSheet.Column(i + 1).Width = 20;
                }

                // 4. Add Data Validations
                int dataRows = 1000; // Allow validation for up to 1000 rows

                // Cinsiyet (E/K)
                var genderValidation = workSheet.DataValidations.AddListValidation(workSheet.Cells[2, 7, dataRows, 7].Address);
                genderValidation.Formula.Values.Add("E");
                genderValidation.Formula.Values.Add("K");
                genderValidation.ShowErrorMessage = true;

                // İl (Dropdown from DB) - Col 8 (H)
                if (iller.Any())
                {
                    var ilValidation = workSheet.DataValidations.AddListValidation(workSheet.Cells[2, 8, dataRows, 8].Address);
                    ilValidation.Formula.ExcelFormula = "IllerList";
                    ilValidation.ShowErrorMessage = true;
                    
                    // Kadro İl (Dropdown from DB) - Col 9 (I)
                    var kadroIlValidation = workSheet.DataValidations.AddListValidation(workSheet.Cells[2, 9, dataRows, 9].Address);
                    kadroIlValidation.Formula.ExcelFormula = "IllerList";
                    kadroIlValidation.ShowErrorMessage = true;
                }
                
                // Kadro İlçe - Col 10 (J) - No generic list validation possible easily without cascading
                
                // Branş (Dropdown from DB) - Col 11 (K)
                if (branslar.Any())
                {
                    var bransValidation = workSheet.DataValidations.AddListValidation(workSheet.Cells[2, 11, dataRows, 11].Address);
                    bransValidation.Formula.ExcelFormula = "BranslarList";
                    bransValidation.ShowErrorMessage = true;
                }

                // Yazılımlar (Multi-Select friendly - No Error Message) - Col 13 (M)
                if (yazilimlar.Any())
                {
                    var val = workSheet.DataValidations.AddListValidation(workSheet.Cells[2, 13, dataRows, 13].Address);
                    val.Formula.ExcelFormula = "YazilimlarList";
                    val.ShowErrorMessage = false; // Allow typing multiple comma separated
                    val.ErrorTitle = "Bilgi";
                    val.Error = "Listeden seçebilir veya virgülle ayırarak çoklu değer girebilirsiniz.";
                }

                // Uzmanliklar - Col 14 (N)
                if (uzmanliklar.Any())
                {
                    var val = workSheet.DataValidations.AddListValidation(workSheet.Cells[2, 14, dataRows, 14].Address);
                    val.Formula.ExcelFormula = "UzmanliklarList";
                    val.ShowErrorMessage = false;
                }

                // Görev Türleri - Col 15 (O)
                if (gorevTurleri.Any())
                {
                    var val = workSheet.DataValidations.AddListValidation(workSheet.Cells[2, 15, dataRows, 15].Address);
                    val.Formula.ExcelFormula = "GorevTurleriList";
                    val.ShowErrorMessage = false;
                }

                // İş Nitelikleri - Col 16 (P)
                if (isNitelikleri.Any())
                {
                    var val = workSheet.DataValidations.AddListValidation(workSheet.Cells[2, 16, dataRows, 16].Address);
                    val.Formula.ExcelFormula = "IsNitelikleriList";
                    val.ShowErrorMessage = false;
                }


                var comment = workSheet.Cells[1, 1].AddComment("Zorunlu alanlar (*) ile işaretlenmiştir. Tarihleri GG.AA.YYYY formatında giriniz.\n\nÇoklu seçim alanlarında (Yazılım, Uzmanlık vb.) listeden bir değer seçebilir veya birden fazla değeri virgül ile ayırarak elle yazabilirsiniz.", "Sistem");
                comment.AutoFit = true;

                return await package.GetAsByteArrayAsync();
            }
        }

        public async Task<(List<Personel> personeller, List<string> errors)> ImportPersonelListAsync(IFormFile file)
        {
            var errors = new List<string>();
            var personeller = new List<Personel>();
            var softwareCache = await _context.Yazilimlar.ToDictionaryAsync(x => x.Ad.ToLower(), x => x);
            var expertiseCache = await _context.Uzmanliklar.ToDictionaryAsync(x => x.Ad.ToLower(), x => x);
            var jobTypeCache = await _context.GorevTurleri.ToDictionaryAsync(x => x.Ad.ToLower(), x => x);
            var jobQualityCache = await _context.IsNitelikleri.ToDictionaryAsync(x => x.Ad.ToLower(), x => x);
            
            // Pre-fetch references to avoid N+1 queries
            var ilList = await _context.Iller.ToListAsync();
            var ilceList = await _context.Ilceler.ToListAsync();
            var bransList = await _context.Branslar.ToListAsync();
            var existingTcs = await _context.Personeller.Select(x => x.TcKimlikNo).ToListAsync();
            var existingEmails = await _context.Personeller.Select(x => x.Eposta).ToListAsync();

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    var workSheet = package.Workbook.Worksheets.FirstOrDefault();
                    if (workSheet == null)
                    {
                        errors.Add("Excel dosyasında sayfa bulunamadı.");
                        return (personeller, errors);
                    }

                    int rowCount = workSheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        try
                        {
                            // Basic Fields
                            var ad = workSheet.Cells[row, 1].Text?.Trim();
                            var soyad = workSheet.Cells[row, 2].Text?.Trim();
                            var tc = workSheet.Cells[row, 3].Text?.Trim();
                            var email = workSheet.Cells[row, 4].Text?.Trim();
                            var phone = workSheet.Cells[row, 5].Text?.Trim();
                            var birthDateStr = workSheet.Cells[row, 6].Text?.Trim();
                            var genderStr = workSheet.Cells[row, 7].Text?.Trim();
                            var ilName = workSheet.Cells[row, 8].Text?.Trim();
                            var kadroIlName = workSheet.Cells[row, 9].Text?.Trim();
                            var kadroIlceName = workSheet.Cells[row, 10].Text?.Trim();
                            var bransName = workSheet.Cells[row, 11].Text?.Trim();
                            var kadroKurum = workSheet.Cells[row, 12].Text?.Trim();

                            // Skip empty rows
                            if (string.IsNullOrEmpty(tc) && string.IsNullOrEmpty(ad)) continue;

                            // Validation
                            if (string.IsNullOrEmpty(ad) || string.IsNullOrEmpty(soyad) || string.IsNullOrEmpty(tc))
                            {
                                errors.Add($"Satır {row}: Ad, Soyad ve TC zorunludur.");
                                continue;
                            }

                            if (existingTcs.Contains(tc))
                            {
                                errors.Add($"Satır {row}: TC Kimlik No ({tc}) zaten kayıtlı.");
                                continue;
                            }

                            if (!string.IsNullOrEmpty(email) && existingEmails.Contains(email))
                            {
                                errors.Add($"Satır {row}: E-posta ({email}) zaten kayıtlı.");
                                continue;
                            }

                            // Foreign Keys lookup
                            var il = ilList.FirstOrDefault(x => x.Ad.Equals(ilName, StringComparison.OrdinalIgnoreCase));
                            if (il == null)
                            {
                                errors.Add($"Satır {row}: '{ilName}' şehri sistemde bulunamadı.");
                                continue;
                            }

                            var brans = bransList.FirstOrDefault(x => x.Ad.Equals(bransName, StringComparison.OrdinalIgnoreCase));
                            if (brans == null)
                            {
                                errors.Add($"Satır {row}: '{bransName}' branşı sistemde bulunamadı.");
                                continue;
                            }

                            DateTime birthDate;
                            if (!DateTime.TryParse(birthDateStr, out birthDate)) birthDate = new DateTime(1990, 1, 1);

                            int? kadroIlId = null;
                            if (!string.IsNullOrEmpty(kadroIlName))
                            {
                                var kil = ilList.FirstOrDefault(x => x.Ad.Equals(kadroIlName, StringComparison.OrdinalIgnoreCase));
                                if (kil != null) kadroIlId = kil.IlId;
                            }

                            int? kadroIlceId = null;
                            if (kadroIlId.HasValue && !string.IsNullOrEmpty(kadroIlceName))
                            {
                                var kilce = ilceList.FirstOrDefault(x => x.IlId == kadroIlId.Value && x.Ad.Equals(kadroIlceName, StringComparison.OrdinalIgnoreCase));
                                if (kilce != null) kadroIlceId = kilce.IlceId;
                            }

                            // Create Personel
                            var p = new Personel
                            {
                                Ad = ad,
                                Soyad = soyad,
                                TcKimlikNo = tc,
                                Eposta = email,
                                Telefon = phone,
                                DogumTarihi = birthDate,
                                PersonelCinsiyet = genderStr == "K",
                                GorevliIlId = il.IlId,
                                KadroIlId = kadroIlId,
                                KadroIlceId = kadroIlceId,
                                BransId = brans.BransId,
                                KadroKurum = kadroKurum ?? "",
                                AktifMi = true,
                                CreatedAt = DateTime.Now
                            };

                            // Default Password logic (TC last 6 digits or default)
                            var rawPass = tc.Length >= 6 ? tc.Substring(tc.Length - 6) : "123456";
                            CreatePasswordHash(rawPass, out byte[] passwordHash, out byte[] passwordSalt);
                            p.SifreHash = passwordHash;
                            p.SifreSalt = passwordSalt;

                            // Process Many-to-Many Collections (Comma separated)
                            // Yazilimlar
                            var yazilimStr = workSheet.Cells[row, 13].Text;
                            if (!string.IsNullOrEmpty(yazilimStr))
                            {
                                var items = yazilimStr.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim());
                                foreach (var item in items)
                                {
                                    if (softwareCache.TryGetValue(item.ToLower(), out var val))
                                    {
                                        p.PersonelYazilimlar.Add(new PersonelYazilim { YazilimId = val.YazilimId });
                                    }
                                }
                            }

                            // Uzmanliklar
                            var uzmanlikStr = workSheet.Cells[row, 14].Text;
                            if (!string.IsNullOrEmpty(uzmanlikStr))
                            {
                                var items = uzmanlikStr.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim());
                                foreach (var item in items)
                                {
                                    if (expertiseCache.TryGetValue(item.ToLower(), out var val))
                                    {
                                        p.PersonelUzmanliklar.Add(new PersonelUzmanlik { UzmanlikId = val.UzmanlikId });
                                    }
                                }
                            }

                            // Gorev Turleri
                            var gorevTuruStr = workSheet.Cells[row, 15].Text;
                            if (!string.IsNullOrEmpty(gorevTuruStr))
                            {
                                var items = gorevTuruStr.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim());
                                foreach (var item in items)
                                {
                                    if (jobTypeCache.TryGetValue(item.ToLower(), out var val))
                                    {
                                        p.PersonelGorevTurleri.Add(new PersonelGorevTuru { GorevTuruId = val.GorevTuruId });
                                    }
                                }
                            }

                             // Is Nitelikleri
                            var isNiteligiStr = workSheet.Cells[row, 16].Text;
                            if (!string.IsNullOrEmpty(isNiteligiStr))
                            {
                                var items = isNiteligiStr.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim());
                                foreach (var item in items)
                                {
                                    if (jobQualityCache.TryGetValue(item.ToLower(), out var val))
                                    {
                                        p.PersonelIsNitelikleri.Add(new PersonelIsNiteligi { IsNiteligiId = val.IsNiteligiId });
                                    }
                                }
                            }

                            personeller.Add(p);
                        }
                        catch (Exception ex)
                        {
                            errors.Add($"Satır {row} hatası: {ex.Message}");
                        }
                    }

                    if (errors.Count == 0 && personeller.Count > 0)
                    {
                        await _context.Personeller.AddRangeAsync(personeller);
                        await _context.SaveChangesAsync();
                    }
                }
            }

            return (personeller, errors);
        }
        
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
