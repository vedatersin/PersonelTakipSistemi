using PersonelTakipSistemi.Models;

namespace PersonelTakipSistemi.ViewModels
{
    public class GorevTanimlarViewModel
    {
        public List<GorevDurum> Durumlar { get; set; } = new List<GorevDurum>();
    }
}
