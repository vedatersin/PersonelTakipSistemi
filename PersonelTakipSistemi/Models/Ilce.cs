using System;
using System.Collections.Generic;

namespace PersonelTakipSistemi.Models
{
    public class Ilce
    {
        public int IlceId { get; set; }
        public string Ad { get; set; } = null!;
        public int IlId { get; set; }
        public Il Il { get; set; } = null!;

        // Navigation Property: Personel Kadroları
        public ICollection<Personel> KadroPersonelleri { get; set; } = new List<Personel>();
    }
}
