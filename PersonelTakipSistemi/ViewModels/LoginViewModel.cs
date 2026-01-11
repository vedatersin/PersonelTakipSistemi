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
}
