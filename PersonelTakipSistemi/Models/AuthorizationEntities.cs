using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonelTakipSistemi.Models
{
    // ANA TABLOLAR

    public class Teskilat
    {
        [Key]
        public int TeskilatId { get; set; }

        [Required]
        [StringLength(50)]
        public string Ad { get; set; } = null!; // "Merkez", "Taşra"

        public string? Aciklama { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Yeni Alanlar
        [StringLength(20)]
        public string Tur { get; set; } = "Merkez"; // "Merkez", "Taşra"
        public bool TasraOrgutlenmesiVarMi { get; set; } = false; // Sadece Merkez için

        public int? BagliMerkezTeskilatId { get; set; } // Sadece Taşra için
        [ForeignKey("BagliMerkezTeskilatId")]
        public Teskilat? BagliMerkezTeskilat { get; set; }

        // İlişkiler
        public int? DaireBaskanligiId { get; set; }
        [ForeignKey("DaireBaskanligiId")]
        public DaireBaskanligi? DaireBaskanligi { get; set; }

        public ICollection<Koordinatorluk> Koordinatorlukler { get; set; } = new List<Koordinatorluk>();
        public ICollection<PersonelTeskilat> PersonelTeskilatlar { get; set; } = new List<PersonelTeskilat>();
    }

    public class Koordinatorluk
    {
        [Key]
        public int KoordinatorlukId { get; set; }

        [Required]
        [StringLength(150)]
        public string Ad { get; set; } = null!;

        public string? Aciklama { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int TeskilatId { get; set; }
        [ForeignKey("TeskilatId")]
        public Teskilat Teskilat { get; set; } = null!;

        public int? IlId { get; set; }
        [ForeignKey("IlId")]
        public Il? Il { get; set; }

        public int? BaskanPersonelId { get; set; }
        [ForeignKey("BaskanPersonelId")]
        public Personel? BaskanPersonel { get; set; }

        public bool TasraTeskilatiVarMi { get; set; } = true; // Default true for existing/Merkez

        // İlişkiler
        public ICollection<Komisyon> Komisyonlar { get; set; } = new List<Komisyon>();
        public ICollection<PersonelKoordinatorluk> PersonelKoordinatorlukler { get; set; } = new List<PersonelKoordinatorluk>();
    }

    public class Komisyon
    {
        [Key]
        public int KomisyonId { get; set; }

        [Required]
        [StringLength(150)]
        public string Ad { get; set; } = null!;

        public string? Aciklama { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int KoordinatorlukId { get; set; }
        [ForeignKey("KoordinatorlukId")]
        public Koordinatorluk Koordinatorluk { get; set; } = null!;

        public int? BaskanPersonelId { get; set; }
        [ForeignKey("BaskanPersonelId")]
        public Personel? BaskanPersonel { get; set; }

        // İlişkiler
        public ICollection<PersonelKomisyon> PersonelKomisyonlar { get; set; } = new List<PersonelKomisyon>();

        // Taşra teşkilatındaki komisyonun bağlı olduğu Merkez Birim Koordinatörlüğü
        public int? BagliMerkezKoordinatorlukId { get; set; }
        [ForeignKey("BagliMerkezKoordinatorlukId")]
        public Koordinatorluk? BagliMerkezKoordinatorluk { get; set; }
    }

    public class KurumsalRol
    {
        [Key]
        public int KurumsalRolId { get; set; }

        [Required]
        [StringLength(100)]
        public string Ad { get; set; } = null!; // "Komisyon Başkanı", "İl Koordinatörü" vb.
    }

    // MANY-TO-MANY İLİŞKİ TABLOLARI

    public class PersonelTeskilat
    {
        public int PersonelId { get; set; }
        public Personel Personel { get; set; } = null!;

        public int TeskilatId { get; set; }
        public Teskilat Teskilat { get; set; } = null!;
    }

    public class PersonelKoordinatorluk
    {
        public int PersonelId { get; set; }
        public Personel Personel { get; set; } = null!;

        public int KoordinatorlukId { get; set; }
        public Koordinatorluk Koordinatorluk { get; set; } = null!;
    }

    public class PersonelKomisyon
    {
        public int PersonelId { get; set; }
        public Personel Personel { get; set; } = null!;

        public int KomisyonId { get; set; }
        public Komisyon Komisyon { get; set; } = null!;
    }

    // ROL ATAMALARI (Bağlamsal)

    public class PersonelKurumsalRolAtama
    {
        [Key]
        public int Id { get; set; }

        public int PersonelId { get; set; }
        [ForeignKey("PersonelId")]
        public Personel Personel { get; set; } = null!;

        public int KurumsalRolId { get; set; }
        [ForeignKey("KurumsalRolId")]
        public KurumsalRol KurumsalRol { get; set; } = null!;

        public int? TeskilatId { get; set; }
        [ForeignKey("TeskilatId")]
        public Teskilat? Teskilat { get; set; }

        public int? KoordinatorlukId { get; set; }
        [ForeignKey("KoordinatorlukId")]
        public Koordinatorluk? Koordinatorluk { get; set; }

        public int? KomisyonId { get; set; }
        [ForeignKey("KomisyonId")]
        public Komisyon? Komisyon { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
