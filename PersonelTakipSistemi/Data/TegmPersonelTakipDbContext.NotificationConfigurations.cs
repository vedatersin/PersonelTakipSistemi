using Microsoft.EntityFrameworkCore;
using PersonelTakipSistemi.Models;

namespace PersonelTakipSistemi.Data
{
    public partial class TegmPersonelTakipDbContext
    {
        private static void ConfigureNotificationModule(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BildirimGonderen>(entity => {
                entity.ToTable("BildirimGonderenler");
                entity.HasOne(e => e.Personel).WithMany().HasForeignKey(e => e.PersonelId).OnDelete(DeleteBehavior.Cascade);
                entity.HasData(new BildirimGonderen
                {
                    Id = 1,
                    Tip = GonderenTip.Sistem,
                    GorunenAd = "Sistem",
                    PersonelId = null
                });
            });

            modelBuilder.Entity<TopluBildirim>(entity => {
                entity.ToTable("TopluBildirimler");
                entity.HasOne(e => e.Gonderen).WithMany().HasForeignKey(e => e.GonderenId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Bildirim>(entity => {
                entity.HasOne(d => d.BildirimGonderen).WithMany().HasForeignKey(d => d.BildirimGonderenId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(d => d.TopluBildirim).WithMany().HasForeignKey(d => d.TopluBildirimId).OnDelete(DeleteBehavior.SetNull);
            });
        }
    }
}
