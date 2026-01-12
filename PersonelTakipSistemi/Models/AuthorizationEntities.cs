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

        // İlişkiler
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

        public int TeskilatId { get; set; }
        [ForeignKey("TeskilatId")]
        public Teskilat Teskilat { get; set; } = null!;

        public int? BaskanPersonelId { get; set; }
        [ForeignKey("BaskanPersonelId")]
        public Personel? BaskanPersonel { get; set; }

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

        public int KoordinatorlukId { get; set; }
        [ForeignKey("KoordinatorlukId")]
        public Koordinatorluk Koordinatorluk { get; set; } = null!;

        public int? BaskanPersonelId { get; set; }
        [ForeignKey("BaskanPersonelId")]
        public Personel? BaskanPersonel { get; set; }

        // İlişkiler
        public ICollection<PersonelKomisyon> PersonelKomisyonlar { get; set; } = new List<PersonelKomisyon>();
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

        public int? KoordinatorlukId { get; set; }
        [ForeignKey("KoordinatorlukId")]
        public Koordinatorluk? Koordinatorluk { get; set; }

        public int? KomisyonId { get; set; }
        [ForeignKey("KomisyonId")]
        public Komisyon? Komisyon { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
