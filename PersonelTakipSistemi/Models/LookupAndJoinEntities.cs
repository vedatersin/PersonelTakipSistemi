using System.Collections.Generic;

namespace PersonelTakipSistemi.Models
{
    // Lookup Entities
    public class Yazilim
    {
        public int YazilimId { get; set; }
        public string Ad { get; set; } = null!;
        public ICollection<PersonelYazilim> PersonelYazilimlar { get; set; } = new List<PersonelYazilim>();
    }

    public class Uzmanlik
    {
        public int UzmanlikId { get; set; }
        public string Ad { get; set; } = null!;
        public ICollection<PersonelUzmanlik> PersonelUzmanliklar { get; set; } = new List<PersonelUzmanlik>();
    }

    public class GorevTuru
    {
        public int GorevTuruId { get; set; }
        public string Ad { get; set; } = null!;
        public ICollection<PersonelGorevTuru> PersonelGorevTurleri { get; set; } = new List<PersonelGorevTuru>();
    }

    public class IsNiteligi
    {
        public int IsNiteligiId { get; set; }
        public string Ad { get; set; } = null!;
        public ICollection<PersonelIsNiteligi> PersonelIsNitelikleri { get; set; } = new List<PersonelIsNiteligi>();
    }

    // Join Entities
    public class PersonelYazilim
    {
        public int PersonelId { get; set; }
        public Personel Personel { get; set; } = null!;
        public int YazilimId { get; set; }
        public Yazilim Yazilim { get; set; } = null!;
    }

    public class PersonelUzmanlik
    {
        public int PersonelId { get; set; }
        public Personel Personel { get; set; } = null!;
        public int UzmanlikId { get; set; }
        public Uzmanlik Uzmanlik { get; set; } = null!;
    }

    public class PersonelGorevTuru
    {
        public int PersonelId { get; set; }
        public Personel Personel { get; set; } = null!;
        public int GorevTuruId { get; set; }
        public GorevTuru GorevTuru { get; set; } = null!;
    }

    public class PersonelIsNiteligi
    {
        public int PersonelId { get; set; }
        public Personel Personel { get; set; } = null!;
        public int IsNiteligiId { get; set; }
        public IsNiteligi IsNiteligi { get; set; } = null!;
    }
}
