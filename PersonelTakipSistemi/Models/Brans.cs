using System.Collections.Generic;

namespace PersonelTakipSistemi.Models
{
    public class Brans
    {
        public int BransId { get; set; }
        public string Ad { get; set; } = null!;

        // Navigation Property
        public ICollection<Personel> Personeller { get; set; } = new List<Personel>();
    }
}
