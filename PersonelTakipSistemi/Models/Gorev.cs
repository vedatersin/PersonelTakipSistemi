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
        // Foreing Keys
        public int KategoriId { get; set; }
        public GorevKategori? Kategori { get; set; }

        public int? PersonelId { get; set; }
        public Personel? Personel { get; set; }

        public int? BirimId { get; set; }
        public Birim? Birim { get; set; }

        // Status Relationship
        public int GorevDurumId { get; set; }
        public GorevDurum? GorevDurum { get; set; }
        
        [StringLength(500)]
        public string? DurumAciklamasi { get; set; }

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
