using Microsoft.EntityFrameworkCore;
using PersonelTakipSistemi.Models;

namespace PersonelTakipSistemi.Data
{
    public class TegmPersonelTakipDbContext : DbContext
    {
        public TegmPersonelTakipDbContext(DbContextOptions<TegmPersonelTakipDbContext> options) : base(options)
        {
        }

        public DbSet<Personel> Personeller { get; set; }
        public DbSet<Yazilim> Yazilimlar { get; set; }
        public DbSet<Uzmanlik> Uzmanliklar { get; set; }
        public DbSet<GorevTuru> GorevTurleri { get; set; }
        public DbSet<IsNiteligi> IsNitelikleri { get; set; }

        public DbSet<PersonelYazilim> PersonelYazilimlar { get; set; }
        public DbSet<PersonelUzmanlik> PersonelUzmanliklar { get; set; }
        public DbSet<PersonelGorevTuru> PersonelGorevTurleri { get; set; }
        public DbSet<PersonelIsNiteligi> PersonelIsNitelikleri { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Table Mappings
            modelBuilder.Entity<Personel>().ToTable("Personeller");
            modelBuilder.Entity<Yazilim>().ToTable("Yazilimlar");
            modelBuilder.Entity<Uzmanlik>().ToTable("Uzmanliklar");
            modelBuilder.Entity<GorevTuru>().ToTable("GorevTurleri");
            modelBuilder.Entity<IsNiteligi>().ToTable("IsNitelikleri");

            // Personel Constraints
            modelBuilder.Entity<Personel>(entity =>
            {
                entity.HasKey(e => e.PersonelId);
                entity.Property(e => e.TcKimlikNo).IsRequired().HasMaxLength(11);
                entity.HasIndex(e => e.TcKimlikNo).IsUnique();
                entity.Property(e => e.Telefon).HasMaxLength(11); // Assuming 11 digits
                entity.Property(e => e.Eposta).IsRequired();
                entity.HasIndex(e => e.Eposta).IsUnique();
                entity.Property(e => e.SifreHash).IsRequired();
                entity.Property(e => e.SifreSalt).IsRequired();
                entity.Property(e => e.PersonelCinsiyet).HasColumnType("bit");
            });

            // Join Tables Configuration

            // PersonelYazilim
            modelBuilder.Entity<PersonelYazilim>(entity =>
            {
                entity.ToTable("PersonelYazilimlar");
                entity.HasKey(e => new { e.PersonelId, e.YazilimId });

                entity.HasOne(e => e.Personel)
                    .WithMany(p => p.PersonelYazilimlar)
                    .HasForeignKey(e => e.PersonelId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Yazilim)
                    .WithMany(y => y.PersonelYazilimlar)
                    .HasForeignKey(e => e.YazilimId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // PersonelUzmanlik
            modelBuilder.Entity<PersonelUzmanlik>(entity =>
            {
                entity.ToTable("PersonelUzmanliklar");
                entity.HasKey(e => new { e.PersonelId, e.UzmanlikId });

                entity.HasOne(e => e.Personel)
                    .WithMany(p => p.PersonelUzmanliklar)
                    .HasForeignKey(e => e.PersonelId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Uzmanlik)
                    .WithMany(u => u.PersonelUzmanliklar)
                    .HasForeignKey(e => e.UzmanlikId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // PersonelGorevTuru
            modelBuilder.Entity<PersonelGorevTuru>(entity =>
            {
                entity.ToTable("PersonelGorevTurleri");
                entity.HasKey(e => new { e.PersonelId, e.GorevTuruId });

                entity.HasOne(e => e.Personel)
                    .WithMany(p => p.PersonelGorevTurleri)
                    .HasForeignKey(e => e.PersonelId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.GorevTuru)
                    .WithMany(g => g.PersonelGorevTurleri)
                    .HasForeignKey(e => e.GorevTuruId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // PersonelIsNiteligi
            modelBuilder.Entity<PersonelIsNiteligi>(entity =>
            {
                entity.ToTable("PersonelIsNitelikleri");
                entity.HasKey(e => new { e.PersonelId, e.IsNiteligiId });

                entity.HasOne(e => e.Personel)
                    .WithMany(p => p.PersonelIsNitelikleri)
                    .HasForeignKey(e => e.PersonelId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.IsNiteligi)
                    .WithMany(i => i.PersonelIsNitelikleri)
                    .HasForeignKey(e => e.IsNiteligiId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
