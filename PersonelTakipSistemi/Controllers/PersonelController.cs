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
    [Microsoft.AspNetCore.Authorization.Authorize]

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
        public IActionResult BenimDetay()
        {
            var personelIdClaim = User.Claims.FirstOrDefault(c => c.Type == "PersonelId");
            if (personelIdClaim != null && int.TryParse(personelIdClaim.Value, out int personelId))
            {
                return RedirectToAction("Detay", new { id = personelId });
            }
            return RedirectToAction("Index"); // Should not happen if authorized
        }

        [HttpGet]
        public async Task<IActionResult> Index(PersonelIndexFilterViewModel filter)
        {
            var sw = Stopwatch.StartNew();
            var query = _context.Personeller
                .Include(p => p.GorevliIl)
                .Include(p => p.Brans)
                .Include(p => p.PersonelYazilimlar).ThenInclude(py => py.Yazilim)
                .Include(p => p.PersonelUzmanliklar).ThenInclude(pu => pu.Uzmanlik)
                .Include(p => p.PersonelGorevTurleri).ThenInclude(pg => pg.GorevTuru)
                .Include(p => p.PersonelIsNitelikleri).ThenInclude(pi => pi.IsNiteligi)
                .AsNoTracking()
                .AsQueryable();

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

            // Note: Filters updated to use IDs
            if (filter.BransId.HasValue)
            {
                query = query.Where(p => p.BransId == filter.BransId.Value);
            }

            if (filter.GorevliIlId.HasValue)
            {
                query = query.Where(p => p.GorevliIlId == filter.GorevliIlId.Value);
            }

            if (filter.DogumBaslangic.HasValue)
            {
                query = query.Where(p => p.DogumTarihi >= filter.DogumBaslangic.Value);
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
                    Brans = p.Brans.Ad,
                    GorevliIl = p.GorevliIl.Ad,
                    Eposta = p.Eposta,
                    AktifMi = p.AktifMi,
                    FotografYolu = p.FotografYolu,
                    Yazilimlar = p.PersonelYazilimlar.Select(py => py.Yazilim.Ad).ToList(),
                    Uzmanliklar = p.PersonelUzmanliklar.Select(pu => pu.Uzmanlik.Ad).ToList(),
                    GorevTurleri = p.PersonelGorevTurleri.Select(pg => pg.GorevTuru.Ad).ToList(),
                    IsNitelikleri = p.PersonelIsNitelikleri.Select(pi => pi.IsNiteligi.Ad).ToList()
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
                ModelState.Clear();
                var cleanModel = new PersonelEkleViewModel
                {
                    IsEditMode = false,
                    PersonelId = 0,
                    DogumTarihi = new DateTime(1990, 1, 1), // Default date
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
                .AsNoTracking() 
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
                DogumTarihi = personel.DogumTarihi,
                GorevliIlId = personel.GorevliIlId,
                BransId = personel.BransId,
                KadroKurum = personel.KadroKurum,
                AktifMi = personel.AktifMi,
                FotografBase64 = personel.FotografYolu,
                
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
        public async Task<IActionResult> Ekle(PersonelEkleViewModel model, IFormFile? personelFoto)
        {
            if (ModelState.IsValid)
            {
                // TELEFON NORMALİZASYONU
                if (!string.IsNullOrEmpty(model.Telefon))
                {
                    // Sadece rakamları al
                    string cleanPhone = new string(model.Telefon.Where(char.IsDigit).ToArray());
                    
                    // Başta 0 varsa kaldır
                    if (cleanPhone.StartsWith("0"))
                    {
                        cleanPhone = cleanPhone.Substring(1);
                    }

                    // 10 hane kontrolü
                    if (cleanPhone.Length != 10)
                    {
                        ModelState.AddModelError("Telefon", "Telefon numarası 10 haneli olmalıdır (Örn: 5551234567).");
                        // Hata durumunda dropdownları tekrar doldurup view'a dön
                        await FillLookupLists(model);
                        return View(model);
                    }

                    // Modele geri ata (DB'ye bu formatta gidecek)
                    model.Telefon = cleanPhone;
                }

                // --- FAZ 2A: Zorunlu Alan Kontrolleri (Multi-Select) ---
                if (model.SeciliYazilimIdleri == null || !model.SeciliYazilimIdleri.Any())
                {
                    ModelState.AddModelError("SeciliYazilimIdleri", "En az bir yazılım seçilmelidir.");
                }
                if (model.SeciliUzmanlikIdleri == null || !model.SeciliUzmanlikIdleri.Any())
                {
                     ModelState.AddModelError("SeciliUzmanlikIdleri", "En az bir uzmanlık seçilmelidir.");
                }
                if (model.SeciliGorevTuruIdleri == null || !model.SeciliGorevTuruIdleri.Any())
                {
                     ModelState.AddModelError("SeciliGorevTuruIdleri", "En az bir görev türü seçilmelidir.");
                }
                if (model.SeciliIsNiteligiIdleri == null || !model.SeciliIsNiteligiIdleri.Any())
                {
                     ModelState.AddModelError("SeciliIsNiteligiIdleri", "En az bir iş niteliği seçilmelidir.");
                }
                
                // Eğer manuel eklediğimiz hatalar varsa IsValid false dönecek (state check needed potentially, or re-check)
                if(!ModelState.IsValid)
                {
                     var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                     TempData["Error"] = "Lütfen tüm zorunlu seçimleri yapınız.";
                     await FillLookupLists(model);
                     return View(model);
                }
                // --- END FAZ 2A ---


                // 1. Logic Separation
                bool isUpdate = model.IsEditMode && model.PersonelId.HasValue && model.PersonelId.Value > 0;

                // 2. Duplicate Check Strategy
                var conflicts = new List<string>();

                if (isUpdate)
                {
                    // UPDATE: Check OTHER records
                    var duplicateTc = await _context.Personeller.AnyAsync(p => p.TcKimlikNo == model.TcKimlikNo && p.PersonelId != model.PersonelId.Value);
                    if (duplicateTc) conflicts.Add($"TC Kimlik No ({model.TcKimlikNo}) kullanımda.");

                    var duplicateEmail = await _context.Personeller.AnyAsync(p => p.Eposta == model.Eposta && p.PersonelId != model.PersonelId.Value);
                    if (duplicateEmail) conflicts.Add($"E-posta ({model.Eposta}) kullanımda.");
                }
                else
                {
                    // INSERT: Check ANY record
                    var duplicateTc = await _context.Personeller.AnyAsync(p => p.TcKimlikNo == model.TcKimlikNo);
                    if (duplicateTc) conflicts.Add($"TC Kimlik No ({model.TcKimlikNo}) kayıtlı.");

                    var duplicateEmail = await _context.Personeller.AnyAsync(p => p.Eposta == model.Eposta);
                    if (duplicateEmail) conflicts.Add($"E-posta ({model.Eposta}) kayıtlı.");
                }

                if (conflicts.Any())
                {
                    TempData["Error"] = "Kayıt Uyarı: " + string.Join(" | ", conflicts);
                    TempData["ShowUserExistsModal"] = "1";
                    TempData["ModalTitle"] = "Kayıt Çakışması";
                    TempData["ModalItems"] = string.Join("|", conflicts);
                    TempData["OpenTab"] = "tab1";
                    
                    var tcConflict = conflicts.Any(c => c.Contains("TC"));
                    TempData["FocusId"] = tcConflict ? "personelTcKimlik" : "personelemail";
                    
                    await FillLookupLists(model);
                    return View(model);
                }

                // 3. Fotoğraf İşlemi
                string? yeniFotoYolu = null;
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
                    yeniFotoYolu = "/uploads/personeller/" + uniqueFileName;
                }
                
                try 
                {
                    if (isUpdate)
                    {
                        // --- UPDATE EXECUTION ---
                        var personel = await _context.Personeller
                            .Include(p => p.PersonelYazilimlar)
                            .Include(p => p.PersonelUzmanliklar)
                            .Include(p => p.PersonelGorevTurleri)
                            .Include(p => p.PersonelIsNitelikleri)
                            .FirstOrDefaultAsync(p => p.PersonelId == model.PersonelId.Value);

                        if (personel == null) return NotFound();

                        // Alan Güncellemeleri
                        personel.Ad = model.Ad;
                        personel.Soyad = model.Soyad;
                        personel.TcKimlikNo = model.TcKimlikNo;
                        personel.Telefon = model.Telefon ?? "";
                        personel.Eposta = model.Eposta;
                        personel.PersonelCinsiyet = model.PersonelCinsiyet == 1;
                        personel.DogumTarihi = model.DogumTarihi;
                        personel.GorevliIlId = model.GorevliIlId;
                        personel.BransId = model.BransId;
                        personel.KadroKurum = model.KadroKurum ?? "";
                        personel.AktifMi = model.AktifMi;
                        
                        // Fotoğraf Güncelleme
                        if (yeniFotoYolu != null) 
                        {
                            DeletePhotoFile(personel.FotografYolu); // Eskiyi sil
                            personel.FotografYolu = yeniFotoYolu;   // Yeniyi ata
                        }
                        
                        personel.UpdatedAt = DateTime.Now;

                        // Şifre Güncelleme (Sadece doluysa)
                        if (!string.IsNullOrEmpty(model.NewPassword))
                        {
                             CreatePasswordHash(model.NewPassword, out byte[] passwordHash, out byte[] passwordSalt);
                             personel.Sifre = model.NewPassword; // Plain text sync
                             personel.SifreHash = passwordHash;
                             personel.SifreSalt = passwordSalt;
                        }

                        // İlişkileri Temizle
                        _context.PersonelYazilimlar.RemoveRange(personel.PersonelYazilimlar);
                        _context.PersonelUzmanliklar.RemoveRange(personel.PersonelUzmanliklar);
                        _context.PersonelGorevTurleri.RemoveRange(personel.PersonelGorevTurleri);
                        _context.PersonelIsNitelikleri.RemoveRange(personel.PersonelIsNitelikleri);

                        // Main Update
                        _context.Personeller.Update(personel);
                        await _context.SaveChangesAsync(); 

                        // İlişkileri Yeniden Ekle
                        AddRelations(personel.PersonelId, model);
                        await _context.SaveChangesAsync();

                        TempData["Success"] = "Personel bilgileri güncellendi.";
                        return RedirectToAction("Detay", new { id = personel.PersonelId });
                    }
                    else
                    {
                        // --- INSERT EXECUTION ---
                        string autoPasword = model.TcKimlikNo.Length >= 6 ? model.TcKimlikNo.Substring(0, 6) : "123456";
                        CreatePasswordHash(autoPasword, out byte[] passwordHash, out byte[] passwordSalt);

                        var personel = new Personel
                        {
                            Ad = model.Ad,
                            Soyad = model.Soyad,
                            TcKimlikNo = model.TcKimlikNo,
                            Telefon = model.Telefon ?? "",
                            Eposta = model.Eposta,
                            PersonelCinsiyet = model.PersonelCinsiyet == 1,
                            DogumTarihi = model.DogumTarihi,
                            GorevliIlId = model.GorevliIlId,
                            BransId = model.BransId,
                            KadroKurum = model.KadroKurum ?? "",
                            AktifMi = model.AktifMi,
                            FotografYolu = yeniFotoYolu,
                            Sifre = autoPasword, // Plain text sync
                            SifreHash = passwordHash,
                            SifreSalt = passwordSalt,
                            CreatedAt = DateTime.Now,
                            UpdatedAt = null
                        };

                        _context.Personeller.Add(personel);
                        await _context.SaveChangesAsync(); // Get ID

                        AddRelations(personel.PersonelId, model);
                        await _context.SaveChangesAsync();

                        TempData["Success"] = $"Yeni personel kaydedildi. Başlangıç şifresi: {autoPasword}";
                        return RedirectToAction("Detay", new { id = personel.PersonelId });
                    }
                }
                catch (Exception ex)
                {
                     ModelState.AddModelError("", $"İşlem sırasında beklenmeyen bir hata oluştu: {ex.Message}");
                     TempData["Error"] = "İşlem hatası.";
                }
            }
            else 
            {
               var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
               TempData["Error"] = "Formda hatalar var: " + string.Join(", ", errors.Take(3)) + "...";
            }

            await FillLookupLists(model);
            return View(model);
        }

        private void AddRelations(int personelId, PersonelEkleViewModel model)
        {
            if (model.SeciliYazilimIdleri != null)
                foreach (var id in model.SeciliYazilimIdleri) _context.PersonelYazilimlar.Add(new PersonelYazilim { PersonelId = personelId, YazilimId = id });
            
            if (model.SeciliUzmanlikIdleri != null)
                foreach (var id in model.SeciliUzmanlikIdleri) _context.PersonelUzmanliklar.Add(new PersonelUzmanlik { PersonelId = personelId, UzmanlikId = id });

            if (model.SeciliGorevTuruIdleri != null)
                foreach (var id in model.SeciliGorevTuruIdleri) _context.PersonelGorevTurleri.Add(new PersonelGorevTuru { PersonelId = personelId, GorevTuruId = id });

            if (model.SeciliIsNiteligiIdleri != null)
                foreach (var id in model.SeciliIsNiteligiIdleri) _context.PersonelIsNitelikleri.Add(new PersonelIsNiteligi { PersonelId = personelId, IsNiteligiId = id });
        }

        private void DeletePhotoFile(string? path)
        {
            if (string.IsNullOrEmpty(path)) return;

            try 
            {
                string webRootPath = _hostEnvironment.WebRootPath;
                string relativePath = path.TrimStart('/'); 
                string fullPath = Path.Combine(webRootPath, relativePath.Replace('/', Path.DirectorySeparatorChar));

                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Fotoğraf silinemedi: {ex.Message}");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Sil(int id)
        {
            var personel = await _context.Personeller.FindAsync(id);
            if (personel != null)
            {
                DeletePhotoFile(personel.FotografYolu);
                
                _context.Personeller.Remove(personel);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Personel silindi.";
            }
            return RedirectToAction("Index");
        }

        [HttpGet("/Personel/Detay/{id:int}")]
        public async Task<IActionResult> Detay(int id)
        {
            // Security Check: Users can only view their own details
            var currentUserId = User.FindFirst("PersonelId")?.Value;
            if (currentUserId != null && currentUserId != id.ToString())
            {
                 // Redirect to their own detail or show access denied
                 return RedirectToAction("AccessDenied", "Account");
            }

            var personel = await _context.Personeller
                .Include(p => p.GorevliIl)
                .Include(p => p.Brans)
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
                GorevliIl = personel.GorevliIl.Ad,
                Brans = personel.Brans.Ad,
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

             // Refactored Lookups for new Entities
             model.Branslar = await _context.Branslar.AsNoTracking().Select(b => new LookupItemVm { Id = b.BransId, Ad = b.Ad }).ToListAsync();
             model.Iller = await _context.Iller.AsNoTracking().Select(i => new LookupItemVm { Id = i.IlId, Ad = i.Ad }).ToListAsync();
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

                // Fetch new lists (can be cached too if desired)
                var iller = await _context.Iller.AsNoTracking().OrderBy(i => i.Ad).Select(x => new LookupItemVm { Id = x.IlId, Ad = x.Ad }).ToListAsync();
                var branslar = await _context.Branslar.AsNoTracking().OrderBy(b => b.Ad).Select(x => new LookupItemVm { Id = x.BransId, Ad = x.Ad }).ToListAsync();

                model.Yazilimlar = yazilimlar ?? new List<LookupItemVm>();
                model.Uzmanliklar = uzmanliklar ?? new List<LookupItemVm>();
                model.GorevTurleri = gorevTurleri ?? new List<LookupItemVm>();
                model.IsNitelikleri = isNitelikleri ?? new List<LookupItemVm>();
                model.Iller = iller;
                model.Branslar = branslar;
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
