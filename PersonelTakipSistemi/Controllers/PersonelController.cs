using Microsoft.AspNetCore.Mvc;
using PersonelTakipSistemi.ViewModels;

namespace PersonelTakipSistemi.Controllers
{
    public class PersonelController : Controller
    {
        [HttpGet]
        public IActionResult Ekle()
        {
            ViewData["Title"] = "Personel Ekle";
            return View(new PersonelEkleViewModel());
        }

        [HttpGet]
        public IActionResult Detay()
        {
            ViewData["Title"] = "Personel Detay";
            // Mock data for display
            var model = new PersonelDetayViewModel
            {
                Ad = "Buraya Ad",
                Soyad = "Gelecek",
                KadroKurum = "Meb",
                GorevliIl = "İzmir",
                PersonelCinsiyet = 1, // Kadın
                Telefon = "0555 555 55 55",
                Eposta = "ornek@mail.com"
            };
            return View(model);
        }
    }
}
