using PersonelTakipSistemi.Models.ViewModels;

namespace PersonelTakipSistemi.Services
{
    public interface IPersonelAuthorizationService
    {
        Task<YetkilendirmeIndexViewModel> BuildAuthorizationIndexAsync(int currentUserId, bool isAdmin, bool isEditor);
        Task<PersonelYetkiDetailViewModel?> BuildAuthorizationDetailAsync(int personelId, int currentUserId, bool isAdmin);
    }
}
