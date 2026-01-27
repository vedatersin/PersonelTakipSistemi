using System.Collections.Generic;

namespace PersonelTakipSistemi.Models.ViewModels
{
    public class GorevAtamaViewModel
    {
        public int GorevId { get; set; }
        
        // Incoming IDs
        public List<int> TeskilatIds { get; set; } = new List<int>();
        public List<int> KoordinatorlukIds { get; set; } = new List<int>();
        public List<int> KomisyonIds { get; set; } = new List<int>();
        public List<int> PersonelIds { get; set; } = new List<int>();
    }

    public class GorevAtamaResultViewModel
    {
        public int GorevId { get; set; }
        public List<IdNamePair> Teskilatlar { get; set; } = new List<IdNamePair>();
        public List<IdNamePair> Koordinatorlukler { get; set; } = new List<IdNamePair>();
        public List<IdNamePair> Komisyonlar { get; set; } = new List<IdNamePair>();
        public List<IdNamePair> Personeller { get; set; } = new List<IdNamePair>();
    }

    public class IdNamePair
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string? Type { get; set; } // "Merkez", "Taşra" etc for badges
    }
}
