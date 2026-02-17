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
        public bool TasraTeskilatiVarMi { get; set; } = true; // Default true (Merkez için)
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
}
