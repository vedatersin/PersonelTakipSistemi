using PersonelTakipSistemi.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PersonelTakipSistemi.ViewModels
{
    public class IstatistikViewModel
    {
        // Kartlar (Özet Sayılar)
        public int ToplamPersonel { get; set; }
        public int ToplamKomisyon { get; set; }
        public int ToplamKoordinatorluk { get; set; }
        public int ToplamGorev { get; set; } 

        // Filtreler için Listeler
        public List<SelectListItem> TeskilatList { get; set; } = new();
        public List<SelectListItem> KoordinatorlukList { get; set; } = new();
        public List<SelectListItem> KomisyonList { get; set; } = new();
        public List<SelectListItem> PersonelList { get; set; } = new(); // Arama için select2 kullanılacak

        public int? TeskilatId { get; set; }
        public int? KoordinatorlukId { get; set; }
        public int? KomisyonId { get; set; }
        public int? PersonelId { get; set; }
        
        // Kişisel İstatistik sayfası için
        public int? PersonnelIdForKisisel { get; set; }


        // Grafik Verileri
        public ChartDataJson KategoriDagilimi { get; set; } = new();
        public MultiSeriesChartJson UzmanlikTrendi { get; set; } = new(); // New Multi-Series Chart
        public ChartDataJson GorevDurumDagilimi { get; set; } = new();
        
        // Personel Modu için Görev Listesi
        public List<PersonelGorevItem> GorevListesi { get; set; } = new();
    }

    public class PersonelGorevItem 
    {
        public int GorevId { get; set; }
        public string Baslik { get; set; } = null!;
        public string Durum { get; set; } = null!;
        public string Renk { get; set; } = null!; // Keep for backward compatibility if needed, or deprecate
        public string? RenkKod { get; set; } // Hex Code
        public DateTime Tarih { get; set; }
        public DateTime? SonIslem { get; set; }
    }

    public class ChartDataJson
    {
        public List<string> Labels { get; set; } = new();
        public List<int> Data { get; set; } = new();
        public List<string> Colors { get; set; } = new(); 
    }

    public class MultiSeriesChartJson
    {
        public List<string> Labels { get; set; } = new();
        public List<ChartDataset> Datasets { get; set; } = new();
    }

    public class ChartDataset
    {
        public string Label { get; set; } = null!;
        public List<int> Data { get; set; } = new();
        public string BorderColor { get; set; } = null!;
        public string BackgroundColor { get; set; } = null!;
    }
}
