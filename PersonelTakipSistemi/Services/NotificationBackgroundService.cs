using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using PersonelTakipSistemi.Data;
using PersonelTakipSistemi.Models;
using PersonelTakipSistemi.Services;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.Generic;

namespace PersonelTakipSistemi.Services
{
    public class NotificationBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public NotificationBackgroundService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await ProcessScheduledNotificationsAsync(stoppingToken);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"NotificationBackgroundService Error: {ex.Message}");
                }

                // Check every 1 minute
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }

        private async Task ProcessScheduledNotificationsAsync(CancellationToken stoppingToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<TegmPersonelTakipDbContext>();
                
                var pendingBatches = await context.TopluBildirimler
                    .Where(t => t.Durum == BildirimDurum.Planlandi && t.PlanlananZaman <= DateTime.Now)
                    .ToListAsync(stoppingToken);

                foreach (var batch in pendingBatches)
                {
                    try
                    {
                        var recipientIds = new List<int>();
                        if (!string.IsNullOrEmpty(batch.RecipientIdsJson))
                        {
                            recipientIds = batch.RecipientIdsJson.Split(',').Select(int.Parse).ToList();
                        }

                        if (recipientIds.Any())
                        {
                            var notifications = recipientIds.Select(id => new Bildirim
                            {
                                AliciPersonelId = id,
                                BildirimGonderenId = batch.GonderenId,
                                TopluBildirimId = batch.Id,
                                // Need to find PersonelId if Sender is Personel for GonderenPersonelId backward compat
                                // But simple workaround: leave logic as is in CreateBulk
                                Baslik = batch.Baslik,
                                Aciklama = batch.Icerik,
                                OlusturmaTarihi = DateTime.Now,
                                OkunduMu = false,
                                Tip = "TopluBildirim"
                            }).ToList();

                            // Backward compat: If Sender is Personel, set GonderenPersonelId
                            // We need to fetch Sender to check Type
                            var sender = await context.BildirimGonderenler.FindAsync(batch.GonderenId);
                            if (sender != null && sender.Tip == GonderenTip.Personel)
                            {
                                foreach (var n in notifications) n.GonderenPersonelId = sender.PersonelId;
                            }

                            context.Bildirimler.AddRange(notifications);
                        }

                        batch.Durum = BildirimDurum.Gonderildi;
                        batch.GonderimZamani = DateTime.Now;
                        await context.SaveChangesAsync(stoppingToken);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"Error processing batch {batch.Id}: {ex.Message}");
                        batch.Durum = BildirimDurum.Hata;
                        await context.SaveChangesAsync(stoppingToken);
                    }
                }
            }
        }
    }
}
