using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PersonelTakipSistemi.Data;
using PersonelTakipSistemi.Models.ViewModels;

namespace PersonelTakipSistemi.Services
{
    public class PersonelAuthorizationService : IPersonelAuthorizationService
    {
        private static readonly int[] CoordinatorRoleIds = { 3, 5 };

        private readonly TegmPersonelTakipDbContext _context;

        public PersonelAuthorizationService(TegmPersonelTakipDbContext context)
        {
            _context = context;
        }

        public async Task<YetkilendirmeIndexViewModel> BuildAuthorizationIndexAsync(int currentUserId, bool isAdmin, bool isEditor)
        {
            var scope = await BuildScopeAsync(currentUserId, isAdmin, isEditor);
            var query = ApplyPersonnelScope(_context.Personeller.AsQueryable(), scope, currentUserId);

            var personeller = await query
                .AsNoTracking()
                .AsSplitQuery()
                .Include(p => p.PersonelTeskilatlar).ThenInclude(pt => pt.Teskilat)
                .Include(p => p.PersonelKoordinatorlukler).ThenInclude(pk => pk.Koordinatorluk)
                .Include(p => p.PersonelKomisyonlar).ThenInclude(pk => pk.Komisyon).ThenInclude(k => k.Koordinatorluk).ThenInclude(koord => koord.Il)
                .Include(p => p.PersonelKurumsalRolAtamalari).ThenInclude(pkr => pkr.KurumsalRol)
                .Include(p => p.SistemRol)
                .Include(p => p.Brans)
                .Include(p => p.PersonelYazilimlar).ThenInclude(py => py.Yazilim)
                .Include(p => p.PersonelUzmanliklar).ThenInclude(pu => pu.Uzmanlik)
                .Include(p => p.PersonelGorevTurleri).ThenInclude(pg => pg.GorevTuru)
                .Include(p => p.PersonelIsNitelikleri).ThenInclude(pi => pi.IsNiteligi)
                .OrderBy(p => p.Ad).ThenBy(p => p.Soyad)
                .ToListAsync();

            var rowViewModels = personeller.Select(p => new PersonelYetkiRowViewModel
            {
                PersonelId = p.PersonelId,
                AdSoyad = $"{p.Ad} {p.Soyad}",
                FotografYolu = p.FotografYolu,
                SistemRol = p.SistemRol?.Ad ?? string.Empty,
                AktifMi = p.AktifMi,
                TeskilatAdlari = p.PersonelTeskilatlar.Select(pt => pt.Teskilat.Ad).ToList(),
                KoordinatorlukAdlari = p.PersonelKoordinatorlukler.Select(pk => pk.Koordinatorluk.Ad).ToList(),
                KomisyonAdlari = p.PersonelKomisyonlar.Select(pk => FormatKomisyonAd(pk.Komisyon)).ToList(),
                KurumsalRolAdlari = p.PersonelKurumsalRolAtamalari.Select(ra => ra.KurumsalRol.Ad).Distinct().ToList(),
                Brans = p.Brans?.Ad,
                Yazilimlar = p.PersonelYazilimlar.Select(py => py.Yazilim.Ad).ToList(),
                Uzmanliklar = p.PersonelUzmanliklar.Select(pu => pu.Uzmanlik.Ad).ToList(),
                GorevTurleri = p.PersonelGorevTurleri.Select(pg => pg.GorevTuru.Ad).ToList(),
                IsNitelikleri = p.PersonelIsNitelikleri.Select(pi => pi.IsNiteligi.Ad).ToList(),
                AddedViaTemplate = p.AddedViaTemplate
            }).ToList();

            return new YetkilendirmeIndexViewModel
            {
                Personeller = rowViewModels,
                TeskilatList = await _context.Teskilatlar.AsNoTracking()
                    .Select(t => new SelectListItem { Value = t.TeskilatId.ToString(), Text = t.Ad })
                    .ToListAsync(),
                KurumsalRolList = await _context.KurumsalRoller.AsNoTracking()
                    .Select(r => new SelectListItem { Value = r.KurumsalRolId.ToString(), Text = r.Ad })
                    .ToListAsync(),
                SistemRolList = await _context.SistemRoller.AsNoTracking()
                    .OrderBy(r => r.Ad)
                    .Select(r => new SelectListItem { Value = r.Ad, Text = r.Ad })
                    .ToListAsync(),
                KomisyonList = await BuildKomisyonSelectListAsync(),
                KoordinatorlukList = await _context.Koordinatorlukler.AsNoTracking()
                    .Select(k => k.Ad)
                    .Distinct()
                    .OrderBy(x => x)
                    .Select(ad => new SelectListItem { Value = ad, Text = ad })
                    .ToListAsync(),
                BransList = await _context.Branslar.AsNoTracking()
                    .Select(x => new SelectListItem { Value = x.Ad, Text = x.Ad })
                    .ToListAsync(),
                YazilimList = await _context.Yazilimlar.AsNoTracking()
                    .Select(x => new SelectListItem { Value = x.Ad, Text = x.Ad })
                    .ToListAsync(),
                UzmanlikList = await _context.Uzmanliklar.AsNoTracking()
                    .Select(x => new SelectListItem { Value = x.Ad, Text = x.Ad })
                    .ToListAsync(),
                GorevTuruList = await _context.GorevTurleri.AsNoTracking()
                    .Select(x => new SelectListItem { Value = x.Ad, Text = x.Ad })
                    .ToListAsync(),
                IsNiteligiList = await _context.IsNitelikleri.AsNoTracking()
                    .Select(x => new SelectListItem { Value = x.Ad, Text = x.Ad })
                    .ToListAsync()
            };
        }

