using Microsoft.AspNetCore.Mvc;

namespace PersonelTakipSistemi.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Ana Sayfa";
            return View();
        }
    }
}
