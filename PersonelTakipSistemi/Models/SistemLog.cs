using System.ComponentModel.DataAnnotations;

namespace PersonelTakipSistemi.Models
{
    public class SistemLog
    {
        [Key]
        public int Id { get; set; }

        public int? PersonelId { get; set; } // Nullable if action is done by system or before login (e.g. failed login)

        [MaxLength(11)]
        public string? TcKimlikNo { get; set; } // Snapshot

        [MaxLength(100)]
        public string? KullaniciAdSoyad { get; set; } // Snapshot

        [MaxLength(50)]
        public string IpAdresi { get; set; } = null!;

        public DateTime Tarih { get; set; } = DateTime.Now;

        [MaxLength(50)]
        public string IslemTuru { get; set; } = null!; // "Giris", "Ekleme", "Silme" etc.

        public string Aciklama { get; set; } = null!;

        public string? Detay { get; set; } // JSON or detailed text
    }
}
