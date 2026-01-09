using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonelTakipSistemi.Data;
using PersonelTakipSistemi.Models;
using PersonelTakipSistemi.ViewModels;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;

namespace PersonelTakipSistemi.Controllers
{
    public class PersonelController : Controller
    {
        private readonly TegmPersonelTakipDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IMemoryCache _memoryCache;

        public PersonelController(TegmPersonelTakipDbContext context, IWebHostEnvironment hostEnvironment, IMemoryCache memoryCache)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _memoryCache = memoryCache;
        }

        [HttpGet]
        public async Task<IActionResult> Ekle()
        {
            var sw = Stopwatch.StartNew();
            var model = new PersonelEkleViewModel();
            await FillLookupLists(model);
            sw.Stop();
            Debug.WriteLine($"Personel/Ekle data preparation took: {sw.ElapsedMilliseconds}ms");
            ViewBag.LoadTime = sw.ElapsedMilliseconds;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Ekle(PersonelEkleViewModel model, IFormFile personelFoto)
        {
            if (ModelState.IsValid)
            {
                // 0. Çakışma Kontrolü (TC ve E-posta)
                var conflicts = new List<string>();
                var existingUsers = await _context.Personeller
                    .Where(p => p.TcKimlikNo == model.TcKimlikNo || p.Eposta == model.Eposta)
                    .Select(p => new { p.TcKimlikNo, p.Eposta })
                    .ToListAsync();

                if (existingUsers.Any())
                {
                    foreach (var user in existingUsers)
                    {
                        if (user.TcKimlikNo == model.TcKimlikNo && !conflicts.Contains("TC Kimlik No zaten kayıtlı: " + model.TcKimlikNo))
                        {
                            conflicts.Add("TC Kimlik No zaten kayıtlı: " + model.TcKimlikNo);
                        }
                        if (user.Eposta == model.Eposta && !conflicts.Contains("E-posta zaten kayıtlı: " + model.Eposta))
                        {
                            conflicts.Add("E-posta zaten kayıtlı: " + model.Eposta);
                        }
                    }

                    if (conflicts.Any())
                    {
                        TempData["ShowUserExistsModal"] = "1";
                        TempData["ModalTitle"] = "Böyle bir kullanıcı zaten var";
                        TempData["ModalItems"] = string.Join("|", conflicts);
                        TempData["OpenTab"] = "tab1";
                        
                        var tcConflict = conflicts.Any(c => c.Contains("TC Kimlik No"));
                        TempData["FocusId"] = tcConflict ? "personelTcKimlik" : "personelemail";
                        
                        await FillLookupLists(model);
                        return View(model);
                    }
                }

                // 1. Fotoğraf Yükleme
                string? fotoYolu = null;
                if (personelFoto != null && personelFoto.Length > 0)
                {
                    string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "uploads", "personeller");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(personelFoto.FileName);
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await personelFoto.CopyToAsync(fileStream);
                    }

                    fotoYolu = "/uploads/personeller/" + uniqueFileName;
                }

                // 2. Şifre Hashleme
                CreatePasswordHash(model.Sifre, out byte[] passwordHash, out byte[] passwordSalt);

                // 3. Entity Oluşturma
                var personel = new Personel
                {
                    Ad = model.Ad,
                    Soyad = model.Soyad,
                    TcKimlikNo = model.TcKimlikNo,
                    Telefon = model.Telefon ?? "",
                    Eposta = model.Eposta,
                    PersonelCinsiyet = model.PersonelCinsiyet == 1, // 0: Erkek (false), 1: Kadın (true)
                    GorevliIl = model.GorevliIl ?? "",
                    Brans = model.Brans ?? "",
                    KadroKurum = model.KadroKurum ?? "",
                    AktifMi = model.AktifMi,
                    FotografYolu = fotoYolu,
                    SifreHash = passwordHash,
                    SifreSalt = passwordSalt,
                    CreatedAt = DateTime.Now
                };

                _context.Personeller.Add(personel);

                try
                {
                    await _context.SaveChangesAsync();

                    if (model.SeciliYazilimIdleri != null)
                    {
                        foreach (var id in model.SeciliYazilimIdleri)
                        {
                            _context.PersonelYazilimlar.Add(new PersonelYazilim { PersonelId = personel.PersonelId, YazilimId = id });
                        }
                    }

                    if (model.SeciliUzmanlikIdleri != null)
                    {
                        foreach (var id in model.SeciliUzmanlikIdleri)
                        {
                            _context.PersonelUzmanliklar.Add(new PersonelUzmanlik { PersonelId = personel.PersonelId, UzmanlikId = id });
                        }
                    }

                    if (model.SeciliGorevTuruIdleri != null)
                    {
                        foreach (var id in model.SeciliGorevTuruIdleri)
                        {
                            _context.PersonelGorevTurleri.Add(new PersonelGorevTuru { PersonelId = personel.PersonelId, GorevTuruId = id });
                        }
                    }

                    if (model.SeciliIsNiteligiIdleri != null)
                    {
                        foreach (var id in model.SeciliIsNiteligiIdleri)
                        {
                            _context.PersonelIsNitelikleri.Add(new PersonelIsNiteligi { PersonelId = personel.PersonelId, IsNiteligiId = id });
                        }
                    }

                    await _context.SaveChangesAsync();

                    TempData["Success"] = "Personel başarıyla kaydedildi.";
                    return RedirectToAction("Detay", new { id = personel.PersonelId });
                }
                catch (DbUpdateException ex)
                {
                    var handled = false;
                    if (ex.InnerException != null)
                    {
                         var msg = ex.InnerException.Message;
                         var conflictItems = new List<string>();
                         
                         if (msg.Contains("UX_Personeller_TcKimlikNo")) conflictItems.Add($"TC Kimlik No zaten kayıtlı: {model.TcKimlikNo}");
                         if (msg.Contains("UX_Personeller_Eposta")) conflictItems.Add($"E-posta zaten kayıtlı: {model.Eposta}");

                         if(conflictItems.Any())
                         {
                            TempData["ShowUserExistsModal"] = "1";
                            TempData["ModalTitle"] = "Böyle bir kullanıcı zaten var";
                            TempData["ModalItems"] = string.Join("|", conflictItems); 
                            TempData["OpenTab"] = "tab1";
                            var tcConflict = conflictItems.Any(c => c.Contains("TC Kimlik No"));
                            TempData["FocusId"] = tcConflict ? "personelTcKimlik" : "personelemail";
                            handled = true;
                         }
                    }

                    if (!handled)
                    {
                         ModelState.AddModelError("", "Veritabanı güncelleme hatası: " + ex.Message);
                         TempData["Error"] = "Veritabanı hatası oluştu.";
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[DB Error] {ex.Message}");
                    TempData["Error"] = "Beklenmeyen bir hata oluştu.";
                    ModelState.AddModelError("", $"Hata: {ex.Message}");
                }
            }

            await FillLookupLists(model);
            return View(model);
        }

