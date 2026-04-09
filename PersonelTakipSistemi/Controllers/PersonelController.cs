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
        private readonly IPersonelMaintenanceService _personelMaintenanceService;

        public PersonelController(TegmPersonelTakipDbContext context, IWebHostEnvironment hostEnvironment, INotificationService notificationService, ILogService logService, IExcelService excelService, IFileValidationService fileValidationService, IPasswordService passwordService, IPersonelLookupService personelLookupService, IPersonelAuthorizationService personelAuthorizationService, IPersonelAssignmentService personelAssignmentService, IPersonelMaintenanceService personelMaintenanceService)
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
            _personelMaintenanceService = personelMaintenanceService;
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
        public async Task<IActionResult> Index(PersonelIndexFilterViewModel filter, bool showAll = false, int? highlightId = null)
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

            var ordered = query.OrderByDescending(p => p.UpdatedAt ?? p.CreatedAt);
            if (showAll)
            {
                filter.Page = 1;
            }

            var paged = showAll
                ? ordered
                : ordered.Skip((filter.Page - 1) * filter.PageSize).Take(filter.PageSize);

            var results = await paged
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
                ShowAll = showAll,
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
        public async Task<IActionResult> ExportExcel(PersonelIndexFilterViewModel filter)
        {
            var query = _context.Personeller
                .Include(p => p.GorevliIl)
                .Include(p => p.Brans)
                .Include(p => p.PersonelYazilimlar).ThenInclude(py => py.Yazilim)
                .Include(p => p.PersonelUzmanliklar).ThenInclude(pu => pu.Uzmanlik)
                .Include(p => p.PersonelGorevTurleri).ThenInclude(pg => pg.GorevTuru)
                .Include(p => p.PersonelIsNitelikleri).ThenInclude(pi => pi.IsNiteligi)
                .AsNoTracking()
                .AsQueryable();

            // Same filtering logic as Index
            if (!string.IsNullOrEmpty(filter.SearchName))
            {
                var term = filter.SearchName.ToLower();
                query = query.Where(p => p.Ad.ToLower().Contains(term) || p.Soyad.ToLower().Contains(term));
            }

            if (!string.IsNullOrEmpty(filter.TcKimlikNo))
            {
                query = query.Where(p => p.TcKimlikNo.StartsWith(filter.TcKimlikNo));
            }

            if (filter.BransId.HasValue) query = query.Where(p => p.BransId == filter.BransId.Value);
            if (filter.GorevliIlId.HasValue) query = query.Where(p => p.GorevliIlId == filter.GorevliIlId.Value);
            if (filter.KadroIlId.HasValue) query = query.Where(p => p.KadroIlId == filter.KadroIlId.Value);
            if (filter.KadroIlceId.HasValue) query = query.Where(p => p.KadroIlceId == filter.KadroIlceId.Value);
            if (filter.DogumBaslangic.HasValue) query = query.Where(p => p.DogumTarihi >= filter.DogumBaslangic.Value);

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

            var rows = await query
                .OrderBy(p => p.Ad).ThenBy(p => p.Soyad)
                .Select(p => new
                {
                    Ad = p.Ad,
                    Soyad = p.Soyad,
                    Eposta = p.Eposta,
                    Brans = p.Brans.Ad,
                    GorevliIl = p.GorevliIl.Ad,
                    KadroIl = p.KadroIl != null ? p.KadroIl.Ad : "",
                    KadroIlce = p.KadroIlce != null ? p.KadroIlce.Ad : "",
                    AktifMi = p.AktifMi,
                    Yazilimlar = string.Join(", ", p.PersonelYazilimlar.Select(py => py.Yazilim.Ad)),
                    Uzmanliklar = string.Join(", ", p.PersonelUzmanliklar.Select(pu => pu.Uzmanlik.Ad)),
                    GorevTurleri = string.Join(", ", p.PersonelGorevTurleri.Select(pg => pg.GorevTuru.Ad)),
                    IsNitelikleri = string.Join(", ", p.PersonelIsNitelikleri.Select(pi => pi.IsNiteligi.Ad))
                })
                .ToListAsync();

            OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using var package = new OfficeOpenXml.ExcelPackage();
            var ws = package.Workbook.Worksheets.Add("Personel");

            string[] headers =
            [
                "Ad", "Soyad", "E-posta", "Branş", "Görevli İl", "Kadro İl", "Kadro İlçe", "Durum",
                "Yazılımlar", "Uzmanlıklar", "Görev Türleri", "İş Nitelikleri"
            ];

            for (int i = 0; i < headers.Length; i++)
            {
                ws.Cells[1, i + 1].Value = headers[i];
                ws.Cells[1, i + 1].Style.Font.Bold = true;
            }

            for (int i = 0; i < rows.Count; i++)
            {
                var r = rows[i];
                ws.Cells[i + 2, 1].Value = r.Ad;
                ws.Cells[i + 2, 2].Value = r.Soyad;
                ws.Cells[i + 2, 3].Value = r.Eposta;
                ws.Cells[i + 2, 4].Value = r.Brans;
                ws.Cells[i + 2, 5].Value = r.GorevliIl;
                ws.Cells[i + 2, 6].Value = r.KadroIl;
                ws.Cells[i + 2, 7].Value = r.KadroIlce;
                ws.Cells[i + 2, 8].Value = r.AktifMi ? "Aktif" : "Pasif";
                ws.Cells[i + 2, 9].Value = r.Yazilimlar;
                ws.Cells[i + 2, 10].Value = r.Uzmanliklar;
                ws.Cells[i + 2, 11].Value = r.GorevTurleri;
                ws.Cells[i + 2, 12].Value = r.IsNitelikleri;
            }

            ws.Cells[ws.Dimension.Address].AutoFitColumns();

            var content = await package.GetAsByteArrayAsync();
            var fileName = $"Personel_Filtreli_{DateTime.Now:yyyyMMdd_HHmm}.xlsx";
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }

        [HttpGet]
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
                .Include(p => p.SistemRol) // Include SistemRol
                .Include(p => p.PersonelKurumsalRolAtamalari).ThenInclude(pk => pk.KurumsalRol)
                .Include(p => p.PersonelKurumsalRolAtamalari).ThenInclude(pk => pk.Koordinatorluk!).ThenInclude(k => k.Teskilat)
                .Include(p => p.PersonelKurumsalRolAtamalari).ThenInclude(pk => pk.Komisyon!).ThenInclude(k => k.Koordinatorluk!).ThenInclude(ko => ko.Teskilat)
                .Include(p => p.PersonelTeskilatlar).ThenInclude(pt => pt.Teskilat)
                .Include(p => p.PersonelKoordinatorlukler).ThenInclude(pk => pk.Koordinatorluk).ThenInclude(k => k.Teskilat)
                .Include(p => p.PersonelKomisyonlar).ThenInclude(pk => pk.Komisyon).ThenInclude(k => k.Koordinatorluk).ThenInclude(ko => ko.Teskilat)
                .AsSplitQuery()
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

        public async Task<IActionResult> CheckPassword(int id, string password)
        {
            var personel = await _context.Personeller.FindAsync(id);
            if(personel == null) return Json(new { success = false, message = "Personel bulunamadi." });

            // Check correctness
            bool isValid = _passwordService.VerifyPassword(password, personel.SifreHash, personel.SifreSalt);
            return Json(new { success = isValid });
        }

        [HttpGet]
        public async Task<IActionResult> CheckDuplicates(int? id, string tc, string? email)
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

