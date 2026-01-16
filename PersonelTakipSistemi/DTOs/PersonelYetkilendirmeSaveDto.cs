using System.Collections.Generic;

namespace PersonelTakipSistemi.DTOs
{
    public class PersonelYetkilendirmeSaveDto
    {
        public int PersonelId { get; set; }
        public string? SistemRol { get; set; } // "Admin", "Yönetici", etc. Name is easier to map from UI
        public List<int> TeskilatIds { get; set; } = new();
        public List<int> KoordinatorlukIds { get; set; } = new();
        public List<int> KomisyonIds { get; set; } = new();
        public List<PersonelGorevDto> Gorevler { get; set; } = new();
    }

    public class PersonelGorevDto 
    {
        public int KurumsalRolId { get; set; }
        public int? KoordinatorlukId { get; set; } // Context
        public int? KomisyonId { get; set; } // Context
    }
}