        private async Task<List<SelectListItem>> BuildKomisyonSelectListAsync()
        {
            var komisyonlar = await _context.Komisyonlar
                .AsNoTracking()
                .Include(k => k.Koordinatorluk)
                .ThenInclude(k => k.Il)
                .ToListAsync();

            return komisyonlar
                .Select(FormatKomisyonAd)
                .Distinct()
                .OrderBy(x => x)
                .Select(ad => new SelectListItem { Value = ad, Text = ad })
                .ToList();
        }

        public async Task<PersonelYetkiDetailViewModel?> BuildAuthorizationDetailAsync(int personelId, int currentUserId, bool isAdmin)
        {
            var personel = await _context.Personeller
                .Include(x => x.PersonelTeskilatlar).ThenInclude(pt => pt.Teskilat)
                .Include(x => x.PersonelKoordinatorlukler).ThenInclude(pk => pk.Koordinatorluk)
                .Include(x => x.PersonelKomisyonlar).ThenInclude(pk => pk.Komisyon)
                .Include(x => x.PersonelKurumsalRolAtamalari).ThenInclude(pkr => pkr.KurumsalRol)
                .Include(x => x.SistemRol)
                .Include(x => x.PersonelKurumsalRolAtamalari).ThenInclude(pkr => pkr.Koordinatorluk)
                .Include(x => x.PersonelKurumsalRolAtamalari).ThenInclude(pkr => pkr.Komisyon)
                .FirstOrDefaultAsync(x => x.PersonelId == personelId);

            if (personel == null)
            {
                return null;
            }

            var scope = await BuildScopeAsync(currentUserId, isAdmin, isEditor: false);

            var model = new PersonelYetkiDetailViewModel
            {
                PersonelId = personel.PersonelId,
                AdSoyad = $"{personel.Ad} {personel.Soyad}",
                FotografYolu = personel.FotografYolu,
                SistemRol = personel.SistemRol?.Ad ?? string.Empty,
                AktifMi = personel.AktifMi,
                SelectedTeskilatIds = personel.PersonelTeskilatlar.Select(x => x.TeskilatId).ToList(),
                SelectedKoordinatorlukIds = personel.PersonelKoordinatorlukler.Select(x => x.KoordinatorlukId).ToList(),
                SelectedKomisyonIds = personel.PersonelKomisyonlar.Select(x => x.KomisyonId).ToList(),
                TeskilatAssignments = personel.PersonelTeskilatlar.Select(x => new AssignmentViewModel { Id = x.TeskilatId, Ad = x.Teskilat.Ad }).ToList(),
                KoordinatorlukAssignments = personel.PersonelKoordinatorlukler.Select(x => new AssignmentViewModel { Id = x.KoordinatorlukId, Ad = x.Koordinatorluk.Ad }).ToList(),
                KomisyonAssignments = personel.PersonelKomisyonlar.Select(x => new AssignmentViewModel { Id = x.KomisyonId, Ad = x.Komisyon.Ad }).ToList(),
                KurumsalRolAssignments = personel.PersonelKurumsalRolAtamalari.Select(x => new RoleAssignmentViewModel
                {
                    AssignmentId = x.Id,
                    KurumsalRolId = x.KurumsalRolId,
                    RolAd = x.KurumsalRol.Ad,
                    ContextAd = x.Koordinatorluk != null ? x.Koordinatorluk.Ad : (x.Komisyon != null ? x.Komisyon.Ad : "Genel"),
                    KoordinatorlukId = x.KoordinatorlukId,
                    KomisyonId = x.KomisyonId
                }).ToList()
            };

            model.AllTeskilatlar = await BuildTeskilatLookupsAsync(scope);
            model.AllKoordinatorlukler = await BuildKoordinatorlukLookupsAsync(scope);
            model.AllKomisyonlar = await BuildKomisyonLookupsAsync(scope);

            return model;
        }

