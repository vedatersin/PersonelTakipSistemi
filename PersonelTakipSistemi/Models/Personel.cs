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
        
        // New FKs
        public int GorevliIlId { get; set; }
        public Il GorevliIl { get; set; } = null!;

        public int BransId { get; set; }
        public Brans Brans { get; set; } = null!;
        
        public string KadroKurum { get; set; } = null!;
        public bool AktifMi { get; set; }
        public string? FotografYolu { get; set; }
        
        // New Field
        public DateTime DogumTarihi { get; set; }

        public string? Sifre { get; set; } // Plain text password for now
        public byte[] SifreHash { get; set; } = null!;
        public byte[] SifreSalt { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }

        // Sistem Rolü (Foreign Key)
        public int SistemRolId { get; set; } = 4; // Default: Kullanıcı (Assuming ID 4 is Kullanıcı)
        public SistemRol SistemRol { get; set; } = null!;

        // Navigation Properties
        public ICollection<PersonelYazilim> PersonelYazilimlar { get; set; } = new List<PersonelYazilim>();
        public ICollection<PersonelUzmanlik> PersonelUzmanliklar { get; set; } = new List<PersonelUzmanlik>();
        public ICollection<PersonelGorevTuru> PersonelGorevTurleri { get; set; } = new List<PersonelGorevTuru>();
        public ICollection<PersonelIsNiteligi> PersonelIsNitelikleri { get; set; } = new List<PersonelIsNiteligi>();

        // Authorization Module Relations
        public ICollection<PersonelTeskilat> PersonelTeskilatlar { get; set; } = new List<PersonelTeskilat>();
        public ICollection<PersonelKoordinatorluk> PersonelKoordinatorlukler { get; set; } = new List<PersonelKoordinatorluk>();
        public ICollection<PersonelKomisyon> PersonelKomisyonlar { get; set; } = new List<PersonelKomisyon>();
        public ICollection<PersonelKurumsalRolAtama> PersonelKurumsalRolAtamalari { get; set; } = new List<PersonelKurumsalRolAtama>();
        
        // Bildirimler
        public ICollection<Bildirim> GelenBildirimler { get; set; } = new List<Bildirim>();
        public ICollection<Bildirim> GonderilenBildirimler { get; set; } = new List<Bildirim>();
    }
}
