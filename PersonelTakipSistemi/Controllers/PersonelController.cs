using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PersonelTakipSistemi.Data;
using PersonelTakipSistemi.Models;
using PersonelTakipSistemi.ViewModels;
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

        public PersonelController(TegmPersonelTakipDbContext context, IWebHostEnvironment hostEnvironment, IMemoryCache memoryCache, INotificationService notificationService)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _memoryCache = memoryCache;
            _notificationService = notificationService;
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
            var personeller = await _context.Personeller
                .Include(p => p.PersonelTeskilatlar).ThenInclude(pt => pt.Teskilat)
                .Include(p => p.PersonelKoordinatorlukler).ThenInclude(pk => pk.Koordinatorluk)
                .Include(p => p.PersonelKomisyonlar).ThenInclude(pk => pk.Komisyon)
                .Include(p => p.PersonelKomisyonlar).ThenInclude(pk => pk.Komisyon)
                .Include(p => p.PersonelKurumsalRolAtamalari).ThenInclude(pkr => pkr.KurumsalRol)
                .Include(p => p.SistemRol) // Added Include
                .Where(p => p.AktifMi)
                .OrderBy(p => p.Ad).ThenBy(p => p.Soyad)
                .ToListAsync();

            // 2. Map to ViewModel
            var rowViewModels = personeller.Select(p => new PersonelYetkiRowViewModel
            {
                PersonelId = p.PersonelId,
                AdSoyad = $"{p.Ad} {p.Soyad}",
                FotografYolu = p.FotografYolu,
                SistemRol = p.SistemRol?.Ad,
                
                // Extract Names for Chips
                TeskilatAdlari = p.PersonelTeskilatlar.Select(pt => pt.Teskilat.Ad).ToList(),
                KoordinatorlukAdlari = p.PersonelKoordinatorlukler.Select(pk => pk.Koordinatorluk.Ad).ToList(),
                KomisyonAdlari = p.PersonelKomisyonlar.Select(pk => pk.Komisyon.Ad).ToList(),
                KurumsalRolAdlari = p.PersonelKurumsalRolAtamalari.Select(ra => ra.KurumsalRol.Ad).Distinct().ToList()
            }).ToList();

            // 3. Prepare View Model
            var model = new YetkilendirmeIndexViewModel
            {
                Personeller = rowViewModels,
                TeskilatList = await _context.Teskilatlar.Select(t => new SelectListItem { Value = t.TeskilatId.ToString(), Text = t.Ad }).ToListAsync(),
                KurumsalRolList = await _context.KurumsalRoller.Select(r => new SelectListItem { Value = r.KurumsalRolId.ToString(), Text = r.Ad }).ToListAsync(),
                SistemRolList = await _context.SistemRoller.OrderBy(r => r.Ad).Select(r => new SelectListItem { Value = r.Ad, Text = r.Ad }).ToListAsync(),
                KomisyonList = await _context.Komisyonlar.Select(k => new SelectListItem { Value = k.Ad, Text = k.Ad }).Distinct().ToListAsync()
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
                    ContextAd = x.Koordinatorluk != null ? x.Koordinatorluk.Ad : (x.Komisyon != null ? x.Komisyon.Ad : "Genel")
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
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AddTeskilat(int personelId, int teskilatId)
        {
            if (await _context.PersonelTeskilatlar.AnyAsync(x => x.PersonelId == personelId && x.TeskilatId == teskilatId)) return Ok();

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
            }
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AddKoordinatorluk(int personelId, int koordinatorlukId)
        {
            if (await _context.PersonelKoordinatorlukler.AnyAsync(x => x.PersonelId == personelId && x.KoordinatorlukId == koordinatorlukId)) return Ok();
            
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
            if (await _context.PersonelKomisyonlar.AnyAsync(x => x.PersonelId == personelId && x.KomisyonId == komisyonId)) return Ok();

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

            // Avoid Duplicate Generic Roles?
            // "Personel" role for same context? Maybe allow duplicates or check unique?
            // Let's assume Unique (PersonelId, RolId, Context)
            if (!await _context.PersonelKurumsalRolAtamalari.AnyAsync(x => 
                x.PersonelId == personelId && 
                x.KurumsalRolId == kurumsalRolId && 
                x.KoordinatorlukId == koordinatorlukId && 
                x.KomisyonId == komisyonId))
            {
                _context.PersonelKurumsalRolAtamalari.Add(assignment);
            }
            
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
            }
            return Ok();
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
                            UpdatedAt = null,
                            SistemRolId = 4 // Default: Kullanıcı
                        };

                        _context.Personeller.Add(personel);
                        await _context.SaveChangesAsync(); // Get ID

                        AddRelations(personel.PersonelId, model);
                        await _context.SaveChangesAsync();

                        // Hoşgeldin Bildirimi (System Sender)
                        await _notificationService.CreateAsync(
                            aliciId: personel.PersonelId,
                            gonderenId: null, // System
                            baslik: "Hoşgeldiniz!",
                            aciklama: "Temel Eğitim Genel Müdürlüğü Personel Takip Sistemine Hoşgeldiniz!",
                            tip: "Sistem"
                        );

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
            
            // --- Silme İşlemi ---
            DeletePhotoFile(silinecekPersonel.FotografYolu);
            
            _context.Personeller.Remove(silinecekPersonel);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Personel başarıyla silindi.";
            
            return RedirectToAction("Index");
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
                .Include(p => p.PersonelYazilimlar).ThenInclude(py => py.Yazilim)
                .Include(p => p.PersonelUzmanliklar).ThenInclude(pu => pu.Uzmanlik)
                .Include(p => p.PersonelGorevTurleri).ThenInclude(pg => pg.GorevTuru)
                .Include(p => p.PersonelIsNitelikleri).ThenInclude(pi => pi.IsNiteligi)
                .Include(p => p.SistemRol) // Include SistemRol
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
                IsNitelikleri = personel.PersonelIsNitelikleri.Select(pi => pi.IsNiteligi.Ad).ToList(),
                SistemRol = personel.SistemRol?.Ad // Updated
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
    }
}
