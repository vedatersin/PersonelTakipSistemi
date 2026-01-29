using System;
using System.Collections.Generic;

namespace PersonelTakipSistemi.ViewModels
{
    public class PersonelDetayViewModel
    {
        public int PersonelId { get; set; }
        public string Ad { get; set; } = null!;
        public string Soyad { get; set; } = null!;
        public string TcKimlikNo { get; set; } = null!;
        public string Telefon { get; set; } = null!;
        public string Eposta { get; set; } = null!;
        public string Cinsiyet { get; set; } = null!; // Erkek/Kadın olarak string dönebiliriz veya View'da işleyebiliriz
        public string GorevliIl { get; set; } = null!;
        public string Brans { get; set; } = null!;
        public string KadroKurum { get; set; } = null!;
        public bool AktifMi { get; set; }
        public string? FotografYolu { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<string> Yazilimlar { get; set; } = new List<string>();
        public List<string> Uzmanliklar { get; set; } = new List<string>();
        public List<string> GorevTurleri { get; set; } = new List<string>();
        public List<string> IsNitelikleri { get; set; } = new List<string>();

        // 2024-01-28 Refactor: Show assigned roles in profile card
        public List<string> KurumsalRoller { get; set; } = new List<string>();


        // Rol bazlı UI kontrolü için
        public string SistemRol { get; set; } = "Kullanıcı";
    }
}
