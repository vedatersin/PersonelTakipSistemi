using System.Collections.Generic;
using PersonelTakipSistemi.Models;

namespace PersonelTakipSistemi.ViewModels
{
    public class PersonelAyarlarViewModel
    {
        public List<Brans> Branslar { get; set; } = new List<Brans>();
        public List<Yazilim> Yazilimlar { get; set; } = new List<Yazilim>();
        public List<Uzmanlik> Uzmanliklar { get; set; } = new List<Uzmanlik>();
        public List<GorevTuru> GorevTurleri { get; set; } = new List<GorevTuru>();
        public List<IsNiteligi> IsNitelikleri { get; set; } = new List<IsNiteligi>();
        public List<KurumsalRol> KurumsalRoller { get; set; } = new List<KurumsalRol>();
    }

    public class PersonelAyarEkleModel
    {
        public string Type { get; set; } // "brans", "yazilim", "uzmanlik", "gorevturu", "isniteligi", "kurumsalrol"
        public string Ad { get; set; }
    }
}
