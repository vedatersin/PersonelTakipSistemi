using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonelTakipSistemi.Data;
using PersonelTakipSistemi.Dtos;
using PersonelTakipSistemi.Services;
using System.Security.Claims;

namespace PersonelTakipSistemi.Controllers
{
    [Authorize(Roles = "Admin,Yönetici")]
    public class BildirimModuluController : Controller
    {
        private readonly INotificationService _notificationService;
        private readonly TegmPersonelTakipDbContext _context;
        private readonly ILogService _logService;

        public BildirimModuluController(INotificationService notificationService, TegmPersonelTakipDbContext context, ILogService logService)
        {
            _notificationService = notificationService;
            _context = context;
            _logService = logService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.TeskilatList = await _context.Teskilatlar.OrderBy(x => x.Ad).Select(x => new { Value = x.TeskilatId, Text = x.Ad }).ToListAsync();

            ViewBag.BransList = await _context.Branslar.OrderBy(x => x.Ad).Select(x => new { Value = x.Ad, Text = x.Ad }).ToListAsync(); // Using Name for Brans as per Yetkilendirme
            ViewBag.SistemRolList = await _context.SistemRoller.OrderBy(x => x.Ad).Select(x => new { Value = x.SistemRolId, Text = x.Ad }).ToListAsync();
            ViewBag.KurumsalRolList = await _context.KurumsalRoller.OrderBy(x => x.Ad).Select(x => new { Value = x.KurumsalRolId, Text = x.Ad }).ToListAsync();
            ViewBag.YazilimList = await _context.Yazilimlar.OrderBy(x => x.Ad).Select(x => new { Value = x.Ad, Text = x.Ad }).ToListAsync();
            ViewBag.UzmanlikList = await _context.Uzmanliklar.OrderBy(x => x.Ad).Select(x => new { Value = x.Ad, Text = x.Ad }).ToListAsync();
            ViewBag.GorevTuruList = await _context.GorevTurleri.OrderBy(x => x.Ad).Select(x => new { Value = x.Ad, Text = x.Ad }).ToListAsync();
            ViewBag.IsNiteligiList = await _context.IsNitelikleri.OrderBy(x => x.Ad).Select(x => new { Value = x.Ad, Text = x.Ad }).ToListAsync();

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetSenders()
        {
            // Get Current User ID
            int? currentUserId = null;
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int uid))
            {
                currentUserId = uid;
            }

            var senders = await _notificationService.GetAvailableSendersAsync(currentUserId);
            return Json(senders);
        }

        [HttpGet]
        public async Task<IActionResult> GetFilterData()
        {
            var iller = await _context.Iller.OrderBy(i => i.Ad).Select(i => new { Id = i.IlId, i.Ad }).ToListAsync();
            var koordinatorlukler = await _context.Koordinatorlukler.OrderBy(k => k.Ad).Select(k => new { k.KoordinatorlukId, k.Ad, k.TeskilatId }).ToListAsync();
            var komisyonlar = await _context.Komisyonlar.OrderBy(k => k.Ad).Select(k => new { k.KomisyonId, k.Ad, k.KoordinatorlukId }).ToListAsync();
            var sistemRoller = await _context.SistemRoller.OrderBy(r => r.Ad).Select(r => new { r.SistemRolId, r.Ad }).ToListAsync();
            var kurumsalRoller = await _context.KurumsalRoller.OrderBy(r => r.Ad).Select(r => new { r.KurumsalRolId, r.Ad }).ToListAsync();

            return Json(new { iller, koordinatorlukler, komisyonlar, sistemRoller, kurumsalRoller });
        }

