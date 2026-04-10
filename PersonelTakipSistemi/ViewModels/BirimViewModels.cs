using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using PersonelTakipSistemi.Models;

namespace PersonelTakipSistemi.ViewModels
{
    // --- Ayarlar Sayfası ---
    public class BirimAyarlariViewModel
    {
        public List<DaireBaskanligi> DaireBaskanliklari { get; set; } = new List<DaireBaskanligi>();
        public List<Teskilat> Teskilatlar { get; set; } = new List<Teskilat>();
        public List<Koordinatorluk> Koordinatorlukler { get; set; } = new List<Koordinatorluk>();
        public List<Komisyon> Komisyonlar { get; set; } = new List<Komisyon>();
        public List<Brans> Branslar { get; set; } = new List<Brans>(); // Opsiyonel branş seçimi için
    }



    public class BirimEkleModel
    {
        public int? Id { get; set; } // İçin düzenleme (opsiyonel)
        [Required]
        public string Ad { get; set; } = null!;
        public string? Tanim { get; set; }
        
        // Parent IDs
        public int? ParentId { get; set; } // Koordinatorluk için TeskilatId, Komisyon için KoordinatorlukId
        public int? IlId { get; set; } // Koordinatorluk için İl
        public bool? TasraTeskilatiVarMi { get; set; } // Default null (değişiklik yok), true/false (değişiklik var)
        
        // Yeni Alanlar (Teşkilat İçin)
        public string? Tur { get; set; } // "Merkez", "Taşra"
        public int? BagliMerkezTeskilatId { get; set; } // Taşra teşkilatı için

        public int? KomisyonBaskaniBirimId { get; set; } // Komisyon için opsiyonel branş vb.
        public int? BagliMerkezKoordinatorlukId { get; set; } // Taşra Komisyonu için Merkez Birim Linki
    }

    // --- Toplu Atama Sayfası ---
    public class TopluAtamaViewModel
    {
        public List<Personel> Personeller { get; set; } = new List<Personel>();
        
        public List<Teskilat> Teskilatlar { get; set; } = new List<Teskilat>();
        // Diğerleri AJAX ile yüklenecek
    }

    public class TopluAtamaPostModel
    {
        public List<int> PersonelIds { get; set; } = new List<int>();
        
        public int? TeskilatId { get; set; }
        public int? KoordinatorlukId { get; set; }
        public int? KomisyonId { get; set; }
    }

    // --- Birimleri Listele Sayfası ---
    public class BirimListeleViewModel
    {
        public List<BirimKartItem> Kartlar { get; set; } = new();
        public List<HaritaIlItem> Iller { get; set; } = new();
        
        // Filtre dropdown'ları
        public List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> DaireList { get; set; } = new();
        public List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> TeskilatList { get; set; } = new();
        
        // Varsayılan seçimler
        public int? DefaultDaireBaskanligiId { get; set; }
        public int? DefaultTeskilatId { get; set; }
        public int? DefaultKoordinatorlukId { get; set; }
    }

    public class BirimKartItem
    {
        public int Id { get; set; }
        public string Ad { get; set; } = null!;
        public string Tur { get; set; } = null!; // "Koordinatorluk" veya "Komisyon"
        public int PersonelSayisi { get; set; }
        public int GorevSayisi { get; set; }
        public int? IlId { get; set; }
        public string? IlAdi { get; set; }
        public int? ParentId { get; set; } // Komisyon için KoordinatorlukId
    }

    public class HaritaIlItem
    {
        public int IlId { get; set; } // Plaka kodu
        public string Ad { get; set; } = null!;
        public bool HasOrganization { get; set; } // Koordinatörlüğü var mı
    }

    // --- Komisyon Detay Sayfası ---
    public class KomisyonDetayViewModel
    {
        public int KomisyonId { get; set; }
        public string KomisyonAd { get; set; } = null!;
        public string? KoordinatorlukAd { get; set; }
        public string? TeskilatAd { get; set; }
        public string? IlAd { get; set; }

        // Personel Listesi
        public List<KomisyonPersonelItem> Personeller { get; set; } = new();

        // Görev Listesi
        public List<KomisyonGorevItem> Gorevler { get; set; } = new();

        // Grafikler
        public ChartDataJson KategoriDagilimi { get; set; } = new();
        public ChartDataJson DurumDagilimi { get; set; } = new();
    }

    public class KomisyonPersonelItem
    {
        public int PersonelId { get; set; }
        public string AdSoyad { get; set; } = null!;
        public string? FotografYolu { get; set; }
        public string? Brans { get; set; }
        public string? Il { get; set; }
        
        // Genişletilmiş Alanlar
        public List<string> Yazilimlar { get; set; } = new();
        public List<string> Uzmanliklar { get; set; } = new();
        public List<string> GorevTurleri { get; set; } = new();
        public List<string> IsNitelikleri { get; set; } = new();
        public string? KadroIl { get; set; }
        public string? KadroIlce { get; set; }
    }

    public class KomisyonGorevItem
    {
        public int GorevId { get; set; }
        public string Baslik { get; set; } = null!;
        public string Durum { get; set; } = null!;
        public string? DurumRenk { get; set; }
        public string? Kategori { get; set; }
        public DateTime? BaslangicTarihi { get; set; }
        public DateTime? BitisTarihi { get; set; }
    }
}
