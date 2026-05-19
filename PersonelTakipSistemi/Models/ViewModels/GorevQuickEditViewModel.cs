using System;

namespace PersonelTakipSistemi.Models.ViewModels
{
    public class GorevQuickEditViewModel
    {
        public int GorevId { get; set; }
        public string Ad { get; set; } = string.Empty;
        public string? Aciklama { get; set; }
        public int IsNiteligiId { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public DateTime? BitisTarihi { get; set; }
        public bool IsActive { get; set; }
    }
}
