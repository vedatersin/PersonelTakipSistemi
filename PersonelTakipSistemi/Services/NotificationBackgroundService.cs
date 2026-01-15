using Microsoft.EntityFrameworkCore;
using PersonelTakipSistemi.Data;
using PersonelTakipSistemi.Models;

namespace PersonelTakipSistemi.Services
{
    public class NotificationBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<NotificationBackgroundService> _logger;

        public NotificationBackgroundService(IServiceProvider serviceProvider, ILogger<NotificationBackgroundService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await ProcessScheduledNotificationsAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing scheduled notifications.");
                }

                // Check every minute
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }

        private async Task ProcessScheduledNotificationsAsync()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<TegmPersonelTakipDbContext>();
                
                // Find pending scheduled notifications that are due
                var pending = await context.TopluBildirimler
                    .Where(t => t.Durum == BildirimDurum.Planlandi && t.PlanlananZaman <= DateTime.Now)
                    .ToListAsync();

                foreach (var toplu in pending)
                {
                    // Convert to individual notifications
                    if (string.IsNullOrEmpty(toplu.RecipientIdsJson)) continue;

                    var recipientIds = toplu.RecipientIdsJson.Split(',').Select(int.Parse).ToList();
                    
                    var notifications = recipientIds.Select(id => new Bildirim
                    {
                        AliciPersonelId = id,
                        BildirimGonderenId = toplu.GonderenId,
                        TopluBildirimId = toplu.Id,
                        GonderenPersonelId = null, // Or handle if needed
                        Baslik = toplu.Baslik,
                        Aciklama = toplu.Icerik,
                        OlusturmaTarihi = DateTime.Now,
                        OkunduMu = false,
                        Tip = "TopluBildirim"
                    }).ToList();

                    context.Bildirimler.AddRange(notifications);
                    
                    toplu.Durum = BildirimDurum.Gonderildi;
                    toplu.GonderimZamani = DateTime.Now;
                }

                if (pending.Any())
                {
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
