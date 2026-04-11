using PersonelTakipSistemi.Models;

namespace PersonelTakipSistemi.ViewModels
{
    public class CihazTanimlariViewModel
    {
        public int? SeciliCihazTuruId { get; set; }
        public List<CihazTuru> CihazTurleri { get; set; } = new();
        public List<CihazMarka> Markalar { get; set; } = new();
        public CihazTuruFormModel YeniCihazTuru { get; set; } = new();
        public CihazMarkaFormModel YeniMarka { get; set; } = new();
    }

    public class CihazTanimResponseViewModel
    {
        public int? SeciliCihazTuruId { get; set; }
        public List<CihazTuruListItemViewModel> CihazTurleri { get; set; } = new();
        public List<CihazMarkaListItemViewModel> Markalar { get; set; } = new();
    }

    public class CihazTuruListItemViewModel
    {
        public int CihazTuruId { get; set; }
        public string Ad { get; set; } = string.Empty;
        public string? KullanimAmaci { get; set; }
        public bool SistemSecenegiMi { get; set; }
        public int BagliMarkaSayisi { get; set; }
        public int BagliCihazSayisi { get; set; }
    }

    public class CihazMarkaListItemViewModel
    {
        public int CihazMarkaId { get; set; }
        public int CihazTuruId { get; set; }
        public string Ad { get; set; } = string.Empty;
        public bool SistemSecenegiMi { get; set; }
        public int BagliCihazSayisi { get; set; }
    }

    public class CihazTuruFormModel
    {
        public int? CihazTuruId { get; set; }
        public string Ad { get; set; } = string.Empty;
        public string? KullanimAmaci { get; set; }
    }

    public class CihazMarkaFormModel
    {
        public int? CihazMarkaId { get; set; }
        public int CihazTuruId { get; set; }
        public string Ad { get; set; } = string.Empty;
    }

    public class CihazTanimSilModel
    {
        public int Id { get; set; }
        public bool Onaylandi { get; set; }
        public int? SeciliCihazTuruId { get; set; }
    }

    public class CihazCreateViewModel
    {
        public int? CihazId { get; set; }
        public int? CihazTuruId { get; set; }
        public int? CihazMarkaId { get; set; }
        public string? DigerCihazTuruAd { get; set; }
        public string? DigerMarkaAd { get; set; }
        public string Model { get; set; } = string.Empty;
        public string Ozellikler { get; set; } = string.Empty;
        public string SeriNo { get; set; } = string.Empty;
        public int? SahipPersonelId { get; set; }
    }

    public class CihazListeFilterViewModel
    {
        public int? KoordinatorlukId { get; set; }
        public int? PersonelId { get; set; }
        public int? CihazTuruId { get; set; }
        public int? CihazMarkaId { get; set; }
        public string? ModelAra { get; set; }
        public string? OzellikAra { get; set; }
        public string? SeriNoAra { get; set; }
        public int YasSliderDegeri { get; set; } = -1;
        public bool SadeceOnayBekleyenler { get; set; }
    }

    public class CihazListeItemViewModel
    {
        public int CihazId { get; set; }
        public int CihazTuruId { get; set; }
        public int CihazMarkaId { get; set; }
        public string? DigerCihazTuruAd { get; set; }
        public string? DigerMarkaAd { get; set; }
        public string CihazTuru { get; set; } = string.Empty;
        public string Marka { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Ozellikler { get; set; } = string.Empty;
        public string SeriNo { get; set; } = string.Empty;
        public string SahipAdSoyad { get; set; } = string.Empty;
        public string KoordinatorlukAd { get; set; } = string.Empty;
        public DateTime IlkKayitTarihi { get; set; }
        public DateTime AktifSahiplikBaslangicTarihi { get; set; }
        public bool KoordinatorTarafindanEklendi { get; set; }
        public CihazOnayDurumu OnayDurumu { get; set; }
    }

    public class CihazListePageViewModel
    {
        public string Baslik { get; set; } = string.Empty;
        public bool KoordinatorPaneliMi { get; set; }
        public bool AdminPaneliMi { get; set; }
        public bool PersonelGorunumuMu { get; set; }
        public bool CihazEklemeYetkisiVarMi { get; set; }
        public bool OnayYetkisiVarMi { get; set; }
        public CihazListeFilterViewModel Filter { get; set; } = new();
        public CihazCreateViewModel YeniCihaz { get; set; } = new();
        public List<CihazListeItemViewModel> Cihazlar { get; set; } = new();
        public List<LookupItemVm> CihazTurleri { get; set; } = new();
        public List<LookupItemVm> Markalar { get; set; } = new();
        public List<LookupItemVm> Koordinatorlukler { get; set; } = new();
        public List<LookupItemVm> Personeller { get; set; } = new();
        public List<LookupItemVm> EklemeIcinCihazTurleri { get; set; } = new();
        public List<LookupItemVm> EklemeIcinMarkalar { get; set; } = new();
        public List<LookupItemVm> EklemeIcinPersoneller { get; set; } = new();
    }

    public class CihazDetayViewModel
    {
        public int CihazId { get; set; }
        public string CihazTuru { get; set; } = string.Empty;
        public string Marka { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Ozellikler { get; set; } = string.Empty;
        public string SeriNo { get; set; } = string.Empty;
        public string SahipAdSoyad { get; set; } = string.Empty;
        public string KoordinatorlukAd { get; set; } = string.Empty;
        public string KayitTarihiBasligi { get; set; } = string.Empty;
        public DateTime GosterilecekKayitTarihi { get; set; }
        public DateTime IlkKayitTarihi { get; set; }
        public CihazOnayDurumu OnayDurumu { get; set; }
        public bool OnayYetkisiVarMi { get; set; }
        public bool HareketlerGorunsunMu { get; set; }
        public bool DevirYetkisiVarMi { get; set; }
        public bool DuzenlemeYetkisiVarMi { get; set; }
        public List<LookupItemVm> CihazTurleri { get; set; } = new();
        public List<LookupItemVm> Markalar { get; set; } = new();
        public CihazCreateViewModel DuzenleFormu { get; set; } = new();
        public List<LookupItemVm> DevredilebilirPersoneller { get; set; } = new();
        public CihazTransferFormModel DevirFormu { get; set; } = new();
        public List<CihazHareketItemViewModel> Hareketler { get; set; } = new();
    }

    public class CihazTransferFormModel
    {
        public int CihazId { get; set; }
        public int YeniSahipPersonelId { get; set; }
        public string DevirNotu { get; set; } = string.Empty;
        public string CihazDurumNotu { get; set; } = string.Empty;
    }

    public class CihazHizliDuzenleViewModel
    {
        public int CihazId { get; set; }
        public int CihazTuruId { get; set; }
        public int CihazMarkaId { get; set; }
        public string? DigerCihazTuruAd { get; set; }
        public string? DigerMarkaAd { get; set; }
        public string Model { get; set; } = string.Empty;
        public string Ozellikler { get; set; } = string.Empty;
        public string SeriNo { get; set; } = string.Empty;
    }

    public class CihazHareketItemViewModel
    {
        public DateTime Tarih { get; set; }
        public string HareketTuru { get; set; } = string.Empty;
        public string IslemYapan { get; set; } = string.Empty;
        public string? OncekiSahip { get; set; }
        public string? YeniSahip { get; set; }
        public string? Aciklama { get; set; }
        public string? DurumNotu { get; set; }
    }

    public class YazilimLisanslarimViewModel
    {
        public List<string> Yazilimlar { get; set; } = new();
    }
}
