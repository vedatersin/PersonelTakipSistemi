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
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;

using PersonelTakipSistemi.Services;

namespace PersonelTakipSistemi.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class PersonelController : Controller
    {
        private readonly TegmPersonelTakipDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IMemoryCache _memoryCache;
        private readonly INotificationService _notificationService;
        private readonly ILogService _logService;
        private readonly IExcelService _excelService;

        public PersonelController(TegmPersonelTakipDbContext context, IWebHostEnvironment hostEnvironment, IMemoryCache memoryCache, INotificationService notificationService, ILogService logService, IExcelService excelService)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _memoryCache = memoryCache;
            _notificationService = notificationService;
            _logService = logService;
            _excelService = excelService;
        }

        private int CurrentUserId => int.Parse(User.FindFirst("PersonelId")?.Value ?? "0");

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
        [Microsoft.AspNetCore.Authorization.Authorize(Roles = "Admin,Yönetici")] // Only Admin/Manager
        public async Task<IActionResult> Yetkilendirme()
        {
            // 1. Fetch Personnel with Relations
            // 1. Fetch Personnel with Relations
            var personeller = await _context.Personeller
                .Include(p => p.PersonelTeskilatlar).ThenInclude(pt => pt.Teskilat)
                .Include(p => p.PersonelKoordinatorlukler).ThenInclude(pk => pk.Koordinatorluk)
                .Include(p => p.PersonelKomisyonlar).ThenInclude(pk => pk.Komisyon).ThenInclude(k => k.Koordinatorluk).ThenInclude(koord => koord.Il)
                .Include(p => p.PersonelKurumsalRolAtamalari).ThenInclude(pkr => pkr.KurumsalRol)
                .Include(p => p.SistemRol)
                .Include(p => p.Brans) // New
                .Include(p => p.PersonelYazilimlar).ThenInclude(py => py.Yazilim) // New
                .Include(p => p.PersonelUzmanliklar).ThenInclude(pu => pu.Uzmanlik) // New
                .Include(p => p.PersonelGorevTurleri).ThenInclude(pg => pg.GorevTuru) // New
                .Include(p => p.PersonelIsNitelikleri).ThenInclude(pi => pi.IsNiteligi) // New
                // .Where(p => p.AktifMi) -- Removed to show passive personnel
                .OrderBy(p => p.Ad).ThenBy(p => p.Soyad)
                .ToListAsync();

            // 2. Map to ViewModel
            var rowViewModels = personeller.Select(p => new PersonelYetkiRowViewModel
            {
                PersonelId = p.PersonelId,
                AdSoyad = $"{p.Ad} {p.Soyad}",
                FotografYolu = p.FotografYolu,
                SistemRol = p.SistemRol?.Ad,
                AktifMi = p.AktifMi,
                
                // Extract Names for Chips
                TeskilatAdlari = p.PersonelTeskilatlar.Select(pt => pt.Teskilat.Ad).ToList(),
                KoordinatorlukAdlari = p.PersonelKoordinatorlukler.Select(pk => pk.Koordinatorluk.Ad).ToList(),
                KomisyonAdlari = p.PersonelKomisyonlar.Select(pk => 
                    pk.Komisyon.BagliMerkezKoordinatorlukId != null && pk.Komisyon.Koordinatorluk?.Il != null
                    ? $"{pk.Komisyon.Koordinatorluk.Il.Ad} Komisyonu"
                    : pk.Komisyon.Ad
                ).ToList(),
                KurumsalRolAdlari = p.PersonelKurumsalRolAtamalari.Select(ra => ra.KurumsalRol.Ad).Distinct().ToList(),
                
                // New Data for Filtering
                Brans = p.Brans?.Ad,
                Yazilimlar = p.PersonelYazilimlar.Select(py => py.Yazilim.Ad).ToList(),
                Uzmanliklar = p.PersonelUzmanliklar.Select(pu => pu.Uzmanlik.Ad).ToList(),
                GorevTurleri = p.PersonelGorevTurleri.Select(pg => pg.GorevTuru.Ad).ToList(),
                IsNitelikleri = p.PersonelIsNitelikleri.Select(pi => pi.IsNiteligi.Ad).ToList()
            }).ToList();

            // 3. Prepare View Model
            var model = new YetkilendirmeIndexViewModel
            {
                Personeller = rowViewModels,
                TeskilatList = await _context.Teskilatlar.Select(t => new SelectListItem { Value = t.TeskilatId.ToString(), Text = t.Ad }).ToListAsync(),
                KurumsalRolList = await _context.KurumsalRoller.Select(r => new SelectListItem { Value = r.KurumsalRolId.ToString(), Text = r.Ad }).ToListAsync(),
                SistemRolList = await _context.SistemRoller.OrderBy(r => r.Ad).Select(r => new SelectListItem { Value = r.Ad, Text = r.Ad }).ToListAsync(),
                KomisyonList = await _context.Komisyonlar.Include(k => k.Koordinatorluk).ThenInclude(k => k.Il)
                    .Select(k => k.BagliMerkezKoordinatorlukId != null && k.Koordinatorluk != null && k.Koordinatorluk.Il != null
                        ? $"{k.Koordinatorluk.Il.Ad} Komisyonu"
                        : k.Ad).Distinct().Select(ad => new SelectListItem { Value = ad, Text = ad }).ToListAsync(),
                KoordinatorlukList = await _context.Koordinatorlukler.Select(k => new SelectListItem { Value = k.Ad, Text = k.Ad }).Distinct().ToListAsync(),
                
                // New Filter Lists
                BransList = await _context.Branslar.Select(x => new SelectListItem { Value = x.Ad, Text = x.Ad }).ToListAsync(),
                YazilimList = await _context.Yazilimlar.Select(x => new SelectListItem { Value = x.Ad, Text = x.Ad }).ToListAsync(),
                UzmanlikList = await _context.Uzmanliklar.Select(x => new SelectListItem { Value = x.Ad, Text = x.Ad }).ToListAsync(),
                GorevTuruList = await _context.GorevTurleri.Select(x => new SelectListItem { Value = x.Ad, Text = x.Ad }).ToListAsync(),
                IsNiteligiList = await _context.IsNitelikleri.Select(x => new SelectListItem { Value = x.Ad, Text = x.Ad }).ToListAsync()
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetYetkilendirmeData(int id)
        {
            var p = await _context.Personeller
                .Include(x => x.PersonelTeskilatlar).ThenInclude(pt => pt.Teskilat)
                .Include(x => x.PersonelKoordinatorlukler).ThenInclude(pk => pk.Koordinatorluk)
                .Include(x => x.PersonelKomisyonlar).ThenInclude(pk => pk.Komisyon)
                .Include(x => x.PersonelKurumsalRolAtamalari).ThenInclude(pkr => pkr.KurumsalRol)
                .Include(x => x.SistemRol) // Added Include
                // Include Contexts for RolAtamalari Display
                .Include(x => x.PersonelKurumsalRolAtamalari).ThenInclude(pkr => pkr.Koordinatorluk)
                .Include(x => x.PersonelKurumsalRolAtamalari).ThenInclude(pkr => pkr.Komisyon)
                .FirstOrDefaultAsync(x => x.PersonelId == id);

            if (p == null) return NotFound();

            var model = new PersonelYetkiDetailViewModel
            {
                PersonelId = p.PersonelId,
                AdSoyad = $"{p.Ad} {p.Soyad}",
                FotografYolu = p.FotografYolu,
                SistemRol = p.SistemRol?.Ad,
                AktifMi = p.AktifMi,

                // Selected IDs
                SelectedTeskilatIds = p.PersonelTeskilatlar.Select(x => x.TeskilatId).ToList(),
                SelectedKoordinatorlukIds = p.PersonelKoordinatorlukler.Select(x => x.KoordinatorlukId).ToList(),
                SelectedKomisyonIds = p.PersonelKomisyonlar.Select(x => x.KomisyonId).ToList(),

                // Assignments for Chips
                TeskilatAssignments = p.PersonelTeskilatlar.Select(x => new AssignmentViewModel { Id = x.TeskilatId, Ad = x.Teskilat.Ad }).ToList(),
                KoordinatorlukAssignments = p.PersonelKoordinatorlukler.Select(x => new AssignmentViewModel { Id = x.KoordinatorlukId, Ad = x.Koordinatorluk.Ad }).ToList(),
                KomisyonAssignments = p.PersonelKomisyonlar.Select(x => new AssignmentViewModel { Id = x.KomisyonId, Ad = x.Komisyon.Ad }).ToList(),
                KurumsalRolAssignments = p.PersonelKurumsalRolAtamalari.Select(x => new RoleAssignmentViewModel
                {
                    AssignmentId = x.Id,
                    KurumsalRolId = x.KurumsalRolId,
                    RolAd = x.KurumsalRol.Ad,
                    ContextAd = x.Koordinatorluk != null ? x.Koordinatorluk.Ad : (x.Komisyon != null ? x.Komisyon.Ad : "Genel"),
                    KoordinatorlukId = x.KoordinatorlukId,
                    KomisyonId = x.KomisyonId
                }).ToList(),

                // Populate Lookups
                AllTeskilatlar = await _context.Teskilatlar.Select(x => new LookupItemViewModel { Id = x.TeskilatId, Ad = x.Ad }).ToListAsync(),
                AllKoordinatorlukler = await _context.Koordinatorlukler.Select(x => new LookupItemViewModel { Id = x.KoordinatorlukId, Ad = x.Ad, ParentId = x.TeskilatId }).ToListAsync(),
                AllKomisyonlar = await _context.Komisyonlar.Select(x => new LookupItemViewModel { Id = x.KomisyonId, Ad = x.Ad, ParentId = x.KoordinatorlukId }).ToListAsync()
            };

            return Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> SetSistemRol(int id, string rol)
        {
            var personel = await _context.Personeller.FindAsync(id);
            if (personel == null) return NotFound();

            var targetRole = await _context.SistemRoller.FirstOrDefaultAsync(r => r.Ad == rol);
            if(targetRole == null) return BadRequest("Geçersiz rol.");

            personel.SistemRolId = targetRole.SistemRolId;
            await _context.SaveChangesAsync();

            // 5. BİLDİRİM GÖNDER
            try 
            {
                var targetP = await _context.Personeller.FindAsync(id); // Use 'id' from method parameter
                if (targetP != null)
                {
                     await _notificationService.CreateAsync( // Changed to CreateAsync as per existing usage
                        aliciId: targetP.PersonelId, 
                        gonderenId: null, // System
                        baslik: "Yetkilendirme bildirimi", 
                        aciklama: $"Sayın {targetP.Ad} {targetP.Soyad}, yetkilendirme ayarlarınız güncellenmiştir.", 
                        tip: "Yetki",
                        refType: "Personel",
                        refId: targetP.PersonelId
                    );

                    // LOG
                    await _logService.LogAsync("Yetkilendirme", $"Sistem rolü güncellendi: {targetP.Ad} {targetP.Soyad}", targetP.PersonelId, $"Yeni Rol: {rol}");
                }
            }
            catch(Exception) { }

            return Ok(); // Original method returned Ok()
        }

        [HttpPost]
        public async Task<IActionResult> AddTeskilat(int personelId, int teskilatId)
        {
            if (await _context.PersonelTeskilatlar.AnyAsync(x => x.PersonelId == personelId && x.TeskilatId == teskilatId)) return BadRequest("Personel zaten bu teşkilata ekli.");

            _context.PersonelTeskilatlar.Add(new PersonelTeskilat { PersonelId = personelId, TeskilatId = teskilatId });
            await _context.SaveChangesAsync();

            // Notification
            var t = await _context.Teskilatlar.FindAsync(teskilatId);
            await _notificationService.CreateAsync(
                aliciId: personelId,
                gonderenId: CurrentUserId,
                baslik: "Teşkilat ataması güncellendi",
                aciklama: $"{User.Identity?.Name} tarafından {t?.Ad} teşkilatı eklendi.",
                tip: "KurumsalAtama",
                refType: "Teskilat",
                refId: teskilatId
            );

            await _logService.LogAsync("Atama", $"Teşkilat eklendi: {t?.Ad}", personelId, $"Teşkilat ID: {teskilatId}");

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> RemoveTeskilat(int personelId, int teskilatId)
        {
            var pt = await _context.PersonelTeskilatlar.FirstOrDefaultAsync(x => x.PersonelId == personelId && x.TeskilatId == teskilatId);
            if (pt != null)
            {
                _context.PersonelTeskilatlar.Remove(pt);
                
                // Cleanup: Remove Coordinators and Commissions belonging to this Teskilat
                var koordsToRemove = await _context.PersonelKoordinatorlukler
                    .Where(x => x.PersonelId == personelId && x.Koordinatorluk.TeskilatId == teskilatId)
                    .ToListAsync();
                
                foreach (var k in koordsToRemove)
                {
                    // Call logic to remove coordinator (which handles commissions and roles)
                    await RemoveKoordinatorlukLogic(personelId, k.KoordinatorlukId);
                }
                
                await _context.SaveChangesAsync();

                // Notification
                var t = await _context.Teskilatlar.FindAsync(teskilatId);
                await _notificationService.CreateAsync(
                    aliciId: personelId,
                    gonderenId: CurrentUserId,
                    baslik: "Teşkilat ataması güncellendi",
                    aciklama: $"{User.Identity?.Name} tarafından {t?.Ad} teşkilatı kaldırıldı.",
                    tip: "KurumsalAtama",
                    refType: "Teskilat",
                    refId: teskilatId
                );

                await _logService.LogAsync("Atama", $"Teşkilat çıkarıldı: {t?.Ad}", personelId, $"Teşkilat ID: {teskilatId}");
            }
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AddKoordinatorluk(int personelId, int koordinatorlukId)
        {
            if (await _context.PersonelKoordinatorlukler.AnyAsync(x => x.PersonelId == personelId && x.KoordinatorlukId == koordinatorlukId)) return BadRequest("Personel zaten bu koordinatörlüğe ekli.");
            
            // Ensure Parent Teskilat is added?
            // Optionally we can auto-add the parent Teskilat if missing, or assume UI handles it.
            // Let's safe-guard:
            var koord = await _context.Koordinatorlukler.FindAsync(koordinatorlukId);
            if (koord != null)
            {
                if (!await _context.PersonelTeskilatlar.AnyAsync(x => x.PersonelId == personelId && x.TeskilatId == koord.TeskilatId))
                {
                    _context.PersonelTeskilatlar.Add(new PersonelTeskilat { PersonelId = personelId, TeskilatId = koord.TeskilatId });
                }
            }

            _context.PersonelKoordinatorlukler.Add(new PersonelKoordinatorluk { PersonelId = personelId, KoordinatorlukId = koordinatorlukId });
            await _context.SaveChangesAsync();

            // Notification
            var k = await _context.Koordinatorlukler.FindAsync(koordinatorlukId);
            await _notificationService.CreateAsync(
                aliciId: personelId,
                gonderenId: CurrentUserId,
                baslik: "Koordinatörlük ataması güncellendi",
                aciklama: $"{k?.Ad} eklendi.",
                tip: "KurumsalAtama",
                refType: "Koordinatorluk",
                refId: koordinatorlukId
            );

            await _logService.LogAsync("Atama", $"Koordinatörlük eklendi: {k?.Ad}", personelId, $"Koordinatörlük ID: {koordinatorlukId}");

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> RemoveKoordinatorluk(int personelId, int koordinatorlukId)
        {
            await RemoveKoordinatorlukLogic(personelId, koordinatorlukId);
            await _context.SaveChangesAsync();

            // Notification
            var k = await _context.Koordinatorlukler.FindAsync(koordinatorlukId);
            await _notificationService.CreateAsync(
                aliciId: personelId,
                gonderenId: CurrentUserId,
                baslik: "Koordinatörlük ataması güncellendi",
                aciklama: $"{k?.Ad} kaldırıldı.",
                tip: "KurumsalAtama",
                refType: "Koordinatorluk",
                refId: koordinatorlukId
            );

            await _logService.LogAsync("Atama", $"Koordinatörlük çıkarıldı: {k?.Ad}", personelId, $"Koordinatörlük ID: {koordinatorlukId}");

            return Ok();
        }

        private async Task RemoveKoordinatorlukLogic(int personelId, int koordinatorlukId)
        {
            var pk = await _context.PersonelKoordinatorlukler.FirstOrDefaultAsync(x => x.PersonelId == personelId && x.KoordinatorlukId == koordinatorlukId);
            if (pk != null)
            {
                _context.PersonelKoordinatorlukler.Remove(pk);

                // Cleanup: Remove Commissions belonging to this Coordinator
                var komsToRemove = await _context.PersonelKomisyonlar
                    .Where(x => x.PersonelId == personelId && x.Komisyon.KoordinatorlukId == koordinatorlukId)
                    .ToListAsync();

                foreach (var k in komsToRemove)
                {
                    await RemoveKomisyonLogic(personelId, k.KomisyonId);
                }

                // Cleanup: Remove Roles assigned to this context
                var rolesToRemove = await _context.PersonelKurumsalRolAtamalari
                    .Where(x => x.PersonelId == personelId && x.KoordinatorlukId == koordinatorlukId)
                    .ToListAsync();
                _context.PersonelKurumsalRolAtamalari.RemoveRange(rolesToRemove);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddKomisyon(int personelId, int komisyonId)
        {
            if (await _context.PersonelKomisyonlar.AnyAsync(x => x.PersonelId == personelId && x.KomisyonId == komisyonId)) return BadRequest("Personel zaten bu komisyona ekli.");

            // Ensure Parent Coordinator (and Teskilat) exists?
            var kom = await _context.Komisyonlar.Include(k => k.Koordinatorluk).FirstOrDefaultAsync(k => k.KomisyonId == komisyonId);
            if (kom != null)
            {
                // Add Koordinatorluk if missing
                if (!await _context.PersonelKoordinatorlukler.AnyAsync(x => x.PersonelId == personelId && x.KoordinatorlukId == kom.KoordinatorlukId))
                {
                     // Add Teskilat if missing (Logic inside AddKoord logic repeated or explicit)
                     // Cleanest: Check Teskilat too.
                     if (!await _context.PersonelTeskilatlar.AnyAsync(x => x.PersonelId == personelId && x.TeskilatId == kom.Koordinatorluk.TeskilatId))
                     {
                         _context.PersonelTeskilatlar.Add(new PersonelTeskilat { PersonelId = personelId, TeskilatId = kom.Koordinatorluk.TeskilatId });
                     }
                     _context.PersonelKoordinatorlukler.Add(new PersonelKoordinatorluk { PersonelId = personelId, KoordinatorlukId = kom.KoordinatorlukId });
                }
            }

            _context.PersonelKomisyonlar.Add(new PersonelKomisyon { PersonelId = personelId, KomisyonId = komisyonId });
            await _context.SaveChangesAsync();
            
            // Notification
            var k = await _context.Komisyonlar.FindAsync(komisyonId);
            await _notificationService.CreateAsync(
                aliciId: personelId,
                gonderenId: CurrentUserId,
                baslik: "Komisyon ataması güncellendi",
                aciklama: $"{k?.Ad} eklendi.",
                tip: "KurumsalAtama",
                refType: "Komisyon",
                refId: komisyonId
            );

            await _logService.LogAsync("Atama", $"Komisyon eklendi: {k?.Ad}", personelId, $"Komisyon ID: {komisyonId}");

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> RemoveKomisyon(int personelId, int komisyonId)
        {
            await RemoveKomisyonLogic(personelId, komisyonId);
            await RemoveKomisyonLogic(personelId, komisyonId);
            await _context.SaveChangesAsync();

            // Notification
            var k = await _context.Komisyonlar.FindAsync(komisyonId);
            await _notificationService.CreateAsync(
                aliciId: personelId,
                gonderenId: CurrentUserId,
                baslik: "Komisyon ataması güncellendi",
                aciklama: $"{k?.Ad} kaldırıldı.",
                tip: "KurumsalAtama",
                refType: "Komisyon",
                refId: komisyonId
            );

            await _logService.LogAsync("Atama", $"Komisyon çıkarıldı: {k?.Ad}", personelId, $"Komisyon ID: {komisyonId}");

            return Ok();
        }

        private async Task RemoveKomisyonLogic(int personelId, int komisyonId)
        {
            var pk = await _context.PersonelKomisyonlar.FirstOrDefaultAsync(x => x.PersonelId == personelId && x.KomisyonId == komisyonId);
            if (pk != null)
            {
                _context.PersonelKomisyonlar.Remove(pk);

                // Cleanup: Remove Roles assigned to this context
                var rolesToRemove = await _context.PersonelKurumsalRolAtamalari
                    .Where(x => x.PersonelId == personelId && x.KomisyonId == komisyonId)
                    .ToListAsync();
                _context.PersonelKurumsalRolAtamalari.RemoveRange(rolesToRemove);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddKurumsalRol(int personelId, int kurumsalRolId, int? koordinatorlukId, int? komisyonId, bool force = false)
        {
            var rol = await _context.KurumsalRoller.FindAsync(kurumsalRolId);
            if (rol == null) return BadRequest("Rol bulunamadı.");

            // Context Validation
            if (rol.Ad == "Komisyon Başkanı" && komisyonId == null) return BadRequest("Komisyon seçilmedi.");
            if ((rol.Ad == "İl Koordinatörü" || rol.Ad == "Genel Koordinatör") && koordinatorlukId == null) return BadRequest("Koordinatörlük seçilmedi.");

            // President Logic
            if (rol.Ad == "Komisyon Başkanı" && komisyonId.HasValue)
            {
                var kom = await _context.Komisyonlar.FindAsync(komisyonId.Value);
                if (kom.BaskanPersonelId != null && kom.BaskanPersonelId != personelId)
                {
                    if (!force)
                    {
                        var baskan = await _context.Personeller.FindAsync(kom.BaskanPersonelId);
                        return Json(new { success = false, warning = $"Bu komisyonun başkanı zaten {baskan?.Ad} {baskan?.Soyad}. Değiştirmek istiyor musunuz?" });
                    }
                    else
                    {
                        // Demote old president
                        var oldRole = await _context.PersonelKurumsalRolAtamalari
                            .FirstOrDefaultAsync(x => x.PersonelId == kom.BaskanPersonelId && x.KurumsalRolId == kurumsalRolId && x.KomisyonId == komisyonId);
                        if (oldRole != null) _context.PersonelKurumsalRolAtamalari.Remove(oldRole);
                    }
                }
                kom.BaskanPersonelId = personelId;
            }
            else if ((rol.Ad == "İl Koordinatörü" || rol.Ad == "Genel Koordinatör") && koordinatorlukId.HasValue)
            {
                var koord = await _context.Koordinatorlukler.FindAsync(koordinatorlukId.Value);
                 if (koord.BaskanPersonelId != null && koord.BaskanPersonelId != personelId)
                {
                    if (!force)
                    {
                        var baskan = await _context.Personeller.FindAsync(koord.BaskanPersonelId);
                         return Json(new { success = false, warning = $"Bu koordinatörlüğün başkanı zaten {baskan?.Ad} {baskan?.Soyad}. Değiştirmek istiyor musunuz?" });
                    }
                     else
                    {
                        // Demote old president
                        var oldRole = await _context.PersonelKurumsalRolAtamalari
                             .FirstOrDefaultAsync(x => x.PersonelId == koord.BaskanPersonelId && x.KurumsalRolId == kurumsalRolId && x.KoordinatorlukId == koordinatorlukId);
                        if (oldRole != null) _context.PersonelKurumsalRolAtamalari.Remove(oldRole);
                    }
                }
                 koord.BaskanPersonelId = personelId;
            }

            // Create Role Assignment
            var assignment = new PersonelKurumsalRolAtama
            {
                PersonelId = personelId,
                KurumsalRolId = kurumsalRolId,
                KoordinatorlukId = koordinatorlukId,
                KomisyonId = komisyonId,
                CreatedAt = DateTime.Now
            };

            // Duplicate Check
            if (await _context.PersonelKurumsalRolAtamalari.AnyAsync(x => 
                x.PersonelId == personelId && 
                x.KurumsalRolId == kurumsalRolId && 
                x.KoordinatorlukId == koordinatorlukId && 
                x.KomisyonId == komisyonId))
            {
                return BadRequest("Bu rol zaten tanımlı.");
            }

            // Auto-Registration to Units
            if (komisyonId.HasValue)
            {
                if (!await _context.PersonelKomisyonlar.AnyAsync(pk => pk.PersonelId == personelId && pk.KomisyonId == komisyonId.Value))
                {
                     _context.PersonelKomisyonlar.Add(new PersonelKomisyon { PersonelId = personelId, KomisyonId = komisyonId.Value });
                     
                     // Ensure Parents
                     var kom = await _context.Komisyonlar.Include(k => k.Koordinatorluk).FirstOrDefaultAsync(k => k.KomisyonId == komisyonId.Value);
                     if(kom != null) 
                     {
                         if (!await _context.PersonelKoordinatorlukler.AnyAsync(pk => pk.PersonelId == personelId && pk.KoordinatorlukId == kom.KoordinatorlukId))
                             _context.PersonelKoordinatorlukler.Add(new PersonelKoordinatorluk { PersonelId = personelId, KoordinatorlukId = kom.KoordinatorlukId });
                             
                         if (!await _context.PersonelTeskilatlar.AnyAsync(pt => pt.PersonelId == personelId && pt.TeskilatId == kom.Koordinatorluk.TeskilatId))
                             _context.PersonelTeskilatlar.Add(new PersonelTeskilat { PersonelId = personelId, TeskilatId = kom.Koordinatorluk.TeskilatId });
                     }
                }
            }
            else if (koordinatorlukId.HasValue)
            {
                if (!await _context.PersonelKoordinatorlukler.AnyAsync(pk => pk.PersonelId == personelId && pk.KoordinatorlukId == koordinatorlukId.Value))
                {
                     _context.PersonelKoordinatorlukler.Add(new PersonelKoordinatorluk { PersonelId = personelId, KoordinatorlukId = koordinatorlukId.Value });
                     
                     // Ensure Parent
                     var koord = await _context.Koordinatorlukler.FindAsync(koordinatorlukId.Value);
                     if (koord != null) 
                     {
                          if (!await _context.PersonelTeskilatlar.AnyAsync(pt => pt.PersonelId == personelId && pt.TeskilatId == koord.TeskilatId))
                             _context.PersonelTeskilatlar.Add(new PersonelTeskilat { PersonelId = personelId, TeskilatId = koord.TeskilatId });
                     }
                }
            }

            _context.PersonelKurumsalRolAtamalari.Add(assignment);
            
            await _context.SaveChangesAsync();

            // Notification
            if (rol != null)
            {
                 string contextName = "Tanımsız";
                 if (koordinatorlukId.HasValue) contextName = (await _context.Koordinatorlukler.FindAsync(koordinatorlukId.Value))?.Ad ?? "";
                 else if (komisyonId.HasValue) contextName = (await _context.Komisyonlar.FindAsync(komisyonId.Value))?.Ad ?? "";
                 else contextName = "Genel";

                 // Specific President Notification
                 if (rol.Ad == "Komisyon Başkanı" || rol.Ad == "İl Koordinatörü" || rol.Ad == "Genel Koordinatör")
                 {
                     await _notificationService.CreateAsync(personelId, CurrentUserId, "Başkanlık ataması", $"{contextName} için başkan/koordinatör olarak atandınız.", "BaskanDegisimi");
                     
                     // Todo: Old president notification could be handled here if we tracked the ID in logic...
                 }
                 else
                 {
                     await _notificationService.CreateAsync(
                         personelId, 
                         CurrentUserId, 
                         "Kurumsal rol ataması güncellendi", 
                         $"{rol.Ad} rolü {contextName} için eklendi.", 
                         "RolAtama"
                     );
                 }

                 await _logService.LogAsync("Kurumsal Rol", $"Kurumsal rol eklendi: {rol.Ad}", personelId, $"Kapsam: {contextName}");
            }

            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveKurumsalRol(int assignmentId)
        {
            var assignment = await _context.PersonelKurumsalRolAtamalari
                .Include(x => x.KurumsalRol)
                .FirstOrDefaultAsync(x => x.Id == assignmentId);
            
            if (assignment != null)
            {
                // Update Parent Table BaskanId if needed
                if (assignment.KurumsalRol.Ad == "Komisyon Başkanı" && assignment.KomisyonId.HasValue)
                {
                    var kom = await _context.Komisyonlar.FindAsync(assignment.KomisyonId.Value);
                    if (kom.BaskanPersonelId == assignment.PersonelId) kom.BaskanPersonelId = null;
                }
                else if ((assignment.KurumsalRol.Ad == "İl Koordinatörü" || assignment.KurumsalRol.Ad == "Genel Koordinatör") && assignment.KoordinatorlukId.HasValue)
                {
                     var koord = await _context.Koordinatorlukler.FindAsync(assignment.KoordinatorlukId.Value);
                     if (koord.BaskanPersonelId == assignment.PersonelId) koord.BaskanPersonelId = null;
                }

                _context.PersonelKurumsalRolAtamalari.Remove(assignment);
                await _context.SaveChangesAsync();
            }
            if (assignment != null)
            {
                // Logic already removed assignment
                // Notify
                await _notificationService.CreateAsync(
                     aliciId: assignment.PersonelId,
                     gonderenId: CurrentUserId,
                     baslik: "Kurumsal rol ataması güncellendi",
                     aciklama: $"{assignment.KurumsalRol.Ad} rolü kaldırıldı.",
                     tip: "RolAtama"
                 );

                 await _logService.LogAsync("Kurumsal Rol", $"Kurumsal rol çıkarıldı: {assignment.KurumsalRol.Ad}", assignment.PersonelId, null);
            }
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> DownloadTemplate()
        {
            var content = await _excelService.GeneratePersonelTemplateAsync();
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "PersonelYuklemeSablonu.xlsx");
        }

        [HttpPost]
        public async Task<IActionResult> ImportExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Json(new { success = false, message = "Dosya seçilmedi." });

            var result = await _excelService.ImportPersonelListAsync(file);
            
            if (result.errors.Any())
            {
                return Json(new { success = false, message = "Hatalar oluştu.", errors = result.errors });
            }

            return Json(new { success = true, count = result.personeller.Count, message = $"{result.personeller.Count} personel başarıyla eklendi." });
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

            // 2. Sayfalama Hazırlığı
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
            await FillIndexLookups(model.Lookups, filter);
            
            if (filter.KadroIlId.HasValue)
            {
                model.Lookups.KadroIlceler = await _context.Ilceler
                    .AsNoTracking()
                    .Where(x => x.IlId == filter.KadroIlId.Value)
                    .OrderBy(x => x.Ad)
                    .Select(x => new LookupItemVm { Id = x.IlceId, Ad = x.Ad })
                    .ToListAsync();
            }

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
                    SeciliIsNiteligiIdleri = new List<int>(),

                    SistemRolId = 4, // Default: Kullanıcı
                    IsAuthSkipped = false,
                    AktifMi = true // Default to Active
                };
                
                await FillLookupLists(cleanModel);
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
                .Include(p => p.PersonelKurumsalRolAtamalari).ThenInclude(pkr => pkr.Koordinatorluk).ThenInclude(k => k.Teskilat)
                .Include(p => p.PersonelKurumsalRolAtamalari).ThenInclude(pkr => pkr.Komisyon).ThenInclude(k => k.Koordinatorluk).ThenInclude(k => k.Teskilat)
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
                
                // İlişkili tabloları seçili olarak işaretle
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

            await FillLookupLists(model);
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
                            .Include(p => p.PersonelYazilimlar).ThenInclude(py => py.Yazilim)
                            .Include(p => p.PersonelUzmanliklar).ThenInclude(pu => pu.Uzmanlik)
                            .Include(p => p.PersonelGorevTurleri).ThenInclude(pg => pg.GorevTuru)
                            .Include(p => p.PersonelIsNitelikleri).ThenInclude(pi => pi.IsNiteligi)
                            .Include(p => p.PersonelIsNitelikleri).ThenInclude(pi => pi.IsNiteligi)
                            .Include(p => p.PersonelKurumsalRolAtamalari) // Fix: Include for deletion logic
                            .Include(p => p.PersonelKomisyonlar)
                            .Include(p => p.PersonelKoordinatorlukler)
                            .Include(p => p.PersonelTeskilatlar)
                            .Include(p => p.Brans)
                            .Include(p => p.GorevliIl)
                            .FirstOrDefaultAsync(p => p.PersonelId == model.PersonelId.Value);

                        if (personel == null) return NotFound();

                        // --- CHANGE DETECTION START ---
                        var changes = new List<string>();

                        // 1. Basic Info Changes
                        if (personel.Ad != model.Ad) changes.Add($"Ad: {personel.Ad} -> {model.Ad}");
                        if (personel.Soyad != model.Soyad) changes.Add($"Soyad: {personel.Soyad} -> {model.Soyad}");
                        if (personel.TcKimlikNo != model.TcKimlikNo) changes.Add($"TC: Değişti");
                        if (personel.Telefon != (model.Telefon ?? "")) changes.Add($"Telefon Değişti");
                        if (personel.Eposta != model.Eposta) changes.Add($"E-posta: {personel.Eposta} -> {model.Eposta}");
                        if (personel.GorevliIlId != model.GorevliIlId) 
                        {
                             var newIl = await _context.Iller.FindAsync(model.GorevliIlId);
                             changes.Add($"İl: {personel.GorevliIl?.Ad} -> {newIl?.Ad}");
                        }
                        if (personel.KadroIlId != model.KadroIlId)
                        {
                             var newKadroIl = await _context.Iller.FindAsync(model.KadroIlId);
                             changes.Add($"Kadro İli: {personel.KadroIl?.Ad} -> {newKadroIl?.Ad}");
                        }
                        if (personel.KadroIlceId != model.KadroIlceId)
                        {
                             var newKadroIlce = await _context.Ilceler.FindAsync(model.KadroIlceId);
                             changes.Add($"Kadro İlçesi: {personel.KadroIlce?.Ad} -> {newKadroIlce?.Ad}");
                        }
                        if (personel.BransId != model.BransId) 
                        {
                             var newBrans = await _context.Branslar.FindAsync(model.BransId);
                             changes.Add($"Branş: {personel.Brans?.Ad} -> {newBrans?.Ad}");
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
                        if (removedYazilim.Any()) changes.Add($"Çıkarılan Beceriler: {string.Join(", ", removedYazilim)}");

                        // Uzmanlik
                        var oldUzmanlik = personel.PersonelUzmanliklar.Select(x => x.Uzmanlik.Ad).ToList();
                        var newUzmanlik = new List<string>();
                        if (model.SeciliUzmanlikIdleri != null && model.SeciliUzmanlikIdleri.Any())
                            newUzmanlik = await _context.Uzmanliklar.Where(x => model.SeciliUzmanlikIdleri.Contains(x.UzmanlikId)).Select(x => x.Ad).ToListAsync();

                        var addedUzmanlik = newUzmanlik.Except(oldUzmanlik).ToList();
                        var removedUzmanlik = oldUzmanlik.Except(newUzmanlik).ToList();
                        if (addedUzmanlik.Any()) changes.Add($"Eklenen Uzmanlıklar: {string.Join(", ", addedUzmanlik)}");
                        if (removedUzmanlik.Any()) changes.Add($"Çıkarılan Uzmanlıklar: {string.Join(", ", removedUzmanlik)}");

                        // Gorev Turu
                        var oldGorev = personel.PersonelGorevTurleri.Select(x => x.GorevTuru.Ad).ToList();
                        var newGorev = new List<string>();
                        if (model.SeciliGorevTuruIdleri != null && model.SeciliGorevTuruIdleri.Any())
                            newGorev = await _context.GorevTurleri.Where(x => model.SeciliGorevTuruIdleri.Contains(x.GorevTuruId)).Select(x => x.Ad).ToListAsync();

                        var addedGorev = newGorev.Except(oldGorev).ToList();
                        var removedGorev = oldGorev.Except(newGorev).ToList();
                        if (addedGorev.Any()) changes.Add($"Eklenen Görev Türleri: {string.Join(", ", addedGorev)}");
                        if (removedGorev.Any()) changes.Add($"Çıkarılan Görev Türleri: {string.Join(", ", removedGorev)}");
                        
                        // Is Niteligi
                        var oldNitelik = personel.PersonelIsNitelikleri.Select(x => x.IsNiteligi.Ad).ToList();
                        var newNitelik = new List<string>();
                        if (model.SeciliIsNiteligiIdleri != null && model.SeciliIsNiteligiIdleri.Any())
                            newNitelik = await _context.IsNitelikleri.Where(x => model.SeciliIsNiteligiIdleri.Contains(x.IsNiteligiId)).Select(x => x.Ad).ToListAsync();

                        var addedNitelik = newNitelik.Except(oldNitelik).ToList();
                        var removedNitelik = oldNitelik.Except(newNitelik).ToList();
                        if (addedNitelik.Any()) changes.Add($"Eklenen İş Nitelikleri: {string.Join(", ", addedNitelik)}");
                        if (removedNitelik.Any()) changes.Add($"Çıkarılan İş Nitelikleri: {string.Join(", ", removedNitelik)}");

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
                        personel.AktifMi = model.AktifMi;
                        personel.SistemRolId = model.SistemRolId;
                        
                        // Fotoğraf Güncelleme
                        if (yeniFotoYolu != null) 
                        {
                            DeletePhotoFile(personel.FotografYolu); // Eskiyi sil
                            personel.FotografYolu = yeniFotoYolu;   // Yeniyi ata
                            changes.Add("Fotoğraf güncellendi");
                        }
                        
                        personel.UpdatedAt = DateTime.Now;

                        // Şifre Güncelleme (Secure Logic)
                        if (!string.IsNullOrEmpty(model.NewPassword))
                        {
                            bool isAdminOrManager = User.IsInRole("Admin") || User.IsInRole("Yönetici");
                            
                            // Kural: Admin/Yönetici değilse Eski Şifre sorulmalı.
                            // İstisna: İlk şifre değişimi ise (Varsayılan şifre kullanılıyorsa) sorulmaz.
                            if (!isAdminOrManager)
                            {
                                // Varsayılan şifre kontrolü (TC ilk 6 hane)
                                string defaultPass = personel.TcKimlikNo.Length >= 6 ? personel.TcKimlikNo.Substring(0, 6) : "123456";
                                bool isDefaultPassword = VerifyPasswordHash(defaultPass, personel.SifreHash, personel.SifreSalt);

                                if (!isDefaultPassword)
                                {
                                    if (string.IsNullOrEmpty(model.EskiSifre))
                                    {
                                        ModelState.AddModelError("EskiSifre", "Mevcut şifrenizi girmelisiniz.");
                                        TempData["Error"] = "Şifre değiştirme hatası: Mevcut şifrenizi giriniz.";
                                        TempData["OpenTab"] = "tab_password"; // Hata durumunda ilgili tabı aç
                                        await FillLookupLists(model);
                                        return View(model);
                                    }

                                    if (!VerifyPasswordHash(model.EskiSifre, personel.SifreHash, personel.SifreSalt))
                                    {
                                        ModelState.AddModelError("EskiSifre", "Mevcut şifreniz hatalı.");
                                        TempData["Error"] = "Şifre değiştirme hatası: Mevcut şifreniz hatalı.";
                                        TempData["OpenTab"] = "tab_password";
                                        await FillLookupLists(model);
                                        return View(model);
                                    }
                                }
                            }

                             CreatePasswordHash(model.NewPassword, out byte[] passwordHash, out byte[] passwordSalt);
                             personel.Sifre = model.NewPassword; 
                             personel.SifreHash = passwordHash;
                             personel.SifreSalt = passwordSalt;
                             changes.Add("Şifre değişti");
                        }

                        // İlişkileri Temizle
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

                        // İlişkileri Yeniden Ekle
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

                        // BİLDİRİM GÖNDER (Profil Güncelleme)
                        try 
                        {
                            await _notificationService.CreateAsync(
                                aliciId: personel.PersonelId, 
                                gonderenId: null, // System
                                baslik: "Profil bilgileri güncellemesi", 
                                aciklama: $"Sayın {personel.Ad} {personel.Soyad}, personel bilgileriniz güncellenmiştir.", 
                                tip: "Profil",
                                refType: "Personel",
                                refId: personel.PersonelId
                            );
                            
                            // LOG
                            string logDetail = changes.Any() ? string.Join(". ", changes) : "Değişiklik yapılmadı.";
                            // Truncate if too long (DB limit protection)
                            if (logDetail.Length > 400) logDetail = logDetail.Substring(0, 397) + "...";

                            await _logService.LogAsync("Guncelleme", $"Personel güncellendi: {personel.Ad} {personel.Soyad}", personel.PersonelId, logDetail);
                        }
                        catch (Exception) { }

                        TempData["Success"] = "Personel bilgileri güncellendi.";
                        return RedirectToAction("Index", new { highlightId = personel.PersonelId });
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
                            GorevliIlId = (int)model.GorevliIlId,
                            KadroIlId = model.KadroIlId,
                            KadroIlceId = model.KadroIlceId,
                            BransId = (int)model.BransId,
                            KadroKurum = model.KadroKurum ?? "",
                            AktifMi = model.AktifMi,
                            FotografYolu = yeniFotoYolu,
                            Sifre = autoPasword, // Plain text sync
                            SifreHash = passwordHash,
                            SifreSalt = passwordSalt,
                            CreatedAt = DateTime.Now,
                            UpdatedAt = null,

                            SistemRolId = (model.IsAuthSkipped || model.SistemRolId == null) ? 4 : model.SistemRolId.Value // Default to 4 if skipped or null
                        };

                        _context.Personeller.Add(personel);
                        await _context.SaveChangesAsync(); // Get ID

                        AddRelations(personel.PersonelId, model);
                        await _context.SaveChangesAsync();


                        // Handle Roles for Insert (Common Logic)
                        if (model.IsAuthSkipped)
                        {
                            // Ensure default role if skipped (already set in object initializer but sure)
                             personel.SistemRolId = 4; // Kullanıcı
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

                        // Hoşgeldin Bildirimi (System Sender)
                        await _notificationService.CreateAsync(
                            aliciId: personel.PersonelId,
                            gonderenId: null, // System
                            baslik: "Hoşgeldiniz!",
                            aciklama: "Temel Eğitim Genel Müdürlüğü Personel Takip Sistemine Hoşgeldiniz!",
                            tip: "Sistem"
                        );

                        // LOG
                        await _logService.LogAsync("Ekleme", $"Yeni personel eklendi: {personel.Ad} {personel.Soyad} ({personel.TcKimlikNo})", personel.PersonelId, $"Ekleyen: {User.Identity?.Name ?? "Bilinmiyor"}");

                        TempData["Success"] = $"Yeni personel kaydedildi. Başlangıç şifresi: {autoPasword}";
                        return RedirectToAction("Index", new { highlightId = personel.PersonelId });
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
                Debug.WriteLine($"Fotoğraf silinemedi: {ex.Message}");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Sil(int id)
        {
            // 1. Mevcut (İşlemi Yapan) Kullanıcıyı Bul
            var currentUserIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(currentUserIdStr) || !int.TryParse(currentUserIdStr, out int currentUserId))
            {
                return RedirectToAction("Login", "Account");
            }

            // 2. Silinecek Personeli Bul
            var silinecekPersonel = await _context.Personeller.Include(p => p.SistemRol).FirstOrDefaultAsync(p => p.PersonelId == id); // Include Role
            if (silinecekPersonel == null)
            {
                 TempData["Error"] = "Personel bulunamadı.";
                 return RedirectToAction("Index");
            }

            // 3. Yetki Kontrolleri (MATRIX)
            
            // Kural 1: Hiç kimse kendini silemez.
            if (silinecekPersonel.PersonelId == currentUserId)
            {
                TempData["Error"] = "Kendi hesabınızı silemezsiniz.";
                return RedirectToAction("Index");
            }

            // Rolleri belirle
            var currentUserRole = User.FindFirstValue(ClaimTypes.Role); // Admin, Yönetici, Editör, Kullanıcı
            var targetUserRole = silinecekPersonel.SistemRol?.Ad; // Admin, Yönetici, Editör, Kullanıcı

            // Kural 2: Editör ve Kullanıcı kimseyi silemez
            if (currentUserRole == "Editör" || currentUserRole == "Kullanıcı")
            {
                 // Controller seviyesinde ekstra koruma (zaten Authorize ile korunmalı ama view gizleme yetmez)
                 return Forbid();
            }

            // Kural 3: Yönetici -> Admin'i SİLEMEZ
            if (currentUserRole == "Yönetici" && targetUserRole == "Admin")
            {
                TempData["Error"] = "Yöneticiler, Admin hesabını silemez.";
                return RedirectToAction("Index");
            }

             // Kural 4: Admin -> Başka Admin'i SİLEMEZ
            if (currentUserRole == "Admin" && targetUserRole == "Admin")
            {
                TempData["Error"] = "Adminler diğer Admin hesaplarını silemez.";
                return RedirectToAction("Index");
            }

            // Kural 5: Yönetici -> Diğer Yöneticiyi SİLEBİLİR (Engel yok)
            
            // --- İlişkili Kayıtları Temizle/Güncelle ---
            
            // 1. Gelen Bildirimleri SİL (Çünkü AliciPersonelId zorunlu alan)
            await _context.Bildirimler.Where(b => b.AliciPersonelId == id).ExecuteDeleteAsync();

            // 2. Giden Bildirimlerin Gönderenini NULL yap (Eğer gönderen silinirse bildirim kalsın ama gönderen anonim olsun)
            await _context.Bildirimler.Where(b => b.GonderenPersonelId == id)
                .ExecuteUpdateAsync(setters => setters.SetProperty(b => b.GonderenPersonelId, (int?)null));

            // 3. Sistem Loglarının PersonelId'sini NULL yap
            await _context.SistemLoglar.Where(l => l.PersonelId == id)
                .ExecuteUpdateAsync(setters => setters.SetProperty(l => l.PersonelId, (int?)null));

            // --- Silme İşlemi ---
            DeletePhotoFile(silinecekPersonel.FotografYolu);
            
            _context.Personeller.Remove(silinecekPersonel);
            await _context.SaveChangesAsync();

            // LOG
            await _logService.LogAsync("Silme", $"Personel silindi: {silinecekPersonel.Ad} {silinecekPersonel.Soyad} ({silinecekPersonel.TcKimlikNo})", null, $"Silen: {User.Identity?.Name ?? "Bilinmiyor"}");

            TempData["Success"] = "Personel başarıyla silindi.";
            
            return RedirectToAction("Index");
        }

        [HttpPost]
        // [ValidateAntiForgeryToken] // AJAX JSON calls need header setup, disabling for bulk action convenience/speed fix.
        public async Task<IActionResult> TopluSil([FromBody] List<int> ids)
        {
            if (ids == null || !ids.Any())
            {
                return Json(new { success = false, message = "Hiçbir kayıt seçilmedi." });
            }

            // 1. Mevcut (İşlemi Yapan) Kullanıcıyı Bul
            var currentUserIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(currentUserIdStr) || !int.TryParse(currentUserIdStr, out int currentUserId))
            {
                // Yetkisiz veya oturum düşmüş
                 return Json(new { success = false, message = "Oturum süreniz dolmuş." });
            }
            
            // Rolleri belirle
            var currentUserRole = User.FindFirstValue(ClaimTypes.Role); // Admin, Yönetici, Editör, Kullanıcı
            
            // Kural: Editör ve Kullanıcı kimseyi silemez
            if (currentUserRole == "Editör" || currentUserRole == "Kullanıcı")
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

                // Kural 3: Yönetici -> Admin'i SİLEMEZ
                if (currentUserRole == "Yönetici" && targetUserRole == "Admin")
                {
                    skippedCount++;
                    continue;
                }

                 // Kural 4: Admin -> Başka Admin'i SİLEMEZ
                if (currentUserRole == "Admin" && targetUserRole == "Admin")
                {
                    skippedCount++;
                    continue;
                }

                // --- İlişkili Kayıtları Temizle/Güncelle ---
                
                try
                {
                    // 1. Görev Sahibi (Creator) Olan Kayıtları Null Yap (Restrict engeli)
                    // Gorev.PersonelId nullable olduğu için null yapabiliriz.
                    await _context.Gorevler.Where(g => g.PersonelId == silinecekPersonel.PersonelId)
                        .ExecuteUpdateAsync(setters => setters.SetProperty(g => g.PersonelId, (int?)null));

                    // 2. Gelen Bildirimleri SİL (AliciPersonelId Restrict)
                    await _context.Bildirimler.Where(b => b.AliciPersonelId == silinecekPersonel.PersonelId).ExecuteDeleteAsync();

                    // 3. Giden Bildirimlerin Gönderenini NULL yap (GonderenPersonelId Restrict)
                    await _context.Bildirimler.Where(b => b.GonderenPersonelId == silinecekPersonel.PersonelId)
                        .ExecuteUpdateAsync(setters => setters.SetProperty(b => b.GonderenPersonelId, (int?)null));

                    // 4. Sistem Loglarının PersonelId'sini NULL yap
                    await _context.SistemLoglar.Where(l => l.PersonelId == silinecekPersonel.PersonelId)
                        .ExecuteUpdateAsync(setters => setters.SetProperty(l => l.PersonelId, (int?)null));

                    // 5. Toplu Bildirim / Bildirim Gonderen Zinciri (TopluBildirim -> Restrict -> BildirimGonderen -> Cascade -> Personel)
                    // Personel silinince BildirimGonderen silinmek ister, ama TopluBildirim buna izin vermez.
                    // Bu yüzden önce kullanıcının gönderdiği toplu bildirimleri bulup silmeliyiz.
                    // "BirimGonderenler" tablosunda bu personele ait kayıt var mı?
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

                    // --- Silme İşlemi ---
                    
                    // Fotoğraf Silme
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
                    // Her döngüde save yapıp hatayı yakalayalım ki hangisinin başarısız olduğunu anlayabilelim (veya batch save dışarıda)
                    // Ancak burada transaction mantığı yoksa partial delete olabilir. 
                    // Güvenlik ve tutarlılık için SaveChanges'i loop içinde yapıyoruz şimdilik (hata yönetimi için).
                    await _context.SaveChangesAsync(); 
                    deletedCount++;
                } 
                catch (Exception ex)
                {
                    // Inner exception'ı logla veya debug et
                    var innerMsg = ex.InnerException?.Message ?? ex.Message;
                    lastError = innerMsg; // Capture the last error
                    skippedCount++;
                    // İsterseniz hatayı bir yerde toplayıp döndürebilirsiniz ancak şimdilik skippedCount yeterli.
                }
            }

            // SaveChanges loop içinde yapıldığı için burada tekrar çağırmaya gerek yok (veya batch yapılacaksa yukarıda remove deyip burada save denirdi)
            // Biz loop içi try-catch tercih ettik ki bir hata tüm işlemi durdurmasın.

            if (deletedCount > 0)
            {
                 // LOG 
                await _logService.LogAsync("Toplu Silme", $"{deletedCount} personel silindi. ({skippedCount} atlandı)", null, $"Silen: {User.Identity?.Name ?? "Bilinmiyor"}");
            }

            if (deletedCount == 0 && skippedCount > 0)
            {
                // If NOTHING was deleted and we had failures, return failure with the error message.
                return Json(new { success = false, message = "Silme işlemi başarısız: " + lastError });
            }

            var message = $"{deletedCount} personel başarıyla silindi.";
            if(skippedCount > 0) message += $" ({skippedCount} kayıt hata/yetki nedeniyle silinemedi)";

            return Json(new { success = true, message = message });
        }

        [HttpGet("/Personel/Detay/{id:int}")]
        public async Task<IActionResult> Detay(int id)
        {
            // Security Check
            // Admin/Yönetici: All Access
            // Editör/Kullanıcı: Only Own Data
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
                .Include(p => p.PersonelKurumsalRolAtamalari).ThenInclude(pk => pk.Koordinatorluk).ThenInclude(k => k.Teskilat)
                .Include(p => p.PersonelKurumsalRolAtamalari).ThenInclude(pk => pk.Komisyon).ThenInclude(k => k.Koordinatorluk).ThenInclude(ko => ko.Teskilat)
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
                Cinsiyet = personel.PersonelCinsiyet ? "Kadın" : "Erkek",
                GorevliIl = personel.GorevliIl.Ad,
                Brans = personel.Brans.Ad,
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

                SistemRol = personel.SistemRol?.Ad,
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
                    
                    coveredKomIds.Add(r.KomisyonId.Value);
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

                    coveredKoordIds.Add(r.KoordinatorlukId.Value);
                    if (koord.TeskilatId != 0) coveredTesIds.Add(koord.TeskilatId);
                }
                
                model.KurumsalRoller.Add(new RoleDisplayModel
                {
                    Title = fullString,
                    Subtitle = null, // No subtitle needed
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
                    Subtitle = null,
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
                    Subtitle = null,
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
                    Subtitle = null,
                    ColorClass = "danger"
                });
            }

            return View(model);
        }

        // Helper Method: Index Lookup Listelerini Doldurma
        private async Task FillIndexLookups(LookupListsViewModel model, PersonelIndexFilterViewModel filter = null)
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
             model.Branslar = await _context.Branslar.AsNoTracking().OrderBy(b => b.Ad).Select(b => new LookupItemVm { Id = b.BransId, Ad = b.Ad }).ToListAsync();
             model.Iller = await _context.Iller.AsNoTracking().OrderBy(i => i.Ad).Select(i => new LookupItemVm { Id = i.IlId, Ad = i.Ad }).ToListAsync();
             
             // Cascading Filter Lookups (Initial Population)
             model.Teskilatlar = await _context.Teskilatlar.AsNoTracking().OrderBy(t => t.Ad).Select(t => new LookupItemVm { Id = t.TeskilatId, Ad = t.Ad }).ToListAsync();
             
             if (filter != null && filter.TeskilatId.HasValue)
             {
                 model.Koordinatorlukler = await _context.Koordinatorlukler.AsNoTracking().Where(k => k.TeskilatId == filter.TeskilatId.Value).OrderBy(k => k.Ad).Select(k => new LookupItemVm { Id = k.KoordinatorlukId, Ad = k.Ad }).ToListAsync();
             }
             else
             {
                 model.Koordinatorlukler = new List<LookupItemVm>();
             }

             if (filter != null && filter.KoordinatorlukId.HasValue)
             {
                 var koms = await _context.Komisyonlar
                     .Include(k => k.Koordinatorluk).ThenInclude(koord => koord.Il)
                     .Where(x => (x.KoordinatorlukId == filter.KoordinatorlukId.Value || x.BagliMerkezKoordinatorlukId == filter.KoordinatorlukId.Value) && x.IsActive)
                     .ToListAsync();

                 model.Komisyonlar = koms.Select(k => new LookupItemVm { 
                     Id = k.KomisyonId, 
                     Ad = k.BagliMerkezKoordinatorlukId == filter.KoordinatorlukId.Value && k.Koordinatorluk?.Il != null ? $"{k.Koordinatorluk.Il.Ad} Komisyonu" : k.Ad 
                 }).OrderBy(x => x.Ad).ToList();
             }
             else
             {
                 model.Komisyonlar = new List<LookupItemVm>();
             }
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

                // Auth Lookups
                model.SistemRolleri = await _context.SistemRoller.AsNoTracking().Select(x => new LookupItemVm { Id = x.SistemRolId, Ad = x.Ad }).ToListAsync();
                model.KurumsalRoller = await _context.KurumsalRoller.AsNoTracking().Select(x => new LookupItemVm { Id = x.KurumsalRolId, Ad = x.Ad }).ToListAsync();
                model.Teskilatlar = await _context.Teskilatlar.AsNoTracking().Select(x => new LookupItemVm { Id = x.TeskilatId, Ad = x.Ad }).ToListAsync();

                // Preload Hierarchy for Client-Side Cascading (Refined)
                try {
                    // Using simple anonymous types to avoid EF Core complexity
                    var allKoords = await _context.Koordinatorlukler
                        .AsNoTracking()
                        .Select(x => new { id = x.KoordinatorlukId, ad = x.Ad, parentId = x.TeskilatId })
                        .ToListAsync();
                    
                    var allKoms = await _context.Komisyonlar
                        .AsNoTracking()
                        .Select(x => new { id = x.KomisyonId, ad = x.Ad, parentId = x.KoordinatorlukId })
                        .ToListAsync();
                    
                    this.ViewData["AllKoordinatorlukler"] = allKoords;
                    this.ViewData["AllKomisyonlar"] = allKoms;
                } catch(Exception ex) {
                    // Fallback to empty lists to avoid null ref in View, but define checking key
                     this.ViewData["AllKoordinatorlukler"] = new List<object>();
                     this.ViewData["AllKomisyonlar"] = new List<object>();
                     Debug.WriteLine("Hierarchy Load Error: " + ex.Message);
                }
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
            // Fix: Must use PBKDF2 to match CreatePasswordHash
            var computedHash = KeyDerivation.Pbkdf2(
                password: password,
                salt: storedSalt,
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 10000,
                numBytesRequested: 256 / 8);

            return computedHash.SequenceEqual(storedHash);
        }

        [HttpGet]
        public async Task<IActionResult> CheckPassword(int id, string password)
        {
            var personel = await _context.Personeller.FindAsync(id);
            if(personel == null) return Json(new { success = false, message = "Personel bulunamadı." });

            // Check correctness
            bool isValid = VerifyPasswordHash(password, personel.SifreHash, personel.SifreSalt);
            return Json(new { success = isValid });
        }

        [HttpGet]
        public async Task<IActionResult> CheckDuplicates(int? id, string tc, string email)
        {
            var isTcExists = false;
            var isEmailExists = false;

            if (id.HasValue && id.Value > 0)
            {
                // Edit Mode: Exclude current user
                isTcExists = await _context.Personeller.AnyAsync(x => x.TcKimlikNo == tc && x.PersonelId != id.Value);
                isEmailExists = await _context.Personeller.AnyAsync(x => x.Eposta == email && x.PersonelId != id.Value);
            }
            else
            {
                // New Mode
                isTcExists = await _context.Personeller.AnyAsync(x => x.TcKimlikNo == tc);
                isEmailExists = await _context.Personeller.AnyAsync(x => x.Eposta == email);
            }

            return Json(new { tcExists = isTcExists, emailExists = isEmailExists });
        }
        // --- Cascading Filter APIs ---

        [HttpGet]
        public async Task<IActionResult> GetKoordinatorlukNames(string teskilatAd)
        {
            if (string.IsNullOrEmpty(teskilatAd))
            {
                // If "All" selected, return all unique koordinatorluks
                var all = await _context.Koordinatorlukler
                    .Select(k => k.Ad)
                    .Distinct()
                    .OrderBy(x => x)
                    .ToListAsync();
                return Json(all);
            }

            var list = await _context.Koordinatorlukler
                .Where(k => k.Teskilat.Ad == teskilatAd)
                .Select(k => k.Ad)
                .Distinct()
                .OrderBy(x => x)
                .ToListAsync();

            return Json(list);
        }

        [HttpGet]
        public async Task<IActionResult> GetKomisyonNames(string koordinatorlukAd)
        {
            if (string.IsNullOrEmpty(koordinatorlukAd))
            {
                // If "All" selected, return all unique komisyons
                var all = await _context.Komisyonlar
                    .Include(k => k.Koordinatorluk).ThenInclude(k => k.Il)
                    .Select(k => k.BagliMerkezKoordinatorlukId != null && k.Koordinatorluk != null && k.Koordinatorluk.Il != null
                        ? $"{k.Koordinatorluk.Il.Ad} Komisyonu"
                        : k.Ad)
                    .Distinct()
                    .ToListAsync();
                
                return Json(all.OrderBy(x => x));
            }

            var list = await _context.Komisyonlar
                .Include(k => k.Koordinatorluk).ThenInclude(k => k.Il)
                .Include(k => k.BagliMerkezKoordinatorluk)
                .Where(k => k.IsActive && 
                            ((k.Koordinatorluk != null && k.Koordinatorluk.Ad == koordinatorlukAd) || 
                             (k.BagliMerkezKoordinatorluk != null && k.BagliMerkezKoordinatorluk.Ad == koordinatorlukAd)))
                .ToListAsync();

            var result = list.Select(k => 
                    k.BagliMerkezKoordinatorlukId != null && k.Koordinatorluk?.Il != null
                        ? $"{k.Koordinatorluk.Il.Ad} Komisyonu"
                        : k.Ad)
                .Distinct()
                .OrderBy(x => x)
                .ToList();

            return Json(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetKoordinatorluklerByTeskilat(int teskilatId)
        {
             var list = await _context.Koordinatorlukler
                 .Where(k => k.TeskilatId == teskilatId)
                 .OrderBy(k => k.Ad)
                 .Select(k => new { id = k.KoordinatorlukId, text = k.Ad })
                 .ToListAsync();
             return Json(list);
        }

        [HttpGet]
        public async Task<IActionResult> GetIlceler(int ilId)
        {
            var ilceler = await _context.Ilceler
                .AsNoTracking()
                .Where(x => x.IlId == ilId)
                .OrderBy(x => x.Ad)
                .Select(x => new { id = x.IlceId, ad = x.Ad })
                .ToListAsync();

            return Json(ilceler);
        }

        public async Task<IActionResult> GetKomisyonlarByKoordinatorluk(int koordinatorlukId)
        {
             var list = await _context.Komisyonlar
                 .Include(k => k.Koordinatorluk).ThenInclude(koord => koord.Il)
                 .Include(k => k.BagliMerkezKoordinatorluk)
                 .Where(k => (k.KoordinatorlukId == koordinatorlukId || k.BagliMerkezKoordinatorlukId == koordinatorlukId) && k.IsActive)
                 .ToListAsync();

             var result = list.Select(k => new { 
                     id = k.KomisyonId, 
                     text = k.BagliMerkezKoordinatorlukId == koordinatorlukId && k.Koordinatorluk?.Il != null
                            ? $"{k.Koordinatorluk.Il.Ad} Komisyonu"
                            : k.Ad 
                 })
                 .OrderBy(k => k.text)
                 .ToList();

             return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetPersonellerByKomisyon(int komisyonId)
        {
             var list = await _context.Personeller
                 .Where(p => p.AktifMi && p.PersonelKomisyonlar.Any(pk => pk.KomisyonId == komisyonId))
                 .OrderBy(p => p.Ad).ThenBy(p => p.Soyad)
                 .Select(p => new { id = p.PersonelId, text = p.Ad + " " + p.Soyad })
                 .ToListAsync();
                 
             return Json(list);
        }
        [HttpPost]
        [Authorize(Roles = "Admin,Yönetici")]
        public async Task<IActionResult> SaveYetkilendirme([FromBody] DTOs.PersonelYetkilendirmeSaveDto dto)
        {
            if (dto == null || dto.PersonelId <= 0) return BadRequest("Geçersiz veri.");

            var strategy = _context.Database.CreateExecutionStrategy();
            return await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    var personel = await _context.Personeller
                        .Include(p => p.PersonelTeskilatlar)
                        .Include(p => p.PersonelKoordinatorlukler)
                        .Include(p => p.PersonelKomisyonlar)
                        .Include(p => p.PersonelKurumsalRolAtamalari)
                        .Include(p => p.SistemRol)
                        .FirstOrDefaultAsync(p => p.PersonelId == dto.PersonelId);

                    if (personel == null) return NotFound("Personel bulunamadı.");

                    // 1. Sistem Rolü Update
                    if (!string.IsNullOrEmpty(dto.SistemRol))
                    {
                        var sistemRol = await _context.SistemRoller.FirstOrDefaultAsync(r => r.Ad == dto.SistemRol);
                        if (sistemRol != null)
                        {
                            personel.SistemRolId = sistemRol.SistemRolId;
                        }
                    }

                    // 2. Teşkilat Sync
                    // Remove deleted
                    var teskilatsToRemove = personel.PersonelTeskilatlar.Where(pt => !dto.TeskilatIds.Contains(pt.TeskilatId)).ToList();
                    _context.PersonelTeskilatlar.RemoveRange(teskilatsToRemove);
                    // Add new
                    foreach (var tid in dto.TeskilatIds)
                    {
                        if (!personel.PersonelTeskilatlar.Any(pt => pt.TeskilatId == tid))
                        {
                            _context.PersonelTeskilatlar.Add(new PersonelTeskilat { PersonelId = personel.PersonelId, TeskilatId = tid });
                        }
                    }

                    // 3. Koordinatörlük Sync
                    var koordsToRemove = personel.PersonelKoordinatorlukler.Where(pk => !dto.KoordinatorlukIds.Contains(pk.KoordinatorlukId)).ToList();
                    _context.PersonelKoordinatorlukler.RemoveRange(koordsToRemove);
                    foreach (var kid in dto.KoordinatorlukIds)
                    {
                        if (!personel.PersonelKoordinatorlukler.Any(pk => pk.KoordinatorlukId == kid))
                        {
                            _context.PersonelKoordinatorlukler.Add(new PersonelKoordinatorluk { PersonelId = personel.PersonelId, KoordinatorlukId = kid });
                        }
                    }

                    // 4. Komisyon Sync
                    var komsToRemove = personel.PersonelKomisyonlar.Where(pk => !dto.KomisyonIds.Contains(pk.KomisyonId)).ToList();
                    _context.PersonelKomisyonlar.RemoveRange(komsToRemove);
                    foreach (var kid in dto.KomisyonIds)
                    {
                        if (!personel.PersonelKomisyonlar.Any(pk => pk.KomisyonId == kid))
                        {
                            _context.PersonelKomisyonlar.Add(new PersonelKomisyon { PersonelId = personel.PersonelId, KomisyonId = kid });
                        }
                    }

                    // 5. Kurumsal Rol Atamaları Sync
                    _context.PersonelKurumsalRolAtamalari.RemoveRange(personel.PersonelKurumsalRolAtamalari);
                    
                    // Validation: Check for duplicates in the new list
                    var duplicates = dto.Gorevler
                        .GroupBy(x => new { x.KurumsalRolId, x.KoordinatorlukId, x.KomisyonId })
                        .Where(g => g.Count() > 1)
                        .Select(y => y.First())
                        .ToList();

                    if (duplicates.Any())
                    {
                        var roleIds = duplicates.Select(d => d.KurumsalRolId).Distinct().ToList();
                        var roleNames = await _context.KurumsalRoller.Where(r => roleIds.Contains(r.KurumsalRolId)).ToDictionaryAsync(r => r.KurumsalRolId, r => r.Ad);
                        var errs = duplicates.Select(d => roleNames.ContainsKey(d.KurumsalRolId) ? roleNames[d.KurumsalRolId] : "Rol").Distinct();
                        return BadRequest($"Aynı yetkiyi birden fazla kez ekleyemezsiniz: {string.Join(", ", errs)}");
                    }

                    foreach (var gorev in dto.Gorevler)
                    {
                        var atama = new PersonelKurumsalRolAtama
                        {
                            PersonelId = personel.PersonelId,
                            KurumsalRolId = gorev.KurumsalRolId,
                            KoordinatorlukId = gorev.KoordinatorlukId,
                            KomisyonId = gorev.KomisyonId,
                            CreatedAt = DateTime.Now
                        };
                        _context.PersonelKurumsalRolAtamalari.Add(atama);

                        if (gorev.KurumsalRolId == 2 && gorev.KomisyonId.HasValue) // Komisyon Başkanı
                        {
                            var komisyon = await _context.Komisyonlar.FindAsync(gorev.KomisyonId.Value);
                            if (komisyon != null)
                            {
                                komisyon.BaskanPersonelId = personel.PersonelId;
                                _context.Komisyonlar.Update(komisyon);
                            }
                        }
                        else if ((gorev.KurumsalRolId == 3 || gorev.KurumsalRolId == 4) && gorev.KoordinatorlukId.HasValue) // İl Koord or Genel Koord
                        {
                            var koord = await _context.Koordinatorlukler.FindAsync(gorev.KoordinatorlukId.Value);
                            if (koord != null)
                            {
                                 koord.BaskanPersonelId = personel.PersonelId;
                                 _context.Koordinatorlukler.Update(koord);
                            }
                        }
                    }

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    // 5. BİLDİRİM GÖNDER
                    try 
                    {
                        var targetP = await _context.Personeller.FindAsync(dto.PersonelId);
                        if (targetP != null)
                        {
                             await _notificationService.CreateAsync(
                                aliciId: targetP.PersonelId, 
                                gonderenId: null, // System
                                baslik: "Yetkilendirme bildirimi", 
                                aciklama: $"Sayın {targetP.Ad} {targetP.Soyad}, yetkilendirme ayarlarınız güncellenmiştir.", 
                                tip: "Yetki",
                                refType: "Personel",
                                refId: targetP.PersonelId
                            );
                            
                            // LOG
                            await _logService.LogAsync("Yetkilendirme", $"Yetkilendirme güncellendi: {targetP.Ad} {targetP.Soyad}", targetP.PersonelId, $"İşlemi Yapan: {User.Identity.Name}");
                        }
                    }
                    catch(Exception) { }

                    return await GetYetkilendirmeData(dto.PersonelId);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return StatusCode(500, "Kaydetme hatası: " + ex.Message);
                }
            });
        }

        [HttpGet]
        public async Task<IActionResult> FixTeskilatNames()
        {
            try
            {
                var merkez = await _context.Teskilatlar.FirstOrDefaultAsync(t => t.Ad.Contains("Merkez"));
                if (merkez != null) { merkez.Ad = "Merkez"; }

                var tasra = await _context.Teskilatlar.FirstOrDefaultAsync(t => t.Ad.Contains("Taşra"));
                if (tasra != null) { tasra.Ad = "Taşra"; }

                await _context.SaveChangesAsync();
                return Content("Teşkilat isimleri güncellendi: Merkez, Taşra");
            }
            catch (Exception ex) { return Content($"Hata: {ex.Message}"); }
        }



    }
}
