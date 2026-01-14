namespace PersonelTakipSistemi.Dtos
{
    public class SenderOptionDto
    {
        public string Id { get; set; } // "System" or "P_123"
        public string Ad { get; set; } = null!;
        public string? Title { get; set; } // Admin, Yönetici
        public string? AvatarUrl { get; set; }
        public bool IsSystem { get; set; }
    }
}
