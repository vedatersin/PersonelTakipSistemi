using System.Collections.Generic;

namespace PersonelTakipSistemi.ViewModels
{
    public class PersonelDetayViewModel
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string TcKimlikNo { get; set; }
        public string Telefon { get; set; }
        public string Eposta { get; set; }
        public int PersonelCinsiyet { get; set; } // 0: Erkek, 1: Kadın
        public string CinsiyetAdi => PersonelCinsiyet == 1 ? "Kadın" : "Erkek";
        public string GorevliIl { get; set; }
        public string Brans { get; set; }
        public string KadroKurum { get; set; }
        public bool AktifMi { get; set; }
        
        // Mock lists for detail view tabs
        public List<string> KullanilanYazilimlar { get; set; } = new List<string>();
        public List<string> UzmanlikAlanlari { get; set; } = new List<string>();

        public PersonelDetayViewModel()
        {
            // Dummy data for visual purposes
            KullanilanYazilimlar = new List<string> { "Visual Studio", "SQL Key", "Postman" };
            UzmanlikAlanlari = new List<string> { ".NET Core MVC", "Entity Framework", "Web API" };
        }
    }
}
