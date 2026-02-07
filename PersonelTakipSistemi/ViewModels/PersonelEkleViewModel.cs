using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace PersonelTakipSistemi.ViewModels
{
    public class PersonelEkleViewModel : IValidatableObject
    {
        public int? PersonelId { get; set; } // For Edit Mode
        public bool IsEditMode { get; set; } // Explicit Mode Flag
        public bool IsAuthSkipped { get; set; } // Skip Authorization Step

        [Required(ErrorMessage = "Ad alanı zorunludur.")]
        public string Ad { get; set; } = null!;

        [Required(ErrorMessage = "Soyad alanı zorunludur.")]
        public string Soyad { get; set; } = null!;

        [Required(ErrorMessage = "TC Kimlik No zorunludur.")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "TC Kimlik No 11 karakter olmalıdır.")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "TC Kimlik No 11 haneli olmalıdır.")]
        public string TcKimlikNo { get; set; } = null!;

        [Required(ErrorMessage = "Telefon alanı zorunludur.")]
        public string Telefon { get; set; } = null!;

        [Required(ErrorMessage = "E-posta alanı zorunludur.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Lütfen geçerli bir e-posta adresi giriniz (Örn: isim@ornek.com).")]
        public string Eposta { get; set; } = null!;

        public int PersonelCinsiyet { get; set; } // 0: Erkek, 1: Kadın
        
        [Required(ErrorMessage = "Doğum Tarihi zorunludur.")]
        public DateTime DogumTarihi { get; set; }

        [Required(ErrorMessage = "İl seçimi zorunludur.")]
        [Range(1, int.MaxValue, ErrorMessage = "İl seçimi zorunludur.")]
        public int GorevliIlId { get; set; }
        
        [Required(ErrorMessage = "Branş seçimi zorunludur.")]
        [Range(1, int.MaxValue, ErrorMessage = "Branş seçimi zorunludur.")]
        public int BransId { get; set; }
        
        [Required(ErrorMessage = "Kadro/Kurum zorunludur.")]
        public string KadroKurum { get; set; } = null!;
        public bool AktifMi { get; set; } = true;
        public string? FotografYolu { get; set; } // Existing photo path
        public string? FotografBase64 { get; set; } // Keep photo on validation error

        // Şifre artık sadece Insert'te otomatik, Update'te isteğe bağlı.
        // Bu yüzden Required değil, nullable.
        public string? Sifre { get; set; } 

        // Şifre Değiştirme (Sadece Edit Modunda Görünür/Kullanılır)
        [DataType(DataType.Password)]
        public string? EskiSifre { get; set; }

        [StringLength(20, MinimumLength = 6, ErrorMessage = "Şifre en az 6 karakter olmalıdır.")]
        public string? NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessage = "Şifreler eşleşmiyor.")]
        public string? NewPasswordConfirm { get; set; }

        // Seçilen Checkbox ID'leri
        public List<int> SeciliYazilimIdleri { get; set; } = new List<int>();
        public List<int> SeciliUzmanlikIdleri { get; set; } = new List<int>();
        public List<int> SeciliGorevTuruIdleri { get; set; } = new List<int>();

        public List<int> SeciliIsNiteligiIdleri { get; set; } = new List<int>();

        // Yeni "Yetkiler" Tabı İçin Özellikler
        // System Role
        public int? SistemRolId { get; set; } // Dropdown
        public string? YetkiKapsami { get; set; } = "Self"; // Self, Koordinatorluk, Komisyon, All

        // Dynamic Role Assignments (Multiple)
        // We will receive a JSON string or manage list binding manually. 
        // For simplicity in MVC form post, complex list binding can be tricky with dynamic JS add/remove.
        // We can use a JSON string hidden field for the list of roles to be parsed on backend.
        public string? AtananRollerJson { get; set; } 

        // Or we use standard list binding if we index inputs correctly:
        // public List<PersonelRolAtamaModel> AtananRoller { get; set; } = new List<PersonelRolAtamaModel>();

        // Ekranda Listelenecek Veriler
        public List<LookupItemVm> Yazilimlar { get; set; } = new List<LookupItemVm>();
        public List<LookupItemVm> Uzmanliklar { get; set; } = new List<LookupItemVm>();
        public List<LookupItemVm> GorevTurleri { get; set; } = new List<LookupItemVm>();
        public List<LookupItemVm> IsNitelikleri { get; set; } = new List<LookupItemVm>();
        public List<LookupItemVm> Iller { get; set; } = new List<LookupItemVm>();

        public List<LookupItemVm> Branslar { get; set; } = new List<LookupItemVm>();
        
        // Yetki Tanımları için Listeler
        public List<LookupItemVm> SistemRolleri { get; set; } = new List<LookupItemVm>();
        public List<LookupItemVm> KurumsalRoller { get; set; } = new List<LookupItemVm>();
        public List<LookupItemVm> Teskilatlar { get; set; } = new List<LookupItemVm>();
        // Koordinatorlukler & Komisyonlar will be loaded via AJAX based on selection

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // 18 yaş kontrolü
            var minDate = DateTime.Today.AddYears(-18);
            if (DogumTarihi > minDate)
            {
                yield return new ValidationResult(
                    "Personel en az 18 yaşında olmalıdır.",
                    new[] { nameof(DogumTarihi) });
            }

            // Mantıksız tarih kontrolü (Opsiyonel, zaten min="1900" view tarafında var ama backend de koruyalım)
            if (DogumTarihi < new DateTime(1900, 1, 1))
            {
                yield return new ValidationResult(
                    "Doğum tarihi 1900 yılından küçük olamaz.",
                    new[] { nameof(DogumTarihi) });
            }
        }
    }

    public class LookupItemVm
    {
        public int Id { get; set; }
        public string Ad { get; set; } = null!;
    }
}
