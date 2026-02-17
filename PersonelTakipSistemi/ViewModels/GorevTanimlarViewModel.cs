using PersonelTakipSistemi.Models;

namespace PersonelTakipSistemi.ViewModels
{
    public class GorevTanimlarViewModel
    {
        public List<GorevKategori> Kategoriler { get; set; } = new List<GorevKategori>();
        public List<GorevDurum> Durumlar { get; set; } = new List<GorevDurum>();
    }
}
