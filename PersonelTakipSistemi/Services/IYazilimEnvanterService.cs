using PersonelTakipSistemi.ViewModels;

namespace PersonelTakipSistemi.Services
{
    public interface IYazilimEnvanterService
    {
        Task<List<LookupItemVm>> GetActiveSoftwareDefinitionsAsync();
        Task<YazilimListePageViewModel> GetMySoftwareAsync(int currentPersonelId, YazilimListeFilterViewModel filter);
        Task<YazilimListePageViewModel> GetManagedSoftwareAsync(int currentPersonelId, bool isAdmin, YazilimListeFilterViewModel filter);
        Task<int> CreateSoftwareAsync(YazilimCreateViewModel model, int currentPersonelId, bool isCoordinator);
        Task<bool> UpdateSoftwareAsync(YazilimCreateViewModel model, int currentPersonelId, bool canManage);
        Task QuickUpdateSoftwareAsync(YazilimHizliDuzenleViewModel model, int currentPersonelId, bool isAdmin);
        Task DeleteSoftwareAsync(int yazilimKaydiId, int currentPersonelId, bool isAdmin);
        Task ApproveSoftwareAsync(int yazilimKaydiId, int currentPersonelId);
        Task<YazilimDetayViewModel?> GetDetailAsync(int yazilimKaydiId, int currentPersonelId, bool canManage);
        Task TransferSoftwareAsync(YazilimTransferFormModel model, int currentPersonelId, bool canManage);
        Task<bool> IsCoordinatorAsync(int personelId);
    }
}
