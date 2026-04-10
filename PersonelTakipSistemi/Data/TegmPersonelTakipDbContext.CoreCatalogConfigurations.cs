using Microsoft.EntityFrameworkCore;
using PersonelTakipSistemi.Data.SeedData;
using PersonelTakipSistemi.Models;

namespace PersonelTakipSistemi.Data
{
    public partial class TegmPersonelTakipDbContext
    {
        private static void ConfigureCoreCatalogs(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Personel>().ToTable("Personeller");
            modelBuilder.Entity<Yazilim>().ToTable("Yazilimlar");
            modelBuilder.Entity<Uzmanlik>().ToTable("Uzmanliklar");
            modelBuilder.Entity<GorevTuru>().ToTable("GorevTurleri");
            modelBuilder.Entity<IsNiteligi>().ToTable("IsNitelikleri");
            modelBuilder.Entity<Il>().ToTable("Iller");
            modelBuilder.Entity<Ilce>().ToTable("Ilceler");
            modelBuilder.Entity<Brans>().ToTable("Branslar");

            modelBuilder.Entity<DaireBaskanligi>().HasData(
                new DaireBaskanligi { Id = 1, Ad = "Araştırma Geliştirme ve Projeler Daire Başkanlığı", IsActive = false },
                new DaireBaskanligi { Id = 2, Ad = "Eğitim Ortamlarının ve Öğrenme Süreçlerinin Geliştirilmesi Daire Başkanlığı", IsActive = false },
                new DaireBaskanligi { Id = 3, Ad = "Eğitim Politikaları Daire Başkanlığı", IsActive = false },
                new DaireBaskanligi { Id = 4, Ad = "Erken Çocukluk Eğitimi Daire Başkanlığı", IsActive = false },
                new DaireBaskanligi { Id = 5, Ad = "İdari ve Mali İşler Daire Başkanlığı", IsActive = false },
                new DaireBaskanligi { Id = 6, Ad = "İzleme ve Değerlendirme Daire Başkanlığı", IsActive = false },
                new DaireBaskanligi { Id = 7, Ad = "Kültür, Sanat ve Spor Etkinlikleri Daire Başkanlığı", IsActive = false },
                new DaireBaskanligi { Id = 8, Ad = "Öğrenci İşleri Daire Başkanlığı", IsActive = false },
                new DaireBaskanligi { Id = 9, Ad = "Programlar ve Öğretim Materyalleri Daire Başkanlığı", IsActive = true }
            );

            modelBuilder.Entity<Brans>(entity =>
            {
                entity.HasKey(e => e.BransId);
                entity.Property(e => e.Ad).IsRequired().HasMaxLength(250);
                entity.HasIndex(e => e.Ad).IsUnique();
                entity.HasData(CatalogSeedData.GetBransSeed());
            });

            modelBuilder.Entity<Il>(entity =>
            {
                entity.HasKey(e => e.IlId);
                entity.Property(e => e.Ad).IsRequired().HasMaxLength(50);
                entity.HasIndex(e => e.Ad).IsUnique();
                entity.HasData(
                    new Il { IlId = 1, Ad = "Adana" }, new Il { IlId = 2, Ad = "Adıyaman" }, new Il { IlId = 3, Ad = "Afyonkarahisar" },
                    new Il { IlId = 4, Ad = "Ağrı" }, new Il { IlId = 5, Ad = "Amasya" }, new Il { IlId = 6, Ad = "Ankara" },
                    new Il { IlId = 7, Ad = "Antalya" }, new Il { IlId = 8, Ad = "Artvin" }, new Il { IlId = 9, Ad = "Aydın" },
                    new Il { IlId = 10, Ad = "Balıkesir" }, new Il { IlId = 11, Ad = "Bilecik" }, new Il { IlId = 12, Ad = "Bingöl" },
                    new Il { IlId = 13, Ad = "Bitlis" }, new Il { IlId = 14, Ad = "Bolu" }, new Il { IlId = 15, Ad = "Burdur" },
                    new Il { IlId = 16, Ad = "Bursa" }, new Il { IlId = 17, Ad = "Çanakkale" }, new Il { IlId = 18, Ad = "Çankırı" },
                    new Il { IlId = 19, Ad = "Çorum" }, new Il { IlId = 20, Ad = "Denizli" }, new Il { IlId = 21, Ad = "Diyarbakır" },
                    new Il { IlId = 22, Ad = "Edirne" }, new Il { IlId = 23, Ad = "Elazığ" }, new Il { IlId = 24, Ad = "Erzincan" },
                    new Il { IlId = 25, Ad = "Erzurum" }, new Il { IlId = 26, Ad = "Eskişehir" }, new Il { IlId = 27, Ad = "Gaziantep" },
                    new Il { IlId = 28, Ad = "Giresun" }, new Il { IlId = 29, Ad = "Gümüşhane" }, new Il { IlId = 30, Ad = "Hakkari" },
                    new Il { IlId = 31, Ad = "Hatay" }, new Il { IlId = 32, Ad = "Isparta" }, new Il { IlId = 33, Ad = "Mersin" },
                    new Il { IlId = 34, Ad = "İstanbul" }, new Il { IlId = 35, Ad = "İzmir" }, new Il { IlId = 36, Ad = "Kars" },
                    new Il { IlId = 37, Ad = "Kastamonu" }, new Il { IlId = 38, Ad = "Kayseri" }, new Il { IlId = 39, Ad = "Kırklareli" },
                    new Il { IlId = 40, Ad = "Kırşehir" }, new Il { IlId = 41, Ad = "Kocaeli" }, new Il { IlId = 42, Ad = "Konya" },
                    new Il { IlId = 43, Ad = "Kütahya" }, new Il { IlId = 44, Ad = "Malatya" }, new Il { IlId = 45, Ad = "Manisa" },
                    new Il { IlId = 46, Ad = "Kahramanmaraş" }, new Il { IlId = 47, Ad = "Mardin" }, new Il { IlId = 48, Ad = "Muğla" },
                    new Il { IlId = 49, Ad = "Muş" }, new Il { IlId = 50, Ad = "Nevşehir" }, new Il { IlId = 51, Ad = "Niğde" },
                    new Il { IlId = 52, Ad = "Ordu" }, new Il { IlId = 53, Ad = "Rize" }, new Il { IlId = 54, Ad = "Sakarya" },
                    new Il { IlId = 55, Ad = "Samsun" }, new Il { IlId = 56, Ad = "Siirt" }, new Il { IlId = 57, Ad = "Sinop" },
                    new Il { IlId = 58, Ad = "Sivas" }, new Il { IlId = 59, Ad = "Tekirdağ" }, new Il { IlId = 60, Ad = "Tokat" },
                    new Il { IlId = 61, Ad = "Trabzon" }, new Il { IlId = 62, Ad = "Tunceli" }, new Il { IlId = 63, Ad = "Şanlıurfa" },
                    new Il { IlId = 64, Ad = "Uşak" }, new Il { IlId = 65, Ad = "Van" }, new Il { IlId = 66, Ad = "Yozgat" },
                    new Il { IlId = 67, Ad = "Zonguldak" }, new Il { IlId = 68, Ad = "Aksaray" }, new Il { IlId = 69, Ad = "Bayburt" },
                    new Il { IlId = 70, Ad = "Karaman" }, new Il { IlId = 71, Ad = "Kırıkkale" }, new Il { IlId = 72, Ad = "Batman" },
                    new Il { IlId = 73, Ad = "Şırnak" }, new Il { IlId = 74, Ad = "Bartın" }, new Il { IlId = 75, Ad = "Ardahan" },
                    new Il { IlId = 76, Ad = "Iğdır" }, new Il { IlId = 77, Ad = "Yalova" }, new Il { IlId = 78, Ad = "Karabük" },
                    new Il { IlId = 79, Ad = "Kilis" }, new Il { IlId = 80, Ad = "Osmaniye" }, new Il { IlId = 81, Ad = "Düzce" }
                );
            });

            modelBuilder.Entity<Ilce>(entity =>
            {
                entity.HasKey(e => e.IlceId);
                entity.Property(e => e.Ad).IsRequired().HasMaxLength(100);
                entity.HasOne(d => d.Il)
                    .WithMany(p => p.Ilceler)
                    .HasForeignKey(d => d.IlId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasData(IlceSeeder.GetDistricts());
            });

            modelBuilder.Entity<Personel>(entity =>
            {
                entity.HasKey(e => e.PersonelId);
                entity.Property(e => e.TcKimlikNo).IsRequired().HasMaxLength(11);
                entity.HasIndex(e => e.TcKimlikNo).IsUnique();
                entity.Property(e => e.Telefon).IsRequired().HasMaxLength(10).IsFixedLength();
                entity.Property(e => e.Eposta).IsRequired();
                entity.HasIndex(e => e.Eposta).IsUnique();
                entity.Property(e => e.SifreHash).IsRequired();
                entity.Property(e => e.SifreSalt).IsRequired();
                entity.Property(e => e.PersonelCinsiyet).HasColumnType("bit");
                entity.Property(e => e.DogumTarihi).HasColumnType("date");
                entity.Property(e => e.AktifMi).HasDefaultValue(true);

                entity.HasOne(d => d.GorevliIl)
                    .WithMany(p => p.Personeller)
                    .HasForeignKey(d => d.GorevliIlId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Brans)
                    .WithMany(p => p.Personeller)
                    .HasForeignKey(d => d.BransId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.SistemRol)
                    .WithMany()
                    .HasForeignKey(d => d.SistemRolId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Yazilim>(entity => {
                entity.HasKey(e => e.YazilimId);
                entity.Property(e => e.Ad).IsRequired();
                entity.HasIndex(e => e.Ad).IsUnique();
                entity.HasData(CatalogSeedData.GetYazilimSeed());
            });

            modelBuilder.Entity<Uzmanlik>(entity => {
                entity.HasKey(e => e.UzmanlikId);
                entity.Property(e => e.Ad).IsRequired();
                entity.HasIndex(e => e.Ad).IsUnique();
                entity.HasData(CatalogSeedData.GetUzmanlikSeed());
            });

            modelBuilder.Entity<GorevTuru>(entity => {
                entity.HasKey(e => e.GorevTuruId);
                entity.Property(e => e.Ad).IsRequired();
                entity.HasIndex(e => e.Ad).IsUnique();
                entity.HasData(CatalogSeedData.GetGorevTuruSeed());
            });

            modelBuilder.Entity<IsNiteligi>(entity => {
                entity.HasKey(e => e.IsNiteligiId);
                entity.Property(e => e.Ad).IsRequired();
                entity.HasIndex(e => e.Ad).IsUnique();
                entity.HasData(CatalogSeedData.GetIsNiteligiSeed());
            });

            modelBuilder.Entity<PersonelYazilim>(entity =>
            {
                entity.ToTable("PersonelYazilimlar");
                entity.HasKey(e => new { e.PersonelId, e.YazilimId });
                entity.HasOne(e => e.Personel).WithMany(p => p.PersonelYazilimlar).HasForeignKey(e => e.PersonelId).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.Yazilim).WithMany(y => y.PersonelYazilimlar).HasForeignKey(e => e.YazilimId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<PersonelUzmanlik>(entity =>
            {
                entity.ToTable("PersonelUzmanliklar");
                entity.HasKey(e => new { e.PersonelId, e.UzmanlikId });
                entity.HasOne(e => e.Personel).WithMany(p => p.PersonelUzmanliklar).HasForeignKey(e => e.PersonelId).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.Uzmanlik).WithMany(u => u.PersonelUzmanliklar).HasForeignKey(e => e.UzmanlikId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<PersonelGorevTuru>(entity =>
            {
                entity.ToTable("PersonelGorevTurleri");
                entity.HasKey(e => new { e.PersonelId, e.GorevTuruId });
                entity.HasOne(e => e.Personel).WithMany(p => p.PersonelGorevTurleri).HasForeignKey(e => e.PersonelId).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.GorevTuru).WithMany(g => g.PersonelGorevTurleri).HasForeignKey(e => e.GorevTuruId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<PersonelIsNiteligi>(entity =>
            {
                entity.ToTable("PersonelIsNitelikleri");
                entity.HasKey(e => new { e.PersonelId, e.IsNiteligiId });
                entity.HasOne(e => e.Personel).WithMany(p => p.PersonelIsNitelikleri).HasForeignKey(e => e.PersonelId).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.IsNiteligi).WithMany(i => i.PersonelIsNitelikleri).HasForeignKey(e => e.IsNiteligiId).OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
