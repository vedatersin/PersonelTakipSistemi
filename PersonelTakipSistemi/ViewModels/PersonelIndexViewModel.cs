using System.Collections.Generic;

namespace PersonelTakipSistemi.ViewModels
{
    public class PersonelIndexViewModel
    {
        public PersonelIndexFilterViewModel Filter { get; set; } = new PersonelIndexFilterViewModel();
        public List<PersonelIndexRowViewModel> Results { get; set; } = new List<PersonelIndexRowViewModel>();
        public PaginationInfoViewModel Pagination { get; set; } = new PaginationInfoViewModel();
        public LookupListsViewModel Lookups { get; set; } = new LookupListsViewModel();
    }

    public class PersonelIndexFilterViewModel
    {
        public string? SearchName { get; set; }
        public string? TcKimlikNo { get; set; }

        // Dropdown selections
        // Dropdown selections
        public int? BransId { get; set; }
        public int? GorevliIlId { get; set; }
        public DateTime? DogumBaslangic { get; set; }
        
        public List<int>? SeciliYazilimIdleri { get; set; }
        public List<int>? SeciliUzmanlikIdleri { get; set; }
        public List<int>? SeciliGorevTuruIdleri { get; set; }
        public List<int>? SeciliIsNiteligiIdleri { get; set; }

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class PersonelIndexRowViewModel
    {
        public int PersonelId { get; set; }
        public string AdSoyad { get; set; } = null!;
        public string? Brans { get; set; }
        public string? GorevliIl { get; set; }
        public string Eposta { get; set; } = null!;
        public bool AktifMi { get; set; }
        public string? FotografYolu { get; set; }
    }

    public class PaginationInfoViewModel
    {
        public int CurrentPage { get; set; }
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalItems / ItemsPerPage);
        
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
    }

    public class LookupListsViewModel
    {
        public List<LookupItemVm> Branslar { get; set; } = new List<LookupItemVm>();
        public List<LookupItemVm> Iller { get; set; } = new List<LookupItemVm>();
        public List<LookupItemVm> Yazilimlar { get; set; } = new List<LookupItemVm>();
        public List<LookupItemVm> Uzmanliklar { get; set; } = new List<LookupItemVm>();
        public List<LookupItemVm> GorevTurleri { get; set; } = new List<LookupItemVm>();
        public List<LookupItemVm> IsNitelikleri { get; set; } = new List<LookupItemVm>();
    }
}
