namespace PersonelTakipSistemi.Services
{
    public sealed class PersonelFormLookupData
    {
        public List<PersonelHierarchyItemDto> AllKoordinatorlukler { get; init; } = new();
        public List<PersonelHierarchyKomisyonItemDto> AllKomisyonlar { get; init; } = new();
    }

    public sealed class PersonelHierarchyItemDto
    {
        public int Id { get; init; }
        public string Ad { get; init; } = string.Empty;
        public int ParentId { get; init; }
    }

    public sealed class PersonelHierarchyKomisyonItemDto
    {
        public int Id { get; init; }
        public string Ad { get; init; } = string.Empty;
        public int ParentId { get; init; }
        public int? BagliMerkezKoordinatorlukId { get; init; }
    }
}
