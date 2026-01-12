using Microsoft.AspNetCore.Mvc.Rendering;

namespace PersonelTakipSistemi.Models.ViewModels
{
    public class YetkilendirmeIndexViewModel
    {
        public List<PersonelYetkiRowViewModel> Personeller { get; set; } = new List<PersonelYetkiRowViewModel>();

        // Filters (Initial Load)
        public List<SelectListItem> TeskilatList { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> SistemRolList { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> KurumsalRolList { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> KomisyonList { get; set; } = new List<SelectListItem>();
    }

    public class PersonelYetkiRowViewModel
    {
        public int PersonelId { get; set; }
        public string AdSoyad { get; set; } = null!;
        public string? FotografYolu { get; set; }
        public string SistemRol { get; set; } = null!;

        // Display Lists (Strings for UI Chips)
        public List<string> TeskilatAdlari { get; set; } = new List<string>();
        public List<string> KoordinatorlukAdlari { get; set; } = new List<string>();
        public List<string> KomisyonAdlari { get; set; } = new List<string>();
        public List<string> KurumsalRolAdlari { get; set; } = new List<string>();
    }

    public class PersonelYetkiDetailViewModel
    {
        public int PersonelId { get; set; }
        public string AdSoyad { get; set; } = null!;
        public string? FotografYolu { get; set; }
        public string SistemRol { get; set; } = null!;

        // Selected Ids for Initial State in Drawer
        public List<int> SelectedTeskilatIds { get; set; } = new List<int>();
        public List<int> SelectedKoordinatorlukIds { get; set; } = new List<int>();
        public List<int> SelectedKomisyonIds { get; set; } = new List<int>();
        
        // Assignments Details (for displaying chips with IDs)
        public List<AssignmentViewModel> TeskilatAssignments { get; set; } = new List<AssignmentViewModel>();
        public List<AssignmentViewModel> KoordinatorlukAssignments { get; set; } = new List<AssignmentViewModel>();
        public List<AssignmentViewModel> KomisyonAssignments { get; set; } = new List<AssignmentViewModel>();
        public List<RoleAssignmentViewModel> KurumsalRolAssignments { get; set; } = new List<RoleAssignmentViewModel>();

        // Lookup Lists (Filtered by Context in JS)
        public List<LookupItemViewModel> AllTeskilatlar { get; set; } = new List<LookupItemViewModel>();
        public List<LookupItemViewModel> AllKoordinatorlukler { get; set; } = new List<LookupItemViewModel>();
        public List<LookupItemViewModel> AllKomisyonlar { get; set; } = new List<LookupItemViewModel>();
    }

    public class AssignmentViewModel
    {
        public int Id { get; set; }
        public string Ad { get; set; } = null!;
    }

    public class RoleAssignmentViewModel
    {
        public int AssignmentId { get; set; } // Id from PersonelKurumsalRolAtama
        public int KurumsalRolId { get; set; }
        public string RolAd { get; set; } = null!;
        public string ContextAd { get; set; } = null!; // E.g. "Mardin İl Koordinatörlüğü" or "Matematik Komisyonu"
    }

    public class LookupItemViewModel
    {
        public int Id { get; set; }
        public string Ad { get; set; } = null!;
        public int? ParentId { get; set; } // TeskilatId (for Koord) or KoordinatorlukId (for Komisyon)
    }
}
