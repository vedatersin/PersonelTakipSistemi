using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PersonelTakipSistemi.Data;
using PersonelTakipSistemi.Models.ViewModels;

namespace PersonelTakipSistemi.Services
{
    public class PersonelAuthorizationService : IPersonelAuthorizationService
    {
        private readonly TegmPersonelTakipDbContext _context;

        public PersonelAuthorizationService(TegmPersonelTakipDbContext context)
        {
            _context = context;
        }

        public async Task<YetkilendirmeIndexViewModel> BuildAuthorizationIndexAsync(int currentUserId, bool isAdmin, bool isEditor)
        {
            var query = _context.Personeller.AsQueryable();

            if (!isAdmin)
            {
                var authUnits = await _context.PersonelKurumsalRolAtamalari
                    .Where(a => a.PersonelId == currentUserId && (a.KurumsalRolId == 3 || a.KurumsalRolId == 5))
                    .Select(a => new { a.KoordinatorlukId })
                    .ToListAsync();

                if (authUnits.Any())
                {
                    var authKoordIds = authUnits.Select(u => u.KoordinatorlukId!.Value).ToList();
                    var authKomIds = await _context.Komisyonlar
                        .Where(k => authKoordIds.Contains(k.KoordinatorlukId) || authKoordIds.Contains(k.BagliMerkezKoordinatorlukId ?? 0))
                        .Select(k => k.KomisyonId)
                        .ToListAsync();

                    query = query.Where(p =>
                        p.PersonelKoordinatorlukler.Any(pk => authKoordIds.Contains(pk.KoordinatorlukId)) ||
                        p.PersonelKomisyonlar.Any(pk => authKomIds.Contains(pk.KomisyonId)) ||
                        p.PersonelId == currentUserId);
                }
                else
                {
                    var chairComms = await _context.PersonelKurumsalRolAtamalari
                        .Where(a => a.PersonelId == currentUserId && a.KurumsalRolId == 2)
                        .Select(a => a.KomisyonId!.Value)
                        .ToListAsync();

                    if (chairComms.Any())
                    {
                        query = query.Where(p => p.PersonelKomisyonlar.Any(pk => chairComms.Contains(pk.KomisyonId)) || p.PersonelId == currentUserId);
                    }
                    else if (!isEditor)
                    {
                        query = query.Where(p => p.PersonelId == currentUserId);
                    }
                }
            }

            var personeller = await query
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
                KomisyonAdlari = p.PersonelKomisyonlar.Select(pk =>
                    pk.Komisyon.BagliMerkezKoordinatorlukId != null && pk.Komisyon.Koordinatorluk?.Il != null
                    ? $"{pk.Komisyon.Koordinatorluk.Il.Ad} Komisyonu"
                    : pk.Komisyon.Ad
                ).ToList(),
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
                TeskilatList = await _context.Teskilatlar.Select(t => new SelectListItem { Value = t.TeskilatId.ToString(), Text = t.Ad }).ToListAsync(),
                KurumsalRolList = await _context.KurumsalRoller.Select(r => new SelectListItem { Value = r.KurumsalRolId.ToString(), Text = r.Ad }).ToListAsync(),
                SistemRolList = await _context.SistemRoller.OrderBy(r => r.Ad).Select(r => new SelectListItem { Value = r.Ad, Text = r.Ad }).ToListAsync(),
                KomisyonList = await _context.Komisyonlar.Include(k => k.Koordinatorluk).ThenInclude(k => k.Il)
                    .Select(k => k.BagliMerkezKoordinatorlukId != null && k.Koordinatorluk != null && k.Koordinatorluk.Il != null
                        ? $"{k.Koordinatorluk.Il.Ad} Komisyonu"
                        : k.Ad)
                    .Distinct()
                    .Select(ad => new SelectListItem { Value = ad, Text = ad })
                    .ToListAsync(),
                KoordinatorlukList = await _context.Koordinatorlukler.Select(k => new SelectListItem { Value = k.Ad, Text = k.Ad }).Distinct().ToListAsync(),
                BransList = await _context.Branslar.Select(x => new SelectListItem { Value = x.Ad, Text = x.Ad }).ToListAsync(),
                YazilimList = await _context.Yazilimlar.Select(x => new SelectListItem { Value = x.Ad, Text = x.Ad }).ToListAsync(),
                UzmanlikList = await _context.Uzmanliklar.Select(x => new SelectListItem { Value = x.Ad, Text = x.Ad }).ToListAsync(),
                GorevTuruList = await _context.GorevTurleri.Select(x => new SelectListItem { Value = x.Ad, Text = x.Ad }).ToListAsync(),
                IsNiteligiList = await _context.IsNitelikleri.Select(x => new SelectListItem { Value = x.Ad, Text = x.Ad }).ToListAsync()
            };
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

