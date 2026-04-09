using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using PersonelTakipSistemi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PersonelTakipSistemi.Data;
using PersonelTakipSistemi.Models;

using PersonelTakipSistemi.Models.ViewModels;
using System.Diagnostics;

using PersonelTakipSistemi.Services;
using PersonelTakipSistemi.Filters;

namespace PersonelTakipSistemi.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [ReadOnlyForHighLevelRoles]
    public partial class PersonelController : Controller
    {
        private readonly TegmPersonelTakipDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly INotificationService _notificationService;
        private readonly ILogService _logService;
        private readonly IExcelService _excelService;
        private readonly IFileValidationService _fileValidationService;
        private readonly IPasswordService _passwordService;
        private readonly IPersonelLookupService _personelLookupService;
        private readonly IPersonelAuthorizationService _personelAuthorizationService;
        private readonly IPersonelAssignmentService _personelAssignmentService;

        public PersonelController(TegmPersonelTakipDbContext context, IWebHostEnvironment hostEnvironment, INotificationService notificationService, ILogService logService, IExcelService excelService, IFileValidationService fileValidationService, IPasswordService passwordService, IPersonelLookupService personelLookupService, IPersonelAuthorizationService personelAuthorizationService, IPersonelAssignmentService personelAssignmentService)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _notificationService = notificationService;
            _logService = logService;
            _excelService = excelService;
            _fileValidationService = fileValidationService;
            _passwordService = passwordService;
            _personelLookupService = personelLookupService;
            _personelAuthorizationService = personelAuthorizationService;
            _personelAssignmentService = personelAssignmentService;
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
        [HttpPost]
        [Authorize(Roles = "Admin,Yönetici")]
        public async Task<IActionResult> ImportExcel(IFormFile file)
        {
            if (file == null) return Json(new { success = false, message = "Dosya seçilmedi." });

            var validation = _fileValidationService.ValidateExcel(file);
            if (!validation.isValid) return Json(new { success = false, message = validation.message });

            var (personeller, errors) = await _excelService.ImportPersonelListAsync(file);

            if (personeller.Count > 0)
            {
                await _logService.LogAsync("Veri Aktarimi", $"Excel ile {personeller.Count} personel eklendi.", null, null);
            }

            if (errors.Any())
            {
                // Partial success or total failure
                return Json(new 
                { 
                    success = personeller.Count > 0, // True if some succeeded, false if all failed
                    partial = personeller.Count > 0,
                    message = personeller.Count > 0 ? $"{personeller.Count} personel eklendi. Ancak bazi satirlarda hatalar mevcut:" : "Hiçbir personel eklenemedi. Lütfen hatalari kontrol edin:",
                    errors = errors,
                    importedIds = personeller.Select(p => p.PersonelId).ToList()
                });
            }

            return Json(new { 
                success = true, 
                message = $"{personeller.Count} personel basariyla eklendi.",
                importedIds = personeller.Select(p => p.PersonelId).ToList()
            });
        }

        [HttpGet]
        public async Task<IActionResult> DownloadTemplate()
        {
            var content = await _excelService.GeneratePersonelTemplateAsync();
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "PersonelYuklemeSablonu.xlsx");
        }


        [HttpGet]
        public async Task<IActionResult> Index(PersonelIndexFilterViewModel filter, int? highlightId = null)
        {
            ViewBag.HighlightId = highlightId;
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

            if (filter.KadroIlId.HasValue)
            {
                query = query.Where(p => p.KadroIlId == filter.KadroIlId.Value);
            }

            if (filter.KadroIlceId.HasValue)
            {
                query = query.Where(p => p.KadroIlceId == filter.KadroIlceId.Value);
            }

            if (filter.DogumBaslangic.HasValue)
            {
                query = query.Where(p => p.DogumTarihi >= filter.DogumBaslangic.Value);
            }

            if (filter.SeciliYazilimIdleri != null && filter.SeciliYazilimIdleri.Any())
            {
                query = query.Where(p => p.PersonelYazilimlar.Any(py => filter.SeciliYazilimIdleri.Contains(py.YazilimId)));
            }


            if (filter.TeskilatId.HasValue)
            {
                query = query.Where(p => p.PersonelTeskilatlar.Any(pt => pt.TeskilatId == filter.TeskilatId.Value));
            }

            if (filter.KoordinatorlukId.HasValue)
            {
                query = query.Where(p => p.PersonelKoordinatorlukler.Any(pk => pk.KoordinatorlukId == filter.KoordinatorlukId.Value));
            }

            if (filter.KomisyonId.HasValue)
            {
                query = query.Where(p => p.PersonelKomisyonlar.Any(pk => pk.KomisyonId == filter.KomisyonId.Value));
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

            // 2. Sayfalama Hazirligi
            var totalItems = await query.CountAsync();
            var results = await query
                .OrderByDescending(p => p.UpdatedAt ?? p.CreatedAt)
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .Select(p => new PersonelIndexRowViewModel
                {
                    PersonelId = p.PersonelId,
                    AdSoyad = p.Ad + " " + p.Soyad,
                    Brans = p.Brans.Ad,
                    GorevliIl = p.GorevliIl.Ad,
                    KadroIl = p.KadroIl != null ? p.KadroIl.Ad : "",
                    KadroIlce = p.KadroIlce != null ? p.KadroIlce.Ad : "",
                    Eposta = p.Eposta,
                    AktifMi = p.AktifMi,
                    FotografYolu = p.FotografYolu,
                    Yazilimlar = p.PersonelYazilimlar.Select(py => py.Yazilim.Ad).ToList(),
                    Uzmanliklar = p.PersonelUzmanliklar.Select(pu => pu.Uzmanlik.Ad).ToList(),
                    GorevTurleri = p.PersonelGorevTurleri.Select(pg => pg.GorevTuru.Ad).ToList(),
                    IsNitelikleri = p.PersonelIsNitelikleri.Select(pi => pi.IsNiteligi.Ad).ToList(),
                    AddedViaTemplate = p.AddedViaTemplate
                })
                .ToListAsync();

            // 3. ViewModel Hazirligi
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
            await FillIndexLookupsAsync(model.Lookups, filter);
            
            if (filter.KadroIlId.HasValue)
            {
                model.Lookups.KadroIlceler = await _personelLookupService.GetIlceLookupItemsAsync(filter.KadroIlId.Value);
            }

            sw.Stop();
            ViewBag.LoadTime = sw.ElapsedMilliseconds;
            return View("PersonelIndex", model); // Explicitly specifying view name
        }

        [HttpGet]
        public IActionResult Yeni()
        {
            // "Yeni Personel Ekle" tiklandiginda kesinlikle temiz state ile gitmek için redirect kullaniyoruz.
            return RedirectToAction("Ekle", new { id = (int?)null });
        }

        [HttpGet]
        public async Task<IActionResult> Ekle(int? id)
        {
            // 1. Yeni Kayit Modu (Insert)
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
                    SeciliIsNiteligiIdleri = new List<int>(),

                    SistemRolId = 4, // Default: Kullanici
                    IsAuthSkipped = false,
                    AktifMi = true // Default to Active
                };
                
                await FillLookupListsAsync(cleanModel);
                return View(cleanModel);
            }

            // 2. Düzenleme Modu (Update)
            
            // Security Check for Edit
            if (id.HasValue && !User.IsInRole("Admin") && !User.IsInRole("Yönetici"))
            {
                 var currentUserId = User.FindFirst("PersonelId")?.Value;
                 if (currentUserId != null && currentUserId != id.Value.ToString())
                 {
                      return RedirectToAction("AccessDenied", "Account");
                 }
            }

            var personel = await _context.Personeller
                .Include(p => p.PersonelYazilimlar)
                .Include(p => p.PersonelUzmanliklar)
                .Include(p => p.PersonelGorevTurleri)
                .Include(p => p.PersonelIsNitelikleri)
                .Include(p => p.PersonelKurumsalRolAtamalari).ThenInclude(pkr => pkr.KurumsalRol)
                .Include(p => p.PersonelKurumsalRolAtamalari).ThenInclude(pkr => pkr.Teskilat)
                .Include(p => p.PersonelKurumsalRolAtamalari).ThenInclude(pkr => pkr.Koordinatorluk!).ThenInclude(k => k.Teskilat)
                .Include(p => p.PersonelKurumsalRolAtamalari).ThenInclude(pkr => pkr.Komisyon!).ThenInclude(k => k.Koordinatorluk!).ThenInclude(k => k.Teskilat)
                // Missing Includes for Implicit Roles
                .Include(p => p.PersonelKomisyonlar).ThenInclude(pk => pk.Komisyon).ThenInclude(k => k.Koordinatorluk).ThenInclude(koord => koord.Teskilat)
                .Include(p => p.PersonelKoordinatorlukler).ThenInclude(pk => pk.Koordinatorluk).ThenInclude(k => k.Teskilat)
                .Include(p => p.PersonelTeskilatlar).ThenInclude(pt => pt.Teskilat)
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
                TcKimlikNo = !string.IsNullOrEmpty(personel.TcKimlikNo) && personel.TcKimlikNo.Length >= 4 
                    ? personel.TcKimlikNo.Substring(0, 2) + "*******" + personel.TcKimlikNo.Substring(personel.TcKimlikNo.Length - 2) 
                    : personel.TcKimlikNo,
                Telefon = personel.Telefon,
                Eposta = personel.Eposta,
                PersonelCinsiyet = personel.PersonelCinsiyet ? 1 : 0,
                DogumTarihi = personel.DogumTarihi,
                GorevliIlId = personel.GorevliIlId,
                KadroIlId = personel.KadroIlId,
                KadroIlceId = personel.KadroIlceId,
                BransId = personel.BransId,
                KadroKurum = personel.KadroKurum,
                AktifMi = personel.AktifMi,
                FotografYolu = personel.FotografYolu,
                
                // Iliskili tablolari seçili olarak isaretle
                SeciliYazilimIdleri = personel.PersonelYazilimlar.Select(x => x.YazilimId).ToList(),
                SeciliUzmanlikIdleri = personel.PersonelUzmanliklar.Select(x => x.UzmanlikId).ToList(),
                SeciliGorevTuruIdleri = personel.PersonelGorevTurleri.Select(x => x.GorevTuruId).ToList(),
                SeciliIsNiteligiIdleri = personel.PersonelIsNitelikleri.Select(x => x.IsNiteligiId).ToList(),

                // Auth Data
                SistemRolId = personel.SistemRolId // Assuming property exists on Personel entity
                // If YetkiKapsami exists on Personel, map it too. If not, default "Self".
            };

            // Existing Roles Serialization for Edit Mode
            var existingRoles = new List<RoleAssignmentDto>();
            var coveredKomIds = new HashSet<int>();
            var coveredKoordIds = new HashSet<int>();
            var coveredTesIds = new HashSet<int>();

            // 1. Explicit Roles
            foreach(var x in personel.PersonelKurumsalRolAtamalari)
            {
                existingRoles.Add(new RoleAssignmentDto
                {
                    rolId = x.KurumsalRolId.ToString(),
                    rolAd = x.KurumsalRol.Ad,
                    teskilatId = x.TeskilatId?.ToString(),
                    teskilatAd = x.Teskilat?.Ad,
                    koordinatorlukId = x.KoordinatorlukId?.ToString(),
                    koordinatorlukAd = x.Koordinatorluk?.Ad,
                    komisyonId = x.KomisyonId?.ToString(),
                    komisyonAd = x.Komisyon?.Ad
                });

                // Ensure hierarchy is populated for Edit (Fix for "Edit not working on explicit roles")
                var lastRole = existingRoles.Last();
                if(lastRole.teskilatId == null)
                {
                     if(x.Koordinatorluk != null) lastRole.teskilatId = x.Koordinatorluk.TeskilatId.ToString();
                     if(x.Komisyon != null && x.Komisyon.Koordinatorluk != null) lastRole.teskilatId = x.Komisyon.Koordinatorluk.TeskilatId.ToString();
                }
                if(lastRole.koordinatorlukId == null)
                {
                     if(x.Komisyon != null) lastRole.koordinatorlukId = x.Komisyon.KoordinatorlukId.ToString();
                }

                if (x.KomisyonId.HasValue) 
                {
                    coveredKomIds.Add(x.KomisyonId.Value);
                    if(x.Komisyon?.KoordinatorlukId != null) coveredKoordIds.Add(x.Komisyon.KoordinatorlukId);
                    if(x.Komisyon?.Koordinatorluk?.TeskilatId != null) coveredTesIds.Add(x.Komisyon.Koordinatorluk.TeskilatId);
                }
                
                if (x.KoordinatorlukId.HasValue) 
                {
                    coveredKoordIds.Add(x.KoordinatorlukId.Value);
                    if(x.Koordinatorluk?.TeskilatId != null) coveredTesIds.Add(x.Koordinatorluk.TeskilatId);
                }
                
                if (x.TeskilatId.HasValue) coveredTesIds.Add(x.TeskilatId.Value);
            }

            // 2. Implicit Roles (Ghosts)
            // Komisyon
            foreach(var pk in personel.PersonelKomisyonlar)
            {
                if(coveredKomIds.Contains(pk.KomisyonId)) continue;
                existingRoles.Add(new RoleAssignmentDto
                {
                     rolId = "", 
                     rolAd = "", // Empty to trigger "Rolü Yok" in UI
                     teskilatId = pk.Komisyon.Koordinatorluk?.TeskilatId.ToString(),
                     teskilatAd = pk.Komisyon.Koordinatorluk?.Teskilat?.Ad,
                     koordinatorlukId = pk.Komisyon.KoordinatorlukId.ToString(),
                     koordinatorlukAd = pk.Komisyon.Koordinatorluk?.Ad,
                     komisyonId = pk.Komisyon.KomisyonId.ToString(),
                     komisyonAd = pk.Komisyon.Ad,
                     isImplicit = true
                });
                if(pk.Komisyon.KoordinatorlukId != 0) coveredKoordIds.Add(pk.Komisyon.KoordinatorlukId);
            }
            // Koordinatorluk
            foreach(var pk in personel.PersonelKoordinatorlukler)
            {
                if(coveredKoordIds.Contains(pk.KoordinatorlukId)) continue;
                 existingRoles.Add(new RoleAssignmentDto
                {
                     rolId = "",
                     rolAd = "",
                     teskilatId = pk.Koordinatorluk.TeskilatId.ToString(),
                     teskilatAd = pk.Koordinatorluk.Teskilat?.Ad,
                     koordinatorlukId = pk.Koordinatorluk.KoordinatorlukId.ToString(),
                     koordinatorlukAd = pk.Koordinatorluk.Ad,
                     isImplicit = true
                });
                if(pk.Koordinatorluk.TeskilatId != 0) coveredTesIds.Add(pk.Koordinatorluk.TeskilatId);
            }
            // Teskilat
            foreach(var pt in personel.PersonelTeskilatlar)
            {
                if(coveredTesIds.Contains(pt.TeskilatId)) continue;
                 existingRoles.Add(new RoleAssignmentDto
                {
                     rolId = "",
                     rolAd = "",
                     teskilatId = pt.Teskilat.TeskilatId.ToString(),
                     teskilatAd = pt.Teskilat.Ad,
                     isImplicit = true
                });
            }

            model.AtananRollerJson = System.Text.Json.JsonSerializer.Serialize(existingRoles);
            
            if (model.KadroIlId.HasValue)
            {
                model.KadroIlceler = await _context.Ilceler
                    .AsNoTracking()
                    .Where(x => x.IlId == model.KadroIlId.Value)
                    .OrderBy(x => x.Ad)
                    .Select(x => new LookupItemVm { Id = x.IlceId, Ad = x.Ad })
                    .ToListAsync();
            }

            await FillLookupListsAsync(model);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Ekle(PersonelEkleViewModel model, IFormFile? personelFoto)
        {
            // --- MASKED TC HANDLING (Edit Mode) ---
            if (model.IsEditMode && model.PersonelId.HasValue && !string.IsNullOrEmpty(model.TcKimlikNo) && model.TcKimlikNo.Contains("*"))
            {
                var originalTc = await _context.Personeller
                    .Where(p => p.PersonelId == model.PersonelId.Value)
                    .Select(p => p.TcKimlikNo)
                    .FirstOrDefaultAsync();

                if (originalTc != null)
                {
                   // Restore original value
                   model.TcKimlikNo = originalTc;

                   // Clear Validation Errors triggered by '*' characters (Regex/Length)
                   var key = ModelState["TcKimlikNo"];
                   if (key != null)
                   {
                       key.Errors.Clear();
                       key.ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
                   }
                }
            }

            if (ModelState.IsValid)
            {
                // TELEFON NORMALIZASYONU
                if (!string.IsNullOrEmpty(model.Telefon))
                {
                    // Sadece rakamlari al
                    string cleanPhone = new string(model.Telefon.Where(char.IsDigit).ToArray());
                    
                    // Basta 0 varsa kaldir
                    if (cleanPhone.StartsWith("0"))
                    {
                        cleanPhone = cleanPhone.Substring(1);
                    }

                    // 10 hane kontrolü
                    if (cleanPhone.Length != 10)
                    {
                        ModelState.AddModelError("Telefon", "Telefon numarasi 10 haneli olmalidir (Örn: 5551234567).");
                        // Hata durumunda dropdownlari tekrar doldurup view'a dön
                        await FillLookupListsAsync(model);
                        return View(model);
                    }

                    // Modele geri ata (DB'ye bu formatta gidecek)
                    model.Telefon = cleanPhone;
                }

                // --- FAZ 2A: Zorunlu Alan Kontrolleri (Multi-Select) ---
                if (model.SeciliYazilimIdleri == null || !model.SeciliYazilimIdleri.Any())
                {
                    ModelState.AddModelError("SeciliYazilimIdleri", "En az bir yazilim seçilmelidir.");
                }
                if (model.SeciliUzmanlikIdleri == null || !model.SeciliUzmanlikIdleri.Any())
                {
                     ModelState.AddModelError("SeciliUzmanlikIdleri", "En az bir uzmanlik seçilmelidir.");
                }
                if (model.SeciliGorevTuruIdleri == null || !model.SeciliGorevTuruIdleri.Any())
                {
                     ModelState.AddModelError("SeciliGorevTuruIdleri", "En az bir görev türü seçilmelidir.");
                }
                if (model.SeciliIsNiteligiIdleri == null || !model.SeciliIsNiteligiIdleri.Any())
                {
                     ModelState.AddModelError("SeciliIsNiteligiIdleri", "En az bir is niteligi seçilmelidir.");
                }
                
                // Eger manuel ekledigimiz hatalar varsa IsValid false dönecek (state check needed potentially, or re-check)
                if(!ModelState.IsValid)
                {
                     var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                     TempData["Error"] = "Lütfen tüm zorunlu seçimleri yapiniz.";
                     await FillLookupListsAsync(model);
                     return View(model);
                }
                // --- END FAZ 2A ---


                // 1. Logic Separation
                bool isUpdate = model.IsEditMode && model.PersonelId.HasValue && model.PersonelId.Value > 0;

                // --- NEW: Security Check for "Yönetici" role ---
                if (isUpdate && User.IsInRole("Yönetici"))
                {
                    var currentUserId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");
                    if (model.PersonelId != currentUserId)
                    {
                         TempData["Error"] = "Yetki Hatasi: Sadece kendi bilgilerinizi güncelleyebilirsiniz.";
                         await FillLookupListsAsync(model);
                         return View(model);
                    }
                }
                // --- End Security Check ---

                // 2. Duplicate Check Strategy
                var conflicts = new List<string>();

                if (isUpdate)
                {
                    // UPDATE: Check OTHER records
                    var duplicateTc = await _context.Personeller.AnyAsync(p => p.TcKimlikNo == model.TcKimlikNo && p.PersonelId != model.PersonelId!.Value);
                    if (duplicateTc) conflicts.Add($"TC Kimlik No ({model.TcKimlikNo}) kullanimda.");

                    var duplicateEmail = await _context.Personeller.AnyAsync(p => p.Eposta == model.Eposta && p.PersonelId != model.PersonelId!.Value);
                    if (duplicateEmail) conflicts.Add($"E-posta ({model.Eposta}) kullanimda.");
                }
                else
                {
                    // INSERT: Check ANY record
                    var duplicateTc = await _context.Personeller.AnyAsync(p => p.TcKimlikNo == model.TcKimlikNo);
                    if (duplicateTc) conflicts.Add($"TC Kimlik No ({model.TcKimlikNo}) kayitli.");

                    var duplicateEmail = await _context.Personeller.AnyAsync(p => p.Eposta == model.Eposta);
                    if (duplicateEmail) conflicts.Add($"E-posta ({model.Eposta}) kayitli.");
                }

                if (conflicts.Any())
                {
                    TempData["Error"] = "Kayit Uyari: " + string.Join(" | ", conflicts);
                    TempData["ShowUserExistsModal"] = "1";
                    TempData["ModalTitle"] = "Kayit Çakismasi";
                    TempData["ModalItems"] = string.Join("|", conflicts);
                    TempData["OpenTab"] = "tab1";
                    
                    var tcConflict = conflicts.Any(c => c.Contains("TC"));
                    TempData["FocusId"] = tcConflict ? "personelTcKimlik" : "personelemail";
                    
                    await FillLookupListsAsync(model);
                    return View(model);
                }

                // 3. Fotograf Islemi
                string? yeniFotoYolu = null;
                if (personelFoto != null && personelFoto.Length > 0)
                {
                    var validation = _fileValidationService.ValidateImage(personelFoto);
                    if (!validation.isValid)
                    {
                        TempData["Error"] = validation.message;
                        await FillLookupListsAsync(model);
                        return View(model);
                    }

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
                            .Include(p => p.PersonelYazilimlar).ThenInclude(py => py.Yazilim)
                            .Include(p => p.PersonelUzmanliklar).ThenInclude(pu => pu.Uzmanlik)
                            .Include(p => p.PersonelGorevTurleri).ThenInclude(pg => pg.GorevTuru)
                            .Include(p => p.PersonelIsNitelikleri).ThenInclude(pi => pi.IsNiteligi)
                            .Include(p => p.PersonelKurumsalRolAtamalari) // Fix: Include for deletion logic
                            .Include(p => p.PersonelKomisyonlar)
                            .Include(p => p.PersonelKoordinatorlukler)
                            .Include(p => p.PersonelTeskilatlar)
                            .Include(p => p.Brans)
                            .Include(p => p.GorevliIl)
                            .FirstOrDefaultAsync(p => p.PersonelId == model.PersonelId!.Value);

                        if (personel == null) return NotFound();

                        // --- CHANGE DETECTION START ---
                        var changes = new List<string>();

                        // 1. Basic Info Changes
                        if (personel.Ad != model.Ad) changes.Add($"Ad: {personel.Ad} -> {model.Ad}");
                        if (personel.Soyad != model.Soyad) changes.Add($"Soyad: {personel.Soyad} -> {model.Soyad}");
                        if (personel.TcKimlikNo != model.TcKimlikNo) changes.Add($"TC: Degisti");
                        if (personel.Telefon != (model.Telefon ?? "")) changes.Add($"Telefon Degisti");
                        if (personel.Eposta != model.Eposta) changes.Add($"E-posta: {personel.Eposta} -> {model.Eposta}");
                        if (personel.GorevliIlId != model.GorevliIlId) 
                        {
                             var newIl = await _context.Iller.FindAsync(model.GorevliIlId);
                             changes.Add($"Il: {personel.GorevliIl?.Ad} -> {newIl?.Ad}");
                        }
                        if (personel.KadroIlId != model.KadroIlId)
                        {
                             var newKadroIl = await _context.Iller.FindAsync(model.KadroIlId);
                             changes.Add($"Kadro Ili: {personel.KadroIl?.Ad} -> {newKadroIl?.Ad}");
                        }
                        if (personel.KadroIlceId != model.KadroIlceId)
                        {
                             var newKadroIlce = await _context.Ilceler.FindAsync(model.KadroIlceId);
                             changes.Add($"Kadro Ilçesi: {personel.KadroIlce?.Ad} -> {newKadroIlce?.Ad}");
                        }
                        if (personel.BransId != model.BransId) 
                        {
                             var newBrans = await _context.Branslar.FindAsync(model.BransId);
                             changes.Add($"Brans: {personel.Brans?.Ad} -> {newBrans?.Ad}");
                        }
                        if (personel.SistemRolId != model.SistemRolId)
                        {
                            var newSistemRol = await _context.SistemRoller.FindAsync(model.SistemRolId);
                            var oldSistemRol = await _context.SistemRoller.FindAsync(personel.SistemRolId);
                            changes.Add($"Sistem Rolü: {oldSistemRol?.Ad} -> {newSistemRol?.Ad}");
                        }

                        // 2. Collection Changes (Helper Function)

                        
                        // Optimized Collection Diffs
                        // Yazilim
                        var oldYazilim = personel.PersonelYazilimlar.Select(x => x.Yazilim.Ad).ToList();
                        var newYazilim = new List<string>();
                        if(model.SeciliYazilimIdleri != null && model.SeciliYazilimIdleri.Any())
                             newYazilim = await _context.Yazilimlar.Where(x => model.SeciliYazilimIdleri.Contains(x.YazilimId)).Select(x => x.Ad).ToListAsync();
                        
                        var addedYazilim = newYazilim.Except(oldYazilim).ToList();
                        var removedYazilim = oldYazilim.Except(newYazilim).ToList();
                        if (addedYazilim.Any()) changes.Add($"Eklenen Beceriler: {string.Join(", ", addedYazilim)}");
                        if (removedYazilim.Any()) changes.Add($"Çikarilan Beceriler: {string.Join(", ", removedYazilim)}");

                        // Uzmanlik
                        var oldUzmanlik = personel.PersonelUzmanliklar.Select(x => x.Uzmanlik.Ad).ToList();
                        var newUzmanlik = new List<string>();
                        if (model.SeciliUzmanlikIdleri != null && model.SeciliUzmanlikIdleri.Any())
                            newUzmanlik = await _context.Uzmanliklar.Where(x => model.SeciliUzmanlikIdleri.Contains(x.UzmanlikId)).Select(x => x.Ad).ToListAsync();

                        var addedUzmanlik = newUzmanlik.Except(oldUzmanlik).ToList();
                        var removedUzmanlik = oldUzmanlik.Except(newUzmanlik).ToList();
                        if (addedUzmanlik.Any()) changes.Add($"Eklenen Uzmanliklar: {string.Join(", ", addedUzmanlik)}");
                        if (removedUzmanlik.Any()) changes.Add($"Çikarilan Uzmanliklar: {string.Join(", ", removedUzmanlik)}");

                        // Gorev Turu
                        var oldGorev = personel.PersonelGorevTurleri.Select(x => x.GorevTuru.Ad).ToList();
                        var newGorev = new List<string>();
                        if (model.SeciliGorevTuruIdleri != null && model.SeciliGorevTuruIdleri.Any())
                            newGorev = await _context.GorevTurleri.Where(x => model.SeciliGorevTuruIdleri.Contains(x.GorevTuruId)).Select(x => x.Ad).ToListAsync();

                        var addedGorev = newGorev.Except(oldGorev).ToList();
                        var removedGorev = oldGorev.Except(newGorev).ToList();
                        if (addedGorev.Any()) changes.Add($"Eklenen Görev Türleri: {string.Join(", ", addedGorev)}");
                        if (removedGorev.Any()) changes.Add($"Çikarilan Görev Türleri: {string.Join(", ", removedGorev)}");
                        
                        // Is Niteligi
                        var oldNitelik = personel.PersonelIsNitelikleri.Select(x => x.IsNiteligi.Ad).ToList();
                        var newNitelik = new List<string>();
                        if (model.SeciliIsNiteligiIdleri != null && model.SeciliIsNiteligiIdleri.Any())
                            newNitelik = await _context.IsNitelikleri.Where(x => model.SeciliIsNiteligiIdleri.Contains(x.IsNiteligiId)).Select(x => x.Ad).ToListAsync();

                        var addedNitelik = newNitelik.Except(oldNitelik).ToList();
                        var removedNitelik = oldNitelik.Except(newNitelik).ToList();
                        if (addedNitelik.Any()) changes.Add($"Eklenen Is Nitelikleri: {string.Join(", ", addedNitelik)}");
                        if (removedNitelik.Any()) changes.Add($"Çikarilan Is Nitelikleri: {string.Join(", ", removedNitelik)}");

                        // --- CHANGE DETECTION END ---

                        // Alan Güncellemeleri
                        personel.Ad = model.Ad;
                        personel.Soyad = model.Soyad;
                        personel.TcKimlikNo = model.TcKimlikNo;
                        personel.Telefon = model.Telefon ?? "";
                        personel.Eposta = model.Eposta;
                        personel.PersonelCinsiyet = model.PersonelCinsiyet == 1;
                        personel.DogumTarihi = model.DogumTarihi;
                        personel.GorevliIlId = (int)model.GorevliIlId;
                        personel.KadroIlId = model.KadroIlId;
                        personel.KadroIlceId = model.KadroIlceId;
                        personel.BransId = (int)model.BransId;
                        personel.KadroKurum = model.KadroKurum ?? "";
                        
                        // Rule: Only Admin can change AktifMi
                        if (User.IsInRole("Admin"))
                        {
                            personel.AktifMi = model.AktifMi;
                        }
                        
                        // Rule: Only Admin can change SistemRolId
                        if (User.IsInRole("Admin"))
                        {
                            personel.SistemRolId = model.SistemRolId;
                        }
                        // Non-admins keep their existing role (personel.SistemRolId remains unchanged)
                        
                        // Fotograf Güncelleme
                        if (yeniFotoYolu != null) 
                        {
                            DeletePhotoFile(personel.FotografYolu); // Eskiyi sil
                            personel.FotografYolu = yeniFotoYolu;   // Yeniyi ata
                            changes.Add("Fotograf güncellendi");
                        }
                        
                        personel.UpdatedAt = DateTime.Now;

                        // Sifre Güncelleme (Secure Logic)
                        if (!string.IsNullOrEmpty(model.NewPassword))
                        {
                            bool isAdminOrManager = User.IsInRole("Admin") || User.IsInRole("Yönetici");
                            
                            // Kural: Admin/Yönetici degilse Eski Sifre sorulmali.
                            // Istisna: Ilk sifre degisimi ise (Varsayilan sifre kullaniliyorsa) sorulmaz.
                            if (!isAdminOrManager)
                            {
                                // Varsayilan sifre kontrolü (TC ilk 6 hane)
                                string defaultPass = personel.TcKimlikNo.Length >= 6 ? personel.TcKimlikNo.Substring(0, 6) : "123456";
                                bool isDefaultPassword = _passwordService.VerifyPassword(defaultPass, personel.SifreHash, personel.SifreSalt);

                                if (!isDefaultPassword)
                                {
                                    if (string.IsNullOrEmpty(model.EskiSifre))
                                    {
                                        ModelState.AddModelError("EskiSifre", "Mevcut sifrenizi girmelisiniz.");
                                        TempData["Error"] = "Sifre degistirme hatasi: Mevcut sifrenizi giriniz.";
                                        TempData["OpenTab"] = "tab_password"; // Hata durumunda ilgili tabi aç
                                        await FillLookupListsAsync(model);
                                        return View(model);
                                    }

                                    if (!_passwordService.VerifyPassword(model.EskiSifre, personel.SifreHash, personel.SifreSalt))
                                    {
                                        ModelState.AddModelError("EskiSifre", "Mevcut sifreniz hatali.");
                                        TempData["Error"] = "Sifre degistirme hatasi: Mevcut sifreniz hatali.";
                                        TempData["OpenTab"] = "tab_password";
                                        await FillLookupListsAsync(model);
                                        return View(model);
                                    }
                                }
                            }

                             _passwordService.SetPassword(personel, model.NewPassword);
                             changes.Add("Sifre degisti");
                        }

                        // Iliskileri Temizle
                        _context.PersonelYazilimlar.RemoveRange(personel.PersonelYazilimlar);
                        _context.PersonelUzmanliklar.RemoveRange(personel.PersonelUzmanliklar);
                        _context.PersonelGorevTurleri.RemoveRange(personel.PersonelGorevTurleri);
                        _context.PersonelIsNitelikleri.RemoveRange(personel.PersonelIsNitelikleri);
                        _context.PersonelKurumsalRolAtamalari.RemoveRange(personel.PersonelKurumsalRolAtamalari); // Clear existing roles
                        _context.PersonelKomisyonlar.RemoveRange(personel.PersonelKomisyonlar);
                        _context.PersonelKoordinatorlukler.RemoveRange(personel.PersonelKoordinatorlukler);
                        _context.PersonelTeskilatlar.RemoveRange(personel.PersonelTeskilatlar);

                        // Main Update
                        _context.Personeller.Update(personel);
                        await _context.SaveChangesAsync(); 

                        // Iliskileri Yeniden Ekle
                        AddRelations(personel.PersonelId, model);
                        await _context.SaveChangesAsync();

                        // Process AtananRoller (JSON) for update
                        if (!string.IsNullOrEmpty(model.AtananRollerJson))
                        {
                            try 
                            {
                                var roles = System.Text.Json.JsonSerializer.Deserialize<List<RoleAssignmentDto>>(model.AtananRollerJson);
                                if (roles != null)
                                {
                                    foreach(var r in roles)
                                    {
                                        if (int.TryParse(r.rolId, out int rId))
                                        {
                                            int? tId = null;
                                            if (r.teskilatId != null && int.TryParse(r.teskilatId, out int parsedTId)) tId = parsedTId;
                                            
                                            int? kId = null;
                                            if (r.koordinatorlukId != null && int.TryParse(r.koordinatorlukId, out int parsedKId)) kId = parsedKId;
                                            
                                            int? cId = null;
                                            if (r.komisyonId != null && int.TryParse(r.komisyonId, out int parsedCId)) cId = parsedCId;
                                            
                                            // Call existing AddKurumsalRol logic (reused)
                                            // Only if IDs are valid
                                            await AddKurumsalRol(personel.PersonelId, rId, kId, cId, true);
                                        }
                                    }
                                }
                            } 
                            catch (Exception ex) { Debug.WriteLine($"Error deserializing or adding roles: {ex.Message}"); /* Ignore parse errors */ }
                        }

                        // BILDIRIM GÖNDER (Profil Güncelleme)
                        try 
                        {
                            await _notificationService.CreateAsync(
                                aliciId: personel.PersonelId, 
                                gonderenId: null, // System
                                baslik: "Profil bilgileri güncellemesi", 
                                aciklama: $"Sayin {personel.Ad} {personel.Soyad}, personel bilgileriniz güncellenmistir.", 
                                tip: "Profil",
                                refType: "Personel",
                                refId: personel.PersonelId
                            );
                            
                            // LOG
                            string logDetail = changes.Any() ? string.Join(". ", changes) : "Degisiklik yapilmadi.";
                            // Truncate if too long (DB limit protection)
                            if (logDetail.Length > 400) logDetail = logDetail.Substring(0, 397) + "...";

                            await _logService.LogAsync("Guncelleme", $"Personel güncellendi: {personel.Ad} {personel.Soyad}", personel.PersonelId, logDetail);
                        }
                        catch (Exception ex) { System.Diagnostics.Debug.WriteLine($"Profil güncelleme bildirim hatasi: {ex.Message}"); }

                        TempData["Success"] = "Personel bilgileri güncellendi.";
                        return RedirectToAction("Index", new { highlightId = personel.PersonelId });
                    }
                    else
                    {
                        // --- INSERT EXECUTION ---
                        string autoPasword = model.TcKimlikNo.Length >= 6 ? model.TcKimlikNo.Substring(0, 6) : "123456";
                        var passwordResult = _passwordService.HashPassword(autoPasword);

                        var personel = new Personel
                        {
                            Ad = model.Ad,
                            Soyad = model.Soyad,
                            TcKimlikNo = model.TcKimlikNo,
                            Telefon = model.Telefon ?? "",
                            Eposta = model.Eposta,
                            PersonelCinsiyet = model.PersonelCinsiyet == 1,
                            DogumTarihi = model.DogumTarihi,
                            GorevliIlId = (int)model.GorevliIlId,
                            KadroIlId = model.KadroIlId,
                            KadroIlceId = model.KadroIlceId,
                            BransId = (int)model.BransId,
                            KadroKurum = model.KadroKurum ?? "",
                            AktifMi = model.AktifMi,
                            FotografYolu = yeniFotoYolu,
                            SifreHash = passwordResult.Hash,
                            SifreSalt = passwordResult.Salt,
                            CreatedAt = DateTime.Now,
                            UpdatedAt = null,

                            // Rule: Only Admin can set SistemRolId for new users. Others default to 4 (Kullanici)
                            SistemRolId = (User.IsInRole("Admin") && model.SistemRolId != null) ? model.SistemRolId.Value : 4
                        };

                        _context.Personeller.Add(personel);
                        await _context.SaveChangesAsync(); // Get ID

                        AddRelations(personel.PersonelId, model);
                        await _context.SaveChangesAsync();


                        // Handle Roles for Insert (Common Logic)
                        if (model.IsAuthSkipped)
                        {
                            // Ensure default role if skipped (already set in object initializer but sure)
                             personel.SistemRolId = 4; // Kullanici
                             // No institutional roles to add
                        }
                        else 
                        {
                            // Save Roles
                             if (!string.IsNullOrEmpty(model.AtananRollerJson))
                             {
                                 try 
                                 {
                                     var roles = System.Text.Json.JsonSerializer.Deserialize<List<RoleAssignmentDto>>(model.AtananRollerJson);
                                     if (roles != null)
                                     {
                                         foreach(var r in roles)
                                         {
                                             if (int.TryParse(r.rolId, out int rId))
                                             {
                                                 int? tId = null;
                                                 if (r.teskilatId != null && int.TryParse(r.teskilatId, out int parsedTId)) tId = parsedTId;
                                                 
                                                 int? kId = null;
                                                 if (r.koordinatorlukId != null && int.TryParse(r.koordinatorlukId, out int parsedKId)) kId = parsedKId;
                                                 
                                                 int? cId = null;
                                                 if (r.komisyonId != null && int.TryParse(r.komisyonId, out int parsedCId)) cId = parsedCId;
                                                 
                                                 await AddKurumsalRol(personel.PersonelId, rId, kId, cId, true);
                                             }
                                         }
                                     }
                                 } 
                                 catch (Exception ex) { Debug.WriteLine($"Error deserializing or adding roles: {ex.Message}"); }
                             }
                        }

                        try
                        {
                            // Hosgeldin Bildirimi (System Sender)
                            await _notificationService.CreateAsync(
                                aliciId: personel.PersonelId,
                                gonderenId: null, // System
                                baslik: "Hosgeldiniz!",
                                aciklama: "Temel Egitim Genel Müdürlügü Personel Takip Sistemine Hosgeldiniz!",
                                tip: "Sistem"
                            );

                            // LOG
                            await _logService.LogAsync("Ekleme", $"Yeni personel eklendi: {personel.Ad} {personel.Soyad} ({personel.TcKimlikNo})", personel.PersonelId, $"Ekleyen: {User.Identity?.Name ?? "Bilinmiyor"}");
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine($"Hata: Bildirim veya log eklenemedi: {ex.Message}");
                        }

                        TempData["Success"] = $"Yeni personel kaydedildi. Baslangiç sifresi: {autoPasword}";
                        return RedirectToAction("Index", new { highlightId = personel.PersonelId });
                    }
                }
                catch (Exception ex)
                {
                     ModelState.AddModelError("", $"Islem sirasinda beklenmeyen bir hata olustu: {ex.Message}");
                     TempData["Error"] = "Islem hatasi.";
                }
            }
            else 
            {
               var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
               TempData["Error"] = "Formda hatalar var: " + string.Join(", ", errors.Take(3)) + "...";
            }

                    await FillLookupListsAsync(model);
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

            // Process AtananRoller (JSON) for BOTH Insert and Update logic here OR in the main block.
            // Since we call AddRelations from both, let's keep it clean and do it in the main block for better control over async/await and error handling, 
            // OR move the common logic here.
            // Given the complexity of the JSON parsing, let's keep it in the Controller action but ensure it runs for Insert too.
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
                Debug.WriteLine($"Fotograf silinemedi: {ex.Message}");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Yönetici")]
        public async Task<IActionResult> Sil(int id)
        {
            // 1. Mevcut (Islemi Yapan) Kullaniciyi Bul
            var currentUserIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(currentUserIdStr) || !int.TryParse(currentUserIdStr, out int currentUserId))
            {
                return RedirectToAction("Login", "Account");
            }

            // Security Check for Manager: Managers cannot delete others.
            if (User.IsInRole("Yönetici"))
            {
                if (id != currentUserId) // If trying to delete someone else
                {
                    TempData["Error"] = "Yetki Hatasi: Personel silme yetkiniz bulunmamaktadir.";
                    return RedirectToAction("Index");
                }
                // If id == currentUserId, the manager is trying to delete themselves.
                // The existing "Kural 1: Hiç kimse kendini silemez." will handle this.
            }

            // 2. Silinecek Personeli Bul
            var silinecekPersonel = await _context.Personeller.Include(p => p.SistemRol).FirstOrDefaultAsync(p => p.PersonelId == id); // Include Role
            if (silinecekPersonel == null)
            {
                 TempData["Error"] = "Personel bulunamadi.";
                 return RedirectToAction("Index");
            }

            // 3. Yetki Kontrolleri (MATRIX)
            
            // Kural 1: Hiç kimse kendini silemez.
            if (silinecekPersonel.PersonelId == currentUserId)
            {
                TempData["Error"] = "Kendi hesabinizi silemezsiniz.";
                return RedirectToAction("Index");
            }

            // Rolleri belirle
            var currentUserRole = User.FindFirstValue(ClaimTypes.Role); // Admin, Yönetici, Editör, Kullanici
            var targetUserRole = silinecekPersonel.SistemRol?.Ad; // Admin, Yönetici, Editör, Kullanici

            // Kural 2: Editör ve Kullanici kimseyi silemez (This is now handled by [Authorize(Roles = "Admin,Yönetici")] )
            // if (currentUserRole == "Editör" || currentUserRole == "Kullanici")
            // {
            //      return Forbid();
            // }

            // Kural 3: Yönetici -> Admin'i SILEMEZ
            if (currentUserRole == "Yönetici" && targetUserRole == "Admin")
            {
                TempData["Error"] = "Yöneticiler, Admin hesabini silemez.";
                return RedirectToAction("Index");
            }

             // Kural 4: Admin -> Baska Admin'i SILEMEZ
            if (currentUserRole == "Admin" && targetUserRole == "Admin")
            {
                TempData["Error"] = "Adminler diger Admin hesaplarini silemez.";
                return RedirectToAction("Index");
            }

            // Kural 5: Yönetici -> Diger Yöneticiyi SILEBILIR (Engel yok) - This is now blocked by the new Yönetici check above if it's not their own account.
            
            // --- Iliskili Kayitlari Temizle/Güncelle ---
            
            // 1. Gelen Bildirimleri SIL (Çünkü AliciPersonelId zorunlu alan)
            await _context.Bildirimler.Where(b => b.AliciPersonelId == id).ExecuteDeleteAsync();

            // 2. Giden Bildirimlerin Gönderenini NULL yap (Eger gönderen silinirse bildirim kalsin ama gönderen anonim olsun)
            await _context.Bildirimler.Where(b => b.GonderenPersonelId == id)
                .ExecuteUpdateAsync(setters => setters.SetProperty(b => b.GonderenPersonelId, (int?)null));

            // 3. Sistem Loglarinin PersonelId'sini NULL yap
            await _context.SistemLoglar.Where(l => l.PersonelId == id)
                .ExecuteUpdateAsync(setters => setters.SetProperty(l => l.PersonelId, (int?)null));

            // 4. Toplu Bildirim / Bildirim Gonderen Zinciri (TopluBildirim -> Restrict -> BildirimGonderen -> Cascade -> Personel)
            // Personel silinince BildirimGonderen silinmek ister, ama TopluBildirim buna izin vermez.
            // Bu yüzden önce kullanicinin gönderdigi toplu bildirimleri bulup silmeliyiz.
            // "BirimGonderenler" tablosunda bu personele ait kayit var mi?
            var bildirimGonderenIds = await _context.BildirimGonderenler
                .Where(bg => bg.PersonelId == silinecekPersonel.PersonelId)
                .Select(bg => bg.Id)
                .ToListAsync();

            if (bildirimGonderenIds.Any())
            {
                // Bu gönderenlere ait toplu bildirimleri sil
                await _context.TopluBildirimler
                    .Where(tb => bildirimGonderenIds.Contains(tb.GonderenId))
                    .ExecuteDeleteAsync();
            }

            // --- Silme Islemi ---
            DeletePhotoFile(silinecekPersonel.FotografYolu);
            
            _context.Personeller.Remove(silinecekPersonel);
            await _context.SaveChangesAsync();

            // LOG
            await _logService.LogAsync("Silme", $"Personel silindi: {silinecekPersonel.Ad} {silinecekPersonel.Soyad} ({silinecekPersonel.TcKimlikNo})", null, $"Silen: {User.Identity?.Name ?? "Bilinmiyor"}");

            TempData["Success"] = "Personel basariyla silindi.";
            
            return RedirectToAction("Index");
        }

        [HttpPost]
        // [ValidateAntiForgeryToken] // AJAX JSON calls need header setup, disabling for bulk action convenience/speed fix.
        [Authorize(Roles = "Admin,Yönetici")]
        public async Task<IActionResult> TopluSil([FromBody] List<int> ids)
        {
            // Security Check for Manager: Managers cannot perform bulk delete at all.
            if (User.IsInRole("Yönetici"))
            {
                return Json(new { success = false, message = "Yetki Hatasi: Toplu silme yetkiniz bulunmamaktadir." });
            }

            if (ids == null || !ids.Any())
            {
                return Json(new { success = false, message = "Hiçbir kayit seçilmedi." });
            }

            // 1. Mevcut (Islemi Yapan) Kullaniciyi Bul
            var currentUserIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(currentUserIdStr) || !int.TryParse(currentUserIdStr, out int currentUserId))
            {
                // Yetkisiz veya oturum düsmüs
                 return Json(new { success = false, message = "Oturum süreniz dolmus." });
            }
            
            // Rolleri belirle
            var currentUserRole = User.FindFirstValue(ClaimTypes.Role); // Admin, Yönetici, Editör, Kullanici
            
            // Kural: Editör ve Kullanici kimseyi silemez
            if (currentUserRole == "Editör" || currentUserRole == "Kullanici")
            {
                 return Json(new { success = false, message = "Silme yetkiniz yok." });
            }

            int deletedCount = 0;
            int skippedCount = 0;
            string lastError = ""; // Initialize error message capture

            // 2. Silinecek Personelleri Bul (Rolleri ile)
            var silinecekPersoneller = await _context.Personeller.Include(p => p.SistemRol).Where(p => ids.Contains(p.PersonelId)).ToListAsync();

            foreach (var silinecekPersonel in silinecekPersoneller)
            {
                 // Kural 1: Hiç kimse kendini silemez.
                if (silinecekPersonel.PersonelId == currentUserId)
                {
                    skippedCount++;
                    continue;
                }

                var targetUserRole = silinecekPersonel.SistemRol?.Ad;

                // Kural 3: Yönetici -> Admin'i SILEMEZ
                if (currentUserRole == "Yönetici" && targetUserRole == "Admin")
                {
                    skippedCount++;
                    continue;
                }

                 // Kural 4: Admin -> Baska Admin'i SILEMEZ
                if (currentUserRole == "Admin" && targetUserRole == "Admin")
                {
                    skippedCount++;
                    continue;
                }

                // --- Iliskili Kayitlari Temizle/Güncelle ---
                
                try
                {
                    // 1. Görev Sahibi (Creator) Olan Kayitlari Null Yap (Restrict engeli)
                    // Gorev.PersonelId nullable oldugu için null yapabiliriz.
                    await _context.Gorevler.Where(g => g.PersonelId == silinecekPersonel.PersonelId)
                        .ExecuteUpdateAsync(setters => setters.SetProperty(g => g.PersonelId, (int?)null));

                    // 2. Gelen Bildirimleri SIL (AliciPersonelId Restrict)
                    await _context.Bildirimler.Where(b => b.AliciPersonelId == silinecekPersonel.PersonelId).ExecuteDeleteAsync();

                    // 3. Giden Bildirimlerin Gönderenini NULL yap (GonderenPersonelId Restrict)
                    await _context.Bildirimler.Where(b => b.GonderenPersonelId == silinecekPersonel.PersonelId)
                        .ExecuteUpdateAsync(setters => setters.SetProperty(b => b.GonderenPersonelId, (int?)null));

                    // 4. Sistem Loglarinin PersonelId'sini NULL yap
                    await _context.SistemLoglar.Where(l => l.PersonelId == silinecekPersonel.PersonelId)
                        .ExecuteUpdateAsync(setters => setters.SetProperty(l => l.PersonelId, (int?)null));

                    // 5. Toplu Bildirim / Bildirim Gonderen Zinciri (TopluBildirim -> Restrict -> BildirimGonderen -> Cascade -> Personel)
                    // Personel silinince BildirimGonderen silinmek ister, ama TopluBildirim buna izin vermez.
                    // Bu yüzden önce kullanicinin gönderdigi toplu bildirimleri bulup silmeliyiz.
                    // "BirimGonderenler" tablosunda bu personele ait kayit var mi?
                    var bildirimGonderenIds = await _context.BildirimGonderenler
                        .Where(bg => bg.PersonelId == silinecekPersonel.PersonelId)
                        .Select(bg => bg.Id)
                        .ToListAsync();

                    if (bildirimGonderenIds.Any())
                    {
                        // Bu gönderenlere ait toplu bildirimleri sil
                        await _context.TopluBildirimler
                            .Where(tb => bildirimGonderenIds.Contains(tb.GonderenId))
                            .ExecuteDeleteAsync();
                    }

                    // --- Silme Islemi ---
                    
                    // Fotograf Silme
                     if (!string.IsNullOrEmpty(silinecekPersonel.FotografYolu))
                    {
                        var webRoot = _hostEnvironment.WebRootPath;
                        var path = Path.Combine(webRoot, silinecekPersonel.FotografYolu.TrimStart('/').Replace("/", "\\"));
                        if (System.IO.File.Exists(path))
                        {
                            System.IO.File.Delete(path);
                        }
                    }

                    _context.Personeller.Remove(silinecekPersonel);
                    // Her döngüde save yapip hatayi yakalayalim ki hangisinin basarisiz oldugunu anlayabilelim (veya batch save disarida)
                    // Ancak burada transaction mantigi yoksa partial delete olabilir. 
                    // Güvenlik ve tutarlilik için SaveChanges'i loop içinde yapiyoruz simdilik (hata yönetimi için).
                    await _context.SaveChangesAsync(); 
                    deletedCount++;
                } 
                catch (Exception ex)
                {
                    // Inner exception'i logla veya debug et
                    var innerMsg = ex.InnerException?.Message ?? ex.Message;
                    lastError = innerMsg; // Capture the last error
                    skippedCount++;
                    // Isterseniz hatayi bir yerde toplayip döndürebilirsiniz ancak simdilik skippedCount yeterli.
                }
            }

            // SaveChanges loop içinde yapildigi için burada tekrar çagirmaya gerek yok (veya batch yapilacaksa yukarida remove deyip burada save denirdi)
            // Biz loop içi try-catch tercih ettik ki bir hata tüm islemi durdurmasin.

            if (deletedCount > 0)
            {
                 // LOG 
                await _logService.LogAsync("Toplu Silme", $"{deletedCount} personel silindi. ({skippedCount} atlandi)", null, $"Silen: {User.Identity?.Name ?? "Bilinmiyor"}");
            }

            if (deletedCount == 0 && skippedCount > 0)
            {
                // If NOTHING was deleted and we had failures, return failure with the error message.
                return Json(new { success = false, message = "Silme islemi basarisiz: " + lastError });
            }

            var message = $"{deletedCount} personel basariyla silindi.";
            if(skippedCount > 0) message += $" ({skippedCount} kayit hata/yetki nedeniyle silinemedi)";

            return Json(new { success = true, message = message });
        }

        [HttpGet("/Personel/Detay/{id:int}")]
        public async Task<IActionResult> Detay(int id)
        {
            // Security Check
            // Admin/Yönetici: All Access
            // Editör/Kullanici: Only Own Data
            if (!User.IsInRole("Admin") && !User.IsInRole("Yönetici"))
            {
                var currentUserId = User.FindFirst("PersonelId")?.Value;
                if (currentUserId != null && currentUserId != id.ToString())
                {
                     return RedirectToAction("AccessDenied", "Account");
                }
            }

            var personel = await _context.Personeller
                .Include(p => p.GorevliIl)
                .Include(p => p.Brans)
                .Include(p => p.KadroIl) // New
                .Include(p => p.KadroIlce) // New
                .Include(p => p.PersonelYazilimlar).ThenInclude(py => py.Yazilim)
                .Include(p => p.PersonelUzmanliklar).ThenInclude(pu => pu.Uzmanlik)
                .Include(p => p.PersonelGorevTurleri).ThenInclude(pg => pg.GorevTuru)
                .Include(p => p.PersonelIsNitelikleri).ThenInclude(pi => pi.IsNiteligi)
                .Include(p => p.PersonelIsNitelikleri).ThenInclude(pi => pi.IsNiteligi)
                .Include(p => p.SistemRol) // Include SistemRol
                .Include(p => p.PersonelKurumsalRolAtamalari).ThenInclude(pk => pk.KurumsalRol)
                .Include(p => p.PersonelKurumsalRolAtamalari).ThenInclude(pk => pk.Koordinatorluk!).ThenInclude(k => k.Teskilat)
                .Include(p => p.PersonelKurumsalRolAtamalari).ThenInclude(pk => pk.Komisyon!).ThenInclude(k => k.Koordinatorluk!).ThenInclude(ko => ko.Teskilat)
                .Include(p => p.PersonelTeskilatlar).ThenInclude(pt => pt.Teskilat)
                .Include(p => p.PersonelKoordinatorlukler).ThenInclude(pk => pk.Koordinatorluk).ThenInclude(k => k.Teskilat)
                .Include(p => p.PersonelKomisyonlar).ThenInclude(pk => pk.Komisyon).ThenInclude(k => k.Koordinatorluk).ThenInclude(ko => ko.Teskilat)
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
                TcKimlikNo = !string.IsNullOrEmpty(personel.TcKimlikNo) && personel.TcKimlikNo.Length >= 4 
                    ? personel.TcKimlikNo.Substring(0, 2) + "*******" + personel.TcKimlikNo.Substring(personel.TcKimlikNo.Length - 2) 
                    : personel.TcKimlikNo,
                Telefon = personel.Telefon,
                Eposta = personel.Eposta,
                Cinsiyet = personel.PersonelCinsiyet ? "Kadin" : "Erkek",
                GorevliIl = personel.GorevliIl?.Ad ?? string.Empty,
                Brans = personel.Brans?.Ad ?? string.Empty,
                KadroIl = personel.KadroIl?.Ad,
                KadroIlce = personel.KadroIlce?.Ad,
                KadroKurum = personel.KadroKurum,
                AktifMi = personel.AktifMi,
                FotografYolu = personel.FotografYolu,
                CreatedAt = personel.CreatedAt,
                Yazilimlar = personel.PersonelYazilimlar.Select(py => py.Yazilim.Ad).ToList(),
                Uzmanliklar = personel.PersonelUzmanliklar.Select(pu => pu.Uzmanlik.Ad).ToList(),
                GorevTurleri = personel.PersonelGorevTurleri.Select(pg => pg.GorevTuru.Ad).ToList(),
                IsNitelikleri = personel.PersonelIsNitelikleri.Select(pi => pi.IsNiteligi.Ad).ToList(),

                SistemRol = personel.SistemRol?.Ad ?? string.Empty,
            };

            // Aggregate Roles Logic (Unified with Frontend)
            // Aggregate Roles Logic (Optimized for Simple Text Display)
            var coveredKomIds = new HashSet<int>();
            var coveredKoordIds = new HashSet<int>();
            var coveredTesIds = new HashSet<int>();

            // 1. Explicit Roles
            foreach (var r in personel.PersonelKurumsalRolAtamalari)
            {
                var roleName = r.KurumsalRol.Ad;
                string fullString = roleName;

                if (r.Komisyon != null)
                {
                    var kom = r.Komisyon;
                    var koord = kom.Koordinatorluk;
                    var tes = koord?.Teskilat;

                    string path = "";
                    if (tes != null) path += tes.Ad + " ";
                    if (koord != null) path += koord.Ad + " ";
                    path += kom.Ad + " ";

                    fullString = path + roleName;
                    
                    if (r.KomisyonId.HasValue)
                    {
                        coveredKomIds.Add(r.KomisyonId.Value);
                    }
                    if (kom.KoordinatorlukId != 0) coveredKoordIds.Add(kom.KoordinatorlukId);
                    if (koord != null && koord.TeskilatId != 0) coveredTesIds.Add(koord.TeskilatId);
                }
                else if (r.Koordinatorluk != null)
                {
                    var koord = r.Koordinatorluk;
                    var tes = koord.Teskilat;

                    string path = "";
                    if (tes != null) path += tes.Ad + " ";
                    path += koord.Ad + " ";
                    
                    fullString = path + roleName;

                    if (r.KoordinatorlukId.HasValue)
                    {
                        coveredKoordIds.Add(r.KoordinatorlukId.Value);
                    }
                    if (koord.TeskilatId != 0) coveredTesIds.Add(koord.TeskilatId);
                }
                
                model.KurumsalRoller.Add(new RoleDisplayModel
                {
                    Title = fullString,
                    Subtitle = string.Empty, // No subtitle needed
                    ColorClass = "primary"
                });
            }

            // 2. Implicit Komisyon Memberships
            foreach (var pk in personel.PersonelKomisyonlar)
            {
                if (coveredKomIds.Contains(pk.KomisyonId)) continue;

                var kom = pk.Komisyon;
                var koord = kom.Koordinatorluk;
                var tes = koord?.Teskilat;

                string fullString = "";
                if (tes != null) fullString += tes.Ad + " ";
                if (koord != null) fullString += koord.Ad + " ";
                fullString += kom.Ad;

                model.KurumsalRoller.Add(new RoleDisplayModel
                {
                    Title = $"{fullString} (Rolü Yok)", 
                    Subtitle = string.Empty,
                    ColorClass = "danger" // Red for warning
                });

                if (kom.KoordinatorlukId != 0) 
                {
                    coveredKoordIds.Add(kom.KoordinatorlukId);
                    if (koord != null && koord.TeskilatId != 0) coveredTesIds.Add(koord.TeskilatId);
                }
            }

            // 3. Implicit Koordinatorluk Memberships
            foreach (var pk in personel.PersonelKoordinatorlukler)
            {
                if (coveredKoordIds.Contains(pk.KoordinatorlukId)) continue;
                
                var koord = pk.Koordinatorluk;
                var tes = koord.Teskilat;
                
                string fullString = "";
                if (tes != null) fullString += tes.Ad + " ";
                fullString += koord.Ad;
                
                model.KurumsalRoller.Add(new RoleDisplayModel
                {
                    Title = $"{fullString} (Rolü Yok)",
                    Subtitle = string.Empty,
                    ColorClass = "danger"
                });

                if (koord.TeskilatId != 0) coveredTesIds.Add(koord.TeskilatId);
            }

            // 4. Implicit Teskilat Memberships
            foreach (var pt in personel.PersonelTeskilatlar)
            {
                if (coveredTesIds.Contains(pt.TeskilatId)) continue;

                model.KurumsalRoller.Add(new RoleDisplayModel
                {
                    Title = $"{pt.Teskilat.Ad} (Rolü Yok)",
                    Subtitle = string.Empty,
                    ColorClass = "danger"
                });
            }

            return View(model);
        }

        private Task FillIndexLookupsAsync(LookupListsViewModel model, PersonelIndexFilterViewModel? filter = null)
        {
            return _personelLookupService.FillIndexLookupsAsync(model, filter);
        }

        private async Task FillLookupListsAsync(PersonelEkleViewModel model)
        {
            try
            {
                var hierarchy = await _personelLookupService.FillFormLookupsAsync(model);
                ViewData["AllKoordinatorlukler"] = hierarchy.AllKoordinatorlukler;
                ViewData["AllKomisyonlar"] = hierarchy.AllKomisyonlar;
            }
            catch (Exception ex)
            {
                ViewData["AllKoordinatorlukler"] = new List<PersonelHierarchyItemDto>();
                ViewData["AllKomisyonlar"] = new List<PersonelHierarchyKomisyonItemDto>();
                Debug.WriteLine("Error fetching lookup data: " + ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> CheckPassword(int id, string password)
        {
            var personel = await _context.Personeller.FindAsync(id);
            if(personel == null) return Json(new { success = false, message = "Personel bulunamadi." });

            // Check correctness
            bool isValid = _passwordService.VerifyPassword(password, personel.SifreHash, personel.SifreSalt);
            return Json(new { success = isValid });
        }

        [HttpGet]
        public async Task<IActionResult> CheckDuplicates(int? id, string tc, string email)
        {
            var result = await _personelLookupService.CheckDuplicatesAsync(id, tc, email);
            return Json(new { tcExists = result.TcExists, emailExists = result.EmailExists });
        }
        // --- Cascading Filter APIs ---

        [HttpGet]
        public async Task<IActionResult> GetKoordinatorlukNames(string teskilatAd)
        {
            return Json(await _personelLookupService.GetKoordinatorlukNamesAsync(teskilatAd));
        }

        [HttpGet]
        public async Task<IActionResult> GetKomisyonNames(string koordinatorlukAd)
        {
            return Json(await _personelLookupService.GetKomisyonNamesAsync(koordinatorlukAd));
        }
        [HttpGet]
        public async Task<IActionResult> GetKoordinatorluklerByTeskilat(int teskilatId)
        {
             var list = await _personelLookupService.GetKoordinatorluklerByTeskilatAsync(teskilatId);
             return Json(list.Select(k => new { id = k.Id, text = k.Ad }));
        }

        [HttpGet]
        public async Task<IActionResult> GetIlceler(int ilId)
        {
            var ilceler = await _personelLookupService.GetIlceLookupItemsAsync(ilId);
            
            return Json(ilceler.Select(x => new { id = x.Id, ad = x.Ad }));
        }

        public async Task<IActionResult> GetKomisyonlarByKoordinatorluk(int koordinatorlukId)
        {
             var list = await _personelLookupService.GetKomisyonlarByKoordinatorlukAsync(koordinatorlukId);
             return Json(list.Select(k => new { id = k.Id, text = k.Ad }));
        }

        [HttpGet]
        public async Task<IActionResult> GetPersonellerByKomisyon(int komisyonId)
        {
             var list = await _personelLookupService.GetPersonellerByKomisyonAsync(komisyonId);
             return Json(list.Select(p => new { id = p.Id, text = p.Ad }));
        }



    }
}

