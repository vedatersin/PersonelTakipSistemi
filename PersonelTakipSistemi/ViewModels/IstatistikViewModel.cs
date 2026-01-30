using PersonelTakipSistemi.Models;

namespace PersonelTakipSistemi.ViewModels
{
    public class IstatistikViewModel
    {
        // Kartlar (Özet Sayılar)
        public int ToplamPersonel { get; set; }
        public int ToplamKomisyon { get; set; }
        public int ToplamGorev { get; set; } // Atama sayısı
        public double DolulukOrani { get; set; } // Personel/Komisyon oranı vb.

        // Tablolar / Listeler
        public List<KomisyonDoluluk> KomisyonDoluluklari { get; set; } = new();
        public List<TeskilatDagilimi> TeskilatDagilimlari { get; set; } = new();
        
        // Grafikler için veri yapıları (gerekirse JSON string olarak da tutulabilir)
        public int KadinPersonelSayisi { get; set; }
        public int ErkekPersonelSayisi { get; set; }
    }

    public class KomisyonDoluluk
    {
        public string KomisyonAd { get; set; } = null!;
        public string BagliBirim { get; set; } = null!; // Koordinatörlük
        public int UyeSayisi { get; set; }
        public double DolulukYuzdesi { get; set; } // Max kapasiteye göre (örn: 10 kişi)
    }

    public class TeskilatDagilimi
    {
        public string BirimAd { get; set; } = null!;
        public int PersonelSayisi { get; set; }
    }
}
