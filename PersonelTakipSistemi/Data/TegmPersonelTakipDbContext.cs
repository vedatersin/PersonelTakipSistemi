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
        public DbSet<Il> Iller { get; set; }
        public DbSet<Brans> Branslar { get; set; }

        public DbSet<PersonelYazilim> PersonelYazilimlar { get; set; }
        public DbSet<PersonelUzmanlik> PersonelUzmanliklar { get; set; }
        public DbSet<PersonelGorevTuru> PersonelGorevTurleri { get; set; }

        public DbSet<PersonelIsNiteligi> PersonelIsNitelikleri { get; set; }

        // Authorization Module DbSets
        public DbSet<Teskilat> Teskilatlar { get; set; }
        public DbSet<Koordinatorluk> Koordinatorlukler { get; set; }
        public DbSet<Komisyon> Komisyonlar { get; set; }
        public DbSet<KurumsalRol> KurumsalRoller { get; set; }
        public DbSet<SistemRol> SistemRoller { get; set; }
        public DbSet<Bildirim> Bildirimler { get; set; }
        public DbSet<PersonelTeskilat> PersonelTeskilatlar { get; set; }
        public DbSet<PersonelKoordinatorluk> PersonelKoordinatorlukler { get; set; }
        public DbSet<PersonelKomisyon> PersonelKomisyonlar { get; set; }
        public DbSet<PersonelKurumsalRolAtama> PersonelKurumsalRolAtamalari { get; set; }

        public DbSet<BildirimGonderen> BildirimGonderenler { get; set; }
        public DbSet<TopluBildirim> TopluBildirimler { get; set; }
        public DbSet<SistemLog> SistemLoglar { get; set; }


        // Görevler Module DbSets
        public DbSet<GorevKategori> GorevKategorileri { get; set; }
        public DbSet<GorevDurum> GorevDurumlari { get; set; }
        public DbSet<Birim> Birimler { get; set; }
        public DbSet<Gorev> Gorevler { get; set; }
        public DbSet<GorevDurumGecmisi> GorevDurumGecmisleri { get; set; }

        public DbSet<GorevAtamaTeskilat> GorevAtamaTeskilatlar { get; set; }
        public DbSet<GorevAtamaKoordinatorluk> GorevAtamaKoordinatorlukler { get; set; }
        public DbSet<GorevAtamaKomisyon> GorevAtamaKomisyonlar { get; set; }
        public DbSet<GorevAtamaPersonel> GorevAtamaPersoneller { get; set; }
        public DbSet<DaireBaskanligi> DaireBaskanliklari { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Table Mappings
            modelBuilder.Entity<Personel>().ToTable("Personeller");
            modelBuilder.Entity<Yazilim>().ToTable("Yazilimlar");
            modelBuilder.Entity<Uzmanlik>().ToTable("Uzmanliklar");
            modelBuilder.Entity<GorevTuru>().ToTable("GorevTurleri");
            modelBuilder.Entity<IsNiteligi>().ToTable("IsNitelikleri");
            modelBuilder.Entity<Il>().ToTable("Iller");
            modelBuilder.Entity<Brans>().ToTable("Branslar");

            // Daire Başkanlıkları Seed
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

            // Notification Module
            modelBuilder.Entity<BildirimGonderen>(entity => {
                entity.ToTable("BildirimGonderenler");
                entity.HasOne(e => e.Personel).WithMany().HasForeignKey(e => e.PersonelId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<TopluBildirim>(entity => {
                entity.ToTable("TopluBildirimler");
                entity.HasOne(e => e.Gonderen).WithMany().HasForeignKey(e => e.GonderenId).OnDelete(DeleteBehavior.Restrict);
            });
            
            modelBuilder.Entity<Bildirim>(entity => {
                 entity.HasOne(d => d.BildirimGonderen).WithMany().HasForeignKey(d => d.BildirimGonderenId).OnDelete(DeleteBehavior.Restrict);
                 entity.HasOne(d => d.TopluBildirim).WithMany().HasForeignKey(d => d.TopluBildirimId).OnDelete(DeleteBehavior.SetNull); // If batch deleted, keep notifications? Or Cascade? SetNull safe.
            });

            // Brans Constraints
            modelBuilder.Entity<Brans>(entity =>
            {
                entity.HasKey(e => e.BransId);
                entity.Property(e => e.Ad).IsRequired().HasMaxLength(250);
                entity.HasIndex(e => e.Ad).IsUnique();

                // Seed Data
                entity.HasData(GetBransSeed());
            });
            
             // Il Constraints & Seed
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


            // Personel Constraints
            modelBuilder.Entity<Personel>(entity =>
            {
                entity.HasKey(e => e.PersonelId);
                entity.Property(e => e.TcKimlikNo).IsRequired().HasMaxLength(11);
                entity.HasIndex(e => e.TcKimlikNo).IsUnique();
                
                // Telefon: char(10) fix
                entity.Property(e => e.Telefon)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength(); 

                entity.Property(e => e.Eposta).IsRequired();
                entity.HasIndex(e => e.Eposta).IsUnique();
                entity.Property(e => e.SifreHash).IsRequired();
                entity.Property(e => e.SifreSalt).IsRequired();
                entity.Property(e => e.PersonelCinsiyet).HasColumnType("bit");
                
                // DogumTarihi: date
                entity.Property(e => e.DogumTarihi).HasColumnType("date");

                // AktifMi: Default true
                entity.Property(e => e.AktifMi).HasDefaultValue(true);

                // Relationships
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

            // Yazilim/Uzmanlik/GorevTuru/IsNiteligi Constraints & Seeding
            modelBuilder.Entity<Yazilim>(entity => {
                entity.HasKey(e => e.YazilimId);
                entity.Property(e => e.Ad).IsRequired();
                entity.HasIndex(e => e.Ad).IsUnique();
                entity.HasData(GetYazilimSeed());
            });

            modelBuilder.Entity<Uzmanlik>(entity => {
                entity.HasKey(e => e.UzmanlikId);
                entity.Property(e => e.Ad).IsRequired();
                entity.HasIndex(e => e.Ad).IsUnique();
                entity.HasData(GetUzmanlikSeed());
            });

            modelBuilder.Entity<GorevTuru>(entity => {
                entity.HasKey(e => e.GorevTuruId);
                entity.Property(e => e.Ad).IsRequired();
                entity.HasIndex(e => e.Ad).IsUnique();
                entity.HasData(GetGorevTuruSeed());
            });

            modelBuilder.Entity<IsNiteligi>(entity => {
                entity.HasKey(e => e.IsNiteligiId);
                entity.Property(e => e.Ad).IsRequired();
                entity.HasIndex(e => e.Ad).IsUnique();
                entity.HasData(GetIsNiteligiSeed());
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

            // ============================================================
            // Authorization Module Configuration
            // ============================================================

            // 1. Teskilat
            modelBuilder.Entity<Teskilat>(entity => {
                entity.ToTable("Teskilatlar");
                entity.HasKey(e => e.TeskilatId);
                
                entity.HasOne(t => t.DaireBaskanligi)
                      .WithMany(d => d.Teskilatlar)
                      .HasForeignKey(t => t.DaireBaskanligiId)
                      .OnDelete(DeleteBehavior.Cascade); // Kademeli silme için Cascade

                entity.HasData(
                    new Teskilat { TeskilatId = 1, Ad = "Merkez", DaireBaskanligiId = 9 },
                    new Teskilat { TeskilatId = 2, Ad = "Taşra", DaireBaskanligiId = 9 }
                );
            });

            // 2. Koordinatorluk
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
                    .OnDelete(DeleteBehavior.SetNull); // Başkan silinirse koordinatörlük silinmesin

                entity.HasData(
                    // Merkez (Id: 1) Coordinatorships - Updated
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

                    // Taşra (Id: 2) - Keep Existing
                    new Koordinatorluk { KoordinatorlukId = 2, Ad = "Mardin İl Koordinatörlüğü", TeskilatId = 2 },
                    new Koordinatorluk { KoordinatorlukId = 3, Ad = "İzmir İl Koordinatörlüğü", TeskilatId = 2 }
                );
            });

            // 3. Komisyon
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

                entity.HasData(
                   // No seed data for Komisyonlar
                );
            });

            // 4. KurumsalRol
            modelBuilder.Entity<KurumsalRol>(entity => {
                entity.ToTable("KurumsalRoller");
                entity.HasKey(e => e.KurumsalRolId);
                entity.HasData(
                    new KurumsalRol { KurumsalRolId = 1, Ad = "Personel" },
                    new KurumsalRol { KurumsalRolId = 2, Ad = "Komisyon Başkanı" },
                    new KurumsalRol { KurumsalRolId = 3, Ad = "İl Koordinatörü" },
                    new KurumsalRol { KurumsalRolId = 4, Ad = "Genel Koordinatör" },
                    new KurumsalRol { KurumsalRolId = 5, Ad = "Merkez Birim Koordinatörlüğü" }
                );
            });

            // 6. SistemRol (New)
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

            // 7. Bildirimler
            modelBuilder.Entity<Bildirim>(entity =>
            {
                entity.ToTable("Bildirimler");
                entity.HasKey(e => e.BildirimId);

                entity.Property(e => e.OlusturmaTarihi).HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.OkunduMu).HasDefaultValue(false);

                // Relationships
                entity.HasOne(d => d.AliciPersonel)
                    .WithMany(p => p.GelenBildirimler)
                    .HasForeignKey(d => d.AliciPersonelId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.GonderenPersonel)
                    .WithMany(p => p.GonderilenBildirimler)
                    .HasForeignKey(d => d.GonderenPersonelId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Indexes
                entity.HasIndex(e => new { e.AliciPersonelId, e.OkunduMu, e.OlusturmaTarihi });
                entity.HasIndex(e => new { e.AliciPersonelId, e.OlusturmaTarihi });
            });

            // 5. M2M Tables
            
            // PersonelTeskilat
            modelBuilder.Entity<PersonelTeskilat>(entity => {
                entity.ToTable("PersonelTeskilatlar");
                entity.HasKey(e => new { e.PersonelId, e.TeskilatId });
                entity.HasOne(e => e.Personel).WithMany(p => p.PersonelTeskilatlar).HasForeignKey(e => e.PersonelId).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.Teskilat).WithMany(t => t.PersonelTeskilatlar).HasForeignKey(e => e.TeskilatId).OnDelete(DeleteBehavior.Cascade);
            });

            // PersonelKoordinatorluk
            modelBuilder.Entity<PersonelKoordinatorluk>(entity => {
                entity.ToTable("PersonelKoordinatorlukler");
                entity.HasKey(e => new { e.PersonelId, e.KoordinatorlukId });
                entity.HasOne(e => e.Personel).WithMany(p => p.PersonelKoordinatorlukler).HasForeignKey(e => e.PersonelId).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.Koordinatorluk).WithMany(k => k.PersonelKoordinatorlukler).HasForeignKey(e => e.KoordinatorlukId).OnDelete(DeleteBehavior.Cascade);
            });

            // PersonelKomisyon
            modelBuilder.Entity<PersonelKomisyon>(entity => {
                entity.ToTable("PersonelKomisyonlar");
                entity.HasKey(e => new { e.PersonelId, e.KomisyonId });
                entity.HasOne(e => e.Personel).WithMany(p => p.PersonelKomisyonlar).HasForeignKey(e => e.PersonelId).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.Komisyon).WithMany(k => k.PersonelKomisyonlar).HasForeignKey(e => e.KomisyonId).OnDelete(DeleteBehavior.Cascade);
            });

            // 6. PersonelKurumsalRolAtama
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
                   .OnDelete(DeleteBehavior.Restrict); // Multiple cascade path fix: Restrict

                entity.HasOne(e => e.Komisyon)
                   .WithMany()
                   .HasForeignKey(e => e.KomisyonId)
                   .OnDelete(DeleteBehavior.Restrict); // Multiple cascade path fix: Restrict
            });
            
            // ===================================
            // Görevler Module Configuration
            // ===================================

            // 1. Birim (Minimal)
            modelBuilder.Entity<Birim>(entity => {
                entity.ToTable("Birimler");
                entity.HasKey(e => e.BirimId);
                entity.Property(e => e.Ad).IsRequired().HasMaxLength(150);
                
                // Seed
                entity.HasData(
                    new Birim { BirimId = 1, Ad = "Yazılım Birimi" },
                    new Birim { BirimId = 2, Ad = "İçerik Birimi" },
                    new Birim { BirimId = 3, Ad = "Grafik Birimi" }
                );
            });

            // 2. GorevKategori
            modelBuilder.Entity<GorevKategori>(entity => {
                entity.ToTable("GorevKategorileri");
                entity.HasKey(e => e.GorevKategoriId);
                entity.Property(e => e.Ad).IsRequired().HasMaxLength(150);
                entity.HasIndex(e => e.Ad).IsUnique();
                
                // Seed
                entity.HasData(
                    new GorevKategori { GorevKategoriId = 1, Ad = "Ders Kitapları", Aciklama = "Ders kitabı hazırlık işleri", Renk = "#3B82F6" },
                    new GorevKategori { GorevKategoriId = 2, Ad = "Yardımcı Kaynaklar", Aciklama = "Soru bankası ve etkinlikler", Renk = "#10B981" },
                    new GorevKategori { GorevKategoriId = 3, Ad = "Dijital İçerik", Aciklama = "Video ve animasyon işleri", Renk = "#F59E0B" },
                    new GorevKategori { GorevKategoriId = 4, Ad = "Programlar", Aciklama = "Müfredat çalışmaları", Renk = "#8B5CF6" }
                );
            });


            // 2.5 GorevDurum
            modelBuilder.Entity<GorevDurum>(entity => {
                entity.ToTable("GorevDurumlari");
                entity.HasKey(e => e.GorevDurumId);
                entity.Property(e => e.Ad).IsRequired().HasMaxLength(100);
                
                // Seed
                entity.HasData(
                    new GorevDurum { GorevDurumId = 1, Ad = "Atanmayı Bekliyor", Sira = 1, RenkSinifi = "bg-warning", Renk = "#F59E0B" },
                    new GorevDurum { GorevDurumId = 2, Ad = "Devam Ediyor", Sira = 2, RenkSinifi = "bg-primary", Renk = "#3B82F6" },
                    new GorevDurum { GorevDurumId = 3, Ad = "Kontrolde", Sira = 3, RenkSinifi = "bg-info", Renk = "#06B6D4" },
                    new GorevDurum { GorevDurumId = 4, Ad = "Tamamlandı", Sira = 4, RenkSinifi = "bg-success", Renk = "#10B981" },
                    new GorevDurum { GorevDurumId = 5, Ad = "İptal", Sira = 5, RenkSinifi = "bg-secondary", Renk = "#6B7280" }
                );
            });

            // 3. Gorev
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

                entity.HasOne(e => e.Birim)
                    .WithMany(b => b.Gorevler)
                    .HasForeignKey(e => e.BirimId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(e => e.GorevDurum)
                    .WithMany(d => d.Gorevler)
                    .HasForeignKey(e => e.GorevDurumId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Indexes
                entity.HasIndex(e => e.KategoriId);
                entity.HasIndex(e => e.PersonelId);
                entity.HasIndex(e => e.GorevDurumId);
                entity.HasIndex(e => e.BaslangicTarihi);

                // Seed Data (updated with GorevDurumId)
                var tasks = new List<Gorev>();
                int idCounter = 1;

                // Test Tasks... skipping original seed lines in replace block usually, but here I'm inserting before Seed.
                // Wait, I should insert AFTER Indexes and BEFORE Seed Data to keep it clean.


                // Kategori 1: Ders Kitapları
                tasks.Add(new Gorev { GorevId = idCounter++, Ad = "Matematik 9 Kitap Dizgisi", Aciklama = "Dizgi taslağını hazırla", KategoriId = 1, PersonelId = 1, BirimId = 3, GorevDurumId = 2, BaslangicTarihi = new DateTime(2025, 11, 01), BitisTarihi = new DateTime(2025, 11, 20) });
                tasks.Add(new Gorev { GorevId = idCounter++, Ad = "Fizik 10 Kapak Tasarımı", Aciklama = "Kapak görseli revizesi", KategoriId = 1, PersonelId = 1, BirimId = 3, GorevDurumId = 3, BaslangicTarihi = new DateTime(2025, 11, 05), BitisTarihi = new DateTime(2025, 11, 08) });
                tasks.Add(new Gorev { GorevId = idCounter++, Ad = "Kimya 11 Yazım Denetimi", Aciklama = "Yazım hatalarının kontrolü", KategoriId = 1, PersonelId = 1, BirimId = 2, GorevDurumId = 1, BaslangicTarihi = new DateTime(2025, 12, 01) });

                // Kategori 2: Yardımcı Kaynaklar
                tasks.Add(new Gorev { GorevId = idCounter++, Ad = "LGS Soru Bankası", Aciklama = "Soru girişleri", KategoriId = 2, PersonelId = 1, BirimId = 2, GorevDurumId = 2, BaslangicTarihi = new DateTime(2025, 11, 15), BitisTarihi = new DateTime(2025, 12, 15) });
                tasks.Add(new Gorev { GorevId = idCounter++, Ad = "YKS Deneme Seti", Aciklama = "Baskı öncesi kontrol", KategoriId = 2, PersonelId = 1, BirimId = 3, GorevDurumId = 2, BaslangicTarihi = new DateTime(2025, 11, 25) });
                tasks.Add(new Gorev { GorevId = idCounter++, Ad = "Etkinlik Yaprakları", Aciklama = "İlkokul seviyesi görselleştirme", KategoriId = 2, PersonelId = 1, BirimId = 3, GorevDurumId = 4, BaslangicTarihi = new DateTime(2025, 10, 20), BitisTarihi = new DateTime(2025, 10, 25) });

                // Kategori 3: Dijital İçerik
                tasks.Add(new Gorev { GorevId = idCounter++, Ad = "EBA Video Montaj", Aciklama = "Ders videoları kurgusu", KategoriId = 3, PersonelId = 1, BirimId = 1, GorevDurumId = 1, BaslangicTarihi = new DateTime(2025, 12, 05) });
                tasks.Add(new Gorev { GorevId = idCounter++, Ad = "Animasyon Karakterleri", Aciklama = "Karakter çizimleri", KategoriId = 3, PersonelId = 1, BirimId = 3, GorevDurumId = 2, BaslangicTarihi = new DateTime(2025, 11, 10), BitisTarihi = new DateTime(2025, 12, 30) });
                tasks.Add(new Gorev { GorevId = idCounter++, Ad = "Seslendirme Kayıtları", Aciklama = "Stüdyo planlaması", KategoriId = 3, PersonelId = 1, BirimId = 2, GorevDurumId = 4, BaslangicTarihi = new DateTime(2025, 11, 01), BitisTarihi = new DateTime(2025, 11, 02) });

                // Kategori 4: Programlar
                tasks.Add(new Gorev { GorevId = idCounter++, Ad = "Müfredat İncelemesi", Aciklama = "Talim Terbiye notları", KategoriId = 4, PersonelId = 1, BirimId = 2, GorevDurumId = 2, BaslangicTarihi = new DateTime(2025, 12, 10) });
                tasks.Add(new Gorev { GorevId = idCounter++, Ad = "Kazanım Eşleştirme", Aciklama = "Excel tablosu hazırlığı", KategoriId = 4, PersonelId = 1, BirimId = 2, GorevDurumId = 1, BaslangicTarihi = new DateTime(2025, 12, 12) });
                tasks.Add(new Gorev { GorevId = idCounter++, Ad = "Haftalık Plan", Aciklama = "2. Dönem planlaması", KategoriId = 4, PersonelId = 1, BirimId = 1, GorevDurumId = 4, BaslangicTarihi = new DateTime(2025, 11, 28), BitisTarihi = new DateTime(2025, 11, 30) });
 
                entity.HasData(tasks);
            });

            // 3.5 GorevDurumGecmisi
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

            // 4. Gorev Assignments (Junction Tables)
            modelBuilder.Entity<GorevAtamaTeskilat>(entity => {
                entity.HasKey(e => new { e.GorevId, e.TeskilatId });
                entity.HasOne(e => e.Gorev)
                      .WithMany(g => g.GorevAtamaTeskilatlar)
                      .HasForeignKey(e => e.GorevId)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.Teskilat).WithMany().HasForeignKey(e => e.TeskilatId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<GorevAtamaKoordinatorluk>(entity => {
                entity.HasKey(e => new { e.GorevId, e.KoordinatorlukId });
                entity.HasOne(e => e.Gorev)
                      .WithMany(g => g.GorevAtamaKoordinatorlukler)
                      .HasForeignKey(e => e.GorevId)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.Koordinatorluk).WithMany().HasForeignKey(e => e.KoordinatorlukId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<GorevAtamaKomisyon>(entity => {
                entity.HasKey(e => new { e.GorevId, e.KomisyonId });
                entity.HasOne(e => e.Gorev)
                      .WithMany(g => g.GorevAtamaKomisyonlar)
                      .HasForeignKey(e => e.GorevId)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.Komisyon).WithMany().HasForeignKey(e => e.KomisyonId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<GorevAtamaPersonel>(entity => {
                entity.HasKey(e => new { e.GorevId, e.PersonelId });
                entity.HasOne(e => e.Gorev)
                      .WithMany(g => g.GorevAtamaPersoneller)
                      .HasForeignKey(e => e.GorevId)
                      .OnDelete(DeleteBehavior.Cascade);
                // Personel delete behavior restrictive to accidental wipes, but for task *assignment* cascade is usually fine (if user is deleted, unassign them).
                entity.HasOne(e => e.Personel).WithMany().HasForeignKey(e => e.PersonelId).OnDelete(DeleteBehavior.Cascade);
            });
        }

        private static IEnumerable<Brans> GetBransSeed()
        {
            return new List<Brans>
            {
                new Brans { BransId = 1, Ad = "Adalet" },
                new Brans { BransId = 2, Ad = "Aile ve Tüketici Hizmetleri" },
                new Brans { BransId = 3, Ad = "Almanca" },
                new Brans { BransId = 4, Ad = "Arapça" },
                new Brans { BransId = 5, Ad = "Ayakkabı ve Saraciye Teknolojisi" },
                new Brans { BransId = 6, Ad = "Beden Eğitimi" },
                new Brans { BransId = 7, Ad = "Bilgisayar ve Öğretim Teknolojileri" },
                new Brans { BransId = 8, Ad = "Bilişim Teknolojileri" },
                new Brans { BransId = 9, Ad = "Biyoloji" },
                new Brans { BransId = 10, Ad = "Büro Yönetimi / Büro Yönetimi ve Yönetici Asistanlığı" },
                new Brans { BransId = 11, Ad = "Coğrafya" },
                new Brans { BransId = 12, Ad = "Çocuk Gelişimi ve Eğitimi" },
                new Brans { BransId = 13, Ad = "Denizcilik / Gemi Makineleri" },
                new Brans { BransId = 14, Ad = "Denizcilik / Gemi Yönetimi" },
                new Brans { BransId = 15, Ad = "Din Kültürü ve Ahlâk Bilgisi" },
                new Brans { BransId = 16, Ad = "El Sanatları Teknolojisi / El Sanatları" },
                new Brans { BransId = 17, Ad = "El Sanatları Teknolojisi / Nakış" },
                new Brans { BransId = 18, Ad = "Elektrik-Elektronik Teknolojisi / Elektrik" },
                new Brans { BransId = 19, Ad = "Elektrik-Elektronik Teknolojisi / Elektronik" },
                new Brans { BransId = 20, Ad = "Endüstriyel Otomasyon Teknolojileri" },
                new Brans { BransId = 21, Ad = "Farsça" },
                new Brans { BransId = 22, Ad = "Felsefe" },
                new Brans { BransId = 23, Ad = "Fen Bilimleri" },
                new Brans { BransId = 24, Ad = "Fizik" },
                new Brans { BransId = 25, Ad = "Gemi Yapımı / Yat İnşa" },
                new Brans { BransId = 26, Ad = "Gıda Teknolojisi" },
                new Brans { BransId = 27, Ad = "Giyim Üretim Teknolojisi / Moda Tasarım Teknolojileri" },
                new Brans { BransId = 28, Ad = "Görsel Sanatlar" },
                new Brans { BransId = 29, Ad = "Grafik ve Fotoğraf / Grafik" },
                new Brans { BransId = 30, Ad = "Harita-Tapu-Kadastro / Harita Kadastro" },
                new Brans { BransId = 31, Ad = "Hasta ve Yaşlı Hizmetleri" },
                new Brans { BransId = 32, Ad = "Hayvan Sağlığı / Hayvan Yetiştiriciliği ve Sağlığı / Hayvan Sağlığı" },
                new Brans { BransId = 33, Ad = "Hayvan Sağlığı / Hayvan Yetiştiriciliği ve Sağlığı / Hayvan Yetiştiriciliği" },
                new Brans { BransId = 34, Ad = "İlköğretim Matematik" },
                new Brans { BransId = 35, Ad = "İmam-Hatip Lisesi Meslek Dersleri" },
                new Brans { BransId = 36, Ad = "İngilizce" },
                new Brans { BransId = 37, Ad = "İnşaat Teknolojisi / Yapı Dekorasyon" },
                new Brans { BransId = 38, Ad = "İnşaat Teknolojisi / Yapı Tasarım" },
                new Brans { BransId = 39, Ad = "İtfaiyecilik ve Yangın Güvenliği" },
                new Brans { BransId = 40, Ad = "Kimya / Kimya Teknolojisi" },
                new Brans { BransId = 41, Ad = "Konaklama ve Seyahat Hizmetleri / Konaklama ve Seyahat" },
                new Brans { BransId = 42, Ad = "Kuyumculuk Teknolojisi" },
                new Brans { BransId = 43, Ad = "Makine Teknolojisi / Makine ve Tasarım Teknolojisi / Makine Model" },
                new Brans { BransId = 44, Ad = "Makine Teknolojisi / Makine ve Tasarım Teknolojisi / Makine Ressamlığı" },
                new Brans { BransId = 45, Ad = "Makine Teknolojisi / Makine ve Tasarım Teknolojisi / Makine ve Kalıp" },
                new Brans { BransId = 46, Ad = "Matbaa / Matbaa Teknolojisi" },
                new Brans { BransId = 47, Ad = "Matematik" },
                new Brans { BransId = 48, Ad = "Metal Teknolojisi" },
                new Brans { BransId = 49, Ad = "Mobilya ve İç Mekan Tasarımı" },
                new Brans { BransId = 50, Ad = "Motorlu Araçlar Teknolojisi" },
                new Brans { BransId = 51, Ad = "Muhasebe ve Finansman" },
                new Brans { BransId = 52, Ad = "Müzik" },
                new Brans { BransId = 53, Ad = "Okul Öncesi" },
                new Brans { BransId = 54, Ad = "Özel Eğitim" },
                new Brans { BransId = 55, Ad = "Pazarlama ve Perakende" },
                new Brans { BransId = 56, Ad = "Plastik Teknolojisi" },
                new Brans { BransId = 57, Ad = "Raylı Sistemler Teknolojisi / Raylı Sistemler Elektrik-Elektronik" },
                new Brans { BransId = 58, Ad = "Rehberlik" },
                new Brans { BransId = 59, Ad = "Rusça" },
                new Brans { BransId = 60, Ad = "Sağlık / Sağlık Hizmetleri" },
                new Brans { BransId = 61, Ad = "Sağlık Bilgisi" },
                new Brans { BransId = 62, Ad = "Seramik ve Cam Teknolojisi" },
                new Brans { BransId = 63, Ad = "Sınıf Öğretmenliği" },
                new Brans { BransId = 64, Ad = "Sosyal Bilgiler" },
                new Brans { BransId = 65, Ad = "Tarım Teknolojileri/Tarım" },
                new Brans { BransId = 66, Ad = "Tarih" },
                new Brans { BransId = 67, Ad = "Teknoloji ve Tasarım" },
                new Brans { BransId = 68, Ad = "Tesisat Teknolojisi ve İklimlendirme" },
                new Brans { BransId = 69, Ad = "Tiyatro" },
                new Brans { BransId = 70, Ad = "Türk Dili ve Edebiyatı" },
                new Brans { BransId = 71, Ad = "Türkçe" },
                new Brans { BransId = 72, Ad = "Ulaştırma Hizmetleri / Lojistik" },
                new Brans { BransId = 73, Ad = "Yaşayan Diller ve Lehçeler (Kürtçe / Kurmançi)" },
                new Brans { BransId = 74, Ad = "Yaşayan Diller ve Lehçeler (Kürtçe / Zazaki)" },
                new Brans { BransId = 75, Ad = "Yenilenebilir Enerji Teknolojileri" },
                new Brans { BransId = 76, Ad = "Yiyecek İçecek Hizmetleri" }
            };
        }

        private static IEnumerable<Yazilim> GetYazilimSeed()
        {
            return new List<Yazilim>
            {
                new Yazilim { YazilimId = 1, Ad = "Photoshop" },
                new Yazilim { YazilimId = 2, Ad = "İllüstrator" },
                new Yazilim { YazilimId = 3, Ad = "InDesign" },
                new Yazilim { YazilimId = 4, Ad = "Camtasia" },
                new Yazilim { YazilimId = 5, Ad = "Premiere" },
                new Yazilim { YazilimId = 6, Ad = "After Effects" },
                new Yazilim { YazilimId = 7, Ad = "Cinema 4D" },
                new Yazilim { YazilimId = 8, Ad = "Blender" },
                new Yazilim { YazilimId = 9, Ad = "Maya" },
                new Yazilim { YazilimId = 10, Ad = "Procreate" },
                new Yazilim { YazilimId = 11, Ad = "Construct" },
                new Yazilim { YazilimId = 12, Ad = "Articulate" },
                new Yazilim { YazilimId = 13, Ad = "Unity" },
                new Yazilim { YazilimId = 14, Ad = "Unreal Engine" },
                new Yazilim { YazilimId = 15, Ad = "PHP" },
                new Yazilim { YazilimId = 16, Ad = "Java" },
                new Yazilim { YazilimId = 17, Ad = ".NET" },
                new Yazilim { YazilimId = 18, Ad = "Diğer" }
            };
        }

        private static IEnumerable<Uzmanlik> GetUzmanlikSeed()
        {
            return new List<Uzmanlik>
            {
                new Uzmanlik { UzmanlikId = 1, Ad = "Bilişim Uzmanı" },
                new Uzmanlik { UzmanlikId = 2, Ad = "Görsel Tasarım Uzmanı" },
                new Uzmanlik { UzmanlikId = 3, Ad = "Yazar" },
                new Uzmanlik { UzmanlikId = 4, Ad = "Dil Uzmanı" },
                new Uzmanlik { UzmanlikId = 5, Ad = "Rehberlik" }
            };
        }

        private static IEnumerable<GorevTuru> GetGorevTuruSeed()
        {
            return new List<GorevTuru>
            {
                new GorevTuru { GorevTuruId = 1, Ad = "Dizgi" },
                new GorevTuru { GorevTuruId = 2, Ad = "Görsel Üretim" },
                new GorevTuru { GorevTuruId = 3, Ad = "Çizer" },
                new GorevTuru { GorevTuruId = 4, Ad = "Çizgi Roman Renklendirme" },
                new GorevTuru { GorevTuruId = 5, Ad = "Görsel Kontrolü" },
                new GorevTuru { GorevTuruId = 6, Ad = "Çoklu Ortam Tasarımı" },
                new GorevTuru { GorevTuruId = 7, Ad = "Video/Kurgu" },
                new GorevTuru { GorevTuruId = 8, Ad = "Fotoğraf Çekimi" },
                new GorevTuru { GorevTuruId = 9, Ad = "Açıklama" }
            };
        }

        private static IEnumerable<IsNiteligi> GetIsNiteligiSeed()
        {
            return new List<IsNiteligi>
            {
                new IsNiteligi { IsNiteligiId = 1, Ad = "Ders Kitabı" },
                new IsNiteligi { IsNiteligiId = 2, Ad = "Çalışma Kitabı" },
                new IsNiteligi { IsNiteligiId = 3, Ad = "Hikâye Kitabı" },
                new IsNiteligi { IsNiteligiId = 4, Ad = "Çizgi Roman" },
                new IsNiteligi { IsNiteligiId = 5, Ad = "Çoklu Ortam" },
                new IsNiteligi { IsNiteligiId = 6, Ad = "MEBİ Mümtaz Şahsiyetler" },
                new IsNiteligi { IsNiteligiId = 7, Ad = "Dergi" },
                new IsNiteligi { IsNiteligiId = 8, Ad = "E Bülten" },
                new IsNiteligi { IsNiteligiId = 9, Ad = "E Dergi" },
                new IsNiteligi { IsNiteligiId = 10, Ad = "Öğretim Programı" },
                new IsNiteligi { IsNiteligiId = 11, Ad = "Açıklama" }
            };
        }
    }
}
