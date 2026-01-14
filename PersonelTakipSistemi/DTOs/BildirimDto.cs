using System;

namespace PersonelTakipSistemi.Dtos
{
    public class BildirimDto
    {
        public int BildirimId { get; set; }
        public string Baslik { get; set; } = null!;
        public string Aciklama { get; set; } = null!;
        public DateTime OlusturmaTarihi { get; set; }
        public bool OkunduMu { get; set; }
        public string? Url { get; set; }
        
        // Sender Info
        public int? GonderenPersonelId { get; set; }
        public string GonderenAdSoyad { get; set; } = "Sistem";
        public string? GonderenKurumsalRolOzet { get; set; }
        public string? GonderenFotoUrl { get; set; }
    }
}
