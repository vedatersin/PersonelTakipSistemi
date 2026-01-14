using Microsoft.EntityFrameworkCore;
using PersonelTakipSistemi.Data;
using PersonelTakipSistemi.Dtos;
using PersonelTakipSistemi.Models;

namespace PersonelTakipSistemi.Services
{
    public class NotificationService : INotificationService
    {
        private readonly TegmPersonelTakipDbContext _context;

        public NotificationService(TegmPersonelTakipDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(int aliciId, int? gonderenId, string baslik, string aciklama, string tip, string? refType = null, int? refId = null, string? url = null)
        {
            var bildirim = new Bildirim
            {
                AliciPersonelId = aliciId,
                GonderenPersonelId = gonderenId,
                Baslik = baslik,
                Aciklama = aciklama,
                Tip = tip,
                RefType = refType,
                RefId = refId,
                Url = url,
                OlusturmaTarihi = DateTime.Now,
                OkunduMu = false
            };

            _context.Bildirimler.Add(bildirim);
            await _context.SaveChangesAsync();
        }

        public async Task<List<BildirimDto>> GetInboxAsync(int aliciId, int take = 200)
        {
            var bildirimler = await _context.Bildirimler
                .Include(b => b.GonderenPersonel)
                .ThenInclude(p => p.PersonelKurumsalRolAtamalari)
                .ThenInclude(pkr => pkr.KurumsalRol)
                .Where(b => b.AliciPersonelId == aliciId)
                .OrderByDescending(b => b.OlusturmaTarihi)
                .Take(take)
                .ToListAsync();

            return bildirimler.Select(b => MapToDto(b)).ToList();
        }

        public async Task<(int unreadCount, List<BildirimMiniDto> top)> GetTopUnreadAsync(int aliciId, int take = 5)
        {
            var unreadCount = await _context.Bildirimler.CountAsync(b => b.AliciPersonelId == aliciId && !b.OkunduMu);

            var topList = await _context.Bildirimler
                .Include(b => b.GonderenPersonel)
                .ThenInclude(p => p.PersonelKurumsalRolAtamalari)
                .ThenInclude(pkr => pkr.KurumsalRol)
                .Where(b => b.AliciPersonelId == aliciId)
                .OrderByDescending(b => b.OlusturmaTarihi)
                .Take(take)
                .ToListAsync();

            var dtos = topList.Select(b => new BildirimMiniDto
            {
                BildirimId = b.BildirimId,
                Baslik = b.Baslik,
                OlusturmaTarihi = b.OlusturmaTarihi,
                OkunduMu = b.OkunduMu,
                GonderenAdSoyad = b.GonderenPersonel != null ? $"{b.GonderenPersonel.Ad} {b.GonderenPersonel.Soyad}" : "Sistem",
                GonderenKurumsalRolOzet = GetHighestRole(b.GonderenPersonel)
            }).ToList();

            return (unreadCount, dtos);
        }

        public async Task MarkAsReadAsync(int aliciId, int bildirimId)
        {
            var bildirim = await _context.Bildirimler.FirstOrDefaultAsync(b => b.BildirimId == bildirimId && b.AliciPersonelId == aliciId);
            if (bildirim != null && !bildirim.OkunduMu)
            {
                bildirim.OkunduMu = true;
                bildirim.OkunmaTarihi = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task MarkAllAsReadAsync(int aliciId)
        {
            var unread = await _context.Bildirimler.Where(b => b.AliciPersonelId == aliciId && !b.OkunduMu).ToListAsync();
            foreach (var b in unread)
            {
                b.OkunduMu = true;
                b.OkunmaTarihi = DateTime.Now;
            }
            if (unread.Any()) await _context.SaveChangesAsync();
        }

        private BildirimDto MapToDto(Bildirim b)
        {
            return new BildirimDto
            {
                BildirimId = b.BildirimId,
                Baslik = b.Baslik,
                Aciklama = b.Aciklama,
                OlusturmaTarihi = b.OlusturmaTarihi,
                OkunduMu = b.OkunduMu,
                Url = b.Url,
                GonderenPersonelId = b.GonderenPersonelId,
                GonderenAdSoyad = b.GonderenPersonel != null ? $"{b.GonderenPersonel.Ad} {b.GonderenPersonel.Soyad}" : "Sistem",
                GonderenFotoUrl = b.GonderenPersonel?.FotografYolu,
                GonderenKurumsalRolOzet = GetHighestRole(b.GonderenPersonel)
            };
        }

        private string GetHighestRole(Personel? p)
        {
            if (p == null || p.PersonelKurumsalRolAtamalari == null || !p.PersonelKurumsalRolAtamalari.Any())
                return "Personel";

            var highest = p.PersonelKurumsalRolAtamalari
                .OrderByDescending(x => x.KurumsalRolId) // 4: Genel Koord > ...
                .Select(x => x.KurumsalRol.Ad)
                .FirstOrDefault();

            return highest ?? "Personel";
        }
    }
}
