using PersonelTakipSistemi.ViewModels;

namespace PersonelTakipSistemi.Services
{
    public interface IPersonelLookupService
    {
        Task FillIndexLookupsAsync(LookupListsViewModel model, PersonelIndexFilterViewModel? filter = null);
        Task<PersonelFormLookupData> FillFormLookupsAsync(PersonelEkleViewModel model);
        Task<List<LookupItemVm>> GetIlceLookupItemsAsync(int ilId);
    }
}
