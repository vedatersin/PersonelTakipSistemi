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
            // 1. Resolve Sender
            int senderId = await GetOrCreateSenderIdAsync(gonderenId);

            var bildirim = new Bildirim
            {
                AliciPersonelId = aliciId,
                BildirimGonderenId = senderId,
                GonderenPersonelId = gonderenId, // Legacy backward compatibility
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

        public async Task<int> CreateBulkAsync(int? gonderenId, List<int> aliciIds, string baslik, string aciklama, DateTime? planlananZaman = null)
        {
            if (aliciIds == null || !aliciIds.Any()) return 0;

            int senderId = await GetOrCreateSenderIdAsync(gonderenId);

            var toplu = new TopluBildirim
            {
                GonderenId = senderId,
                Baslik = baslik,
                Icerik = aciklama,
                OlusturmaTarihi = DateTime.Now,
                PlanlananZaman = planlananZaman,
                RecipientIdsJson = string.Join(",", aliciIds),
                Durum = planlananZaman.HasValue ? BildirimDurum.Planlandi : BildirimDurum.Gonderildi
            };

            _context.TopluBildirimler.Add(toplu);
            await _context.SaveChangesAsync();

            // If not scheduled, send immediately
            if (!planlananZaman.HasValue)
            {
                var notifications = aliciIds.Select(id => new Bildirim
                {
                    AliciPersonelId = id,
                    BildirimGonderenId = senderId,
                    TopluBildirimId = toplu.Id,
                    GonderenPersonelId = gonderenId,
                    Baslik = baslik,
                    Aciklama = aciklama,
                    OlusturmaTarihi = DateTime.Now,
                    OkunduMu = false,
                    Tip = "TopluBildirim"
                }).ToList();

                _context.Bildirimler.AddRange(notifications);
                toplu.GonderimZamani = DateTime.Now;
                await _context.SaveChangesAsync();
            }

            return toplu.Id;
        }

        public async Task<List<SenderOptionDto>> GetAvailableSendersAsync(int? currentUserId = null)
        {
            var options = new List<SenderOptionDto>();

            // 1. System (Always Available for Admins)
            options.Add(new SenderOptionDto 
            { 
                Id = "System", 
                Ad = "Sistem", 
                IsSystem = true, 
                AvatarUrl = "/img/system_avatar.png" 
            });

            // 2. Filter Logic
            // If currentUserId is provided, we only want to show THAT user if they are allowed (Admin/Yonetici)
            // If currentUserId is null (or legacy logic), we show all Admins (old behavior, restricted now by preference)
            
            var query = _context.Personeller
                .Include(p => p.SistemRol)
                .Where(p => p.SistemRol != null && (p.SistemRol.Ad == "Admin" || p.SistemRol.Ad == "Yönetici") && p.AktifMi);

            if (currentUserId.HasValue)
            {
                query = query.Where(p => p.PersonelId == currentUserId.Value);
            }

            var users = await query
                .OrderBy(p => p.Ad)
                .Select(p => new 
                {
                    p.PersonelId,
                    p.Ad,
                    p.Soyad,
                    RolAd = p.SistemRol!.Ad,
                    p.FotografYolu
                })
                .ToListAsync();

            foreach (var user in users)
            {
                options.Add(new SenderOptionDto
                {
                    Id = user.PersonelId.ToString(), // Client sends "123"
                    Ad = $"{user.Ad} {user.Soyad}",
                    Title = user.Ad, 
                    AvatarUrl = user.FotografYolu,
                    IsSystem = false
                });
            }

            return options;
        }

        private async Task<int> GetOrCreateSenderIdAsync(int? personelId)
        {
            // Case 1: System (personelId null)
            if (personelId == null) return 1; // 1 is seeded as System

            // Case 2: Check if Sender exists
            var existing = await _context.BildirimGonderenler
                .FirstOrDefaultAsync(bg => bg.PersonelId == personelId);

            if (existing != null) return existing.Id;

            // Case 3: Create new Sender for this Personel
            var personel = await _context.Personeller.FindAsync(personelId);
            if (personel == null) return 1; // Fallback to System?

            var newSender = new BildirimGonderen
            {
                Tip = GonderenTip.Personel,
                PersonelId = personelId,
                GorunenAd = $"{personel.Ad} {personel.Soyad}",
                AvatarUrl = personel.FotografYolu
            };

            _context.BildirimGonderenler.Add(newSender);
            await _context.SaveChangesAsync();
            return newSender.Id;
        }

        public async Task<List<BildirimDto>> GetInboxAsync(int aliciId, int take = 200)
        {
            var bildirimler = await _context.Bildirimler
                .Include(b => b.BildirimGonderen) // Use new FK
                .Include(b => b.GonderenPersonel) // Include legacy for role fallback
                .ThenInclude(p => p.PersonelKurumsalRolAtamalari)
                .ThenInclude(pkr => pkr.KurumsalRol)
                .Where(b => b.AliciPersonelId == aliciId && !b.SilindiMi) // Filter Deleted
                .OrderByDescending(b => b.OlusturmaTarihi)
                .Take(take)
                .ToListAsync();

            return bildirimler.Select(b => MapToDto(b)).ToList();
        }

        public async Task<(int unreadCount, List<BildirimMiniDto> top)> GetTopUnreadAsync(int aliciId, int take = 5)
        {
            var unreadCount = await _context.Bildirimler.CountAsync(b => b.AliciPersonelId == aliciId && !b.OkunduMu && !b.SilindiMi);

            var topList = await _context.Bildirimler
                .Include(b => b.BildirimGonderen)
                .Where(b => b.AliciPersonelId == aliciId && !b.SilindiMi)
                .OrderByDescending(b => b.OlusturmaTarihi)
                .Take(take)
                .ToListAsync();

            var dtos = topList.Select(b => new BildirimMiniDto
            {
                BildirimId = b.BildirimId,
                Baslik = b.Baslik,
                OlusturmaTarihi = b.OlusturmaTarihi,
                OkunduMu = b.OkunduMu,
                GonderenAdSoyad = b.BildirimGonderen?.GorunenAd ?? "Sistem",
                GonderenKurumsalRolOzet = "Sistem" // Simplified for topbar
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
            // Only unread & not deleted
            var unread = await _context.Bildirimler.Where(b => b.AliciPersonelId == aliciId && !b.OkunduMu && !b.SilindiMi).ToListAsync();
            foreach (var b in unread)
            {
                b.OkunduMu = true;
                b.OkunmaTarihi = DateTime.Now;
            }
            if (unread.Any()) await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int aliciId, int bildirimId)
        {
            var bildirim = await _context.Bildirimler.FirstOrDefaultAsync(b => b.BildirimId == bildirimId && b.AliciPersonelId == aliciId);
            if (bildirim != null)
            {
                _context.Bildirimler.Remove(bildirim); // Hard Delete
                await _context.SaveChangesAsync();
            }
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
                GonderenAdSoyad = b.BildirimGonderen?.GorunenAd ?? "Sistem",
                GonderenFotoUrl = b.BildirimGonderen?.AvatarUrl,
                GonderenKurumsalRolOzet = b.BildirimGonderen?.Tip == GonderenTip.Sistem ? "Sistem" : GetHighestRole(b.GonderenPersonel)
            };
        }

        private string GetHighestRole(Personel? p)
        {
            if (p == null || p.PersonelKurumsalRolAtamalari == null || !p.PersonelKurumsalRolAtamalari.Any())
                return "Personel";

            var highest = p.PersonelKurumsalRolAtamalari
                .OrderByDescending(x => x.KurumsalRolId) 
                .Select(x => x.KurumsalRol.Ad)
                .FirstOrDefault();

            return highest ?? "Personel";
        }
    }
}
