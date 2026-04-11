using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonelTakipSistemi.Models
{
    public enum CihazOnayDurumu
    {
        Beklemede = 1,
        Onaylandi = 2
    }

    public enum CihazHareketTuru
    {
        Kayit = 1,
        Onay = 2,
        Devir = 3
    }

    public enum LisansSuresiTuru
    {
        Aylik = 1,
        Yillik = 2,
        Suresiz = 3
    }

    public enum LisansKullaniciOnayDurumu
    {
        OnayaGonderildi = 1,
        OnayBekleniyor = 2,
        Onaylandi = 3
    }

    public class CihazTuru
    {
        [Key]
        public int CihazTuruId { get; set; }

        [Required]
        [StringLength(150)]
        public string Ad { get; set; } = null!;

        [StringLength(300)]
        public string? KullanimAmaci { get; set; }

        public bool SistemSecenegiMi { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<CihazMarka> Markalar { get; set; } = new List<CihazMarka>();
        public ICollection<Cihaz> Cihazlar { get; set; } = new List<Cihaz>();
    }

    public class CihazMarka
    {
        [Key]
        public int CihazMarkaId { get; set; }

        public int CihazTuruId { get; set; }

        [ForeignKey(nameof(CihazTuruId))]
        public CihazTuru CihazTuru { get; set; } = null!;

        [Required]
        [StringLength(150)]
        public string Ad { get; set; } = null!;

        public bool SistemSecenegiMi { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<Cihaz> Cihazlar { get; set; } = new List<Cihaz>();
    }

    public class Cihaz
    {
        [Key]
        public int CihazId { get; set; }

        public int CihazTuruId { get; set; }
        public int CihazMarkaId { get; set; }

        [ForeignKey(nameof(CihazTuruId))]
        public CihazTuru CihazTuru { get; set; } = null!;

        [ForeignKey(nameof(CihazMarkaId))]
        public CihazMarka CihazMarka { get; set; } = null!;

        [StringLength(150)]
        public string? DigerCihazTuruAd { get; set; }

        [StringLength(150)]
        public string? DigerMarkaAd { get; set; }

        [Required]
        [StringLength(150)]
        public string Model { get; set; } = null!;

        [Required]
        [StringLength(500)]
        public string Ozellikler { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string SeriNo { get; set; } = null!;

        public int? SahipPersonelId { get; set; }

        [ForeignKey(nameof(SahipPersonelId))]
        public Personel? SahipPersonel { get; set; }

        public int KoordinatorlukId { get; set; }

        [ForeignKey(nameof(KoordinatorlukId))]
        public Koordinatorluk Koordinatorluk { get; set; } = null!;

        public DateTime IlkKayitTarihi { get; set; } = DateTime.Now;
        public DateTime AktifSahiplikBaslangicTarihi { get; set; } = DateTime.Now;
        public DateTime? SonDevirTarihi { get; set; }

        public CihazOnayDurumu OnayDurumu { get; set; } = CihazOnayDurumu.Beklemede;

        public int? OnaylayanPersonelId { get; set; }

        [ForeignKey(nameof(OnaylayanPersonelId))]
        public Personel? OnaylayanPersonel { get; set; }

        public DateTime? OnayTarihi { get; set; }

        public int? OlusturanPersonelId { get; set; }

        [ForeignKey(nameof(OlusturanPersonelId))]
        public Personel? OlusturanPersonel { get; set; }

        public bool KoordinatorTarafindanEklendi { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }

        public ICollection<CihazHareketi> Hareketler { get; set; } = new List<CihazHareketi>();
    }

    public class CihazHareketi
    {
        [Key]
        public int CihazHareketiId { get; set; }

        public int CihazId { get; set; }

        [ForeignKey(nameof(CihazId))]
        public Cihaz Cihaz { get; set; } = null!;

        public CihazHareketTuru HareketTuru { get; set; }

        public int? OncekiSahipPersonelId { get; set; }

        [ForeignKey(nameof(OncekiSahipPersonelId))]
        public Personel? OncekiSahipPersonel { get; set; }

        public int? YeniSahipPersonelId { get; set; }

        [ForeignKey(nameof(YeniSahipPersonelId))]
        public Personel? YeniSahipPersonel { get; set; }

        public int? IslemYapanPersonelId { get; set; }

        [ForeignKey(nameof(IslemYapanPersonelId))]
        public Personel? IslemYapanPersonel { get; set; }

        [StringLength(200)]
        public string? IslemYapanAdSoyad { get; set; }

        [StringLength(200)]
        public string? OncekiSahipAdSoyad { get; set; }

        [StringLength(200)]
        public string? YeniSahipAdSoyad { get; set; }

        [StringLength(500)]
        public string? Aciklama { get; set; }

        [StringLength(500)]
        public string? DurumNotu { get; set; }

        public DateTime Tarih { get; set; } = DateTime.Now;
    }

    public class YazilimLisans
    {
        [Key]
        public int YazilimLisansId { get; set; }

        public int YazilimId { get; set; }

        [ForeignKey(nameof(YazilimId))]
        public Yazilim Yazilim { get; set; } = null!;

        public int MaksimumLisansHesapAdedi { get; set; }

        public LisansSuresiTuru LisansSuresiTuru { get; set; }

        public DateTime? BaslangicTarihi { get; set; }
        public DateTime? BitisTarihi { get; set; }

        [StringLength(200)]
        public string? HesapBasiOrtakKullanimBilgisi { get; set; }

        [StringLength(500)]
        public string? KullanimAmaci { get; set; }

        public int OlusturanPersonelId { get; set; }

        [ForeignKey(nameof(OlusturanPersonelId))]
        public Personel OlusturanPersonel { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }

        public ICollection<YazilimLisansKullanici> Kullanicilar { get; set; } = new List<YazilimLisansKullanici>();
    }

    public class YazilimLisansKullanici
    {
        [Key]
        public int YazilimLisansKullaniciId { get; set; }

        public int YazilimLisansId { get; set; }

        [ForeignKey(nameof(YazilimLisansId))]
        public YazilimLisans YazilimLisans { get; set; } = null!;

        public int PersonelId { get; set; }

        [ForeignKey(nameof(PersonelId))]
        public Personel Personel { get; set; } = null!;

        [StringLength(200)]
        public string? KullaniciAdi { get; set; }

        [StringLength(250)]
        public string? Eposta { get; set; }

        public LisansKullaniciOnayDurumu OnayDurumu { get; set; } = LisansKullaniciOnayDurumu.OnayaGonderildi;

        public int? OnaylayanPersonelId { get; set; }

        [ForeignKey(nameof(OnaylayanPersonelId))]
        public Personel? OnaylayanPersonel { get; set; }

        public DateTime? OnayTarihi { get; set; }

        public int KaydiOlusturanPersonelId { get; set; }

        [ForeignKey(nameof(KaydiOlusturanPersonelId))]
        public Personel KaydiOlusturanPersonel { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
    }
}
