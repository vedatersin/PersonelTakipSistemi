using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonelTakipSistemi.Models
{
    public class Gorev
    {
        [Key]
        public int GorevId { get; set; }

        [Required]
        [StringLength(200)]
        public string Ad { get; set; } = null!;

        public string? Aciklama { get; set; }

        // Foreing Keys
        public int KategoriId { get; set; }
        public GorevKategori Kategori { get; set; } = null!;

        public int PersonelId { get; set; }
        public Personel Personel { get; set; } = null!;

        public int? BirimId { get; set; }
        public Birim? Birim { get; set; }

        // 0=Beklemede, 1=Aktif, 2=Tamamlandı
        public byte Durum { get; set; } 

        public DateTime BaslangicTarihi { get; set; }
        public DateTime? BitisTarihi { get; set; }

        public bool IsActive { get; set; } = true;
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }

        // Assignments
        public ICollection<GorevAtamaTeskilat> GorevAtamaTeskilatlar { get; set; } = new List<GorevAtamaTeskilat>();
        public ICollection<GorevAtamaKoordinatorluk> GorevAtamaKoordinatorlukler { get; set; } = new List<GorevAtamaKoordinatorluk>();
        public ICollection<GorevAtamaKomisyon> GorevAtamaKomisyonlar { get; set; } = new List<GorevAtamaKomisyon>();
        public ICollection<GorevAtamaPersonel> GorevAtamaPersoneller { get; set; } = new List<GorevAtamaPersonel>();
    }
}
