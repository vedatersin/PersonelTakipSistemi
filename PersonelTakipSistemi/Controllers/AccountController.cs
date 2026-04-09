using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonelTakipSistemi.Data;
using PersonelTakipSistemi.Services;
using PersonelTakipSistemi.ViewModels;
using System.Security.Claims;

namespace PersonelTakipSistemi.Controllers
{
    public class AccountController : Controller
    {
        private readonly TegmPersonelTakipDbContext _context;
        private readonly ILogService _logService;
        private readonly IPasswordService _passwordService;

        public AccountController(TegmPersonelTakipDbContext context, ILogService logService, IPasswordService passwordService)
        {
            _context = context;
            _logService = logService;
            _passwordService = passwordService;
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
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var personel = await _context.Personeller
                .Include(p => p.SistemRol)
                .FirstOrDefaultAsync(p => p.TcKimlikNo == model.TcKimlikNo);

            if (personel == null)
            {
                ModelState.AddModelError("", "Böyle bir kullanıcı yok");
                return View(model);
            }

            var verification = _passwordService.VerifyPassword(model.Sifre, personel);
            if (!verification.Succeeded)
            {
                ModelState.AddModelError("", "Parola hatalıdır");
                return View(model);
            }

            if (verification.RequiresUpgrade)
            {
                _passwordService.SetPassword(personel, model.Sifre);
                personel.UpdatedAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, personel.PersonelId.ToString()),
                new Claim(ClaimTypes.Name, $"{personel.Ad} {personel.Soyad}"),
                new Claim("PersonelId", personel.PersonelId.ToString()),
                new Claim("TcKimlikNo", personel.TcKimlikNo),
                new Claim("PhotoUrl", personel.FotografYolu ?? ""),
                new Claim("LoginUtc", DateTime.UtcNow.ToString("O")),
                new Claim(ClaimTypes.Role, personel.SistemRol?.Ad ?? "Kullanıcı")
            };

            if (!model.RememberMe)
            {
                claims.Add(new Claim("InstanceId", ApplicationState.InstanceId));
            }

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = model.RememberMe,
                ExpiresUtc = DateTime.UtcNow.AddHours(4)
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            await _logService.LogAsync("Giris", "Kullanıcı sisteme giriş yaptı.", personel.PersonelId);

            return RedirectToAction("BenimDetay", "Personel");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _logService.LogAsync("Cikis", "Kullanıcı çıkış yaptı.");

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
