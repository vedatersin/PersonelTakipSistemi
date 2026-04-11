using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using PersonelTakipSistemi.Data;
using PersonelTakipSistemi.Services;
using PersonelTakipSistemi.ViewModels;
using System.Security.Claims;

namespace PersonelTakipSistemi.Controllers
{
    public class AccountController : Controller
    {
        private const int MaxFailedLoginAttempts = 5;
        private static readonly TimeSpan LoginLockDuration = TimeSpan.FromMinutes(10);

        private readonly TegmPersonelTakipDbContext _context;
        private readonly ILogService _logService;
        private readonly IPasswordService _passwordService;
        private readonly IMemoryCache _memoryCache;

        public AccountController(
            TegmPersonelTakipDbContext context,
            ILogService logService,
            IPasswordService passwordService,
            IMemoryCache memoryCache)
        {
            _context = context;
            _logService = logService;
            _passwordService = passwordService;
            _memoryCache = memoryCache;
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

            var tc = NormalizeTc(model.TcKimlikNo);
            var clientIp = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";
            if (IsLockedOut(tc, clientIp, out var lockoutMessage))
            {
                ModelState.AddModelError(string.Empty, lockoutMessage);
                return View(model);
            }

            var personel = await _context.Personeller
                .Include(p => p.SistemRol)
                .FirstOrDefaultAsync(p => p.TcKimlikNo == tc);

            if (personel == null)
            {
                RegisterFailedAttempt(tc, clientIp);
                ModelState.AddModelError(string.Empty, "TC Kimlik No veya parola hatalı.");
                return View(model);
            }

            if (!personel.AktifMi)
            {
                RegisterFailedAttempt(tc, clientIp);
                ModelState.AddModelError(string.Empty, "Yetkiniz bulunmamaktadır.");
                return View(model);
            }

            var verification = _passwordService.VerifyPassword(model.Sifre, personel);
            if (!verification.Succeeded)
            {
                RegisterFailedAttempt(tc, clientIp);
                ModelState.AddModelError(string.Empty, "TC Kimlik No veya parola hatalı.");
                return View(model);
            }

            ClearFailedAttempts(tc, clientIp);

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
                new Claim("PhotoUrl", personel.FotografYolu ?? string.Empty),
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

        private bool IsLockedOut(string tc, string ip, out string message)
        {
            var lockUntil = _memoryCache.Get<DateTimeOffset?>(BuildLockKey(tc, ip));
            if (lockUntil.HasValue && lockUntil.Value > DateTimeOffset.UtcNow)
            {
                var remainingSeconds = Math.Max(1, (int)Math.Ceiling((lockUntil.Value - DateTimeOffset.UtcNow).TotalSeconds));
                message = $"Çok fazla hatalı giriş denemesi. Lütfen {remainingSeconds} saniye sonra tekrar deneyin.";
                return true;
            }

            message = string.Empty;
            return false;
        }

        private void RegisterFailedAttempt(string tc, string ip)
        {
            var failKey = BuildFailKey(tc, ip);
            var lockKey = BuildLockKey(tc, ip);
            var attempts = _memoryCache.Get<int?>(failKey) ?? 0;
            attempts++;

            if (attempts >= MaxFailedLoginAttempts)
            {
                _memoryCache.Remove(failKey);
                _memoryCache.Set(lockKey, DateTimeOffset.UtcNow.Add(LoginLockDuration), LoginLockDuration);
                return;
            }

            _memoryCache.Set(failKey, attempts, LoginLockDuration);
        }

        private void ClearFailedAttempts(string tc, string ip)
        {
            _memoryCache.Remove(BuildFailKey(tc, ip));
            _memoryCache.Remove(BuildLockKey(tc, ip));
        }

        private static string NormalizeTc(string? tc)
        {
            if (string.IsNullOrWhiteSpace(tc))
            {
                return string.Empty;
            }

            return new string(tc.Where(char.IsDigit).ToArray());
        }

        private static string BuildFailKey(string tc, string ip) => $"auth:fail:{tc}:{ip}";
        private static string BuildLockKey(string tc, string ip) => $"auth:lock:{tc}:{ip}";
    }
}

