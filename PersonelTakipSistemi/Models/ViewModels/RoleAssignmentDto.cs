namespace PersonelTakipSistemi.ViewModels
{
    public class RoleAssignmentDto
    {
        public string? rolId { get; set; }
        public string? rolAd { get; set; } // For UI
        public string? teskilatId { get; set; }
        public string? teskilatAd { get; set; } // For UI
        public string? koordinatorlukId { get; set; }
        public string? koordinatorlukAd { get; set; } // For UI
        public string? komisyonId { get; set; }
        public string? komisyonAd { get; set; } // For UI
    }
}
