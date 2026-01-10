using System.Collections.Generic;

namespace PersonelTakipSistemi.Models
{
    public class Il
    {
        public int IlId { get; set; }
        public string Ad { get; set; } = null!;

        // Navigation Property
        public ICollection<Personel> Personeller { get; set; } = new List<Personel>();
    }
}
