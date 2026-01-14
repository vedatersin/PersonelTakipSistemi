using System;

namespace PersonelTakipSistemi.Dtos
{
    public class BildirimMiniDto
    {
        public int BildirimId { get; set; }
        public string Baslik { get; set; } = null!;
        public DateTime OlusturmaTarihi { get; set; }
        public bool OkunduMu { get; set; }
        public string GonderenAdSoyad { get; set; } = "Sistem";
        public string? GonderenKurumsalRolOzet { get; set; }
    }
}
