using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonelTakipSistemi.Data;
using PersonelTakipSistemi.Models;
using PersonelTakipSistemi.ViewModels;

namespace PersonelTakipSistemi.Controllers
{
    [Authorize(Roles = "Admin,Yönetici")]
    public class GorevTanimlarController : Controller
    {
        private readonly TegmPersonelTakipDbContext _context;

        public GorevTanimlarController(TegmPersonelTakipDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new GorevTanimlarViewModel
            {
                Kategoriler = await _context.GorevKategorileri.OrderBy(k => k.Ad).ToListAsync(),
                Durumlar = await _context.GorevDurumlari.OrderBy(d => d.Sira).ToListAsync()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EkleKategori(string ad, string? aciklama, string? renk)
        {
            if (string.IsNullOrWhiteSpace(ad))
                return BadRequest("Kategori adı boş olamaz.");

            var kategori = new GorevKategori
            {
                Ad = ad.Trim(),
                Aciklama = aciklama?.Trim(),
                Renk = renk,
                IsActive = true
            };

            _context.GorevKategorileri.Add(kategori);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> GuncelleKategori(int id, string ad, string? aciklama, string? renk)
        {
            var kategori = await _context.GorevKategorileri.FindAsync(id);
            if (kategori == null) return NotFound();

            if (string.IsNullOrWhiteSpace(ad))
                return BadRequest("Kategori adı boş olamaz.");

            kategori.Ad = ad.Trim();
            kategori.Aciklama = aciklama?.Trim();
            kategori.Renk = renk;
            
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> SilKategori(int id)
        {
            var kategori = await _context.GorevKategorileri
                .Include(k => k.Gorevler)
                .FirstOrDefaultAsync(k => k.GorevKategoriId == id);

            if (kategori == null) return NotFound();

            if (kategori.Gorevler.Any())
                return BadRequest("Bu kategoriye bağlı görevler var, silinemez.");

            _context.GorevKategorileri.Remove(kategori);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> EkleDurum(string ad, string? renk)
        {
            if (string.IsNullOrWhiteSpace(ad))
                return BadRequest("Durum adı boş olamaz.");

            // Basic logic for Sira? Just put at end.
            int maxSira = await _context.GorevDurumlari.MaxAsync(d => (int?)d.Sira) ?? 0;

            var durum = new GorevDurum
            {
                Ad = ad.Trim(),
                Renk = renk,
                Sira = maxSira + 1,
                RenkSinifi = "bg-secondary" // Default fallback
            };

            _context.GorevDurumlari.Add(durum);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> GuncelleDurum(int id, string ad, string? renk)
        {
            var durum = await _context.GorevDurumlari.FindAsync(id);
            if (durum == null) return NotFound();

            if (string.IsNullOrWhiteSpace(ad))
                return BadRequest("Durum adı boş olamaz.");

            durum.Ad = ad.Trim();
            durum.Renk = renk;
            
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> SilDurum(int id)
        {
            var durum = await _context.GorevDurumlari
                .Include(d => d.Gorevler)
                .FirstOrDefaultAsync(d => d.GorevDurumId == id);

            if (durum == null) return NotFound();

            if (durum.Gorevler.Any())
                return BadRequest("Bu duruma bağlı görevler var, silinemez.");

            _context.GorevDurumlari.Remove(durum);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
