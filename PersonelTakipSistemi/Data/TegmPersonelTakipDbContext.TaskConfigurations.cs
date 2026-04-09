using Microsoft.EntityFrameworkCore;
using PersonelTakipSistemi.Models;

namespace PersonelTakipSistemi.Data
{
    public partial class TegmPersonelTakipDbContext
    {
        private static void ConfigureTaskModule(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Birim>(entity => {
                entity.ToTable("Birimler");
                entity.HasKey(e => e.BirimId);
                entity.Property(e => e.Ad).IsRequired().HasMaxLength(150);

                entity.HasData(
                    new Birim { BirimId = 1, Ad = "Yazılım Birimi" },
                    new Birim { BirimId = 2, Ad = "İçerik Birimi" },
                    new Birim { BirimId = 3, Ad = "Grafik Birimi" }
                );
            });

            modelBuilder.Entity<GorevDurum>(entity => {
                entity.ToTable("GorevDurumlari");
                entity.HasKey(e => e.GorevDurumId);
                entity.Property(e => e.Ad).IsRequired().HasMaxLength(100);

                entity.HasData(
                    new GorevDurum { GorevDurumId = 1, Ad = "Atanmayı Bekliyor", Sira = 1, RenkSinifi = "bg-warning", Renk = "#F59E0B" },
                    new GorevDurum { GorevDurumId = 2, Ad = "Devam Ediyor", Sira = 2, RenkSinifi = "bg-primary", Renk = "#3B82F6" },
                    new GorevDurum { GorevDurumId = 3, Ad = "Kontrolde", Sira = 3, RenkSinifi = "bg-info", Renk = "#06B6D4" },
                    new GorevDurum { GorevDurumId = 4, Ad = "Tamamlandı", Sira = 4, RenkSinifi = "bg-success", Renk = "#10B981" },
                    new GorevDurum { GorevDurumId = 5, Ad = "İptal", Sira = 5, RenkSinifi = "bg-secondary", Renk = "#6B7280" }
                );
            });

            modelBuilder.Entity<Gorev>(entity => {
                entity.ToTable("Gorevler");
                entity.HasKey(e => e.GorevId);
                entity.Property(e => e.Ad).IsRequired().HasMaxLength(200);

                entity.HasOne(e => e.IsNiteligi)
                    .WithMany(k => k.Gorevler)
                    .HasForeignKey(e => e.IsNiteligiId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Personel)
                    .WithMany()
                    .HasForeignKey(e => e.PersonelId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.CreatedByPersonel)
                    .WithMany()
                    .HasForeignKey(e => e.CreatedByPersonelId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(e => e.Birim)
                    .WithMany(b => b.Gorevler)
                    .HasForeignKey(e => e.BirimId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(e => e.GorevDurum)
                    .WithMany(d => d.Gorevler)
                    .HasForeignKey(e => e.GorevDurumId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(e => e.IsNiteligiId);
                entity.HasIndex(e => e.PersonelId);
                entity.HasIndex(e => e.GorevDurumId);
                entity.HasIndex(e => e.BaslangicTarihi);

                var tasks = new List<Gorev>();
                int idCounter = 1;

                tasks.Add(new Gorev { GorevId = idCounter++, Ad = "Matematik 9 Kitap Dizgisi", Aciklama = "Dizgi taslağını hazırla", IsNiteligiId = 1, PersonelId = 1, BirimId = 3, GorevDurumId = 2, BaslangicTarihi = new DateTime(2025, 11, 01), BitisTarihi = new DateTime(2025, 11, 20) });
                tasks.Add(new Gorev { GorevId = idCounter++, Ad = "Fizik 10 Kapak Tasarımı", Aciklama = "Kapak görseli revizesi", IsNiteligiId = 1, PersonelId = 1, BirimId = 3, GorevDurumId = 3, BaslangicTarihi = new DateTime(2025, 11, 05), BitisTarihi = new DateTime(2025, 11, 08) });
                tasks.Add(new Gorev { GorevId = idCounter++, Ad = "Kimya 11 Yazım Denetimi", Aciklama = "Yazım hatalarının kontrolü", IsNiteligiId = 1, PersonelId = 1, BirimId = 2, GorevDurumId = 1, BaslangicTarihi = new DateTime(2025, 12, 01) });

                tasks.Add(new Gorev { GorevId = idCounter++, Ad = "LGS Soru Bankası", Aciklama = "Soru girişleri", IsNiteligiId = 2, PersonelId = 1, BirimId = 2, GorevDurumId = 2, BaslangicTarihi = new DateTime(2025, 11, 15), BitisTarihi = new DateTime(2025, 12, 15) });
                tasks.Add(new Gorev { GorevId = idCounter++, Ad = "YKS Deneme Seti", Aciklama = "Baskı öncesi kontrol", IsNiteligiId = 2, PersonelId = 1, BirimId = 3, GorevDurumId = 2, BaslangicTarihi = new DateTime(2025, 11, 25) });
                tasks.Add(new Gorev { GorevId = idCounter++, Ad = "Etkinlik Yaprakları", Aciklama = "İlkokul seviyesi görselleştirme", IsNiteligiId = 2, PersonelId = 1, BirimId = 3, GorevDurumId = 4, BaslangicTarihi = new DateTime(2025, 10, 20), BitisTarihi = new DateTime(2025, 10, 25) });

                tasks.Add(new Gorev { GorevId = idCounter++, Ad = "EBA Video Montaj", Aciklama = "Ders videoları kurgusu", IsNiteligiId = 9, PersonelId = 1, BirimId = 1, GorevDurumId = 1, BaslangicTarihi = new DateTime(2025, 12, 05) });
                tasks.Add(new Gorev { GorevId = idCounter++, Ad = "Animasyon Karakterleri", Aciklama = "Karakter çizimleri", IsNiteligiId = 5, PersonelId = 1, BirimId = 3, GorevDurumId = 2, BaslangicTarihi = new DateTime(2025, 11, 10), BitisTarihi = new DateTime(2025, 12, 30) });
                tasks.Add(new Gorev { GorevId = idCounter++, Ad = "Seslendirme Kayıtları", Aciklama = "Stüdyo planlaması", IsNiteligiId = 8, PersonelId = 1, BirimId = 2, GorevDurumId = 4, BaslangicTarihi = new DateTime(2025, 11, 01), BitisTarihi = new DateTime(2025, 11, 02) });

                tasks.Add(new Gorev { GorevId = idCounter++, Ad = "Müfredat İncelemesi", Aciklama = "Talim Terbiye notları", IsNiteligiId = 10, PersonelId = 1, BirimId = 2, GorevDurumId = 2, BaslangicTarihi = new DateTime(2025, 12, 10) });
                tasks.Add(new Gorev { GorevId = idCounter++, Ad = "Kazanım Eşleştirme", Aciklama = "Excel tablosu hazırlığı", IsNiteligiId = 10, PersonelId = 1, BirimId = 2, GorevDurumId = 1, BaslangicTarihi = new DateTime(2025, 12, 12) });
                tasks.Add(new Gorev { GorevId = idCounter++, Ad = "Haftalık Plan", Aciklama = "2. Dönem planlaması", IsNiteligiId = 10, PersonelId = 1, BirimId = 1, GorevDurumId = 4, BaslangicTarihi = new DateTime(2025, 11, 28), BitisTarihi = new DateTime(2025, 11, 30) });

                entity.HasData(tasks);
            });

            modelBuilder.Entity<GorevDurumGecmisi>(entity => {
                entity.ToTable("GorevDurumGecmisleri");
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.Gorev)
                    .WithMany(g => g.GorevDurumGecmisleri)
                    .HasForeignKey(e => e.GorevId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.GorevDurum)
                    .WithMany()
                    .HasForeignKey(e => e.GorevDurumId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.IslemYapanPersonel)
                    .WithMany()
                    .HasForeignKey(e => e.IslemYapanPersonelId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<GorevAtamaTeskilat>(entity => {
                entity.HasKey(e => new { e.GorevId, e.TeskilatId });
                entity.HasOne(e => e.Gorev)
                    .WithMany(g => g.GorevAtamaTeskilatlar)
                    .HasForeignKey(e => e.GorevId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.Teskilat)
                    .WithMany()
                    .HasForeignKey(e => e.TeskilatId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.GorevTuru)
                    .WithMany()
                    .HasForeignKey(e => e.GorevTuruId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(e => e.GorevTuruId);
            });

            modelBuilder.Entity<GorevAtamaKoordinatorluk>(entity => {
                entity.HasKey(e => new { e.GorevId, e.KoordinatorlukId });
                entity.HasOne(e => e.Gorev)
                    .WithMany(g => g.GorevAtamaKoordinatorlukler)
                    .HasForeignKey(e => e.GorevId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.Koordinatorluk)
                    .WithMany()
                    .HasForeignKey(e => e.KoordinatorlukId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.GorevTuru)
                    .WithMany()
                    .HasForeignKey(e => e.GorevTuruId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(e => e.GorevTuruId);
            });

            modelBuilder.Entity<GorevAtamaKomisyon>(entity => {
                entity.HasKey(e => new { e.GorevId, e.KomisyonId });
                entity.HasOne(e => e.Gorev)
                    .WithMany(g => g.GorevAtamaKomisyonlar)
                    .HasForeignKey(e => e.GorevId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.Komisyon)
                    .WithMany()
                    .HasForeignKey(e => e.KomisyonId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.GorevTuru)
                    .WithMany()
                    .HasForeignKey(e => e.GorevTuruId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(e => e.GorevTuruId);
            });

            modelBuilder.Entity<GorevAtamaPersonel>(entity => {
                entity.HasKey(e => new { e.GorevId, e.PersonelId });
                entity.HasOne(e => e.Gorev)
                    .WithMany(g => g.GorevAtamaPersoneller)
                    .HasForeignKey(e => e.GorevId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.Personel)
                    .WithMany()
                    .HasForeignKey(e => e.PersonelId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.GorevTuru)
                    .WithMany()
                    .HasForeignKey(e => e.GorevTuruId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(e => e.GorevTuruId);
            });
        }
    }
}
