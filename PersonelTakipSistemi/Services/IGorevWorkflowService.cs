using PersonelTakipSistemi.Models;
using PersonelTakipSistemi.Models.ViewModels;

namespace PersonelTakipSistemi.Services
{
    public interface IGorevWorkflowService
    {
        Task<GorevAtamaResultViewModel?> GetAssignmentDataAsync(int gorevId);
        Task<GorevCommandResult> SaveAssignmentsAsync(GorevAtamaViewModel model, int? senderPersonelId);
        Task<GorevCommandResult> UpdateStatusAsync(GorevDurumUpdateViewModel model, int currentUserId, bool isManager);
        Task<List<GorevSearchEntityResult>> SearchEntitiesAsync(string type, string query);
        Task<List<GorevStatusHistoryResult>> GetStatusHistoryAsync(int gorevId);
        Task<List<Gorev>> GetUserTasksAsync(int userId);
        Task<Gorev?> GetDetailAsync(int gorevId);
    }
}