        private async Task<AuthorizationScope> BuildScopeAsync(int currentUserId, bool isAdmin, bool isEditor)
        {
            if (isAdmin)
            {
                return AuthorizationScope.Admin;
            }

            var coordinatorAssignments = await _context.PersonelKurumsalRolAtamalari
                .Where(a => a.PersonelId == currentUserId && CoordinatorRoleIds.Contains(a.KurumsalRolId))
                .Select(a => new { a.TeskilatId, a.KoordinatorlukId })
                .ToListAsync();

            if (coordinatorAssignments.Any())
            {
                var koordinatorlukIds = coordinatorAssignments
                    .Where(x => x.KoordinatorlukId.HasValue)
                    .Select(x => x.KoordinatorlukId!.Value)
                    .Distinct()
                    .ToList();

                var komisyonIds = await GetAuthorizedKomisyonIdsAsync(koordinatorlukIds);

                return AuthorizationScope.Coordinator(
                    coordinatorAssignments.Select(x => x.TeskilatId).FirstOrDefault(),
                    koordinatorlukIds,
                    komisyonIds);
            }

            var chairKomisyonIds = await _context.PersonelKurumsalRolAtamalari
                .Where(a => a.PersonelId == currentUserId && a.KurumsalRolId == 2 && a.KomisyonId.HasValue)
                .Select(a => a.KomisyonId!.Value)
                .ToListAsync();

            if (chairKomisyonIds.Any())
            {
                return AuthorizationScope.Chair(chairKomisyonIds);
            }

            return isEditor ? AuthorizationScope.Editor : AuthorizationScope.SelfOnly;
        }

        private IQueryable<Models.Personel> ApplyPersonnelScope(IQueryable<Models.Personel> query, AuthorizationScope scope, int currentUserId)
        {
            return scope.Kind switch
            {
                AuthorizationScopeKind.Admin => query,
                AuthorizationScopeKind.Editor => query,
                AuthorizationScopeKind.Coordinator => query.Where(p =>
                    p.PersonelKoordinatorlukler.Any(pk => scope.KoordinatorlukIds.Contains(pk.KoordinatorlukId)) ||
                    p.PersonelKomisyonlar.Any(pk => scope.KomisyonIds.Contains(pk.KomisyonId)) ||
                    p.PersonelId == currentUserId),
                AuthorizationScopeKind.Chair => query.Where(p =>
                    p.PersonelKomisyonlar.Any(pk => scope.KomisyonIds.Contains(pk.KomisyonId)) ||
                    p.PersonelId == currentUserId),
                _ => query.Where(p => p.PersonelId == currentUserId)
            };
        }

        private async Task<List<int>> GetAuthorizedKomisyonIdsAsync(List<int> koordinatorlukIds)
        {
            if (koordinatorlukIds.Count == 0)
            {
                return new List<int>();
            }

            return await _context.Komisyonlar
                .Where(k => koordinatorlukIds.Contains(k.KoordinatorlukId) || koordinatorlukIds.Contains(k.BagliMerkezKoordinatorlukId ?? 0))
                .Select(k => k.KomisyonId)
                .ToListAsync();
        }

