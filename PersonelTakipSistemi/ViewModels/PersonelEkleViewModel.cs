namespace PersonelTakipSistemi.ViewModels
{
    public class PersonelEkleViewModel
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string TcKimlikNo { get; set; }
        public string Telefon { get; set; }
        public string Eposta { get; set; }
        public int PersonelCinsiyet { get; set; } // 0: Erkek, 1: Kadın
        public string GorevliIl { get; set; }
        public string Brans { get; set; }
        public string KadroKurum { get; set; }
        public bool AktifMi { get; set; } = true;
    }
}
