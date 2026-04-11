using Microsoft.EntityFrameworkCore;
using PersonelTakipSistemi.Models;

namespace PersonelTakipSistemi.Data
{
    public partial class TegmPersonelTakipDbContext
    {
        private static void ConfigureDeviceModule(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CihazTuru>(entity =>
            {
                entity.ToTable("CihazTurleri");
                entity.HasKey(e => e.CihazTuruId);
                entity.Property(e => e.Ad).IsRequired().HasMaxLength(150);
                entity.Property(e => e.KullanimAmaci).HasMaxLength(300);
                entity.HasIndex(e => e.Ad).IsUnique();

                entity.HasData(
                    new CihazTuru { CihazTuruId = 1, Ad = "Masaüstü Bilgisayar", KullanimAmaci = "Ofis ve içerik üretimi" },
                    new CihazTuru { CihazTuruId = 2, Ad = "Dizüstü Bilgisayar", KullanimAmaci = "Mobil çalışma ve üretim" },
                    new CihazTuru { CihazTuruId = 3, Ad = "Monitör", KullanimAmaci = "Görüntüleme ve çoklu ekran" },
                    new CihazTuru { CihazTuruId = 4, Ad = "Çizim Tableti", KullanimAmaci = "Tasarım ve illüstrasyon" },
                    new CihazTuru { CihazTuruId = 5, Ad = "Fotoğraf Makinesi", KullanimAmaci = "Fotoğraf çekimi" },
                    new CihazTuru { CihazTuruId = 6, Ad = "Video Kamera", KullanimAmaci = "Video kayıt ve prodüksiyon" },
                    new CihazTuru { CihazTuruId = 7, Ad = "Mikrofon", KullanimAmaci = "Ses kayıt" },
                    new CihazTuru { CihazTuruId = 8, Ad = "Ses Kartı", KullanimAmaci = "Ses işleme" },
                    new CihazTuru { CihazTuruId = 9, Ad = "Kulaklık", KullanimAmaci = "Kurgu ve monitörleme" },
                    new CihazTuru { CihazTuruId = 10, Ad = "Yazıcı", KullanimAmaci = "Doküman çıktı alma" },
                    new CihazTuru { CihazTuruId = 11, Ad = "Tarayıcı", KullanimAmaci = "Doküman dijitalleştirme" },
                    new CihazTuru { CihazTuruId = 12, Ad = "Projektör", KullanimAmaci = "Sunum ve eğitim" },
                    new CihazTuru { CihazTuruId = 999, Ad = "Diğer", KullanimAmaci = "Sistemde tanımlı olmayan cihaz türleri", SistemSecenegiMi = true }
                );
            });

            modelBuilder.Entity<CihazMarka>(entity =>
            {
                entity.ToTable("CihazMarkalari");
                entity.HasKey(e => e.CihazMarkaId);
                entity.Property(e => e.Ad).IsRequired().HasMaxLength(150);
                entity.HasIndex(e => new { e.CihazTuruId, e.Ad }).IsUnique();
                entity.HasOne(e => e.CihazTuru)
                    .WithMany(t => t.Markalar)
                    .HasForeignKey(e => e.CihazTuruId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasData(
                    new CihazMarka { CihazMarkaId = 1001, CihazTuruId = 1, Ad = "Dell" },
                    new CihazMarka { CihazMarkaId = 1002, CihazTuruId = 1, Ad = "HP" },
                    new CihazMarka { CihazMarkaId = 1003, CihazTuruId = 1, Ad = "Lenovo" },
                    new CihazMarka { CihazMarkaId = 2001, CihazTuruId = 2, Ad = "Apple" },
                    new CihazMarka { CihazMarkaId = 2002, CihazTuruId = 2, Ad = "Dell" },
                    new CihazMarka { CihazMarkaId = 2003, CihazTuruId = 2, Ad = "Lenovo" },
                    new CihazMarka { CihazMarkaId = 2004, CihazTuruId = 2, Ad = "HP" },
                    new CihazMarka { CihazMarkaId = 3001, CihazTuruId = 3, Ad = "LG" },
                    new CihazMarka { CihazMarkaId = 3002, CihazTuruId = 3, Ad = "Samsung" },
                    new CihazMarka { CihazMarkaId = 3003, CihazTuruId = 3, Ad = "BenQ" },
                    new CihazMarka { CihazMarkaId = 4001, CihazTuruId = 4, Ad = "Wacom" },
                    new CihazMarka { CihazMarkaId = 4002, CihazTuruId = 4, Ad = "Huion" },
                    new CihazMarka { CihazMarkaId = 4003, CihazTuruId = 4, Ad = "XP-Pen" },
                    new CihazMarka { CihazMarkaId = 5001, CihazTuruId = 5, Ad = "Canon" },
                    new CihazMarka { CihazMarkaId = 5002, CihazTuruId = 5, Ad = "Sony" },
                    new CihazMarka { CihazMarkaId = 5003, CihazTuruId = 5, Ad = "Nikon" },
                    new CihazMarka { CihazMarkaId = 6001, CihazTuruId = 6, Ad = "Sony" },
                    new CihazMarka { CihazMarkaId = 6002, CihazTuruId = 6, Ad = "Canon" },
                    new CihazMarka { CihazMarkaId = 6003, CihazTuruId = 6, Ad = "Panasonic" },
                    new CihazMarka { CihazMarkaId = 7001, CihazTuruId = 7, Ad = "Rode" },
                    new CihazMarka { CihazMarkaId = 7002, CihazTuruId = 7, Ad = "Shure" },
                    new CihazMarka { CihazMarkaId = 7003, CihazTuruId = 7, Ad = "Audio-Technica" },
                    new CihazMarka { CihazMarkaId = 8001, CihazTuruId = 8, Ad = "Focusrite" },
                    new CihazMarka { CihazMarkaId = 8002, CihazTuruId = 8, Ad = "Zoom" },
                    new CihazMarka { CihazMarkaId = 9001, CihazTuruId = 9, Ad = "Sony" },
                    new CihazMarka { CihazMarkaId = 9002, CihazTuruId = 9, Ad = "Sennheiser" },
                    new CihazMarka { CihazMarkaId = 10001, CihazTuruId = 10, Ad = "HP" },
                    new CihazMarka { CihazMarkaId = 10002, CihazTuruId = 10, Ad = "Canon" },
                    new CihazMarka { CihazMarkaId = 11001, CihazTuruId = 11, Ad = "Epson" },
                    new CihazMarka { CihazMarkaId = 11002, CihazTuruId = 11, Ad = "Canon" },
                    new CihazMarka { CihazMarkaId = 12001, CihazTuruId = 12, Ad = "Epson" },
                    new CihazMarka { CihazMarkaId = 12002, CihazTuruId = 12, Ad = "ViewSonic" },
                    new CihazMarka { CihazMarkaId = 99999, CihazTuruId = 999, Ad = "Diğer", SistemSecenegiMi = true }
                );
            });

            modelBuilder.Entity<Cihaz>(entity =>
            {
                entity.ToTable("Cihazlar");
                entity.HasKey(e => e.CihazId);
                entity.Property(e => e.Model).IsRequired().HasMaxLength(150);
                entity.Property(e => e.Ozellikler).IsRequired().HasMaxLength(500);
                entity.Property(e => e.SeriNo).IsRequired().HasMaxLength(100);
                entity.Property(e => e.DigerCihazTuruAd).HasMaxLength(150);
                entity.Property(e => e.DigerMarkaAd).HasMaxLength(150);
                entity.Property(e => e.OnayDurumu).HasConversion<int>();
                entity.HasIndex(e => e.SeriNo);
                entity.HasIndex(e => new { e.KoordinatorlukId, e.OnayDurumu });

                entity.HasOne(e => e.CihazTuru).WithMany(t => t.Cihazlar).HasForeignKey(e => e.CihazTuruId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(e => e.CihazMarka).WithMany(m => m.Cihazlar).HasForeignKey(e => e.CihazMarkaId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(e => e.SahipPersonel).WithMany(p => p.SahipOlduguCihazlar).HasForeignKey(e => e.SahipPersonelId).OnDelete(DeleteBehavior.SetNull);
                entity.HasOne(e => e.OlusturanPersonel).WithMany(p => p.OlusturduguCihazKayitlari).HasForeignKey(e => e.OlusturanPersonelId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(e => e.OnaylayanPersonel).WithMany(p => p.OnayladigiCihazlar).HasForeignKey(e => e.OnaylayanPersonelId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(e => e.Koordinatorluk).WithMany(k => k.Cihazlar).HasForeignKey(e => e.KoordinatorlukId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<CihazHareketi>(entity =>
            {
                entity.ToTable("CihazHareketleri");
                entity.HasKey(e => e.CihazHareketiId);
                entity.Property(e => e.HareketTuru).HasConversion<int>();
                entity.Property(e => e.Aciklama).HasMaxLength(500);
                entity.Property(e => e.DurumNotu).HasMaxLength(500);
                entity.Property(e => e.IslemYapanAdSoyad).HasMaxLength(200);
                entity.Property(e => e.OncekiSahipAdSoyad).HasMaxLength(200);
                entity.Property(e => e.YeniSahipAdSoyad).HasMaxLength(200);
                entity.HasIndex(e => new { e.CihazId, e.Tarih });

                entity.HasOne(e => e.Cihaz).WithMany(c => c.Hareketler).HasForeignKey(e => e.CihazId).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.OncekiSahipPersonel).WithMany().HasForeignKey(e => e.OncekiSahipPersonelId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(e => e.YeniSahipPersonel).WithMany().HasForeignKey(e => e.YeniSahipPersonelId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(e => e.IslemYapanPersonel).WithMany(p => p.YaptigiCihazHareketleri).HasForeignKey(e => e.IslemYapanPersonelId).OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<YazilimLisans>(entity =>
            {
                entity.ToTable("YazilimLisanslar");
                entity.HasKey(e => e.YazilimLisansId);
                entity.Property(e => e.MaksimumLisansHesapAdedi).IsRequired();
                entity.Property(e => e.LisansSuresiTuru).HasConversion<int>();
                entity.Property(e => e.KullanimAmaci).HasMaxLength(500);
                entity.Property(e => e.HesapBasiOrtakKullanimBilgisi).HasMaxLength(200);
                entity.HasIndex(e => e.YazilimId);

                entity.HasOne(e => e.Yazilim)
                    .WithMany()
                    .HasForeignKey(e => e.YazilimId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.OlusturanPersonel)
                    .WithMany()
                    .HasForeignKey(e => e.OlusturanPersonelId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<YazilimLisansKullanici>(entity =>
            {
                entity.ToTable("YazilimLisansKullanicilar");
                entity.HasKey(e => e.YazilimLisansKullaniciId);
                entity.Property(e => e.KullaniciAdi).HasMaxLength(200);
                entity.Property(e => e.Eposta).HasMaxLength(250);
                entity.Property(e => e.OnayDurumu).HasConversion<int>();
                entity.HasIndex(e => new { e.YazilimLisansId, e.PersonelId }).IsUnique();
                entity.HasIndex(e => e.PersonelId);

                entity.HasOne(e => e.YazilimLisans)
                    .WithMany(x => x.Kullanicilar)
                    .HasForeignKey(e => e.YazilimLisansId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Personel)
                    .WithMany()
                    .HasForeignKey(e => e.PersonelId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.OnaylayanPersonel)
                    .WithMany()
                    .HasForeignKey(e => e.OnaylayanPersonelId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(e => e.KaydiOlusturanPersonel)
                    .WithMany()
                    .HasForeignKey(e => e.KaydiOlusturanPersonelId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}

