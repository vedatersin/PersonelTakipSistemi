using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PersonelTakipSistemi.Controllers
{
    [Authorize(Roles = "Admin,Yönetici,Program Geliştirme Uzmanı")]
    public class ProgramGelistirmeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToAction(nameof(GunesIsiniDiagrami));
        }

        [HttpGet]
        public IActionResult AgAnaliziRaporu()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GunesIsiniDiagrami()
        {
            return View();
        }

        [HttpGet]
        public IActionResult IsiHaritasiRaporu()
        {
            return View();
        }

        [HttpGet]
        public IActionResult MatrisDiagrami()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GenelRaporlama()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult OgrenciProfiliYonetimi()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult OgrenciProfiliTanimlari()
        {
            return View();
        }
    }
}
