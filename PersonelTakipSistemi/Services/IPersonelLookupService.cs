using PersonelTakipSistemi.ViewModels;

namespace PersonelTakipSistemi.Services
{
    public interface IPersonelLookupService
    {
        Task FillIndexLookupsAsync(LookupListsViewModel model, PersonelIndexFilterViewModel? filter = null);
        Task<PersonelFormLookupData> FillFormLookupsAsync(PersonelEkleViewModel model);
        Task<List<LookupItemVm>> GetIlceLookupItemsAsync(int ilId);
        Task<(bool TcExists, bool EmailExists)> CheckDuplicatesAsync(int? id, string tc, string email);
        Task<List<string>> GetKoordinatorlukNamesAsync(string? teskilatAd);
        Task<List<string>> GetKomisyonNamesAsync(string? koordinatorlukAd);
        Task<List<LookupItemVm>> GetKoordinatorluklerByTeskilatAsync(int teskilatId);
        Task<List<LookupItemVm>> GetKomisyonlarByKoordinatorlukAsync(int koordinatorlukId);
        Task<List<LookupItemVm>> GetPersonellerByKomisyonAsync(int komisyonId);
    }
}
