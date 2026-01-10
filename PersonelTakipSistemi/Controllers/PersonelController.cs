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
        public async Task<IActionResult> Index(PersonelIndexFilterViewModel filter)
        {
            var sw = Stopwatch.StartNew();
            var query = _context.Personeller.AsNoTracking().AsQueryable();

            // 1. Filtreleme
            if (!string.IsNullOrEmpty(filter.SearchName))
            {
                var term = filter.SearchName.ToLower();
                query = query.Where(p => p.Ad.ToLower().Contains(term) || p.Soyad.ToLower().Contains(term));
            }

            if (!string.IsNullOrEmpty(filter.TcKimlikNo))
            {
                query = query.Where(p => p.TcKimlikNo.StartsWith(filter.TcKimlikNo));
            }

            if (!string.IsNullOrEmpty(filter.Brans))
            {
                query = query.Where(p => p.Brans == filter.Brans);
            }

            if (!string.IsNullOrEmpty(filter.GorevliIl))
            {
                query = query.Where(p => p.GorevliIl == filter.GorevliIl);
            }

            if (filter.SeciliYazilimIdleri != null && filter.SeciliYazilimIdleri.Any())
            {
                query = query.Where(p => p.PersonelYazilimlar.Any(py => filter.SeciliYazilimIdleri.Contains(py.YazilimId)));
            }

            if (filter.SeciliUzmanlikIdleri != null && filter.SeciliUzmanlikIdleri.Any())
            {
                query = query.Where(p => p.PersonelUzmanliklar.Any(pu => filter.SeciliUzmanlikIdleri.Contains(pu.UzmanlikId)));
            }

            if (filter.SeciliGorevTuruIdleri != null && filter.SeciliGorevTuruIdleri.Any())
            {
                query = query.Where(p => p.PersonelGorevTurleri.Any(pg => filter.SeciliGorevTuruIdleri.Contains(pg.GorevTuruId)));
            }

            if (filter.SeciliIsNiteligiIdleri != null && filter.SeciliIsNiteligiIdleri.Any())
            {
                query = query.Where(p => p.PersonelIsNitelikleri.Any(pi => filter.SeciliIsNiteligiIdleri.Contains(pi.IsNiteligiId)));
            }

            // 2. Sayfalama Hazırlığı
            var totalItems = await query.CountAsync();
            var results = await query
                .OrderByDescending(p => p.CreatedAt)
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .Select(p => new PersonelIndexRowViewModel
                {
                    PersonelId = p.PersonelId,
                    AdSoyad = p.Ad + " " + p.Soyad,
                    Brans = p.Brans,
                    GorevliIl = p.GorevliIl,
                    Eposta = p.Eposta,
                    AktifMi = p.AktifMi,
                    FotografYolu = p.FotografYolu
                })
                .ToListAsync();

            // 3. ViewModel Hazırlığı
            var model = new PersonelIndexViewModel
            {
                Filter = filter,
                Results = results,
                Pagination = new PaginationInfoViewModel
                {
                    CurrentPage = filter.Page,
                    ItemsPerPage = filter.PageSize,
                    TotalItems = totalItems
                }
            };

            // 4. Lookup Doldurma
            await FillIndexLookups(model.Lookups);

            sw.Stop();
            ViewBag.LoadTime = sw.ElapsedMilliseconds;
            return View("PersonelIndex", model); // Explicitly specifying view name
        }

        [HttpGet]
        public IActionResult Yeni()
        {
            // "Yeni Personel Ekle" tıklandığında kesinlikle temiz state ile gitmek için redirect kullanıyoruz.
            return RedirectToAction("Ekle", new { id = (int?)null });
        }

        [HttpGet]
        public async Task<IActionResult> Ekle(int? id)
        {
            // 1. Yeni Kayıt Modu (Insert)
            if (id == null || id == 0)
            {
                // "Yapışma" sorununu önlemek için ModelState ve ViewModel'i temizle
                ModelState.Clear();
                var cleanModel = new PersonelEkleViewModel
                {
                    IsEditMode = false,
                    PersonelId = 0,
                    SeciliYazilimIdleri = new List<int>(),
                    SeciliUzmanlikIdleri = new List<int>(),
                    SeciliGorevTuruIdleri = new List<int>(),
                    SeciliIsNiteligiIdleri = new List<int>()
                };
                
                await FillLookupLists(cleanModel);
                return View(cleanModel);
            }

            // 2. Düzenleme Modu (Update)
            var personel = await _context.Personeller
                .Include(p => p.PersonelYazilimlar)
                .Include(p => p.PersonelUzmanliklar)
                .Include(p => p.PersonelGorevTurleri)
                .Include(p => p.PersonelIsNitelikleri)
                .AsNoTracking() // Önemli: Tracking kapatılabilir, sadece okuma yapıyoruz
                .FirstOrDefaultAsync(p => p.PersonelId == id.Value);

            if (personel == null)
            {
                return NotFound();
            }

            var model = new PersonelEkleViewModel
            {
                IsEditMode = true,
                PersonelId = personel.PersonelId,
                Ad = personel.Ad,
                Soyad = personel.Soyad,
                TcKimlikNo = personel.TcKimlikNo,
                Telefon = personel.Telefon,
                Eposta = personel.Eposta,
                PersonelCinsiyet = personel.PersonelCinsiyet ? 1 : 0,
                GorevliIl = personel.GorevliIl,
                Brans = personel.Brans,
                KadroKurum = personel.KadroKurum,
                AktifMi = personel.AktifMi,
                FotografBase64 = personel.FotografYolu, // Mevcut fotoğraf yolu
                
                // İlişkili tabloları seçili olarak işaretle
                SeciliYazilimIdleri = personel.PersonelYazilimlar.Select(x => x.YazilimId).ToList(),
                SeciliUzmanlikIdleri = personel.PersonelUzmanliklar.Select(x => x.UzmanlikId).ToList(),
                SeciliGorevTuruIdleri = personel.PersonelGorevTurleri.Select(x => x.GorevTuruId).ToList(),
                SeciliIsNiteligiIdleri = personel.PersonelIsNitelikleri.Select(x => x.IsNiteligiId).ToList()
            };

            await FillLookupLists(model);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Ekle(PersonelEkleViewModel model, IFormFile personelFoto)
        {
            if (ModelState.IsValid)
            {
                // 0. Çakışma Kontrolü (TC ve E-posta) - Kendi ID'si hariç
                var conflicts = new List<string>();
                var existingUsers = await _context.Personeller
                    .Where(p => (p.TcKimlikNo == model.TcKimlikNo || p.Eposta == model.Eposta) && p.PersonelId != model.PersonelId)
                    .Select(p => new { p.TcKimlikNo, p.Eposta })
                    .ToListAsync();

                if (existingUsers.Any())
                {
                    foreach (var user in existingUsers)
                    {
                        if (user.TcKimlikNo == model.TcKimlikNo && !conflicts.Contains("TC Kimlik No zaten kayıtlı: " + model.TcKimlikNo))
                            conflicts.Add("TC Kimlik No zaten kayıtlı: " + model.TcKimlikNo);
                        
                        if (user.Eposta == model.Eposta && !conflicts.Contains("E-posta zaten kayıtlı: " + model.Eposta))
                            conflicts.Add("E-posta zaten kayıtlı: " + model.Eposta);
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

                // 1. Fotoğraf İşlemi (Ortak)
                string? fotoYolu = null;
                if (personelFoto != null && personelFoto.Length > 0)
                {
                    string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "uploads", "personeller");
                    if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

                    string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(personelFoto.FileName);
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await personelFoto.CopyToAsync(fileStream);
                    }
                    fotoYolu = "/uploads/personeller/" + uniqueFileName;
                }
                
                try 
                {
                    // 2. Logic Separation: Update vs Insert
                    if (model.IsEditMode && model.PersonelId > 0)
                    {
                        // --- UPDATE ---
                        var personel = await _context.Personeller
                            .Include(p => p.PersonelYazilimlar)
                            .Include(p => p.PersonelUzmanliklar)
                            .Include(p => p.PersonelGorevTurleri)
                            .Include(p => p.PersonelIsNitelikleri)
                            .FirstOrDefaultAsync(p => p.PersonelId == model.PersonelId);

                        if (personel == null) return NotFound();

                        // Alan Güncellemeleri
                        personel.Ad = model.Ad;
                        personel.Soyad = model.Soyad;
                        personel.TcKimlikNo = model.TcKimlikNo;
                        personel.Telefon = model.Telefon ?? "";
                        personel.Eposta = model.Eposta;
                        personel.PersonelCinsiyet = model.PersonelCinsiyet == 1;
                        personel.GorevliIl = model.GorevliIl ?? "";
                        personel.Brans = model.Brans ?? "";
                        personel.KadroKurum = model.KadroKurum ?? "";
                        personel.AktifMi = model.AktifMi;
                        
                        if (fotoYolu != null) personel.FotografYolu = fotoYolu; // Sadece yeni foto varsa güncelle
                        
                        personel.UpdatedAt = DateTime.Now;

                        // Şifre Logic: Sadece doluysa güncelle, yoksa eski şifre kalsın
                        // Not: ViewModel'de [Required] olduğu için şu an şifre girmek zorunlu.
                        // Eğer Edit modunda şifre opsiyonel olacaksa ViewModel'de değişiklik gerekir veya ModelState hatasını manuel kaldırmak gerekir.
                        // Mevcut yapıda şifre her zaman giriliyor kabul ediyoruz.
                        CreatePasswordHash(model.Sifre, out byte[] passwordHash, out byte[] passwordSalt);
                        personel.SifreHash = passwordHash;
                        personel.SifreSalt = passwordSalt;

                        // İlişkileri Temizle
                        _context.PersonelYazilimlar.RemoveRange(personel.PersonelYazilimlar);
                        _context.PersonelUzmanliklar.RemoveRange(personel.PersonelUzmanliklar);
                        _context.PersonelGorevTurleri.RemoveRange(personel.PersonelGorevTurleri);
                        _context.PersonelIsNitelikleri.RemoveRange(personel.PersonelIsNitelikleri);

                        // Main Update
                        _context.Personeller.Update(personel);
                        await _context.SaveChangesAsync(); // IDs cleared, main entity updated

                        // İlişkileri Yeniden Ekle
                        if (model.SeciliYazilimIdleri != null)
                            foreach (var id in model.SeciliYazilimIdleri) _context.PersonelYazilimlar.Add(new PersonelYazilim { PersonelId = personel.PersonelId, YazilimId = id });
                        
                        if (model.SeciliUzmanlikIdleri != null)
                            foreach (var id in model.SeciliUzmanlikIdleri) _context.PersonelUzmanliklar.Add(new PersonelUzmanlik { PersonelId = personel.PersonelId, UzmanlikId = id });

                        if (model.SeciliGorevTuruIdleri != null)
                            foreach (var id in model.SeciliGorevTuruIdleri) _context.PersonelGorevTurleri.Add(new PersonelGorevTuru { PersonelId = personel.PersonelId, GorevTuruId = id });

                        if (model.SeciliIsNiteligiIdleri != null)
                            foreach (var id in model.SeciliIsNiteligiIdleri) _context.PersonelIsNitelikleri.Add(new PersonelIsNiteligi { PersonelId = personel.PersonelId, IsNiteligiId = id });

                        await _context.SaveChangesAsync();
                        TempData["Success"] = "Personel bilgileri güncellendi.";
                        return RedirectToAction("Detay", new { id = personel.PersonelId });
                    }
                    else
                    {
                        // --- INSERT ---
                        CreatePasswordHash(model.Sifre, out byte[] passwordHash, out byte[] passwordSalt);

                        var personel = new Personel
                        {
                            Ad = model.Ad,
                            Soyad = model.Soyad,
                            TcKimlikNo = model.TcKimlikNo,
                            Telefon = model.Telefon ?? "",
                            Eposta = model.Eposta,
                            PersonelCinsiyet = model.PersonelCinsiyet == 1,
                            GorevliIl = model.GorevliIl ?? "",
                            Brans = model.Brans ?? "",
                            KadroKurum = model.KadroKurum ?? "",
                            AktifMi = model.AktifMi,
                            FotografYolu = fotoYolu,
                            SifreHash = passwordHash,
                            SifreSalt = passwordSalt,
                            CreatedAt = DateTime.Now,
                            UpdatedAt = null
                        };

                        _context.Personeller.Add(personel);
                        await _context.SaveChangesAsync(); // Get ID

                        // İlişkileri Ekle
                        if (model.SeciliYazilimIdleri != null)
                            foreach (var id in model.SeciliYazilimIdleri) _context.PersonelYazilimlar.Add(new PersonelYazilim { PersonelId = personel.PersonelId, YazilimId = id });
                        
                        if (model.SeciliUzmanlikIdleri != null)
                            foreach (var id in model.SeciliUzmanlikIdleri) _context.PersonelUzmanliklar.Add(new PersonelUzmanlik { PersonelId = personel.PersonelId, UzmanlikId = id });

                        if (model.SeciliGorevTuruIdleri != null)
                            foreach (var id in model.SeciliGorevTuruIdleri) _context.PersonelGorevTurleri.Add(new PersonelGorevTuru { PersonelId = personel.PersonelId, GorevTuruId = id });

                        if (model.SeciliIsNiteligiIdleri != null)
                            foreach (var id in model.SeciliIsNiteligiIdleri) _context.PersonelIsNitelikleri.Add(new PersonelIsNiteligi { PersonelId = personel.PersonelId, IsNiteligiId = id });

                        await _context.SaveChangesAsync();
                        TempData["Success"] = "Yeni personel başarıyla kaydedildi.";
                        return RedirectToAction("Detay", new { id = personel.PersonelId });
                    }
                }
                catch (DbUpdateException ex)
                {
                    // Hata yönetimi
                    var msg = ex.InnerException?.Message ?? ex.Message;
                    if (msg.Contains("UX_Personeller_TcKimlikNo")) ModelState.AddModelError("", "TC Kimlik No kullanımda.");
                    else ModelState.AddModelError("", "Veritabanı hatası: " + msg);
                }
                catch (Exception ex)
                {
                     ModelState.AddModelError("", $"Hata: {ex.Message}");
                }
            }

            await FillLookupLists(model);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Sil(int id)
        {
            var personel = await _context.Personeller.FindAsync(id);
            if (personel != null)
            {
                // Relations are cascade delete due to DbContext configuration
                _context.Personeller.Remove(personel);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Personel silindi.";
            }
            return RedirectToAction("Index");
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

        // Helper Method: Index Lookup Listelerini Doldurma
        private async Task FillIndexLookups(LookupListsViewModel model)
        {
             var cacheDuration = TimeSpan.FromMinutes(10);
             
             model.Yazilimlar = await _memoryCache.GetOrCreateAsync("YazilimlarList", async entry =>
             {
                 entry.AbsoluteExpirationRelativeToNow = cacheDuration;
                 return await _context.Yazilimlar.AsNoTracking().Select(x => new LookupItemVm { Id = x.YazilimId, Ad = x.Ad }).ToListAsync();
             }) ?? new List<LookupItemVm>();

             model.Uzmanliklar = await _memoryCache.GetOrCreateAsync("UzmanliklarList", async entry =>
             {
                 entry.AbsoluteExpirationRelativeToNow = cacheDuration;
                 return await _context.Uzmanliklar.AsNoTracking().Select(x => new LookupItemVm { Id = x.UzmanlikId, Ad = x.Ad }).ToListAsync();
             }) ?? new List<LookupItemVm>();

             model.GorevTurleri = await _memoryCache.GetOrCreateAsync("GorevTurleriList", async entry =>
             {
                 entry.AbsoluteExpirationRelativeToNow = cacheDuration;
                 return await _context.GorevTurleri.AsNoTracking().Select(x => new LookupItemVm { Id = x.GorevTuruId, Ad = x.Ad }).ToListAsync();
             }) ?? new List<LookupItemVm>();

             model.IsNitelikleri = await _memoryCache.GetOrCreateAsync("IsNitelikleriList", async entry =>
             {
                 entry.AbsoluteExpirationRelativeToNow = cacheDuration;
                 return await _context.IsNitelikleri.AsNoTracking().Select(x => new LookupItemVm { Id = x.IsNiteligiId, Ad = x.Ad }).ToListAsync();
             }) ?? new List<LookupItemVm>();

            model.Branslar = await _context.Personeller.AsNoTracking().Select(p => p.Brans).Distinct().Where(x => !string.IsNullOrEmpty(x)).ToListAsync();
            model.Iller = await _context.Personeller.AsNoTracking().Select(p => p.GorevliIl).Distinct().Where(x => !string.IsNullOrEmpty(x)).ToListAsync();
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
