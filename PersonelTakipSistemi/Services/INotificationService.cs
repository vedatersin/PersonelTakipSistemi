using PersonelTakipSistemi.Dtos;

namespace PersonelTakipSistemi.Services
{
    public interface INotificationService
    {
        Task CreateAsync(int aliciId, int? gonderenId, string baslik, string aciklama, string tip, string? refType = null, int? refId = null, string? url = null);
        Task<List<BildirimDto>> GetInboxAsync(int aliciId, int take = 200);
        Task<(int unreadCount, List<BildirimMiniDto> top)> GetTopUnreadAsync(int aliciId, int take = 5);
        Task MarkAsReadAsync(int aliciId, int bildirimId);
        Task MarkAllAsReadAsync(int aliciId);
    }
}
