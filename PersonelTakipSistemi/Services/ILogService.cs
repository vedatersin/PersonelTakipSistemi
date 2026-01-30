using PersonelTakipSistemi.Models;

namespace PersonelTakipSistemi.Services
{
    public interface ILogService
    {
        Task LogAsync(string islemTuru, string aciklama, int? personelId = null, string? detay = null);
        Task<List<SistemLog>> GetLogsAsync(int page = 1, int pageSize = 20, string search = "", string type = "", DateTime? baslangic = null, DateTime? bitis = null);
        Task<int> GetTotalCountAsync(string search = "", string type = "", DateTime? baslangic = null, DateTime? bitis = null);
    }
}
