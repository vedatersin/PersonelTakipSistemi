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

        public async Task LogAsync(string islemTuru, string aciklama, int? personelId = null, string? detay = null)
        {
            try
            {
                var context = _httpContextAccessor.HttpContext;
                
                // 1. IP Address
                string ip = context?.Connection?.RemoteIpAddress?.ToString() ?? "Unknown";
                if (context?.Request?.Headers?.ContainsKey("X-Forwarded-For") == true)
                    ip = context.Request.Headers["X-Forwarded-For"].ToString();

                // 2. Resolve User if not provided
                string? tc = null;
                string? adSoyad = null;

                if (personelId == null && context?.User?.Identity?.IsAuthenticated == true)
                {
                    var pidClaim = context.User.FindFirst("PersonelId")?.Value;
                    if (int.TryParse(pidClaim, out int pid))
                    {
                        personelId = pid;
                    }
                }

                if (personelId.HasValue)
                {
                    var p = await _context.Personeller.FindAsync(personelId.Value);
                    if (p != null)
                    {
                        tc = p.TcKimlikNo;
                        adSoyad = $"{p.Ad} {p.Soyad}";
                    }
                }

                var log = new SistemLog
                {
                    PersonelId = personelId,
                    TcKimlikNo = tc,
                    KullaniciAdSoyad = adSoyad,
                    IpAdresi = ip,
                    Tarih = DateTime.Now,
                    IslemTuru = islemTuru, // "Giris", "Cikis", "Ekleme", "Silme", "Guncelleme", "Yetkilendirme", "Bildirim"
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
