namespace PersonelTakipSistemi.Services
{
    public interface IPersonelAssignmentService
    {
        Task<PersonelAssignmentResult> AddTeskilatAsync(int personelId, int teskilatId, int currentUserId, string? currentUserName);
        Task<PersonelAssignmentResult> RemoveTeskilatAsync(int personelId, int teskilatId, int currentUserId, string? currentUserName);
        Task<PersonelAssignmentResult> AddKoordinatorlukAsync(int personelId, int koordinatorlukId, int currentUserId);
        Task<PersonelAssignmentResult> RemoveKoordinatorlukAsync(int personelId, int koordinatorlukId, int currentUserId);
        Task<PersonelAssignmentResult> AddKomisyonAsync(int personelId, int komisyonId, int currentUserId);
        Task<PersonelAssignmentResult> RemoveKomisyonAsync(int personelId, int komisyonId, int currentUserId);
        Task<PersonelAssignmentResult> AddKurumsalRolAsync(int personelId, int kurumsalRolId, int? koordinatorlukId, int? komisyonId, bool force, int currentUserId);
        Task<PersonelAssignmentResult> RemoveKurumsalRolAsync(int assignmentId, int currentUserId);
    }
}
