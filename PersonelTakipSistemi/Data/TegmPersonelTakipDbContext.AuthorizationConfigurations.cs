using Microsoft.EntityFrameworkCore;
using PersonelTakipSistemi.Models;

namespace PersonelTakipSistemi.Data
{
    public partial class TegmPersonelTakipDbContext
    {
        private static void ConfigureAuthorizationModule(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Teskilat>(entity => {
                entity.ToTable("Teskilatlar");
                entity.HasKey(e => e.TeskilatId);

                entity.HasOne(t => t.DaireBaskanligi)
                      .WithMany(d => d.Teskilatlar)
                      .HasForeignKey(t => t.DaireBaskanligiId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasData(
                    new Teskilat { TeskilatId = 1, Ad = "Merkez", DaireBaskanligiId = 9, Tur = "Merkez", TasraOrgutlenmesiVarMi = true },
                    new Teskilat { TeskilatId = 2, Ad = "Taşra", DaireBaskanligiId = 9, Tur = "Taşra", BagliMerkezTeskilatId = 1 }
                );
            });

            modelBuilder.Entity<Koordinatorluk>(entity => {
                entity.ToTable("Koordinatorlukler");
                entity.HasKey(e => e.KoordinatorlukId);

                entity.HasOne(d => d.Teskilat)
                    .WithMany(p => p.Koordinatorlukler)
                    .HasForeignKey(d => d.TeskilatId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.BaskanPersonel)
                    .WithMany()
                    .HasForeignKey(d => d.BaskanPersonelId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasData(
                    new Koordinatorluk { KoordinatorlukId = 4, Ad = "Fen Bilimleri Birim Koordinatörlüğü", TeskilatId = 1 },
                    new Koordinatorluk { KoordinatorlukId = 5, Ad = "İngilizce Birim Koordinatörlüğü", TeskilatId = 1 },
                    new Koordinatorluk { KoordinatorlukId = 6, Ad = "İlkokul Türkçe Birim Koordinatörlüğü", TeskilatId = 1 },
                    new Koordinatorluk { KoordinatorlukId = 7, Ad = "İlkokul Hayat Bilgisi Birim Koordinatörlüğü", TeskilatId = 1 },
                    new Koordinatorluk { KoordinatorlukId = 8, Ad = "Ortaokul Matematik Birim Koordinatörlüğü", TeskilatId = 1 },
                    new Koordinatorluk { KoordinatorlukId = 9, Ad = "İlkokul Matematik Birim Koordinatörlüğü", TeskilatId = 1 },
                    new Koordinatorluk { KoordinatorlukId = 10, Ad = "Ortaokul Türkçe Birim Koordinatörlüğü", TeskilatId = 1 },
                    new Koordinatorluk { KoordinatorlukId = 11, Ad = "Sosyal Bilgiler Birim Koordinatörlüğü", TeskilatId = 1 },
                    new Koordinatorluk { KoordinatorlukId = 12, Ad = "T.C. İnkılap Tarihi ve Atatürkçülük Birim Koordinatörlüğü", TeskilatId = 1 },
                    new Koordinatorluk { KoordinatorlukId = 13, Ad = "Almanca Birim Koordinatörlüğü", TeskilatId = 1 },
                    new Koordinatorluk { KoordinatorlukId = 14, Ad = "Görsel Tasarım Birim Koordinatörlüğü", TeskilatId = 1 },
                    new Koordinatorluk { KoordinatorlukId = 15, Ad = "Mebi Dijital Birim Koordinatörlüğü", TeskilatId = 1 },
                    new Koordinatorluk { KoordinatorlukId = 16, Ad = "Müzik Birim Koordinatörlüğü", TeskilatId = 1 },
                    new Koordinatorluk { KoordinatorlukId = 17, Ad = "Uzmanlar Birim Koordinatörlüğü", TeskilatId = 1 },
                    new Koordinatorluk { KoordinatorlukId = 18, Ad = "BÖTE Birim Koordinatörlüğü", TeskilatId = 1 },
                    new Koordinatorluk { KoordinatorlukId = 19, Ad = "Dil İnceleme Birim Koordinatörlüğü", TeskilatId = 1 },
                    new Koordinatorluk { KoordinatorlukId = 2, Ad = "Mardin İl Koordinatörlüğü", TeskilatId = 2 },
                    new Koordinatorluk { KoordinatorlukId = 3, Ad = "İzmir İl Koordinatörlüğü", TeskilatId = 2 }
                );
            });

            modelBuilder.Entity<Komisyon>(entity => {
                entity.ToTable("Komisyonlar");
                entity.HasKey(e => e.KomisyonId);

                entity.HasOne(d => d.Koordinatorluk)
                    .WithMany(p => p.Komisyonlar)
                    .HasForeignKey(d => d.KoordinatorlukId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.BaskanPersonel)
                    .WithMany()
                    .HasForeignKey(d => d.BaskanPersonelId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<KurumsalRol>(entity => {
                entity.ToTable("KurumsalRoller");
                entity.HasKey(e => e.KurumsalRolId);
                entity.HasData(
                    new KurumsalRol { KurumsalRolId = 1, Ad = "Personel", SadeceMerkezMi = false },
                    new KurumsalRol { KurumsalRolId = 2, Ad = "Komisyon Başkanı", SadeceMerkezMi = false },
                    new KurumsalRol { KurumsalRolId = 3, Ad = "İl Koordinatörü", SadeceMerkezMi = false },
                    new KurumsalRol { KurumsalRolId = 4, Ad = "Genel Koordinatör", SadeceMerkezMi = true },
                    new KurumsalRol { KurumsalRolId = 5, Ad = "Merkez Birim Koordinatörlüğü", SadeceMerkezMi = true },
                    new KurumsalRol { KurumsalRolId = 6, Ad = "Uzman", SadeceMerkezMi = true },
                    new KurumsalRol { KurumsalRolId = 7, Ad = "Şube Müdürü", SadeceMerkezMi = true },
                    new KurumsalRol { KurumsalRolId = 8, Ad = "Şef", SadeceMerkezMi = true },
                    new KurumsalRol { KurumsalRolId = 9, Ad = "Daire Başkanı", SadeceMerkezMi = true },
                    new KurumsalRol { KurumsalRolId = 10, Ad = "Genel Müdür", SadeceMerkezMi = true }
                );
            });

            modelBuilder.Entity<SistemRol>(entity => {
                entity.ToTable("SistemRoller");
                entity.HasKey(e => e.SistemRolId);
                entity.HasData(
                    new SistemRol { SistemRolId = 1, Ad = "Admin" },
                    new SistemRol { SistemRolId = 2, Ad = "Yönetici" },
                    new SistemRol { SistemRolId = 3, Ad = "Editör" },
                    new SistemRol { SistemRolId = 4, Ad = "Kullanıcı" }
                );
            });

            modelBuilder.Entity<Bildirim>(entity =>
            {
                entity.ToTable("Bildirimler");
                entity.HasKey(e => e.BildirimId);
                entity.Property(e => e.OlusturmaTarihi).HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.OkunduMu).HasDefaultValue(false);

                entity.HasOne(d => d.AliciPersonel)
                    .WithMany(p => p.GelenBildirimler)
                    .HasForeignKey(d => d.AliciPersonelId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.GonderenPersonel)
                    .WithMany(p => p.GonderilenBildirimler)
                    .HasForeignKey(d => d.GonderenPersonelId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(e => new { e.AliciPersonelId, e.OkunduMu, e.OlusturmaTarihi });
                entity.HasIndex(e => new { e.AliciPersonelId, e.OlusturmaTarihi });
            });

            modelBuilder.Entity<PersonelTeskilat>(entity => {
                entity.ToTable("PersonelTeskilatlar");
                entity.HasKey(e => new { e.PersonelId, e.TeskilatId });
                entity.HasOne(e => e.Personel).WithMany(p => p.PersonelTeskilatlar).HasForeignKey(e => e.PersonelId).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.Teskilat).WithMany(t => t.PersonelTeskilatlar).HasForeignKey(e => e.TeskilatId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<PersonelKoordinatorluk>(entity => {
                entity.ToTable("PersonelKoordinatorlukler");
                entity.HasKey(e => new { e.PersonelId, e.KoordinatorlukId });
                entity.HasOne(e => e.Personel).WithMany(p => p.PersonelKoordinatorlukler).HasForeignKey(e => e.PersonelId).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.Koordinatorluk).WithMany(k => k.PersonelKoordinatorlukler).HasForeignKey(e => e.KoordinatorlukId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<PersonelKomisyon>(entity => {
                entity.ToTable("PersonelKomisyonlar");
                entity.HasKey(e => new { e.PersonelId, e.KomisyonId });
                entity.HasOne(e => e.Personel).WithMany(p => p.PersonelKomisyonlar).HasForeignKey(e => e.PersonelId).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.Komisyon).WithMany(k => k.PersonelKomisyonlar).HasForeignKey(e => e.KomisyonId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<PersonelKurumsalRolAtama>(entity => {
                entity.ToTable("PersonelKurumsalRolAtamalari");
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.Personel)
                   .WithMany(p => p.PersonelKurumsalRolAtamalari)
                   .HasForeignKey(e => e.PersonelId)
                   .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.KurumsalRol)
                   .WithMany()
                   .HasForeignKey(e => e.KurumsalRolId)
                   .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Koordinatorluk)
                   .WithMany()
                   .HasForeignKey(e => e.KoordinatorlukId)
                   .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Komisyon)
                   .WithMany()
                   .HasForeignKey(e => e.KomisyonId)
                   .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}

