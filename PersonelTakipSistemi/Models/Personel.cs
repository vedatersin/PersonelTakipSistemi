using System;
using System.Collections.Generic;

namespace PersonelTakipSistemi.Models
{
    public class Personel
    {
        public int PersonelId { get; set; }
        public string Ad { get; set; } = null!;
        public string Soyad { get; set; } = null!;
        public string TcKimlikNo { get; set; } = null!;
        public string Telefon { get; set; } = null!;
        public string Eposta { get; set; } = null!;
        public bool PersonelCinsiyet { get; set; } // false: Erkek, true: Kadın
        public string GorevliIl { get; set; } = null!;
        public string Brans { get; set; } = null!;
        public string KadroKurum { get; set; } = null!;
        public bool AktifMi { get; set; }
        public string? FotografYolu { get; set; }
        public byte[] SifreHash { get; set; } = null!;
        public byte[] SifreSalt { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }

        // Navigation Properties
        public ICollection<PersonelYazilim> PersonelYazilimlar { get; set; } = new List<PersonelYazilim>();
        public ICollection<PersonelUzmanlik> PersonelUzmanliklar { get; set; } = new List<PersonelUzmanlik>();
        public ICollection<PersonelGorevTuru> PersonelGorevTurleri { get; set; } = new List<PersonelGorevTuru>();
        public ICollection<PersonelIsNiteligi> PersonelIsNitelikleri { get; set; } = new List<PersonelIsNiteligi>();
    }
}
