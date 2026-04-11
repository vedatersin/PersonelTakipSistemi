using PersonelTakipSistemi.ViewModels;

namespace PersonelTakipSistemi.Services
{
    public interface IYazilimLisansService
    {
        Task<YazilimLisansYonetimPageViewModel> GetYonetimPageAsync(int currentPersonelId, bool lisansYonetebilirMi, bool kullaniciYonetebilirMi);
        Task<YazilimLisansDetayPageViewModel?> GetDetayPageAsync(int lisansId, int currentPersonelId, bool lisansYonetebilirMi, bool kullaniciYonetebilirMi);
        Task CreateLisansAsync(YazilimLisansFormViewModel model, int currentPersonelId, bool lisansYonetebilirMi);
        Task UpdateLisansAsync(YazilimLisansFormViewModel model, int currentPersonelId, bool lisansYonetebilirMi);
        Task DeleteLisansAsync(int lisansId, int currentPersonelId, bool lisansYonetebilirMi);
        Task AddKullaniciAsync(YazilimLisansKullaniciFormViewModel model, int currentPersonelId, bool kullaniciYonetebilirMi);
        Task UpdateKullaniciAsync(YazilimLisansKullaniciFormViewModel model, int currentPersonelId, bool kullaniciYonetebilirMi);
        Task DeleteKullaniciAsync(int lisansKullaniciId, int lisansId, int currentPersonelId, bool kullaniciYonetebilirMi);
        Task PersonelOnayaGonderAsync(YazilimLisansKullaniciOnayFormViewModel model, int currentPersonelId);
        Task YoneticiOnayKarariVerAsync(YazilimLisansKullaniciKararFormViewModel model, int currentPersonelId, bool kullaniciYonetebilirMi);
        Task<YazilimLisanslarimViewModel> GetPersonelLisanslarimAsync(int personelId);
    }
}
