using System;
using System.ComponentModel.DataAnnotations;

namespace PersonelTakipSistemi.Models
{
    public class Bildirim
    {
        public int BildirimId { get; set; }

        public int AliciPersonelId { get; set; }
        public Personel AliciPersonel { get; set; } = null!;

        public int? GonderenPersonelId { get; set; }
        public Personel? GonderenPersonel { get; set; }

        public int? BildirimGonderenId { get; set; }
        public BildirimGonderen? BildirimGonderen { get; set; }

        public int? TopluBildirimId { get; set; }
        public TopluBildirim? TopluBildirim { get; set; }

        [Required]
        [MaxLength(200)]
        public string Baslik { get; set; } = null!;

        [Required]
        public string Aciklama { get; set; } = null!;

        public DateTime OlusturmaTarihi { get; set; } = DateTime.Now;

        public bool OkunduMu { get; set; } = false;

        public DateTime? OkunmaTarihi { get; set; }

        [MaxLength(50)]
        public string? Tip { get; set; } // "KurumsalAtama", "RolAtama", "BaskanDegisimi"

        [MaxLength(50)]
        public string? RefType { get; set; } // "Komisyon", "Koordinatorluk"

        public int? RefId { get; set; }

        [MaxLength(300)]
        public string? Url { get; set; }

        public bool SilindiMi { get; set; } = false;
    }
}
