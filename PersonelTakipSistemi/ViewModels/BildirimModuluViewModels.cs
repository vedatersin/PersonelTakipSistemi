namespace PersonelTakipSistemi.ViewModels
{
    public class BildirimListeItemViewModel
    {
        public int BildirimId { get; set; }
        public string Baslik { get; set; } = null!;
        public string AliciAdSoyad { get; set; } = null!;
        public string GonderenAdSoyad { get; set; } = null!;
        public string Tip { get; set; } = "-";
        public DateTime GonderimTarihi { get; set; }
        public string Durum { get; set; } = null!;
        public DateTime? DurumTarihi { get; set; }
        public string TickClass { get; set; } = "text-muted";
        public int TickCount { get; set; } = 1;
    }

    public class BildirimListeViewModel
    {
        public List<BildirimListeItemViewModel> Items { get; set; } = new();
        public int Page { get; set; }
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }
        public string? Search { get; set; }
    }

    public class BildirimDetayViewModel : BildirimListeItemViewModel
    {
        public string Aciklama { get; set; } = null!;
        public string? Url { get; set; }
        public string? RefType { get; set; }
        public int? RefId { get; set; }
        public bool OkunduMu { get; set; }
        public DateTime? OkunmaTarihi { get; set; }
    }
}
