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
                    new Birim { BirimId = 1, Ad = "YazÄ±lÄ±m Birimi" },
                    new Birim { BirimId = 2, Ad = "Ä°Ã§erik Birimi" },
                    new Birim { BirimId = 3, Ad = "Grafik Birimi" }
                );
            });

            modelBuilder.Entity<GorevKategori>(entity => {
                entity.ToTable("GorevKategorileri");
                entity.HasKey(e => e.GorevKategoriId);
                entity.Property(e => e.Ad).IsRequired().HasMaxLength(150);
                entity.HasIndex(e => e.Ad).IsUnique();

                entity.HasData(
                    new GorevKategori { GorevKategoriId = 1, Ad = "Ders KitaplarÄ±", Aciklama = "Ders kitabÄ± hazÄ±rlÄ±k iÅŸleri", Renk = "#3B82F6" },
                    new GorevKategori { GorevKategoriId = 2, Ad = "YardÄ±mcÄ± Kaynaklar", Aciklama = "Soru bankasÄ± ve etkinlikler", Renk = "#10B981" },
                    new GorevKategori { GorevKategoriId = 3, Ad = "Dijital Ä°Ã§erik", Aciklama = "Video ve animasyon iÅŸleri", Renk = "#F59E0B" },
                    new GorevKategori { GorevKategoriId = 4, Ad = "Programlar", Aciklama = "MÃ¼fredat Ã§alÄ±ÅŸmalarÄ±", Renk = "#8B5CF6" }
                );
            });

            modelBuilder.Entity<GorevDurum>(entity => {
                entity.ToTable("GorevDurumlari");
                entity.HasKey(e => e.GorevDurumId);
                entity.Property(e => e.Ad).IsRequired().HasMaxLength(100);

                entity.HasData(
                    new GorevDurum { GorevDurumId = 1, Ad = "AtanmayÄ± Bekliyor", Sira = 1, RenkSinifi = "bg-warning", Renk = "#F59E0B" },
                    new GorevDurum { GorevDurumId = 2, Ad = "Devam Ediyor", Sira = 2, RenkSinifi = "bg-primary", Renk = "#3B82F6" },
                    new GorevDurum { GorevDurumId = 3, Ad = "Kontrolde", Sira = 3, RenkSinifi = "bg-info", Renk = "#06B6D4" },
                    new GorevDurum { GorevDurumId = 4, Ad = "TamamlandÄ±", Sira = 4, RenkSinifi = "bg-success", Renk = "#10B981" },
                    new GorevDurum { GorevDurumId = 5, Ad = "Ä°ptal", Sira = 5, RenkSinifi = "bg-secondary", Renk = "#6B7280" }
                );
            });

            modelBuilder.Entity<Gorev>(entity => {
                entity.ToTable("Gorevler");
                entity.HasKey(e => e.GorevId);
                entity.Property(e => e.Ad).IsRequired().HasMaxLength(200);

                entity.HasOne(e => e.Kategori)
                    .WithMany(k => k.Gorevler)
                    .HasForeignKey(e => e.KategoriId)
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

                entity.HasIndex(e => e.KategoriId);
                entity.HasIndex(e => e.PersonelId);
                entity.HasIndex(e => e.GorevDurumId);
                entity.HasIndex(e => e.BaslangicTarihi);

                var tasks = new List<Gorev>();
                int idCounter = 1;

                tasks.Add(new Gorev { GorevId = idCounter++, Ad = "Matematik 9 Kitap Dizgisi", Aciklama = "Dizgi taslaÄŸÄ±nÄ± hazÄ±rla", KategoriId = 1, PersonelId = 1, BirimId = 3, GorevDurumId = 2, BaslangicTarihi = new DateTime(2025, 11, 01), BitisTarihi = new DateTime(2025, 11, 20) });
                tasks.Add(new Gorev { GorevId = idCounter++, Ad = "Fizik 10 Kapak TasarÄ±mÄ±", Aciklama = "Kapak gÃ¶rseli revizesi", KategoriId = 1, PersonelId = 1, BirimId = 3, GorevDurumId = 3, BaslangicTarihi = new DateTime(2025, 11, 05), BitisTarihi = new DateTime(2025, 11, 08) });
                tasks.Add(new Gorev { GorevId = idCounter++, Ad = "Kimya 11 YazÄ±m Denetimi", Aciklama = "YazÄ±m hatalarÄ±nÄ±n kontrolÃ¼", KategoriId = 1, PersonelId = 1, BirimId = 2, GorevDurumId = 1, BaslangicTarihi = new DateTime(2025, 12, 01) });

                tasks.Add(new Gorev { GorevId = idCounter++, Ad = "LGS Soru BankasÄ±", Aciklama = "Soru giriÅŸleri", KategoriId = 2, PersonelId = 1, BirimId = 2, GorevDurumId = 2, BaslangicTarihi = new DateTime(2025, 11, 15), BitisTarihi = new DateTime(2025, 12, 15) });
                tasks.Add(new Gorev { GorevId = idCounter++, Ad = "YKS Deneme Seti", Aciklama = "BaskÄ± Ã¶ncesi kontrol", KategoriId = 2, PersonelId = 1, BirimId = 3, GorevDurumId = 2, BaslangicTarihi = new DateTime(2025, 11, 25) });
                tasks.Add(new Gorev { GorevId = idCounter++, Ad = "Etkinlik YapraklarÄ±", Aciklama = "Ä°lkokul seviyesi gÃ¶rselleÅŸtirme", KategoriId = 2, PersonelId = 1, BirimId = 3, GorevDurumId = 4, BaslangicTarihi = new DateTime(2025, 10, 20), BitisTarihi = new DateTime(2025, 10, 25) });

                tasks.Add(new Gorev { GorevId = idCounter++, Ad = "EBA Video Montaj", Aciklama = "Ders videolarÄ± kurgusu", KategoriId = 3, PersonelId = 1, BirimId = 1, GorevDurumId = 1, BaslangicTarihi = new DateTime(2025, 12, 05) });
                tasks.Add(new Gorev { GorevId = idCounter++, Ad = "Animasyon Karakterleri", Aciklama = "Karakter Ã§izimleri", KategoriId = 3, PersonelId = 1, BirimId = 3, GorevDurumId = 2, BaslangicTarihi = new DateTime(2025, 11, 10), BitisTarihi = new DateTime(2025, 12, 30) });
                tasks.Add(new Gorev { GorevId = idCounter++, Ad = "Seslendirme KayÄ±tlarÄ±", Aciklama = "StÃ¼dyo planlamasÄ±", KategoriId = 3, PersonelId = 1, BirimId = 2, GorevDurumId = 4, BaslangicTarihi = new DateTime(2025, 11, 01), BitisTarihi = new DateTime(2025, 11, 02) });

                tasks.Add(new Gorev { GorevId = idCounter++, Ad = "MÃ¼fredat Ä°ncelemesi", Aciklama = "Talim Terbiye notlarÄ±", KategoriId = 4, PersonelId = 1, BirimId = 2, GorevDurumId = 2, BaslangicTarihi = new DateTime(2025, 12, 10) });
                tasks.Add(new Gorev { GorevId = idCounter++, Ad = "KazanÄ±m EÅŸleÅŸtirme", Aciklama = "Excel tablosu hazÄ±rlÄ±ÄŸÄ±", KategoriId = 4, PersonelId = 1, BirimId = 2, GorevDurumId = 1, BaslangicTarihi = new DateTime(2025, 12, 12) });
                tasks.Add(new Gorev { GorevId = idCounter++, Ad = "HaftalÄ±k Plan", Aciklama = "2. DÃ¶nem planlamasÄ±", KategoriId = 4, PersonelId = 1, BirimId = 1, GorevDurumId = 4, BaslangicTarihi = new DateTime(2025, 11, 28), BitisTarihi = new DateTime(2025, 11, 30) });

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
            });
        }
    }
}
