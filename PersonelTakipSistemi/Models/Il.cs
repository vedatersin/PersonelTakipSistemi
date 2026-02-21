using System.Collections.Generic;

namespace PersonelTakipSistemi.Models
{
    public class Il
    {
        public int IlId { get; set; }
        public string Ad { get; set; } = null!;

        // Navigation Property: Görev Yeri Personelleri
        public ICollection<Personel> Personeller { get; set; } = new List<Personel>();

        // Navigation Property: Kadrosu Bu İlde Olan Personeller
        public ICollection<Personel> KadroPersonelleri { get; set; } = new List<Personel>();

        // Navigation Property: İlçeler
        public ICollection<Ilce> Ilceler { get; set; } = new List<Ilce>();
    }
}
