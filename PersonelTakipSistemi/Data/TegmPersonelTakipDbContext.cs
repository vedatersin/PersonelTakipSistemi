using Microsoft.EntityFrameworkCore;
using PersonelTakipSistemi.Models;

namespace PersonelTakipSistemi.Data
{
    public partial class TegmPersonelTakipDbContext : DbContext
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
        public DbSet<Ilce> Ilceler { get; set; } = null!;
        public DbSet<Brans> Branslar { get; set; }

        public DbSet<PersonelYazilim> PersonelYazilimlar { get; set; }
        public DbSet<PersonelUzmanlik> PersonelUzmanliklar { get; set; }
        public DbSet<PersonelGorevTuru> PersonelGorevTurleri { get; set; }
        public DbSet<PersonelIsNiteligi> PersonelIsNitelikleri { get; set; }

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

        public DbSet<GorevDurum> GorevDurumlari { get; set; }
        public DbSet<Birim> Birimler { get; set; }
        public DbSet<Gorev> Gorevler { get; set; }
        public DbSet<GorevDurumGecmisi> GorevDurumGecmisleri { get; set; }

        public DbSet<GorevAtamaTeskilat> GorevAtamaTeskilatlar { get; set; }
        public DbSet<GorevAtamaKoordinatorluk> GorevAtamaKoordinatorlukler { get; set; }
        public DbSet<GorevAtamaKomisyon> GorevAtamaKomisyonlar { get; set; }
        public DbSet<GorevAtamaPersonel> GorevAtamaPersoneller { get; set; }
        public DbSet<DaireBaskanligi> DaireBaskanliklari { get; set; }
        public DbSet<CihazTuru> CihazTurleri { get; set; }
        public DbSet<CihazMarka> CihazMarkalari { get; set; }
        public DbSet<Cihaz> Cihazlar { get; set; }
        public DbSet<CihazHareketi> CihazHareketleri { get; set; }
        public DbSet<YazilimLisans> YazilimLisanslar { get; set; }
        public DbSet<YazilimLisansKullanici> YazilimLisansKullanicilar { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            ConfigureNotificationModule(modelBuilder);
            ConfigureAuthorizationModule(modelBuilder);
            ConfigureCoreCatalogs(modelBuilder);
            ConfigureTaskModule(modelBuilder);
            ConfigureDeviceModule(modelBuilder);
        }
    }
}
