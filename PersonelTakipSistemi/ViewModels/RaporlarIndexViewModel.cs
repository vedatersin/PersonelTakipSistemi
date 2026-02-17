using Microsoft.AspNetCore.Mvc.Rendering;
using PersonelTakipSistemi.Models;

namespace PersonelTakipSistemi.ViewModels
{
    public class RaporlarIndexViewModel
    {
        // Filtreler
        public DateTime? BaslangicTarihi { get; set; }
        public DateTime? BitisTarihi { get; set; }
        
        public int? TeskilatId { get; set; }
        public int? KoordinatorlukId { get; set; }
        public int? KomisyonId { get; set; }

        public List<SelectListItem> TeskilatList { get; set; } = new();
        public List<SelectListItem> KoordinatorlukList { get; set; } = new();
        public List<SelectListItem> KomisyonList { get; set; } = new();

        // Rapor Sonuçları (Tablolar)
        public List<PersonelPerformansRaporu> PerformansRaporlari { get; set; } = new();
        public List<GorevYogunlukRaporu> GorevRaporlari { get; set; } = new();
    }

    public class PersonelPerformansRaporu
    {
        public int PersonelId { get; set; }
        public string AdSoyad { get; set; } = null!;
        public string Birim { get; set; } = null!;
        public int ToplamGorev { get; set; }
        public int Tamamlanan { get; set; }
        public int Geciken { get; set; }
        public double BasariOrani { get; set; } // % olarak

        // Navigasyon için birim ID'leri
        public int? TeskilatId { get; set; }
        public int? KoordinatorlukId { get; set; }
        public int? KomisyonId { get; set; }
    }

    public class GorevYogunlukRaporu
    {
        public string Donem { get; set; } = null!; // "Ocak 2024" vb.
        public int AtananGorevSayisi { get; set; }
        public int TamamlananGorevSayisi { get; set; }
    }
}
