using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace PersonelTakipSistemi.ViewModels
{
    public class PersonelEkleViewModel
    {
        [Required(ErrorMessage = "Ad alanı zorunludur.")]
        public string Ad { get; set; } = null!;

        [Required(ErrorMessage = "Soyad alanı zorunludur.")]
        public string Soyad { get; set; } = null!;

        [Required(ErrorMessage = "TC Kimlik No zorunludur.")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "TC Kimlik No 11 karakter olmalıdır.")]
        public string TcKimlikNo { get; set; } = null!;

        public string? Telefon { get; set; }

        [Required(ErrorMessage = "E-posta alanı zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string Eposta { get; set; } = null!;

        public int PersonelCinsiyet { get; set; } // 0: Erkek, 1: Kadın

        public string? GorevliIl { get; set; }
        public string? Brans { get; set; }
        public string? KadroKurum { get; set; }
        public bool AktifMi { get; set; }
        public string? FotografBase64 { get; set; } // Keep photo on validation error

        [Required(ErrorMessage = "Şifre zorunludur.")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "Şifre tam 6 karakter olmalıdır.")]
        public string Sifre { get; set; } = null!;

        // Seçilen Checkbox ID'leri
        public List<int> SeciliYazilimIdleri { get; set; } = new List<int>();
        public List<int> SeciliUzmanlikIdleri { get; set; } = new List<int>();
        public List<int> SeciliGorevTuruIdleri { get; set; } = new List<int>();
        public List<int> SeciliIsNiteligiIdleri { get; set; } = new List<int>();

        // Ekranda Listelenecek Veriler
        public List<LookupItemVm> Yazilimlar { get; set; } = new List<LookupItemVm>();
        public List<LookupItemVm> Uzmanliklar { get; set; } = new List<LookupItemVm>();
        public List<LookupItemVm> GorevTurleri { get; set; } = new List<LookupItemVm>();
        public List<LookupItemVm> IsNitelikleri { get; set; } = new List<LookupItemVm>();
    }

    public class LookupItemVm
    {
        public int Id { get; set; }
        public string Ad { get; set; } = null!;
    }
}
