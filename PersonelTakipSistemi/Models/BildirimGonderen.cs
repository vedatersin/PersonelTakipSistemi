using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonelTakipSistemi.Models
{
    public enum GonderenTip
    {
        Sistem = 1,
        Personel = 2
    }

    public class BildirimGonderen
    {
        [Key]
        public int Id { get; set; }

        public GonderenTip Tip { get; set; }

        public int? PersonelId { get; set; } // Null if Sistem, non-null if Personel
        
        [ForeignKey("PersonelId")]
        public Personel? Personel { get; set; }

        [MaxLength(100)]
        public string GorunenAd { get; set; } = null!;

        [MaxLength(300)]
        public string? AvatarUrl { get; set; }
    }
}
