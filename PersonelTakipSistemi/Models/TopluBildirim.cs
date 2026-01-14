using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonelTakipSistemi.Models
{
    public enum BildirimDurum
    {
        Taslak = 0,
        Planlandi = 1,
        Gonderildi = 2,
        Iptal = 3,
        Hata = 4
    }

    public class TopluBildirim
    {
        [Key]
        public int Id { get; set; }

        public int GonderenId { get; set; }
        
        [ForeignKey("GonderenId")]
        public BildirimGonderen Gonderen { get; set; } = null!;

        [Required]
        [MaxLength(200)]
        public string Baslik { get; set; } = null!;

        [Required]
        public string Icerik { get; set; } = null!;

        public DateTime OlusturmaTarihi { get; set; } = DateTime.Now;

        public DateTime? PlanlananZaman { get; set; }

        public DateTime? GonderimZamani { get; set; }

        public BildirimDurum Durum { get; set; } = BildirimDurum.Taslak;

        public string? HedefKitleJson { get; set; } // Stores filter criteria for audit
        
        public string? RecipientIdsJson { get; set; } // Stores valid recipient IDs at creation
    }
}