        [HttpGet]
        public async Task<IActionResult> GetPersonelList(
            string? search, 
            int? teskilatId, 
            int? koordinatorlukId, 
            int? komisyonId, 
            int? sistemRolId, 
            int? kurumsalRolId,
            string? bransAd,
            string? yazilimAd,
            string? uzmanlikAd,
            string? gorevTuruAd,
            string? isNiteligiAd)
        {
            // Filter logic
            var query = _context.Personeller.AsQueryable();

            // Exclude Inactive
            // Exclude Inactive -- Removed to show passive users
            // query = query.Where(p => p.AktifMi);

            // Search by Name/Surname
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.Ad.Contains(search) || p.Soyad.Contains(search));
            }

            // Teşkilat
            if (teskilatId.HasValue)
            {
                query = query.Where(p => p.PersonelTeskilatlar.Any(pt => pt.TeskilatId == teskilatId));
            }

            // Koordinatörlük
            if (koordinatorlukId.HasValue)
            {
                query = query.Where(p => p.PersonelKoordinatorlukler.Any(pk => pk.KoordinatorlukId == koordinatorlukId));
            }

            // Komisyon
            if (komisyonId.HasValue)
            {
                query = query.Where(p => p.PersonelKomisyonlar.Any(pk => pk.KomisyonId == komisyonId));
            }

            // Sistem Rolü
            if (sistemRolId.HasValue)
            {
                query = query.Where(p => p.SistemRolId == sistemRolId);
            }

            // Kurumsal Rol
            if (kurumsalRolId.HasValue)
            {
                query = query.Where(p => p.PersonelKurumsalRolAtamalari.Any(pkr => pkr.KurumsalRolId == kurumsalRolId));
            }

            // Branş
            if (!string.IsNullOrEmpty(bransAd))
            {
                query = query.Where(p => p.Brans.Ad == bransAd);
            }

            // Yazılım
            if (!string.IsNullOrEmpty(yazilimAd))
            {
                query = query.Where(p => p.PersonelYazilimlar.Any(py => py.Yazilim.Ad == yazilimAd));
            }

            // Uzmanlık
            if (!string.IsNullOrEmpty(uzmanlikAd))
            {
                query = query.Where(p => p.PersonelUzmanliklar.Any(pu => pu.Uzmanlik.Ad == uzmanlikAd));
            }

            // Görev Türü
            if (!string.IsNullOrEmpty(gorevTuruAd))
            {
                query = query.Where(p => p.PersonelGorevTurleri.Any(pg => pg.GorevTuru.Ad == gorevTuruAd));
            }

            // İş Niteliği
            if (!string.IsNullOrEmpty(isNiteligiAd))
            {
                query = query.Where(p => p.PersonelIsNitelikleri.Any(pi => pi.IsNiteligi.Ad == isNiteligiAd));
            }



            var list = await query
                .Include(p => p.PersonelTeskilatlar).ThenInclude(pt => pt.Teskilat)
                .Include(p => p.PersonelKoordinatorlukler).ThenInclude(pk => pk.Koordinatorluk)
                .Include(p => p.PersonelKomisyonlar).ThenInclude(pk => pk.Komisyon)
                .Include(p => p.SistemRol)
                .Include(p => p.PersonelKurumsalRolAtamalari).ThenInclude(pkr => pkr.KurumsalRol)
                .Include(p => p.PersonelUzmanliklar).ThenInclude(pu => pu.Uzmanlik)
                .Include(p => p.GorevliIl) 
                .OrderBy(p => p.Ad)
                .Select(p => new
                {
                    p.PersonelId,
                    AdSoyad = p.Ad + " " + p.Soyad,
                    p.FotografYolu,
                    SistemRol = p.SistemRol != null ? p.SistemRol.Ad : "-",
                    KurumsalRoller = p.PersonelKurumsalRolAtamalari.Select(k => k.KurumsalRol.Ad).ToList(),
                    Teskilatlar = p.PersonelTeskilatlar.Select(t => t.Teskilat.Ad).ToList(),
                    Koordinatorlukler = p.PersonelKoordinatorlukler.Select(k => k.Koordinatorluk.Ad).ToList(),
                    Komisyonlar = p.PersonelKomisyonlar.Select(k => k.Komisyon.Ad).ToList(),
                    Uzmanliklar = p.PersonelUzmanliklar.Select(u => u.Uzmanlik.Ad).ToList(),

                    Sehir = p.GorevliIl.Ad,
                    AktifMi = p.AktifMi // Add this
                })
                .ToListAsync();

            return Json(list);
        }

        [HttpPost]
        public async Task<IActionResult> SendBulk([FromBody] BulkSendRequest request)
        {
            if (request == null || !request.RecipientIds.Any())
            {
                return BadRequest("En az bir kişi seçmelisiniz.");
            }

            if (string.IsNullOrEmpty(request.Baslik) || string.IsNullOrEmpty(request.Icerik))
            {
                return BadRequest("Başlık ve içerik zorunludur.");
            }

            int? gonderenPersonelId = null;
            if (request.SenderId == "System") gonderenPersonelId = null;
            else if (int.TryParse(request.SenderId, out int pid2)) gonderenPersonelId = pid2;

            DateTime? finalTime = null;
            if (!string.IsNullOrEmpty(request.ScheduledTime))
            {
                if (DateTime.TryParse(request.ScheduledTime, out DateTime dt))
                {
                    finalTime = dt;
                }
                else
                {
                    return BadRequest("Geçersiz tarih formatı.");
                }
            }

            try
            {
                await _notificationService.CreateBulkAsync(
                    gonderenPersonelId, 
                    request.RecipientIds, 
                    request.Baslik, 
                    request.Icerik, 
                    finalTime);

                var recipientNames = await _context.Personeller
                    .Where(p => request.RecipientIds.Contains(p.PersonelId))
                    .Select(p => p.Ad + " " + p.Soyad)
                    .ToListAsync();
                var namesStr = string.Join(", ", recipientNames);
                if(namesStr.Length > 200) namesStr = namesStr.Substring(0, 197) + "...";

                await _logService.LogAsync("Bildirim", $"Toplu bildirim gönderildi: {request.Baslik}", null, $"Kişi Sayısı: {request.RecipientIds.Count}, Tarih: {request.ScheduledTime ?? "Anlık"}, Kişiler: {namesStr}");

                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Gönderim hatası: " + ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetKoordinatorlukler(int teskilatId)
        {
            var list = await _context.Koordinatorlukler
                .Where(k => k.TeskilatId == teskilatId)
                .OrderBy(k => k.Ad)
                .Select(k => new { Value = k.KoordinatorlukId, Text = k.Ad })
                .ToListAsync();
            return Json(list);
        }

        [HttpGet]
        public async Task<IActionResult> GetKomisyonlar(int koordinatorlukId)
        {
            var data = await _context.Komisyonlar
                .Include(x => x.Koordinatorluk).ThenInclude(k => k.Il)
                .Include(x => x.BagliMerkezKoordinatorluk)
                .Where(x => (x.KoordinatorlukId == koordinatorlukId || x.BagliMerkezKoordinatorlukId == koordinatorlukId) && x.IsActive)
                .ToListAsync();

            var result = data.Select(x => new { 
                    Value = x.KomisyonId, 
                    Text = x.BagliMerkezKoordinatorlukId == koordinatorlukId && x.Koordinatorluk?.Il != null
                           ? $"{x.Koordinatorluk.Il.Ad} Komisyonu"
                           : x.Ad 
                })
                .OrderBy(x => x.Text)
                .ToList();

            return Json(result);
        }
    }

    public class BulkSendRequest
    {
        public List<int> RecipientIds { get; set; } = new();
        public string SenderId { get; set; } = null!; 
        public string Baslik { get; set; } = null!;
        public string Icerik { get; set; } = null!;
        public string? ScheduledTime { get; set; } 
    }
}
