using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonelTakipSistemi.Data;
using PersonelTakipSistemi.Models;
using PersonelTakipSistemi.ViewModels;

namespace PersonelTakipSistemi.Controllers
{
    [Authorize(Roles = "Admin,Yönetici")]
    public class PersonelAyarlarController : Controller
    {
        private readonly TegmPersonelTakipDbContext _context;

        public PersonelAyarlarController(TegmPersonelTakipDbContext context)
        {
            _context = context;
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
                switch (type)
                {
                    case "brans":
                        var b = await _context.Branslar.FindAsync(id);
                        if (b != null) _context.Branslar.Remove(b);
                        break;
                    case "yazilim":
                        var y = await _context.Yazilimlar.FindAsync(id);
                        if (y != null) _context.Yazilimlar.Remove(y);
                        break;
                    case "uzmanlik":
                        var u = await _context.Uzmanliklar.FindAsync(id);
                        if (u != null) _context.Uzmanliklar.Remove(u);
                        break;
                    case "gorevturu":
                        var gt = await _context.GorevTurleri.FindAsync(id);
                        if (gt != null) _context.GorevTurleri.Remove(gt);
                        break;
                    case "isniteligi":
                        var i = await _context.IsNitelikleri.FindAsync(id);
                        if (i != null) _context.IsNitelikleri.Remove(i);
                        break;
                    case "kurumsalrol":
                        var kr = await _context.KurumsalRoller.FindAsync(id);
                        if (kr != null) _context.KurumsalRoller.Remove(kr);
                        break;
                    default:
                        return BadRequest("Geçersiz tür.");
                }

                await _context.SaveChangesAsync();
                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                // Foreign key constraint errors might happen here if the item is in use.
                // For now, we'll just return the error.
                return StatusCode(500, "Silinemedi. Bu kayıt kullanımda olabilir.");
            }
        }
    }
}
