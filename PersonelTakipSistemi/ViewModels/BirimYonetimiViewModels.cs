using System.Collections.Generic;
using PersonelTakipSistemi.Models;

namespace PersonelTakipSistemi.ViewModels
{
    public class KordinatorlukYonetimiViewModel
    {
        public bool IsMerkez { get; set; }
        public int KoordinatorlukId { get; set; }
        public string KoordinatorlukAd { get; set; } = null!;
        public string? IlAd { get; set; }
        public int? IlId { get; set; }

        public List<HaritaIlItem> HaritaIlleri { get; set; } = new();
        public List<BirimKartItem> Komisyonlar { get; set; } = new();
        
        // Sadece Merkez için
        public List<KomisyonPersonelItem> MerkezPersonelleri { get; set; } = new();
        public List<KomisyonGorevItem> MerkezGorevleri { get; set; } = new();
    }
}
