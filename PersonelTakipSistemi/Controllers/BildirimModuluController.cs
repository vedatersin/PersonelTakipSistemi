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

        public BildirimModuluController(INotificationService notificationService, TegmPersonelTakipDbContext context)
        {
            _notificationService = notificationService;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetSenders()
        {
            var senders = await _notificationService.GetAvailableSendersAsync();
            return Json(senders);
        }

        [HttpGet]
        public async Task<IActionResult> GetPersonelList(
            string? search, 
            int? teskilatId, 
            int? koordinatorlukId, 
            int? komisyonId, 
            int? sistemRolId, 
            int? kurumsalRolId)
        {
            // Filter logic
            var query = _context.Personeller.AsQueryable();

            // Exclude Inactive
            query = query.Where(p => p.AktifMi);

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

            var list = await query
                .Include(p => p.PersonelTeskilatlar).ThenInclude(pt => pt.Teskilat)
                .Include(p => p.PersonelKoordinatorlukler).ThenInclude(pk => pk.Koordinatorluk)
                .Include(p => p.PersonelKomisyonlar).ThenInclude(pk => pk.Komisyon)
                .Include(p => p.SistemRol)
                .Include(p => p.PersonelKurumsalRolAtamalari).ThenInclude(pkr => pkr.KurumsalRol)
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
                    Komisyonlar = p.PersonelKomisyonlar.Select(k => k.Komisyon.Ad).ToList()
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
                
                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Gönderim hatası: " + ex.Message);
            }
        }
    }

    public class BulkSendRequest
    {
        public List<int> RecipientIds { get; set; } = new();
        public string SenderId { get; set; } = null!; 
        public string Baslik { get; set; } = null!;
        public string Icerik { get; set; } = null!;
        public string? ScheduledTime { get; set; } // Changed to string for safer binding
    }
}
