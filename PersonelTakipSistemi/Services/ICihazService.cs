using PersonelTakipSistemi.ViewModels;

namespace PersonelTakipSistemi.Services
{
    public interface ICihazService
    {
        Task<CihazTanimlariViewModel> GetDefinitionsAsync(int? selectedTypeId = null);
        Task<CihazTanimResponseViewModel> GetDefinitionsDataAsync(int? selectedTypeId = null);
        Task AddDeviceTypeAsync(CihazTuruFormModel model);
        Task UpdateDeviceTypeAsync(CihazTuruFormModel model);
        Task DeleteDeviceTypeAsync(int cihazTuruId, bool onaylandi);
        Task AddBrandAsync(CihazMarkaFormModel model);
        Task UpdateBrandAsync(CihazMarkaFormModel model);
        Task DeleteBrandAsync(int cihazMarkaId, bool onaylandi);
        Task<List<LookupItemVm>> GetActiveDeviceTypesAsync();
        Task<List<LookupItemVm>> GetBrandsByTypeAsync(int cihazTuruId);
        Task<CihazListePageViewModel> GetMyDevicesAsync(int currentPersonelId, CihazListeFilterViewModel filter);
        Task<CihazListePageViewModel> GetManagedDevicesAsync(int currentPersonelId, bool isAdmin, CihazListeFilterViewModel filter);
        Task<int> CreateDeviceAsync(CihazCreateViewModel model, int currentPersonelId, bool isCoordinator);
        Task<bool> UpdateDeviceAsync(CihazCreateViewModel model, int currentPersonelId, bool canManage);
        Task QuickUpdateDeviceAsync(CihazHizliDuzenleViewModel model, int currentPersonelId, bool isAdmin);
        Task DeleteDeviceAsync(int cihazId, int currentPersonelId, bool isAdmin);
        Task ApproveDeviceAsync(int cihazId, int currentPersonelId);
        Task<CihazDetayViewModel?> GetDetailAsync(int cihazId, int currentPersonelId, bool canManage);
        Task TransferDeviceAsync(CihazTransferFormModel model, int currentPersonelId, bool canManage);
        Task<YazilimLisanslarimViewModel> GetSoftwareLicensesAsync(int personelId);
        Task<bool> IsCoordinatorAsync(int personelId);
    }
}
