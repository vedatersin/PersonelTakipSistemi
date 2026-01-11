using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonelTakipSistemi.Data;
using PersonelTakipSistemi.ViewModels;
using System.Security.Claims;

namespace PersonelTakipSistemi.Controllers
{
    public class AccountController : Controller
    {
        private readonly TegmPersonelTakipDbContext _context;

        public AccountController(TegmPersonelTakipDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("BenimDetay", "Personel");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // 1. Kullanıcıyı TC ile bul
                var personel = await _context.Personeller
                    .FirstOrDefaultAsync(p => p.TcKimlikNo == model.TcKimlikNo);

                if (personel == null)
                {
                    ModelState.AddModelError("", "Böyle bir kullanıcı yok");
                    return View(model);
                }

                // 2. Şifre Kontrolü (Düz metin)
                if (personel.Sifre != model.Sifre)
                {
                    ModelState.AddModelError("", "Parola hatalıdır");
                    return View(model);
                }

                // 3. Claims Oluştur
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, personel.PersonelId.ToString()),
                    new Claim(ClaimTypes.Name, $"{personel.Ad} {personel.Soyad}"),
                    new Claim("PersonelId", personel.PersonelId.ToString()), // Custom claim for security checks
                    new Claim("PhotoUrl", personel.FotografYolu ?? "")
                };

                if (!model.RememberMe)
                {
                    // Oturum bazlı (RememberMe yoksa) girişlerde, sunucu yeniden başlatılınca oturumun düşmesi için
                    // o anki sunucu InstanceId'sini claim olarak ekliyoruz.
                    claims.Add(new Claim("InstanceId", PersonelTakipSistemi.ApplicationState.InstanceId));
                }

                // 4. Identity ve Principal
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = model.RememberMe,
                    ExpiresUtc = model.RememberMe ? DateTime.UtcNow.AddDays(30) : null
                };

                // 5. Sign In
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                // 6. Yönlendirme (Kendi Detay Sayfasına)
                return RedirectToAction("BenimDetay", "Personel");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
        
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