        [HttpGet("/Personel/Detay/{id:int}")]
        public async Task<IActionResult> Detay(int id)
        {
            var personel = await _context.Personeller
                .Include(p => p.PersonelYazilimlar).ThenInclude(py => py.Yazilim)
                .Include(p => p.PersonelUzmanliklar).ThenInclude(pu => pu.Uzmanlik)
                .Include(p => p.PersonelGorevTurleri).ThenInclude(pg => pg.GorevTuru)
                .Include(p => p.PersonelIsNitelikleri).ThenInclude(pi => pi.IsNiteligi)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.PersonelId == id);

            if (personel == null)
            {
                return NotFound();
            }

            var model = new PersonelDetayViewModel
            {
                PersonelId = personel.PersonelId,
                Ad = personel.Ad,
                Soyad = personel.Soyad,
                TcKimlikNo = personel.TcKimlikNo,
                Telefon = personel.Telefon,
                Eposta = personel.Eposta,
                Cinsiyet = personel.PersonelCinsiyet ? "Kadın" : "Erkek",
                GorevliIl = personel.GorevliIl,
                Brans = personel.Brans,
                KadroKurum = personel.KadroKurum,
                AktifMi = personel.AktifMi,
                FotografYolu = personel.FotografYolu,
                CreatedAt = personel.CreatedAt,
                Yazilimlar = personel.PersonelYazilimlar.Select(py => py.Yazilim.Ad).ToList(),
                Uzmanliklar = personel.PersonelUzmanliklar.Select(pu => pu.Uzmanlik.Ad).ToList(),
                GorevTurleri = personel.PersonelGorevTurleri.Select(pg => pg.GorevTuru.Ad).ToList(),
                IsNitelikleri = personel.PersonelIsNitelikleri.Select(pi => pi.IsNiteligi.Ad).ToList()
            };

            return View(model);
        }

        // Helper Method: Lookup Listelerini Doldurma
        private async Task FillLookupLists(PersonelEkleViewModel model)
        {
            try
            {
                var cacheDuration = TimeSpan.FromMinutes(10);

                var yazilimlar = await _memoryCache.GetOrCreateAsync("YazilimlarList", async entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow = cacheDuration;
                    return await _context.Yazilimlar
                        .AsNoTracking()
                        .Select(x => new LookupItemVm { Id = x.YazilimId, Ad = x.Ad })
                        .ToListAsync();
                });

                var uzmanliklar = await _memoryCache.GetOrCreateAsync("UzmanliklarList", async entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow = cacheDuration;
                    return await _context.Uzmanliklar
                        .AsNoTracking()
                        .Select(x => new LookupItemVm { Id = x.UzmanlikId, Ad = x.Ad })
                        .ToListAsync();
                });

                var gorevTurleri = await _memoryCache.GetOrCreateAsync("GorevTurleriList", async entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow = cacheDuration;
                    return await _context.GorevTurleri
                        .AsNoTracking()
                        .Select(x => new LookupItemVm { Id = x.GorevTuruId, Ad = x.Ad })
                        .ToListAsync();
                });

                var isNitelikleri = await _memoryCache.GetOrCreateAsync("IsNitelikleriList", async entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow = cacheDuration;
                    return await _context.IsNitelikleri
                        .AsNoTracking()
                        .Select(x => new LookupItemVm { Id = x.IsNiteligiId, Ad = x.Ad })
                        .ToListAsync();
                });

                model.Yazilimlar = yazilimlar ?? new List<LookupItemVm>();
                model.Uzmanliklar = uzmanliklar ?? new List<LookupItemVm>();
                model.GorevTurleri = gorevTurleri ?? new List<LookupItemVm>();
                model.IsNitelikleri = isNitelikleri ?? new List<LookupItemVm>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error fetching lookup data: " + ex.Message);
            }
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            // Salt üretimi
            passwordSalt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(passwordSalt);
            }

            // Hash üretimi (PBKDF2)
            passwordHash = KeyDerivation.Pbkdf2(
                password: password,
                salt: passwordSalt,
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 10000,
                numBytesRequested: 256 / 8);
        }

        private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            using (var hmac = new HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }
            return true;
        }
    }
}
