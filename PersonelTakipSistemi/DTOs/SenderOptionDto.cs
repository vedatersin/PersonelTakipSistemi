namespace PersonelTakipSistemi.Dtos
{
    public class SenderOptionDto
    {
        public string Id { get; set; } = null!;      // "System" or "123"
        public string Ad { get; set; } = null!;
        public string Title { get; set; } = null!;   // Group Name (e.g. System, Manager)
        public string? AvatarUrl { get; set; }
        public bool IsSystem { get; set; }
    }
}
