using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonelTakipSistemi.Services;

namespace PersonelTakipSistemi.Controllers
{
    [Authorize(Roles = "Admin,Yönetici")]
    public class ToolsController : Controller
    {
        private readonly DataSeedingService _seedingService;

        public ToolsController(DataSeedingService seedingService)
        {
            _seedingService = seedingService;
        }

        public async Task<IActionResult> Seed()
        {
            await _seedingService.SeedAsync();
            TempData["Success"] = "Veri tabanı başarıyla sıfırlandı ve örnek veriler yüklendi.";
            return RedirectToAction("Index", "Personel");
        }
    }
}
