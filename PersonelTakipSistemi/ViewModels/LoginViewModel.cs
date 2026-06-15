using System.ComponentModel.DataAnnotations;

namespace PersonelTakipSistemi.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "T.C. Kimlik No zorunludur.")]
        [Display(Name = "T.C. Kimlik No")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Hatalı kullanıcı giriş bilgisi")] // 11 char exact
        [RegularExpression(@"^\d{11}$", ErrorMessage = "Hatalı kullanıcı giriş bilgisi")] // Only digits
        public string TcKimlikNo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Şifre zorunludur.")]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string Sifre { get; set; } = string.Empty;

        [Display(Name = "Beni Hatırla")]
        public bool RememberMe { get; set; }
    }

    public class SifreSifirlaViewModel
    {
        [Required(ErrorMessage = "T.C. Kimlik No zorunludur.")]
        [Display(Name = "T.C. Kimlik No")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "T.C. Kimlik No 11 haneli olmalıdır.")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "T.C. Kimlik No sadece rakamlardan oluşmalıdır.")]
        public string TcKimlikNo { get; set; } = string.Empty;

        public string CaptchaKey { get; set; } = string.Empty;
        public string CaptchaQuestion { get; set; } = string.Empty;

        [Required(ErrorMessage = "Güvenlik doğrulaması zorunludur.")]
        [Display(Name = "CAPTCHA Kodu")]
        public string CaptchaCevap { get; set; } = string.Empty;
    }

    public class SifreBelirleViewModel
    {
        [Required]
        public string Token { get; set; } = string.Empty;

        [Required(ErrorMessage = "Yeni şifre zorunludur.")]
        [DataType(DataType.Password)]
        [Display(Name = "Yeni Şifre")]
        public string YeniSifre { get; set; } = string.Empty;

        [Required(ErrorMessage = "Şifre tekrarı zorunludur.")]
        [DataType(DataType.Password)]
        [Display(Name = "Yeni Şifre Tekrar")]
        [Compare(nameof(YeniSifre), ErrorMessage = "Şifreler eşleşmiyor.")]
        public string YeniSifreTekrar { get; set; } = string.Empty;
    }
}
