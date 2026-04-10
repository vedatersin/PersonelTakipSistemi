using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonelTakipSistemi.Data;
using PersonelTakipSistemi.Models;
using PersonelTakipSistemi.ViewModels;
using PersonelTakipSistemi.Filters;

namespace PersonelTakipSistemi.Controllers
{
    [Authorize(Roles = "Admin,Yönetici")]
    [ReadOnlyForHighLevelRoles]
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
                Durumlar = await _context.GorevDurumlari.OrderBy(d => d.Sira).ToListAsync()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EkleDurum(string ad, string? renk)
        {
            if (string.IsNullOrWhiteSpace(ad))
            {
                return BadRequest("Durum adi bos olamaz.");
            }

            int maxSira = await _context.GorevDurumlari.MaxAsync(d => (int?)d.Sira) ?? 0;

            var durum = new GorevDurum
            {
                Ad = ad.Trim(),
                Renk = renk,
                Sira = maxSira + 1,
                RenkSinifi = "bg-secondary"
            };

            _context.GorevDurumlari.Add(durum);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> GuncelleDurum(int id, string ad, string? renk)
        {
            var durum = await _context.GorevDurumlari.FindAsync(id);
            if (durum == null)
            {
                return NotFound();
            }

            if (string.IsNullOrWhiteSpace(ad))
            {
                return BadRequest("Durum adi bos olamaz.");
            }

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

            if (durum == null)
            {
                return NotFound();
            }

            if (durum.Gorevler.Any())
            {
                return BadRequest("Bu duruma bagli gorevler var, silinemez.");
            }

            _context.GorevDurumlari.Remove(durum);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
