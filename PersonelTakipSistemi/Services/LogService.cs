using Microsoft.EntityFrameworkCore;
using PersonelTakipSistemi.Data;
using PersonelTakipSistemi.Models;
using System.Security.Claims;

namespace PersonelTakipSistemi.Services
{
    public class LogService : ILogService
    {
        private readonly TegmPersonelTakipDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LogService(TegmPersonelTakipDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task LogAsync(string islemTuru, string aciklama, int? ilgiliPersonelId = null, string? detay = null)
        {
            try
            {
                var context = _httpContextAccessor.HttpContext;
                
                // 1. IP Address
                string ip = context?.Connection?.RemoteIpAddress?.ToString() ?? "Unknown";
                if (context?.Request?.Headers?.ContainsKey("X-Forwarded-For") == true)
                    ip = context.Request.Headers["X-Forwarded-For"].ToString();

                // 2. Identify Performer (The one logged in)
                int? performerId = null;
                string? performerTc = null;
                string? performerName = null;

                if (context?.User?.Identity?.IsAuthenticated == true)
                {
                    // Try get Performer ID
                    var pidClaim = context.User.FindFirst("PersonelId")?.Value ?? 
                                   context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    if (int.TryParse(pidClaim, out int pid))
                    {
                        performerId = pid;
                    }

                    // Try get Performer Name
                    performerName = context.User.FindFirst(ClaimTypes.Name)?.Value;

                    // Try get Performer TC (Added in AccountController)
                    performerTc = context.User.FindFirst("TcKimlikNo")?.Value;

                    // Fallback: If logged in but TC missing (e.g. old session), fetch from DB once
                    if (performerId.HasValue && (performerTc == null || performerName == null))
                    {
                        var p = await _context.Personeller.FindAsync(performerId.Value);
                        if (p != null)
                        {
                            if (performerTc == null) performerTc = p.TcKimlikNo;
                            if (performerName == null) performerName = $"{p.Ad} {p.Soyad}";
                        }
                    }
                }

                // 2b. Special Case for Login (Giris): User is NOT in Context yet, but passed as argument
                if (!performerId.HasValue && islemTuru == "Giris" && ilgiliPersonelId.HasValue)
                {
                    performerId = ilgiliPersonelId.Value;
                    var p = await _context.Personeller.FindAsync(performerId.Value);
                    if (p != null)
                    {
                        performerTc = p.TcKimlikNo;
                        performerName = $"{p.Ad} {p.Soyad}";
                    }
                }

                // 3. Handle Target Person (ilgiliPersonelId)
                // PersonelId is stored in the log record, no need to inject into Detay text

                // 4. Create Log
                var log = new SistemLog
                {
                    // Always store Performer info in the main columns
                    PersonelId = performerId, 
                    TcKimlikNo = performerTc,
                    KullaniciAdSoyad = performerName,
                    
                    IpAdresi = ip,
                    Tarih = DateTime.Now,
                    IslemTuru = islemTuru, 
                    Aciklama = aciklama,
                    Detay = detay
                };

                _context.SistemLoglar.Add(log);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                // Logging should not crash the app
            }
        }

        public async Task<List<SistemLog>> GetLogsAsync(int page = 1, int pageSize = 20, string search = "", string type = "", DateTime? baslangic = null, DateTime? bitis = null)
        {
            var query = _context.SistemLoglar.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToLower();
                query = query.Where(l => 
                    (l.Aciklama != null && l.Aciklama.ToLower().Contains(search)) || 
                    (l.KullaniciAdSoyad != null && l.KullaniciAdSoyad.ToLower().Contains(search)) ||
                    (l.TcKimlikNo != null && l.TcKimlikNo.Contains(search)) ||
                    (l.IpAdresi != null && l.IpAdresi.Contains(search))
                );
            }

            if (!string.IsNullOrEmpty(type))
            {
                query = query.Where(l => l.IslemTuru == type);
            }

            if (baslangic.HasValue) query = query.Where(l => l.Tarih >= baslangic.Value);
            if (bitis.HasValue) query = query.Where(l => l.Tarih <= bitis.Value);

            return await query
                .OrderByDescending(l => l.Tarih)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetTotalCountAsync(string search = "", string type = "", DateTime? baslangic = null, DateTime? bitis = null)
        {
            var query = _context.SistemLoglar.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToLower();
                query = query.Where(l => 
                    (l.Aciklama != null && l.Aciklama.ToLower().Contains(search)) || 
                    (l.KullaniciAdSoyad != null && l.KullaniciAdSoyad.ToLower().Contains(search)) ||
                    (l.TcKimlikNo != null && l.TcKimlikNo.Contains(search)) ||
                    (l.IpAdresi != null && l.IpAdresi.Contains(search))
                );
            }

            if (!string.IsNullOrEmpty(type))
            {
                query = query.Where(l => l.IslemTuru == type);
            }

            if (baslangic.HasValue) query = query.Where(l => l.Tarih >= baslangic.Value);
            if (bitis.HasValue) query = query.Where(l => l.Tarih <= bitis.Value);

            return await query.CountAsync();
        }
    }
}