        private async Task<List<LookupItemViewModel>> BuildTeskilatLookupsAsync(AuthorizationScope scope)
        {
            if (scope.Kind == AuthorizationScopeKind.Admin || scope.Kind == AuthorizationScopeKind.Editor)
            {
                return await _context.Teskilatlar
                    .Select(x => new LookupItemViewModel { Id = x.TeskilatId, Ad = x.Ad })
                    .ToListAsync();
            }

            if (!scope.TeskilatId.HasValue)
            {
                return new List<LookupItemViewModel>();
            }

            return await _context.Teskilatlar
                .Where(t => t.TeskilatId == scope.TeskilatId.Value)
                .Select(x => new LookupItemViewModel { Id = x.TeskilatId, Ad = x.Ad })
                .ToListAsync();
        }

        private async Task<List<LookupItemViewModel>> BuildKoordinatorlukLookupsAsync(AuthorizationScope scope)
        {
            if (scope.Kind == AuthorizationScopeKind.Admin || scope.Kind == AuthorizationScopeKind.Editor)
            {
                return await _context.Koordinatorlukler
                    .Select(x => new LookupItemViewModel { Id = x.KoordinatorlukId, Ad = x.Ad, ParentId = x.TeskilatId })
                    .ToListAsync();
            }

            if (scope.KoordinatorlukIds.Count == 0)
            {
                return new List<LookupItemViewModel>();
            }

            return await _context.Koordinatorlukler
                .Where(k => scope.KoordinatorlukIds.Contains(k.KoordinatorlukId))
                .Select(x => new LookupItemViewModel { Id = x.KoordinatorlukId, Ad = x.Ad, ParentId = x.TeskilatId })
                .ToListAsync();
        }

        private async Task<List<LookupItemViewModel>> BuildKomisyonLookupsAsync(AuthorizationScope scope)
        {
            var query = _context.Komisyonlar
                .Include(k => k.Koordinatorluk)
                .ThenInclude(koord => koord.Il)
                .AsQueryable();

            if (scope.Kind != AuthorizationScopeKind.Admin && scope.Kind != AuthorizationScopeKind.Editor)
            {
                if (scope.KomisyonIds.Count == 0)
                {
                    return new List<LookupItemViewModel>();
                }

                query = query.Where(k => scope.KomisyonIds.Contains(k.KomisyonId));
            }

            return await query
                .Select(x => new LookupItemViewModel
                {
                    Id = x.KomisyonId,
                    Ad = FormatKomisyonLookupAd(x),
                    ParentId = x.KoordinatorlukId,
                    BagliMerkezKoordinatorlukId = x.BagliMerkezKoordinatorlukId
                })
                .ToListAsync();
        }

        private static string FormatKomisyonAd(Models.Komisyon komisyon)
        {
            return komisyon.BagliMerkezKoordinatorlukId != null && komisyon.Koordinatorluk?.Il != null
                ? $"{komisyon.Koordinatorluk.Il.Ad} Komisyonu"
                : komisyon.Ad;
        }

        private static string FormatKomisyonLookupAd(Models.Komisyon komisyon)
        {
            return komisyon.BagliMerkezKoordinatorlukId != null && komisyon.Koordinatorluk?.Il != null
                ? $"{komisyon.Koordinatorluk.Il.Ad} {komisyon.Ad}"
                : komisyon.Ad;
        }

        private sealed record AuthorizationScope(
            AuthorizationScopeKind Kind,
            int? TeskilatId,
            List<int> KoordinatorlukIds,
            List<int> KomisyonIds)
        {
            public static AuthorizationScope Admin { get; } =
                new(AuthorizationScopeKind.Admin, null, new List<int>(), new List<int>());

            public static AuthorizationScope Editor { get; } =
                new(AuthorizationScopeKind.Editor, null, new List<int>(), new List<int>());

            public static AuthorizationScope SelfOnly { get; } =
                new(AuthorizationScopeKind.SelfOnly, null, new List<int>(), new List<int>());

            public static AuthorizationScope Coordinator(int? teskilatId, List<int> koordinatorlukIds, List<int> komisyonIds) =>
                new(AuthorizationScopeKind.Coordinator, teskilatId, koordinatorlukIds, komisyonIds);

            public static AuthorizationScope Chair(List<int> komisyonIds) =>
                new(AuthorizationScopeKind.Chair, null, new List<int>(), komisyonIds);
        }

        private enum AuthorizationScopeKind
        {
            Admin,
            Editor,
            Coordinator,
            Chair,
            SelfOnly
        }
    }
}