            if (isAdmin)
            {
                model.AllTeskilatlar = await _context.Teskilatlar.Select(x => new LookupItemViewModel { Id = x.TeskilatId, Ad = x.Ad }).ToListAsync();
                model.AllKoordinatorlukler = await _context.Koordinatorlukler.Select(x => new LookupItemViewModel { Id = x.KoordinatorlukId, Ad = x.Ad, ParentId = x.TeskilatId }).ToListAsync();
                model.AllKomisyonlar = await _context.Komisyonlar.Include(k => k.Koordinatorluk).ThenInclude(koord => koord.Il)
                    .Select(x => new LookupItemViewModel
                    {
                        Id = x.KomisyonId,
                        Ad = x.BagliMerkezKoordinatorlukId != null && x.Koordinatorluk != null && x.Koordinatorluk.Il != null ? $"{x.Koordinatorluk.Il.Ad} {x.Ad}" : x.Ad,
                        ParentId = x.KoordinatorlukId,
                        BagliMerkezKoordinatorlukId = x.BagliMerkezKoordinatorlukId
                    })
                    .ToListAsync();
            }
            else
            {
                var authUnits = await _context.PersonelKurumsalRolAtamalari
                    .Where(a => a.PersonelId == currentUserId && (a.KurumsalRolId == 3 || a.KurumsalRolId == 5))
                    .Select(a => new { a.TeskilatId, a.KoordinatorlukId })
                    .ToListAsync();

                var authKoordIds = authUnits.Select(u => u.KoordinatorlukId!.Value).ToList();
                var authTesId = authUnits.FirstOrDefault()?.TeskilatId;

                model.AllTeskilatlar = await _context.Teskilatlar.Where(t => t.TeskilatId == authTesId)
                    .Select(x => new LookupItemViewModel { Id = x.TeskilatId, Ad = x.Ad })
                    .ToListAsync();

                model.AllKoordinatorlukler = await _context.Koordinatorlukler.Where(k => authKoordIds.Contains(k.KoordinatorlukId))
                    .Select(x => new LookupItemViewModel { Id = x.KoordinatorlukId, Ad = x.Ad, ParentId = x.TeskilatId })
                    .ToListAsync();

                model.AllKomisyonlar = await _context.Komisyonlar
                    .Include(k => k.Koordinatorluk).ThenInclude(koord => koord.Il)
                    .Where(k => authKoordIds.Contains(k.KoordinatorlukId) || authKoordIds.Contains(k.BagliMerkezKoordinatorlukId ?? 0))
                    .Select(x => new LookupItemViewModel
                    {
                        Id = x.KomisyonId,
                        Ad = x.BagliMerkezKoordinatorlukId != null && x.Koordinatorluk != null && x.Koordinatorluk.Il != null ? $"{x.Koordinatorluk.Il.Ad} {x.Ad}" : x.Ad,
                        ParentId = x.KoordinatorlukId,
                        BagliMerkezKoordinatorlukId = x.BagliMerkezKoordinatorlukId
                    })
                    .ToListAsync();
            }

            return model;
        }
    }
}
