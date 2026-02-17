using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonelTakipSistemi.Data;
using PersonelTakipSistemi.Models;
using PersonelTakipSistemi.ViewModels;
using PersonelTakipSistemi.Services;

namespace PersonelTakipSistemi.Controllers
{
    [Authorize(Roles = "Admin,Yönetici")]
    public class PersonelAyarlarController : Controller
    {
        private readonly TegmPersonelTakipDbContext _context;
        private readonly ILogService _logService;

        public PersonelAyarlarController(TegmPersonelTakipDbContext context, ILogService logService)
        {
            _context = context;
            _logService = logService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = new PersonelAyarlarViewModel
            {
                Branslar = await _context.Branslar.OrderBy(x => x.Ad).ToListAsync(),
                Yazilimlar = await _context.Yazilimlar.OrderBy(x => x.Ad).ToListAsync(),
                Uzmanliklar = await _context.Uzmanliklar.OrderBy(x => x.Ad).ToListAsync(),
                GorevTurleri = await _context.GorevTurleri.OrderBy(x => x.Ad).ToListAsync(),
                IsNitelikleri = await _context.IsNitelikleri.OrderBy(x => x.Ad).ToListAsync(),
                KurumsalRoller = await _context.KurumsalRoller.OrderBy(x => x.Ad).ToListAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Ekle([FromBody] PersonelAyarEkleModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Ad)) return BadRequest("Ad alanı boş olamaz.");

            try 
            {
                switch (model.Type)
                {
                    case "brans":
                        var b = new Brans { Ad = model.Ad };
                        _context.Branslar.Add(b);
                        await _context.SaveChangesAsync();
                        break;
                    case "yazilim":
                        var y = new Yazilim { Ad = model.Ad };
                        _context.Yazilimlar.Add(y);
                        await _context.SaveChangesAsync();
                        break;
                    case "uzmanlik":
                        var u = new Uzmanlik { Ad = model.Ad };
                        _context.Uzmanliklar.Add(u);
                        await _context.SaveChangesAsync();
                        break;
                    case "gorevturu":
                        var gt = new GorevTuru { Ad = model.Ad };
                        _context.GorevTurleri.Add(gt);
                        await _context.SaveChangesAsync();
                        break;
                    case "isniteligi":
                        var i = new IsNiteligi { Ad = model.Ad };
                        _context.IsNitelikleri.Add(i);
                        await _context.SaveChangesAsync();
                        break;
                    case "kurumsalrol":
                        var kr = new KurumsalRol { Ad = model.Ad };
                        _context.KurumsalRoller.Add(kr);
                        await _context.SaveChangesAsync();
                        break;
                    default:
                        return BadRequest("Geçersiz tür.");
                }

                await _logService.LogAsync("Tanım Ekleme", $"Yeni tanım eklendi: {model.Ad}", null, $"Tür: {model.Type}");
                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Hata oluştu: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Sil(string type, int id)
        {
            try
            {
                string deletedName = "";

                switch (type)
                {
                    case "brans":
                        var b = await _context.Branslar.FindAsync(id);
                        if (b != null) { deletedName = b.Ad; _context.Branslar.Remove(b); }
                        break;
                    case "yazilim":
                        var y = await _context.Yazilimlar.FindAsync(id);
                        if (y != null) { deletedName = y.Ad; _context.Yazilimlar.Remove(y); }
                        break;
                    case "uzmanlik":
                        var u = await _context.Uzmanliklar.FindAsync(id);
                        if (u != null) { deletedName = u.Ad; _context.Uzmanliklar.Remove(u); }
                        break;
                    case "gorevturu":
                        var gt = await _context.GorevTurleri.FindAsync(id);
                        if (gt != null) { deletedName = gt.Ad; _context.GorevTurleri.Remove(gt); }
                        break;
                    case "isniteligi":
                        var i = await _context.IsNitelikleri.FindAsync(id);
                        if (i != null) { deletedName = i.Ad; _context.IsNitelikleri.Remove(i); }
                        break;
                    case "kurumsalrol":
                        var kr = await _context.KurumsalRoller.FindAsync(id);
                        if (kr != null) { deletedName = kr.Ad; _context.KurumsalRoller.Remove(kr); }
                        break;
                    default:
                        return BadRequest("Geçersiz tür.");
                }

                await _context.SaveChangesAsync();
                await _logService.LogAsync("Tanım Silme", $"Tanım silindi: {deletedName}", null, $"Tür: {type}");
                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Silinemedi. Bu kayıt kullanımda olabilir.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Guncelle([FromBody] PersonelAyarGuncelleModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Ad)) return BadRequest("Ad alanı boş olamaz.");

            try
            {
                string oldName = "";

                switch (model.Type)
                {
                    case "brans":
                        var b = await _context.Branslar.FindAsync(model.Id);
                        if (b != null) { oldName = b.Ad; b.Ad = model.Ad; }
                        break;
                    case "yazilim":
                        var y = await _context.Yazilimlar.FindAsync(model.Id);
                        if (y != null) { oldName = y.Ad; y.Ad = model.Ad; }
                        break;
                    case "uzmanlik":
                        var u = await _context.Uzmanliklar.FindAsync(model.Id);
                        if (u != null) { oldName = u.Ad; u.Ad = model.Ad; }
                        break;
                    case "gorevturu":
                        var gt = await _context.GorevTurleri.FindAsync(model.Id);
                        if (gt != null) { oldName = gt.Ad; gt.Ad = model.Ad; }
                        break;
                    case "isniteligi":
                        var i = await _context.IsNitelikleri.FindAsync(model.Id);
                        if (i != null) { oldName = i.Ad; i.Ad = model.Ad; }
                        break;
                    case "kurumsalrol":
                        var kr = await _context.KurumsalRoller.FindAsync(model.Id);
                        if (kr != null) { oldName = kr.Ad; kr.Ad = model.Ad; }
                        break;
                    default:
                        return BadRequest("Geçersiz tür.");
                }

                await _context.SaveChangesAsync();
                await _logService.LogAsync("Tanım Güncelleme", $"Tanım güncellendi: {oldName} → {model.Ad}", null, $"Tür: {model.Type}");
                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Güncelleme başarısız: " + ex.Message);
            }
        }
    }
}
