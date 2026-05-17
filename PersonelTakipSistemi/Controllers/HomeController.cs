using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonelTakipSistemi.Data;
using System.Security.Claims;

namespace PersonelTakipSistemi.Controllers
{
    public class HomeController : Controller
    {
        private readonly TegmPersonelTakipDbContext _context;

        public HomeController(TegmPersonelTakipDbContext context)
        {
            _context = context;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("BirimListele", "Birimler");
            }

            var personelIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(personelIdStr, out var personelId))
            {
                return RedirectToAction("Login", "Account");
            }

            var coordinatorRoleIds = new[] { 3, 4, 5, 14 };

            var hasCoordinatorRole = await _context.PersonelKurumsalRolAtamalari
                .AsNoTracking()
                .AnyAsync(x => x.PersonelId == personelId && x.KoordinatorlukId.HasValue && coordinatorRoleIds.Contains(x.KurumsalRolId));

            if (hasCoordinatorRole)
            {
                return RedirectToAction("KordinatorlukYonetimi", "BirimYonetimi");
            }

            var chairAssignment = await _context.PersonelKurumsalRolAtamalari
                .AsNoTracking()
                .Where(x => x.PersonelId == personelId && x.KurumsalRolId == 2 && x.KomisyonId.HasValue)
                .OrderBy(x => x.KomisyonId)
                .Select(x => x.KomisyonId)
                .FirstOrDefaultAsync();

            if (chairAssignment.HasValue && chairAssignment.Value > 0)
            {
                return RedirectToAction("KomisyonYonetimi", "BirimYonetimi", new { id = chairAssignment.Value });
            }

            return RedirectToAction("BenimDetay", "Personel");
        }
    }
}
