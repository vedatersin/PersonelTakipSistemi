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
        private static readonly TimeSpan CaptchaDuration = TimeSpan.FromMinutes(10);
        private static readonly TimeSpan PasswordResetTokenDuration = TimeSpan.FromMinutes(20);

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
        public async Task<IActionResult> Login(bool normal = false)
        {
            if (User.Identity!.IsAuthenticated)
            {
                return await RedirectToLandingPageAsync();
            }

            if (!normal)
            {
                return RedirectToAction(nameof(SifreSifirla));
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

            if (personel.SifreSifirlamaGerekli)
            {
                ClearFailedAttempts(tc, clientIp);
                TempData["PasswordResetInfo"] = "Şifreniz sıfırlanmış. Devam etmek için güvenlik doğrulamasından sonra yeni şifrenizi belirleyin.";
                return RedirectToAction(nameof(SifreSifirla), new { tcKimlikNo = tc });
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

            return await RedirectToLandingPageAsync(personel.PersonelId, personel.SistemRol?.Ad, personel.YetkiliModlar);
        }

        [HttpGet]
        public IActionResult SifreSifirla(string? tcKimlikNo = null)
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).GetAwaiter().GetResult();
            }

            var model = CreateCaptchaModel(new SifreSifirlaViewModel
            {
                TcKimlikNo = NormalizeTc(tcKimlikNo)
            });

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SifreSifirla(SifreSifirlaViewModel model)
        {
            var tc = NormalizeTc(model.TcKimlikNo);
            model.TcKimlikNo = tc;

            if (!ValidateCaptcha(model))
            {
                ModelState.AddModelError(nameof(model.CaptchaCevap), "Güvenlik doğrulaması hatalı veya süresi dolmuş.");
            }

            if (!ModelState.IsValid)
            {
                return View(CreateCaptchaModel(model));
            }

            var personel = await _context.Personeller
                .FirstOrDefaultAsync(p => p.TcKimlikNo == tc && p.AktifMi);

            if (personel == null)
            {
                ModelState.AddModelError(string.Empty, "Bu bilgilerle aktif personel kaydı bulunamadı.");
                return View(CreateCaptchaModel(model));
            }

            var token = Guid.NewGuid().ToString("N");
            _memoryCache.Set(BuildPasswordResetTokenKey(token), personel.PersonelId, PasswordResetTokenDuration);
            _memoryCache.Remove(BuildCaptchaCacheKey(model.CaptchaKey));

            TempData["PasswordResetPersonel"] = $"{personel.Ad} {personel.Soyad}";
            return RedirectToAction(nameof(SifreBelirle), new { token });
        }

        [HttpGet]
        public IActionResult SifreBelirle(string token)
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).GetAwaiter().GetResult();
            }

            if (string.IsNullOrWhiteSpace(token) || !_memoryCache.TryGetValue(BuildPasswordResetTokenKey(token), out int _))
            {
                TempData["PasswordResetInfo"] = "Şifre belirleme bağlantısının süresi dolmuş. Lütfen tekrar güvenlik doğrulaması yapın.";
                return RedirectToAction(nameof(SifreSifirla));
            }

            return View(new SifreBelirleViewModel { Token = token });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SifreBelirle(SifreBelirleViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Token) ||
                !_memoryCache.TryGetValue(BuildPasswordResetTokenKey(model.Token), out int personelId))
            {
                ModelState.AddModelError(string.Empty, "Şifre belirleme bağlantısının süresi dolmuş. Lütfen işlemi yeniden başlatın.");
                return View(model);
            }

            var personel = await _context.Personeller.FirstOrDefaultAsync(p => p.PersonelId == personelId && p.AktifMi);
            if (personel == null)
            {
                ModelState.AddModelError(string.Empty, "Aktif personel kaydı bulunamadı.");
                return View(model);
            }

            ValidatePasswordPolicy(model.YeniSifre, personel.TcKimlikNo);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _passwordService.SetPassword(personel, model.YeniSifre);
            personel.SifreSifirlamaGerekli = false;
            personel.SifreSonDegistirmeTarihi = DateTime.Now;
            personel.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();

            _memoryCache.Remove(BuildPasswordResetTokenKey(model.Token));
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await _logService.LogAsync("Şifre Belirleme", "Kullanıcı yeni şifresini belirledi.", personel.PersonelId);

            TempData["PasswordResetSuccess"] = "Şifreniz güncellendi. Yeni şifrenizle giriş yapabilirsiniz.";
            return RedirectToAction(nameof(Login), new { normal = true });
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

        private SifreSifirlaViewModel CreateCaptchaModel(SifreSifirlaViewModel model)
        {
            var captchaCode = GenerateCaptchaCode();
            var key = Guid.NewGuid().ToString("N");

            model.CaptchaKey = key;
            model.CaptchaQuestion = captchaCode;
            model.CaptchaCevap = string.Empty;
            _memoryCache.Set(BuildCaptchaCacheKey(key), captchaCode, CaptchaDuration);

            return model;
        }

        private bool ValidateCaptcha(SifreSifirlaViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.CaptchaKey) ||
                !_memoryCache.TryGetValue(BuildCaptchaCacheKey(model.CaptchaKey), out string? expected) ||
                string.IsNullOrWhiteSpace(expected))
            {
                return false;
            }

            var normalizedAnswer = NormalizeCaptchaCode(model.CaptchaCevap);
            return string.Equals(expected, normalizedAnswer, StringComparison.OrdinalIgnoreCase);
        }

        private static string GenerateCaptchaCode()
        {
            const string alphabet = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";
            return new string(Enumerable.Range(0, 6)
                .Select(_ => alphabet[Random.Shared.Next(alphabet.Length)])
                .ToArray());
        }

        private static string NormalizeCaptchaCode(string? value)
        {
            return new string((value ?? string.Empty)
                .Where(char.IsLetterOrDigit)
                .Select(char.ToUpperInvariant)
                .ToArray());
        }

        private void ValidatePasswordPolicy(string password, string tcKimlikNo)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                return;
            }

            if (password.Length < 8)
            {
                ModelState.AddModelError(nameof(SifreBelirleViewModel.YeniSifre), "Şifre en az 8 karakter olmalıdır.");
            }

            if (!password.Any(char.IsUpper))
            {
                ModelState.AddModelError(nameof(SifreBelirleViewModel.YeniSifre), "Şifre en az bir büyük harf içermelidir.");
            }

            if (!password.Any(char.IsLower))
            {
                ModelState.AddModelError(nameof(SifreBelirleViewModel.YeniSifre), "Şifre en az bir küçük harf içermelidir.");
            }

            if (!password.Any(char.IsDigit))
            {
                ModelState.AddModelError(nameof(SifreBelirleViewModel.YeniSifre), "Şifre en az bir rakam içermelidir.");
            }

            if (!password.Any(ch => !char.IsLetterOrDigit(ch)))
            {
                ModelState.AddModelError(nameof(SifreBelirleViewModel.YeniSifre), "Şifre en az bir özel karakter içermelidir.");
            }

            if (password.Any(char.IsWhiteSpace))
            {
                ModelState.AddModelError(nameof(SifreBelirleViewModel.YeniSifre), "Şifre boşluk içeremez.");
            }

            if (!string.IsNullOrWhiteSpace(tcKimlikNo) && password.Contains(tcKimlikNo, StringComparison.Ordinal))
            {
                ModelState.AddModelError(nameof(SifreBelirleViewModel.YeniSifre), "Şifre T.C. Kimlik No içeremez.");
            }
        }

        private async Task<IActionResult> RedirectToLandingPageAsync(int? signedInPersonelId = null, string? signedInRole = null, string? signedInModeText = null)
        {
            if (string.Equals(signedInRole, "Program Geliştirme Uzmanı", StringComparison.OrdinalIgnoreCase) || User.IsInRole("Program Geliştirme Uzmanı"))
            {
                return RedirectToAction("GunesIsiniDiagrami", "ProgramGelistirme");
            }

            if (string.Equals(signedInRole, "Admin", StringComparison.OrdinalIgnoreCase) || User.IsInRole("Admin"))
            {
                var adminModes = ParseModeList(signedInModeText);
                if (!adminModes.Any() && !signedInPersonelId.HasValue)
                {
                    var personelIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    if (int.TryParse(personelIdStr, out var currentPersonelId))
                    {
                        adminModes = ParseModeList(await _context.Personeller
                            .AsNoTracking()
                            .Where(p => p.PersonelId == currentPersonelId)
                            .Select(p => p.YetkiliModlar)
                            .FirstOrDefaultAsync());
                    }
                }

                if (IsProgramModeRequested(adminModes))
                {
                    return RedirectToAction("GunesIsiniDiagrami", "ProgramGelistirme");
                }

                return RedirectToAction("BirimListele", "Birimler");
            }

            var personelId = signedInPersonelId;
            if (!personelId.HasValue)
            {
                var personelIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (int.TryParse(personelIdStr, out var parsedPersonelId))
                {
                    personelId = parsedPersonelId;
                }
            }

            if (!personelId.HasValue)
            {
                return RedirectToAction("BenimDetay", "Personel");
            }

            var coordinatorRoleIds = new[] { 3, 4, 5, 14 };

            var coordinatorAssignment = await _context.PersonelKurumsalRolAtamalari
                .AsNoTracking()
                .Where(x => x.PersonelId == personelId.Value && x.KoordinatorlukId.HasValue && coordinatorRoleIds.Contains(x.KurumsalRolId))
                .OrderBy(x => x.KoordinatorlukId)
                .FirstOrDefaultAsync();

            if (coordinatorAssignment != null)
            {
                return RedirectToAction("KordinatorlukYonetimi", "BirimYonetimi");
            }

            var chairAssignment = await _context.PersonelKurumsalRolAtamalari
                .AsNoTracking()
                .Where(x => x.PersonelId == personelId.Value &&
                    x.KurumsalRolId == 2 &&
                    x.KomisyonId.HasValue &&
                    x.Komisyon != null &&
                    x.Komisyon.IsActive)
                .OrderBy(x => x.KomisyonId)
                .FirstOrDefaultAsync();

            if (chairAssignment?.KomisyonId is int komisyonId)
            {
                return RedirectToAction("KomisyonYonetimi", "BirimYonetimi", new { id = komisyonId });
            }

            return RedirectToAction("BenimDetay", "Personel");
        }

        private bool IsProgramModeRequested(IReadOnlyCollection<string> allowedModes)
        {
            return string.Equals(Request.Cookies["tegm-system-mode"], "program", StringComparison.OrdinalIgnoreCase)
                && allowedModes.Contains("program");
        }

        private static List<string> ParseModeList(string? rawModes)
        {
            return (rawModes ?? string.Empty)
                .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(mode => mode.ToLowerInvariant())
                .Where(mode => mode is "program" or "komisyon" or "master")
                .Distinct()
                .ToList();
        }

        private static string BuildFailKey(string tc, string ip) => $"auth:fail:{tc}:{ip}";
        private static string BuildLockKey(string tc, string ip) => $"auth:lock:{tc}:{ip}";
        private static string BuildCaptchaCacheKey(string key) => $"auth:captcha:{key}";
        private static string BuildPasswordResetTokenKey(string token) => $"auth:password-reset:{token}";
    }
}

