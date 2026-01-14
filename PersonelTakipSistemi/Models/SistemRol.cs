using System;

namespace PersonelTakipSistemi.Models
{
    public class SistemRol
    {
        public int SistemRolId { get; set; }
        public string Ad { get; set; } = null!; // Admin, Yönetici, Editör, Kullanıcı
    }
}
